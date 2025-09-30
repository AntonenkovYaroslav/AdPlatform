using AdPlatform.Models;
using Microsoft.AspNetCore.Http;
namespace AdPlatform.Services
{
    public interface IAdService
    {
        void LoadPlatformsFromFile(IFormFile file);
        List<string> FindPlatformsForLocation(string location);
    }
    public class AdService : IAdService
    {
        private List<AdsPlatform> _platforms = new List<AdsPlatform>();
        public void LoadPlatformsFromFile(IFormFile file) 
        {
            if (file == null || file.Length == 0 )
            {
                throw new ArgumentException("файл пустой");
            }
            var newPlatform = new List<AdsPlatform>();
            using (var reader = new StreamReader(file.OpenReadStream())) 
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Пропускаем пустые строки
                    if (string.IsNullOrWhiteSpace(line)) 
                        continue;
                    try
                    {   //Разделяем строку на части по двоеточию
                        var parts = line.Split(':');
                        if (parts.Length < 2)
                        {
                            continue;
                        }
                        var platformName = parts[0].Trim();
                        var locationsString = parts[1].Trim();

                        // Разделяем локации по запятой и удаляем лишние пробелы
                        var locations = locationsString.Split(',')
                                                       .Select(loc => loc.Trim())
                                                       .Where(loc => !string.IsNullOrWhiteSpace(loc))
                                                       .ToList();
                        if (!string.IsNullOrEmpty(platformName) && locations.Any())
                        {
                            newPlatform.Add(new AdsPlatform
                            {
                                Name = platformName,
                                Locations = locations
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        //игнорирует неверные строки и продолжает
                        Console.WriteLine($"Ошибка обработки строки: {line}. Ошибка: {ex.Message}");
                    }
                }
            }

            //замена старых платформ на новые
            _platforms = newPlatform;

        }



        private bool IsLocationMatch(string targetLocation, string platformLocation)
        {


            return targetLocation.StartsWith(platformLocation);
                   
        }
        public List<string> FindPlatformsForLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
               return new List<string>();
            }
            var result = new List<string>();
            foreach (var platform in _platforms)
            {
                foreach(var platformLocation in platform.Locations)
                {
                    //подходит ли локация 
                    if (IsLocationMatch(location, platformLocation))
                    {
                        result.Add(platform.Name);
                        break;
                    }
                }
            }
            return result.Distinct().ToList();
           
        }
        // Метод для установки тестовых данных 
        public void SetTestData(List<AdsPlatform> platforms)
        {
            _platforms = platforms;
        }
    }
}
