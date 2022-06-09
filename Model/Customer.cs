using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bedirhan_Hafta_1.Model
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string TelNumber { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }

    }
}
