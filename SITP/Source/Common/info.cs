using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Security;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace SITP
{

    public class Info
    {

        //체크타임
        private int _nCheckTime;
       
        //자동로그인
        private Boolean  _bAutoLogin  { get; set; }
        private String _strModulePath;
        
        ///////////////////////////////////////
        /// Config DATA 
        /// /////////////////////////////////
        public int m_nAutoStart { get; set; }
        public bool m_bWorkAuto { get; set; }
        public int m_nUseCrypto { get; set; }
        public int m_nUseFTP { get; set; }

        //로그 저장 경로
        public string m_strLogPath { get; set; }
        public String m_strCryptoKey { get; set; }

        //FTP설정
        public string m_strFtpIP { get; set; }
        public string m_strFtpPort { get; set; }
        public string m_strFtpID { get; set; }
        public string m_strFtpPass { get; set; }
        public int m_nFtpSecue { get; set; }

        public string m_strFtpInputRoot { get; set; }
        public string m_strFtpOutputRoot { get; set; }

        public string m_strNetAddr { get; set; }
        public string m_strNetID { get; set; }
        public string m_strNetPass { get; set; }

        /// <summary>
        /// 카테고리 데이타
        /// </summary>
        public int nPortCnt { get; set; }
        public int nPortSplitCnt { get; set; }
        public string[] arrPort { get; set; }
        public int nCommCnt { get; set; }
        public int nCommSplitCnt { get; set; }
        public string[] arrComm { get; set; }
        public int nModelCnt { get; set; }
        public string[] arrModel { get; set; }
        public int nCategoryCnt { get; set; }
        public string[] arrCategory { get; set; }
        public int nAgingModelCnt { get; set; }
        public string[] arrAgingModel { get; set; }
        public string strIniName { get; set; }

        public AES256Cipher aCyper { get; set; }
        public TIniFile ini { get; set; }

        private StreamWriter _logger;
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

        public Info()
        {

            _strModulePath = String.Empty;
            _nCheckTime = 0;

            _bAutoLogin = false;
            _strModulePath = System.Windows.Forms.Application.StartupPath;

            m_strLogPath = String.Empty;
            m_nCheckTime = 10;
            m_nAutoStart = 0;
            m_bWorkAuto = false;
            m_nFtpSecue = 0;
            m_nUseCrypto = 0;
            m_nUseFTP = 0;

            m_strFtpIP = String.Empty;
            m_strFtpPort = String.Empty;
            m_strFtpID = String.Empty;
            m_strFtpPass = String.Empty;
            m_strFtpInputRoot = String.Empty;
            m_strFtpOutputRoot = String.Empty;

            m_strCryptoKey = String.Empty;

            m_strNetAddr = String.Empty;
            m_strNetID = string.Empty;
            m_strNetPass = string.Empty;

            nPortCnt = 0;
            nPortSplitCnt = 0;
            arrPort = null;
            nCommCnt = 0;
            arrComm = null;
            nModelCnt = 0;
            arrModel = null;
            nCategoryCnt = 0;
            arrCategory = null;
            nAgingModelCnt = 0;
            arrAgingModel = null;
            strIniName = String.Empty;
            ini = null;
            aCyper = null;
        }

        public String m_strInfoPath
        {
            get;set;
        }

        public int m_nCheckTime
        {
            get
            {
                return this._nCheckTime;
            }
            set
            {
                this._nCheckTime = value;
            }
        }
        public bool m_bAutoLogin
        {
            get
            {
                return this._bAutoLogin;
            }
            set
            {
                this._bAutoLogin = value;
            }
        }
        public String m_strModulePath
        {
            get
            {
                return this._strModulePath;
            }
            set
            {
                this._strModulePath = value;
            }
        }


        public Boolean GetConfigValue(string strPath)
        {
            strIniName = strPath;
            //MessageBox.Show(new Form() { TopMost = true }, "Config.ini:" + strIniName);

            if (System.IO.File.Exists(strIniName))
            {
                try
                {
                    aCyper = new AES256Cipher();
                    ini = new TIniFile(strIniName);

                    //////////////////////////////////////////////////////////
                    string strSection = "Config";
                    //////////////////////////////////////////////////////////
                    string strKey = "";

                    strKey = "AutoStart";
                    this.m_nAutoStart = ini.GetInteger(strSection, strKey, 0);
                    strKey = "LogPath";
                    this.m_strLogPath = ini.GetString(strSection, strKey);
                    //this.m_strLogPath = this.m_strModulePath;

                    strKey = "CheckTime";
                    this.m_nCheckTime = ini.GetInteger(strSection, strKey, 0);

                    strKey = "UseCrypto";
                    this.m_nUseCrypto = ini.GetInteger(strSection, strKey, 0);

                    strKey = "UseFTP";
                    this.m_nUseFTP = ini.GetInteger(strSection, strKey, 0);

                    string strTemp = "";
                    //////////////////////////////////////////////////////////
                    strSection = "FTP";
                    //////////////////////////////////////////////////////////
                    strKey = "FtpIP";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strFtpIP = aCyper.AES_decrypt(strTemp, CommonData.keyData);
                    strKey = "FtpPort";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strFtpPort = aCyper.AES_decrypt(strTemp, CommonData.keyData);
                    strKey = "FtpID";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strFtpID = aCyper.AES_decrypt(strTemp, CommonData.keyData);
                    strKey = "FtpPass";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strFtpPass = aCyper.AES_decrypt(strTemp, CommonData.keyData);

                    strKey = "FtpInputRoot";
                    this.m_strFtpInputRoot = ini.GetString(strSection, strKey);
                    strKey = "FtpOutputRoot";
                    this.m_strFtpOutputRoot = ini.GetString(strSection, strKey);

                    //////////////////////////////////////////////////////////
                    strSection = "NETDRIVE";
                    //////////////////////////////////////////////////////////
                    strKey = "NetAddr";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strNetAddr = aCyper.AES_decrypt(strTemp, CommonData.keyData);
                    strKey = "NetID";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strNetID = aCyper.AES_decrypt(strTemp, CommonData.keyData);
                    strKey = "NetPass";
                    strTemp = ini.GetString(strSection, strKey);
                    this.m_strNetPass = aCyper.AES_decrypt(strTemp, CommonData.keyData);


                    //////////////////////////////////////////////////////////
                    strSection = "PORT";
                    //////////////////////////////////////////////////////////
                    strKey = "PortCnt";
                    this.nPortCnt = ini.GetInteger(strSection, strKey, 0);
                    strKey = "PortSplitCnt";
                    this.nPortSplitCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nPortCnt > 0)
                    {
                        arrPort = new string[nPortCnt];
                        for (int i = 0; i < nPortCnt; i++)
                        {
                            strKey = "Port"+Convert.ToString(i+1).Trim();
                            this.arrPort[i] = ini.GetString(strSection, strKey);
                        }
                    }

                    //////////////////////////////////////////////////////////
                    strSection = "CommSet";
                    //////////////////////////////////////////////////////////
                    strKey = "CommSetCnt";
                    this.nCommCnt = ini.GetInteger(strSection, strKey, 0);
                    strKey = "CommSplitCnt";
                    this.nCommSplitCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nCommCnt > 0)
                    {
                        arrComm = new string[nCommCnt];
                        for (int i = 0; i < nCommCnt; i++)
                        {
                            strKey = "Comm" + Convert.ToString(i + 1).Trim();
                            this.arrComm[i] = ini.GetString(strSection, strKey);
                        }
                    }

                    //////////////////////////////////////////////////////////
                    strSection = "Model";
                    //////////////////////////////////////////////////////////
                    strKey = "ModelCnt";
                    this.nModelCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nModelCnt > 0)
                    {
                        arrModel = new string[nModelCnt];
                        for (int i = 0; i < nModelCnt; i++)
                        {
                            strKey = "Model" + Convert.ToString(i + 1).Trim();
                            this.arrModel[i] = ini.GetString(strSection, strKey);

                        }
                    }

                    //////////////////////////////////////////////////////////
                    strSection = "Category";
                    //////////////////////////////////////////////////////////
                    strKey = "CategoryCnt";
                    this.nCategoryCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nCategoryCnt > 0)
                    {
                        arrCategory = new string[nCategoryCnt];
                        for (int i = 0; i < nCategoryCnt; i++)
                        {
                            strKey = "Category" + Convert.ToString(i + 1).Trim();
                            this.arrCategory[i] = ini.GetString(strSection, strKey);

                        }
                    }

                    //////////////////////////////////////////////////////////
                    strSection = "AgingModel";
                    //////////////////////////////////////////////////////////
                    strKey = "AgingModelCnt";
                    this.nAgingModelCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nAgingModelCnt > 0)
                    {
                        arrAgingModel = new string[nAgingModelCnt];
                        for (int i = 0; i < nAgingModelCnt; i++)
                        {
                            strKey = "AgingModel" + Convert.ToString(i + 1).Trim();
                            this.arrAgingModel[i] = ini.GetString(strSection, strKey);

                        }
                    }


                    this.m_strCryptoKey = CommonData.keyData;

                }
                catch (Exception ex)
                {
                    this._logger.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }


            return true;
        }

        public Boolean SetConfigValue(string strSection, string strKey, string strData, string strType)
        {
            if (ini != null)
            {
                try
                {
                    if (strType == "int")
                    {
                        ini.SetInteger(strSection, strKey, Convert.ToInt32(strData));
                    } else if (strType == "bool")
                    {
                        if (strData == "true") ini.SetBoolean(strSection, strKey, true);
                        else ini.SetBoolean(strSection, strKey, false);
                    } else
                    {
                        ini.SetString(strSection, strKey, strData);
                    }

                }
                catch (Exception ex)
                {
                    this._logger.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public void reloadCommInfo()
        {
            if (ini != null)
            {
                try
                {
                    this.nCommCnt = 0;
                    arrComm = null;

                    //////////////////////////////////////////////////////////
                    string strSection = "CommSet";
                    //////////////////////////////////////////////////////////
                    string strKey = "CommSetCnt";
                    this.nCommCnt = ini.GetInteger(strSection, strKey, 0);
                    strKey = "CommSplitCnt";
                    this.nCommSplitCnt = ini.GetInteger(strSection, strKey, 0);
                    if (nCommCnt > 0)
                    {
                        arrComm = new string[nCommCnt];
                        for (int i = 0; i < nCommCnt; i++)
                        {
                            strKey = "Comm" + Convert.ToString(i + 1).Trim();
                            this.arrComm[i] = ini.GetString(strSection, strKey);
                        }
                    }

                }
                catch (Exception ex)
                {
                    this._logger.WriteLine(ex);
                    return;
                }
            }
            else
            {
                return;
            }

            return;
        }

        /// <summary>
        /// Info클래스의 데이터 값을 초기화 한다.
        /// </summary>
        /// <remarks>
        /// 레지스트리,맥어드레스등의 필요한 정보를 가져 오는
        /// 함수를 호출 한다.
        /// </remarks>
        public Boolean Initialize()
        {
            try
            {
                string strIniName = this.m_strModulePath + "\\Config.ini";
                if (!GetConfigValue(strIniName))
                {
                    return false;
                }

            }
            catch { return false; }

            return true;
        }

        /// <summary>
        /// Info클래스의 데이터 값을 초기화 한다.
        /// </summary>
        /// <remarks>
        /// 레지스트리,맥어드레스등의 필요한 정보를 가져 오는
        /// 함수를 호출 한다.
        /// </remarks>
        public Boolean InitializeForPrint()
        {
            string strIniName = System.Windows.Forms.Application.StartupPath + "\\" +"Config.ini";

            if (!GetConfigValue(strIniName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// UUID를 생성함.
        /// </summary>
        /// <remarks>
        /// UUID를 생성
        /// </remarks>
        public string getUUID()
        {
            string strResult = "";

            try
            {
                char[] g_wcsRandomKey = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                                      'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                                      'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                                      's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

                StringBuilder builder = new StringBuilder(15);
                Random rnd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < 15; i++)
                {
                    int nKeyIndex = rnd.Next() % g_wcsRandomKey.Length;
                    builder.Append(g_wcsRandomKey[nKeyIndex]);
                }

                strResult = builder.ToString();

            }
            catch (Exception ex)
            {
                this._logger.WriteLine(ex.Message);
            }

            return strResult;

        }


    }
}
