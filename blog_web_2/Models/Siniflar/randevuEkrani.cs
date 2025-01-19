using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blog_web_2.Models.Siniflar
{
    public class randevuEkrani
    {
        [Key]
        public int ID { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string mail { get; set; }
        public string tel { get; set; }
        public DateTime randevubilgi { get; set; }
    }
}