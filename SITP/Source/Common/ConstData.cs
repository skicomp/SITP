using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SITP
{
    public struct CommonData
    {
        ///////////////////////////////////////////////////////
        // Const DATA
        ///////////////////////////////////////////////////////
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpDBConnADDR;

        ///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////
        //Benefit Test Server
        public const string m_strConnName = "OCRMasking";

        public const string m_strInFtpID = "masking";
        public const string m_strInFtpPass = "masking";
        public const string m_strOutFtpID = "masking";
        public const string m_strOutFtpPass = "masking";

        public const string keyData = "F12E34D56C78B90A";
        public const string IVData = "LctechOCRServer1";

        public override String ToString()
        {
            //String str = "LocalName: " + lpLocalName + " RemoteName: " + lpRemoteName
            //+ " Comment: " + lpComment + " lpProvider: " + lpProvider;
            //return (str);
            string str = "";
            return str;
        }

    }

}
