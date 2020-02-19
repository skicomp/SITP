using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SITP
{
    public class LogManager
    {
        private System.Object lockThis = new System.Object();

        const int WM_COPYDATA = 0x004A;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, ref COPYDATASTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, ref COPYDATASTRUCT lParam);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        private String _strModulePath;
        private String _strLogDir;
        private String _strLogDate;
        private String _strLogMessage;
        private String _strLogName;

        public String _strLogPath;

        ////////////////////////////////////////////
        //Log Gubun
        ///////////////////////////////////////////
        // DIR : ..../ModuleDir/Logs/yyyy-mm-dd/
        //1. - Accept Data   : accept.log
        //2. - Complete Data : complete.log
        //3. - Error Data    : error.log
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// 
        private int _nLogGubun;
        private String _strLogGubun;
        private StreamWriter _logger;

        public string m_strLogPath = string.Empty;

        public LogManager(string strLogPath,string strName)
        {
            lock (lockThis)
            {
                _strLogDate = String.Empty;
                _strLogDir = String.Empty;
                _strLogPath = String.Empty;
                _strLogName = String.Empty;
                _nLogGubun = 0;
                _strLogGubun = String.Empty;
                _strLogMessage = String.Empty;
                _logger = null;
                //_strModulePath = System.Windows.Forms.Application.StartupPath;
                //_strModulePath = String.Empty;
                _strModulePath = strLogPath;
                _strLogName = strName;
            }
        }

        ~LogManager()
        {
            lock (lockThis)
            {
                if (this._logger != null)
                {
                    this._logger.Close();
                    this._logger = null;
                }
            }
        }

        public bool SetData(int nGubun, string strMessage)
        {
            lock (lockThis)
            {
                try
                {
                    this._nLogGubun = nGubun;
                    this._strLogMessage = strMessage;

                    //MessageSend(this._strLogMessage);

                    if (this._nLogGubun == 1) this._strLogGubun = "accept.log";
                    else if (this._nLogGubun == 2) this._strLogGubun = "complete.log";
                    else if (this._nLogGubun == 3) this._strLogGubun = "error.log";
                    else this._strLogGubun = "error.log";

                    DateTime dt = DateTime.Now;
                    //this._strLogDate = String.Format(dt.ToShortDateString());
                    this._strLogDate = DateTime.Now.ToString("yyyy-MM-dd");

                    this._strLogPath = this._strModulePath + "\\Logs";
                    if (!System.IO.Directory.Exists(this._strLogPath)) Directory.CreateDirectory(this._strLogPath);
                    this._strLogPath = this._strModulePath + "\\Logs" + "\\" + this._strLogName;
                    if (!System.IO.Directory.Exists(this._strLogPath)) Directory.CreateDirectory(this._strLogPath);
                    this._strLogPath = this._strModulePath + "\\Logs" + "\\" + this._strLogName + "\\" + this._strLogDate;
                    if (!System.IO.Directory.Exists(this._strLogPath)) Directory.CreateDirectory(this._strLogPath);

                    this._strLogPath = this._strModulePath + "\\Logs" + "\\" + this._strLogName + "\\" + this._strLogDate + "\\" + this._strLogGubun;
                    this.m_strLogPath = this._strLogPath;
                    //this.m_strLogPath = this._strModulePath + "\\Logs" + "\\SERVICE\\" + this._strLogDate + "\\" + "accept.log";
                    /*this.m_strLogPath = this._strModulePath + "\\Logs" + "\\SERVICE";
                    if (!System.IO.Directory.Exists(this.m_strLogPath)) Directory.CreateDirectory(this.m_strLogPath);
                    this.m_strLogPath = this._strModulePath + "\\Logs" + "\\SERVICE\\" + this._strLogDate;
                    if (!System.IO.Directory.Exists(this.m_strLogPath)) Directory.CreateDirectory(this.m_strLogPath);
                    this.m_strLogPath = this._strModulePath + "\\Logs" + "\\SERVICE\\" + this._strLogDate + "\\" + "accept.log";
                    if (!System.IO.File.Exists(this.m_strLogPath)) File.CreateText(this.m_strLogPath);*/

                    this._logger = new StreamWriter(this._strLogPath, true, Encoding.UTF8);
                    this._logger.AutoFlush = true;

                    string strDateTime = String.Format(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                    string strData = "[" + strDateTime + "]" + this._strLogMessage;

                    this._logger.WriteLine(strData);

                    this._logger.Flush();
                    this._logger.Close();

                    this._logger = null;

                    return true;

                }
                catch (Exception e)
                {
                    if (this._logger != null)
                    {
                        this._logger.Close();
                        this._logger = null;
                    }
                    string strException = e.Message;
                    
                    return false;
                }
            
            }
        
        }

        public string GetLogPath()
        {
            return this.m_strLogPath;
        }

        public void SetLogPath(string strPath)
        {
            if (strPath.Trim() == "")
            {
                this._strModulePath = System.Windows.Forms.Application.StartupPath;
            }
            else
            {
                this._strModulePath = strPath;
            }
        }

        private void MessageSend(String strMsg)
        {
            string msg = strMsg;

            /*Process[] pro = Process.GetProcessesByName("Archiving Agent");
            if (pro.Length > 0)
            {
                byte[] buff = System.Text.Encoding.Default.GetBytes(msg);

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero;
                cds.cbData = buff.Length + 1;
                cds.lpData = msg;

                SendMessage(pro[0].MainWindowHandle, WM_COPYDATA, 0, ref cds);

                MessageBox.Show(new Form(){TopMost = true},"SendMessage:" + cds.lpData);
            }*/

           /* foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ToString().Contains("ArchAgent"))
                {
                    if (!proc.ToString().Contains("ArchAgent."))
                    {
                        byte[] buff = System.Text.Encoding.Default.GetBytes(msg);

                        COPYDATASTRUCT cds = new COPYDATASTRUCT();
                        cds.dwData = IntPtr.Zero;
                        cds.cbData = buff.Length + 1;
                        cds.lpData = msg;

                        //SendMessage(proc.MainWindowHandle, WM_COPYDATA, 0, ref cds);
                        PostMessage(proc.MainWindowHandle, WM_COPYDATA, 0, ref cds);
                        break;
                    }
                }
            }*/

            //System.IntPtr handle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, "Archiving Agent");
            /*System.IntPtr handle = FindWindowByCaption(IntPtr.Zero, "Archiving Agent");
            if (handle != IntPtr.Zero)
            {
                byte[] buff = System.Text.Encoding.Default.GetBytes(msg);

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero;
                cds.cbData = buff.Length + 1;
                cds.lpData = msg;

                SendMessage(handle, WM_COPYDATA, 0, ref cds);
            }*/

            Process[] pro = Process.GetProcessesByName("ArchAgent");
            if (pro.Length > 0)
            {
                byte[] buff = System.Text.Encoding.Default.GetBytes(msg);

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero;
                cds.cbData = buff.Length + 1;
                cds.lpData = msg;

                SendMessage(pro[0].MainWindowHandle, WM_COPYDATA, 0, ref cds);
            }

        }

    }
}
