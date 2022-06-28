using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BaseEntities;
public abstract class BaseEntityNoId
{
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? UpdateDate { get; set; }
    public bool IsRemoved { get; set; } = false;
    public DateTime? RemoveTime { get; set; }

}
