using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace blog_web_2.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [HttpPost]
        public ActionResult SaveContact(string contact_adsoyad, string contact_mail, string contact_konu,string contact_tel)
        {
            // Connection string'i Web.config'den al
            string connectionString = @"Server=DESKTOP-BEHG9E8\SQLEXPRESS;Database=BlogDB;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Veritabanına kaydetme komutu
                    string query = "INSERT INTO iletisims (AdSoyad, Mail, Konu,Tel) VALUES (@AdSoyad, @Mail, @Konu,@Tel)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdSoyad", contact_adsoyad);
                        cmd.Parameters.AddWithValue("@Mail", contact_mail);
                        cmd.Parameters.AddWithValue("@Konu", contact_konu);
                        cmd.Parameters.AddWithValue("@Tel", contact_tel);

                        cmd.ExecuteNonQuery(); // Komutu çalıştır
                    }
                }

                // Başarılı işlem sonrası ana sayfaya yönlendirme
                return RedirectToAction("Contact", "Default"); // HomeController'daki Index aksiyonuna yönlendir
            }
            catch (Exception ex)
            {
                // Hata durumunda ana sayfaya yönlendirme
                return RedirectToAction("Contact", "Default"); // Hata olsa bile ana sayfaya yönlendirilir
            }
        }
    }
}
