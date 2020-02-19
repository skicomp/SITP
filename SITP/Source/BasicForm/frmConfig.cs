using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SITP
{
    public partial class frmConfig : Form
    {
        public Form1 mainForm { get; set; }

        public frmConfig()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strGetText = mainForm.getPortItem(1);
            if (strGetText != "") this.cmbPort.Text = strGetText;

            DispPort();
        }

        private void DispPort()
        {
            string strPort = this.cmbPort.Text;

            for (int i = 0; i < mainForm.portManager.PORTDatas.Count; i ++)
            {
                PORTData portData = mainForm.portManager.PORTDatas[i];
                if (strPort == portData.PortName)
                {
                    if (portData.bUseComm) this.chkSerial.Checked = true;
                    else this.chkSerial.Checked = false;

                    this.cmbData.Text = portData.CommData;
                    this.cmbFlow.Text = portData.CommFlow;
                    this.cmbParity.Text = portData.CommParity;
                    this.cmbSpeed.Text = Convert.ToString(portData.nCommSpeed);
                    this.cmbStop.Text = portData.CommStop;

                    if (portData.bUseTelnet) this.chkTelnet.Checked = true;
                    else this.chkTelnet.Checked = false;

                    this.txtTelnetAddr.Text = portData.TelnetServer;
                    this.txtID.Text = portData.UserID;
                    this.txtPassword.Text = portData.Password;
                    this.cmbEncode.Text = portData.Encoding;
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            string strTemp = "";
            string strData = "";
            strData = strData + this.cmbPort.Text;
            if (this.chkSerial.Checked) strTemp = "checked";
            else strTemp = "unchecked";
            strData = strData + ";" + strTemp;
            strData = strData + ";" + this.cmbSpeed.Text;
            strData = strData + ";" + this.cmbData.Text;
            strData = strData + ";" + this.cmbParity.Text;
            strData = strData + ";" + this.cmbStop.Text;
            strData = strData + ";" + this.cmbFlow.Text;
            if (this.txtTelnetAddr.Text == "") strData = strData + ";Empty";
            else strData = strData + ";" + this.txtTelnetAddr.Text;
            if (this.chkTelnet.Checked) strTemp = "checked";
            else strTemp = "unchecked";
            strData = strData + ";" + strTemp;
            if (this.txtID.Text == "") strData = strData + ";Empty";
            else strData = strData + ";" + this.txtID.Text;
            if (this.txtPassword.Text == "") strData = strData + ";Empty";
            else strData = strData + ";" + this.txtPassword.Text;
            if (this.cmbEncode.Text == "") strData = strData + ";Empty";
            else strData = strData + ";" + this.cmbEncode.Text;

            this.mainForm.saveCommData(strData);

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSerial.Checked)
            {
                //this.cmbPort.Enabled = true;
                this.cmbData.Enabled = true;
                this.cmbFlow.Enabled = true;
                this.cmbParity.Enabled = true;
                this.cmbSpeed.Enabled = true;
                this.cmbStop.Enabled = true;
            } else
            {
                //this.cmbPort.Enabled = false;
                this.cmbData.Enabled = false;
                this.cmbFlow.Enabled = false;
                this.cmbParity.Enabled = false;
                this.cmbSpeed.Enabled = false;
                this.cmbStop.Enabled = false;
            }
        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            DispPort();
        }

        private void chkTelnet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTelnet.Checked)
            {
                this.txtTelnetAddr.Enabled = true;
            } else
            {
                this.txtTelnetAddr.Enabled = false;
            }
        }
    }
}
