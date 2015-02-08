using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Domain
{
    public class TaskItem : Entity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        [Range(0, 100)]
        public double PercentageCompleted { get; set; }

        public uint Order { get; set; }

        public User Owner { get; set; }

        public virtual Board OwnerBoard { get; set; }
    }
}
