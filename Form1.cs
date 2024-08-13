using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace pvz
{
    public class CAdvImag
    {
        public int x, y;
        public int width, height;
        public Bitmap img;
        public Rectangle rectD;
        public Rectangle rectS;
        public int row;
        public int column;
        public int landoccupied;
    }
    public class CActor
    {
        public int x, y;
        public int w, h;
        public Color cl;
    }
    public class CImActor
    {
        public int X, Y, W, H;
        //public Bitmap img;
        public List<Bitmap> img;

        public int fpea = 0;
        public int fdir = 0;
        public int iFrame;
        public int price;
        public int fdyingzombie;
        public int ismove;
        public int peaoriginalx;
        public int fshoot = 0;
        public int tick = 1;
        public int tick2= 1;
        public int tickdeath = 1;
        public int fbright = 0;
        public int index;
        public int RandomDestination;
        public int freached;
        public int feat;
        public int whichanimation;
        public int deadhero;
        public int wallnutf = 0;
        public int ctwnut = 0;
        public int drag = 0;
        public int speed = 23;
        public int ftick = 0;
        public int fup = 0;
        public int fdown = 0;
    }
    public partial class Form1 : Form
    {
        Bitmap off;
        Timer tt = new Timer();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.KeyDown += Form1_Keydown;
            tt.Interval = 10;
            tt.Start();
            tt.Tick += Tt_Tick;
        }
        List<CAdvImag> land = new List<CAdvImag>();
        List<CAdvImag> lmap = new List<CAdvImag>();
        List<CImActor> lpeashooter = new List<CImActor>();
        List<CImActor> lwallnut = new List<CImActor>();
        List<CImActor> lpotatomine = new List<CImActor>();
        List<CImActor> lsunflower = new List<CImActor>();
        List<CImActor> lpea = new List<CImActor>();
        List<CImActor> lzombie = new List<CImActor>();
        List<CImActor> lshop = new List<CImActor>();
        List<CImActor> lmenu = new List<CImActor>();
        List<CImActor> shoplist = new List<CImActor>();
        List<CImActor> imglist = new List<CImActor>();
        List<CImActor> lheros = new List<CImActor>();
        List<CActor> lsuncounter = new List<CActor>();
        List<CActor> lhealthbar = new List<CActor>();
        List<CImActor> lsuns = new List<CImActor>();
        List<CImActor> lsun2 = new List<CImActor>();
        List<CImActor> lsnowpeashooter = new List<CImActor>();
        List<CImActor> lsnowpea = new List<CImActor>();
        List<CImActor> lawnmover = new List<CImActor>(); 
        int ctTick = 0;
        int xOld = -1;
        int yOld = -1;
        int randomticks = 1;
        int randomticks2 = 0;
        int ctn = 0;
        int pos;
        bool isDrag = false;
        int val = 0;
        int ct = 0;
        int rand = 0;
        int rand2 = 0;
        int ct2 = 0;
        int Flose = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            createmap();

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Keydown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{

            //    case Keys.Space:
            //        tt.Stop();
            //        break;

            //}

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {


                for (int i = 0; i < shoplist.Count; i++)
                {
                    if (e.X >= shoplist[i].X && e.X <= shoplist[i].X + shoplist[0].img[0].Width + 10 && e.Y >= shoplist[i].Y && e.Y <= shoplist[i].Y + shoplist[i].img[0].Height)//&& val >= shoplist[i].price) 
                    {
                        pos = i;
                        xOld = e.X;
                        yOld = e.Y;
                        isDrag = true;
                        createactors(pos);
                    }
                }

                for (int i = 0; i < lsuns.Count; i++)
                {
                    if (e.X >= lsuns[i].X && e.X <= lsuns[i].X + lsuns[i].W + 10 && e.Y >= lsuns[i].Y && e.Y <= lsuns[i].Y + lsuns[i].H)
                    {

                        val += 25;
                        lsuns.RemoveAt(i);
                        break;
                    }

                }
                for (int i = 0; i < lsun2.Count; i++)
                {
                    if (e.X >= lsun2[i].X && e.X <= lsun2[i].X + lsun2[i].W + 10 && e.Y >= lsun2[i].Y && e.Y <= lsun2[i].Y + lsun2[i].H)
                    {
                        val += 25;
                        lsun2.RemoveAt(i);
                        break;
                    }

                }
                for (int i = 0; i < shoplist.Count; i++)
                {
                    if (shoplist[i].price <= val)
                    {
                        shoplist[i].iFrame = 0;
                    }
                    else
                    {
                        shoplist[i].iFrame = 1;
                    }

                }
                //
            }
            if (Flose == 0)
            {
                animateall();
            }
                DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                int dx = e.X - xOld;
                int dy = e.Y - yOld;
                Drag(pos, dx, dy);
                xOld = e.X;
                yOld = e.Y;

            }

            if (Flose == 0)
            {
                animateall();
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {

                int x = isHit(land, lheros[lheros.Count - 1]);
                if (x != -1 && land[x].landoccupied == 0)
                {

                    lheros[lheros.Count - 1].X = land[x].x + 10;
                    lheros[lheros.Count - 1].Y = land[x].y + 20;

                    land[x].landoccupied = 1;
                    if (pos == 0)
                    {
                        val -= 100;
                        createpea();
                        //MessageBox.Show("" + lpea.Count + ", " + lpeashooter.Count);
                    }
                    if (pos == 1)
                    {
                        val -= 50;
                    }
                    if (pos == 2)
                    {
                        val -= 50;
                    }
                    if (pos == 3)
                    {
                        val -= 25;
                    }
                    if (pos == 4)
                    {
                        createsnowpea();
                        val -= 150;
                    }
                    for (int i = 0; i < shoplist.Count; i++)
                    {
                        if (shoplist[i].price <= val)
                        {
                            shoplist[i].iFrame = 0;
                        }
                        else
                        {
                            shoplist[i].iFrame = 1;
                        }

                    }
                    if (pos == 0)
                    {
                        lpeashooter[lpeashooter.Count - 1].drag = 0;
                    }
                    if (pos == 1)
                    {
                        lsunflower[lsunflower.Count - 1].drag = 0;
                    }
                    if (pos == 2)
                    {
                        lwallnut[lwallnut.Count - 1].drag = 0;
                    }
                    if (pos == 3)
                    {
                        lpotatomine[lpotatomine.Count - 1].drag = 0;
                    }
                    if (pos == 4)
                    {
                        lsnowpeashooter[lsnowpeashooter.Count - 1].drag = 0;
                    }

                    lheros[lheros.Count - 1].drag = 0;
                }
                else
                {

                    lheros.RemoveAt(lheros.Count - 1);
                    if (pos == 0)
                    {
                        lpeashooter.RemoveAt(lpeashooter.Count - 1);
                    }
                    if (pos == 1)
                    {
                        lsunflower.RemoveAt(lsunflower.Count - 1);
                    }
                    if (pos == 2)
                    {
                        lwallnut.RemoveAt(lwallnut.Count - 1);
                    }
                    if (pos == 3)
                    {
                        lpotatomine.RemoveAt(lpotatomine.Count - 1);
                    }
                    if (pos == 4)
                    {
                        lsnowpeashooter.RemoveAt(lsnowpeashooter.Count - 1);
                    }
                }
            }
            isDrag = false;

            DrawDubb(this.CreateGraphics());
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            Random rr;
            if (ct == 0)
            {
                 rr = new Random();
                rand = rr.Next(20, 40);
                ct++;
            }
            if(ct2==0)
            {
                 rr = new Random();
                rand2 = rr.Next(110, 130);
                ct2++;
            }
           
            for (int i = 0; i < lpeashooter.Count; i++)
            {


                if (lpeashooter[i].ftick == 1)
                {
                    lpeashooter[i].tick++;
                    if (lpeashooter[i].tick == 5)
                    {

                        lpeashooter[i].tick = 0;
                        lpeashooter[i].ftick = 0;
                    }
                }
            }
            for (int i = 0; i < lsnowpeashooter.Count; i++)
            {
                if (lsnowpeashooter[i].ftick == 1)
                {
                    lsnowpeashooter [i].tick++;
                    if (lsnowpeashooter[i].tick == 5)
                    {

                        lsnowpeashooter[i].tick = 0;
                        lsnowpeashooter[i].ftick = 0;
                        
                    }
                }
            }
            for (int i = 0; i < lzombie.Count; i++)
            {
                


            }
        

            if (randomticks % rand == 0)
            {
                createsuns();
                ct = 0;
                randomticks = 1;

            }
            if (randomticks2 % rand2 == 0)
            {
                createzombie();
                ct2 = 0;
                randomticks = 1;

            }
            for (int i = 0; i < lsuns.Count; i++)
            {
                if (lsuns[i].freached == 1)
                {
                    lsuns[i].tickdeath++;
                    if (lsuns[i].tickdeath == 45)
                    {


                        lsuns.RemoveAt(i);
                    }



                }


            }

            if (ctTick % 40 == 0)
            {

            }
            if (Flose == 0)
            {
                animateall();
            }
            ctTick++;
            randomticks++;
            randomticks2++;

            DrawDubb(this.CreateGraphics());
        }
        void animateall()
        {
            //    Check that land is occupied or not
            if (isDrag == false)
            {
                for (int j = 0; j < land.Count; j++)
                {
                    land[j].landoccupied = 0;
                }


                for (int j = 0; j < lheros.Count; j++)
                {
                    int x = isHit(land, lheros[j]);
                    if (x != -1)
                    {
                        land[x].landoccupied = 1;
                    }
                }
            }
            //lawnmover logic
            for (int i = 0; i < lawnmover.Count; i++)
            {
                for (int j = 0; j < lzombie.Count; j++)
                {
                    if (lawnmover[i].fshoot == 0)
                    {
                        if (lzombie[j].X <= land[0].rectD.X - 70 && lzombie[j].Y == lawnmover[i].Y)
                        {
                            lawnmover[i].fshoot = 1;
                            lzombie[j].fdyingzombie=1;
                            lzombie[j].iFrame=93;
                            lzombie[j].ismove = -1;
                         

                        }
                    }
                }
            }
            for (int i = 0; i < lawnmover.Count; i++)
            {
                if (lawnmover[i].fshoot==1)
                {
                    for (int j = 0; j < lzombie.Count; j++)
                    {
                        if (lzombie[j].X <= lawnmover[i].X + lawnmover[i].W &&lzombie[j].X>=lawnmover[i].X&&lzombie[j].Y == lawnmover[i].Y)
                        {
                            lzombie[j].fdyingzombie = 1;
                            lzombie[j].iFrame = 93;
                            lzombie[j].ismove = -1;
                        }  
                    }
                    lawnmover[i].X += 20;

                }
                
            }


            //


            //potatomine logic
            for (int i = 0; i < lzombie.Count; i++)
            {
                for (int j = 0; j < lpotatomine.Count; j++)
                {
                    if(lpotatomine.Count>0)
                    {


                        if ((lpotatomine[j].X + 70 > lzombie[i].X && lpotatomine[j].X + 40 < lzombie[i].X + lzombie[i].img[0].Width && lpotatomine[j].Y > lzombie[i].Y && lpotatomine[j].Y < lzombie[i].Y + lzombie[i].img[0].Height - 5))
                        {
                            if (lpotatomine[j].drag == 0 && lpotatomine[j].fbright==0)
                            {
                                lpotatomine[j].fbright = 1;
                                lpotatomine[j].iFrame = 29;
                                
                                
                               
                                lzombie.RemoveAt(i);
                                lhealthbar.RemoveAt(i);
                            }

                        }
                    }
                }
            }
            for (int i = 0; i < lzombie.Count; i++)
            {
                for (int j = 0; j < lpotatomine.Count; j++)
                {
                    if (lpotatomine[j].fbright == 1 )
                    {
                        lpotatomine[j].tick2++;

                        if (lpotatomine[j].tick2 < 15)
                        {
                          
                            
                       
                           
                        }
                     
                        else
                        {
                            for(int k=0;k<lheros.Count;k++)
                            { 
                            
                                if (lpotatomine[j].X == lheros[k].X && lpotatomine[j].Y == lheros[k].Y)
                                {
                                    lheros.RemoveAt(k);


                                }
                            }
                            lpotatomine.RemoveAt(j); 
                        }
                    }
                 
                }
            }



            ///


                //

                //pea logic
                //pea shooting logic
                for (int i = 0; i < lpeashooter.Count; i++)
            {
                for (int j = 0; j < lzombie.Count; j++)
                {
                    if (lpeashooter[i].drag == 0)
                    {

                        if (lpeashooter[i].Y - 20 == lzombie[j].Y)
                        {
                            if (lpeashooter[i].ftick == 0)
                            {
                                lpea[i].iFrame = 0;
                                lpea[i].X += lpea[i].speed;
                            }
                            //MessageBox.Show("" + lpea[i].speed);

                        }
                    }
                }
            }
            //snowpea shooting logic
            for (int i = 0; i < lsnowpeashooter.Count; i++)
            {
                for (int j = 0; j < lzombie.Count; j++)
                {
                    if (lsnowpeashooter[i].drag == 0)
                    {

                        if (lsnowpeashooter[i].Y - 20 == lzombie[j].Y)
                        {

                            if (lsnowpeashooter[i].ftick == 0)
                            {
                                lsnowpea[i].iFrame = 0;
                                lsnowpea[i].X += lsnowpea[i].speed;
                            }
                            //MessageBox.Show("" + lpea[i].speed);

                        }
                    }
                }
            }
            //


            //checking if zombie has died and the ball is still in the air
            for (int i = 0; i < lpea.Count; i++)
            {

                for (int j = 0; j < lzombie.Count; j++)
                {
                    if (lzombie[j].fdyingzombie == 1 && lhealthbar[j].w <= 0)
                    {
                        if (lpeashooter[i].Y - 20 == lzombie[j].Y)
                        {
                            lpea[i].iFrame = 0;

                            lpea[i].X = lpea[i].peaoriginalx;


                        }
                    }
                }
            }
            //
            //checking if zombie has died and the ball is still in the air
            for (int i = 0; i < lsnowpea.Count; i++)
            {

                for (int j = 0; j < lzombie.Count; j++)
                {
                    if (lzombie[j].fdyingzombie == 1 && lhealthbar[j].w <= 0)
                    {
                        if (lsnowpeashooter[i].Y - 20 == lzombie[j].Y)
                        {
                            lsnowpea[i].iFrame = 0;
                            lsnowpea[i].X = lsnowpea[i].peaoriginalx;


                        }
                    }
                }
            }
            //
            //pea and zombie collision logic+health bar

            for (int i = 0; i < lpea.Count; i++)
            {

                for (int j = 0; j < lzombie.Count; j++)
                {

                    if (lzombie[j].fdyingzombie == 0)
                    {

                        int s = isHit(lzombie[j], lpea[i]);
                        if (s == 1)
                        {
                            lpea[i].iFrame = 1;


                            lhealthbar[j].w -= 15;
                            if (lhealthbar[j].w <= 60 && lhealthbar[j].w > 50)
                            {
                                lhealthbar[j].cl = Color.Yellow;
                            }
                            if (lhealthbar[j].w <= 50 && lhealthbar[j].w >= 40)
                            {
                                lhealthbar[j].cl = Color.Orange;
                            }
                            if (lhealthbar[j].w < 40 && lhealthbar[j].w > 30)
                            {
                                lhealthbar[j].cl = Color.Red;
                            }
                            if (lhealthbar[j].w <= 30 && lhealthbar[j].w > 0)
                            {
                                lhealthbar[j].cl = Color.DarkRed;
                            }
                            if (lhealthbar[j].w <= 0)
                            {



                                lzombie[j].ismove = -1;
                                lzombie[j].fdyingzombie = 1;
                                lzombie[j].iFrame = 93;




                            }


                            if (lhealthbar[j].w < 0)
                            {

                                lhealthbar[j].w = 0;
                            }

                            lpeashooter[i].ftick = 1;

                            lpea[i].X = lpea[i].peaoriginalx;


                        }

                    }
                }

            }

            /////
            for (int j = 0; j < lzombie.Count; j++)
            {
                if (lzombie[j].ftick == 1)
                {
                    lzombie[j].speed = 2;
                }
                else
                {
                    lzombie[j].speed = 5;
                }
            }
            ///  //snowpea and zombie collision logic

            for (int i = 0; i < lsnowpea.Count; i++)
            {

                for (int j = 0; j < lzombie.Count; j++)
                {

                    if (lzombie[j].fdyingzombie == 0)
                    {

                        int s = isHit(lzombie[j], lsnowpea[i]);
                        if (s == 1)
                        {
                            lhealthbar[j].w -= 15;
                            if (lhealthbar[j].w <= 60 && lhealthbar[j].w > 50)
                            {
                                lhealthbar[j].cl = Color.Yellow;
                            }
                            if (lhealthbar[j].w <= 50 && lhealthbar[j].w >= 40)
                            {
                                lhealthbar[j].cl = Color.Orange;
                            }
                            if (lhealthbar[j].w < 40 && lhealthbar[j].w>30)
                            {
                                lhealthbar[j].cl = Color.Red;
                            }
                            if (lhealthbar[j].w <= 30 && lhealthbar[j].w>0)
                            {
                                lhealthbar[j].cl = Color.DarkRed;
                            }
                            if (lhealthbar[j].w <= 0)
                            {



                                lzombie[j].ismove = -1;
                                lzombie[j].fdyingzombie = 1;
                                lzombie[j].iFrame = 93;




                            }
                            if (lhealthbar[j].w < 0)
                            {

                                lhealthbar[j].w = 0;
                            }

                            lsnowpea[i].iFrame = 1;

                            lzombie[j].ftick = 1;
                            
                                                      
                            lzombie[j].ftick = 1;
                            lsnowpeashooter[i].ftick = 1;

                            lsnowpea[i].X = lsnowpea[i].peaoriginalx;


                        }

                    }
                }

            }

            /////

            //pea end

            //zombie logic
            //zombie moving to house
            if (Flose == 0)
            {
                for (int i = 0; i < lzombie.Count; i++)
                {

                    if (lzombie[i].ismove != -1)
                    {
                        if (lzombie[i].X < land[0].rectD.X - 145)
                        {
                            Flose = 1;
                        }

                        lzombie[i].X -= lzombie[i].speed;
                        lhealthbar[i].x -= lzombie[i].speed;
                    }
                    if (lzombie[i].X < land[0].rectD.X - 130)
                    {
                        lzombie[i].ismove = -1;
                        if (lzombie[i].Y < 300 || lzombie[i].Y >= ClientSize.Height / 2)
                        {
                            if (lzombie[i].Y < 300)
                            {
                                lzombie[i].Y += lzombie[i].speed;
                                lhealthbar[i].y += lzombie[i].speed;
                            }
                            if (lzombie[i].Y > ClientSize.Height / 2)
                            {
                                lzombie[i].Y -= lzombie[i].speed;
                                lhealthbar[i].y -= lzombie[i].speed;
                            }
                        }
                        else
                        {
                            lzombie[i].ismove = 0;
                        }
                        
                    }
                }
            }
               //
            
           
               
            

            //

            //zombie animation
            //walk animation
            for (int i = 0; i < lzombie.Count; i++)
            {
                if (lzombie[i].fdyingzombie == 0 && lzombie[i].feat == 0)
                {

                    lzombie[i].iFrame = (lzombie[i].iFrame + 1) % 93;

                }

            }




            //death 
            for (int i = 0; i < lzombie.Count; i++)
            {


                if (lzombie[i].fdyingzombie == 1)
                {
                    lhealthbar[i].w = 0;
                    if (lzombie[i].iFrame < 130)
                    {
                        lzombie[i].iFrame++;
                    }
                    else
                    {
                        ctn = 0;
                        lzombie.RemoveAt(i);
                        lhealthbar.RemoveAt(i);


                    }
                }



            }
            // Eating
            for (int i = 0; i < lzombie.Count; i++)
            {
                for (int j = 0; j < lheros.Count; j++)
                {
                    if (lzombie[i].fdyingzombie == 0)
                    {
                        if ((lheros[j].X + 40 > lzombie[i].X && lheros[j].X + 40 < lzombie[i].X + lzombie[i].img[0].Width && lheros[j].Y > lzombie[i].Y && lheros[j].Y < lzombie[i].Y + lzombie[i].img[0].Height - 5))
                        {
                            if (lheros[j].drag == 0)
                            {
                                // Check for wallnut
                                for (int w = 0; w < lwallnut.Count; w++)
                                {
                                    if (lheros[j].X == lwallnut[w].X && lheros[j].Y == lwallnut[w].Y)
                                    {
                                        lheros[j].wallnutf = 1;
                                    }
                                }

                                if (lheros[j].wallnutf == 1)
                                {

                                    if (lzombie[i].tick == 0)
                                    {
                                        lzombie[i].iFrame = 132;
                                    }

                                    lzombie[i].tick++;
                                    lzombie[i].ismove = -1;
                                    lzombie[i].feat = 1;

                                    if (lzombie[i].iFrame < 172)
                                    {
                                        lzombie[i].iFrame++;
                                        lheros[j].ctwnut++;
                                    }
                                    if (lheros[j].ctwnut % 40 == 0)
                                    {
                                        //
                                        for (int b = 0; b < lwallnut.Count; b++)
                                        {
                                            for (int a = 0; a < lzombie.Count; a++)
                                            {

                                                if ((lwallnut[b].X + 40 > lzombie[a].X && lwallnut[b].X + 40 < lzombie[a].X + lzombie[a].img[0].Width && lwallnut[b].Y > lzombie[a].Y && lwallnut[b].Y < lzombie[a].Y + lzombie[a].img[0].Height - 5))
                                                {

                                                    lzombie[a].iFrame = 132;
                                                    //tt.Stop();
                                                    //MessageBox.Show("were both alive and our x is" + lzombie[a].X +" "+" "+lzombie[a].Y);
                                                    //tt.Start();
                                                }
                                            }
                                        }
                                        //
                                        lzombie[i].iFrame = 132;
                                    }
                                    if (lheros[j].ctwnut == 321)
                                    {

                                        for (int b = 0; b < lwallnut.Count; b++)
                                        {
                                            for (int a = 0; a < lzombie.Count; a++)
                                            {

                                                if ((lwallnut[b].X + 40 > lzombie[a].X && lwallnut[b].X + 40 < lzombie[a].X + lzombie[a].img[0].Width && lwallnut[b].Y > lzombie[a].Y && lwallnut[b].Y < lzombie[a].Y + lzombie[a].img[0].Height - 5))
                                                {
                                                    Random rr = new Random();
                                                    int xx = rr.Next(1, 90);
                                                    lzombie[a].tick = 0;
                                                    lzombie[a].iFrame = xx;
                                                    lzombie[a].ismove = 0;
                                                    lzombie[a].feat = 0;
                                                    //tt.Stop();
                                                    //MessageBox.Show("were both alive and our x is" + lzombie[a].X +" "+" "+lzombie[a].Y);
                                                    //tt.Start();
                                                }
                                            }
                                        }
                                        for (int k = 0; k < lwallnut.Count; k++)
                                        {
                                            if (lwallnut[k].X == lheros[j].X&& lwallnut[k].Y == lheros[j].Y)
                                            {
                                                lwallnut.RemoveAt(k);


                                            }
                                        }
                                        lheros.RemoveAt(j);

                                    }

                                }
                                else
                                {
                                    if (lzombie[i].tick2 == 1)
                                    {
                                        lzombie[i].iFrame = 132;
                                    }
                                    lzombie[i].tick2++;
                                    lzombie[i].ismove = -1;
                                    lzombie[i].feat = 1;

                                    if (lzombie[i].iFrame < 171)
                                    {
                                        lzombie[i].iFrame++;
                                    }
                                    else
                                    {


                                        for (int b = 0; b < lheros.Count; b++)
                                        {
                                            for (int a = 0; a < lzombie.Count; a++)
                                            {

                                                if ((lheros[b].X + 40 > lzombie[a].X && lheros[b].X + 40 < lzombie[a].X + lzombie[a].img[0].Width && lheros[b].Y > lzombie[a].Y && lheros[b].Y < lzombie[a].Y + lzombie[a].img[0].Height - 5))
                                                {
                                                    Random rr = new Random();
                                                    int xx=rr.Next(1, 90);
                                                    lzombie[a].tick2 = 0;
                                                    lzombie[a].iFrame = xx;
                                                    lzombie[a].ismove = 0;
                                                    lzombie[a].feat = 0;
                                                    //tt.Stop();
                                                    //MessageBox.Show("were both alive and our x is" + lzombie[a].X +" "+" "+lzombie[a].Y);
                                                    //tt.Start();
                                                }
                                            }
                                        }
                                        for (int k = 0; k < lsunflower.Count; k++)
                                        {
                                            if (lsunflower[k].X == lheros[j].X && lsunflower[k].Y == lheros[j].Y)
                                            {
                                                lsunflower.RemoveAt(k);
                                              

                                            }
                                        }

                                        for (int k = 0; k < lpeashooter.Count; k++)
                                        {
                                            if (lpeashooter[k].X == lheros[j].X && lpeashooter[k].Y == lheros[j].Y)
                                            {
                                                lpeashooter.RemoveAt(k);
                                                lpea.RemoveAt(k);

                                            }
                                        }
                                        for (int k = 0; k < lsnowpeashooter.Count; k++)
                                        {
                                            if (lsnowpeashooter[k].X == lheros[j].X && lsnowpeashooter[k].Y == lheros[j].Y)
                                            {
                                                if (lzombie[i].ftick == 1 && lzombie[i].Y  == lsnowpeashooter[k].Y - 20)
                                                {
                                                    lzombie[i].ftick = 0;
                                                }
                                                lsnowpeashooter.RemoveAt(k);
                                                lsnowpea.RemoveAt(k);
                                              

                                            }
                                        }
                                        lheros.RemoveAt(j);

                                    }


                                }

                            }







                        }
                    }
                }
            }

            ///zombie animation END
            //heros animation

            for (int i = 0; i < lsnowpeashooter.Count; i++)

            {
                lsnowpeashooter[i].iFrame = (lsnowpeashooter[i].iFrame + 1) % 25;
            }
            for (int i = 0; i < lpeashooter.Count; i++)

            {
                lpeashooter[i].iFrame = (lpeashooter[i].iFrame + 1) % 49;
            }
            for (int i = 0; i < lsunflower.Count; i++)
            {
                if (lsunflower[i].fbright == 0)
                {
                    lsunflower[i].iFrame = (lsunflower[i].iFrame + 1) % 55;
                }


            }
            for (int i = 0; i < lwallnut.Count; i++)

            {
                lwallnut[i].iFrame = (lwallnut[i].iFrame + 1) % 44;
            }
            for (int i = 0; i < lpotatomine.Count; i++)
            {
                if (lpotatomine[i].fbright == 0)
                {
                    lpotatomine[i].iFrame = (lpotatomine[i].iFrame + 1) % 29;
                }
            }

            //
            //suns start
            for (int i = 0; i < lsunflower.Count; i++)
            {
                if (lsunflower[i].drag == 0)
                {
                    lsunflower[i].tick++;
                    lsunflower[i].tickdeath++;


                    if (lsunflower[i].tick % 115 == 0)
                    {


                        lsunflower[i].tickdeath = lsunflower[i].tick;
                                sunflowersun(i);
                    }
                    if (lsunflower[i].tickdeath == 155)
                    {
                        for (int j = 0; j < lsun2.Count; j++)
                        {

                            if (lsun2[j].index == i && lsun2[j].fdir == -1)
                            {
                                lsun2.RemoveAt(j);

                            }
                        }

                        lsunflower[i].tickdeath = lsunflower[i].tick;

                    }
                }
            }
            for (int i = 0; i < lsun2.Count; i++)
            {

                if (lsun2[i].fbright == 1)
                {
                    int assignedsunflower = lsun2[i].index;
                    if (lsun2[i].fdir == 0)
                    {

                        if (lsun2[i].Y >= lsunflower[assignedsunflower].Y - 40)
                        {

                            lsun2[i].Y -= 15;
                        }
                        else
                        {
                            lsun2[i].fdir = 1;
                        }
                    }
                    if (lsun2[i].fdir == 1)
                    {
                        if (lsun2[i].X > lsunflower[assignedsunflower].X - 4)
                        {
                            lsun2[i].Y += 4;
                            lsun2[i].X -= 6;
                        }
                        else
                        {
                            lsun2[i].fdir = 2;


                        }

                    }
                    if (lsun2[i].fdir == 2)
                    {
                        if (lsun2[i].Y <= lsunflower[assignedsunflower].Y + 25)
                        {
                            lsun2[i].Y += 15;
                        }
                        else
                        {
                            lsun2[i].fdir = -1;
                            lsun2[i].fbright = 0;
                            lsunflower[assignedsunflower].tick = 1;

                        }
                    }

                }
            }
            for (int i = 0; i < lsuns.Count; i++)
            {
                if (lsuns[i].Y <= lsuns[i].RandomDestination)
                {
                    lsuns[i].Y += 10;
                }
                else
                {
                    lsuns[i].freached = 1;

                }
            }
            //suns end
        }
        void sunflowersun(int sunflowerIndex)
        {

            CImActor pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            Bitmap img = new Bitmap("PvZ/sun1.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.X = lsunflower[sunflowerIndex].X + 2;
            pnn2.Y = lsunflower[sunflowerIndex].Y + 2;
            pnn2.W = pnn2.img[0].Width - 100;
            pnn2.H = pnn2.img[0].Height - 100;
            pnn2.index = sunflowerIndex;
            pnn2.fbright = 1;
            lsun2.Add(pnn2);
        }
        void createsnowpea()
        {
            CImActor pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();

            Bitmap img = new Bitmap("PvZ/snowpea.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
           
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);

            pnn2.X = lsnowpeashooter[lsnowpeashooter.Count - 1].X + 25;
            pnn2.Y = lsnowpeashooter[lsnowpeashooter.Count - 1].Y + 13;
            pnn2.W = pnn2.img[0].Width;
            pnn2.H = pnn2.img[0].Height;
            //pnn2.peaindex = lpeashooter.Count - 1;
            pnn2.peaoriginalx = pnn2.X;
            lsnowpea.Add(pnn2);
        }
        void createpea()
        {
            CImActor pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            Bitmap img = new Bitmap("PvZ/pea.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
           
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.X = lpeashooter[lpeashooter.Count - 1].X + 25;
            pnn2.Y = lpeashooter[lpeashooter.Count - 1].Y + 13;
            pnn2.W = pnn2.img[0].Width;
            pnn2.H = pnn2.img[0].Height;
            //pnn2.peaindex = lpeashooter.Count - 1;
            pnn2.peaoriginalx = pnn2.X;
            lpea.Add(pnn2);
        }
        int isHit(CImActor zombie, CImActor l)
        {



            if (l.X >= zombie.X && l.X <= zombie.X + zombie.img[0].Width && l.Y > zombie.Y && l.Y < zombie.Y + zombie.img[0].Height)
            {
                return 1;
            }
            return -1;
        }
        int isHit(List<CAdvImag> land, CImActor l)
        {
            int xmid = l.X + 20;
            int ymid = l.Y + 20;
            for (int i = 0; i < land.Count; i++)
            {

                if (xmid > land[i].rectD.Left && xmid < land[i].rectD.Right && ymid > land[i].rectD.Top && ymid < land[i].rectD.Bottom)
                {
                    return i;

                }
            }

            return -1;
        }
        void createsuns()
        {
            Random rr = new Random();
            CImActor pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            Bitmap img = new Bitmap("PvZ/sun1.png");
            img.MakeTransparent(img.GetPixel(0, 0));

            pnn2.img.Add(img);
            pnn2.X = rr.Next(0, ClientSize.Width - 100);
            pnn2.Y = lshop[0].Y;
            pnn2.W = pnn2.img[0].Width - 100;
            pnn2.H = pnn2.img[0].Height - 100;
            pnn2.RandomDestination = rr.Next(ClientSize.Height / 2, ClientSize.Height - 20);
            lsuns.Add(pnn2);

        }
        void Drag(int i, int dx, int dy)
        {
            if (i == 0)
            {

                lpeashooter[lpeashooter.Count - 1].X += dx;
                lpeashooter[lpeashooter.Count - 1].Y += dy;

            }
            if (i == 1)
            {
                lsunflower[lsunflower.Count - 1].X += dx;
                lsunflower[lsunflower.Count - 1].Y += dy;
            }
            if (i == 2)
            {

                lwallnut[lwallnut.Count - 1].X += dx;
                lwallnut[lwallnut.Count - 1].Y += dy;

            }
            if (i == 3)
            {
                lpotatomine[lpotatomine.Count - 1].X += dx;
                lpotatomine[lpotatomine.Count - 1].Y += dy;
            }
            if (i == 4)
            {
                lsnowpeashooter[lsnowpeashooter.Count - 1].X += dx;
                lsnowpeashooter[lsnowpeashooter.Count - 1].Y += dy;
            }
        }
        void createactors(int i)
        {
            if (i == 0)
            {
                CImActor pnn = new CImActor();
                pnn.img = new List<Bitmap>();
                Bitmap img;
                for (int k = 0; k < 49; k++)
                {
                    img = new Bitmap("PvZ/peashooterAnimation/peashooter (" + (k + 1) + ").gif");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.img.Add(img);
                }
                pnn.X = shoplist[0].X;
                pnn.Y = shoplist[0].Y;
                pnn.W = 90;
                pnn.H = 92;
                pnn.iFrame = 0;
                pnn.index = lsunflower.Count - 1;
                pnn.drag = 1;
                lpeashooter.Add(pnn);
                lheros.Add(pnn);
                ///////


            }
            if (i == 1)
            {
                CImActor pnn = new CImActor();
                pnn.img = new List<Bitmap>();
                Bitmap img;
                for (int k = 0; k < 55; k++)
                {
                    img = new Bitmap("PvZ/sunflowerAnimation/sunflower (" + (k + 1) + ").gif");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.img.Add(img);
                }


                pnn.X = shoplist[1].X;
                pnn.Y = shoplist[1].Y;
                pnn.W = 90;
                pnn.H = 92;
                pnn.iFrame = 0;
                pnn.index = lsunflower.Count - 1;
                pnn.drag = 1;
                lsunflower.Add(pnn);
                lheros.Add(pnn);
            }
            if (i == 2)
            {
                CImActor pnn = new CImActor();
                pnn.img = new List<Bitmap>();

                for (int k = 0; k < 44; k++)
                {
                    Bitmap img = new Bitmap("PvZ/wallnutAnimation/wallnut (" + (k + 1) + ").gif");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.img.Add(img);
                }
                pnn.X = shoplist[2].X;
                pnn.Y = shoplist[2].Y;
                pnn.W = 90;
                pnn.H = 92;
                pnn.iFrame = 0;
                pnn.index = lsunflower.Count - 1;
                pnn.wallnutf = 0;
                pnn.drag = 1;
                lwallnut.Add(pnn);
                lheros.Add(pnn);
            }
            if (i == 3)
            {
                CImActor pnn = new CImActor();
                pnn.img = new List<Bitmap>();
                Bitmap img;
                for (int k = 0; k < 29; k++)
                {
                    img = new Bitmap("PvZ/potatomineAnimation/potatomine (" + (k + 1) + ").gif");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.img.Add(img);
                   
                }
                img = new Bitmap("PvZ/exploded.png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
                pnn.X = shoplist[2].X;
                pnn.Y = shoplist[2].Y;
                pnn.W = 90;
                pnn.H = 92;

                pnn.iFrame = 0;
                pnn.index = lsunflower.Count - 1;
                pnn.drag = 1;
                lpotatomine.Add(pnn);
                lheros.Add(pnn);
            }
            if (i == 4)
            {
                CImActor pnn = new CImActor();
                pnn.img = new List<Bitmap>();
              
                for (int k = 0; k < 25; k++)
                {
                    Bitmap img = new Bitmap("PvZ/snowpeashooterAnimation/snowpeashooter (" + (k + 1) + ").gif");
                    img.MakeTransparent(img.GetPixel(0, 0));
                    pnn.img.Add(img);
                }
          
               
               
                pnn.X = shoplist[3].X;
                pnn.Y = shoplist[3].Y;
                pnn.W = 90;
                pnn.H = 92;
                pnn.iFrame = 0;
                pnn.index = lsunflower.Count - 1;
                pnn.drag = 1;
                lsnowpeashooter.Add(pnn);
                lheros.Add(pnn);
            }

        }
        void createzombie()
        {
            Random rr = new Random();

            CImActor pnn = new CImActor();
            pnn.img = new List<Bitmap>();

            for (int k = 0; k < 93; k++)
            {
                Bitmap img = new Bitmap("PvZ/zombiewalkAnimation/zombiewalk (" + (k + 1) + ").gif");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
                pnn.whichanimation = 0;
            }
            for (int k = 0; k < 39; k++)
            {
                Bitmap img = new Bitmap("PvZ/zombiedeathAnimation/zombiedeath (" + (k + 1) + ").gif");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
                pnn.whichanimation = 1;
            }
            for (int k = 0; k < 40; k++)
            {
                Bitmap img = new Bitmap("PvZ/zombieeatAnimation/zombieeat (" + (k + 1) + ").gif");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
                pnn.whichanimation = 2;
            }
            pnn.X = ClientSize.Width - 10;
            int yy = rr.Next(5);
            if (yy == 0)
            {
                pnn.Y = land[0].y;
            }
            if (yy == 1)
            {
                pnn.Y = land[9].y;

            }
            if (yy == 2)
            {
                pnn.Y = land[18].y;
            }
            if (yy == 3)
            {
                pnn.Y = land[27].y;

            }
            if (yy == 4)
            {
                pnn.Y = land[36].y;
            }
            pnn.W = pnn.img[0].Width;
            pnn.H = pnn.img[0].Height;
            pnn.iFrame = 0;
            pnn.tick = 0;
            pnn.speed = 5;
            lzombie.Add(pnn);
            //health bar
            CActor pnn2 = new CActor();
            pnn2.cl = Color.LawnGreen;
            pnn2.x = lzombie[lzombie.Count - 1].X - 10;
            pnn2.y = lzombie[lzombie.Count - 1].Y - 10;
            pnn2.w = 100;
            pnn2.h = 10;
            lhealthbar.Add(pnn2);
        }
        void createmap()
        {
            CAdvImag pnn = new CAdvImag();
            pnn.img = new Bitmap("PvZ/background_day.png");
            pnn.rectD = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            pnn.rectS = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            lmap.Add(pnn);
            int xmargin = 310;
            int ymargin = 90;
            //create land
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    pnn = new CAdvImag();

                    pnn.img = new Bitmap("PvZ/background.jpg");
                    pnn.x = xmargin;
                    pnn.y = ymargin;


                    pnn.rectS = new Rectangle(pnn.x, pnn.y, 100, 108);
                    pnn.rectD = new Rectangle(pnn.x, pnn.y, 100, 108);
                    pnn.landoccupied = 0;
                    land.Add(pnn);
                    xmargin += 100;
                }
                xmargin = 310;
                ymargin += 108;
            }
            //create list
            CImActor pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            Bitmap img = new Bitmap("PvZ/itemShop.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.X = 5;
            pnn2.Y = 0;
            lshop.Add(pnn2);
            //create menu
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/menu.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.X = ClientSize.Width - pnn2.img[0].Width + 35;
            pnn2.Y = 0;
            lshop.Add(pnn2);
            //shoplist items
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/peashooteritem.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.price = 100;
            pnn2.img.Add(img);
            img = new Bitmap("PvZ/greypeeshooter.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.iFrame = 1;
            pnn2.X = lshop[0].X + 85;
            pnn2.Y = lshop[0].Y;
            pnn2.W = pnn2.img[0].Width + 10;
            pnn2.H = pnn2.img[0].Height;
            shoplist.Add(pnn2);
            //////////////////////////////////
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/sunfloweritem.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.iFrame = 1;
            pnn2.img.Add(img);
            img = new Bitmap("PvZ/greysunflower.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.price = 50;
            pnn2.X = shoplist[0].X + shoplist[0].img[0].Width + 11;
            pnn2.iFrame = 1;
            pnn2.Y = shoplist[0].Y;
            pnn2.W = pnn2.img[0].Width + 10;
            pnn2.H = pnn2.img[0].Height;
            shoplist.Add(pnn2);
            //////////////////////////////////////
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/wallnutItem.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            img = new Bitmap("PvZ/greynut.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.iFrame = 1;
            pnn2.X = shoplist[1].X + shoplist[1].img[0].Width + 11;
            pnn2.Y = shoplist[1].Y;
            pnn2.W = pnn2.img[0].Width + 10;
            pnn2.H = pnn2.img[0].Height;
            pnn2.price = 50;
            pnn2.iFrame = 1;
            shoplist.Add(pnn2);
            /////////////////////////////////
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/potatomineItem.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            img = new Bitmap("PvZ/greypotatomine.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.price = 25;

            pnn2.X = shoplist[2].X + shoplist[2].img[0].Width + 11;
            pnn2.Y = shoplist[2].Y;
            pnn2.W = pnn2.img[0].Width + 10;
            pnn2.H = pnn2.img[0].Height;
            pnn2.iFrame = 1;
            shoplist.Add(pnn2);
            ////
            CActor pnn3 = new CActor();
            pnn3.x = 7;
            pnn3.y = 65;
            pnn3.w = 80;
            pnn3.h = 30;
            pnn2.price = 25;
            lsuncounter.Add(pnn3);
            /////
            pnn2 = new CImActor();
            pnn2.img = new List<Bitmap>();
            img = new Bitmap("PvZ/snowpeashooterItem.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            img = new Bitmap("PvZ/greysnowpeashooter.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn2.img.Add(img);
            pnn2.price = 175;
            pnn2.X = shoplist[3].X + shoplist[2].img[0].Width + 11;
            pnn2.Y = shoplist[3].Y;
            pnn2.W = pnn2.img[0].Width + 15;
            pnn2.H = pnn2.img[0].Height;
            pnn2.iFrame = 1;
            shoplist.Add(pnn2);
            ////
            int sss = 0; 
            for (int i = 0; i < 5; i++)
            {
                pnn2 = new CImActor();
                pnn2.img = new List<Bitmap>();
                img = new Bitmap("PvZ/lawn_mover.png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn2.img.Add(img);
                pnn2.X = land[0].rectD.X - 90;
                pnn2.Y = land[sss].rectD.Y;
                pnn2.W = 92;
                pnn2.H = 90;
                sss+=9;

                lawnmover.Add(pnn2);
            }
            ///
        }
        void drawscene(Graphics g)
        {
            g.Clear(Color.Black);

           
            for (int i = 0; i < land.Count; i++)
            {
                CAdvImag ptrav = land[i];
                g.DrawImage(ptrav.img, ptrav.rectD, ptrav.rectS, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < lmap.Count; i++)
            {
                CAdvImag ptrav = lmap[i];
                g.DrawImage(ptrav.img, ptrav.rectD, ptrav.rectS, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < lshop.Count; i++)
            {
                int k = lshop[i].iFrame;
                g.DrawImage(lshop[i].img[k], lshop[i].X, lshop[i].Y, lshop[i].img[k].Width - 35, lshop[i].img[k].Height - 35);
            }

            for (int i = 0; i < shoplist.Count; i++)
            {
                int k = shoplist[i].iFrame;
                g.DrawImage(shoplist[i].img[k], shoplist[i].X, shoplist[i].Y, shoplist[i].W, shoplist[i].H);
            }
            for (int i = 0; i < lpea.Count; i++)
            {
                int k = lpea[i].iFrame;
                g.DrawImage(lpea[i].img[k], lpea[i].X, lpea[i].Y, lpea[i].W, lpea[i].H);
            }
            for (int i = 0; i < lsnowpea.Count; i++)
            {
                int k = lsnowpea[i].iFrame;
                g.DrawImage(lsnowpea[i].img[k], lsnowpea[i].X, lsnowpea[i].Y, lsnowpea[i].W, lsnowpea[i].H);
            }
            for (int i = 0; i < lheros.Count; i++)
            {
                int k = lheros[i].iFrame;
                g.DrawImage(lheros[i].img[k], lheros[i].X, lheros[i].Y, lheros[i].W, lheros[i].H);
            }
           
            for (int i = 0; i < lzombie.Count; i++)
            {
                int k = lzombie[i].iFrame;
                g.DrawImage(lzombie[i].img[k], lzombie[i].X, lzombie[i].Y, lzombie[i].W, lzombie[i].H);
            }

            for (int i = 0; i < lsuns.Count; i++)
            {
                int k = lsuns[i].iFrame;
                g.DrawImage(lsuns[i].img[k], lsuns[i].X, lsuns[i].Y, lsuns[i].W, lsuns[i].H);
            }
            for (int i = 0; i < lsun2.Count; i++)
            {
                int k = lsun2[i].iFrame;
                g.DrawImage(lsun2[i].img[k], lsun2[i].X, lsun2[i].Y, lsun2[i].W, lsun2[i].H);
            }
            for (int i = 0; i < lawnmover.Count; i++)
            {
                int k = lawnmover[i].iFrame;
                g.DrawImage(lawnmover[i].img[k], lawnmover[i].X, lawnmover[i].Y, lawnmover[i].W, lawnmover[i].H);
            }
            for (int i = 0; i < lsuncounter.Count; i++)
            {
                SolidBrush brsh = new SolidBrush(Color.Beige);
                g.FillRectangle(brsh, lsuncounter[0].x, lsuncounter[0].y, lsuncounter[0].w, lsuncounter[0].h);
            }
            for (int i = 0; i < lhealthbar.Count; i++)
            {
                SolidBrush brsh = new SolidBrush(lhealthbar[i].cl);
                g.FillRectangle(brsh, lhealthbar[i].x, lhealthbar[i].y, lhealthbar[i].w, lhealthbar[i].h);
            }
            if (Flose == 1)
            {
                Bitmap img = new Bitmap("PvZ/gameover.png");
                g.DrawImage(img, 100, 50, 800, 700);
                tt.Stop();
            }
            //
            int fontSize = 20;
            Font font = new Font("Arial", fontSize, FontStyle.Regular);

            g.DrawString(val.ToString(), font, Brushes.Black, lsuncounter[0].x + 20, lsuncounter[0].y);
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
