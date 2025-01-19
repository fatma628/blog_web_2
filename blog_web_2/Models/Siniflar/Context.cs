using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace blog_web_2.Models.Siniflar
{
    public class Context : DbContext    
    {
        public DbSet<admin> admins { get; set; }
        public DbSet<iletisim> iletisims { get; set; }
        public DbSet<randevuEkrani> randevuEkranis { get; set; }

    }
}