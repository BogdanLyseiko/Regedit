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

namespace WindowsFormsApplication2
{
    public partial class AddPartial : Form
    {
        public RegistryKey k = null;
        public string pathWithoutRoot = ""; 

        public AddPartial(RegistryKey k,string PathWithoutRoot)
        {
            
            InitializeComponent();
            pathWithoutRoot = PathWithoutRoot;
            this.k = k;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
         //   MessageBox.Show(k.Name + "\\" + textBox1.Text);
            /////////////   k.CreateSubKey(k.Name+ "\\"+textBox1.Text);
            try
            {
                if (pathWithoutRoot != "")
                    pathWithoutRoot += "\\";

                k.CreateSubKey(pathWithoutRoot + textBox1.Text);
                //k =  k.CreateSubKey(textBox1.Text);
                pathWithoutRoot = textBox1.Text;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            //}
            //catch( Exception ee)
            //{
            //    MessageBox.Show(ee.Message);
            //}
        }
    }
}
