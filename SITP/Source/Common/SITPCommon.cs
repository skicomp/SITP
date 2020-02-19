using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Sockets;

namespace SITP

{
    public class RUData
    {
        public int RUNo { get; set; }
        public string RUName { get; set; }
        public int RUPathNo { get; set; }
        public string RUDispText { get; set; }
        public Boolean OptValue { get; set; }
        public Boolean Visible { get; set; }
        public int RUStatusVal { get; set; } //0:Nomal 1:OK....
        public string RUStatusText { get; set; }
        public Color deFaultOptColor { get; set; }
        public Color deFaultTxtColor { get; set; }
        public int optPosX { get; set; }
        public int optPosY { get; set; }
        public int optWidth { get; set; }
        public int optHeight { get; set; }
        public int txtPosX { get; set; }
        public int txtPosY { get; set; }
        public int txtWidth { get; set; }
        public int txtHeight { get; set; }

    }

    /// <summary>
    /// Comm Port Data
    /// </summary>
    /// 

    public class PORTData
    {
        public int Seq { get; set; }
        public string PortName { get; set; }
        /// <summary>
        /// Connected / Disconneted / Login / Logout /Started / Stopped
        /// </summary>
        public string RunStatus { get; set; }
        public string Category { get; set; }
        public string FertCode { get; set; }
        public string FertSN { get; set; }
        public string UmpSN { get; set; }
        public string ECP1SN { get; set; }
        public string ECP2SN { get; set; }
        public string ECP3SN { get; set; }
        public bool Au_Rssi { get; set; }
        public bool Au_311 { get; set; }
        public string IEMISN { get; set; }
        public bool USIM { get; set; }
        public bool CHOnly { get; set; }
        public string Alarm { get; set; }
        public string TelnetServer { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string WorkPC { get; set; }
        /// <summary>
        /// wait / running / success / fail / error
        /// </summary>
        public string Result { get; set; }
        public bool bUseComm { get; set; }
        public int nCommSpeed { get; set; }
        public string CommData { get; set; }
        public string CommParity { get; set; }
        public string CommStop { get; set; }
        public string CommFlow { get; set; }
        public bool bUseTelnet { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Encoding { get; set; }
        public bool bIsConnected { get; set; }
        public bool bIsDisConnected { get; set; }
        public bool bIsLoged { get; set; }
        public bool bIsStarted { get; set; }
        public bool bIsStoped { get; set; }
        public ConnectPort portMan { get; set; }
    }

    public class COMMData
    {
        public string CommName { get; set; }
        public bool CommUse { get; set; }
        public int nSpeed { get; set; }
        public string strData { get; set; }
        public string strParity { get; set; }
        public string strStop { get; set; }
        public string strFlow { get; set; }
        public string strTelnetIP { get; set; }
        public bool TelnetUse { get; set; }
        public string strUserID { get; set; }
        public string strPassword { get; set; }
        public string strEncoding { get; set; }
    }


    public class SITPCommon
    {
    }


}
