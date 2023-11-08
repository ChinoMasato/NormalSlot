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
            public PictureBox pic;
            //引数に入れたラインの絵柄を返す
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
        int[] prize = { 3, 5, 5, 7, 30, 50,100 };
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
            }
            reel[0].pic = pictureBox1;
            reel[1].pic = pictureBox2;
            reel[2].pic = pictureBox3;
            credit = 100;//100枚セット
            label1.Text = credit.ToString();
        }
        private bool isAllStop()
        {
            for (int i = 0; i < reel.Length; i++) {
                if (reel[i].active)
                {
                    return false;
                }
            }
            return true;
        }
        private void pay(int num)
        {
            credit += prize[num];
            label1.Text = credit.ToString();
        }
        private void WinCheck()
        {
            if (!isAllStop()) return;
            //横３列
            for (int i = 0; i < 3; i++)
            {
                if (reel[0].ReelGraphNo(i) == reel[1].ReelGraphNo(i) &&
                   reel[1].ReelGraphNo(i) == reel[2].ReelGraphNo(i))
                {
                    pay(reel[0].ReelGraphNo(i));
                }
            }
            //斜め
            if(reel[0].ReelGraphNo(2) == reel[1].ReelGraphNo(1) && reel[1].ReelGraphNo(1) == reel[2].ReelGraphNo(0))
            {
                pay(reel[0].ReelGraphNo(2));
            }
            if (reel[2].ReelGraphNo(0) == reel[1].ReelGraphNo(1) && reel[1].ReelGraphNo(1) == reel[0].ReelGraphNo(2))
            {
                pay(reel[2].ReelGraphNo(0));
            }
            //デバッグ表示
            label2.Text = reel[0].ReelGraphNo(2).ToString() + reel[1].ReelGraphNo(2).ToString() + reel[2].ReelGraphNo(2).ToString() + "\n" +
            reel[0].ReelGraphNo(1).ToString() + reel[1].ReelGraphNo(1).ToString() + reel[2].ReelGraphNo(1).ToString() + "\n" +
            reel[0].ReelGraphNo(0).ToString() + reel[1].ReelGraphNo(0).ToString() + reel[2].ReelGraphNo(0).ToString();

        }
        //リール更新メソッド（関数）
        private void reelUpdate(ref Reel reel)
        {
            //リールを止めるか否か
            if (reel.stop && reel.count % 4 == 0)
            {
                reel.stop = false;
                reel.active = false;
                WinCheck();//配当チェック
            }
            else
            {
                reel.pic.Top += 16;
                reel.count++;
                if (reel.pic.Top > -1197 + 64 * 21)
                {
                    reel.pic.Top = -1197;
                    reel.count = 0;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i=0;i<reel.Length;i++)
            {
                if (reel[i].active)
                {
                    reelUpdate(ref reel[i]);
                }
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
