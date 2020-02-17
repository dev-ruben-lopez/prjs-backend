using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API.DataRepository
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityDbContext _dbContext;

        public CityInfoRepository(CityDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        public City GetCity(int cityId)
        {
            return _dbContext.Cities.FirstOrDefault(c => c.CityId == cityId);
        }


        public IEnumerable<City> GetCities()
        {
            return _dbContext.Cities.ToList();
        }

        public PointOfInterest GetPointOfInterest(int pointId)
        {
            return _dbContext.PointOfInterest.FirstOrDefault(p => p.PointId == pointId);
        }


        public IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId)
        {
            return _dbContext.PointOfInterest.Where(p=>p.CityId == cityId).ToList();
        }


    }
}
