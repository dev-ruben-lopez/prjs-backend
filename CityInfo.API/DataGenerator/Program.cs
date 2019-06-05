using Bogus;
using System;
using CityInfo.API.Models;
using Newtonsoft.Json;
using System.IO;

namespace DataGenerator
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //Define the data rules for the generation

            var testPointsOfInterest = new Faker<PointsOfInterestDto>()
                .RuleFor(p => p.Id, f => f.IndexFaker)
                .RuleFor(p => p.Name, f => f.Lorem.Word())
                .RuleFor(p => p.Description, f => f.Lorem.Paragraph(1));

            //var testPointsOfInterestList = new Faker


            var testCitiesDto = new Faker<CityDto>()
                .RuleFor(c => c.Id, f => f.IndexFaker)
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Description, f => f.Lorem.Paragraph(1))
                .RuleFor(c => c.PointOfIntererest, f => testPointsOfInterest.Generate(3))
                .FinishWith((f,c)=> Console.WriteLine($"City Created. ID={c.Id}" ));



            var cityFaked = testCitiesDto.Generate(30);


            var jFile = JsonConvert.SerializeObject(cityFaked, Formatting.Indented);
            var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "CitiesFakeData.json");
            File.WriteAllText(fileLocation, jFile);

            Console.ReadKey();
        }





    }
}
