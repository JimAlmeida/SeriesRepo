using System;
using System.Collections.Generic;
using System.Linq;
using SqlKata.Execution;
using SqlKata.Compilers;
using System.Data.SqlClient;
using DIORepository.DataTransferObjects;

namespace DIORepository
{
    class CrudDb : ICRUD
    {
        private QueryFactory database;
        private SqlConnection connection;
        private string connectionString;

        public CrudDb()
        {
            connectionString = "Server=localhost\\SQLEXPRESS;Database=DIORepo;Trusted_Connection=True;";
            connection = new SqlConnection(connectionString);
            database = new QueryFactory(connection, new SqlServerCompiler());
        }

        public int Create(IEnumerable<Payload> items)
        {
            int rowsAffected = 0;
            foreach (SQLPayload record in items)
            {
                rowsAffected += database.Execute(database.Query(record.tableName).AsInsert(record.columns, record.recordData));
            }
            return rowsAffected;
        }

        public IEnumerable<T> Read<T>(Payload options)
        {
            SQLPayload payload = (SQLPayload)options;
            SqlKata.Query query;
            if (payload.primaryKeyValue != null)
            {
                query = database.Query(payload.tableName).Where(payload.primaryKeyIdentifier, payload.primaryKeyValue).Select(payload.columns);
            }
            else
            {
                query = database.Query(payload.tableName).Select(payload.columns);
            }
            return query.Get<T>();
        }

        public int Update(IEnumerable<Payload> items)
        {
            int rowsAffected = 0;
            foreach(SQLPayload record in items)
            {
                var query = database.Query(record.tableName).Where(record.primaryKeyIdentifier, record.primaryKeyValue).AsUpdate(record.columns, record.recordData);
                rowsAffected += database.Execute(query);
            }
            return rowsAffected;
        }

        public int Delete(IEnumerable<Payload> items)
        {
            int rowsAffected = 0;
            foreach (SQLPayload record in items)
            {
                var query = database.Query(record.tableName).Where(record.primaryKeyIdentifier, record.primaryKeyValue).AsDelete();
                rowsAffected += database.Execute(query);
            }
            return rowsAffected;
        }
    }
}
