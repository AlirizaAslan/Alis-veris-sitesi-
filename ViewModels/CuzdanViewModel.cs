using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBMS.ViewModels
{
    public class CuzdanViewModel
    {
        public Musteri Musteri { get; set; }
        public Kullanici Kullanici { get; set; }
        public List<KrediKarti> KrediKartlari { get; set; }
    }
}