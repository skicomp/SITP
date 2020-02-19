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
    /// 일정한 간격 동안 타이머 이벤트를 발생 시킨다.
    /// </summary>
    class AgentTimer
    {
        private Form1 appMain;

        /// <summary>
        /// System.Threading.Timer 클래스 레퍼런스.
        /// </summary>
        private System.Timers.Timer _Timer;

        /// <summary>
        /// 타이머 Elapsed 간격.
        /// </summary>
        /// <remarks>디폴트 10초.</remarks>
        private int _Interval;

        /// <summary>
        /// 타이머 스타팅 유무
        /// </summary>
        private bool _bIsStarted;

        /// <summary>
        /// CInfo의 레퍼런스.
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
        /// 오버로드 생성자. 이걸 사용한다.
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
        /// 설정한 시간 간격마다 호출되는 콜백 함수.
        /// </summary>
        private void _Timer_Elapsed(object sender,System.Timers.ElapsedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            this._Timer.Enabled = false;


            //this.ClsCommon.LogData(1, "Timer Elapsed");

            

            this._Timer.Enabled = true;
        }

        /// <summary>
        /// 타이머를 활성화.
        /// </summary>
        public void StartTimer()
        {
            DateTime dt = DateTime.Now;
            this._Timer.Enabled = true;
            this._bIsStarted = true;
        }

        /// <summary>
        /// 타이머 비활성화.
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
