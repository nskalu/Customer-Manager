﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomerManager.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTimeOffset DOB { get; set; }
        [Required]
        public string Email { get; set; }
        public byte Imageurl { get; set; }

        

        
    }
}