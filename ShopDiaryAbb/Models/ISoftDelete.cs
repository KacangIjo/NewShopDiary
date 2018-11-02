using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryAbb.Models
{
   public interface ISoftDelete
    {
        Guid Id { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
