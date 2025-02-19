using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CarparkInfoBatchJob
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dbContext = new CarparkDbContext();
                dbContext.Database.EnsureCreated();

                var projectPath = AppDomain.CurrentDomain.BaseDirectory;
                var rootPath = Directory.GetParent(projectPath).Parent.Parent.Parent.Parent.FullName;
                var csvFilePath = Path.Combine(rootPath, "hdb-carpark-information-20220824010400.csv");

                var batchJob = new CarparkInfoBatchJob(dbContext);
                batchJob.BatchProcess(csvFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}