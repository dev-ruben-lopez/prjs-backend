using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;

namespace CityInfo.API.DataRepository
{
    public interface ICityInfoRepository
    {
        City GetCity(int cityId);
        IEnumerable<City> GetCities();
        PointOfInterest GetPointOfInterest(int pointId);
        IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId);


    }
}
