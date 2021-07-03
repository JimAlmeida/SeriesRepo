using System;
using System.Collections.Generic;
using System.Linq;

using DIORepository.DataTransferObjects;
using DIORepository.DTOExtensions;
namespace DIORepository
{

    class SeriesRepositoryConsoleInterface: IRepositoryInterface
    {
        private CrudDb databaseInterface;

        public SeriesRepositoryConsoleInterface(CrudDb dbInterface = null)
        {
            if (dbInterface == null)
            {
                databaseInterface = new CrudDb();
            }
            else
            {
                databaseInterface = dbInterface;
            }
        }
        //########################## CREATE ###################################
        public void AddItemsToRepository() {
            var userInput = userDialogCreate();
            var itemsToInsert = prepareItemsCreate(ref userInput);
            databaseInterface.Create(itemsToInsert);
        }
        private List<SQLPayload> prepareItemsCreate(ref List<Series> series)
        {
            List<SQLPayload> sqlData = new();
            foreach (var serie in series)
            {
                sqlData.Add
                (
                    new SQLPayload()
                    {
                        tableName = "SeriesRepository",
                        columns = serie.NotNullParameterList(),
                        recordData = serie.AsList()
                    }
                );
            }
            return sqlData;
        }
        private List<Series> userDialogCreate()
        {
            List<Series> series = new();
            Console.WriteLine("You will now insert new items to the repository");
            while (true)
            {
                Series serie = new Series();

                Console.WriteLine("###### Insert new series ######");

                Console.WriteLine("Title: "); serie.title = Console.ReadLine();
                Console.WriteLine("Genre: "); serie.genre = Console.ReadLine();
                Console.WriteLine("Description: "); serie.description = Console.ReadLine();
                Console.WriteLine("Year: "); serie.startYear = Console.ReadLine();

                series.Add(serie);

                Console.WriteLine("Do you want to add another series? Y-Yes/N-No");
                string option = Console.ReadLine().ToLower();
                if (option == "n") break;
            }
            return series;
        }

        //##########################  READ  ###################################
        public void ReadItemsFromRepository() {
            SQLPayload options;
            while (true)
            {
                var userInput = userDialogRead();

                options = prepareReadOptions(userInput.Item1, userInput.Item2);

                Console.WriteLine("Reading from the database");

                var queryResults = databaseInterface.Read<Series>(options);

                displayQueryResults(queryResults);

                Console.WriteLine("Do you wish to read more records? Y-Yes/N-No");
                if (Console.ReadLine().ToLower() == "n") break;
            }

        }
        private Tuple<int, int?> userDialogRead()
        {
            Console.Clear();
            Console.WriteLine("Select an option to read items from the repository:");
            Console.WriteLine("1 - Summary View");
            Console.WriteLine("2 - Detailed View");
            Console.WriteLine("3 - Search by Id");

            int option = Convert.ToInt32(Console.ReadLine());
            int? id = null;
            if (option == 3)
            {
                Console.Write("Type in the Series Id: ");
                id = Convert.ToInt32(Console.Read());
            }

            return new Tuple<int, int?>(option, id);
        }
        private SQLPayload prepareReadOptions(int userOption, int? id)
        {
            SQLPayload options = new();
            options.tableName = "SeriesRepository";
            options.primaryKeyIdentifier = "id";
            options.columns = new string[5] { "id", "genre", "title", "startYear", "description" };
            options.primaryKeyValue = id;

            if (userOption == 1)
            {
                options.columns = new string[2] { "id", "title" };
            }
           
            return options;
        }
        private void displayQueryResults<Model>(IEnumerable<Model> results)
        {
            if (results.Count() > 0)
            {
                Console.WriteLine("Displaying results from the database");
                foreach (var record in results)
                {
                    Console.WriteLine(record.ToString());
                }
            }
            else
            {
                Console.WriteLine("No records were found");
            }
        }

        //########################## UPDATE ###################################
        public void UpdateItemsInRepository()
        {
            List<Series> series = new();
            Console.WriteLine("You will be updating items now.");
            while (true)
            {
                series.Add(collectItemInformation());

                Console.WriteLine("Record added into update queue.");
                Console.WriteLine("Do you wish to update another record? Y-Yes/N-No");
                if (Console.ReadLine().ToLower() == "n") break;
            }

            int updatedRecords = databaseInterface.Update(prepareItemsForUpdate(ref series));
            Console.WriteLine($"{updatedRecords} records were deleted");
        }
        private List<SQLPayload> prepareItemsForUpdate(ref List<Series> series)
        {
            List<SQLPayload> items = new();
            foreach (var serie in series)
            {
                items.Add(
                    new()
                    {
                        tableName = "SeriesRepository",
                        columns = serie.NotNullParameterList(),
                        primaryKeyIdentifier = "id",
                        primaryKeyValue = serie.id,
                        recordData = serie.AsList()
                    }
                );
            }
            return items;
        }
        private Series collectItemInformation()
        {
            Series serie = new();
            Console.WriteLine("Creating Update Fields...");
            while (true)
            {
                Console.WriteLine("Select which property you want to change");
                Console.WriteLine("1 - Title");
                Console.WriteLine("2 - Genre");
                Console.WriteLine("3 - Year");
                Console.WriteLine("4 - Description");
                int propertyToChange = Convert.ToInt32(Console.ReadLine());

                switch (propertyToChange)
                {
                    case 1:
                        Console.Write("Title: ");
                        serie.title = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Genre: ");
                        serie.genre = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Year: ");
                        serie.startYear = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Description: ");
                        serie.description = Console.ReadLine();
                        break;
                }
                Console.WriteLine("There are the current fields you have prepared for update: ");
                Console.WriteLine(serie.ToString());
                Console.WriteLine("Do you wish to edit any other property for this record? Y-Yes/N-No");
                if (Console.ReadLine().ToLower() == "n") break;
            }
            Console.WriteLine("Type in the record id associated with the row you wish to update: ");
            serie.id = Convert.ToInt32(Console.ReadLine());
            return serie;
        }

        //########################## DELETE ###################################
        public void DeleteItemsFromRepository()
        {
            if (authenticatedUser())
            {
                var listOfIds = userDialogDelete();
                List<SQLPayload> deleteOptions = prepareDeleteOptions(listOfIds);
                Console.WriteLine("Deleting records now...");

                int deletedRecords = databaseInterface.Delete(deleteOptions);
                Console.WriteLine($"{deletedRecords} records were deleted");
            }
        }
        private List<int> userDialogDelete()
        {
            List<int> ids = new();
            while (true)
            {
                Console.WriteLine("Provide the record id: ");
                ids.Add(Convert.ToInt32(Console.ReadLine()));

                Console.WriteLine("Do you have more records to delete? Y-Yes/N-No");
                if (Console.ReadLine().ToLower() == "n") break;
            }
            return ids;
        }
        private List<SQLPayload> prepareDeleteOptions(IEnumerable<int> ids)
        {
            List<SQLPayload> items = new();
            foreach (int id in ids)
            {
                items.Add(new()
                {
                    tableName = "SeriesRepository",
                    primaryKeyIdentifier = "id",
                    primaryKeyValue = id
                });
            }
            return items;
        }
        private bool authenticatedUser()
        {
            Console.WriteLine("You will be deleting items from the repository.");
            Console.WriteLine("Note that this is an important operation and as such, only special users can perform it.");
            Console.WriteLine("Please type your password to continue: ");

            if (Console.ReadLine().ToLower() == "admin")
            {
                return true;
            }
            else
            {
                Console.WriteLine("You don't have the privileges for this. Contact an administrator.");
                return false;
            }
            
        }
    }
}
