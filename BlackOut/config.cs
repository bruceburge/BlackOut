using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;

namespace BlackOut
{
    public partial class config : Form
    {
        private Form1 m_parent;

        public config(Form1 tmp)
        {
            InitializeComponent();
            m_parent = tmp;
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Text = "Opacity " + trackBar1.Value + "%";
            m_parent.setOpacity(trackBar1.Value);
            BlackOut.Properties.Settings.Default.Opacity = trackBar1.Value;
            BlackOut.Properties.Settings.Default.Save();


        }

        private void config_Load(object sender, EventArgs e)
        {
            this.Text = "Opacity " + trackBar1.Value + "%";
            trackBar1.SetRange(1, 100);

            
        }

        private void config_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
              m_parent.setColor(colorDialog1.Color);
              BlackOut.Properties.Settings.Default.Color = colorDialog1.Color;
              BlackOut.Properties.Settings.Default.Save();
            }
        }
    }
}