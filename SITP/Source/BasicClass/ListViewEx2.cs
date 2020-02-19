using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SITP
{
    /// <summary>
    /// A slightly extended version of the standard ListView.
    /// It has two additional properties to draw an insertion line before or after an item.
    /// </summary>
    public class ListViewEx2 : ListView
    {
        // from WinUser.h
        private const int WM_PAINT = 0x000F;

        private const int LVM_FIRST = 0x1000;
        private const int LVM_INSERTITEMA = (ListViewEx2.LVM_FIRST + 7);
        private const int LVM_INSERTITEMW = (ListViewEx2.LVM_FIRST + 77);

        private Graphics graphics;

        public ListViewEx2()
        {
            // Reduce flicker
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.graphics = this.CreateGraphics();
            base.View = View.Details;

            this.AutoSizeRowHeight = true;

        }

        private int _LineBefore = -1;
        /// <summary>
        /// If set to a value >= 0, an insertion line is painted before the item with the given index.
        /// </summary>
        public int LineBefore
        {
            get { return _LineBefore; }
            set { _LineBefore = value; }
        }

        private int _LineAfter = -1;
        /// <summary>
        /// If set to a value >= 0, an insertion line is painted after the item with the given index.
        /// </summary>
        public int LineAfter
        {
            get { return _LineAfter; }
            set { _LineAfter = value; }
        }

        protected override void WndProc(ref Message m)
        {
            //base.WndProc(ref m);

            // We have to take this way (instead of overriding OnPaint()) because the ListView is just a wrapper
            // around the common control ListView and unfortunately does not call the OnPaint overrides.
            if (m.Msg == WM_PAINT)
            {
                base.WndProc(ref m);

                if (LineBefore >= 0 && LineBefore < Items.Count)
                {
                    Rectangle rc = Items[LineBefore].GetBounds(ItemBoundsPortion.Entire);
                    DrawInsertionLine(rc.Left, rc.Right, rc.Top);
                }
                if (LineAfter >= 0 && LineBefore < Items.Count)
                {
                    Rectangle rc = Items[LineAfter].GetBounds(ItemBoundsPortion.Entire);
                    DrawInsertionLine(rc.Left, rc.Right, rc.Bottom);
                }

                return;
            }
            /*if (m.Msg == ListViewEx2.LVM_INSERTITEMA || m.Msg == ListViewEx2.LVM_INSERTITEMW)
            {
                ListViewItem lvi = this.Items[this.Items.Count - 1];

                for (int i = 0; i < lvi.SubItems.Count; ++i)
                {
                    ListViewItem.ListViewSubItem lvsi = lvi.SubItems[i];

                    string text = lvsi.Text;

                    int tmpHeight = 0;
                    int maxWidth = this.Columns[i].Width;

                    SizeF stringSize = this.graphics.MeasureString(text, this.Font);

                    if (stringSize.Width > 0)
                    {
                        tmpHeight = (int)Math.Ceiling((stringSize.Width / maxWidth) * stringSize.Height);

                        if (tmpHeight > this.rowHeight)
                        {
                            this.RowHeight = tmpHeight;
                        }
                    }
                }

                base.WndProc(ref m);
                return;
            }*/

            base.WndProc(ref m);

        }

        /// <summary>
        /// Draw a line with insertion marks at each end
        /// </summary>
        /// <param name="X1">Starting position (X) of the line</param>
        /// <param name="X2">Ending position (X) of the line</param>
        /// <param name="Y">Position (Y) of the line</param>
        private void DrawInsertionLine(int X1, int X2, int Y)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.DrawLine(Pens.Red, X1, Y, X2 - 1, Y);

                Point[] leftTriangle = new Point[3] {
                            new Point(X1,      Y-4),
                            new Point(X1 + 7,  Y),
                            new Point(X1,      Y+4)
                        };
                Point[] rightTriangle = new Point[3] {
                            new Point(X2,     Y-4),
                            new Point(X2 - 8, Y),
                            new Point(X2,     Y+4)
                        };
                g.FillPolygon(Brushes.Red, leftTriangle);
                g.FillPolygon(Brushes.Red, rightTriangle);
            }
        }

        private void updateRowHeight()
        {
            //small image list hack
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(this.rowHeight, this.rowHeight);
            this.SmallImageList = imgList;
        }

        [System.ComponentModel.DefaultValue(true)]
        public bool AutoSizeRowHeight { get; set; }

        private int rowHeight;
        public int RowHeight
        {
            get
            {
                return this.rowHeight;
            }
            private set
            {
                //Remove value > this.rowHeight if you ever want to scale down the height on remove item
                if (value > this.rowHeight && this.AutoSizeRowHeight)
                {
                    this.rowHeight = value;
                    this.updateRowHeight();
                }
            }
        }

        // only allow details view
        [Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new View View
        {
            get
            {
                return base.View;
            }
            set
            {

            }
        }

    }
}
