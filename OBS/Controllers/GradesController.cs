using OBS.Models;
using OBS.Models.GradesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace OgrenciTest.Controllers
{
    public class GradesController : Controller
    {
        private ogrencisistemiEntities context;

        // GET: Grades
        public ActionResult Info()
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login == null )
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            if (login.teacher_id != null && login.employee.position.position_name == "teacher")
            {
                return Redirect("https://localhost:44317/Teacher/Teacher"); // Öğretmen sayfası linki konulacak
            }
            if(login.teacher_id != null && login.employee.position.position_name == "officer")
            {
                return Redirect("https://localhost:44317/Register/Officer");
            }
            List<GradesModel> participatingLessons = new List<GradesModel>();

            lessons lesson = new lessons();
            employee teacher = new employee();
            lesson_teacher lesson_teacher = new lesson_teacher();
            students student = new students();
            foreach (studentlessons studentlesson in context.studentlessons.ToList())
            {

                if (GetLogged() == studentlesson.student_id.ToString())
                {
                    GradesModel gradesModel = new GradesModel();
                    gradesModel.Grade = studentlesson.grade;
                    lesson_teacher = context.lesson_teacher.FirstOrDefault(x => x.id == studentlesson.teacher_lesson);
                    student = context.students.FirstOrDefault(x => x.id == studentlesson.student_id);
                    lesson = context.lessons.FirstOrDefault(x=>x.id == lesson_teacher.lesson_id);
                    teacher = context.employee.FirstOrDefault(x => x.id == lesson_teacher.teacher_id);
                    gradesModel.LessonName = lesson.lesson_name;
                    gradesModel.TeacherName = teacher.employee_name;
                    if (gradesModel.Grade == null)
                    {
                        participatingLessons.Add(gradesModel);
                    }
                }
            }

            var loggedUser = GetLogged();
            var loggedinuser = context.students.FirstOrDefault(x => x.id.ToString() == loggedUser);
            List<string> user = new List<string>();
            user.Add("Merhaba! : " + loggedinuser.student_name);
            user.Add("Öğrenci Numarası : " + loggedinuser.student_no);
            user.Add("Email : " + loggedinuser.student_email);
            user.Add("Telefon Numarası : "+loggedinuser.student_phoneno);
            user.Add("Öğrenci T.C.K.N : "+loggedinuser.student_tckn);
            ViewBag.User = user; //info ekranında kullanıcı bilgilerini göstermek için kullanılan viewbag
            List<SelectListItem> lessons = (from x in context.lesson_teacher.ToList()
                                            select new SelectListItem
                                            {

            Text = x.lessons.lesson_name + "    (" +x.employee.employee_name+")",
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.lessons = lessons;
            ViewBag.studentList = participatingLessons;

            return View("Info");
        }
        public ActionResult AddLesson(lesson_teacher addLesson)
        {
            context = new ogrencisistemiEntities();
            var studentlessonAdd = new studentlessons();
            login logged = new login();
            logged= context.login.FirstOrDefault(x => x.login1 == true);
            studentlessonAdd.student_id = logged.student_id;
            studentlessonAdd.teacher_lesson = addLesson.id;
            context.studentlessons.Add(studentlessonAdd);
            context.SaveChanges();
            MessageBox.Show("Ders Başarıyla Eklendi", "Bilgilendirme Penceresi");
            return Redirect("https://localhost:44317/Grades/Info");
        }
        public ActionResult RemoveLesson(lesson_teacher removeLesson)
        {
            context = new ogrencisistemiEntities();
            var studentlessonRemove = new studentlessons();
            studentlessonRemove = context.studentlessons.FirstOrDefault(x => x.lesson_teacher.lessons.lesson_name == removeLesson.lessons.lesson_name && x.lesson_teacher.employee.employee_name == removeLesson.employee.employee_name);
            context.studentlessons.Remove(studentlessonRemove);
            context.SaveChanges();
            MessageBox.Show("Ders Başarıyla Silindi !", "Bilgilendirme Penceresi");
            return Redirect("https://localhost:44317/Grades/Info");
        }
        public ActionResult Grades()
        {
            context = new ogrencisistemiEntities();
            login login = context.login.FirstOrDefault(x => x.login1 == true);
            if (login == null)
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            if (login.teacher_id != null && login.employee.position.position_name == "teacher")
            {
                return Redirect("https://localhost:44317/Teacher/Teacher"); // Öğretmen sayfası linki konulacak
            }
            if (login.teacher_id != null && login.employee.position.position_name == "officer")
            {
                return Redirect("https://localhost:44317/Register/Officer");
            }
            List<GradesModel> gradedLessons = new List<GradesModel>();
            lessons lesson = new lessons();
            employee teacher = new employee();
            lesson_teacher lesson_teacher = new lesson_teacher();
            students student = new students();
            foreach (studentlessons studentlesson in context.studentlessons.ToList())
            {

                if (GetLogged() == studentlesson.student_id.ToString())
                {
                    GradesModel gradesModel = new GradesModel();
                    gradesModel.Grade = studentlesson.grade;
                    lesson_teacher = context.lesson_teacher.FirstOrDefault(x => x.id == studentlesson.teacher_lesson);
                    student = context.students.FirstOrDefault(x => x.id == studentlesson.student_id);
                    lesson = context.lessons.FirstOrDefault(x => x.id == lesson_teacher.lesson_id);
                    teacher = context.employee.FirstOrDefault(x => x.id == lesson_teacher.teacher_id);
                    gradesModel.LessonName = lesson.lesson_name;
                    gradesModel.TeacherName = teacher.employee_name;
                    if (gradesModel.Grade == null)
                    {
                        gradesModel.Grade = "-";
                        gradedLessons.Add(gradesModel);
                    }
                    else
                    {
                        gradedLessons.Add(gradesModel);
                    }
                }
            }
            if (gradedLessons == null)
            {
                return Redirect("https://localhost:44317/Login/Index"); //graded lessons ekranı gelicek
            }

            //ViewBag.Id = id;
            return View("Grades",gradedLessons);

        }
 

        public string GetLogged()
        {
            context = new ogrencisistemiEntities();
            login log = new login();
            log = context.login.FirstOrDefault(x => x.login1 == true);
            if (log == null)
            {
                return null;
            }
            else if (log.student_id != null)
            {
                return log.student_id.ToString(); // düzelt bunu
            }
            else if(log.teacher_id != null)
            {
                return "teacher";
            }
            return null;
        }
    }

}