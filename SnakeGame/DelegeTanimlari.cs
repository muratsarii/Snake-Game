using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public delegate void YilanHareketiHandler(Yilan yilan, KonsolKonum kuyrukSonu, KonsolKonum yilanBasi);

    public delegate void OyunBittiHandler(YilanOyunu oyun);

    public delegate void YeminYeriDegistiHandler(Yem yem);

    public delegate void OyunBasliyorHandler(KonsolKonum[] yilaninGovdeKonumlari, KonsolKonum yemKonumu);
}
