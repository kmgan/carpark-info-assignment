using System;
using System.Diagnostics;
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
                Debug.WriteLine("Starting CSV Processing...");

                var projectPath = Directory.GetCurrentDirectory(); 
                var rootPath = Directory.GetParent(projectPath).Parent.Parent.Parent.FullName; 

                var csvFilePath = Path.Combine(rootPath, "hdb-carpark-information-20220824010400.csv");

                if (!File.Exists(csvFilePath))
                {
                    Debug.WriteLine($"CSV file not found at path: {csvFilePath}");
                    return;
                }

                var dbContext = new CarparkDbContext();

                var batchJob = new CarparkInfoBatchJob(dbContext);
                batchJob.BatchProcess(csvFilePath);

                Debug.WriteLine("CSV Processing Complete.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
