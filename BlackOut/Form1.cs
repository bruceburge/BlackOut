using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BlackOut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        config configfrm;
        string frmColorName = "Black";
        int currentWindowEX;

        private void Form1_Load(object sender, EventArgs e)
        {            
            int tmpwidth = 0;
            int tmpheight = 0;
            int tmpLocationX = 0;
            int tmpLocationY = 0;
            
            /*
             * move through all the screens setting x and y to the lowest settings
             * then increase the form size to cover all screens.
             * Do NOT use WindowState or it will revert to one screen.
             */ 
            foreach (Screen tmpscr in Screen.AllScreens)
            {
                if (this.Location.X > tmpscr.WorkingArea.Location.X)
                {
                    tmpLocationX = tmpscr.WorkingArea.Location.X;
                }
                if (this.Location.Y > tmpscr.WorkingArea.Location.Y)
                {
                    tmpLocationY = tmpscr.WorkingArea.Location.Y;
                }
                tmpwidth += tmpscr.WorkingArea.Width;
                tmpheight += tmpscr.WorkingArea.Height;
            }
            
            this.Width = tmpwidth;
            this.Height = tmpheight;
            this.Location = new Point(tmpLocationX, tmpLocationY);
            //set original click through state
            currentWindowEX = ClickThrough.GetWindowLong(Handle, ClickThrough.ExStyle);
            configfrm = new config(this);
            //load settings
            this.Opacity = (double)BlackOut.Properties.Settings.Default.Opacity / 100;
            this.BackColor = (Color)BlackOut.Properties.Settings.Default.Color;
            //set color name
            if (this.BackColor.IsNamedColor == true)
            {
                frmColorName = this.BackColor.Name;
            }
            else
            {
                frmColorName = "Colored";
            }
            bToolStripMenuItem.Text = frmColorName+" Out " + this.Opacity * 100 + "%";
            //set form to be click through
            ClickThrough.SetWindowLong(this.Handle, ClickThrough.ExStyle, currentWindowEX | ClickThrough.WS_EX_Layered | ClickThrough.WS_EX_Transparent);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bToolStripMenuItem.Checked == true)
            {
                this.Hide();
                bToolStripMenuItem.Checked = false;
            }
            else
            {
                
                this.Show();
                //configfrm.Hide();
                bToolStripMenuItem.Checked = true;
                ClickThrough.SetWindowLong(this.Handle, ClickThrough.ExStyle, currentWindowEX | ClickThrough.WS_EX_Layered | ClickThrough.WS_EX_Transparent);
                /* 
                 * Handle through managed code
                 * ClickThrough.SetLayeredWindowAttributes(this.Handle, 0, 255*this.Opacity, ClickThrough.LWA_Alpha);
                 */ 
            }
        }


        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskBar tmp = new taskBar();
            configfrm.Location = tmp.touchSysTray(configfrm);
            configfrm.Show();
        }

        public void setOpacity(int Opacity)
        {           
            this.Opacity = (double)Opacity/100;
            bToolStripMenuItem.Text = frmColorName+" Out " + this.Opacity * 100+ "%";
        }

        /*setting color to anything other than black, typically looks horrible*/
        public void setColor(Color Color)
        {
            this.BackColor = Color;

            if (this.BackColor.IsNamedColor == true)
            {
                frmColorName = this.BackColor.Name;
            }
            else
            {
                frmColorName = "Colored";
            }

            bToolStripMenuItem.Text = frmColorName + " Out " + this.Opacity * 100 + "%";

        }
    }
}