using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBMS.ViewModels
{
    public class UrunGoruntule
    {
        public IEnumerable<Renk> Renkler { get; set; }
        public Renk Renk { get; set; }
        
        public List<Beden> Bedenler { get; set; }
        public Beden Beden { get; set; }
        public Urun Urun { get; set; }
        public int Miktar { get; set; }
        public List<FisUrun> FisUrun { get; set; }
    }
}