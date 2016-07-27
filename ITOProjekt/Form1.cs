using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using aima.logic.propositional.algorithms;
using aima.logic.propositional.parsing;
using System.Threading;

namespace ITOProjekt
{
    public partial class Form1 : Form
    {
        Labirynt labirynt = new Labirynt();
        Agent szpieg = new Agent();
        PEParser parser = new PEParser();
        PLFCEntails plfce = new PLFCEntails();
        KnowledgeBase KB = new KnowledgeBase();
        
        public static int N;
        public int[,] array = new int[N,N];

        public Form1()
        {
            InitializeComponent();
            
        }

        private bool ASK(KnowledgeBase baza, string s)
        {
            return plfce.plfcEntails(baza, s);
        }

        private void TELL(KnowledgeBase baza, string s)
        {
                 baza.tell(s);
        }

        private void render(int[,] array)
        {
            pictureBox1.Image = labirynt.Draw(620, 620, array);
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            int N = 9;
            array = labirynt.GenerujLabirynt(N);
            render(array);
            button5.Enabled = true;
            button7.Enabled = true;
            clearText();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int N = 11;
            array = labirynt.GenerujLabirynt(N);
            render(array);
            button5.Enabled = true;
            button7.Enabled = true;
            clearText();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int N = 13;
            array = labirynt.GenerujLabirynt(N);
            render(array);
            button5.Enabled = true;
            button7.Enabled = true;
            clearText();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int N = 15;
            array = labirynt.GenerujLabirynt(N);
            render(array);
            button5.Enabled = true;
            button7.Enabled = true;
            clearText();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            backgroundWorker1.RunWorkerAsync();


            
        }
        
        private void button7_Click(object sender, EventArgs e)  // Stop
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            backgroundWorker1.CancelAsync();
            
         }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Point spy = labirynt.ZnajdzPunkt(array, -2);
            Point wayout = labirynt.ZnajdzPunkt(array, 100);
            Point next = new Point();

