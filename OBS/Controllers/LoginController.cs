using OBS.Models;
using OBS.Models.UserModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace OBS.Controllers
{
    public class LoginController : Controller
    {
        private ogrencisistemiEntities context;
        // GET: Login
        public ActionResult Index()
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login != null)
            {
                if (login.student_id != null)
                {
                    return Redirect("https://localhost:44317/Grades/Info");
                }
                else if (login.teacher_id != null)
                {
                    if (login.employee.position.position_name == "officer")
                    {
                        return Redirect("https://localhost:44317/Register/Officer");
                    }
                    return Redirect("https://localhost:44317/Teacher/Teacher");
                }
               
            }
            return View("Login");
        }

        public ActionResult Login(UserModel userModel)
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login != null)
            {
                MessageBox.Show("Tekrar giriş yapmadan önce lütfen çıkış yapın", "Bilgilendirme Penceresi");
                return Redirect("https://localhost:44317/");
            }
            if (userModel.Password == null || userModel.Username == null)
            {
                MessageBox.Show("Kullanıcı adı ve şifre kısmını lütfen doldurunuz", "Bilgilendirme Penceresi");
                return Redirect("https://localhost:44317/Login/Index");
            }
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(userModel.Password);
            var sha1data = sha1.ComputeHash(data);
            var bilgiler = context.students.FirstOrDefault(x => x.student_no == userModel.Username && x.student_password == sha1data);
            if (bilgiler != null)
            {
                MessageBox.Show("Giriş Başarılı!", "Bilgilendirme Penceresi");
                Setlogin1True(userModel, context);
                return Redirect("https://localhost:44317/Grades/Info");
            }
            else
            {

                var ogretmenkontrol = context.employee.FirstOrDefault(x => x.employee_tckn == userModel.Username && x.employee_password == data);

                if (ogretmenkontrol != null)
                {
                    Setlogin1True(userModel, context);
                    var isTeacher = false;
                    var logged = context.login.FirstOrDefault(x => x.login1 == true);
                    foreach (employee employee in context.employee.ToList())
                    {

                        if (logged.teacher_id == employee.id)
                        {
                            if (employee.position.position_name == "teacher")
                            {
                                isTeacher = true;
                            }
                        }
                    }
                    if (!isTeacher)
                    {
                        MessageBox.Show("Giriş Başarılı!", "Bilgilendirme Penceresi");
                        return Redirect("https://localhost:44317/Register/Officer"); //görevli ekranı gelicek ayrıca logincontrollerdaki öğretmen kontrolü düzeltilicek bi teachera yolluyo bi logine
                    }
                    MessageBox.Show("Giriş Başarılı!", "Bilgilendirme Penceresi");
                    return Redirect("https://localhost:44317/Teacher/Teacher"); //OGRETMEN SAYFASI GELDİĞİNDE BURAYA EKLENİCEK
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız!", "Bilgilendirme Penceresi");
                    return Redirect("https://localhost:44317");
                }
            }
        }
        public void Setlogin1True(UserModel userModel, ogrencisistemiEntities context)
        {
            context = new ogrencisistemiEntities();
            var user = context.students.FirstOrDefault(x => x.student_no == userModel.Username);
            if (user != null)
            {
                login login = context.login.FirstOrDefault(x => x.student_id == user.id);
                login.login1 = true;
                login.login_lastlogin = DateTime.Now;
                context.SaveChanges();
            }
            else
            {
                var userTeacher = context.employee.FirstOrDefault(x => x.employee_tckn == userModel.Username);
                login login = context.login.FirstOrDefault(x => x.teacher_id == userTeacher.id);
                var teacher = context.employee.FirstOrDefault(x => x.employee_tckn == userModel.Username);
                login = context.login.FirstOrDefault(x => x.teacher_id == teacher.id);
                login.login1 = true;
                login.login_lastlogin = DateTime.Now;
                context.SaveChanges();
            }
        }
        public ActionResult Logout()
        {
            context = new ogrencisistemiEntities();
            var user = context.login.FirstOrDefault(x => x.login1 == true);
            if (user == null)
            {
                MessageBox.Show("Çıkış yapmak için önce giriş yapmanız gerekiyor !", "Bilgilendirme Penceresi");
                return Redirect("https://localhost:44317/Login/Index");
            }
            user.login1 = false;
            context.SaveChanges();
            MessageBox.Show("Başarıyla çıkış yapıldı", "Bilgilendirme Penceresi");
            return Redirect("https://localhost:44317/Login/Index");
        }
    }
}