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
    public class ConnectPort
    {
        public PortManager m_portManager { get; set; }
        public bool bIsActive = false;
        public bool bIsConnect = false;
        public bool bIsDisConnect = false;
        public bool bIsLogon = false;
        public bool bIsStarted = false;
        public bool bIsStoped = false;

        public bool bUseComm = false;
        public bool bUseTelnet = false;

        public SerialPort CommPort;
        NetworkStream stream = default(NetworkStream);
        public TelnetConnection tc = null;
        string strEncode = String.Empty;

        public bool bIsDataRecieved1 = false;
        public bool bIsDataRecieved2 = false;

        private delegate void SafeCallDelegate(string text);
        private Thread thread1 = null;
        private Thread thread2 = null;
        private string strResult1 = String.Empty;
        private string strResult2 = String.Empty;

        private Thread t_handler = null;

        public PORTData m_PortData { get; set; }

        public event ProcessRunHandler ProcessRun = delegate { };
        public ConnectPort()
        {
            bUseComm = false;
            bUseTelnet = false;
            bIsStoped = false;
            bIsActive = false;

            strEncode = "Default";
        }
        public ConnectPort(PortManager portManager, PORTData portData)
        {
            bUseComm = portData.bUseComm;
            bUseTelnet = portData.bUseTelnet;

            bIsStarted = false;
            bIsStoped = false;
            bIsActive = false;
            bIsConnect = false;
            bIsDisConnect = false;

            this.m_portManager = portManager;
            this.m_PortData = portData;
            strEncode = "Default";
        }

        protected void RaiseEventProcessRun(int nGubun, string data, string portname)
        {
            ProcessRun(this, new ProcessThreadEventArgs(nGubun, data, portname));
        }

        private void Telnet_Receive()
        {
            while (true)
            {
                if (bIsDisConnect) break;

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

                    MessageDispTelnet(indata);
                    bIsDataRecieved2 = true;

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
            MessageDispComm(indata);
            bIsDataRecieved1 = true;
        }

        private void MessageDispComm(string Data)
        {
            //this.txtLog.AppendText(Data);
            this.strResult1 = Data;

            thread1 = new Thread(new ThreadStart(SetTextComm));
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
                this.CommPort.Write(this.m_PortData.UserID + (char)13);

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
                this.CommPort.Write(this.m_PortData.Password + (char)13);

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

        public void Connect(PORTData portData)
        {
            try
            {
                this.m_PortData = portData;

                if (this.m_PortData.UserID == "")
                {
                    MessageBox.Show("시리얼연결 아이디가 정확하지 않습니다");
                    return;
                }
                if (this.m_PortData.Password == "")
                {
                    MessageBox.Show("시리얼연결 패스워드가 정확하지 않습니다");
                    return;
                }

                if (this.m_PortData.bUseComm)
                {

                    if (CommPort != null)
                    {
                        CommPort.Close();
                        CommPort = null;
                    }

                    CommPort = new SerialPort(this.m_PortData.PortName);
                    CommPort.BaudRate = this.m_PortData.nCommSpeed;

                    if (this.m_PortData.CommData == "7 Bit") CommPort.DataBits = 7;
                    else if (this.m_PortData.CommData == "8 Bit") CommPort.DataBits = 8;

                    if (this.m_PortData.CommParity == "none") CommPort.Parity = Parity.None;
                    else if (portData.CommParity == "odd") CommPort.Parity = Parity.Odd;
                    else if (portData.CommParity == "even") CommPort.Parity = Parity.Even;
                    else if (portData.CommParity == "mark") CommPort.Parity = Parity.Mark;
                    else if (portData.CommParity == "space") CommPort.Parity = Parity.Space;

                    if (this.m_PortData.CommStop == "1 bit") CommPort.StopBits = StopBits.One;
                    else if (this.m_PortData.CommStop == "1.5 bit") CommPort.StopBits = StopBits.OnePointFive;
                    else if (this.m_PortData.CommStop == "2 bit") CommPort.StopBits = StopBits.Two;

                    if (this.m_PortData.CommFlow == "Xon/Xoff") CommPort.Handshake = Handshake.XOnXOff;
                    else if (this.m_PortData.CommFlow == "hardware") CommPort.Handshake = Handshake.None;
                    else if (this.m_PortData.CommFlow == "none") CommPort.Handshake = Handshake.None;

                    CommPort.RtsEnable = true;

                    CommPort.DataReceived += new SerialDataReceivedEventHandler(CommPort_DataReceived);

                    CommPort.Open();
                    Thread.Sleep(2000);

                    if (CommPort == null || !CommPort.IsOpen)
                    {
                        DisConnect(false);
                        MessageBox.Show("시리얼포트 연결에 실패하였습니다.");
                        return;
                    }

                    MessageBox.Show("시리얼포트 연결에 성공하였습니다.");

                } //UseComm

                if (this.m_PortData.bUseTelnet)
                {
                    if (this.m_PortData.TelnetServer == "")
                    {
                        MessageBox.Show("텔렛주소가 정확하지 않습니다");
                        return;
                    }
                    if (this.m_PortData.UserID == "")
                    {
                        MessageBox.Show("텔렛 아이디가 정확하지 않습니다");
                        return;
                    }
                    if (this.m_PortData.Password == "")
                    {
                        MessageBox.Show("텔렛 패스워드가 정확하지 않습니다");
                        return;
                    }

                    if (tc != null && tc.IsConnected)
                    {
                        tc.Close();
                    }
                    tc = null;

                    tc = new TelnetConnection(this.m_PortData.TelnetServer, 23);
                    tc.strEncode = this.strEncode;
                    Thread.Sleep(2000);

                    if (tc == null || !tc.IsConnected)
                    {
                        DisConnect(false);
                        MessageBox.Show("텔렛 연결에 실패하였습니다.");
                        return;
                    }

                    MessageBox.Show("텔렛 연결에 성공하였습니다.");

                } //Use Telnet

                bIsConnect = true;
                bIsDisConnect = false;
                bIsLogon = false;
                bIsStarted = false;
                bIsStoped = false;

                returnStatus("Connected");

            }
            catch (Exception ex)
            {
                DisConnect(false);
                MessageBox.Show("연결이 실패하였습니다:" + ex.ToString());
            }

        }

        public void DisConnect(bool bIsMsg)
        {
            try
            {
                try
                {
                    if (CommPort != null && CommPort.IsOpen)
                    {
                        CommPort.Write("exit" + (char)13);
                        if (bIsMsg) Thread.Sleep(3000);
                        CommPort.Close();
                    }
                }
                catch { }

                CommPort = null;

                try
                {
                    if (tc != null && tc.IsConnected)
                    {
                        tc.Close();
                    }
                }
                catch { }

                tc = null;

                if (bIsMsg)
                {
                    MessageBox.Show("연결이 해제되었습니다.");
                }
            }
            catch {  }

            bIsConnect = false;
            bIsDisConnect = true;
            bIsLogon = false;
            bIsStarted = false;
            bIsStoped = true;

            returnStatus("DisConnected");


        }
        public void Logon()
        {
            try
            {
                string s = "";

                if (!bIsConnect)
                {
                    MessageBox.Show("장치가 연결이 되어있지 않습니다");
                    return;
                }

                if (bUseComm)
                {
                    try
                    {
                        s = CommLogin(this.m_PortData.UserID, this.m_PortData.Password);
                    }
                    catch (Exception ex)
                    {
                        DisConnect(false);
                        MessageBox.Show("시리얼포트 로그인 오류:"+ex.ToString());
                        return;
                    }
                    MessageDispComm(s);

                    string strRet = s.TrimEnd();
                    strRet = s.Substring(strRet.Length - 1, 1);
                    if (strRet != "$" && strRet != ">" && strRet != "#")
                    {
                        //throw new Exception("Connection failed");
                        DisConnect(false);
                        MessageBox.Show("시리얼포트에 로그인 되지 않았습니다.");
                        return;
                    }

                }

                if (this.m_PortData.bUseTelnet)
                {
                    try
                    {
                        s = tc.Login(this.m_PortData.UserID, this.m_PortData.Password, 200);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("텔렛연결오류:" + ex.ToString());
                        DisConnect(false);
                        return;
                    }
                    MessageDispTelnet(s);

                    string prompt = s.TrimEnd();
                    prompt = s.Substring(prompt.Length - 1, 1);
                    //if (prompt != "$" && prompt != ">")
                    if (prompt != "$" && prompt != ">" && prompt != "#")
                    {
                        //throw new Exception("Connection failed");
                        DisConnect(false);
                        MessageBox.Show("텔렛이 연결되지 않았습니다.");
                        return;
                    }

                    t_handler = new Thread(Telnet_Receive);
                    t_handler.IsBackground = true;
                    t_handler.Start();

                }

                bIsLogon = true;
                returnStatus("Logon");

                MessageBox.Show("로그인 되었습니다");

            }
            catch (Exception ex) 
            {
                MessageBox.Show("로그인오류:"+ex.ToString());
                DisConnect(false);
            }

        }

        public void Start()
        {
            try
            {
                if (!bIsConnect || !bIsLogon)
                {
                    MessageBox.Show("연결이 되지 않았거나 로그온하지 않았습니다.");
                    return;
                }

                bIsStarted = true;
                bIsStoped = false;
                returnStatus("Started");

                MessageBox.Show("검사가 시작되었습니다");

            }
            catch (Exception ex)
            {
                MessageBox.Show("시작오류:" + ex.ToString());
            }
        }

        public void Stop()
        {
            try
            {
                if (!bIsStarted)
                {
                    MessageBox.Show("검사가 시작되어있지 않습니다..");
                    return;
                }

                bIsStarted = false;
                bIsStoped = true;
                returnStatus("Stoped");

                MessageBox.Show("검사가 중지되었습니다");

            }
            catch (Exception ex)
            {
                MessageBox.Show("중지오류:" + ex.ToString());
            }
        }

        /// <summary>
        /// 여기서 정한다
        /// RaiseEventProcessRun:Gubun 1:Comm Msg  2. TelnetMsg 3.Status 4.Result
        /// </summary>
        /// <param name="text"></param>
        private void WriteTextSafeComm(string text)
        {
            RaiseEventProcessRun(1, text, this.m_PortData.PortName);
        }

        private void Invoke(SafeCallDelegate d, object[] v)
        {
            throw new NotImplementedException();
        }

        private void WriteTextSafeTelnet(string text)
        {
            RaiseEventProcessRun(2, text, this.m_PortData.PortName);
        }
        private void SetTextComm()
        {
            WriteTextSafeComm(this.strResult1);
        }
        private void SetTextTelnet()
        {
            WriteTextSafeTelnet(this.strResult2);
        }

        private void returnStatus(string text)
        {
            RaiseEventProcessRun(3, text, this.m_PortData.PortName);
        }
        private void returnResult(string text)
        {
            RaiseEventProcessRun(4, text, this.m_PortData.PortName);
        }

    }
}
