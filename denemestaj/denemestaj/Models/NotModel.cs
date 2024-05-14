using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denemestaj.Models
{
    public class NotModel
    {
        public int ID { get; set; }
        public int OgrID { get; set; }
        public int DersID { get; set; }
        public string OgrenciAdi { get; set; }
        public double Sinav1 { get; set; }
        public double Sinav2 { get; set; }
        public double Sinav3 { get; set; }
        public double Sozlu1 { get; set; }
        public double Sozlu2 { get; set; }
        public double NotOrtalamasi { get; set; }
        public string SonucDegerlendirme { get; set; }


    }
}
