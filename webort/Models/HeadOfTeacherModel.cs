using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webort.Models
{
    public class HeadOfTeacherModel
    {
        //Данные сотрудника
        public int id_HeadOfTeacher { get; set; }
        public string surname { get; set; }
        public string first_name { get; set; }
        public string third_Name { get; set; }
        public DateTime birth_date { get; set; }
        public string gender { get; set; }
        public string phone_number { get; set; }
        public string inn_passport { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        //Атрибуты Авторизации
        public string login { get; set; }
        public string password { get; set; }
        public Nullable<bool> FiredEmpl { get; set; }
        public Nullable <DateTime> date_of_appointment { get; set; }
        //Дата уволенени
        public Nullable<DateTime> date_leavingFired { get; set; }
        //Предыдущий дата назначения
        public Nullable<DateTime> date_appointmentFired { get; set; }
        //Наименование должности
        public string position { get; set; }
        //Наименование уровня доступа
        public string access_level { get; set; }
        //Наименование школы
        public string schoolName { get; set; }
        //Код школы
        public Nullable<int> schoolCode { get; set; }
        public Nullable<byte> Position { get; set; }
        public Nullable<byte> Access_level { get; set; }
        public Nullable<short> School { get; set; }
        public Nullable<int> Authotization { get; set; }
        public virtual Access_level Access_levelEmpl { get; set; }
        public virtual Position PositionEmpl { get; set; }
        public virtual School SchoolEmpl { get; set; }
        public virtual Authorization Authorization { get; set; }
    }
}