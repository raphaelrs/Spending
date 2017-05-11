using DbUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater
{
    public class Program
    {
        static int Main(string[] args)
        {
            var connectionString = @"Server = PE-REC-PC181\SQLEXPRESS; Database = SpendingDb; Trusted_Connection = True";
            //var connectionString = @"Server = 192.168.1.52; Database = SpendingDb; User Id=contract2; Password=M@stertech2017";

            //"Server =PE-REC-PC181\\SQLEXPRESS; Database=SpendingDb; Trusted_connection=true";

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

                #if DEBUG
                Console.ReadLine();
                #endif

                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            Console.ReadKey();
            return 0;
        }
    }
}

