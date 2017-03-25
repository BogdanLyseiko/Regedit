using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryKey key =  Registry.CurrentUser.CreateSubKey("NameKey");
            key.SetValue("x",6,RegistryValueKind.DWord);

            RegistryKey k = Registry.CurrentUser.OpenSubKey("NameKey");
            int x = (int)k.GetValue("x");
            Console.WriteLine(x);
            Console.ReadLine();
        }
    }
}
