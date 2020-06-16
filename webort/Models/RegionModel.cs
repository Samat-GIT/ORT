using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webort.Models
{
    public class RegionModel
    {
        public byte id_Region { get; set; }
        public string region_name { get; set; }
    }
}