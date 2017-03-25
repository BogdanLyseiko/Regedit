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
    public partial class AddFile : Form
    {
        RegistryKey key=null;
        RegistryValueKind kind;
       
        public AddFile(RegistryKey key, RegistryValueKind r)
        {
            InitializeComponent();
            this.key = key;
            kind = r;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // key = Registry.CurrentUser.OpenSubKey("11",true);
            key.SetValue(textBox1.Text, textBox2.Text,kind);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
