using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SITP
{
	// our own custom event args class
	public class ProcessThreadEventArgs : EventArgs
	{
		//1.Serial 2.Telnet
		public int gubun { get; set; }
		public string Data       { get; set; }
		public string PortName   { get; set; }

		public ProcessThreadEventArgs()
		{
			Data      = "";
			PortName = "";
			gubun = 0;
		}
		public ProcessThreadEventArgs(int nGubun, string data, string portname)
		{
			gubun = nGubun;
			Data      = data;
			PortName = portname;
		}
	}

	// delegates for the various events - names should be sufficient indication of event usage
    public delegate void ProcessRunHandler(object sender, ProcessThreadEventArgs e);
    public delegate void ProcessErrorHandler(object sender, ProcessThreadEventArgs e);
    public delegate void ProcessCompleteHandler(object sender, ProcessThreadEventArgs e);

	public class ProcessManagerEventArgs : EventArgs
	{
		public string Data       { get; set; }

		public ProcessManagerEventArgs()
		{
			Data      = "";
		}
		public ProcessManagerEventArgs(string data)
		{
			Data      = data;
		}
	}

	public delegate void ManagerQueuedHandler(object sender, ProcessManagerEventArgs e);
}
