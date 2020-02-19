 using System;
using System.Collections;							// Access to the Array list
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Security;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography;
using System.Web;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.Globalization;


namespace SITP
{
    public class CommonUtils
    {
        ////////////////////////////////////////////////////////////////////////////////////
        private System.Object lockThis = new System.Object();
        ////////////////////////////////////////////////////////////////////////////////////

        private StreamWriter _logger;
        private Info _clsInfo;
        private LogManager _mgrLog;

        public System.IO.StreamWriter logger
        {
            get
            {
                return this._logger;
            }
            set
            {
                this._logger = value;
            }
        }
        public Info clsinfo
        {
            get
            {
                return this._clsInfo;
            }
            set
            {
                this._clsInfo = value;
            }
        }

        public CommonUtils()
        {
        }

        public string GetGuid()
        {
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            return g.ToString();
        }

        public void SetLogName(string strLogPath, string strName)
        {
            this._mgrLog = new LogManager(strLogPath, strName);
        }
        
        public void SetLogPath(string strPath)
        {
            //this._mgrLog.SetLogPath(strPath);
        }
        public string GetLogPath()
        {
            return this._mgrLog.GetLogPath();
        }

        public string GetPrintLogPath()
        {
            return this._mgrLog._strLogPath;
        }

        public bool IsTextValidated(string strTextEntry)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strTextEntry)
                 && (strTextEntry != "");
        }

        /// <summary>
        /// 로그매니저 호출.
        /// </summary>
        /// <remarks>
        /// 날자별 종류별 로그데이타 출력 (1:accept 2:Complete 3:Error)
        /// </remarks>
        public Boolean LogData(int nGubun, string strData)
        {
            lock (lockThis)
            {
                string strLog = "";
                try
                {
                    string strGetData = strData;
                    int nGetGubun = nGubun;
                    return this._mgrLog.SetData(nGetGubun, strGetData);
                }
                catch (Exception e)
                {
                    strLog = "[LogData Exception]:" + e.Message;
                    if (!this.LogData(1, strLog)) { }
                    throw;
                }
            }
        }

    } //CommonUtils


    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    public class CommonParser
    {
        ////////////////////////////////////////////////////////////////////////////////////
        private System.Object lockThis = new System.Object();
        ////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Procesing Database Result DATA
        /// </summary>
        /// <param name="getData"></param>
        /// <returns></returns>
        public bool ParsingDBData(string getData, ref Hashtable dbHash)
        {
            dbHash.Clear();
            int iMsgSize = getData.Length;
            string strTemp = "";

            try
            {
                String[] arPath = getData.Split('|');

                for (int i = 0; i < arPath.Length; i++)
                {
                    strTemp = arPath[i];
                    strTemp.Trim();

                    if (strTemp.Equals("")) continue;

                    string strKey = "";
                    string strValue = "";
                    int first = strTemp.IndexOf("=");

                    if (first != -1)
                    {
                        strKey = strTemp.Substring(0, first).Trim().ToUpper();
                        strValue = strTemp.Substring(first + 1).Trim();
                        if (strValue == null) strValue = "";

                        dbHash.Add(strKey, strValue);
                    }
                }

            }
            catch 
            {
                return false;
            }
            return true;
        }
    
    } //Common Parser

}

