using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjektKonradNiedzwiedzki
{
    class Klienci
    {
        public int nrPolki;
        public int nrProduktu = 0;
        public int rodzajKlienta = 0;
        public int Width = 40;
        public int Height = 40;
        public int X = 980, Y = 830;
        bool czyKupione = false;

        public Klienci(int nrPolki)
        {
            this.nrPolki = nrPolki;
        }

        public void Incicjuj()
        {
            Random losujNrProduktu = new Random();
            Random losujRodzajKlienta = new Random(); // 1 - ograniczona cierpliwosc ; 2 - zdeterminowany
            nrProduktu = losujNrProduktu.Next(1, 4);
            rodzajKlienta = losujRodzajKlienta.Next(1, 3);

            switch (nrPolki)
            {
                case 1:
                    X = 410;
                    Y = 830;
                    Sklep.polkaWolna[0] = false;
                    drogaPierwsza();
                    break;

                case 2:
                    X = 690;
                    Y = 830;
                    Sklep.polkaWolna[1] = false;
                    drogaDruga();
                    break;

                case 3:                    
                    X = 980;
                    Y = 830;
                    Sklep.polkaWolna[2] = false;
                    drogaTrzecia();
                    break;               
            }
        }

        void drogaPierwsza()
        {
            while (Y <= 830)
            {
                #region
                if (czyKupione == false && Y > 580)
                    Y--;

                if (czyKupione == false && Y == 580 && X > 330)
                    X--;
                #endregion

                if (Y == 580 && X == 330)
                {
                    if (!czyKupione)
                    {
                        if (Sklep.iloscProduktu[nrProduktu - 1] > 0)
                        {
                            Thread.Sleep(1000);
                            Sklep.semaforDostawyI.WaitOne();
                            Sklep.iloscProduktu[nrProduktu - 1]--;
                            Sklep.semaforDostawyI.Release();
                            Thread.Sleep(1000);
                            czyKupione = true;
                        }
                        else if (rodzajKlienta == 1)
                        {
                            Thread.Sleep(1000);

                            if (Sklep.iloscProduktu[nrProduktu - 1] > 0)
                            {
                                Sklep.semaforDostawyI.WaitOne();
                                Sklep.iloscProduktu[nrProduktu - 1]--;
                                Sklep.semaforDostawyI.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                Thread.Sleep(10000);
                                if (Sklep.iloscProduktu[nrProduktu - 1] > 0)
                                {
                                    Sklep.semaforDostawyI.WaitOne();
                                    Sklep.iloscProduktu[nrProduktu - 1]--;
                                    Sklep.semaforDostawyI.Release();
                                }
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                        }
                        else if (rodzajKlienta == 2)
                        {
                            Thread.Sleep(1000);
                            if (Sklep.iloscProduktu[nrProduktu - 1] > 0)
                            {
                                Sklep.semaforDostawyI.WaitOne();
                                Sklep.iloscProduktu[nrProduktu - 1]--;
                                Sklep.semaforDostawyI.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                while (Sklep.iloscProduktu[nrProduktu - 1] == 0) {; }
                                Sklep.semaforDostawyI.WaitOne();
                                Sklep.iloscProduktu[nrProduktu - 1]--;
                                Sklep.semaforDostawyI.Release();
                                czyKupione = true;
                            }
                        }
                    }
                }

                #region
                if (czyKupione == true && Y == 580 && X > 250)
                    X--;

                if (X == 250 && czyKupione == true)
                    Y++;

                Thread.Sleep(5);
            }
            Width = 0;
            Height = 0;
            Sklep.polkaWolna[0] = true;
            #endregion
        }


        void drogaDruga()
        {
            while (Y <= 830)
            {
                #region
                if (czyKupione == false && Y > 580)
                    Y--;

                if (czyKupione == false && Y == 580 && X > 610)
                    X--;
                #endregion

                if (Y == 580 && X == 610)
                {
                    if (!czyKupione)
                    {
                        if (Sklep.iloscProduktu[2 + nrProduktu] > 0)
                        {
                            Thread.Sleep(1000);

                            Sklep.semaforDostawyII.WaitOne();
                            Sklep.iloscProduktu[2 + nrProduktu]--;
                            Sklep.semaforDostawyII.Release();
                            Thread.Sleep(1000);
                            czyKupione = true;
                        }
                        else if (rodzajKlienta == 1)
                        {
                            Thread.Sleep(1000);

                            if (Sklep.iloscProduktu[2 + nrProduktu] > 0)
                            {
                                Sklep.semaforDostawyII.WaitOne();
                                Sklep.iloscProduktu[2 + nrProduktu]--;
                                Sklep.semaforDostawyII.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                Thread.Sleep(10000);
                                if (Sklep.iloscProduktu[2 + nrProduktu] > 0)
                                {
                                    Sklep.semaforDostawyII.WaitOne();
                                    Sklep.iloscProduktu[2 + nrProduktu]--;
                                    Sklep.semaforDostawyII.Release();
                                }
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                        }
                        else if (rodzajKlienta == 2)
                        {
                            Thread.Sleep(1000);

                            if (Sklep.iloscProduktu[2 + nrProduktu] > 0)
                            {
                                Sklep.semaforDostawyII.WaitOne();
                                Sklep.iloscProduktu[2 + nrProduktu]--;
                                Sklep.semaforDostawyII.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                while (Sklep.iloscProduktu[2 + nrProduktu] == 0) {; }
                                Sklep.semaforDostawyII.WaitOne();
                                Sklep.iloscProduktu[2 + nrProduktu]--;
                                Sklep.semaforDostawyII.Release();
                                czyKupione = true;
                            }
                        }
                    }
                }

                #region
                if (czyKupione == true && Y == 580 && X > 550)
                    X--;

                if (X == 550 && czyKupione == true)
                    Y++;

                Thread.Sleep(5);
            }

            Width = 0;
            Height = 0;
            Sklep.polkaWolna[1] = true;
            #endregion
        }


        void drogaTrzecia()
        {
            while (Y <= 830)
            {
                #region
                if (czyKupione == false && Y > 580)
                    Y--;

                if (czyKupione == false && Y == 580 && X > 900)
                    X--;
                #endregion

                if (Y == 580 && X == 900)
                {
                    if (!czyKupione)
                    {
                        if (Sklep.iloscProduktu[5 + nrProduktu] > 0)
                        {
                            Thread.Sleep(1000);
                            Sklep.semaforDostawyIII.WaitOne();
                            Sklep.iloscProduktu[5 + nrProduktu]--;
                            Sklep.semaforDostawyIII.Release();
                            Thread.Sleep(1000);
                            czyKupione = true;
                        }
                        else if (rodzajKlienta == 1)
                        {
                            Thread.Sleep(1000);

                            if (Sklep.iloscProduktu[5 + nrProduktu] > 0)
                            {
                                Sklep.semaforDostawyIII.WaitOne();
                                Sklep.iloscProduktu[5 + nrProduktu]--;
                                Sklep.semaforDostawyIII.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                Thread.Sleep(10000);
                                if (Sklep.iloscProduktu[5 + nrProduktu] > 0)
                                {
                                    Sklep.semaforDostawyIII.WaitOne();
                                    Sklep.iloscProduktu[5 + nrProduktu]--;
                                    Sklep.semaforDostawyIII.Release();
                                }

                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                        }
                        else if (rodzajKlienta == 2)
                        {
                            Thread.Sleep(1000);

                            if (Sklep.iloscProduktu[5 + nrProduktu] > 0)
                            {
                                Sklep.semaforDostawyIII.WaitOne();
                                Sklep.iloscProduktu[5 + nrProduktu]--;
                                Sklep.semaforDostawyIII.Release();
                                Thread.Sleep(1000);
                                czyKupione = true;
                            }
                            else
                            {
                                while (Sklep.iloscProduktu[5 + nrProduktu] == 0) {; }
                                Sklep.semaforDostawyIII.WaitOne();
                                Sklep.iloscProduktu[5 + nrProduktu]--;
                                Sklep.semaforDostawyIII.Release();
                                czyKupione = true;
                            }
                        }
                    }
                }

                #region
                if (czyKupione == true && Y == 580 && X > 835)
                    X--;

                if (X == 835 && czyKupione == true)
                    Y++;

                Thread.Sleep(5);
            }

            Width = 0;
            Height = 0;
            Sklep.polkaWolna[2] = true;
            #endregion
        }
    }
}
