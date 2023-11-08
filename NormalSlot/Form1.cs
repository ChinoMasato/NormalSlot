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
        class Reel
        {
            public bool active;//動いているか
            public bool stop;//ストップボタンが押されている
            public int count;//カウンター
            public int[] graph;//絵柄
            public int ReelGraphNo(int line)
            {
                if(graph.Length==0)
                {
                    return -1;
                }
                return graph[(int)(count / 4)+line];
            }

        }
        Reel[] reel = new Reel[3];
        int credit;//コイン

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;

            for (int i = 0; i < reel.Length; i++)
            {
                reel[i] = new Reel();
                reel[i].active = false;
                reel[i].stop = false;
                reel[i].count = 0;
                reel[i].graph = new int[24] { 3,6,1,2,3,2,5,0,2,3,2,4,6,2,3,2,0,5,2,3,2,3,6,1};
                /*
                 { 1, 6, 3, 2, 3, 2, 5, 0, 2, 3, 2, 6, 4, 2, 3, 2, 0, 5, 2, 3, 2, 1, 6, 3 };
                    reel[i].graph[0] = 1;
                    reel[i].graph[1] = 6;
                    reel[i].graph[2] = 3;
                    reel[i].graph[3] = 2;
                    reel[i].graph[4] = 3;
                    reel[i].graph[5] = 2;
                    reel[i].graph[6] = 5;
                    reel[i].graph[7] = 0;
                    reel[i].graph[8] = 2;
                    reel[i].graph[9] = 3;
                    reel[i].graph[10] = 2;
                    reel[i].graph[11] = 6;
                    reel[i].graph[12] = 4;
                    reel[i].graph[13] = 2;
                    reel[i].graph[14] = 3;
                    reel[i].graph[15] = 2;
                    reel[i].graph[16] = 0;
                    reel[i].graph[17] = 5;
                    reel[i].graph[18] = 2;
                    reel[i].graph[19] = 3;
                    reel[i].graph[20] = 2;
                    reel[i].graph[21] = 1;
                    reel[i].graph[22] = 6;
                    reel[i].graph[23] = 3;
                */
            }
            credit = 100;//100枚セット
            label1.Text = credit.ToString();
        }
        private bool isAllStop()
        {
            for (int i = 0; i < reel.Length; i++) {
                if (!reel[i].active)
                {
                    return false;
                }
            }
            return true;
        }
        private int isWinNum()
        {
            int num = 0;
            if (!isAllStop()) return 0;
            for (int i = 0; i < 3; i++)
            {
                if (reel[0].ReelGraphNo(i) == reel[1].ReelGraphNo(i) &&
                   reel[1].ReelGraphNo(i) == reel[2].ReelGraphNo(i))
                {
                    num++;
                }
            }

            return num;

        }
        //リール更新メソッド（関数）
        private void reelUpdate(ref Reel reel,ref PictureBox pic)
        {
            //リールを止めるか否か
            if (reel.stop && reel.count % 4 == 0)
            {
                reel.stop = false;
                reel.active = false;

                credit += 10 * isWinNum();
                label1.Text = credit.ToString();
                
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
            credit--;
            label1.Text = credit.ToString();
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
