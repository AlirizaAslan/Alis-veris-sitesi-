using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBMS.ViewModels
{
    public class SepetViewModel
    {
        public List<Sepet> Sepettekiler { get; set; }
        public List<Adres> Adresler { get; set; }
        public List<KrediKarti> KrediKartlari { get; set; }
        public Fis Fis { get; set; }
        public List<Email> Emailler { get; set; }
    }
}