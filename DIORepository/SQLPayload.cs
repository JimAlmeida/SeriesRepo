using System.Collections.Generic;

namespace DIORepository.DataTransferObjects
{
    
    public class Payload
    {
        public IEnumerable<object> recordData { get; set; }
    }
    
    public class SQLPayload: Payload
    {
        public string tableName { get; set; }
        public string[] columns { get; set; }
        public string primaryKeyIdentifier { get; set; }
        public object primaryKeyValue { get; set; }
    }

    public class TXTPayload: Payload
    {
        public string fileName { get; set; }
        public string header { get; set; }
    }
}
