namespace SITP
{
    partial class ConnectTest
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.cmdDisConnect = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtTelnetAddr = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.optTelnet = new System.Windows.Forms.RadioButton();
            this.optSerial = new System.Windows.Forms.RadioButton();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLogClear = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtTelnetLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.cmdDisConnect);
            this.groupBox1.Controls.Add(this.cmdConnect);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 576);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " [연결설정] ";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbEncoding);
            this.groupBox6.Location = new System.Drawing.Point(17, 452);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(181, 47);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " [인코딩] ";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "Default",
            "UTF8",
            "Unicode"});
            this.cmbEncoding.Location = new System.Drawing.Point(8, 19);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(162, 20);
            this.cmbEncoding.TabIndex = 0;
            this.cmbEncoding.Text = "Default";
            this.cmbEncoding.SelectedIndexChanged += new System.EventHandler(this.cmbEncoding_SelectedIndexChanged);
            // 
            // cmdDisConnect
            // 
            this.cmdDisConnect.Location = new System.Drawing.Point(118, 514);
            this.cmdDisConnect.Name = "cmdDisConnect";
            this.cmdDisConnect.Size = new System.Drawing.Size(80, 50);
            this.cmdDisConnect.TabIndex = 7;
            this.cmdDisConnect.Text = "해제";
            this.cmdDisConnect.UseVisualStyleBackColor = true;
            this.cmdDisConnect.Click += new System.EventHandler(this.cmdDisConnect_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(13, 514);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(80, 50);
            this.cmdConnect.TabIndex = 6;
            this.cmdConnect.Text = "연결";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtTelnetAddr);
            this.groupBox5.Location = new System.Drawing.Point(13, 297);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(185, 50);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = " [텔렛서버주소] ";
            // 
            // txtTelnetAddr
            // 
            this.txtTelnetAddr.Location = new System.Drawing.Point(10, 22);
            this.txtTelnetAddr.Name = "txtTelnetAddr";
            this.txtTelnetAddr.Size = new System.Drawing.Size(167, 21);
            this.txtTelnetAddr.TabIndex = 2;
            this.txtTelnetAddr.Text = "127.0.0.1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtPassword);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtID);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(13, 356);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(185, 87);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " [사용자정보] ";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(86, 48);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(90, 21);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "admin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "패스워드";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(85, 21);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(90, 21);
            this.txtID.TabIndex = 2;
            this.txtID.Text = "admin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "아이디";
            // 
            // groupBox3
            // 
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
            this.groupBox3.Location = new System.Drawing.Point(9, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(189, 199);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " 포트셋팅] ";
            // 
            // cmbFlow
            // 
            this.cmbFlow.FormattingEnabled = true;
            this.cmbFlow.Items.AddRange(new object[] {
            "Xon/Xoff",
            "hardware",
            "none"});
            this.cmbFlow.Location = new System.Drawing.Point(86, 166);
            this.cmbFlow.Name = "cmbFlow";
            this.cmbFlow.Size = new System.Drawing.Size(93, 20);
            this.cmbFlow.TabIndex = 11;
            this.cmbFlow.Text = "none";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 170);
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
            this.cmbStop.Location = new System.Drawing.Point(86, 137);
            this.cmbStop.Name = "cmbStop";
            this.cmbStop.Size = new System.Drawing.Size(93, 20);
            this.cmbStop.TabIndex = 9;
            this.cmbStop.Text = "1 bit";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 141);
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
            this.cmbParity.Location = new System.Drawing.Point(86, 110);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(93, 20);
            this.cmbParity.TabIndex = 7;
            this.cmbParity.Text = "none";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 114);
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
            this.cmbData.Location = new System.Drawing.Point(86, 82);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(93, 20);
            this.cmbData.TabIndex = 5;
            this.cmbData.Text = "8 Bit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
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
            this.cmbSpeed.Location = new System.Drawing.Point(86, 53);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(93, 20);
            this.cmbSpeed.TabIndex = 3;
            this.cmbSpeed.Text = "115200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "속도(B)";
            // 
            // cmbPort
            // 
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
            this.cmbPort.Location = new System.Drawing.Point(86, 26);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(93, 20);
            this.cmbPort.TabIndex = 1;
            this.cmbPort.Text = "COM3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "포트(P)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optTelnet);
            this.groupBox2.Controls.Add(this.optSerial);
            this.groupBox2.Location = new System.Drawing.Point(6, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 46);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " [통신방식] ";
            // 
            // optTelnet
            // 
            this.optTelnet.AutoSize = true;
            this.optTelnet.Checked = true;
            this.optTelnet.Location = new System.Drawing.Point(105, 20);
            this.optTelnet.Name = "optTelnet";
            this.optTelnet.Size = new System.Drawing.Size(58, 16);
            this.optTelnet.TabIndex = 1;
            this.optTelnet.TabStop = true;
            this.optTelnet.Text = "Telnet";
            this.optTelnet.UseVisualStyleBackColor = true;
            this.optTelnet.CheckedChanged += new System.EventHandler(this.optTelnet_CheckedChanged);
            // 
            // optSerial
            // 
            this.optSerial.AutoSize = true;
            this.optSerial.Location = new System.Drawing.Point(16, 21);
            this.optSerial.Name = "optSerial";
            this.optSerial.Size = new System.Drawing.Size(55, 16);
            this.optSerial.TabIndex = 0;
            this.optSerial.Text = "Serial";
            this.optSerial.UseVisualStyleBackColor = true;
            this.optSerial.CheckedChanged += new System.EventHandler(this.optSerial_CheckedChanged);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLog.Location = new System.Drawing.Point(3, 17);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(689, 245);
            this.txtLog.TabIndex = 1;
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(275, 570);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(581, 21);
            this.txtCommand.TabIndex = 2;
            this.txtCommand.TextChanged += new System.EventHandler(this.txtCommand_TextChanged);
            this.txtCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommand_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 574);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "입력";
            // 
            // btnLogClear
            // 
            this.btnLogClear.Location = new System.Drawing.Point(862, 567);
            this.btnLogClear.Name = "btnLogClear";
            this.btnLogClear.Size = new System.Drawing.Size(75, 29);
            this.btnLogClear.TabIndex = 6;
            this.btnLogClear.Text = "로그지우기";
            this.btnLogClear.UseVisualStyleBackColor = true;
            this.btnLogClear.Click += new System.EventHandler(this.btnLogClear_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(242, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer1.Size = new System.Drawing.Size(695, 549);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 7;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtLog);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(695, 265);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = " [Comm Log] ";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtTelnetLog);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(695, 280);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = " [Telnet Log]";
            // 
            // txtTelnetLog
            // 
            this.txtTelnetLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelnetLog.Location = new System.Drawing.Point(3, 17);
            this.txtTelnetLog.Multiline = true;
            this.txtTelnetLog.Name = "txtTelnetLog";
            this.txtTelnetLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTelnetLog.Size = new System.Drawing.Size(689, 260);
            this.txtTelnetLog.TabIndex = 0;
            // 
            // ConnectTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 600);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnLogClear);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "장비연결테스트";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optTelnet;
        private System.Windows.Forms.RadioButton optSerial;
        private System.Windows.Forms.Button cmdDisConnect;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtTelnetAddr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLogClear;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtTelnetLog;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}

