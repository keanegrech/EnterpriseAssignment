using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IItemValidating
    {
        // returns list of email addresses that can approve the item
        public List<string> GetValidators();

        // returns the file name of the partial file of the resturant/item card
        public string GetCardPartial();
    }
}
