using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjektKonradNiedzwiedzki
{
    class Dostawca
    {
        public int Width = 30;
        public int Height = 30;
        public int X, Y;
        public bool[] nrPustejPolki = new bool[] {false, false, false };
        public int wybranaPolka;
        public bool flagaRuchu = false;
        public bool dostarczyl = false;

        public Dostawca()
        {
            Thread watekStworca = new Thread(funkcjaDostawcy);
            watekStworca.IsBackground = true;
            watekStworca.Start();
            X = 620;
            Y = 60;
        }

        public void funkcjaDostawcy()
        {
            while (true)
            {
                #region
                if(Sklep.iloscProduktu[0] == 0 || Sklep.iloscProduktu[1] == 0 || Sklep.iloscProduktu[2] == 0)
                {
                    nrPustejPolki[0] = true;
                }
                else
                {
                    nrPustejPolki[0] = false;
                }

                if (Sklep.iloscProduktu[3] == 0 || Sklep.iloscProduktu[4] == 0 || Sklep.iloscProduktu[5] == 0)
                {
                    nrPustejPolki[1] = true;
                }
                else
                {
                    nrPustejPolki[1] = false;
                }

                if (Sklep.iloscProduktu[6] == 0 || Sklep.iloscProduktu[7] == 0 || Sklep.iloscProduktu[8] == 0)
                {
                    nrPustejPolki[2] = true;
                }
                else
                {
                    nrPustejPolki[2] = false;
                }
                #endregion // Określenie braków w zasobach // Określenie braków w zasobach

                Random losujPolke = new Random();                 
                wybranaPolka = losujPolke.Next(1, 4);

                switch (wybranaPolka)
                {
                    case 1:
                        if (nrPustejPolki[0] == true)
                            ruchDoPierwszejPolki();
                    break;

                    case 2:
                        if (nrPustejPolki[1] == true)
                            ruchDoDrugiejPolki(); 
                    break;

                    case 3:
                        if (nrPustejPolki[2] == true)
                            ruchDoTrzeciejPolki();
                    break;
                }           
            }
        }


        void ruchDoPierwszejPolki()
        {
            flagaRuchu = true;

            while (flagaRuchu == true)
            {       
                #region
                if (X > 350 && dostarczyl == false)
                {
                    X--;
                }
                if(X == 350 && Y< 230 && dostarczyl == false)
                {
                    Y++;
                }
                #endregion

                if (X == 350 && Y == 230 && dostarczyl == false)
                {
                    if(dostarczyl == false)
                    {
                        dostarczyl = true;
                        Sklep.semaforDostawyI.WaitOne();
                        dostawaI();
                        Sklep.semaforDostawyI.Release();
                    }                      
                }

                #region
                if (X == 350 && Y > 60 && dostarczyl == true)
                {
                    Y--;
                }

                if(Y == 60 && X < 620 && dostarczyl == true)
                {
                    X++;
                }

                if(dostarczyl == true && X == 620 && Y == 60)
                {
                    flagaRuchu = false;
                    dostarczyl = false;
                }
                #endregion

                Thread.Sleep(5);
            }
        }


        void ruchDoDrugiejPolki()
        {
            flagaRuchu = true;

            while (flagaRuchu == true)
            {
                #region
                if (dostarczyl == false && Y < 230)
                    Y++;
                #endregion

                if ( Y == 230)
                {
                    if(dostarczyl == false)
                    {
                        dostarczyl = true;
                        Sklep.semaforDostawyII.WaitOne();
                        dostawaII();
                        Sklep.semaforDostawyII.Release();
                    }
                }

                #region
                if (Y > 60 && dostarczyl == true)
                {
                    Y--;
                }

                if(Y == 60 && dostarczyl == true)
                {
                    flagaRuchu = false;
                    dostarczyl = false;
                }
                #endregion

                Thread.Sleep(5);
            }
        }


        void ruchDoTrzeciejPolki()
        {
            flagaRuchu = true;

            while (flagaRuchu == true)
            {
                #region
                if (X < 890 && dostarczyl == false)
                {
                    X++;
                }
                if (X == 890 && Y < 230 && dostarczyl == false)
                {
                    Y++;
                }
                #endregion

                if (X == 890 && Y == 230 && dostarczyl == false)
                {
                    if (dostarczyl == false)
                    {
                        dostarczyl = true;
                        Sklep.semaforDostawyIII.WaitOne();
                        dostawaIII();
                        Sklep.semaforDostawyIII.Release();
                    }
                }

                #region
                if (X == 890 && Y > 60 && dostarczyl == true)
                {
                    Y--;
                }

                if (Y == 60 && X > 620 && dostarczyl == true)
                {
                    X--;
                }

                if (dostarczyl == true && X == 620 && Y == 60)
                {
                    flagaRuchu = false;
                    dostarczyl = false;
                }
                #endregion

                Thread.Sleep(5);
            }
        }


        void dostawaI()
        {
            Sklep.sPolkaI = true;
            int a = 1000;
            if(Sklep.iloscProduktu[0] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[0] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[0] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[0] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[0] += 2;
                Thread.Sleep(a);
            }
            else if(Sklep.iloscProduktu[1] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[1] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[1] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[1] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[1] += 2;
                Thread.Sleep(a);
            }
            else if(Sklep.iloscProduktu[2] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[2] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[2] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[2] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[2] += 2;
                Thread.Sleep(a);
            }
            Sklep.sPolkaI = false;
        }


        void dostawaII()
        {
            Sklep.sPolkaII = true;
            int a = 1000;
            if (Sklep.iloscProduktu[3] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[3] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[3] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[3] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[3] += 2;
                Thread.Sleep(a);
            }
            else if (Sklep.iloscProduktu[4] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[4] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[4] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[4] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[4] += 2;
                Thread.Sleep(a);
            }
            else if (Sklep.iloscProduktu[5] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[5] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[5] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[5] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[5] += 2;
                Thread.Sleep(a);
            }
            Sklep.sPolkaII = false;
        }


        void dostawaIII()
        {
            Sklep.sPolkaIII = true;
            int a = 1000;
            if (Sklep.iloscProduktu[6] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[6] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[6] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[6] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[6] += 2;
                Thread.Sleep(a);
            }
            else if (Sklep.iloscProduktu[7] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[7] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[7] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[7] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[7] += 2;
                Thread.Sleep(a);
            }
            else if (Sklep.iloscProduktu[8] == 0)
            {
                Thread.Sleep(a);
                Sklep.iloscProduktu[8] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[8] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[8] += 2;
                Thread.Sleep(a);
                Sklep.iloscProduktu[8] += 2;
                Thread.Sleep(a);
            }
            Sklep.sPolkaIII = false;
        }
    }
}