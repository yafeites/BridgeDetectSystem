using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MAC加密
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string res =MacHelper.GetMacAddressByDos();
            string s = "";
            s += res;
            textBox1.Text = s;
            String model = "98:E7:F4:6D:10:B5";
            if (model.Equals(res))
            {
                textBox2.Text = "mac地址识别正确，可在本机运行此程序";
            }
        }
        

        /// <summary>  
        /// 通过DOS命令获得MAC地址  
        /// </summary>  
        /// <returns></returns>  
        public static string GetMacAddressByDos()
        {
            string macAddress = "";
            Process p = null;
            StreamReader reader = null;
            try
            {
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                start.FileName = "ipconfig";
                start.Arguments = "/all";

                start.CreateNoWindow = true;

                start.RedirectStandardOutput = true;

                start.RedirectStandardInput = true;

                start.UseShellExecute = false;

                p = Process.Start(start);

                reader = p.StandardOutput;

                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                    {
                        int index = line.IndexOf(":");
                        index += 2;
                        macAddress = line.Substring(index);
                        macAddress = macAddress.Replace('-', ':');
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
            catch
            {

            }
            finally
            {
                if (p != null)
                {
                    p.WaitForExit();
                    p.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return macAddress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                f();
            }
            catch (Exception ex)
            {
                textBox3.Text = ex.Message;
            }
        }
        public void f()
        {
            throw new Exception("自己抛出的异常");
        }
    }
}
