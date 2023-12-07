// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace HelloWorld // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);
            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow=dapper.LoadDataSingle<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,  
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 954.87m,
                VideoCard = "RTX 2060"
            };

           // entityFramework.Add(myComputer);
          //  entityFramework.SaveChanges();
            // string sql = @"INSERT INTO TutorialAppSchema.Computer(
            //     Motherboard,
            //     HasWifi,  
            //     HasLTE,
            //     ReleaseDate,              
            //     Price,
            //     VideoCard  
            // ) VALUES( '" + myComputer.Motherboard + "', '" + myComputer.HasWifi + "', '" + myComputer.HasLTE + "','" + myComputer.ReleaseDate + "','" + myComputer.Price + "','" + myComputer.VideoCard + "')";

            // Console.WriteLine(sql);
            // int result = dapper.ExecuteSqlWithRowCount(sql);
            // Console.WriteLine(result);

            string sqlSelect = @"SELECT
                Motherboard,
                HasWifi,  
                HasLTE,
                ReleaseDate,              
                Price,
                VideoCard 
            FROM  TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            // foreach(Computer singleComputer in computers)
            // {
            //     Console.WriteLine("'" + myComputer.Motherboard + "','" + myComputer.HasWifi + "', '" + myComputer.HasLTE + "','" + myComputer.ReleaseDate + "','" + myComputer.Price + "','" + myComputer.VideoCard + "'");
            // }
            foreach(Computer singleComputer in computersEF
            )
            {
                Console.WriteLine("'" + singleComputer.ComputerId + "','" + singleComputer.Motherboard + "','" + singleComputer.HasWifi + "', '" + singleComputer.HasLTE + "','" + singleComputer.ReleaseDate + "','" + singleComputer.Price + "','" + singleComputer.VideoCard + "'");
            }

        }
    }
}

