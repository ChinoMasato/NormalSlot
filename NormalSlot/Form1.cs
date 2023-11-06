using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormalSlot
{
    public partial class Form1 : Form
    {
        bool reel1;//リールが動いているか止まっているか
        bool reel2;//リールが動いているか止まっているか
        bool reel3;//リールが動いているか止まっているか
        bool stop1;//リール１のストップボタンが押されているか
        bool stop2;//リール２のストップボタンが押されているか
        bool stop3;//リール３のストップボタンが押されているか
        int count1;
        int count2;
        int count3;

        public Form1()
        {
            InitializeComponent();
            reel1 = false;//リール停止状態
            reel2 = false;//リール停止状態
            reel3 = false;//リール停止状態
            timer1.Enabled = true;
            stop1 = false;//ストップボタン押されていない
            stop2 = false;//ストップボタン押されていない
            stop3 = false;//ストップボタン押されていない
            count1 = 0;
            count2 = 0;
            count3 = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //リールを止めるか否か
            if(stop1 && count1 % 4 == 0)
            {
                stop1 = false;
                reel1 = false;
            }
            if (stop2 && count2 % 4 == 0)
            {
                stop2 = false;
                reel2 = false;
            }
            if (stop3 && count3 % 4 == 0)
            {
                stop3 = false;
                reel3 = false;
            }


            if (reel1)
            {
                pictureBox1.Top+=16;
                count1++;
                if (pictureBox1.Top>-1197+64*21)
                {
                    pictureBox1.Top = -1197;
                    count1 = 0;
                }
            }
            if (reel2)
            {
                pictureBox2.Top+=16;
                count2++;
                if (pictureBox2.Top > -1197 + 64 * 21)
                {
                    pictureBox2.Top = -1197;
                    count2 = 0;
                }
            }
            if (reel3)
            {
                pictureBox3.Top+=16;
                count3++;
                if (pictureBox3.Top > -1197 + 64 * 21)
                {
                    pictureBox3.Top = -1197;
                    count3 = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reel1 = true;//リール回転状態
            reel2 = true;//リール回転状態
            reel3 = true;//リール回転状態
            stop1 = false;
            stop2 = false;
            stop3 = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop1 = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stop2 = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stop3 = true;
        }
    }
}
