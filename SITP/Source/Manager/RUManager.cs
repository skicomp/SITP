using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SITP
{
    public class RUManager
    {
        private Form1 MainForm { get; set; }

        public List<RUData> RUDatas { get; set; }

        public RUManager()
        {
        }

        public RUManager(Form1 _form_)
        {
            MainForm = _form_; //the calling form
        }

        public void InitializeRUOpt()
        {
            RUDatas = new List<RUData>();

            int nOptStartX = 12;
            int nOptStartY = 3;
            int nTxtStartX = 12;
            int nTxtStartY = 18;
            double nMok = 0;
            double nRemainder = 0;

            for (int i = 0; i < 64; i++)
            {
                RUData RU = new RUData();
                RU.RUNo = i;
                RU.RUName = Convert.ToString(i);
                RU.RUDispText = Convert.ToString(i); ;
                RU.RUPathNo = 0;
                RU.OptValue = false;
                RU.Visible = false;
                RU.RUStatusVal = 0;
                RU.RUStatusText = "";
                RU.deFaultOptColor = Color.FromName("SlateBlue");
                RU.deFaultTxtColor = Color.FromName("Gold");
                RU.optWidth = 60;
                RU.optHeight = 14;
                RU.txtWidth = 60;
                RU.txtHeight = 21;

                //가로
                int nCheck = i;

                if (nCheck > 0)
                {
                    nMok = nCheck / 8;
                    nRemainder = nCheck % 8;
                } else
                {
                    nMok = 0;
                    nRemainder = 0;
                }

                /*RU.optPosX = nOptStartX + ((int)nRemainder * 68);
                RU.optPosY = nOptStartY + ((int)nMok * 38);
                RU.txtPosX = nTxtStartX + ((int)nRemainder * 68);
                RU.txtPosY = nTxtStartY + ((int)nMok * 38);*/

                int nWeightX = 0;
                if (nMok == 0) nWeightX = 0;
                else if (nMok == 1) nWeightX = 2;
                else if (nMok == 2) nWeightX = 4;
                else if (nMok == 3) nWeightX = 6;
                else if (nMok == 4) nWeightX = 1;
                else if (nMok == 5) nWeightX = 3;
                else if (nMok == 6) nWeightX = 5;
                else if (nMok == 7) nWeightX = 7;

                RU.optPosX = nOptStartX + ((int)nWeightX * 68);
                RU.optPosY = nOptStartY + ((int)nRemainder * 38);
                RU.txtPosX = nTxtStartX + ((int)nWeightX * 68);
                RU.txtPosY = nTxtStartY + ((int)nRemainder * 38);

                RUDatas.Add(RU);

            }
        }


    }
}
