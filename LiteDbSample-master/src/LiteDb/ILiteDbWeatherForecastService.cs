using System.Collections.Generic;

namespace LiteDbSample.LiteDb
{
    public interface ILiteDbWeatherForecastService
    {
        int Delete(int id);
        IEnumerable<WeatherForecast> FindAll();
        WeatherForecast FindOne(int id);
        int Insert(WeatherForecast forecast);
        bool Update(WeatherForecast forecast);
    }
    public interface ILiteDbGoldenService
    {
        int Delete(int id);
        IEnumerable<Golden> FindAll();
        Golden FindOne(int id);
        int Insert(Golden forecast);
        bool Update(Golden forecast);
    }
}
