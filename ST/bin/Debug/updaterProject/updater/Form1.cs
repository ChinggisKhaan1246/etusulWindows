using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

// SharpZipLib
using ICSharpCode.SharpZipLib.Zip;

namespace updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updater();
        }

        private void updater()
        {
            // --- Тохиргоо ---
            
            string updateUrl = "http://etusul.com/downloads/updates/DS.zip";

            // updater.exe байрлал
            string updaterPath = Application.StartupPath;

            // ЭЦЭГ хавтас = updater.exe-ийн гадна талын хавтас (энд DS.exe байрлана)
            string clientAppPath = Directory.GetParent(updaterPath).FullName;

            // Түр файлуудыг системийн TEMP дээр хадгална
            string tmp = Path.GetTempPath();
            string zipPath = Path.Combine(tmp, "DS_update.zip");            // татах түр файл
            string extractPath = Path.Combine(tmp, "DS_update_temp");       // задлах түр хавтас

            string newExePath = Path.Combine(extractPath, "DS.exe");     // ZIP-ээс гаргах шинэ exe
            string targetExePath = Path.Combine(clientAppPath, "DS.exe");   // сольж тавих зорилтот exe
            string backupPath = Path.Combine(clientAppPath, "DS_old.exe");// бэкап нэр

            // 1) DS.exe хаагдсан эсэхийг хүлээнэ
            while (IsProcessRunning("DS"))
            {
                // DS.exe ажиллаж байвал 2 сек тутамд шалгана
                Thread.Sleep(2000);
            }

            // 2) Түр хавтсыг цэвэрлээд бэлдэнэ
            SafeDeleteFile(zipPath);
            SafeDeleteDir(extractPath);
            Directory.CreateDirectory(extractPath);

            using (var client = new WebClient())
            {
                // HTTPS-р шилжвэл TLS 1.2 идэвхжүүлэх хэрэг гарч магадгүй:
                // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // Tls12

                client.DownloadProgressChanged += (s, ev) =>
                {
                    try { progressBar1.Value = ev.ProgressPercentage; }
                    catch { /* progressBar байхгүй/хаалттай үед алдаа залгина */ }
                };

                client.DownloadFileCompleted += (s, ev) =>
                {
                    if (ev.Error != null)
                    {
                        MessageBox.Show("Файл татахад алдаа гарлаа: " + ev.Error.Message, "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        // 3) ZIP-ээс зөвхөн DS.exe-г гаргана
                        ExtractZipOnlyDsExe(zipPath, extractPath);

                        if (!File.Exists(newExePath))
                            throw new FileNotFoundException("ZIP дотор DS.exe олдсонгүй.");

                        SafeDeleteFile(backupPath);
                        if (File.Exists(targetExePath))
                        {
                            // зарим OS lock-ийг давж гарахын тулд 3 удаа оролдъё
                            MoveWithRetry(targetExePath, backupPath, 3, 500);
                        }

                        // Шинэ DS.exe-г тавина
                        CopyWithRetry(newExePath, targetExePath, true, 3, 500);

                        // 5) Түр файлуудыг цэвэрлэх
                        SafeDeleteFile(zipPath);
                        SafeDeleteDir(extractPath);
                        MessageBox.Show("Шинэчлэл амжилттай хийгдлээ.");
                       
                        // 6) Шинэ DS.exe-г асаана
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = targetExePath,
                            UseShellExecute = true
                            // Админ эрхээр асаах шаардлагатай бол:
                            // Verb = "runas"
                        });

                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Шинэчлэл хийхэд алдаа гарлаа: " + ex.Message,
                            "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                try
                {
                    client.DownloadFileAsync(new Uri(updateUrl), zipPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Таталт эхлүүлэхэд алдаа гарлаа: " + ex.Message,
                        "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        // ZIP-ээс зөвхөн DS.exe-г гаргаж авах
        private void ExtractZipOnlyDsExe(string zipFilePath, string destDir)
        {
            using (var fs = File.OpenRead(zipFilePath))
            using (var zis = new ZipInputStream(fs))
            {
                ZipEntry entry;
                while ((entry = zis.GetNextEntry()) != null)
                {
                    if (!entry.IsFile) continue;
                    if (!string.Equals(Path.GetFileName(entry.Name), "DS.exe", StringComparison.OrdinalIgnoreCase))
                        continue;

                    Directory.CreateDirectory(destDir);
                    string outPath = Path.Combine(destDir, "DS.exe");
                    using (var outFile = File.Create(outPath))
                    {
                        zis.CopyTo(outFile);
                    }
                    break; // олмогц зогсооно
                }
            }
        }

        // Туслах: аюулгүй устгал
        private void SafeDeleteFile(string path)
        {
            try { if (File.Exists(path)) File.Delete(path); }
            catch { }
        }
        private void SafeDeleteDir(string path)
        {
            try { if (Directory.Exists(path)) Directory.Delete(path, true); }
            catch { }
        }

        // Туслах: retry-тай move/copy (файл lock-оос сэргийлэх)
        private void MoveWithRetry(string src, string dst, int attempts, int delayMs)
        {
            for (int i = 0; i < attempts; i++)
            {
                try { File.Move(src, dst); return; }
                catch
                {
                    if (i == attempts - 1) throw;
                    Thread.Sleep(delayMs);
                }
            }
        }

        private void CopyWithRetry(string src, string dst, bool overwrite, int attempts, int delayMs)
        {
            for (int i = 0; i < attempts; i++)
            {
                try { File.Copy(src, dst, overwrite); return; }
                catch
                {
                    if (i == attempts - 1) throw;
                    Thread.Sleep(delayMs);
                }
            }
        }
    }
}
