﻿using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Services.Dto
{
    public class WishlistOutputDto
    {
        public List<Wishlist> Data { get; set; }
    }
}
