using System;
using System.Windows.Forms;
using BridgeDetectSystem.video;
using BridgeDetectSystem.service;
using BridgeDetectSystem.adam;
using System.Threading;

namespace BridgeDetectSystem
{
    public partial class VideoMonitorWin : MetroFramework.Forms.MetroForm
    {

        VideoPlayer player = null;
        //AdamHelper2 adamHelper2 = null;
       // WarningManager2 warningManager2 = null;

        public VideoMonitorWin()
        {
            InitializeComponent();
///////
            //warningManager2 = WarningManager2.GetInstance();
          
        }

        private void VideoMonitor_Load(object sender, EventArgs e)
        {
            this.initial();
            comboBox1.SelectedIndex = 3;
            // adamHelper2.StartTimer(250);
            //warningManager2.BgStart();//开始后台报警
            #region 初始化视频监控

            string ip = "192.168.1.100";


            string userName = "admin";
            string password = "admin123456";

            VideoPlayer.initClass(ip, userName, password);
            player = VideoPlayer.GetInstance();
           
            try
            {
                player.Initial();
                player.Login();
            }
            catch (VideoPlayerException ex)
            {
                MessageBox.Show("登陆录像成功，请点击全部显示按钮查看实时视频监控。");
                
                //MessageBox.Show("视频预览初始化出错! " + ex.Message);
                return;
            }

            #endregion


            ShowPreview();
          

            

        }

        private void initial()
        {
            #region 窗体初始化
            this.panel1.Width = this.panelmax.Width / 2;
            this.panel2.Height = this.panel1.Height / 2;
            this.panel4.Width = this.panel2.Width / 2;
            this.panel6.Width = this.panel3.Width / 2;
            this.panel10.Height = 6 * this.panel8.Height / 10;
           
            
            #endregion
        }

        private void ShowPreview()
        {
            for (int i = 0; i < 4; i++)
            {
                Control[] ctr = this.panel1.Controls.Find("picBox" + (i + 1), true);
                if (ctr.Length > 0)
                {
                    PictureBox picbox = (PictureBox)ctr[0];
                    try
                    {
                        player.Preview(picbox, i);
                    }
                    catch (VideoPlayerException ex)
                    {
                        MessageBox.Show("第" + (i + 1) + "路摄像头出现问题：" + ex.Message);
                    }
                }
            }
            Control[] c = this.panel10.Controls.Find("picBox" + 5, true);
            if(c.Length>0)
            {
                PictureBox picbox1 = (PictureBox)c[0];
                try
                {
                    player.Preview(picbox1, 4);
                }
                catch (VideoPlayerException ex)
                {
                   MessageBox.Show("第" + 5 + "路摄像头出现问题：" + ex.Message);
                   
                }
            }

        }

        private void StopPreview()
        {
            for (int i = 0; i < 4; i++)
            {
                Control[] ctr = this.panel1.Controls.Find("picBox" + (i + 1), true);
                if (ctr.Length > 0)
                {
                    PictureBox picbox = (PictureBox)ctr[0];
                    try
                    {
                        player.StopPreview(picbox, i);
                    }
                    catch (VideoPlayerException ex)
                    {
                        MessageBox.Show("第" + (i + 1) + "路摄像头出现问题：" + ex.Message);
                    }
                }

            }
            Control[] c = this.panel10.Controls.Find("picBox" + 5, true);
            if (c.Length > 0)
            {
                PictureBox picbox1 = (PictureBox)c[0];
                try
                {
                    player.StopPreview(picbox1, 4);
                }
                catch (VideoPlayerException ex)
                {
                    MessageBox.Show("第" + 5 + "路摄像头出现问题：" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 关闭--退出视频，结束数据接收，报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VideoMonitorWin_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            player.CleanUp();

            //if (warningManager2.isStart)
            //{
            //    warningManager2.BgCancel();
            //}

            //adamHelper2.StopTimer();
        }

        private void RemoveAllPanelAndPictureBox()
        {
            foreach (Control ctr in Controls)
            {
                if ((ctr is Panel &&!ctr.Name.Equals("paneltop") || ctr is PictureBox))
                {
                    Controls.Remove(ctr);
                }
            }
        }

        private void AddPictureBox()
        {
            PictureBox picbox = new PictureBox();
            picbox.Name = "picBox1";
            picbox.Dock = DockStyle.Fill;
            picbox.Visible = true;
            Controls.Add(picbox);
            picbox.BringToFront();
        }

        private void ShowSinglePreview(int index)
        {
            PictureBox picbox = (PictureBox)Controls["picBox1"];
            try
            {
                player.Preview(picbox, index, 0);
            }
            catch (VideoPlayerException ex)
            {
                MessageBox.Show("第" + (index + 1) + "路摄像头出现问题：" + ex.Message);
            }
        }

        private void CreateSinglePreview(int index)
        {
            StopPreview();
            RemoveAllPanelAndPictureBox();
            AddPictureBox();
            ShowSinglePreview(index);
        }

        private void btnVideo1_Click(object sender, EventArgs e)
        {
            CreateSinglePreview(0);
        }

        private void btnVideo2_Click(object sender, EventArgs e)
        {
            CreateSinglePreview(1);
        }

        private void btnVideo3_Click(object sender, EventArgs e)
        {
            CreateSinglePreview(2);
        }

        private void btnVideo4_Click(object sender, EventArgs e)
        {
            CreateSinglePreview(3);
        }

        private void btnAllVideo_Click(object sender, EventArgs e)
        {
            //增加停掉。
            //if (warningManager2.isStart==true)
            //{
            //    warningManager2.BgCancel();
            //}
            //adamHelper2.StopTimer();
            this.Close();

            VideoMonitorWin win = new VideoMonitorWin();
            win.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //double y = Math.Round(adamHelper2.v, 1);
            //txtRailway.Text = y.ToString();

        }

        private void btnVideo5_Click(object sender, EventArgs e)
        {
            CreateSinglePreview(4);
        }

        // <summary>
        /// 旋转摄像头的手动模式的开启和关闭
        /// </summary>

        private void up_MouseUp(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "up", false, (uint)comboBox1.SelectedIndex + 1);
        }

        private void up_MouseDown(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "up", true, (uint)comboBox1.SelectedIndex + 1);
        }

        

        private void right_MouseDown(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "right", true, (uint)comboBox1.SelectedIndex + 1);
        }

        private void right_MouseUp(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "right", false, (uint)comboBox1.SelectedIndex + 1);
        }

        private void left_MouseDown(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "left", true, (uint)comboBox1.SelectedIndex + 1);
        }

        private void left_MouseUp(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "left",false, (uint)comboBox1.SelectedIndex + 1);
        }
            private void down_MouseDown(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "down", true, (uint)comboBox1.SelectedIndex + 1);
           
        }

        private void down_MouseUp(object sender, MouseEventArgs e)
        {
            player.ControlOpertion(4, "down", false, (uint)comboBox1.SelectedIndex + 1);
            
        }
        private void right_Click(object sender, EventArgs e)
        {

        }
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// 旋转摄像头的自动模式的开启和关闭
        /// </summary>
       
        private void auto_Click(object sender, EventArgs e)
        {
            if( auto.Text.Equals("AUTO"))
             { 
                player.ControlAuto(4, true, (uint)comboBox1.SelectedIndex + 1);
                auto.Text = "STOP";
            }
            else if(auto.Text.Equals("STOP"))
            {
                player.ControlAuto(4, false, (uint)comboBox1.SelectedIndex + 1);
                auto.Text = "AUTO";
            }
            

        }
    }

}
