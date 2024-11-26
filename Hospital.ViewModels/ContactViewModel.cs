﻿using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hospital.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public int HospitalInfoId {  get; set; }
        public ContactViewModel(Contact model)
        {
             
        }
        public ContactViewModel(ContactViewModel model)
        {
            Id = model.Id;
            Email = model.Email;
            Phone = model.Phone;
            HospitalInfoId = model.HospitalInfoId;
        }

        public ContactViewModel()
        {
        }
        public Contact ConvertViewModel(ContactViewModel model)
        {
            return new Contact
            {
                Id = model.Id,
                Email = model.Email,
                Phone = model.Phone,
                HospitalId = model.HospitalInfoId,
            };

        }
    }
}
