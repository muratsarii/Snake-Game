using System.Collections.Generic;
using System.Diagnostics;

namespace SnakeGame
{
    public class Yilan
    {
        public event YilanHareketiHandler YilanHareketEtti;
        public event YilanHareketiHandler YilanKendisineDegdi;
        public Queue<KonsolKonum> Konumlar { get; set; }
        private KonsolKonum _mevcutKafaKonumu;
        private HareketYonleri _mevcutYon;
        private int _oyunAlaniGenisligi;
        private int _oyunAlaniYuksekligi;

        public Yilan(int oyunAlaniGenisligi,int oyunAlaniYuksekligi)
        {
            Konumlar = new Queue<KonsolKonum>();
            _oyunAlaniGenisligi = oyunAlaniGenisligi;
            _oyunAlaniYuksekligi = oyunAlaniYuksekligi;
            _mevcutYon = HareketYonleri.Saga;
            for (int i = 0; i < 4; i++)
            {
                var varsayılanNokta = new KonsolKonum()
                    {SolaOlanUzaklik = 5 + i, UsteOlanUzaklik = oyunAlaniYuksekligi / 2};
                Konumlar.Enqueue(varsayılanNokta);
                if (i == 3)
                    _mevcutKafaKonumu = varsayılanNokta;
            }
        }
        public void HareketEt()
        {
            var yeniKafaKonumu = YeniKafaKonumuYarat();
            
            var kuyruktanCikanKonum = Konumlar.Dequeue();
            CiktiysaGeriGirdir(yeniKafaKonumu);

            if (KuyrugaCarptiMi(yeniKafaKonumu))
                YilanKendisineDegdi(this, kuyruktanCikanKonum, yeniKafaKonumu);

            Konumlar.Enqueue(yeniKafaKonumu);
            _mevcutKafaKonumu = yeniKafaKonumu;

            if (YilanHareketEtti != null)
                YilanHareketEtti(this, kuyruktanCikanKonum, yeniKafaKonumu);
        }
        public void YonDegistir(HareketYonleri yon)
        {
            if(((int)_mevcutYon)%2 == ((int)yon)%2)
                return;
            _mevcutYon = yon;
        }

        public void Buyu()
        {
            var yeniKonum = YeniKafaKonumuYarat();
            this.Konumlar.Enqueue(yeniKonum);
        }
        
        private KonsolKonum YeniKafaKonumuYarat()
        {
            KonsolKonum yeniKafaKonumu = new KonsolKonum();
            switch (_mevcutYon)
            {
                case HareketYonleri.Yukari:
                    yeniKafaKonumu.SolaOlanUzaklik = _mevcutKafaKonumu.SolaOlanUzaklik;
                    yeniKafaKonumu.UsteOlanUzaklik = _mevcutKafaKonumu.UsteOlanUzaklik - 1;
                    break;
                case HareketYonleri.Asagi:
                    yeniKafaKonumu.SolaOlanUzaklik = _mevcutKafaKonumu.SolaOlanUzaklik;
                    yeniKafaKonumu.UsteOlanUzaklik = _mevcutKafaKonumu.UsteOlanUzaklik + 1;
                    break;
                case HareketYonleri.Sola:
                    yeniKafaKonumu.SolaOlanUzaklik = _mevcutKafaKonumu.SolaOlanUzaklik - 1;
                    yeniKafaKonumu.UsteOlanUzaklik = _mevcutKafaKonumu.UsteOlanUzaklik;
                    break;
                case HareketYonleri.Saga:
                    yeniKafaKonumu.SolaOlanUzaklik = _mevcutKafaKonumu.SolaOlanUzaklik + 1;
                    yeniKafaKonumu.UsteOlanUzaklik = _mevcutKafaKonumu.UsteOlanUzaklik;
                    break;
            }

            return yeniKafaKonumu;
        }
        private void CiktiysaGeriGirdir(KonsolKonum yeniKonum)
        {
            yeniKonum.SolaOlanUzaklik = yeniKonum.SolaOlanUzaklik > _oyunAlaniGenisligi ? 1 : yeniKonum.SolaOlanUzaklik;
            yeniKonum.UsteOlanUzaklik =
                yeniKonum.UsteOlanUzaklik > _oyunAlaniYuksekligi ? 1 : yeniKonum.UsteOlanUzaklik;
            yeniKonum.SolaOlanUzaklik = yeniKonum.SolaOlanUzaklik < 1  ? _oyunAlaniGenisligi : yeniKonum.SolaOlanUzaklik;
            yeniKonum.UsteOlanUzaklik =
                yeniKonum.UsteOlanUzaklik < 1 ? _oyunAlaniYuksekligi : yeniKonum.UsteOlanUzaklik;
        }

        private bool KuyrugaCarptiMi(KonsolKonum yeniKafaKonumu)
        {
            return Konumlar.Contains(yeniKafaKonumu);
        }
    }
}