using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Collections.Generic;

namespace SITP
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : System.Windows.Forms.Form
	{
        public Info _clsInfo;
        public CommonUtils _clsCommon;

        public FileStream _logfs;
        public StreamWriter _logger;

        private AgentTimer _TimersCls;
        private Tail winTail;
        private clsResize _form_resize;

        public RUManager ruManager { get; set; }
        public PortManager portManager { get; set; }

        // The LVItem being dragged
        private System.Windows.Forms.ListViewItem _itemDnD = null;

        private Thread threadLogMain = null;
        private Thread threadPortChange = null;

        private string strLogResult = String.Empty;
        private int nMsgGubun = 0;
        private delegate void SafeCallDelegate(string text);

        public string[] portNames = null;
        public string strCurrSeq { get; set; }

        private string strResult1 = String.Empty;

        private PORTData currPortData { get; set; }

        public Form1()
		{
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            _form_resize = new clsResize(this);
            portManager = new PortManager(this);
            ruManager = new RUManager(this);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            lstPort.SubItemClicked += new ListViewEx.SubItemEventHandler(lstPort_SubItemClicked);
			lstPort.SubItemEndEditing += new ListViewEx.SubItemEndEditingEventHandler(lstPort_SubItemEndEditing);
            
            strCurrSeq = "";

        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

            //_form_resize._get_initial_size();
            // this.MainTab.TabPages[0].i..Icon = Icon.FromHandle(((Bitmap)imageList1.Images[1]).GetHicon());

            //로그 파일 ------------------
            String log_path;
            log_path = @"SITP_Log.txt";
            if (System.IO.File.Exists(log_path))
            {
                _logfs = new FileStream(log_path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
                _logfs = new FileStream(log_path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            }
            _logger = new StreamWriter(this._logfs, System.Text.Encoding.UTF8);
            _logger.AutoFlush = true;

            this._clsInfo = new Info();
            this._clsInfo.logger = this._logger;

            bool bInit = this._clsInfo.Initialize();
            if (!bInit)
            {
                MessageBox.Show(new Form() { TopMost = true }, "초기화 오류입니다. 프로그램을 실행할 수 없습니다.");
                this.Close();
                Application.Exit();
            }

            this._clsCommon = new CommonUtils();
            this._clsCommon.SetLogName(this._clsInfo.m_strLogPath, "DATA");
            this._clsCommon.LogData(1, "InitializeComponent() MainForm.");
            this._TimersCls = new AgentTimer(this);

            this._clsCommon.clsinfo = this._clsInfo;
            this._clsCommon.logger = this._logger;

            this._TimersCls.Clsinfo = this._clsInfo;
            this._TimersCls.ClsCommon = this._clsCommon;

            if (!this._TimersCls.m_bIsStarted)
            {
                int nTime = this._clsInfo.m_nCheckTime;
                //if (nTime < 10) //10 Sec
                //{
                //    nTime = 10;
                //}
                nTime = nTime * 1000;
                this._TimersCls.SetTimerInterval(nTime);
                this._TimersCls.StartTimer();
            }

            try
            {
                if (winTail == null)
                {
                    winTail = new Tail(this._clsCommon.GetLogPath());
                    winTail.MoreData += new Tail.MoreDataHandler(winTail_MoreData);
                    winTail.Start();
                }
            }
            catch { }

            this._clsInfo.logger.WriteLine("\r\n Mainform_Load() 정상 ..");

            // Immediately accept the new value once the value of the control has changed
            // (for example, the dateTimePicker and the comboBox)
            dateTimePicker1.ValueChanged += new EventHandler(control_SelectedValueChanged);
			cmbWorkPC.SelectedIndexChanged += new EventHandler(control_SelectedValueChanged);
            cmbCategory.SelectedIndexChanged += new EventHandler(control_SelectedValueChanged);
            lstPort.DoubleClickActivation = true;


            //////////////////////////////////////////////////////////////////
            //// Inspect Item
            //////////////////////////////////////////////////////////////////

            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] { "Site ENV Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] { "Env Setting", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] { "Power OFF/ON Test", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] { "Alarm Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] { "Status Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] { "Inventory Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] { "Firmware Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] { "RF Test", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] { "Optic Power Check", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] { "Command Reset Test", "wait" }, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] { "Tes Data Deleting", "wait" }, -1);

            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

            this.lstInspect.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstInspect.AllowColumnReorder = true;
            this.lstInspect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1, this.columnHeader2});

            this.lstInspect.FullRowSelect = true;
            this.lstInspect.GridLines = true;
            this.lstInspect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstInspect.HideSelection = false;
            this.lstInspect.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11
            });
            //this.listView1.Location = new System.Drawing.Point(0, 38);
            this.lstInspect.MultiSelect = false;
            this.lstInspect.Name = "listView1";
            this.lstInspect.Size = new System.Drawing.Size(352, 310);
            this.lstInspect.TabIndex = 4;
            this.lstInspect.UseCompatibleStateImageBehavior = false;
            this.lstInspect.View = System.Windows.Forms.View.Details;

            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Inspect Item";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 60;

            //////////////////////////////////////////////////////////////////
            ///// RF Item
            dgv.AutoGenerateColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            //dgv.Columns.Add(checkBoxColumn);
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Test Item",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 120,
                FillWeight = 75
            }); ;
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Codition",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 120,
                FillWeight = 60
            });

            var col1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Spec",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 120,
                FillWeight = 60,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = System.Drawing.Color.Black
                }
            };
            dgv.Columns.Add(col1);

            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Result1",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 60,
                FillWeight = 25
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Result2",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 60,
                FillWeight = 25
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Result3",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 60,
                FillWeight = 25
            });

            dgv.Rows.Add(new String[] { "1.PWR FT, EVM FA-0", "3650.01/100M", "34-+dbm\n+-182.505Hz\n256QAM/5%", "", "", "" });
            dgv.Rows.Add(new String[] { "2.PWR FT, EVM FA-1", "-", "", "", "", "" });
            dgv.Rows.Add(new String[] { "3.PWR FT, EVM FA-2", "-", "", "", "", "" });
            dgv.Rows.Add(new String[] { "4.PWR FT, EVM FA-3", "-", "", "", "", "" });
            dgv.Rows.Add(new String[] { "4.ACLR-1", "-", ">45dBc", "", "", ""  });
            dgv.Rows.Add(new String[] { "5.ACLR-2", "-", ">45dBc", "", "", "" });
            dgv.Rows.Add(new String[] { "7.QBW,OBUE", "OBW<BW,PASS/FAIL", "", "", "", "" });

            /////////////////////////////////////////////////////
            ///Data Initializ
            /// /////////////////////////////////////////////////
            portManager.initialzeList();

            portNames = SerialPort.GetPortNames();
            SetPortHeader();
            RefreshPortData();

            this.ruManager.InitializeRUOpt();
            this.RURadios = new System.Windows.Forms.RadioButton[ruManager.RUDatas.Count];
            this.RUTexts = new System.Windows.Forms.TextBox[ruManager.RUDatas.Count];
            InitDrawRUOpt();

            _form_resize._get_initial_size();

            //this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);

            //Guid id = new Guid("{5F6C41E5-A7CB-47d4-AAEA-1E226CB7E7C6}");
            //manager = new ListViewPersonalisationManager(listView1, id);

        }

        private void MessageDisp(string Data, bool bDelete, int nGubun)
        {
            //this.txtLog.AppendText(Data);
            this.strLogResult = Data;
            this.nMsgGubun = nGubun;

            threadLogMain = new Thread(new ThreadStart(SetText));
            threadLogMain.Start();
            Thread.Sleep(50);
        }

        private void WriteTextSafe(string text)
        {
            if (this.txtMainLog.InvokeRequired)
            {
                try
                {
                    var d = new SafeCallDelegate(WriteTextSafe);
                    Invoke(d, new object[] { text });
                }
                catch { }
            }
            else
            {
                try
                {
                    if (this.toolCmbOption.Text == "Serial" && this.nMsgGubun == 1) text = "[Serial Message]" + this.strLogResult;
                    else if (this.toolCmbOption.Text == "Telnet" && this.nMsgGubun == 2) text = "[Telnet Message]" + this.strLogResult;
                    else text = "[General Message]" + this.strLogResult;

                    this.txtMainLog.SuspendLayout();

                    int maxChars = 65535;
                    if (text.Length > maxChars)
                    {
                        text = text.Remove(0, text.Length - maxChars);
                    }
                    int newLength = this.txtMainLog.Text.Length + text.Length;
                    if (newLength > maxChars)
                    {
                        this.txtMainLog.Text = this.txtMainLog.Text.Remove(0, newLength - (int)maxChars);
                    }

                    this.txtMainLog.AppendText(text);

                    this.txtMainLog.SelectionStart = this.txtMainLog.Text.Length;
                    this.txtMainLog.ScrollToCaret();

                    this.txtMainLog.ResumeLayout();
                }
                catch { }

            }
        }

        private void SetText()
        {
            WriteTextSafe(this.strLogResult);

        }

        private void control_SelectedValueChanged(object sender, System.EventArgs e)
		{
			lstPort.EndEditing(true);
		}

		private void lstPort_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
		{
			if (e.SubItem == 3) // Password field
			{
				// the current value (text) of the subitem is ****, so we have to provide
				// the control with the actual text (that's been saved in the item's Tag property)
				//e.Item.SubItems[e.SubItem].Text = e.Item.Tag.ToString();
			}

			lstPort.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
		}

		private void lstPort_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
		{
			/*if (e.SubItem == 3) // Password field
			{
				if (e.Cancel)
				{
					e.DisplayText = new string(textBoxPassword.PasswordChar, e.Item.Tag.ToString().Length);
				}
				else
				{
					// in order to display a series of asterisks instead of the plain password text
					// (textBox.Text _gives_ plain text, after all), we have to modify what'll get
					// displayed and save the plain value somewhere else.
					string plain = e.DisplayText;
					e.DisplayText = new string(textBoxPassword.PasswordChar, plain.Length);
					e.Item.Tag = plain;
				}
			}*/
		}

		private void checkBoxDoubleClickActivation_CheckedChanged(object sender, System.EventArgs e)
		{
			lstPort.DoubleClickActivation = checkBoxDoubleClickActivation.Checked;
		}

		private void lstPort_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// To show the real password (remember, the subitem's Text _is_ '*******'),
			// set the tooltip to the ListViewItem's tag (that's where the password is stored)
			ListViewItem item;
			int idx = lstPort.GetSubItemAt(e.X, e.Y, out item);
			if (item != null && idx == 3)
				toolTip1.SetToolTip(lstPort, item.Tag.ToString());
			else
				toolTip1.SetToolTip(lstPort, null);
		}

		private void toolTip1_Popup(object sender, PopupEventArgs e)
		{

		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			try
			{
				_form_resize._resize();
			}
			catch { }

		}

        private void lstInspect_MouseDown(object sender, MouseEventArgs e)
        {
            _itemDnD = lstInspect.GetItemAt(e.X, e.Y);
            // if the LV is still empty, no item will be found anyway, so we don't have to consider this case
        }

        private void lstInspect_MouseMove(object sender, MouseEventArgs e)
        {
            if (_itemDnD == null)
                return;

            // Show the user that a drag operation is happening
            Cursor = Cursors.Hand;

            // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
            int lastItemBottom = Math.Min(e.Y, lstInspect.Items[lstInspect.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

            // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
            ListViewItem itemOver = lstInspect.GetItemAt(0, lastItemBottom);

            if (itemOver == null)
                return;

            Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);
            if (e.Y < rc.Top + (rc.Height / 2))
            {
                lstInspect.LineBefore = itemOver.Index;
                lstInspect.LineAfter = -1;
            }
            else
            {
                lstInspect.LineBefore = -1;
                lstInspect.LineAfter = itemOver.Index;
            }

            // invalidate the LV so that the insertion line is shown
            lstInspect.Invalidate();

        }

        private void lstInspect_MouseUp(object sender, MouseEventArgs e)
        {
            if (_itemDnD == null)
                return;

            try
            {
                // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
                int lastItemBottom = Math.Min(e.Y, lstInspect.Items[lstInspect.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

                // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
                ListViewItem itemOver = lstInspect.GetItemAt(0, lastItemBottom);

                if (itemOver == null)
                    return;

                Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);

                // find out if we insert before or after the item the mouse is over
                bool insertBefore;
                if (e.Y < rc.Top + (rc.Height / 2))
                {
                    insertBefore = true;
                }
                else
                {
                    insertBefore = false;
                }

                if (_itemDnD != itemOver) // if we dropped the item on itself, nothing is to be done
                {
                    if (insertBefore)
                    {
                        lstInspect.Items.Remove(_itemDnD);
                        lstInspect.Items.Insert(itemOver.Index, _itemDnD);
                    }
                    else
                    {
                        lstInspect.Items.Remove(_itemDnD);
                        lstInspect.Items.Insert(itemOver.Index + 1, _itemDnD);
                    }
                }

                // clear the insertion line
                lstInspect.LineAfter =
                lstInspect.LineBefore = -1;

                lstInspect.Invalidate();

            }
            finally
            {
                // finish drag&drop operation
                _itemDnD = null;
                Cursor = Cursors.Default;
            }

        }

        private void SetHeight(ListView LV, int height)
        {
            // listView 높이 지정
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            LV.SmallImageList = imgList;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void InitDrawRUOpt()
        {
            for (int i = 0; i < ruManager.RUDatas.Count; ++i)
            {
                RURadios[i] = new RadioButton();
                RURadios[i].Name = "DUOpt_"+Convert.ToString(i);
                RURadios[i].Text = ruManager.RUDatas[i].RUDispText;
                RURadios[i].Checked = ruManager.RUDatas[i].OptValue;
                RURadios[i].Width = ruManager.RUDatas[i].optWidth;
                RURadios[i].Height = ruManager.RUDatas[i].optHeight;
                RURadios[i].BackColor = ruManager.RUDatas[i].deFaultOptColor;
                RURadios[i].Location = new System.Drawing.Point(ruManager.RUDatas[i].optPosX, ruManager.RUDatas[i].optPosY);

                RUTexts[i] = new TextBox();
                RUTexts[i].Name = "DUTxt_" + Convert.ToString(i);
                RUTexts[i].Text = "";
                RUTexts[i].Width = ruManager.RUDatas[i].txtWidth;
                RUTexts[i].Height = ruManager.RUDatas[i].txtHeight;
                RUTexts[i].BackColor = ruManager.RUDatas[i].deFaultTxtColor;
                RUTexts[i].Location = new System.Drawing.Point(ruManager.RUDatas[i].txtPosX, ruManager.RUDatas[i].txtPosY);

                this.panOptGrp.Controls.Add(RURadios[i]);
                this.panOptGrp.Controls.Add(RUTexts[i]);
            }

        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = "";
            if (cmbModel.SelectedIndex >= 0)
            {
                itemSelected = cmbModel.SelectedItem as string;
            }

            //MessageBox.Show(itemSelected + ":" + Convert.ToString(cmbModel.SelectedIndex));
            int nCount = 0;
            if (cmbModel.SelectedIndex == 0) nCount = 2;
            else if (cmbModel.SelectedIndex == 1) nCount = 64;
            else if (cmbModel.SelectedIndex == 2) nCount = 32;

            dispDrawRUOpt(nCount);

        }

        private void dispDrawRUOpt(int nCount)
        {
            foreach (Control control in this.panOptGrp.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    radio.Visible = false;
                }
                if (control is TextBox)
                {
                    TextBox txtData = control as TextBox;
                    txtData.Visible = false;
                }
            }

            for (int i = 0; i < nCount; ++i)
            {
                RURadios[i].Text = Convert.ToString(i);
                RURadios[i].Visible = true;
                RUTexts[i].Visible = true;
            }

        }

        private void SetPortHeader()
        {
            //////////////////////////////////////////////////////////////////
            //// 포트관리
            //////////////////////////////////////////////////////////////////
            // Fill combo
            cmbWorkPC.Items.AddRange(new string[] { "aging_pc1", "aging_pc2", "Mary", "mass_pc", "gi_pc" });
            cmbCheck.Items.AddRange(new string[] { "uncheck", "check" });
            cmbInhibit.Items.AddRange(new string[] { "", "inhibit all", "inhibit" });

            for (int i = 0; i <this._clsInfo.nCategoryCnt; i++)
            {
                cmbCategory.Items.Add(this._clsInfo.arrCategory[i]);
            }

            for (int i = 0; i < this._clsInfo.nModelCnt; i++)
            {
                try
                {
                    string strData = this._clsInfo.arrModel[i];
                    string strBuyer = "";
                    string strModel = "";
                    string[] arrData = strData.Split(';');
                    if (arrData.Length > 1)
                    {
                        strBuyer = arrData[0];
                        strModel = arrData[1];

                        if (!cmbBuyer.Items.Contains(strBuyer))
                        {
                            cmbBuyer.Items.Add(strBuyer);
                        }
                        cmbModel.Items.Add(strModel);
                    }
                }
                catch { }
            }

            for (int i = 0; i < this._clsInfo.nAgingModelCnt; i++)
            {
                cmbAgingModel.Items.Add(this._clsInfo.arrAgingModel[i]);
            }

            // Add Columns
            lstPort.Columns.Add("순서", 60, HorizontalAlignment.Right);          //1.seq
            lstPort.Columns.Add("포트", 80, HorizontalAlignment.Left);           //2.CommPort
            lstPort.Columns.Add("실행상태", 120, HorizontalAlignment.Left);     //3.Run taus
            lstPort.Columns.Add("Category", 80, HorizontalAlignment.Left);       //4.Product Category
            lstPort.Columns.Add("FERT CODE", 120, HorizontalAlignment.Left);     //5.FERT Code
            lstPort.Columns.Add("FERT S/N", 80, HorizontalAlignment.Left);       //6.FERT S/N
            lstPort.Columns.Add("UMP S/N", 80, HorizontalAlignment.Left);        //7.UMP S?N
            lstPort.Columns.Add("ECP1 S/N", 80, HorizontalAlignment.Left);       //8.ECP1 S/N
            lstPort.Columns.Add("ECP2 S/N", 80, HorizontalAlignment.Left);       //9.ECP2 S/N
            lstPort.Columns.Add("ECP3 S/N", 80, HorizontalAlignment.Left);       //10.ECP3 S/N
            lstPort.Columns.Add("au_rssi", 80, HorizontalAlignment.Left);        //11.au_rssi 
            lstPort.Columns.Add("au 3:1:1 TDD", 120, HorizontalAlignment.Left);   //12.au 3:1:1 TDD
            lstPort.Columns.Add("IMEI S/N", 90, HorizontalAlignment.Left);       //13.IMEI S/N
            lstPort.Columns.Add("USIM", 80, HorizontalAlignment.Left);           //14.USIM_Check
            lstPort.Columns.Add("CH Only", 80, HorizontalAlignment.Left);        //15.CH Only
            lstPort.Columns.Add("Alarm", 80, HorizontalAlignment.Left);          //16.Alarm
            lstPort.Columns.Add("telnet server", 100, HorizontalAlignment.Left); //17.telnet server
            lstPort.Columns.Add("시작시간", 80, HorizontalAlignment.Left);     //18.start Date
            lstPort.Columns.Add("종료시간", 80, HorizontalAlignment.Left);       //19.end date
            lstPort.Columns.Add("작업PC", 80, HorizontalAlignment.Left);        //20.test pc
            lstPort.Columns.Add("실행결과", 80, HorizontalAlignment.Left);         //21.Result
            
            Editors = new Control[] {
                                    textBoxComment,		// for column 1
                                    textBoxComment,		// for column 2
                                    textBoxComment,		// for column 3
                                    cmbCategory,  		// for column 4
                                    textBoxComment,		// for column 5
                                    textBoxComment,		// for column 6
                                    textBoxComment,		// for column 7
                                    textBoxComment,		// for column 8
                                    textBoxComment,		// for column 9
                                    textBoxComment,		// for column 10
                                    cmbCheck,  		// for column 11
                                    cmbCheck,  		// for column 12
                                    textBoxComment,		// for column 13
                                    cmbCheck,  		// for column 14
                                    cmbCheck,  		// for column 15
                                    cmbInhibit,  		// for column 16
                                    textBoxComment,		// for column 17
                                    textBoxComment,		// for column 18
                                    textBoxComment,		// for column 19
                                    textBoxComment,		// for column 20
									textBoxComment		// for column 21
									};


            SetHeight(this.lstPort, 20);

        }

        private void RefreshPortData()
        {
            this.lstPort.Items.Clear();
            ListViewItem lvi;

            for (int i = 0; i < this.portManager.PORTDatas.Count; i++)
            {
                try
                {
                    PORTData portData = this.portManager.PORTDatas[i];

                    lvi = new ListViewItem(Convert.ToString(portData.Seq));
                    lvi.SubItems.Add(portData.PortName);
                    lvi.SubItems.Add(portData.RunStatus);
                    lvi.SubItems.Add(portData.Category);
                    lvi.SubItems.Add(portData.FertCode);
                    lvi.SubItems.Add(portData.FertSN);
                    lvi.SubItems.Add(portData.UmpSN);
                    lvi.SubItems.Add(portData.ECP1SN);
                    lvi.SubItems.Add(portData.ECP2SN);
                    lvi.SubItems.Add(portData.ECP3SN);
                    if (portData.Au_Rssi) lvi.SubItems.Add("check");
                    else lvi.SubItems.Add("uncheck");
                    if (portData.Au_311) lvi.SubItems.Add("check");
                    else lvi.SubItems.Add("uncheck");
                    lvi.SubItems.Add(portData.IEMISN);
                    if (portData.USIM) lvi.SubItems.Add("check");
                    else lvi.SubItems.Add("uncheck");
                    if (portData.CHOnly) lvi.SubItems.Add("check");
                    else lvi.SubItems.Add("uncheck");
                    lvi.SubItems.Add(portData.Alarm);
                    lvi.SubItems.Add(portData.TelnetServer);
                    lvi.SubItems.Add(portData.StartTime);
                    lvi.SubItems.Add(portData.EndTime);
                    lvi.SubItems.Add(portData.WorkPC);
                    lvi.SubItems.Add(portData.Result);

                    this.lstPort.Items.Add(lvi);
                }
                catch { }
            }

            for (int k = 0; k < this.lstPort.Items.Count; k++)
            {
                PORTData portData = this.portManager.PORTDatas[k];

                ListViewItem lvi2 = this.lstPort.Items[k];
                bool bIsFind = false;

                if (portData.bUseComm)
                {
                    string strFindName = lvi2.SubItems[1].Text;
                    for (int j = 0; j < portNames.Length; j++)
                    {
                        if (portNames[j].Equals(strFindName)) bIsFind = true;
                    }
                } else if (portData.bUseTelnet && portData.UserID != "" && portData.Password != "")
                {
                    bIsFind = true;
                }

                if (bIsFind)
                {
                    lvi2.SubItems[2].Text = "stopped";
                    lvi2.BackColor = Color.Aqua;
                }
                else
                {
                    lvi2.SubItems[2].Text = "NotFound";
                    lvi2.BackColor = Color.Gray;
                }
                
            }
            
        }

        private void winTail_MoreData(object tailObject, string newData)
        {
            //MessageDisp(newData, false, 0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveAllPortList();

            if (this._TimersCls.m_bIsStarted)
            {
                this._TimersCls.StopTimer();
            }

            try
            {
                if (winTail != null)
                {
                    winTail.Stop();
                    winTail = null;
                }
            }
            catch { }

            if (this.portManager != null)
            {
                this.portManager.DisconnectALL();
            }

        }

        private void lstPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lstPort.FocusedItem.Bounds.Contains(e.Location))
                {
                    if (lstPort.SelectedItems.Count < 1) return;

                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;

                    contextMenuStrip1.Visible = true;

                    try
                    {
                        int nIndex = Convert.ToInt32(strSeq);
                        nIndex = nIndex - 1;
                        if (nIndex < 0) nIndex = 0;
                        PORTData portData = portManager.PORTDatas[nIndex];
                        if (strPort == portData.PortName)
                        {
                            if (!portData.bIsConnected)
                            {
                                this.menuConnect.Enabled = true;
                                this.menuDisconnect.Enabled = false;
                                this.menuLogon.Enabled = false;
                                this.menuStart.Enabled = false;
                                this.menStop.Enabled = false;
                            } else if (portData.bIsConnected)
                            {
                                this.menuConnect.Enabled = false;
                                this.menuDisconnect.Enabled = true;

                                if (portData.bIsLoged)
                                {
                                    this.menuLogon.Enabled = false;

                                    if (portData.bIsStarted)
                                    {
                                        this.menuStart.Enabled = false;
                                        this.menStop.Enabled = true;
                                    } else
                                    {
                                        this.menuStart.Enabled = true;
                                        this.menStop.Enabled = false;
                                    }

                                } else
                                {
                                    this.menuLogon.Enabled = true;
                                }

                            }

                        }
                    }
                    catch { }

                    //contextMenuStrip1.Visible = true;
                    //contextMenuStrip1.Show(e.Location);
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void menuStatus_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;
                    string strTelnetAddr = lstPort.SelectedItems[0].SubItems[16].Text;
                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);
                    string strStatus = lstPort.SelectedItems[0].SubItems[2].Text;
                    this.toolStatus.Text = strStatus;                    

                    this.toolTxtCurrSeq.Text = strSeq;
                    strCurrSeq = this.toolTxtCurrSeq.Text;

                    string strDispText = "";
                    if (strTelnetAddr != "" && strTelnetAddr != "Empty")
                    {
                        strDispText = "SEQ(" + strCurrSeq + ") : " + strPort + " / " + strTelnetAddr + " / " + strCategory + " / " + strFertCode;
                    } else
                    {
                        strDispText = "SEQ(" + strCurrSeq + ") : " + strPort + " / " + strCategory + " / " + strFertCode;

                    }
                    this.lblStatusDisp.Text = strDispText;

                    if (this.portManager != null)
                    {
                        this.portManager.Status(strPort);
                    }
                }
                catch { }
            }

        }

        private void menuConnect_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;

                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    if (strSeq != strCurrSeq)
                    {
                        MessageBox.Show("현재 상태보기 저장된 항목만 실행할 수 있습니다.");
                        return;
                    }

                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);
                    if (this.portManager != null)
                    {
                        this.portManager.Connect(strPort);
                    }

                }
                catch { }
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;

                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    if (strSeq != strCurrSeq)
                    {
                        MessageBox.Show("현재 상태보기 저장된 항목만 실행할 수 있습니다.");
                        return;
                    }

                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);

                    if (this.portManager != null)
                    {
                        this.portManager.Disconnect(strPort);
                    }

                }
                catch { }
            }
        }

        private void menuLogon_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;

                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    if (strSeq != strCurrSeq)
                    {
                        MessageBox.Show("현재 상태보기 저장된 항목만 실행할 수 있습니다.");
                        return;
                    }

                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);
                    if (this.portManager != null)
                    {
                        this.portManager.Logon(strPort);
                    }

                }
                catch { }
            }
        }

  
        private void lstPort_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //if (e.IsSelected && e.Item.BackColor == Color.Gray)
            if (e.IsSelected && e.Item.SubItems[2].Text == "NotFound")
            {
                //SKICOMP - Config Menu
                e.Item.Selected = false;
                this.contextMenuStrip1.Visible = false;
            } 
        }

        private void menuStart_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;

                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    if (strSeq != strCurrSeq)
                    {
                        MessageBox.Show("현재 상태보기 저장된 항목만 실행할 수 있습니다.");
                        return;
                    }

                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);
                    if (this.portManager != null)
                    {
                        this.portManager.Start(strPort);
                    }

                }
                catch { }
            }

        }

        private void menStop_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;
                    String strPort = lstPort.SelectedItems[0].SubItems[1].Text;
                    String strCategory = lstPort.SelectedItems[0].SubItems[3].Text;
                    string strFertCode = lstPort.SelectedItems[0].SubItems[4].Text;

                    String strSeq = lstPort.SelectedItems[0].SubItems[0].Text;
                    if (strSeq != strCurrSeq)
                    {
                        MessageBox.Show("현재 상태보기 저장된 항목만 실행할 수 있습니다.");
                        return;
                    }

                    //MessageBox.Show("[" + clickedItem.Text + "]: 포트번호:" + strPort + " 카테고리:" + strCategory + " FertCode:" + strFertCode);
                    if (this.portManager != null)
                    {
                        this.portManager.Stop(strPort);
                    }

                }
                catch { }
            }

        }

        private void menuConfig_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                try
                {
                    //SKICOMP - menu popup
                    if (lstPort.SelectedItems[0].SubItems[2].Text == "NotFound") return;
                    if (lstPort.SelectedItems[0].BackColor.Name == "Gray") return;

                    frmConfig configForm = new frmConfig();
                    configForm.mainForm = this;
                    configForm.Show();
                }
                catch { }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ConnectTest testForm = new ConnectTest(); ;
            testForm.Show();
        }

        public string getPortItem(int nIndex)
        {
            string strReturn = "";

            if (lstPort.SelectedItems.Count > 0)
            {
                strReturn = lstPort.SelectedItems[0].SubItems[1].Text;
            }

            return strReturn;
        }

        public string setPortItem(int nIndex)
        {
            string strReturn = "";

            if (lstPort.SelectedItems.Count > 0)
            {
                strReturn = lstPort.SelectedItems[0].SubItems[1].Text;
            }

            return strReturn;
        }

        public void saveCommData(string strComm)
        {
            if (strComm == "") return;

            try
            {
                string[] arrData = strComm.Split(';');
                if (arrData.Length < this._clsInfo.nCommSplitCnt) return;

                string strCommName = arrData[0];
                string strCommUse = arrData[1];
                string strCommSpeed = arrData[2];
                string strCommData = arrData[3];
                string strCommParity = arrData[4];
                string strCommStop = arrData[5];
                string strCommFlow = arrData[6];
                string strTelnetIP = arrData[7];
                string strTelnetUse = arrData[8];
                string strUserID = arrData[9];
                string strPassword = arrData[10];
                string strEncode = arrData[11];

                int nCommCount = this._clsInfo.nCommCnt; 
                int nFindIndex = 0;
                bool bIsFind = false;
                for (int i = 0; i < nCommCount; i++)
                {
                    string[] arrIniData = this._clsInfo.arrComm[i].Split(';');
                    if (arrIniData.Length < this._clsInfo.nCommSplitCnt) return;

                    if (strCommName == arrIniData[0])
                    {
                        nFindIndex = i + 1;
                        bIsFind = true;
                        break;
                    }
                }

                if (nFindIndex < 1) nFindIndex = nCommCount + 1;

                int nSetCnt = 0;
                if (bIsFind) nSetCnt = nCommCount;
                else nSetCnt = nFindIndex;

                string strSection = "CommSet";
                string strKey = "CommSetCnt";
                string strType = "int";
                this._clsInfo.SetConfigValue(strSection, strKey, Convert.ToString(nSetCnt), strType);

                strSection = "CommSet";
                strKey = "Comm" + Convert.ToString(nFindIndex);
                strType = "string";
                this._clsInfo.SetConfigValue(strSection, strKey, strComm, strType);

                // PORTData 변경
                for (int i = 0; i <  portManager.PORTDatas.Count; i++)
                {
                    PORTData portData = portManager.PORTDatas[i];
                    if (strCommName == portData.PortName)
                    {
                        if (strCommUse == "checked") portData.bUseComm = true;
                        else portData.bUseComm = false;
                        portData.nCommSpeed = Convert.ToInt32(strCommSpeed);
                        portData.CommData = strCommData;
                        portData.CommParity = strCommParity;
                        portData.CommStop = strCommStop;
                        portData.CommFlow = strCommFlow;
                        if (strTelnetUse == "checked") portData.bUseTelnet = true;
                        else portData.bUseTelnet = false;
                        portData.TelnetServer = strTelnetIP;
                        portData.UserID = strUserID;
                        portData.Password = strPassword;
                        portData.Encoding = strEncode;
                        portListTelnetUpdate(i);
                    }
                }
            }
            catch { }

        }

        public void portListTelnetUpdate(int arrayIndex)
        {
            PORTData portData = portManager.PORTDatas[arrayIndex];
            string strPortName = portData.PortName;

            for (int k = 0; k < this.lstPort.Items.Count; k++)
            {
                ListViewItem lvi = this.lstPort.Items[k];
                string strFindName = lvi.SubItems[1].Text;
                if (strPortName == strFindName)
                {
                    lvi.SubItems[16].Text = portData.TelnetServer;
                }
            }
        }

        /// <summary>
        /// ////
        /// </summary>
        /// <param name="arrayIndex"></param>
        public void portListChangeStatus(PORTData portData)
        {
            this.currPortData = portData;

            threadPortChange = new Thread(new ThreadStart(SetChange));
            threadPortChange.Start();
            Thread.Sleep(50);


            //PORTData portData = portManager.PORTDatas[arrayIndex];
            /*string strPortName = portData.PortName;

            for (int k = 0; k < this.lstPort.Items.Count; k++)
            {
                ListViewItem lvi = this.lstPort.Items[k];
                string strFindName = lvi.SubItems[1].Text;
                if (strPortName == strFindName)
                {
                    lvi.SubItems[2].Text = portData.RunStatus;
                    this.toolStatus.Text = portData.RunStatus;
                }
            }*/
        }


        private void SetChange()
        {
            if (this.currPortData != null)
            {
                WriteTextChange(this.currPortData.RunStatus);
            }

        }

        private void WriteTextChange(string text)
        {
            if (this.lstPort.InvokeRequired)
            {
                try
                {
                    var d = new SafeCallDelegate(WriteTextChange);
                    Invoke(d, new object[] { text });
                }
                catch { }
            }
            else
            {
                try
                {
                    if (this.currPortData != null)
                    {
                        this.lstPort.SuspendLayout();

                        string strPortName = this.currPortData.PortName;

                        for (int k = 0; k < this.lstPort.Items.Count; k++)
                        {
                            ListViewItem lvi = this.lstPort.Items[k];
                            string strFindName = lvi.SubItems[1].Text;
                            if (strPortName == strFindName)
                            {
                                lvi.SubItems[2].Text = this.currPortData.RunStatus;
                                this.toolStatus.Text = this.currPortData.RunStatus;
                            }
                        }

                        this.lstPort.ResumeLayout();
                    }
                }
                catch { }

            }
        }

        /// <summary>
        /// 포트리스트 변경 데이타를 Config.ini에 저장
        /// </summary>
        public void saveAllPortList()
        {
            try
            {
                for (int k = 0; k < this.lstPort.Items.Count; k++)
                {
                    ListViewItem lvi = this.lstPort.Items[k];

                    string strTemp = "";
                    string strSeq = lvi.SubItems[0].Text;
                    string strPortName = lvi.SubItems[1].Text;
                    strTemp = lvi.SubItems[2].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strStatus = strTemp;
                    string strCategory = lvi.SubItems[3].Text;
                    strTemp = lvi.SubItems[4].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strFertCode = strTemp;
                    strTemp = lvi.SubItems[5].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strFertSN = strTemp;
                    strTemp = lvi.SubItems[6].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strUmpSN = strTemp;
                    strTemp = lvi.SubItems[7].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strECP1 = strTemp;
                    strTemp = lvi.SubItems[8].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strECP2 = strTemp;
                    strTemp = lvi.SubItems[9].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strECP3 = strTemp;
                    strTemp = lvi.SubItems[10].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strAu_Rssi = strTemp;
                    strTemp = lvi.SubItems[11].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strAu_3_1_1 = strTemp;
                    strTemp = lvi.SubItems[12].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strIEMI = strTemp;
                    strTemp = lvi.SubItems[13].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strUSIM = strTemp;
                    strTemp = lvi.SubItems[14].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strCHOnly = strTemp;
                    strTemp = lvi.SubItems[15].Text;
                    string strAlarm = strTemp; //inherit
                    strTemp = lvi.SubItems[16].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strTelnet = strTemp;
                    strTemp = lvi.SubItems[17].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strStartTime = strTemp;
                    strTemp = lvi.SubItems[18].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strEndTime = strTemp;
                    strTemp = lvi.SubItems[19].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strWorkPC = strTemp;
                    strTemp = lvi.SubItems[20].Text;
                    if (strTemp == "") strTemp = "Empty";
                    string strResult = strTemp;

                    string strData = strSeq;
                    strData = strData + ";" + strPortName;
                    strData = strData + ";" + strStatus;
                    strData = strData + ";" + strCategory;
                    strData = strData + ";" + strFertCode;
                    strData = strData + ";" + strFertSN;
                    strData = strData + ";" + strUmpSN;
                    strData = strData + ";" + strECP1;
                    strData = strData + ";" + strECP2;
                    strData = strData + ";" + strECP3;
                    strData = strData + ";" + strAu_Rssi;
                    strData = strData + ";" + strAu_3_1_1;
                    strData = strData + ";" + strIEMI;
                    strData = strData + ";" + strUSIM;
                    strData = strData + ";" + strCHOnly;
                    strData = strData + ";" + strAlarm;
                    strData = strData + ";" + strTelnet;
                    strData = strData + ";" + strStartTime;
                    strData = strData + ";" + strEndTime;
                    strData = strData + ";" + strWorkPC;
                    strData = strData + ";" + strResult;

                    string strSection = "PORT";
                    string strKey = "Port" + Convert.ToString(k+1);
                    string strType = "string";
                    this._clsInfo.SetConfigValue(strSection, strKey, strData, strType);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolBtnConfig_Click(object sender, EventArgs e)
        {

        }

        public void getPortMsg(int nGubun, string strPortName, string strText)
        {
            MessageDisp(strText, false, nGubun);
        }

        private void toolTxtCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                
                if (this.portManager.clsCurrConnector == null)
                {
                    MessageBox.Show("선택된 활성화된 연결이 없습니다.");
                    return;
                }

                if (this.toolCmbOption.Text == "Serial")
                {
                    if (!this.portManager.clsCurrConnector.bIsLogon)
                    {
                        MessageBox.Show("로그온된 장치가 없습니다.");
                        return;
                    }

                    if (this.portManager.clsCurrConnector.m_PortData.portMan.CommPort != null &&
                         this.portManager.clsCurrConnector.m_PortData.portMan.CommPort.IsOpen)
                    {
                        this.portManager.clsCurrConnector.m_PortData.portMan.CommPort.Write(this.toolTxtCommand.Text + (char)13);
                        //txtLog.AppendText(this.txtCommand.Text + Environment.NewLine);

                        if (this.toolTxtCommand.Text.Trim().ToLower() == "exit")
                        {
                            Thread.Sleep(3000);
                            this.portManager.Disconnect(this.portManager.clsCurrConnector.m_PortData.PortName);
                            //MessageBox.Show("COMM 연결이 해제되었습니다.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("COMM 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                    }
                }
                else
                {
                    if (!this.portManager.clsCurrConnector.bIsLogon)
                    {
                        MessageBox.Show("로그온된 텔렛서버가 없습니다.");
                        return;
                    }

                    if (this.portManager.clsCurrConnector.m_PortData.portMan.tc == null || 
                        !this.portManager.clsCurrConnector.m_PortData.portMan.tc.IsConnected)
                    {
                        MessageBox.Show("텔렛 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                    }
                    else
                    {
                        this.portManager.clsCurrConnector.m_PortData.portMan.tc.WriteLine(this.toolTxtCommand.Text.Trim() + (char)13);
                        //MessageDisp(tc.Read());

                        if (this.toolTxtCommand.Text.Trim().ToLower() == "exit")
                        {
                            this.portManager.Disconnect(this.portManager.clsCurrConnector.m_PortData.PortName);
                        }

                    }

                }

                this.toolTxtCommand.Text = "";

            }

        }

        private void toolBtnSend_Click(object sender, EventArgs e)
        {
            if (this.portManager.clsCurrConnector == null)
            {
                MessageBox.Show("선택된 활성화된 연결이 없습니다.");
                return;
            }

            if (this.toolCmbOption.Text == "Serial")
            {
                if (!this.portManager.clsCurrConnector.bIsLogon)
                {
                    MessageBox.Show("로그온된 장치가 없습니다.");
                    return;
                }

                //this.portManager.clsCurrConnector.m_PortData.portMan.CommPort.Write(this.toolTxtCommand.Text + (char)13);

                //MessageBox.Show(this.txtCommand.Text);
                if (this.portManager.clsCurrConnector.m_PortData.portMan.CommPort != null &&
                     this.portManager.clsCurrConnector.m_PortData.portMan.CommPort.IsOpen)
                {
                    this.portManager.clsCurrConnector.m_PortData.portMan.CommPort.Write(this.toolTxtCommand.Text + (char)13);
                    //txtLog.AppendText(this.txtCommand.Text + Environment.NewLine);

                    if (this.toolTxtCommand.Text.Trim().ToLower() == "exit")
                    {
                        Thread.Sleep(3000);
                        this.portManager.Disconnect(this.portManager.clsCurrConnector.m_PortData.PortName);
                        //MessageBox.Show("COMM 연결이 해제되었습니다.");
                    }

                }
                else
                {
                    MessageBox.Show("COMM 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                }
            }
            else
            {
                if (!this.portManager.clsCurrConnector.bIsLogon)
                {
                    MessageBox.Show("로그온된 텔렛서버가 없습니다.");
                    return;
                }

                if (this.portManager.clsCurrConnector.m_PortData.portMan.tc == null ||
                    !this.portManager.clsCurrConnector.m_PortData.portMan.tc.IsConnected)
                {
                    MessageBox.Show("텔렛 연결이 되어있지 않습니다. 연결 후 사용하세요.");
                }
                else
                {
                    this.portManager.clsCurrConnector.m_PortData.portMan.tc.WriteLine(this.toolTxtCommand.Text.Trim() + (char)13);
                    //MessageDisp(tc.Read());

                    if (this.toolTxtCommand.Text.Trim().ToLower() == "exit")
                    {
                        this.portManager.Disconnect(this.portManager.clsCurrConnector.m_PortData.PortName);
                    }

                }

            }

            this.toolTxtCommand.Text = "";

        }

    }
}
