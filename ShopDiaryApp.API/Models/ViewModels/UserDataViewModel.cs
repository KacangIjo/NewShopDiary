﻿using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class UserDataViewModel  
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public UserData ToModel()
        {
            return new UserData
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                UserId = UserId,
                Email = Email
            };
        }
        public UserDataViewModel()
        {
        }

        public UserDataViewModel(UserData s)
        {
            this.Id = s.Id;
            this.UserId = s.UserId;
            this.Email = s.Email;
        }
       
    }
}