using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class RoleLocationViewModel
    {
        public Guid Id { get; set; }
        public int RoleCode { get; set; }
        public string Description { get; set; }
        public string AddedUserId { get; set; }


        public RoleLocation ToModel()
        {
            return new RoleLocation
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                RoleCode = RoleCode,
                Description = Description,
               
                AddedUserId = AddedUserId,
            };
        }
        public RoleLocationViewModel()
        {

        }

        public RoleLocationViewModel(RoleLocation l)
        {
            this.Id = l.Id;
            this.RoleCode = l.RoleCode;
            this.Description = l.Description;
            this.AddedUserId = l.AddedUserId;


        }
       
    }
}