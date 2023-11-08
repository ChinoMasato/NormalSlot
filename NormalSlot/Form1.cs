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
        struct Reel
        {
            public bool active;//動いているか
            public bool stop;//ストップボタンが押されている
            public int count;//カウンター
        }
        Reel[] reel = new Reel[3];


        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;

            for(int i=0; i<reel.Length; i++)
            {
                reel[i].active = false;
                reel[i].stop = false;
                reel[i].count = 0;
            }
        }
        //リール更新メソッド（関数）
        private void reelUpdate(ref Reel reel,ref PictureBox pic)
        {
            //リールを止めるか否か
            if (reel.stop && reel.count % 4 == 0)
            {
                reel.stop = false;
                reel.active = false;
            }
            else
            {
                pic.Top += 16;
                reel.count++;
                if (pic.Top > -1197 + 64 * 21)
                {
                    pic.Top = -1197;
                    reel.count = 0;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (reel[0].active)
            {
                reelUpdate(ref reel[0],ref pictureBox1);
            }
            if (reel[1].active)
            {
                reelUpdate(ref reel[1], ref pictureBox2);
            }
            if (reel[2].active)
            {
                reelUpdate(ref reel[2], ref pictureBox3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<reel.Length;i++)
            {
                reel[i].active = true;//リール回転状態
                reel[i].stop = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reel[0].stop = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reel[1].stop = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reel[2].stop = true;
        }
    }
}
