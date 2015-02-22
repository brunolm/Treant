namespace Treant.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Board : Entity
    {
        public Board()
        {
            TaskItems = new List<TaskItem>();
        }

        [Required]
        public string Name { get; set; }

        public uint Order { get; set; }

        [Required]
        public int OwnerID { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}
