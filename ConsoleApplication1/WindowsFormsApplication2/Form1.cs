using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
//ріхтер, доктор веб
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {

        class myClass
        {
            public RegistryKey key { get; set; }
            public string Name { get; set; }

        }

        public Form1()
        {
            InitializeComponent();
        }

        void ThreadFunc(object o)
        {
            try
            {
                myClass m = o as myClass;
                TreeNode t = KeyToNode(m.key, m.Name);
                treeView1.BeginInvoke(new Action(() =>
                {
                    treeView1.Nodes.Add(t);
                }));
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            //  MessageBox.Show("All");
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            //     treeView1.Nodes.Add("1");
            //treeView1.Nodes.Add("HKEY_CURRENT_USER");
            //treeView1.Nodes.Add("HKEY_LOCAL_MACHINE");
            //treeView1.Nodes.Add("HKEY_USERS");
            //treeView1.Nodes.Add("HKEY_CURRENT_CONFIG");
            ////treeView1.Nodes.Add(KeyToNode(Registry.CurrentUser, "HKEY_CURRENT_USER"));
            ////treeView1.Nodes.Add(KeyToNode(Registry.LocalMachine, "HKEY_LOCAL_MACHINE"));
            ////treeView1.Nodes.Add(KeyToNode(Registry.Users, "HKEY_USERS"));
            ////treeView1.Nodes.Add(KeyToNode(Registry.CurrentConfig, "HKEY_CURRENT_CONFIG"));

            //treeView1.Nodes.Add(KeyToNode(Registry.CurrentUser, "HKEY_CURRENT_USER"));
            ////////////////////ThreadPool.QueueUserWorkItem(ThreadFunc, new myClass { key = Registry.CurrentUser, Name = "HKEY_CURRENT_USER" });


            ////////////////////ThreadPool.QueueUserWorkItem(ThreadFunc, new myClass { key = Registry.LocalMachine, Name = "HKEY_LOCAL_MACHINE" });


            ////////////////////ThreadPool.QueueUserWorkItem(ThreadFunc, new myClass { key = Registry.Users, Name = "HKEY_USERS" });


            ////////////////////ThreadPool.QueueUserWorkItem(ThreadFunc, new myClass { key = Registry.CurrentConfig, Name = "HKEY_CURRENT_CONFIG" });
            Thread t1 = new Thread(ThreadFunc);
            Thread t2 = new Thread(ThreadFunc);
            Thread t3 = new Thread(ThreadFunc);
            Thread t4 = new Thread(ThreadFunc);
            Thread t5 = new Thread(ThreadFunc);
            t1.IsBackground = true;
            t2.IsBackground = true;
            t3.IsBackground = true;
            t4.IsBackground = true;
            t5.IsBackground = true;


            t1.Start(new myClass { key = Registry.CurrentUser, Name = "HKEY_CURRENT_USER" });
            t2.Start(new myClass { key = Registry.LocalMachine, Name = "HKEY_LOCAL_MACHINE" });
            t3.Start(new myClass { key = Registry.Users, Name = "HKEY_USERS" });
            t4.Start(new myClass { key = Registry.CurrentConfig, Name = "HKEY_CURRENT_CONFIG" });
            t5.Start(new myClass { key = Registry.ClassesRoot, Name = "HKEY_CLASSES_ROOT" });
          //  label1.Visible = false;
        }

        private TreeNode KeyToNode(RegistryKey key, string shortName)
        {
            var node = new TreeNode(shortName, 0, 1);
            node.Tag = key;

            if (key.SubKeyCount > 0)
            {
                foreach (var subkey in key.GetSubKeyNames())
                {
                    try
                    {
                        node.Nodes.Add(GetNode(key.OpenSubKey(subkey), subkey));
                    }
                    catch (System.Security.SecurityException exc)
                    {
                        var restrictedNode = new TreeNode(subkey, 3, 3);
                        restrictedNode.ToolTipText = exc.Message;
                        node.Nodes.Add(restrictedNode);
                    }
                }
            }
            //node.Nodes.Add(string.Empty);
            return node;
        }

        private TreeNode GetNode(RegistryKey key, string shortname)
        {
            var node = new TreeNode(shortname);
            node.Tag = key;

            try
            {
                foreach (var subkey in key.GetSubKeyNames())
                {
                    try
                    {
                        node.Nodes.Add(GetNode(key.OpenSubKey(subkey), subkey));
                    }
                    catch (System.Security.SecurityException exc)
                    {
                        var restrictedNode = new TreeNode(subkey, 3, 3);
                        restrictedNode.ToolTipText = exc.Message;
                        node.Nodes.Add(restrictedNode);
                    }
                }
            }
            catch (Exception) { }
            return node;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
          //  MessageBox.Show(treeView1.ShowRootLines.ToString());
            try
            {
                listView1.Items.Clear();
                RegistryKey key = null;

                var r = treeView1.SelectedNode.FullPath.Split('\\');
                if (r.Length == 1)
                {
                    switch (treeView1.SelectedNode.FullPath)
                    {
                        case "HKEY_CURRENT_USER":
                            key = Registry.CurrentUser;
                            break;
                        case "HKEY_LOCAL_MACHINE":
                            key = Registry.LocalMachine;
                            break;
                        case "HKEY_USERS":
                            key = Registry.Users;
                            break;
                        case "HKEY_CURRENT_CONFIG":
                            key = Registry.CurrentConfig;
                            break;
                        case "HKEY_CLASSES_ROOT":
                            key = Registry.ClassesRoot;

                            break;
                    }
                    foreach (var item in key.GetValueNames())
                    {
                        //MessageBox.Show(item);

                        listView1.Items.Add(item);
                    }
                    return;
                }
                   

              //  MessageBox.Show("sdfsdf");
                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                key = key.OpenSubKey(treeView1.SelectedNode.FullPath.Replace(r.First() + "\\", string.Empty));
                foreach (var item in key.GetValueNames())
                {
                    //MessageBox.Show(item);
                    
                    listView1.Items.Add(item);
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void createPartialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                RegistryKey key = null;
                var r = treeView1.SelectedNode.FullPath.Split('\\');

                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                string path = "";
                if (r.Length > 1)
                    path = treeView1.SelectedNode.FullPath;
                AddPartial p2 = new AddPartial(key, path.Replace(key.Name + "\\", string.Empty));
                if (p2.ShowDialog() == DialogResult.OK)
                {
                    treeView1.SelectedNode.Nodes.Add(p2.pathWithoutRoot);

                }
                return;
            }
            else
                MessageBox.Show("Select partial please!");
        }

        private void createStringParamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                RegistryKey key = null;
                var r = treeView1.SelectedNode.FullPath.Split('\\');

                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                string path = "";
                if (r.Length > 1)
                    path = treeView1.SelectedNode.FullPath;
                path = path.Replace(key.Name + "\\", string.Empty);
                MessageBox.Show(path);
              key =   key.OpenSubKey(path,true);
                AddFile a = new AddFile(key, RegistryValueKind.String);
                if (a.ShowDialog() == DialogResult.OK)
                {
                    treeView1_AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
                }
            }
            else
                MessageBox.Show("Select partial please!");
        }

        private void createBinaryParamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                RegistryKey key = null;
                var r = treeView1.SelectedNode.FullPath.Split('\\');

                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                string path = "";
                if (r.Length > 1)
                    path = treeView1.SelectedNode.FullPath;
                path = path.Replace(key.Name + "\\", string.Empty);
                MessageBox.Show(path);
                key = key.OpenSubKey(path, true);
                AddFile a = new AddFile(key, RegistryValueKind.DWord);
                if (a.ShowDialog() == DialogResult.OK)
                {
                    treeView1_AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
                }
            }
            else
                MessageBox.Show("Select partial please!");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var r2 = listView1.SelectedItems[0];

                RegistryKey key = null;
                var r = treeView1.SelectedNode.FullPath.Split('\\');

                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                string path = "";
                if (r.Length > 1)
                    path = treeView1.SelectedNode.FullPath;
                path = path.Replace(key.Name + "\\", string.Empty);
               // MessageBox.Show(path);
                key = key.OpenSubKey(path, true);
                try
                {
                    key.DeleteValue(r2.Text);
                    treeView1_AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
                MessageBox.Show("Select file!");
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var r2 = listView1.SelectedItems[0];

                RegistryKey key = null;
                var r = treeView1.SelectedNode.FullPath.Split('\\');

                switch (r.First())
                {
                    case "HKEY_CURRENT_USER":
                        key = Registry.CurrentUser;
                        break;
                    case "HKEY_LOCAL_MACHINE":
                        key = Registry.LocalMachine;
                        break;
                    case "HKEY_USERS":
                        key = Registry.Users;
                        break;
                    case "HKEY_CURRENT_CONFIG":
                        key = Registry.CurrentConfig;
                        break;
                    case "HKEY_CLASSES_ROOT":
                        key = Registry.ClassesRoot;

                        break;
                }
                string path = "";
                if (r.Length > 1)
                    path = treeView1.SelectedNode.FullPath;
                path = path.Replace(key.Name + "\\", string.Empty);
                // MessageBox.Show(path);
                key = key.OpenSubKey(path, true);
                EditValue ee = new EditValue(listView1.SelectedItems[0].Text, key);
                if(ee.ShowDialog()==DialogResult.OK)
                {

                }
                //try
                //{
                //    key.DeleteValue(r2.Text);
                //    treeView1_AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
                //}
                //catch (Exception ee)
                //{
                //    MessageBox.Show(ee.Message);
                //}
            }
            else
                MessageBox.Show("Select file!");

        }
            //switch (r.First())
            //{
            //    case "HKEY_CURRENT_USER":
            //        key = Registry.CurrentUser;
            //        break;
            //    case "HKEY_LOCAL_MACHINE":
            //        key = Registry.LocalMachine;
            //        break;
            //    case "HKEY_USERS":
            //        key = Registry.Users;
            //        break;
            //    case "HKEY_CURRENT_CONFIG":
            //        key = Registry.CurrentConfig;
            //        break;
            //    case "HKEY_CLASSES_ROOT":
            //        key = Registry.ClassesRoot;

            //        break;
            //}
            //key = key.OpenSubKey(treeView1.SelectedNode.FullPath.Replace(r.First() + "\\", string.Empty));
            //AddPartial p = new AddPartial(key);
            //if (p.ShowDialog() == DialogResult.OK)
            //{
            //    treeView1.SelectedNode.Nodes.Add(p.k.Name.Split('\\').Last());
            //}
        //}
    }
}
