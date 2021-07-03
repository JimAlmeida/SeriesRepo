using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIORepository
{
    interface IRepositoryInterface
    {
        public void AddItemsToRepository();
        public void ReadItemsFromRepository();
        public void UpdateItemsInRepository();
        public void DeleteItemsFromRepository();

    }
}
