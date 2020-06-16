using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webort.Models
{
    public class SchoolModel
    {
        public short id_School { get; set; }
        public string school_name { get; set; }
        public Nullable<int> postal_zip { get; set; }
        public Nullable<int> code_of_school { get; set; }
        public Nullable<int> password_school { get; set; }
        public string districtname { get; set; }
        public string localityname { get; set; }
        public string localityaddress { get; set; }
        public string region { get; set; }
        public Nullable<short> District { get; set; }
        public virtual District_town_ DistritSchool { get; set; }
     }
}