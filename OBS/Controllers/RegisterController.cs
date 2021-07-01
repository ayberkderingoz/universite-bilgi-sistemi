using OBS.Models;
using OBS.Models.TeacherModel;
using OBS.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace OBS.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        private ogrencisistemiEntities context;
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
                    return Redirect("https://localhost:44317/Teacher/Teacher");
                }
            }
            return View("Register");
        }
        public ActionResult Register(UserModel userModel)
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if(login.employee.position.position_name.ToString() != "officer" || login.employee == null)
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            students studentObject = new students();
            if (userModel.Password == null || userModel.Username == null)
            {
                return Redirect("https://localhost:44317/Register/Index");
            }
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(userModel.Password);
            var sha1data = sha1.ComputeHash(data);
            studentObject.student_no = userModel.Username;
            studentObject.student_name = userModel.Name;
            studentObject.student_email = userModel.Email;
            studentObject.student_phoneno = userModel.CepNo;
            studentObject.student_tckn = userModel.TCKN;
            studentObject.student_password = sha1data;
            var bilgiler = context.students.FirstOrDefault(x => x.student_no == studentObject.student_no);
            if (bilgiler != null || !IsPasswordAvailable(userModel) || !IsEmailAvailable(userModel) || !IsPhoneNoAvailable(userModel) || !IsTcknAvailable(userModel))
            {
                return View("Register");
            }
            context.students.Add(studentObject);
            context.SaveChanges();
            createLoginData(studentObject);
            return Redirect("https://localhost:44317/Login/Login");
        }
        public ActionResult Officer()
        {
            context = new ogrencisistemiEntities();
            var isOfficer = false;
            var logged = context.login.FirstOrDefault(x => x.login1 == true);
            if (logged == null || logged.employee.position.position_name.ToString() != "officer")
            {
                return Redirect("https://localhost:44317");
            }
            foreach (employee employee in context.employee.ToList())
            {

                if (logged.teacher_id == employee.id)
                {
                    if (employee.position.position_name == "officer")
                    {
                        isOfficer = true;
                    }
                }
            }
            if (!isOfficer)
            {
                return Redirect("https://localhost:44317/Login/Index"); //görevli ekranı gelicek ayrıca logincontrollerdaki öğretmen kontrolü düzeltilicek bi teachera yolluyo bi logine
            }
            List<SelectListItem> positionList = (from x in context.position.ToList()
                                            select new SelectListItem
                                            {

                                                Text = x.position_name ,
                                                Value = x.id.ToString()
                                            }).ToList();
            
            var loggedUser = GetLogged();
            var loggedinuser = context.employee.FirstOrDefault(x => x.id.ToString() == loggedUser);
            List<string> user = new List<string>();
            user.Add("Merhaba! : " + loggedinuser.employee_name);
            user.Add("Email : " + loggedinuser.employee_email);
            user.Add("Telefon Numarası : " + loggedinuser.employee_phoneno);
            user.Add("Öğretmenin T.C.K.N'si : " + loggedinuser.employee_tckn);
            TeacherModel teacherModel1 = new TeacherModel();
            ViewBag.Officer = user;
            ViewBag.OfficerRegister = positionList;
            return View("Officer");
        }
        public ActionResult EmployeeRegister(position pos)
        {
            switch (pos.id)
            {
                case 1:
                    return View("TeacherRegister");
                case 2:
                    return View("OfficerRegister");
                case 99:
                    return View("Register");
                default:
                    break;
            }
            return View("Officer");
        }
        public ActionResult OfficerRegister(UserModel userModel)
        {
            context = new ogrencisistemiEntities();
            var logged = context.login.FirstOrDefault(x => x.login1 == true);
            if (logged.employee.position.position_name.ToString() != "officer" || logged == null)
            {
                return Redirect("https://localhost:44317");
            }
            var bilgiler = context.employee.FirstOrDefault(x => x.employee_tckn == userModel.TCKN);
            if (bilgiler != null || !IsPasswordAvailable(userModel) || !IsEmailAvailable(userModel) || !IsPhoneNoAvailable(userModel) || !IsTcknAvailable(userModel))
            {
                return View("OfficerRegister");
            }

            employee employee = new employee();
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(userModel.Password);
            var sha1data = sha1.ComputeHash(data);
            employee.employee_name = userModel.Name;
            employee.employee_email = userModel.Email;
            employee.employee_password = sha1data;
            employee.employee_phoneno = userModel.CepNo;
            employee.employee_tckn = userModel.TCKN;
            employee.employee_position = 2;
            employee.employee_lastchange = DateTime.Now;
            employee.emoployee_status = true;
            context.employee.Add(employee);
            context.SaveChanges();
            createLoginEmployee(employee);
            return View();
        }
        public ActionResult TeacherRegister(UserModel userModel)
        {
            context = new ogrencisistemiEntities();
            var logged = context.login.FirstOrDefault(x => x.login1 == true);
            if (logged.employee.position.position_name.ToString() != "officer" || logged == null)
            {
                return Redirect("https://localhost:44317");
            }
            var bilgiler = context.employee.FirstOrDefault(x => x.employee_tckn == userModel.TCKN);
            if (bilgiler != null || !IsPasswordAvailable(userModel) || !IsEmailAvailable(userModel) || !IsPhoneNoAvailable(userModel) || !IsTcknAvailable(userModel))
            {
                return View("TeacherRegister");
            }

            employee employee = new employee();
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(userModel.Password);
            var sha1data = sha1.ComputeHash(data);
            employee.employee_name = userModel.Name;
            employee.employee_email = userModel.Email;
            employee.employee_password = sha1data;
            employee.employee_phoneno = userModel.CepNo;
            employee.employee_tckn = userModel.TCKN;
            employee.employee_position = 1;
            employee.employee_lastchange = DateTime.Now;
            employee.emoployee_status = true;
            context.employee.Add(employee);
            context.SaveChanges();
            var isStudentBefore = context.login.FirstOrDefault(x => x.students.student_tckn == employee.employee_tckn);
            if (isStudentBefore == null) {
                createLoginEmployee(employee);
            }
            else
            {
                isStudentBefore.employee = employee;
                context.SaveChanges();
            }
            return View();
        }
        public ActionResult GetStudentList()
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login == null)
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            List<students> studentsList = context.students.ToList();
            return View("StudentList",studentsList);
        }
        public ActionResult GetEmployeeList()
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login == null)
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            List<employee> employeeList = context.employee.ToList();
            return View("EmployeeList", employeeList);
        }
        public ActionResult EditStudent(int id)
        {
            context = new ogrencisistemiEntities();
            var student = context.students.FirstOrDefault(x => x.id == id);
            return View(student);
        }
        [HttpPost]
        public ActionResult EditStudent(students student)
        {
            context = new ogrencisistemiEntities();
            var std = context.students.FirstOrDefault(x => x.id == student.id);
            std.student_email = student.student_email;
            std.student_name = student.student_name;
            std.student_no = student.student_no;
            std.student_status = student.student_status;
            std.student_tckn = student.student_tckn;
            std.student_phoneno = student.student_phoneno;
            context.SaveChanges();
            MessageBox.Show("Değiştirme işlemi başarılı", "Bilgilendirme Penceresi");
            return View();
        }
        public ActionResult EditEmployee(int id) //lastchange değeri gelicek
        {
            context = new ogrencisistemiEntities();
            var employee = context.employee.FirstOrDefault(x => x.id == id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult EditEmployee(employee employee)
        {
            context = new ogrencisistemiEntities();
            var emp = context.employee.FirstOrDefault(x => x.id == employee.id);
            emp.employee_email = employee.employee_email;
            emp.employee_name = employee.employee_name;
            emp.employee_phoneno = employee.employee_phoneno;
            emp.employee_tckn = employee.employee_tckn;
            emp.employee_position = employee.employee_position;
            emp.emoployee_status = employee.emoployee_status;
            MessageBox.Show("Değiştirme işlemi başarılı", "Bilgilendirme Penceresi");
            return View();
        }
        public string GetLogged()
        {
            context = new ogrencisistemiEntities();
            login login = new login();
            login = context.login.FirstOrDefault(x => x.login1 == true && (x.teacher_id != null));
            if (login == null)
            {
                return "";
            }
            return login.teacher_id.ToString();
        }
        public bool IsPasswordAvailable(UserModel usermodel)//checks if the password is available
        {
            if (usermodel.Password != usermodel.MatchPassword)
            {
                return false;
            }
            bool hasP = false, hasUp = false, hasLow = false, hasNum = false;
            if (usermodel.Password == null)
            {
                return false;
            }
            if (usermodel.Password.Length < 8)
            {
                return false;
            }
            for (int i = 0; i < usermodel.Password.Length; i++)
            {
                if ((usermodel.Password[i] >= 33 && usermodel.Password[i] <= 46) || (usermodel.Password[i] >= 58 && usermodel.Password[i] <= 64))
                {
                    hasP = true;
                }
                else if (usermodel.Password[i] >= 65 && usermodel.Password[i] <= 90)
                {
                    hasUp = true;
                }
                else if (usermodel.Password[i] >= 97 && usermodel.Password[i] <= 122)
                {
                    hasLow = true;
                }
                else if (usermodel.Password[i] >= 48 && usermodel.Password[i] <= 57)
                {
                    hasNum = true;
                }
            }
            if (hasP == true && hasUp == true && hasLow == true && hasNum == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsEmailAvailable(UserModel userModel)//checks if the email is available
        {
            if (userModel.Email.Length > 64)
            {
                return false;
            }
            if (userModel.Email.Length < 5)
            {
                return false;
            }
            for (int i = 0; i < userModel.Email.Length; i++)
            {
                if (userModel.Email[i] == 64)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsTcknAvailable(UserModel userModel)//checks if the tckn is available
        {
            if (userModel.TCKN[0] == '0')
            {
                return false;
            }
            if (userModel.TCKN[userModel.TCKN.Length - 1] % 2 == 1)
            {
                return false;
            }
            int hanetest = (userModel.TCKN[0] - 48 + userModel.TCKN[2] - 48 + userModel.TCKN[4] - 48 + userModel.TCKN[6] - 48 + userModel.TCKN[8] - 48) * 7 - (userModel.TCKN[1] - 48 + userModel.TCKN[3] - 48 + userModel.TCKN[5] - 48 + userModel.TCKN[7] - 48);
            if (hanetest % 10 != userModel.TCKN[9] - 48)
            {
                return false;
            }
            hanetest = 0;
            for (int i = 0; i < 10; i++)
            {
                hanetest += userModel.TCKN[i] - 48;
            }
            if (hanetest % 10 != userModel.TCKN[10] - 48)
            {
                return false;
            }
            return true;
        }
        public bool IsPhoneNoAvailable(UserModel userModel)//checks if the phone number is available
        {
            if (userModel.CepNo.Length != 10)
            {
                return false;
            }
            if (userModel.CepNo[0] != '5')
            {
                return false;
            }
            return true;
        }
        public void createLoginData(students usermodel)//if user passes all the controls adds user into the db
        {
            login login = new login();
            login.student_id = usermodel.id;
            login.login1 = false;
            context = new ogrencisistemiEntities();
            context.login.Add(login);
            context.SaveChanges();
        }
        public void createLoginEmployee(employee employee)
        {
            login login = new login();
            login.teacher_id = employee.id;
            login.login1 = false;
            context = new ogrencisistemiEntities();
            context.login.Add(login);
            context.SaveChanges();
        }
    }
}