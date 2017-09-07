using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class WatermarkTextBox : TextBox
    {
        private string watermark;
        private Color watermarkColor;
        private Color foreColor;
        private bool empty;
        [Browsable(true)]
        public Color WatermarkColor
        {
            get { return watermarkColor; }
            set
            {
                watermarkColor = value;
                if (empty)
                {
                    base.ForeColor = watermarkColor;
                }
            }
        }
        [Browsable(true)]
        public string Watermark
        {
            get { return watermark; }
            set
            {
                watermark = value;
                if (empty)
                {
                    base.Text = watermark;
                    base.ForeColor = watermarkColor;
                }
            }
        }
        public WatermarkTextBox()
        {
            empty = true;
            foreColor = ForeColor;
        }
        [Browsable(true)]
        public new Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                if (!empty)
                    base.ForeColor = value;
            }
        }
        public override string Text
        {
            get
            {
                if (empty)
                    return "";
                return base.Text;
            }
            set
            {
                if (value == "")
                {
                    empty = true;
                    base.ForeColor = watermarkColor;
                    base.Text = watermark;
                }
                else
                    base.Text = value;
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            if (empty)
            {
                empty = false;
                base.ForeColor = foreColor;
                base.Text = "";
            }
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (base.Text == "")
            {
                empty = true;
                base.ForeColor = watermarkColor;
                base.Text = watermark;
            }
            else
                empty = false;
        }
    }
}
