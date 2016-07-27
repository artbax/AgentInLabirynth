using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using aima.logic.propositional.algorithms;
using aima.logic.propositional.parsing;

namespace ITOProjekt
{
    public struct Pole
    {
        public string nazwa { get; set; }  
        public string atrybut { get; set; }  
        public int wx { get; set; }  
        public int wy { get; set; }  
        public int wartosc { get; set; } 
    }

    public class Agent
    {

        public List<Pole> CzteryPola = new List<Pole>();
        
        public int CzujBliskosc(int[,] array, Point point)
        {
            int wartosc = 0;
            Point pkt = new Point();
            pkt = point;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == pkt.X && j == pkt.Y)
                    {
                        wartosc = array[i, j];
                    }
                }
            }

            return wartosc;
        }

        public List<Pole> ObserwujFakty(int[,] array, Point start)
        {
           
            Point polnoc = new Point(start.X, start.Y - 1);
            var pkt1 = new Pole();
            pkt1.nazwa = "Polnoc";
            pkt1.wx = polnoc.X;
            pkt1.wy = polnoc.Y;
            pkt1.wartosc = CzujBliskosc(array, polnoc);
            
            if (pkt1.wartosc == -1)
            {
                pkt1.atrybut = "mur";

            }
            else
            {
                pkt1.atrybut = "korytarz";
                
            }
            CzteryPola.Add(pkt1);


            

            Point poludnie = new Point(start.X, start.Y + 1);
            var pkt2 = new Pole();
            pkt2.nazwa = "Poludnie";
            pkt2.wx = poludnie.X;
            pkt2.wy = poludnie.Y;
            pkt2.wartosc = CzujBliskosc(array, poludnie);
            if (pkt2.wartosc == -1)
            {
                pkt2.atrybut = "mur";
                
            }
            else
            {
                pkt2.atrybut = "korytarz";
                
            }
            CzteryPola.Add(pkt2);
            

            Point wschod = new Point(start.X + 1, start.Y);
            var pkt3 = new Pole();
            pkt3.nazwa = "Wschod";
            pkt3.wx = wschod.X;
            pkt3.wy = wschod.Y;
            pkt3.wartosc = CzujBliskosc(array, wschod);
            if (pkt3.wartosc == -1)
            {
                pkt3.atrybut = "mur";
                
            }
            else
            {
                pkt3.atrybut = "korytarz";
                
            }
            CzteryPola.Add(pkt3);
           

            Point zachod = new Point(start.X - 1, start.Y);
            var pkt4 = new Pole();
            pkt4.nazwa = "Zachod";
            pkt4.wx = zachod.X;
            pkt4.wy = zachod.Y;
            pkt4.wartosc = CzujBliskosc(array, zachod);
            if (pkt4.wartosc == -1)
            {
                pkt4.atrybut = "mur";
                
            }
            else
            {
                pkt4.atrybut = "korytarz";
                
            }
            CzteryPola.Add(pkt4);
            

            

            return CzteryPola;
        }

  }
}
