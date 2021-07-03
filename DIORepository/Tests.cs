using System;
using System.Collections.Generic;
using DIORepository.DTOExtensions;
using DIORepository.DataTransferObjects;

namespace DIORepository.Tests
{
    class CRUDTest
    {
        public static void CreateTest()
        {
            CrudDb database = new CrudDb();

            Series theAmericans = new Series()
            {
                title = "The Americans",
                startYear = "2013",
                genre = "Drama",
                description = "At the height of the Cold War two Russian agents pose as your average American couple, complete with family.",
            };

            Series dexter = new Series()
            {
                title = "Dexter",
                startYear = "2006",
                genre = "Drama",
                description = "By day, mild-mannered Dexter is a blood-spatter analyst for the Miami police. But at night, he is a serial killer who only targets other murderers."
            };

            SQLPayload americansPayload = new()
            {
                tableName = "SeriesRepository",
                columns = new string[4] { "genre", "title", "startYear", "description" },
                recordData = theAmericans.AsList()
            };

            SQLPayload dexterPayload = new()
            {
                tableName = "SeriesRepository",
                columns = new string[4] { "genre", "title", "startYear", "description" },
                recordData = dexter.AsList()
            };

            database.Create(new List<SQLPayload> { americansPayload, dexterPayload });
        }
        public static void ReadTest()
        {
            CrudDb database = new CrudDb();
            
            SQLPayload options = new()
            {
                tableName = "SeriesRepository",
                columns = new string[5] { "id", "genre", "title", "startYear", "description" }
            };

            List<Series> records  = (List<Series>)database.Read<Series>(options);
            
            foreach(var record in records)
            {
                Console.WriteLine(record.ToString());
            }
        }
        public static void UpdateTest()
        {
            CrudDb database = new CrudDb();

            Series updateItem1 = new Series()
            {
                startYear = "2007"
            };
            Series updateItem2 = new Series()
            {
                startYear = "2007"
            };

            SQLPayload item = new()
            {
                tableName = "SeriesRepository",
                columns = updateItem1.NotNullParameterList(),
                primaryKeyIdentifier = "id",
                primaryKeyValue = 2003,
                recordData = updateItem1.AsList()
            };

            SQLPayload item2 = new()
            {
                tableName = "SeriesRepository",
                columns = updateItem2.NotNullParameterList(),
                primaryKeyIdentifier = "id",
                primaryKeyValue = 2005,
                recordData = updateItem2.AsList()
            };

            Console.WriteLine(database.Update(new List<SQLPayload> { item, item2 }));
        }
        public static void DeleteTest()
        {
            CrudDb database = new CrudDb();

            SQLPayload item = new()
            {
                tableName = "SeriesRepository",
                primaryKeyIdentifier = "id",
                primaryKeyValue = 2003
            };

            SQLPayload item2 = new()
            {
                tableName = "SeriesRepository",
                primaryKeyIdentifier = "id",
                primaryKeyValue = 2005
            };

            Console.WriteLine(database.Delete(new List<SQLPayload> { item, item2 }));
        }
    }
}