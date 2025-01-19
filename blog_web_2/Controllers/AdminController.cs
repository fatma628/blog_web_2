using blog_web_2.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog_web_2.Controllers
{
    public class AdminController : Controller
    {
        // Admin paneline yönlendirme
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                // Kullanıcı giriş yapmadıysa login sayfasına yönlendir
                return RedirectToAction("Login");
            }

            List<iletisim> iletisimler = GetIletisimList();
            return View(iletisimler); // Iletisimler listesini view'e gönderiyoruz
        }

        // Login sayfası
        public ActionResult Login()
        {
            return View();
        }

        // Giriş kontrolü yapma
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            // Veritabanı bağlantı dizesi
            string connectionString = @"Server=DESKTOP-BEHG9E8\SQLEXPRESS;Database=BlogDB;Integrated Security=True;";

            // Kullanıcı doğrulama
            bool isValidUser = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kullanıcı adı ve şifreyi kontrol eden SQL sorgusu
                    string query = "SELECT COUNT(*) FROM admins WHERE Kullanici = @username AND Sifre = @password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametreler ekleniyor
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Sonuç alınıyor
                        int userCount = (int)cmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            // Kullanıcı mevcutsa giriş başarılı
                            isValidUser = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya başka işlemler yapılabilir
                ViewBag.Message = "Bir hata oluştu: " + ex.Message;
                return View();
            }

            if (isValidUser)
            {
                // Giriş başarılı, kullanıcıyı session'a kaydediyoruz
                Session["User"] = username;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Geçersiz kullanıcı adı veya şifre";
                return View();
            }
        }

        // Veritabanından iletisimleri çekme
        private List<iletisim> GetIletisimList()
        {
            string connectionString = @"Server=DESKTOP-BEHG9E8\SQLEXPRESS;Database=BlogDB;Integrated Security=True;";
            List<iletisim> iletisimler = new List<iletisim>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM iletisims"; // iletisims tablosundaki tüm verileri al
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Verileri modele ekle
                            iletisim iletisim = new iletisim
                            {
                                ID = Convert.ToInt32(reader["Id"]),
                                AdSoyad = reader["AdSoyad"].ToString(),
                                Mail = reader["Mail"].ToString(),
                                Konu = reader["Konu"].ToString(),
                                Tel = reader["Tel"].ToString()
                            };
                            iletisimler.Add(iletisim); // Listeye ekle
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda geri bildirim ver
            }

            return iletisimler;
        }

        // Çıkış yapma
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login");
        }
    }
}
