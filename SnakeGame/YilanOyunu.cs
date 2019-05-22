using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace SnakeGame
{
    public class YilanOyunu
    {
        public event OyunBasliyorHandler OyunBasliyor;
        public event OyunBittiHandler OyunBitti;
        public event YilanHareketiHandler YilanHareketEtti;
        public event YeminYeriDegistiHandler YeminYeriDegisti;

        private Yem _aktifYem;
        private Yilan _yilan;
        private YemUretici _yemUretici;
        private int _puan;
        private int _oyunAlaniGenisligi;
        private int _oyunAlaniYüksekligi;
        public int Puan {get { return _puan; }}

        public YilanOyunu(int oyunAlanıGenislik,int oyunAlanıYukseklik)
        {
            _oyunAlaniGenisligi = oyunAlanıGenislik;
            _oyunAlaniYüksekligi = oyunAlanıYukseklik;
        }
        public void Baslat()
        {
            _yilan = new Yilan(_oyunAlaniGenisligi, _oyunAlaniYüksekligi);
            _yemUretici = new YemUretici(_oyunAlaniYüksekligi, _oyunAlaniGenisligi);
            _aktifYem = _yemUretici.YeniYemUret();
            _puan = 0;
            _yilan.YilanHareketEtti += new YilanHareketiHandler(_yilan_hareketEtti);
            _yilan.YilanKendisineDegdi += new YilanHareketiHandler(_yilan_KendisineDegdi);
            OyunBasliyor(_yilan.Konumlar.ToArray(), _aktifYem.Konum);
        }


        public void YilaniHareketEttir(HareketYonleri yon)
        {
            _yilan.YonDegistir(yon);
            _yilan.HareketEt();
        }

        private void _yilan_hareketEtti(Yilan yilan, KonsolKonum kuyrukSonu, KonsolKonum yilanBasi)
        {
            YilanHareketEtti(yilan, kuyrukSonu, yilanBasi);
        }
        private void _yilan_KendisineDegdi(Yilan yilan, KonsolKonum kuyrukSonu, KonsolKonum yilanBasi)
        {

        }
    }
}