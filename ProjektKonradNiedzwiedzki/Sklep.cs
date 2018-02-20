using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektKonradNiedzwiedzki
{
    public partial class Sklep : Form
    {
        List<Klienci> listaKlientow;
        public static int[] iloscProduktu = new int[] { 1, 0, 1, 1, 0, 1, 0, 4, 6 }; 
        Semaphore semaforListyKlientow;
        public static Semaphore semaforDostawyI, semaforDostawyII, semaforDostawyIII;
        public static bool sPolkaI = false;
        public static bool sPolkaII = false;
        public static bool sPolkaIII = false;

        public static bool[] polkaWolna = new bool [] { true, true, true };
        Dostawca dostawca = new Dostawca();
        //Funkcje Visual

        public Sklep()
        {
            InitializeComponent();

            listaKlientow = new List<Klienci>();

            semaforListyKlientow = new Semaphore(1, 1);
            semaforDostawyI = new Semaphore(1, 1);
            semaforDostawyII = new Semaphore(1, 1);
            semaforDostawyIII = new Semaphore(1, 1);

            Thread watekStworca = new Thread(dodajKlienta);
            watekStworca.IsBackground = true;
            watekStworca.Start();
        }

        private void Sklep_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void Sklep_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Font drawFont = new Font("Arial", 10, FontStyle.Bold);

            //Rysowanie wszystkich klientów
            semaforListyKlientow.WaitOne();        
            foreach (Klienci obiektKlient in listaKlientow)
            {
                if (obiektKlient.rodzajKlienta == 1)
                {
                    e.Graphics.FillRectangle(Brushes.Blue, obiektKlient.X, obiektKlient.Y, obiektKlient.Width, obiektKlient.Height);
                }
                else if (obiektKlient.rodzajKlienta == 2)
                {
                    e.Graphics.FillRectangle(Brushes.DarkRed, obiektKlient.X, obiektKlient.Y, obiektKlient.Width, obiektKlient.Height);
                }
           }

           if (sPolkaI == true) e.Graphics.DrawString("Blokada! Dostawa realizowana!", drawFont, Brushes.Red, 255, 545);
           if (sPolkaII == true) e.Graphics.DrawString("Blokada! Dostawa realizowana!", drawFont, Brushes.Red, 540, 545);
           if (sPolkaIII == true) e.Graphics.DrawString("Blokada! Dostawa realizowana!", drawFont, Brushes.Red, 820, 545);

           e.Graphics.FillEllipse(Brushes.DarkKhaki, dostawca.X, dostawca.Y, dostawca.Width, dostawca.Height);

           // e.Graphics.FillRectangle(Brushes.DarkGreen, 410, 700, 40, 40);

           semaforListyKlientow.Release();

            //Rysowanie slupkow produktow
           #region
           e.Graphics.FillRectangle(Brushes.DarkRed, 280, 300, iloscProduktu[0] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[0], DefaultFont , Brushes.Red, 260, 310);
           e.Graphics.FillRectangle(Brushes.DarkRed, 280, 400, iloscProduktu[1] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[1], DefaultFont, Brushes.Red, 260, 410);
           e.Graphics.FillRectangle(Brushes.DarkRed, 280, 500, iloscProduktu[2] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[2], DefaultFont, Brushes.Red, 260, 510);

           e.Graphics.FillRectangle(Brushes.DarkRed, 555, 300, iloscProduktu[3] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[3], DefaultFont, Brushes.Red, 535, 310);
           e.Graphics.FillRectangle(Brushes.DarkRed, 555, 400, iloscProduktu[4] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[4], DefaultFont, Brushes.Red, 535, 410);
           e.Graphics.FillRectangle(Brushes.DarkRed, 555, 500, iloscProduktu[5] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[5], DefaultFont, Brushes.Red, 535, 510);

           e.Graphics.FillRectangle(Brushes.DarkRed, 840, 300, iloscProduktu[6] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[6], DefaultFont, Brushes.Red, 820, 310);
           e.Graphics.FillRectangle(Brushes.DarkRed, 840, 400, iloscProduktu[7] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[7], DefaultFont, Brushes.Red, 820, 410);
           e.Graphics.FillRectangle(Brushes.DarkRed, 840, 500, iloscProduktu[8] * 20, 30); e.Graphics.DrawString("" + iloscProduktu[8], DefaultFont, Brushes.Red, 820, 510);
           #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        // Funkcje własne//***************************************************************************************//

        void dodajKlienta()
        {
            //Funkcje losujące
            Random losowaniePolki = new Random();
            Random losujPolke = new Random();
            int wybor;
            
            while (true)
            {
                semaforListyKlientow.WaitOne();
                wybor = losujPolke.Next(1, 4);

                if (polkaWolna[0] == true && wybor == 1)
                {
                    dodajKlientaII(1);
                }
              
                if (polkaWolna[1] == true && wybor == 2)
                {
                    dodajKlientaII(2);
                }
                
                if (polkaWolna[2] == true && wybor == 3)
                {
                    dodajKlientaII(3);
                }
                    
                semaforListyKlientow.Release();
                Thread.Sleep(3000);
            }
        }

        private void dodajKlientaII(int nrPolki)
        {
            Klienci obiektKlient;
            obiektKlient = new Klienci(nrPolki);
            listaKlientow.Add(obiektKlient);
            Thread watekKlienta = new Thread(obiektKlient.Incicjuj);
            watekKlienta.IsBackground = true;
            watekKlienta.Start();
        }
    }
}