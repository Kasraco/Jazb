﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Jazb.Model.AdminModel
{
    public class EditUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام کاربری باید وارد شود"),
         MaxLength(20, ErrorMessage = "نام کاربری باید کمتر از 20 حرف باشد"),
         MinLength(3, ErrorMessage = "نام کاربری باید بیشتر از 3 حرف باشد")]
        public string UserName { get; set; }

        [MaxLength(20, ErrorMessage = "کلمه عبور باید کمتر از 20 حرف باشد"),
         MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 6 حرف باشد")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه عبور و تکرارش باید یکسان باشند")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "ایمیل باید وارد شود"), MaxLength(50, ErrorMessage = "ایمیل باید کمتر از 50 حرف باشد"),
         MinLength(4, ErrorMessage = "ایمیل باید بیشتر از 4 حرف باشد"),
         RegularExpression(
             @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
             ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major { get; set; }
        public string Description { get; set; }

        [RegularExpression(@"^([1][234][0-9]{2})/[0-9]{1,2}/([0-9]{1,2})$",
            ErrorMessage = "تاریخ وارد شده معتبر نمی باشد")]
        public DateTime? BirthDay { get; set; }

        public int RoleId { get; set; }
        public bool AvatarStatus { get; set; }
        public bool IsBaned { get; set; }
        public DateTime? BanedDate { get; set; }
    }
}