using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Security.Principal;

namespace SITP
{
    /// <summary>
    /// ������ ���� ���� Ÿ�̸� �̺�Ʈ�� �߻� ��Ų��.
    /// </summary>
    class AgentTimer
    {
        private Form1 appMain;

        /// <summary>
        /// System.Threading.Timer Ŭ���� ���۷���.
        /// </summary>
        private System.Timers.Timer _Timer;

        /// <summary>
        /// Ÿ�̸� Elapsed ����.
        /// </summary>
        /// <remarks>����Ʈ 10��.</remarks>
        private int _Interval;

        /// <summary>
        /// Ÿ�̸� ��Ÿ�� ����
        /// </summary>
        private bool _bIsStarted;

        /// <summary>
        /// CInfo�� ���۷���.
        /// </summary>
        private Info _clsInfo;

        private CommonUtils _clsCommon;

        public Info Clsinfo
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

        public CommonUtils ClsCommon
        {
            get
            {
                return this._clsCommon;
            }
            set
            {
                this._clsCommon = value;
            }
        }

        public bool m_bIsStarted
        {
            get
            {
                return this._bIsStarted;
            }
            set
            {
                this._bIsStarted = value;
            }
        }

        /// <summary>
        /// �����ε� ������. �̰� ����Ѵ�.
        /// </summary>
        //public AgentTimer(int nInterval)
        public AgentTimer(Form1 app)
        {
            this.appMain = app;

            this._Timer = new System.Timers.Timer();
            this._Timer.Elapsed += new System.Timers.ElapsedEventHandler(_Timer_Elapsed);
            this._Timer.Enabled = false;
            this._bIsStarted = false;
        }

        ~AgentTimer()
        {
        }

        public int Interval
        {
            get
            {
                return this._Interval;
            }
            set
            {
                this._Interval = value;
            }
        }

        /// <summary>
        /// ������ �ð� ���ݸ��� ȣ��Ǵ� �ݹ� �Լ�.
        /// </summary>
        private void _Timer_Elapsed(object sender,System.Timers.ElapsedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            this._Timer.Enabled = false;


            //this.ClsCommon.LogData(1, "Timer Elapsed");

            

            this._Timer.Enabled = true;
        }

        /// <summary>
        /// Ÿ�̸Ӹ� Ȱ��ȭ.
        /// </summary>
        public void StartTimer()
        {
            DateTime dt = DateTime.Now;
            this._Timer.Enabled = true;
            this._bIsStarted = true;
        }

        /// <summary>
        /// Ÿ�̸� ��Ȱ��ȭ.
        /// </summary>
        public void StopTimer()
        {
            this._Timer.Enabled = false;
            this._bIsStarted = false;
        }

        public void SetTimerInterval(Int32 nInterval)
        {
            this._Interval = nInterval;
            this._Timer.Interval = this._Interval;
        }


    }
}
