using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {
        private const string yilanSembolu = "*";
        private const string yemSembolu = "o";
        private const int _oyunAlaniYukseklik = 30;
        private const int _oyunAlaniGenislik = 80;
        static void Main(string[] args)
        {
            //Console.BufferHeight = _oyunAlaniYukseklik;
            //Console.BufferWidth = _oyunAlaniGenislik;
            Console.CursorVisible = false;

            YilanOyunu oyun = new YilanOyunu(_oyunAlaniGenislik,_oyunAlaniYukseklik);
            oyun.OyunBasliyor += new OyunBasliyorHandler(YilaniVeYemiCiz);
            oyun.YilanHareketEtti += new YilanHareketiHandler(yilan_HareketEtti);
            oyun.Baslat();
            do
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        oyun.YilaniHareketEttir(HareketYonleri.Yukari);
                        break;
                    case ConsoleKey.DownArrow:
                        oyun.YilaniHareketEttir(HareketYonleri.Asagi);
                        break;
                    case ConsoleKey.LeftArrow:
                        oyun.YilaniHareketEttir(HareketYonleri.Sola);
                        break;
                    case ConsoleKey.RightArrow:
                        oyun.YilaniHareketEttir(HareketYonleri.Saga);
                        break;
                }               
            } while (true);
        }

        public static void YilaniVeYemiCiz(KonsolKonum[] yilanGovdeKonumlari, KonsolKonum yemKonumu)
        {
            foreach (var konum in yilanGovdeKonumlari)
            {
                Console.SetCursorPosition(konum.SolaOlanUzaklik,konum.UsteOlanUzaklik);
                Console.Write(yilanSembolu);
            }
            Console.SetCursorPosition(yemKonumu.SolaOlanUzaklik,yemKonumu.UsteOlanUzaklik);
            Console.Write(yemSembolu);
        }

        private static void yilan_HareketEtti(Yilan yilan, KonsolKonum kuyruk, KonsolKonum bas)
        {
            Console.SetCursorPosition(bas.SolaOlanUzaklik,bas.UsteOlanUzaklik);
            Console.Write(yilanSembolu);
            Console.SetCursorPosition(kuyruk.SolaOlanUzaklik,kuyruk.UsteOlanUzaklik);
            Console.Write(" ");
        }
    }
}

