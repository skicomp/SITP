using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SITP
{
    public partial class ConnectTest : Form
    {
        clsResize _form_resize;
        bool bIsConnected = false;
        SerialPort CommPort;

        //bool bIsStop = false;
        bool bIsDataRecieved1 = false;
        //bool bIsDataRecieved2 = false;

        bool bIsStop = false;

        private delegate void SafeCallDelegate(string text);
        private Thread thread1 = null;
        private Thread thread2 = null;
        private string strResult1 = String.Empty;
        private string strResult2 = String.Empty;

        NetworkStream stream = default(NetworkStream);
        TelnetConnection tc = null;
        string strEncode = String.Empty;

        public ConnectTest()
        {
            InitializeComponent();
            _form_resize = new clsResize(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _form_resize._get_initial_size();

            this.optTelnet.Checked = true;

            this.cmbPort.Enabled = false;
            this.cmbSpeed.Enabled = false;
            this.cmbData.Enabled = false;
            this.cmbParity.Enabled = false;
            this.cmbFlow.Enabled = false;
            this.cmbStop.Enabled = false;

            this.txtTelnetAddr.Enabled = true;
            bIsStop = false;
            strEncode = "Default";

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                _form_resize._resize();
            }
            catch { }

        }

        private void Telnet_Receive()
        {
            while (true)
            {
                if (bIsStop) break;

                try
                {
                    if (tc == null) break;

                    stream = tc.tcpSocket.GetStream();
                    int BUFFERSIZE = tc.tcpSocket.ReceiveBufferSize;
                    byte[] buffer = new byte[BUFFERSIZE];
                    int bytes = stream.Read(buffer, 0, buffer.Length);

                    string indata = "";

                    if (strEncode.Trim() == "Default")
                    {
                        indata = Encoding.Default.GetString(buffer, 0, bytes);
                    } else if (strEncode.Trim() == "Unicode")
                    {
                        indata = Encoding.Unicode.GetString(buffer, 0, bytes);
                    }
                    else if (strEncode.Trim() == "UTF8")
                    {
                        indata = Encoding.UTF8.GetString(buffer, 0, bytes);
                    }
                    //indata = Encoding.Default.GetString(buffer, 0, bytes);
                    MessageDispTelnet(indata);
                    //bIsDataRecieved2 = true;

                } catch //(Exception ex) 
                {
                    //MessageBox.Show("" + ex);
                    break; 
                }
            }

            //MessageBox.Show("Telnet Receive End.");
        }

        private void CommPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            MessageDisp(indata);
            bIsDataRecieved1 = true;
        }

        private void MessageDisp(string Data)
        {
            //this.txtLog.AppendText(Data);
            this.strResult1 = Data;

            thread1 = new Thread(new ThreadStart(SetText));
            thread1.Start();
            Thread.Sleep(50);
        }

        private void MessageDispTelnet(string Data)
        {
            //this.txtLog.AppendText(Data);
            this.strResult2 = Data;

            thread2 = new Thread(new ThreadStart(SetTextTelnet));
            thread2.Start();
            Thread.Sleep(50);
        }

        private string CommLogin(string strID, string strPassword)
        {
            string strReturn = "";

            int nLoopCnt = 0;
            if (this.CommPort != null && this.CommPort.IsOpen)
            {
                bIsDataRecieved1 = false;
                this.CommPort.Write(""+(char)13);

                while (true)
                {
                    nLoopCnt++;
                    if (nLoopCnt > 5)
                    {
                        throw new Exception("Failed to connect : no User login Receive Data. Timeout.");
                    }

                    if (bIsDataRecieved1)
                    {
                        if (strResult1.ToLower().Contains("username"))
                        {
                            if (!strResult1.TrimEnd().EndsWith(":"))
                                throw new Exception("Failed to connect : no login prompt");
                            break;
                        }
                    }
                    Thread.Sleep(1000);
                }

                bIsDataRecieved1 = false;
                this.CommPort.Write(this.txtID.Text + (char)13);

                nLoopCnt = 0;
                while (true)
                {
                    nLoopCnt++;
                    if (nLoopCnt > 5)
                    {
                        throw new Exception("Failed to connect : no User login Receive Data. Timeout.");
                    }

                    if (bIsDataRecieved1)
                    {
                        if (!strResult1.TrimEnd().EndsWith(":"))
                            throw new Exception("Failed to connect : no password prompt");
                        break;
                    }
                    Thread.Sleep(1000);

                }

                bIsDataRecieved1 = false;
                this.CommPort.Write(this.txtPassword.Text + (char)13);

                nLoopCnt = 0;
                while (true)
                {
                    nLoopCnt++;
                    if (nLoopCnt > 5)
                    {
                        throw new Exception("Failed to connect : no User login Receive Data. Timeout.");
                    }

                    if (bIsDataRecieved1)
                    {
                        strReturn = this.strResult1;
                        break;
                    }
                    Thread.Sleep(1000);

                }

            }

            bIsDataRecieved1 = false;
            return strReturn;
        }


        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                bIsStop = false;

                if (optSerial.Checked)
                {
                    if (this.txtID.Text.Trim() == "")
                    {
                        MessageBox.Show("아이디를 입력하세요");
                        return;
                    }
                    if (this.txtPassword.Text.Trim() == "")
                    {
                        MessageBox.Show("패스워드를 입력하세요");
                        return;
                    }

                    if (bIsConnected)
                    {
                        if (CommPort != null)
                        {
                            CommPort.Close();
                            CommPort = null;
                        }
                        bIsConnected = false;
                    }

                    CommPort = new SerialPort(this.cmbPort.Text.Trim());
                    CommPort.BaudRate = Convert.ToInt32(this.cmbSpeed.Text.Trim());

                    if (this.cmbData.Text.Trim() == "7 Bit") CommPort.DataBits = 7;
                    else if (this.cmbData.Text.Trim() == "8 Bit") CommPort.DataBits = 8;

                    if (this.cmbParity.Text.Trim() == "none") CommPort.Parity = Parity.None;
                    else if (this.cmbParity.Text.Trim() == "odd") CommPort.Parity = Parity.Odd;
                    else if (this.cmbParity.Text.Trim() == "even") CommPort.Parity = Parity.Even;
                    else if (this.cmbParity.Text.Trim() == "mark") CommPort.Parity = Parity.Mark;
                    else if (this.cmbParity.Text.Trim() == "space") CommPort.Parity = Parity.Space;

                    if (this.cmbStop.Text.Trim() == "1 bit") CommPort.StopBits = StopBits.One;
                    else if (this.cmbStop.Text.Trim() == "1.5 bit") CommPort.StopBits = StopBits.OnePointFive;
                    else if (this.cmbStop.Text.Trim() == "2 bit") CommPort.StopBits = StopBits.Two;

                    if (this.cmbFlow.Text.Trim() == "Xon/Xoff") CommPort.Handshake = Handshake.XOnXOff;
                    else if (this.cmbFlow.Text.Trim() == "hardware") CommPort.Handshake = Handshake.None;
                    else if (this.cmbFlow.Text.Trim() == "none") CommPort.Handshake = Handshake.None;

                    CommPort.RtsEnable = true;

                    CommPort.DataReceived += new SerialDataReceivedEventHandler(CommPort_DataReceived);

                    CommPort.Open();
                    bIsConnected = true;

                    string s = "";

                    try
                    {
                        s = CommLogin(this.txtID.Text.Trim(), this.txtPassword.Text.Trim());
                    }
                    catch //(Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        return;
                    }
                    MessageDisp(s);

                    string prompt = s.TrimEnd();
                    prompt = s.Substring(prompt.Length - 1, 1);
                    if (prompt != "$" && prompt != ">" && prompt != "#")
                    {
                        //throw new Exception("Connection failed");
                        MessageBox.Show("로그인 되지 않았습니다.");
                        return;
                    }

                    MessageBox.Show("로그인 되었습니다");

                }
                else //Telnet
                {
                    if (tc != null && tc.IsConnected)
                    {
                        tc.Close();
                    }
                    tc = null;

                    if (this.txtTelnetAddr.Text.Trim() == "")
                    {
                        MessageBox.Show("텔렛주소를 입력하세요");
                        return;
                    }
                    if (this.txtID.Text.Trim() =="")
                    {
                        MessageBox.Show("아이디를 입력하세요");
                        return;
                    }
                    if (this.txtPassword.Text.Trim() == "")
                    {
                        MessageBox.Show("패스워드를 입력하세요");
                        return;
                    }

                    string s = "";
                    tc = new TelnetConnection(this.txtTelnetAddr.Text.Trim(), 23);
                    tc.strEncode = this.strEncode;

                    try
                    {
                        s = tc.Login(this.txtID.Text.Trim(), this.txtPassword.Text.Trim(), 200);
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        if (tc != null && tc.IsConnected)
                        {
                            tc.Close();
                        }
                        tc = null;
                        return;
                    }
                    MessageDispTelnet(s);

                    string prompt = s.TrimEnd();
                    prompt = s.Substring(prompt.Length - 1, 1);
                    //if (prompt != "$" && prompt != ">")
                    if (prompt != "$" && prompt != ">" && prompt != "#")
                    {
                        //throw new Exception("Connection failed");
                        MessageBox.Show("연결이 되지 않았습니다.");
                        return;
                    }
                  
                    Thread t_handler = new Thread(Telnet_Receive);
                    t_handler.IsBackground = true;
                    t_handler.Start();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmdDisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                bIsStop = true;
            
                if (CommPort != null && CommPort.IsOpen)
                {
                    CommPort.Write("exit" + (char)13);
                    Thread.Sleep(3000);
                    CommPort.Close();
                }
                bIsConnected = false;
                CommPort = null;

                if (tc != null && tc.IsConnected)
                {
                    tc.Close();
                }
                tc = null;

                Thread.Sleep(2000);
                MessageBox.Show("연결이 해제 되었습니다");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)   
            {
                if (this.optSerial.Checked)
                {
                    //MessageBox.Show(this.txtCommand.Text);
                    if (this.CommPort != null && this.CommPort.IsOpen)
                    {
                        this.CommPort.Write(this.txtCommand.Text + (char)13);
                        //txtLog.AppendText(this.txtCommand.Text + Environment.NewLine);

                        if (this.txtCommand.Text.Trim().ToLower() == "exit")
                        {
                            Thread.Sleep(3000);
                            this.CommPort.Close();
                            this.CommPort = null;
                            bIsConnected = false;

                            MessageBox.Show("COMM 연결이 해제되었습니다.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("COMM 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                    }
                } else
                {
                    if (tc == null || !tc.IsConnected)
                    {
                        MessageBox.Show("텔렛 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                    } else
                    {
                        tc.WriteLine(this.txtCommand.Text.Trim() + (char)13);
                        //MessageDisp(tc.Read());

                        if (this.txtCommand.Text.Trim().ToLower() == "exit")
                        {
                            bIsStop = true;
                            tc.Close();
                            tc = null;
                            MessageBox.Show("텔렛 연결이 해제되었습니다.");
                        }

                    }

                }

                this.txtCommand.Text = "";

            }
        }

        private void btnLogClear_Click(object sender, EventArgs e)
        {
            if (this.optSerial.Checked)
            {
                this.txtLog.Clear();
            } else
            {
                this.txtTelnetLog.Clear();
            }
        }

        private void WriteTextSafe(string text)
        {
            if (txtLog.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                Invoke(d, new object[] { text });
            }
            else
            {
                this.txtLog.AppendText(text);
            }
        }
        private void WriteTextSafeTelnet(string text)
        {
            if (txtLog.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafeTelnet);
                Invoke(d, new object[] { text });
            }
            else
            {
                this.txtTelnetLog.AppendText(text);
            }
        }
        private void SetText()
        {
            WriteTextSafe(this.strResult1);
        }
        private void SetTextTelnet()
        {
            WriteTextSafeTelnet(this.strResult2);
        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bIsStop = true;

                if (CommPort != null && CommPort.IsOpen)
                {
                    CommPort.Write("exit" + (char)13);
                    Thread.Sleep(3000);
                    CommPort.Close();
                }
                bIsConnected = false;
                CommPort = null;

                if (tc != null && tc.IsConnected)
                {
                    tc.Close();
                }
                tc = null;

                MessageBox.Show("연결이 해제 되었습니다");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void optSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (this.optSerial.Checked)
            {
                this.cmbPort.Enabled = true;
                this.cmbSpeed.Enabled = true;
                this.cmbData.Enabled = true;
                this.cmbParity.Enabled = true;
                this.cmbFlow.Enabled = true;
                this.cmbStop.Enabled = true;

                this.txtTelnetAddr.Enabled = false;
            }
            else
            {
                this.cmbPort.Enabled = false;
                this.cmbSpeed.Enabled = false;
                this.cmbData.Enabled = false;
                this.cmbParity.Enabled = false;
                this.cmbFlow.Enabled = false;
                this.cmbStop.Enabled = false;

                this.txtTelnetAddr.Enabled = true;
            }

        }

        private void optTelnet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.optSerial.Checked)
            {
                this.cmbPort.Enabled = true;
                this.cmbSpeed.Enabled = true;
                this.cmbData.Enabled = true;
                this.cmbParity.Enabled = true;
                this.cmbFlow.Enabled = true;
                this.cmbStop.Enabled = true;

                this.txtTelnetAddr.Enabled = false;
            }
            else
            {
                this.cmbPort.Enabled = false;
                this.cmbSpeed.Enabled = false;
                this.cmbData.Enabled = false;
                this.cmbParity.Enabled = false;
                this.cmbFlow.Enabled = false;
                this.cmbStop.Enabled = false;

                this.txtTelnetAddr.Enabled = true;
            }

        }

        private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            strEncode = this.cmbEncoding.Text;
            if (tc != null) tc.strEncode = this.strEncode;
        }
    }
}
