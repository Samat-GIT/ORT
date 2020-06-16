using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webort.Models;

namespace webort.Repository
{
    public class HeadOfTeacherRepository
    {
        private SqlConnection con;

        private void connection()
        {
            string connectionString = @"Data Source=DESKTOP-73AMEOQ\Samat;Initial Catalog=ORT;Integrated Security=True";

            con = new SqlConnection(connectionString);

        }
        public bool AddHeadOfSchool(HeadOfTeacherModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("insert into [Authorization]([Login],[Password]) values(@login,@password);select SCOPE_IDENTITY();", con);
            com.Parameters.AddWithValue("@login", obj.login);
            com.Parameters.AddWithValue("@password", obj.password);
            var inserdetID = com.ExecuteScalar();
            com = new SqlCommand("insert into Employees(Surname,First_name,Third_Name,Birth_date,Gender,Phone_number," +
                "INN_passport, Address, Email, Date_of_appointment, Position, Access_level, School, Authotization)" +
                " VALUES (@surname,@firstname,@thirdname,@birthdate,@gender,@phonenumber,@innpassport, @adress," +
                " @email, @dateofappointment, @position, @accesslevel,@school,@Authorization); ", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@surname", obj.surname);
            com.Parameters.AddWithValue("@firstname", obj.first_name);
            com.Parameters.AddWithValue("@thirdname", obj.third_Name);
            com.Parameters.AddWithValue("@birthdate", obj.birth_date);
            com.Parameters.AddWithValue("@gender", obj.gender);
            com.Parameters.AddWithValue("@phonenumber", obj.phone_number);
            com.Parameters.AddWithValue("@innpassport", obj.inn_passport);
            com.Parameters.AddWithValue("@adress", obj.address);
            com.Parameters.AddWithValue("@email", obj.email);
       //     com.Parameters.AddWithValue("@login", obj.login);
         //   com.Parameters.AddWithValue("@password", obj.password);
            com.Parameters.AddWithValue("@dateofappointment", obj.date_of_appointment);
            com.Parameters.AddWithValue("@position", obj.Position);
            com.Parameters.AddWithValue("@accesslevel", obj.Access_level);
            com.Parameters.AddWithValue("@school", obj.School);
            com.Parameters.AddWithValue("@Authorization", inserdetID);
          //  com.Parameters.AddWithValue("@Authorization", obj.Authotization); ;
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
    }
}