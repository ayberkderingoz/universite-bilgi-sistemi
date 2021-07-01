using OBS.Models;
using OBS.Models.TeacherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgi.Controllers
{
    public class TeacherController : Controller
    {
        private ogrencisistemiEntities context;
        // GET: Teacher

        public ActionResult Teacher()
        {
            context = new ogrencisistemiEntities();
            bool isTeacher=false;
            if (GetLogged() == "")
            {
                return Redirect("https://localhost:44317/Login/Index");
            }
            var logged = context.login.FirstOrDefault(x => x.login1 == true);
            foreach (employee employee in context.employee.ToList()) {

                if (logged.teacher_id == employee.id)
                {
                    if (employee.position.position_name == "teacher")
                    {
                        isTeacher = true;
                    }
                }
            }
            if (!isTeacher) {
                return Redirect("https://localhost:44317/Login/Index"); //görevli ekranı gelicek ayrıca logincontrollerdaki öğretmen kontrolü düzeltilicek bi teachera yolluyo bi logine
            }
            List<TeacherModel> notGradedLessons = new List<TeacherModel>();
            List<TeacherModel> gradedLessons = new List<TeacherModel>();
            foreach (studentlessons lesson in context.studentlessons.ToList())
            {
                if (GetLogged() == lesson.lesson_teacher.teacher_id.ToString())
                {
                    TeacherModel teacherModel = new TeacherModel();
                    var student = context.lesson_teacher.FirstOrDefault(x => x.teacher_id == lesson.lesson_teacher.teacher_id);
                    if (student != null)
                    {
                        if (lesson.grade == null)
                        {
                            teacherModel.studentno = lesson.students.student_no;
                            teacherModel.studentname = lesson.students.student_name;
                            teacherModel.LessonName = lesson.lesson_teacher.lessons.lesson_name;
                            notGradedLessons.Add(teacherModel);
                        }
                        else
                        {
                            teacherModel.studentno = lesson.students.student_no;
                            teacherModel.studentname = lesson.students.student_name;
                            teacherModel.LessonName = lesson.lesson_teacher.lessons.lesson_name;
                            teacherModel.grade = lesson.grade;
                            gradedLessons.Add(teacherModel);
                        }
                    }
                }
            }
            var loggedUser = GetLogged();
            var loggedinuser = context.employee.FirstOrDefault(x => x.id.ToString() == loggedUser);
            List<string> user = new List<string>();
            user.Add("Merhaba! : " + loggedinuser.employee_name);
            user.Add("Email : " + loggedinuser.employee_email);
            user.Add("Telefon Numarası : " + loggedinuser.employee_phoneno);
            user.Add("Öğretmenin T.C.K.N'si : " + loggedinuser.employee_tckn);
            TeacherModel teacherModel1 = new TeacherModel();
            ViewBag.Teacher = user;
            notGradedLessons = notGradedLessons.OrderBy(q => q.LessonName).ToList();
            gradedLessons = gradedLessons.OrderBy(q => q.LessonName).ToList();
            ViewBag.notGradedLessons = notGradedLessons;
            ViewBag.gradedLessons = gradedLessons;
            return View("Teacher");
        }
        [HttpPost]
        public ActionResult Teacher(TeacherModel teacherModel)
        {
            context = new ogrencisistemiEntities();
            studentlessons grade = new studentlessons();
            grade.grade = teacherModel.grade;
            var loggedUser = GetLogged();
            var bilgiler = context.studentlessons.FirstOrDefault(x => x.students.student_no == teacherModel.studentno && x.lesson_teacher.lessons.lesson_name == teacherModel.LessonName
            && x.lesson_teacher.teacher_id.ToString() == loggedUser);
            if (bilgiler != null)
            {

                bilgiler.grade = teacherModel.grade;
                context.SaveChanges();
                return Redirect("https://localhost:44317/Teacher/Teacher");

            }
            return Redirect("https://localhost:44317/Teacher/Teacher");
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
            if(login.employee.position.position_name == "officer")
            {
                return "";
            }
            return login.teacher_id.ToString();
        }
    }
}