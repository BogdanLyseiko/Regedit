using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            BackColor = Color.Red;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            BackColor = Color.Green;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            BackColor = Color.Blue;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey("NameKey");
                RadioButton r = null;
                if (radioButton1.Checked)
                    r = radioButton1;
                if (radioButton2.Checked)
                    r = radioButton2;
                if (radioButton3.Checked)
                    r = radioButton3;
                if (this.WindowState != FormWindowState.Minimized)
                {
                    key.SetValue("back", r.Text, RegistryValueKind.ExpandString);

                    key.SetValue("LX", Location.X, RegistryValueKind.DWord);
                    key.SetValue("LY", Location.Y, RegistryValueKind.DWord);
                    key.SetValue("top", Top, RegistryValueKind.DWord);
                    // key.SetValue("top", Top, RegistryValueKind.DWord);
                    key.SetValue("left", Left, RegistryValueKind.DWord);
                    key.SetValue("width", Width, RegistryValueKind.DWord);
                    key.SetValue("height", Height, RegistryValueKind.DWord);
                }




            }
            catch (Exception rr)
            {
                MessageBox.Show(rr.Message);
            }
            //Close();
        //    MessageBox.Show("sadf");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey k = Registry.CurrentUser.OpenSubKey("NameKey");
                string s = (string)k.GetValue("back");
                switch (s)
                {
                    case "Red":
                        radioButton1.Checked = true; break;
                    case "Green":
                        radioButton2.Checked = true; break;
                    case "Blue":
                        radioButton3.Checked = true; break;
                        break;
                }

                Location = new Point((int)k.GetValue("LX"), (int)k.GetValue("LY"));//  (int)k.GetValue("LX");
                Top = (int)k.GetValue("top");
                Left = (int)k.GetValue("left");
                Width = (int)k.GetValue("width");
                Height = (int)k.GetValue("height");
                //    Close();
            }
            catch (Exception)
            {

              
            }
          

        }
    }
}
