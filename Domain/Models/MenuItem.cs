using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MenuItem
    {
        [Key()]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }

        [ForeignKey("ResturantFK")]
        public virtual Resturant Resturant { get; set; }
        public int ResturantFK { get; set; }
        public string Status { get; set; }
    }
}
