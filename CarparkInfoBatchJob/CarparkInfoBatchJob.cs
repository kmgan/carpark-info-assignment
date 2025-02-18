using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;
using System.IO;
using System.Linq;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace CarparkInfoBatchJob
{
    public class CarparkInfoBatchJob
    {
        private readonly CarparkDbContext _dbContext;

        public CarparkInfoBatchJob(CarparkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public class CustomBooleanConverter : BooleanConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (text == "YES" || text == "Y")
                {
                    return true;
                }
                if (text == "NO" || text == "N")
                {
                    return false;
                }
                return base.ConvertFromString(text, row, memberMapData);
            }
        }

        public List<Carpark> ReadCSVFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.WriteLine("The specified CSV file does not exist: " + filePath);
                return new List<Carpark>();
            }

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.TypeConverterCache.AddConverter<bool>(new CustomBooleanConverter());
                var records = csv.GetRecords<Carpark>().ToList();

                if (records.Count == 0)
                {
                    Debug.WriteLine("No records were read from the CSV file.");
                    return new List<Carpark>();
                }

                return records;
            }
        }

        public List<Carpark> ValidateRecords(List<Carpark> records)
        {
            var validRecords = new List<Carpark>();

            foreach (var record in records)
            {
                if (IsValidRecord(record))
                {
                    validRecords.Add(record);
                }
                else
                {
                    Debug.WriteLine($"Invalid record: {record.car_park_no}");
                    throw new InvalidOperationException($"Invalid record: {record.car_park_no}. Aborting file processing.");
                }
            }

            return validRecords;
        }

        private static bool IsBool(bool value)
        {
            return value == true || value == false;
        }

        private static bool IsValidRecord(Carpark record)
        {
            return !string.IsNullOrEmpty(record.car_park_no) &&
                   !string.IsNullOrEmpty(record.address) &&
                   !string.IsNullOrEmpty(record.car_park_type) &&
                   !string.IsNullOrEmpty(record.type_of_parking_system) &&
                   !string.IsNullOrEmpty(record.short_term_parking) &&
                   !string.IsNullOrEmpty(record.free_parking) &&
                   record.car_park_decks >= 0 &&
                   record.gantry_height >= 0 &&
                   IsBool(record.night_parking) &&  
                   IsBool(record.car_park_basement); 
        }

        public void SaveRecords(List<Carpark> validRecords)
        {
            try
            {
                _dbContext.Carparks.AddRange(validRecords);
                _dbContext.SaveChanges();
                Debug.WriteLine("Records saved successfully.");
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine($"Error saving records: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving records: {ex.Message}");
            }
        }

        public void BatchProcess(string filePath)
        {
            var records = ReadCSVFile(filePath);
            var validRecords = ValidateRecords(records);
            SaveRecords(validRecords);
        }
    }
}
