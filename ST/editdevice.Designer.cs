namespace ST
{
    partial class editdevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editdevice));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.devicetype = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.ready = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ooriin = new DevExpress.XtraEditors.ComboBoxEdit();
            this.power = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.too = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.made = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.producted = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.ulsdugaar = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.mark = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.URL11 = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.simpleButtonFile = new DevExpress.XtraEditors.SimpleButton();
            this.label18 = new System.Windows.Forms.Label();
            this.ognoo = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ner = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.deviceID = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.devicetype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ready.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ooriin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.power.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.too.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.made.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.producted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ulsdugaar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL11.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ognoo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ognoo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ner.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "\"pdf files (*.pdf)|*.pdf|Pictures (*.jpg)|*.jpg|All files (*.*)|*.*\"";
            this.openFileDialog1.Title = "PDF, JPG";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // devicetype
            // 
            this.devicetype.Location = new System.Drawing.Point(359, 67);
            this.devicetype.Name = "devicetype";
            this.devicetype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.devicetype.Properties.Items.AddRange(new object[] {
            "механизм",
            "багаж, тоног төхөөрөмж",
            "ХАБЭА хэрэгсэл"});
            this.devicetype.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.devicetype.Size = new System.Drawing.Size(106, 20);
            this.devicetype.TabIndex = 7;
            this.devicetype.SelectedIndexChanged += new System.EventHandler(this.devicetype_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Tөрөл:";
            // 
            // ready
            // 
            this.ready.Location = new System.Drawing.Point(537, 41);
            this.ready.Name = "ready";
            this.ready.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ready.Properties.Items.AddRange(new object[] {
            "ажилд бэлэн",
            "засвартай"});
            this.ready.Size = new System.Drawing.Size(144, 20);
            this.ready.TabIndex = 5;
            // 
            // ooriin
            // 
            this.ooriin.Location = new System.Drawing.Point(119, 41);
            this.ooriin.Name = "ooriin";
            this.ooriin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ooriin.Properties.Items.AddRange(new object[] {
            "өөрийн ",
            "түрээсийн"});
            this.ooriin.Size = new System.Drawing.Size(153, 20);
            this.ooriin.TabIndex = 3;
            // 
            // power
            // 
            this.power.Location = new System.Drawing.Point(537, 67);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(144, 20);
            this.power.TabIndex = 8;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(468, 70);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(67, 13);
            this.labelControl10.TabIndex = 41;
            this.labelControl10.Text = "Хүчин чадал:";
            // 
            // too
            // 
            this.too.Location = new System.Drawing.Point(359, 41);
            this.too.Name = "too";
            this.too.Size = new System.Drawing.Size(105, 20);
            this.too.TabIndex = 4;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(319, 44);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(34, 13);
            this.labelControl7.TabIndex = 38;
            this.labelControl7.Text = "Тоо/ш:";
            // 
            // made
            // 
            this.made.Location = new System.Drawing.Point(119, 67);
            this.made.Name = "made";
            this.made.Size = new System.Drawing.Size(153, 20);
            this.made.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(20, 70);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(93, 13);
            this.labelControl8.TabIndex = 36;
            this.labelControl8.Text = "Үйлдвэрлэсэн улс:";
            // 
            // producted
            // 
            this.producted.Location = new System.Drawing.Point(119, 93);
            this.producted.Name = "producted";
            this.producted.Size = new System.Drawing.Size(153, 20);
            this.producted.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(25, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(88, 13);
            this.labelControl6.TabIndex = 34;
            this.labelControl6.Text = "Үйлдвэрлэсэн он:";
            // 
            // ulsdugaar
            // 
            this.ulsdugaar.Location = new System.Drawing.Point(359, 93);
            this.ulsdugaar.Name = "ulsdugaar";
            this.ulsdugaar.Size = new System.Drawing.Size(105, 20);
            this.ulsdugaar.TabIndex = 10;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(278, 96);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 13);
            this.labelControl5.TabIndex = 32;
            this.labelControl5.Text = "Улсын дугаар:";
            // 
            // mark
            // 
            this.mark.Location = new System.Drawing.Point(430, 12);
            this.mark.Name = "mark";
            this.mark.Size = new System.Drawing.Size(251, 20);
            this.mark.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(394, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 13);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "Марк:";
            // 
            // URL11
            // 
            this.URL11.Enabled = false;
            this.URL11.Location = new System.Drawing.Point(119, 133);
            this.URL11.Name = "URL11";
            this.URL11.Size = new System.Drawing.Size(242, 20);
            this.URL11.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Файл:";
            // 
            // simpleButtonFile
            // 
            this.simpleButtonFile.Location = new System.Drawing.Point(383, 131);
            this.simpleButtonFile.Name = "simpleButtonFile";
            this.simpleButtonFile.Size = new System.Drawing.Size(71, 22);
            this.simpleButtonFile.TabIndex = 12;
            this.simpleButtonFile.Text = "Файл...";
            this.simpleButtonFile.Click += new System.EventHandler(this.simpleButtonFile_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(494, 96);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "Oгноо:";
            // 
            // ognoo
            // 
            this.ognoo.EditValue = null;
            this.ognoo.Location = new System.Drawing.Point(537, 93);
            this.ognoo.Name = "ognoo";
            this.ognoo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ognoo.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.ognoo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ognoo.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.ognoo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ognoo.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.ognoo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ognoo.Size = new System.Drawing.Size(144, 20);
            this.ognoo.TabIndex = 11;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(51, 48);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Өөрийн эсэх:";
            // 
            // ner
            // 
            this.ner.Location = new System.Drawing.Point(119, 12);
            this.ner.Name = "ner";
            this.ner.Size = new System.Drawing.Size(257, 20);
            this.ner.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(95, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Нэр:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(479, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(56, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Бэлэн эсэх:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.deviceID);
            this.panelControl1.Controls.Add(this.devicetype);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.ready);
            this.panelControl1.Controls.Add(this.ooriin);
            this.panelControl1.Controls.Add(this.power);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.too);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.made);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.producted);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.ulsdugaar);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.mark);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.URL11);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.simpleButtonFile);
            this.panelControl1.Controls.Add(this.label18);
            this.panelControl1.Controls.Add(this.ognoo);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.ner);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(695, 192);
            this.panelControl1.TabIndex = 5;
            // 
            // deviceID
            // 
            this.deviceID.Location = new System.Drawing.Point(119, 159);
            this.deviceID.Name = "deviceID";
            this.deviceID.Size = new System.Drawing.Size(105, 20);
            this.deviceID.TabIndex = 43;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(537, 131);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(144, 44);
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "Хадгалах";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // editdevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 192);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.Name = "editdevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Тоног төхөөрөмжийн мэдээлэл засах";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editdevice_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.devicetype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ready.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ooriin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.power.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.too.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.made.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.producted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ulsdugaar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.URL11.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ognoo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ognoo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ner.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.TextEdit power;
        public DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit too;
        public DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.TextEdit made;
        public DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.TextEdit producted;
        public DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit ulsdugaar;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit mark;
        public DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit URL11;
        public System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.SimpleButton simpleButtonFile;
        public System.Windows.Forms.Label label18;
        public DevExpress.XtraEditors.DateEdit ognoo;
        public DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit ner;
        public DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ComboBoxEdit devicetype;
        public DevExpress.XtraEditors.ComboBoxEdit ready;
        public DevExpress.XtraEditors.ComboBoxEdit ooriin;
        public DevExpress.XtraEditors.TextEdit deviceID;
    }
}