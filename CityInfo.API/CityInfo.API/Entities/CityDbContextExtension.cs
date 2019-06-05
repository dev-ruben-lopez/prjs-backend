using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public static class CityDbContextExtension
    {

        public static void EnsureSeedDataCreatedForContextDB(this CityDbContext context)
        {
            if(context.Cities.Any())
            { return; }

            var testPointsOfInterest = new Faker<PointOfInterest>()
               //.RuleFor(p => p.PointId, f => f.IndexFaker)
               .RuleFor(p => p.PointName, f => f.Lorem.Word())
               .RuleFor(p => p.PointDescription, f => f.Lorem.Paragraph(1));


            var testCitiesDto = new Faker<City>()
                //.RuleFor(c => c.CityId, f => f.IndexFaker)
                .RuleFor(c => c.CityName, f => f.Name.FirstName())
                .RuleFor(c => c.CityDescription, f => f.Lorem.Paragraph(1))
                .RuleFor(c => c.PointOfInterest, f => testPointsOfInterest.Generate(3));

            context.AddRange(testCitiesDto.Generate(5));
            context.SaveChanges();


        }
    }
}
