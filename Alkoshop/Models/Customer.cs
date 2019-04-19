﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alkoshop.Models
{
    public class Customer
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(111111111, 999999999,ErrorMessage ="Neni regulerni telefonni cislo")]
        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        public string BirthDate { get; set; }
        
        public Address Address { get; set; }
        
    }
}