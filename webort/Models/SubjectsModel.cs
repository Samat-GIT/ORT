using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webort.Models
{
    public class SubjectsModel
    {
        public byte id_subject { get; set; }
        [Required(ErrorMessage = "Наименование предмета обязательно.")]
         
        
        public string subject_name { get; set; }   
        [Required(ErrorMessage = "Введите стоимость !")]

        public Nullable<decimal> subject_Price { get; set; }
    }
}