using System;
using System.Collections.Generic;
using DIORepository.DataTransferObjects;


namespace DIORepository
{
    class Program
    {
        static void ExitProgram()
        {
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            SeriesRepositoryConsoleInterface consoleInterface = new();

            Dictionary<int, Action> interfaceMap = new();
            interfaceMap.Add(1, (Action)consoleInterface.AddItemsToRepository);
            interfaceMap.Add(2, (Action)consoleInterface.ReadItemsFromRepository);
            interfaceMap.Add(3, (Action)consoleInterface.UpdateItemsInRepository);
            interfaceMap.Add(4, (Action)consoleInterface.DeleteItemsFromRepository);
            interfaceMap.Add(5, (Action)Program.ExitProgram);

            while (true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1 - Create records");
                Console.WriteLine("2 - Read records");
                Console.WriteLine("3 - Update records");
                Console.WriteLine("4 - Delete records");
                Console.WriteLine("5 - Exit program");
                int op = Convert.ToInt32(Console.ReadLine());
                interfaceMap[op].Invoke();
            }
        }
    }
}
