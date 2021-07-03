using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIORepository.DataTransferObjects;

namespace DIORepository
{
    interface ICRUD
    {
        public int Create(IEnumerable<Payload> items);
        public IEnumerable<T> Read<T>(Payload options);
        public int Update(IEnumerable<Payload> items);
        public int Delete(IEnumerable<Payload> items);
    }
}
