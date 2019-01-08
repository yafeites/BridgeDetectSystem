using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MAC加密
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string mac = MacHelper.GetMacAddressByDos();
            String model = "98:E7:F4:6D:10:B5";
          
            if (!model.Equals(mac))
            {
               // AutoClosingMessageBox.Show("正在识别电脑地址......请等待", 2500);
              //  MessageBox.Show("识别失败。程序未在指定的电脑上运行，请联系机械设计研究所管教授，电话13634165139","提示");
                MessageBox.Show("程序未在指定的电脑上运行，请联系张飞机。。。");
                System.Environment.Exit(0);
            }
            AutoClosingMessageBox.Show("正在识别电脑地址......请等待", 1000);
            AutoClosingMessageBox.Show("识别正确，下面请登录。", 1500);
            Application.Run(new Form1());
        }
    }
}
