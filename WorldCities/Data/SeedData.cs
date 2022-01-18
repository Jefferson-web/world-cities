using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorldCities.Models;

namespace WorldCities.Data
{
    public class SeedData
    {

        public static async Task SeedCountries(ApplicationDbContext context) {

            if (!context.Countries.Any()) {

                var currentDirectory = Directory.GetCurrentDirectory();

                string filename = currentDirectory + Path.DirectorySeparatorChar + "Source"
                        + Path.DirectorySeparatorChar + "worldcities.xlsx";

                FileInfo fileInfo = new FileInfo(filename);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {

                    ExcelWorksheets worksheets = package.Workbook.Worksheets;

                    var worksheet = worksheets.First();

                    int rows = worksheet.Dimension.End.Row;

                    List<Country> countries = new List<Country>();

                    for (int i = 1; i <= rows; i++)
                    {

                        Country country = new Country();
                        country.Name = worksheet.Cells[i, 5].Value.ToString();
                        country.ISO2 = worksheet.Cells[i, 6].Value.ToString();
                        country.ISO3 = worksheet.Cells[i, 7].Value.ToString();

                        if (!countries.Where(c => c.Name == country.Name).Any()) {
                            countries.Add(country);
                        }

                    }

                    context.Countries.AddRange(countries);
                    await context.SaveChangesAsync();

                }

            }

        }

        public static async Task SeedCities(ApplicationDbContext context) {

            if (!context.Cities.Any()) {

                var currentDirectory = Directory.GetCurrentDirectory();

                string filename = currentDirectory + Path.DirectorySeparatorChar + "Source"
                        + Path.DirectorySeparatorChar + "worldcities.xlsx";

                FileInfo fileInfo = new FileInfo(filename);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {

                    ExcelWorksheets worksheets = package.Workbook.Worksheets;

                    var worksheet = worksheets.First();

                    int rows = worksheet.Dimension.End.Row;

                    List<City> cities = new List<City>();

                    for (int i = 1; i <= rows; i++)
                    {

                        City city = new City();
                        city.CityId = int.Parse(worksheet.Cells[i, 11].Value.ToString());
                        city.Name = worksheet.Cells[i, 1].Value.ToString();
                        city.Code = worksheet.Cells[i, 2].Value.ToString();
                        city.Lat = decimal.Parse(worksheet.Cells[i, 3].Value.ToString());
                        city.Lng = decimal.Parse(worksheet.Cells[i, 4].Value.ToString());

                        string ISO2 = worksheet.Cells[i, 6].Value.ToString();

                        Country country = await context.Countries.Where(c => c.ISO2 == ISO2)
                                .FirstOrDefaultAsync();

                        city.CountryId = country.CountryId;
                        cities.Add(city);

                    }

                    context.AddRange(cities);
                    await context.SaveChangesAsync();

                }

            }

        }

    }
}
