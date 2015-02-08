using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Domain
{
    public class Board : Entity
    {
        [Required]
        public string Name { get; set; }

        public uint Order { get; set; }

        public User Owner { get; set; }

        public virtual IQueryable<TaskItem> TaskItems { get; set; }
    }
}
