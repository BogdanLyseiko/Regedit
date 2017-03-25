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
    public partial class EditValue : Form
    {
        RegistryKey key = null;
        
        public EditValue(string param, RegistryKey k)
        {
            InitializeComponent();
            key = k;
            textBox1.ReadOnly = true;
            textBox1.Text = param;
            textBox2.Text = k.GetValue(param).ToString();
        }

        private void EditValue_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                key.SetValue(textBox1.Text, textBox2.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
