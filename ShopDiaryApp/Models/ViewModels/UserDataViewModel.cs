using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryApp.Models.ViewModels
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
                UserId = this.UserId,
                Email=this.Email,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public UserDataViewModel(UserData p)
        {
            this.UserId = p.UserId;
            this.Email = p.Email;
            this.Id = p.Id;
        }

        public UserDataViewModel()
        {

        }
    }
}
