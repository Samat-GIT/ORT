using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webort.Models;

namespace webort.Repository
{
    public class SchoolLocRepository
    {
        private SqlConnection con;

        private void connection()
        {
            string connectionString = @"Data Source=DESKTOP-73AMEOQ\Samat;Initial Catalog=ORT;Integrated Security=True";

            con = new SqlConnection(connectionString);

        }
        public bool AddSchoolLoc(SchoolModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("Add_School", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@schoolname", obj.school_name);
            com.Parameters.AddWithValue("@postalzip", obj.postal_zip);
            com.Parameters.AddWithValue("@codeofschool", obj.code_of_school);
            com.Parameters.AddWithValue("@passwordschool", obj.password_school);
            com.Parameters.AddWithValue("@district", obj.District);
            com.Parameters.AddWithValue("@localiatynamee", obj.localityname);
            com.Parameters.AddWithValue("@localiatyaddresss", obj.localityaddress);
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
        ORTEntities db = new ORTEntities();
        public List<SchoolModel> GetSchools()
        {
            var result = (from c in db.School.AsEnumerable()
                          join d in db.District_town_.AsEnumerable() on c.District equals d.ID_District
                          join r in db.Region.AsEnumerable() on d.Region equals r.ID_Region
                          select new SchoolModel
                          {
                              id_School = c.ID_School,
                              school_name = c.School_name,
                              postal_zip = c.Postal_zip,
                              code_of_school = c.Code_of_school,
                              password_school = c.Password_school,
                              districtname = d.District_name,
                              localityname = c.LocalitiName,
                              localityaddress = c.LocalityAddress,
                              region = r.Region_name
                          }).ToList();
            return result;
        }
      
    }
}