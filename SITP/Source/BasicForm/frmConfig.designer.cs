namespace SITP
{
    partial class frmConfig
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkTelnet = new System.Windows.Forms.CheckBox();
            this.txtTelnetAddr = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSerial = new System.Windows.Forms.CheckBox();
            this.cmbFlow = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStop = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbEncode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cmdCancel);
            this.groupBox1.Controls.Add(this.cmdOK);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 269);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " [연결설정] ";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(474, 82);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(80, 46);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "취소";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(474, 33);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(80, 46);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "적용";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.chkTelnet);
            this.groupBox5.Controls.Add(this.txtTelnetAddr);
            this.groupBox5.Location = new System.Drawing.Point(219, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(238, 80);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = " [텔렛서버] ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "서버아이피";
            // 
            // chkTelnet
            // 
            this.chkTelnet.AutoSize = true;
            this.chkTelnet.Location = new System.Drawing.Point(10, 30);
            this.chkTelnet.Name = "chkTelnet";
            this.chkTelnet.Size = new System.Drawing.Size(96, 16);
            this.chkTelnet.TabIndex = 13;
            this.chkTelnet.Text = "텔렛서버접속";
            this.chkTelnet.UseVisualStyleBackColor = true;
            this.chkTelnet.CheckedChanged += new System.EventHandler(this.chkTelnet_CheckedChanged);
            // 
            // txtTelnetAddr
            // 
            this.txtTelnetAddr.Location = new System.Drawing.Point(79, 50);
            this.txtTelnetAddr.Name = "txtTelnetAddr";
            this.txtTelnetAddr.Size = new System.Drawing.Size(143, 21);
            this.txtTelnetAddr.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPassword);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtID);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(219, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(238, 82);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " [사용자정보] ";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(86, 49);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(90, 21);
            this.txtPassword.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "패스워드";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(85, 22);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(90, 21);
            this.txtID.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "아이디";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSerial);
            this.groupBox3.Controls.Add(this.cmbFlow);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbStop);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbParity);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cmbData);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbSpeed);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cmbPort);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(9, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(189, 229);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "  [시리얼포트] ";
            // 
            // chkSerial
            // 
            this.chkSerial.AutoSize = true;
            this.chkSerial.Checked = true;
            this.chkSerial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSerial.Location = new System.Drawing.Point(14, 30);
            this.chkSerial.Name = "chkSerial";
            this.chkSerial.Size = new System.Drawing.Size(106, 16);
            this.chkSerial.TabIndex = 12;
            this.chkSerial.Text = "Serial Port사용";
            this.chkSerial.UseVisualStyleBackColor = true;
            this.chkSerial.CheckedChanged += new System.EventHandler(this.chkSerial_CheckedChanged);
            // 
            // cmbFlow
            // 
            this.cmbFlow.FormattingEnabled = true;
            this.cmbFlow.Items.AddRange(new object[] {
            "Xon/Xoff",
            "hardware",
            "none"});
            this.cmbFlow.Location = new System.Drawing.Point(86, 200);
            this.cmbFlow.Name = "cmbFlow";
            this.cmbFlow.Size = new System.Drawing.Size(93, 20);
            this.cmbFlow.TabIndex = 11;
            this.cmbFlow.Text = "none";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "흐름제어(F)";
            // 
            // cmbStop
            // 
            this.cmbStop.FormattingEnabled = true;
            this.cmbStop.Items.AddRange(new object[] {
            "1 bit",
            "1.5 bit",
            "2 bit"});
            this.cmbStop.Location = new System.Drawing.Point(86, 171);
            this.cmbStop.Name = "cmbStop";
            this.cmbStop.Size = new System.Drawing.Size(93, 20);
            this.cmbStop.TabIndex = 9;
            this.cmbStop.Text = "1 bit";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "스탑비트(S)";
            // 
            // cmbParity
            // 
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "none",
            "odd",
            "even",
            "mark",
            "space"});
            this.cmbParity.Location = new System.Drawing.Point(86, 144);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(93, 20);
            this.cmbParity.TabIndex = 7;
            this.cmbParity.Text = "none";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "패리티(A)";
            // 
            // cmbData
            // 
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Items.AddRange(new object[] {
            "7 Bit",
            "8 Bit"});
            this.cmbData.Location = new System.Drawing.Point(86, 116);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(93, 20);
            this.cmbData.TabIndex = 5;
            this.cmbData.Text = "8 Bit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "데이터(D)";
            // 
            // cmbSpeed
            // 
            this.cmbSpeed.FormattingEnabled = true;
            this.cmbSpeed.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600",
            "",
            "",
            "",
            "",
            ""});
            this.cmbSpeed.Location = new System.Drawing.Point(86, 87);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(93, 20);
            this.cmbSpeed.TabIndex = 3;
            this.cmbSpeed.Text = "115200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "속도(B)";
            // 
            // cmbPort
            // 
            this.cmbPort.Enabled = false;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12"});
            this.cmbPort.Location = new System.Drawing.Point(86, 60);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(93, 20);
            this.cmbPort.TabIndex = 1;
            this.cmbPort.Text = "COM3";
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.cmbPort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "포트(P)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbEncode);
            this.groupBox2.Location = new System.Drawing.Point(219, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 49);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " [인코딩] ";
            // 
            // cmbEncode
            // 
            this.cmbEncode.FormattingEnabled = true;
            this.cmbEncode.Items.AddRange(new object[] {
            "Default",
            "UTF8",
            "Unicode"});
            this.cmbEncode.Location = new System.Drawing.Point(10, 18);
            this.cmbEncode.Name = "cmbEncode";
            this.cmbEncode.Size = new System.Drawing.Size(212, 20);
            this.cmbEncode.TabIndex = 0;
            this.cmbEncode.Text = "Default";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 299);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "장비연결속성";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbFlow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtTelnetAddr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkTelnet;
        private System.Windows.Forms.CheckBox chkSerial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbEncode;
    }
}