            while (array[spy.X, spy.Y] != array[wayout.X, wayout.Y])
            {
                
                List<Pole> lista = szpieg.ObserwujFakty(array, spy);
                int h = 1;
                for (int i = 0; i < lista.Count; i++)
                {
                    if ((lista[i].wartosc >= h) && (lista[i].wartosc != -1))
                    {
                        h = lista[i].wartosc;
                    }

                }

                this.richTextBox2.Invoke(new MethodInvoker(delegate
                {
                    richTextBox2.Text = "Północ: " + lista[0].atrybut + ", bliskość: " + lista[0].wartosc + "\n"
                        + "Południe: " + lista[1].atrybut + ", bliskość: " + lista[1].wartosc + "\n"
                        + "Zachód: " + lista[2].atrybut + ", bliskość: " + lista[2].wartosc + "\n"
                        + "Wschód: " + lista[3].atrybut + ", bliskość: " + lista[3].wartosc + "\n";

                }));

                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].atrybut == "korytarz")
                    {
                        if (lista[i].nazwa == "Polnoc")
                        {
                            TELL(KB, "korytarz");
                            TELL(KB, "(Polnoc)");
                            TELL(KB, "(Polnoc) AND (korytarz)");
                            TELL(KB, "((Polnoc AND korytarz) => IdzNaPolnoc)");
                            this.richTextBox1.Invoke(new MethodInvoker(delegate
                            {
                                richTextBox1.Text += "(korytarz)" + "\n"
                                    + "(Polnoc)" + "\n"
                                    + "(Polnoc) AND (korytarz)" + "\n"
                                    + "((Polnoc AND korytarz) => IdzNaPolnoc)" + "\n";

                            }));
                        }
                        else if (lista[i].nazwa == "Poludnie")
                        {
                            TELL(KB, "korytarz");
                            TELL(KB, "(Poludnie)");
                            TELL(KB, "(Poludnie) AND (korytarz)");
                            TELL(KB, "((Poludnie AND korytarz) => IdzNaPoludnie)");
                            this.richTextBox1.Invoke(new MethodInvoker(delegate
                            {
                                richTextBox1.Text += "(korytarz)" + "\n"
                                    + "(Poludnie)" + "\n"
                                    + "(Poludnie) AND (korytarz)" + "\n"
                                    + "((Poludnie AND korytarz) => IdzNaPoludnie)" + "\n";

                            }));
                        }
                        else if (lista[i].nazwa == "Zachod")
                        {
                            TELL(KB, "korytarz");
                            TELL(KB, "(Zachod)");
                            TELL(KB, "(Zachod) AND (korytarz)");
                            TELL(KB, "((Zachod AND korytarz) => IdzNaZachod)");
                            this.richTextBox1.Invoke(new MethodInvoker(delegate
                            {
                                richTextBox1.Text += "(korytarz)" + "\n"
                                    + "(Zachod)" + "\n"
                                    + "(Zachod) AND (korytarz)" + "\n"
                                    + "((Zachod AND korytarz) => IdzNaZachod)" + "\n";

                            }));
                        }
                        else
                        {
                            TELL(KB, "korytarz");
                            TELL(KB, "(Wschod)");
                            TELL(KB, "(Wschod) AND (korytarz)");
                            TELL(KB, "((Wschod AND korytarz) => IdzNaWschod)");
                            this.richTextBox1.Invoke(new MethodInvoker(delegate
                            {
                                richTextBox1.Text += "(korytarz)" + "\n"
                                    + "(Wschod)" + "\n"
                                    + "(Wschod) AND (korytarz)" + "\n"
                                    + "((Wschod AND korytarz) => IdzNaWschod)" + "\n";

                            }));
                        }
                    }


                }


                if ((ASK(KB, "IdzNaPolnoc") == true) && (lista[0].wartosc == h))
                {
                    next = new Point(lista[0].wx, lista[0].wy);
                    this.richTextBox3.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox3.Text = "Wniosek: " + "IDŹ NA PÓŁNOC" + "\n";

                    }));


                }
                else if ((ASK(KB, "IdzNaPoludnie") == true) && (lista[1].wartosc == h))
                {
                    next = new Point(lista[1].wx, lista[1].wy);
                    this.richTextBox3.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox3.Text = "Wniosek: " + "IDŹ NA POŁUDNIE" + "\n";

                    }));



                }
                else if ((ASK(KB, "IdzNaWschod") == true) && (lista[2].wartosc == h))
                {
                    next = new Point(lista[2].wx, lista[2].wy);
                    this.richTextBox3.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox3.Text = "Wniosek: " + "IDŹ NA WSCHÓD" + "\n";

                    }));



                }
                else if ((ASK(KB, "IdzNaZachod") == true) && (lista[3].wartosc == h))
                {
                    next = new Point(lista[3].wx, lista[3].wy);
                    this.richTextBox3.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox3.Text = "Wniosek: " + "IDŹ NA ZACHÓD" + "\n";

                    }));

                }
                

                Thread.Sleep(1000);


               

                Random r = new Random();
                int[,] temptab = new int[N, N];
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if ((i == spy.X) && (j == spy.Y))
                        {
                            temptab = heurystyka(array, wayout);
                            int temp = temptab[next.X, next.Y] - 2;
                            array[i, j] = temp;//array[spy.X, spy.Y] + Math.Abs(i - wayout.X) + Math.Abs(j - wayout.Y);
                            array[next.X, next.Y] = -2;
                            
                        }

                    }
                }

                lista.Clear();
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                render(array);
                spy = next;
            }
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
               if (e.Cancelled)
               {              
                  MessageBox.Show("Po naciśnięciu OK wybierz START, aby kontynuować, lub wybierz nowy labirynt");
               }
               else
               {

	             MessageBox.Show("Osiągnąłeś EXIT!");
                 button1.Enabled = true;
                 button2.Enabled = true;
                 button3.Enabled = true;
                 button4.Enabled = true;
                 button5.Enabled = false;
                 button7.Enabled = false;

	           }
        }

        private int[,] heurystyka(int[,] labirynt, Point point)
        {
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
                    labirynt[i, j] += N - j;
                    
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= point.Y; j++)
                {
                    labirynt[i, j] += N + j;
                    
                }
            }


            return labirynt;
        }

        private void clearText()
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button7.Enabled = false;
            clearText();

        }

        
    }
}
