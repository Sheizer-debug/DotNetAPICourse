// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Text.Json;
using AutoMapper;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            // DataContextEF entityFramework = new DataContextEF(config);
            // string sqlCommand = "SELECT GETDATE()";
            // DateTime rightNow=dapper.LoadDataSingle<DateTime>(sqlCommand);
            // Console.WriteLine(rightNow);

            // Computer myComputer = new Computer()
            // {
            //     Motherboard = "Z690",
            //     HasWifi = true,  
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 954.87m,
            //     VideoCard = "RTX 2060"
            // };

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

            // string sqlSelect = @"SELECT
            //     Motherboard,
            //     HasWifi,  
            //     HasLTE,
            //     ReleaseDate,              
            //     Price,
            //     VideoCard 
            // FROM  TutorialAppSchema.Computer";

            // IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            // IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>();
            // foreach(Computer singleComputer in computers)
            // {
            //     Console.WriteLine("'" + myComputer.Motherboard + "','" + myComputer.HasWifi + "', '" + myComputer.HasLTE + "','" + myComputer.ReleaseDate + "','" + myComputer.Price + "','" + myComputer.VideoCard + "'");
            // }
            // foreach(Computer singleComputer in computersEF
            // )
            // {
            //     Console.WriteLine("'" + singleComputer.ComputerId + "','" + singleComputer.Motherboard + "','" + singleComputer.HasWifi + "', '" + singleComputer.HasLTE + "','" + singleComputer.ReleaseDate + "','" + singleComputer.Price + "','" + singleComputer.VideoCard + "'");
            // }
////////////////////////////////// Serializing and Deserializing Json//////////////////////////////////////////////////////////////////////////
            //string computersJson = File.ReadAllText("Computers.json");
            // Console.WriteLine(computersJson);
            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
            // IEnumerable<Computer>? computerSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson,options);
            // if(computersNewtonsoft != null)
            // {
            //     foreach(Computer computer in computersNewtonsoft)
            //     {
            //        // Console.WriteLine(computer.Motherboard);
            //         string sql = @"INSERT INTO TutorialAppSchema.Computer(
            //             Motherboard,
            //             HasWifi,  
            //             HasLTE,
            //             ReleaseDate,              
            //             Price,
            //             VideoCard  
            //         ) VALUES( '" + EscapeSingleQuote(computer.Motherboard) + "', '" + computer.HasWifi + "', '" + computer.HasLTE + "','" + computer.ReleaseDate.Value.ToString("yyyy-MM-dd") + "','" + computer.Price + "','" + EscapeSingleQuote(computer.VideoCard) + "')";

            //         dapper.ExecuteSql(sql);
            //     }
            // }
            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };
            // string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft,settings);
            // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computerSystem,options);
            // File.WriteAllText("computersCopySystem.txt", computersCopySystem);

/////////////////////////////////////Model Mapping///////////////////////////////////////////////////

////////////////////////////////////AutoMapper//////////////////////////////////////////////////////


            string computersJson = File.ReadAllText("ComputersSnake.json");
            Mapper mapper = new Mapper(new MapperConfiguration((cfg)=>{
                cfg.CreateMap<ComputerSnake,Computer>()
                .ForMember(destination => destination.ComputerId, options =>
                options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.Motherboard, options =>
                options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.CPUCores, options =>
                options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasWifi, options =>
                options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.HasLTE, options =>
                options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.ReleaseDate, options =>
                options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.Price, options =>
                options.MapFrom(source => source.price))
                .ForMember(destination => destination.VideoCard, options =>
                options.MapFrom(source => source.video_card));
            }));
            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);
            if(computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                foreach(Computer computer in computerResult)
                {
                   Console.WriteLine(computer.Motherboard);
                }
            }

/////////////////////////////////////////////////JsonPropertyMapping/////////////////////////////////////////////////////////////

            IEnumerable<Computer>? computersSystemJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);
            if(computersSystemJsonPropertyMapping != null)
            {
                foreach(Computer computer in computersSystemJsonPropertyMapping)
                {
                   Console.WriteLine(computer.Motherboard);
                }
            }
        }
        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'","''");
            return output;
        }
        
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}

