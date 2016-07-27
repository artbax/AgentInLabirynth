using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace ITOProjekt
{
    public class Labirynt
    {
        private Stack stos;
        int a = 40;
        

        private Image mur = Image.FromFile("C:\\Users\\Artur\\Documents\\Visual Studio 2013\\Projects\\ITOProjekt\\ITOProjekt\\Resources\\mur.png");
        private Image agent = Image.FromFile("C:\\Users\\Artur\\Documents\\Visual Studio 2013\\Projects\\ITOProjekt\\ITOProjekt\\Resources\\agent.png");
        private Image exit = Image.FromFile("C:\\Users\\Artur\\Documents\\Visual Studio 2013\\Projects\\ITOProjekt\\ITOProjekt\\Resources\\exit3.png");

        public Labirynt()
        {

        }

        public int[,] GenerujLabirynt(int N)
        {
            int[,] labirynt = new int[N, N];

            
            for (int i = 0; i < labirynt.GetLength(0); i++)
            {
                for (int j = 0; j < labirynt.GetLength(1); j++)
                {
                    labirynt[i, j] = -1;
                }
            }

            stos = new Stack(N);

            
            Random r = new Random();
            int x = r.Next(1, N);
            while (x % 2 == 0)
            {
                x = r.Next(1, N);
            }

            int y = r.Next(1, N);
            while (y % 2 == 0)
            {
                y = r.Next(1, N);
            }

            
            labirynt[x, y] = 0;

            int total = (N * N) / 4;
            int visited = 1;
            int[] czterypola = new int[4];
            int totalrand;

            while (visited < total)
            {
                totalrand = 0;
                if (x > 1 && labirynt[x - 2, y] == -1)
                    czterypola[totalrand++] = 1;
                if (x < N - 2 && labirynt[x + 2, y] == -1)  
                    czterypola[totalrand++] = 2;
                if (y > 1 && labirynt[x, y - 2] == -1)
                    czterypola[totalrand++] = 3;
                if (y < N - 2 && labirynt[x, y + 2] == -1) 
                    czterypola[totalrand++] = 4;

                if (totalrand > 0)
                {
                    switch (czterypola[r.Next(0, totalrand)])
                    {
                        case 1: labirynt[x - 2, y] = labirynt[x - 1, y] = 0; 
                            x -= 2;
                            stos.Push(x * N + y);
                            visited++;
                            break;
                        case 2: labirynt[x + 2, y] = labirynt[x + 1, y] = 0;
                            x += 2;
                            stos.Push(x * N + y);
                            visited++;
                            break;
                        case 3: labirynt[x, y - 2] = labirynt[x, y - 1] = 0;  
                            y -= 2;
                            stos.Push(x * N + y);
                            visited++;
                            break;
                        case 4: labirynt[x, y + 2] = labirynt[x, y + 1] = 0;
                            y += 2;
                            stos.Push(x * N + y);
                            visited++;
                            break;
                    }
                }
                else
                {
                    if (stos.Count != 0)
                    {
                        int vert = (int)stos.Pop();
                        x = vert / N;
                        y = vert % N;
                    }
                    else
                    {

                        Random ran = new Random();
                        x = ran.Next(1, N - 1);
                        y = ran.Next(1, N - 1);

                    }
                }
            }

            for (int i = 0; i < labirynt.GetLength(0); i++)
            {
                for (int j = 0; j < labirynt.GetLength(1); j++)
                {
                    labirynt[0, j] = -1;
                    labirynt[i, 0] = -1;
                    labirynt[i, N - 1] = -1;
                    labirynt[N - 1, j] = -1;

                }
            }

            
            
            
            Random q = new Random();
            int m = q.Next(1, N / 2);
            int n = q.Next(1, N / 2);
            while (labirynt[m, n] != -1)
            {
                m = q.Next(1, N / 2);
                n = q.Next(1, N / 2);
            }
            labirynt[m, n] = -2;  // agent

            int o = q.Next(N / 2, N-1);
            int p = q.Next(N / 2, N-1);
            while (labirynt[o, p] != -1 && labirynt[o, p] != -2)
            {
                o = q.Next(N / 2, N-1);
                p = q.Next(N / 2, N-1);
            }
            labirynt[o, p] = 100; // exit


            // MANHATTAN
            /*Point point = new Point();
            point = ZnajdzPunkt(labirynt, -3);

            for (int i = 0; i < labirynt.GetLength(0); i++)
            {
                for (int j = 0; j < labirynt.GetLength(1); j++)
                {
                    if (labirynt[i, j] == 0)
                    {
                        labirynt[i, j] = Math.Abs(i - point.X) + Math.Abs(j - point.Y);
                    }
                }
            }*/

            Point point = new Point();
            point = ZnajdzPunkt(labirynt, 100);
            for (int i = 0; i < labirynt.GetLength(0); i++ )
            {
                for(int j = 0; j < labirynt.GetLength(1); j++)
                {
                    if ((labirynt[i, j] != -1) && (labirynt[i, j] != -2) && (labirynt[i, j] != 100))
                    {
                        labirynt[i, j] = i + j;
                    }
                }
            }

                /*for (int i = 0; i <= point.X; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (labirynt[i, j] == 0)
                        {
                            labirynt[i, j] += i + 1;
                        }
                    }
                }

                for (int i = point.X; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (labirynt[i, j] == 0)
                        {
                            labirynt[i, j] += N - i;
                        }
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    for (int j = point.Y; j < N; j++)
                    {
                        if ((labirynt[i, j] != -1) && (labirynt[i, j] != -2) && (labirynt[i, j] != 100))
                        {
                            labirynt[i, j] += N - j;
                        }
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j <= point.Y; j++)
                    {
                        if ((labirynt[i, j] != -1) && (labirynt[i, j] != -2) && (labirynt[i, j] != 100))
                        {
                            labirynt[i, j] += N + j;
                        }
                    }
                }*/




                return labirynt;
        }

        

        public Point ZnajdzPunkt(int[,] array, int value)  // do ustalania wspolrzednych startu i exit
        {
            Point point = new Point();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value)
                    {
                        point.X = i;
                        point.Y = j;
                    }
                }
            }
            return point;
        }

       

        public Bitmap Draw(int xbmp, int ybmp, int[,] tab)
        {
            int[,] plansza = tab;

            Bitmap bmp = new Bitmap(xbmp + 1, ybmp + 1);
            Graphics g = Graphics.FromImage((Image)bmp);
            g.PageUnit = GraphicsUnit.Pixel;

            Pen blackPen = new Pen(Color.Black, 2);
            Pen b = new Pen(Color.Black, 1);
            Font aFont = new Font("Arial", 9, FontStyle.Regular);

            for (int row = 0; row < plansza.GetLength(0); row++)
            {
                for (int col = 0; col < plansza.GetLength(1); col++)
                {
                    if (plansza[row, col] == -1)
                    {
                        g.DrawImage(mur, row * a, col * a, a, a);
                    }
                    else if (plansza[row, col] == -2)
                    {
                        g.DrawImage(agent, row * a, col * a, a, a);
                    }
                    else if (plansza[row, col] == 100)
                    {

                        g.DrawImage(exit, row * a, col * a, a, a);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, row * a, col * a, a, a);
                        //string val = Convert.ToString(plansza[row, col]);
                        //g.DrawString(val, aFont, Brushes.Black, (float)((row * 40)), (float)((col * 40)));
                    }
                }
            }


            return bmp;

        }

        /*public void GenerujBliskosc()
        {
            Point point = new Point();
            point = ZnajdzPunkt(labirynt, -3);

            for (int i = 0; i <= point.X; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (labirynt[i, j] == 0)
                    {
                        labirynt[i, j] += i + 1;
                    }
                }
            }

            for (int i = point.X; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (labirynt[i, j] == 0)
                    {
                        labirynt[i, j] += N - i;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = point.Y; j < N; j++)
                {
                    if (labirynt[i, j] == 0)
                    {
                        labirynt[i, j] += N - j;
                    }
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= point.Y; j++)
                {
                    if (labirynt[i, j] == 0)
                    {
                        labirynt[i, j] += N + j;
                    }
                }
            }



        }*/
    }
}
