using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mühle.Steon.Game
{
    public partial class Form1 : Form
    {
        
        // Liste mit den feldern und in der rehein folge von dem Bild


        public Form1()
        {
            InitializeComponent();
        }
        Bitmap Whitestone = new Bitmap("white.stone.png");
        Bitmap Blackstone = new Bitmap("black.stone.png");
        List<Board> Board_ = new List<Board>();
        List<Mühle> Mühle_ = new List<Mühle>();
        bool Turn = true;
        int last_clicked;
        int clicks_alltime = 0;

        int clicks = 0;
      




        //0= leer 1==schwarz 2=weiß

        //check if move is possible
        public int whitestonesleft()
        {
            if (clicks_alltime < 19)
            {
                int clicksleft = 9 -clicks_alltime / 2;
                return clicksleft;

            }
            else
            {
                return 0;

            }

            

        }
        public int blackstonesleft()
        {
            if (clicks_alltime < 19)
            {
                int clicksleft = 9 - (clicks_alltime+1) / 2;
                return clicksleft;

            }
            else
            {
                return 0;

            }

        }
        public bool checkifpossible(int Pointnow, int Pointto)
        {
            if (Board_[Pointto].status == 1 || Board_[Pointto].status == 2)
            {
                return false;

            }
            if (Board_[Pointto].status == 1 && Turn)
            {
                
                if (Pointnow + 1 == Pointto || Pointnow - 1 == Pointto)
                {
                    if ((Pointnow < 8 && Pointto < 8) || (Pointnow < 16 && Pointto < 16 && Pointnow > 7 && Pointto > 7) || (Pointnow < 24 && Pointto < 24 && Pointnow > 15 && Pointto > 15))
                    {
                        return true;
                    }


                }
                int linepointto = Pointto / 8;
                if ((Pointto % 2 == 1 && Pointnow % 2 == 1) && (Board_[Pointto].posline + 1 == linepointto || Board_[Pointto].posline - 1 == linepointto))
                {
                    //gerade
                }


            }
            if (Board_[Pointto].status == 2 && Turn==false)
            {
                
                if (Pointnow + 1 == Pointto || Pointnow - 1 == Pointto)
                {
                    if ((Pointnow < 8 && Pointto < 8) || (Pointnow < 16 && Pointto < 16 && Pointnow > 7 && Pointto > 7) || (Pointnow < 24 && Pointto < 24 && Pointnow > 15 && Pointto > 15))
                    {
                        return true;
                    }


                }
                int linepointto = Pointto / 8;
                if ((Pointto % 2 == 1 && Pointnow % 2 == 1) && (Board_[Pointto].posline + 1 == linepointto || Board_[Pointto].posline - 1 == linepointto))
                {
                    //gerade
                }


            }

            
            return false;


        }
        //dass mit den zügen muss überartbeitet werden also bei possibel dass nur schwarz schzarz bewegen kann und  nich beides
        public void changeturn()
        {
            if (Turn)
            {
                Turn = false;
            }
            if (Turn== false)
            {
                Turn = true;
            }
        }
        public void onButton(int posnow)
        {
            if (clicks_alltime < 18)
            {
                if (Turn==true)
                {
                    Board_[posnow].status = 1;
                }
                if (Turn== false)
                {
                    Board_[posnow].status = 2;
                }
               

                

            }
            else
            {
                clicks++;
                if (clicks == 1)
                {
                    last_clicked = posnow;
                }
                else if (clicks == 2)
                {
                    if (checkifpossible(last_clicked, posnow))
                    {
                        if (Board_[last_clicked].status == 1)
                        {
                            Board_[posnow].status = 1;
                        }
                        if (Board_[last_clicked].status == 2)
                        {
                            Board_[last_clicked].status = 0;
                            Board_[posnow].status = 1;


                        }

                        



                        

                    }
                    else
                    {
                        MessageBox.Show("Take a  other Field:");
                    }


                }
                if ((checkmühleblack() || checkmühlewhite()) && Mühlechanged())
                {
                    MessageBox.Show("You've got a Mühle");
                    if (clicks == 3)
                    {
                        Board_[posnow].status = 0;
                        clicks = 0;

                    }

                }
                else
                {

                    clicks = 0;
                    //turn variable mus nach jedem zug gaändert werden
                }


            }
            label4.Text = blackstonesleft().ToString();
            label5.Text = whitestonesleft().ToString();
            label3.Text = stonesingameblack().ToString();
            label6.Text = stonesingametwhite().ToString();
            clicks_alltime++;
            gui();
            

            




        }
        public int stonesingametwhite()
        {
            int count = 0;
            foreach(Board l in Board_)
            {
                if (l.status == 2)
                {
                    count++;
                }
                
            }
            return count;

        }
        public int stonesingameblack()
        {
            int count = 0;
            foreach (Board l in Board_)
            {
                if (l.status == 1)
                {
                    count++;
                }

            }
            return count;

        }
        public void gui()
        {
            int count = 0;
            foreach (Button btn in panel1.Controls)
            {
                if (Board_[count].status == 2)
                {
                    btn.BackColor = Color.LightGray;
                    
                }
                else if (Board_[count].status == 1)
                {
                    btn.BackColor = Color.Black;

                }
                count++;

            }
                  
        }
        public bool Mühlechanged()
        {
            return true;

        }






        public bool checkmühleblack()
        {
            
            for (int line = 0; line < 12; line++)
            {
                
                try
                {
                    if (Board_[line*2].status == 1&& Board_[line * 2+1].status == 1&& Board_[line * 2+2].status == 1)
                    {
                        return true;
                    }


                }
                catch
                {

                }
                
                

            }
            if (Board_[1].status == 1 && Board_[9].status == 1 && Board_[17].status == 1)
            {

                return true;

            }
            if (Board_[1 + 2].status == 1 && Board_[9 + 2].status == 1 && Board_[17 + 2].status == 1)
            {

                return true;

            }
            if (Board_[1 + 4].status == 1 && Board_[9 + 4].status == 1 && Board_[17 + 4].status == 1)
            {

                return true;

            }
            if (Board_[1 + 6].status == 1 && Board_[9 + 6].status == 1 && Board_[17 + 6].status == 1)
            {

                return true;

            }
            return false;
           
            

        }
        
        public bool checkmühlewhite()
        {

            for (int line = 0; line < 12; line++)
            {

                try
                {
                    if (Board_[line * 2].status == 2 && Board_[line * 2 + 1].status == 2 && Board_[line * 2 + 2].status == 2)
                    {
                        
                    }
                    


                }
                catch
                {

                }



            }
            if (Board_[1].status == 2 && Board_[9].status == 2 && Board_[17].status == 2)
            {

                return true;

            }
            if (Board_[1+2].status == 2 && Board_[9+2].status == 2 && Board_[17+2].status == 2)
            {

                return true;

            }
            if (Board_[1+4].status == 2 && Board_[9+4].status == 2 && Board_[17+4].status == 2)
            {

                return true;

            }
            if (Board_[1+6].status == 2 && Board_[9+6].status == 2 && Board_[17+6].status == 2)
            {

                return true;

            }
            return false;




        }


        

        private void Form1_Load(object sender, EventArgs e)
        {
           
            for (int i = 0; i < 24; i++)
            {
                Board p = new Board();
                p.status = 0;
                p.posline = i/8;
                p.Mühle = false;
               


                Board_.Add(p);








            }
            

        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            
            button3.BackColor = Color.Black;
            

           
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;

            //onButton(2);
            

        }
        private void button4_Click(object sender, EventArgs e)
        {
            onButton(3);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            onButton(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            onButton(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            onButton(6);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            onButton(7);

        }

       

        private void button9_Click(object sender, EventArgs e)
        {
            onButton(8);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            onButton(9);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            onButton(10);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            onButton(11);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            onButton(12);

        }

        private void button14_Click(object sender, EventArgs e)
        {
            onButton(13);

        }

        private void button15_Click(object sender, EventArgs e)
        {
            onButton(14);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            onButton(15);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            onButton(16);

        }

        private void button18_Click(object sender, EventArgs e)
        {
            onButton(17);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            onButton(18);

        }

        private void button20_Click(object sender, EventArgs e)
        {
            onButton(19);

        }

        private void button21_Click(object sender, EventArgs e)
        {
            onButton(20);

        }

        private void button22_Click(object sender, EventArgs e)
        {
            onButton(21);

        }

        private void button23_Click(object sender, EventArgs e)
        {
            onButton(22);

        }

        private void button24_Click(object sender, EventArgs e)
        {
            onButton(23);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            onButton(0);


        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
