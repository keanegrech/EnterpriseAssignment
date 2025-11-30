using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Resturant : IItemValidating
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerEmailAddress { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public string GetCardPartial()
        {
            throw new NotImplementedException();
        }

        public List<string> GetValidators()
        {
            throw new NotImplementedException();
        }
    }
}
