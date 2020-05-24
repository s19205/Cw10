﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Models
{
    public class Patient
    {
        public Patient()
        {
            Prescriptions = new HashSet<Prescription>();
        }
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
