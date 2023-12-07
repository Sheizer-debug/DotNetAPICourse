
using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data{
    public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Computer>? Computer{get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
            //.HasNoKey();
            //.HasKey(c => c.Motherboard);
            //.ToTable("Computer","TutorialAppSchema");//tableName,schemaName
        }
        // private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        // public IEnumerable<T> LoadData<T>(string sql)
        // {
        //     IDbConnection dbConnection = new SqlConnection(_connectionString);
        //     return dbConnection.Query<T>(sql);
        // }

        // public T LoadDataSingle<T>(string sql)
        // {
        //     IDbConnection dbConnection = new SqlConnection(_connectionString);
        //     return dbConnection.QuerySingle<T>(sql);
        // }
        // public bool ExecuteSql(string sql)
        // {
        //     IDbConnection dbConnection = new SqlConnection(_connectionString);
        //     return dbConnection.Execute(sql) > 0;
        // }

        // public int ExecuteSqlWithRowCount(string sql)
        // {
        //     IDbConnection dbConnection = new SqlConnection(_connectionString);
        //     return dbConnection.Execute(sql);
        // }
    }
}