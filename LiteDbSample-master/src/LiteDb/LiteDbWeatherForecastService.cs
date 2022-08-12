using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteDbSample.LiteDb
{
    public class LiteDbWeatherForecastService : ILiteDbWeatherForecastService
    {

        private LiteDatabase _liteDb;

        public LiteDbWeatherForecastService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<WeatherForecast> FindAll()
        {
            var result = _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .FindAll();
            return result;
        }

        public WeatherForecast FindOne(int id)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(WeatherForecast forecast)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Insert(forecast);
        }

        public bool Update(WeatherForecast forecast)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Update(forecast);
        }

        public int Delete(int id)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Delete(x => x.Id == id);
        }
    }

    public class LiteDbGoldenService : ILiteDbGoldenService
    {

        private LiteDatabase _liteDb;

        public LiteDbGoldenService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<Golden> FindAll()
        {
            var result = _liteDb.GetCollection<Golden>("Golden")
                .FindAll();
            return result;
        }

        public Golden FindOne(int id)
        {
            return _liteDb.GetCollection<Golden>("Golden")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(Golden forecast)
        {
            return _liteDb.GetCollection<Golden>("Golden")
                .Insert(forecast);
        }

        public bool Update(Golden forecast)
        {
            return _liteDb.GetCollection<Golden>("Golden")
                .Update(forecast);
        }

        public int Delete(int id)
        {
            return _liteDb.GetCollection<Golden>("Golden")
                .Delete(x => x.Id == id);
        }
    }

}
