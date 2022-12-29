using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBMS.ViewModels
{
    public class KullaniciViewModel
    {
        public Kullanici Kullanici { get; set; }
        public List<Email> Emailler { get; set; }
        public List<KrediKarti> KrediKartlari { get; set; }
        public List<Adres> Adresler { get; set; }


    }
}