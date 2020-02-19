using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SITP
{
    public class PortManager
    {
        private Form1 MainForm { get; set; }

        public List<PORTData> PORTDatas { get; set; }
        public List<COMMData> COMMDatas { get; set; }
        public List<ConnectPort> ConnectPorts { get; set; }
        public ConnectPort clsCurrConnector { get; set; }

        public PortManager()
        {
        }

        public PortManager(Form1 _form_)
        {
            MainForm = _form_; //the calling form
            PORTDatas = new List<PORTData>();
            COMMDatas = new List<COMMData>();
            ConnectPorts = new List<ConnectPort>();
            clsCurrConnector = null;
        }

        public void initialzeList()
        {
            int nPortCount = this.MainForm._clsInfo.nPortCnt;
            for (int i = 0; i < nPortCount; i++)
            {
                try
                {
                    string strData = this.MainForm._clsInfo.arrPort[i];
                    string[] arrData = strData.Split(';');
                    if (arrData.Length < this.MainForm._clsInfo.nPortSplitCnt) continue;
                    string strTemp = "";

                    PORTData portData = new PORTData();

                    portData.Seq = Convert.ToInt32(arrData[0]);
                    strTemp = arrData[1];
                    if (strTemp == "Empty") portData.PortName = "";
                    else portData.PortName = strTemp;
                    strTemp = arrData[2];
                    if (strTemp == "Empty") portData.RunStatus = "";
                    else portData.RunStatus = strTemp;
                    strTemp = arrData[3];
                    if (strTemp == "Empty") portData.Category = "";
                    else portData.Category = strTemp;
                    strTemp = arrData[4];
                    if (strTemp == "Empty") portData.FertCode = "";
                    else portData.FertCode = strTemp;
                    strTemp = arrData[5];
                    if (strTemp == "Empty") portData.FertSN = "";
                    else portData.FertSN = strTemp;
                    strTemp = arrData[6];
                    if (strTemp == "Empty") portData.UmpSN = "";
                    else portData.UmpSN = strTemp;
                    strTemp = arrData[7];
                    if (strTemp == "Empty") portData.ECP1SN = "";
                    else portData.ECP1SN = strTemp;
                    strTemp = arrData[8];
                    if (strTemp == "Empty") portData.ECP2SN = "";
                    else portData.ECP2SN = strTemp;
                    strTemp = arrData[9];
                    if (strTemp == "Empty") portData.ECP3SN = "";
                    else portData.ECP3SN = strTemp;
                    strTemp = arrData[10];
                    if (strTemp == "check") portData.Au_Rssi = true;
                    else portData.Au_Rssi = false;
                    strTemp = arrData[11];
                    if (strTemp == "check") portData.Au_311 = true;
                    else portData.Au_311 = false;
                    strTemp = arrData[12];
                    if (strTemp == "Empty") portData.IEMISN = "";
                    else portData.IEMISN = strTemp;
                    strTemp = arrData[13];
                    if (strTemp == "check") portData.USIM = true;
                    else portData.USIM = false;
                    strTemp = arrData[14];
                    if (strTemp == "check") portData.CHOnly = true;
                    else portData.CHOnly = false;
                    strTemp = arrData[15];
                    portData.Alarm = strTemp;
                    strTemp = arrData[16];
                    if (strTemp == "Empty") portData.TelnetServer = "";
                    else portData.TelnetServer = strTemp;
                    strTemp = arrData[17];
                    if (strTemp == "Empty") portData.StartTime = "";
                    else portData.StartTime = strTemp;
                    strTemp = arrData[18];
                    if (strTemp == "Empty") portData.EndTime = "";
                    else portData.EndTime = strTemp;
                    strTemp = arrData[19];
                    if (strTemp == "Empty") portData.WorkPC = "";
                    else portData.WorkPC = strTemp;
                    strTemp = arrData[20];
                    if (strTemp == "Empty") portData.Result = "";
                    else portData.Result = strTemp;

                    portData.bIsConnected = false;
                    portData.bIsDisConnected = true;
                    portData.bIsLoged = false;
                    portData.bIsStarted = false;
                    portData.bIsStoped = true;

                    portData.portMan = null;

                    PORTDatas.Add(portData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            int nCommCount = this.MainForm._clsInfo.nCommCnt;
            for (int i = 0; i < nCommCount; i++)
            {
                try
                {
                    string strData = this.MainForm._clsInfo.arrComm[i];
                    string[] arrData = strData.Split(';');
                    if (arrData.Length < this.MainForm._clsInfo.nCommSplitCnt) continue;

                    string strName = arrData[0];
                    string strTemp = "";

                    for (int k = 0; k < PORTDatas.Count; k++)
                    {
                        strTemp = PORTDatas[k].PortName;
                        if (strName == strTemp)
                        {
                            strTemp = arrData[1];
                            if (strTemp == "checked") PORTDatas[k].bUseComm = true;
                            else PORTDatas[k].bUseComm = false;
                            strTemp = arrData[2];
                            PORTDatas[k].nCommSpeed = Convert.ToInt32(strTemp);
                            strTemp = arrData[3];
                            if (strTemp == "Empty") PORTDatas[k].CommData = "";
                            else PORTDatas[k].CommData = strTemp;
                            strTemp = arrData[4];
                            if (strTemp == "Empty") PORTDatas[k].CommParity = "";
                            else PORTDatas[k].CommParity = strTemp;
                            strTemp = arrData[5];
                            if (strTemp == "Empty") PORTDatas[k].CommStop = "";
                            else PORTDatas[k].CommStop = strTemp;
                            strTemp = arrData[6];
                            if (strTemp == "Empty") PORTDatas[k].CommFlow = "";
                            else PORTDatas[k].CommFlow = strTemp;
                            strTemp = arrData[7];
                            if (strTemp == "Empty") PORTDatas[k].TelnetServer = "";
                            else PORTDatas[k].TelnetServer = strTemp;
                            strTemp = arrData[8];
                            if (strTemp == "checked") PORTDatas[k].bUseTelnet = true;
                            else PORTDatas[k].bUseTelnet = false;
                            strTemp = arrData[9];
                            if (strTemp == "Empty") PORTDatas[k].UserID = "";
                            else PORTDatas[k].UserID = strTemp;
                            strTemp = arrData[10];
                            if (strTemp == "Empty") PORTDatas[k].Password = "";
                            else PORTDatas[k].Password = strTemp;
                            strTemp = arrData[11];
                            if (strTemp == "Empty") PORTDatas[k].Encoding = "Default";
                            else PORTDatas[k].Encoding = strTemp;
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            ////////////////////////////////////////////////////
            /// PortData Initializing
            ////////////////////////////////////////////////////
            for (int k = 0; k < nPortCount; k++)
            {
                try
                {
                    ConnectPort clsPort = new ConnectPort(this, PORTDatas[k]);
                    clsPort.ProcessRun += new ProcessRunHandler(ProcessRun_Return);
                    ConnectPorts.Add(clsPort);
                    PORTDatas[k].portMan = clsPort;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        } //class initialize

        void ProcessRun_Return(object sender, ProcessThreadEventArgs e)
        {
            ConnectPort pConnect = sender as ConnectPort;

            //MessageBox.Show("" + e.gubun + ":" + e.PortName + ":" + e.Data);

            if (e.gubun == 3) //1.comm 2.telnet 3.status 4.result
            {
                StatusChange(pConnect, e.PortName, e.Data);
            }
            ReceiveMessage(e.gubun, e.PortName, e.Data);
        }

        public void Status(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan;
                    this.clsCurrConnector.bIsActive = true;
                }
                else
                {
                    PORTDatas[k].portMan.bIsActive = false;
                }
            }
        }

        public void Connect(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan;
                    this.clsCurrConnector.bIsActive = true;
                    this.clsCurrConnector.Connect(PORTDatas[k]);
                } else
                {
                    this.clsCurrConnector.bIsActive = false;
                }
            }
        }

        public void Disconnect(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan;
                    this.clsCurrConnector.bIsActive = false;
                    this.clsCurrConnector.DisConnect(true);
                }
                else
                {
                    this.clsCurrConnector.bIsActive = false;
                }
            }
        }

        public void DisconnectALL()
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                this.clsCurrConnector = PORTDatas[k].portMan;
                this.clsCurrConnector.bIsActive = false;
                this.clsCurrConnector.DisConnect(false);
            }
        }

        public void Logon(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan; ;
                    this.clsCurrConnector.bIsActive = true;
                    this.clsCurrConnector.Logon();
                }
                else
                {
                    this.clsCurrConnector.bIsActive = false;
                }
            }
        }

        public void Start(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan; ;
                    this.clsCurrConnector.bIsActive = true;
                    this.clsCurrConnector.Start();
                }
                else
                {
                    this.clsCurrConnector.bIsActive = false;
                }
            }
        }
        public void Stop(string strPortName)
        {
            for (int k = 0; k < PORTDatas.Count; k++)
            {
                string strTemp = PORTDatas[k].PortName;
                if (strPortName == strTemp)
                {
                    this.clsCurrConnector = PORTDatas[k].portMan; ;
                    this.clsCurrConnector.bIsActive = true;
                    this.clsCurrConnector.Stop();
                }
                else
                {
                    this.clsCurrConnector.bIsActive = false;
                }
            }
        }

        //1.Comm Message 2.Telnet Message 
        public void ReceiveMessage(int nGubun, string strPortName, string strMsg)
        {
            if (this.MainForm != null)
            {
                this.MainForm.getPortMsg(nGubun, strPortName, strMsg);
            }
        }

        public void StatusChange(ConnectPort pConnect, string strPortName, string strText)
        {
            for (int i = 0; i < PORTDatas.Count; i++)
            {
                PORTData portData = PORTDatas[i];
                if (strPortName == portData.PortName)
                {
                    portData.bIsConnected = pConnect.bIsConnect;
                    portData.bIsDisConnected = pConnect.bIsDisConnect;
                    portData.bIsLoged = pConnect.bIsLogon;
                    portData.bIsStarted = pConnect.bIsStarted;
                    portData.bIsStoped = pConnect.bIsStoped;
                    portData.RunStatus = strText;

                    this.MainForm.portListChangeStatus(portData);

                    break;
                }
            }
        }


    }
}
