using AdPlatform.Services;
using AdPlatform.Models;
using Microsoft.AspNetCore.Http; 
using System.Reflection;

namespace AdPlatform.Tests
{
    public class AdServiceTest
    {
        [Fact]
        public void FindPlatformsForLocation_ShouldReturnCorrectPlatforms()
        {
            var service = new AdService();
            var platforms = new List<AdsPlatform>
            {
                new AdsPlatform { Name = "яндекс.ƒирект", Locations = new List<string> {  "/ru" } } ,
                new AdsPlatform { Name = "–евдинский рабочий",Locations = new List<string>{ "/ru/svrd/revda", "/ru/svrd/pervik" }  },
                new AdsPlatform { Name = "√азета уральских москвичей", Locations = new List<string>{ "/ru/msk", "/ru/permobl", "/ru/chelobl" } },
                new AdsPlatform { Name = " рута€ реклама", Locations = new List<string>{ "/ru/svrd" } }
            };
            service.SetTestData(platforms);
            // ”станавливаем данные в сервис через рефлексию
            SetPrivateField(service, "_platforms", platforms);

            // Act
            var resultForMsk = service.FindPlatformsForLocation("/ru/msk");
            var resultForSvrD = service.FindPlatformsForLocation("/ru/svrd");
            var resultForRevda = service.FindPlatformsForLocation("/ru/svrd/revda");
            var resultForRu = service.FindPlatformsForLocation("/ru");

            // Assert
            // ƒл€ /ru/msk
            Assert.Contains("яндекс.ƒирект", resultForMsk);
            Assert.Contains("√азета уральских москвичей", resultForMsk);
            Assert.Equal(2, resultForMsk.Count);

            // ƒл€ /ru/svrd
            Assert.Contains("яндекс.ƒирект", resultForSvrD);
            Assert.Contains(" рута€ реклама", resultForSvrD);
            Assert.Equal(2, resultForSvrD.Count);

            // ƒл€ /ru/svrd/revda
            Assert.Contains("яндекс.ƒирект", resultForRevda);
            Assert.Contains("–евдинский рабочий", resultForRevda);
            Assert.Contains(" рута€ реклама", resultForRevda);
            Assert.Equal(3, resultForRevda.Count);

            // ƒл€ /ru
            Assert.Contains("яндекс.ƒирект", resultForRu);
            Assert.Single(resultForRu); // “олько яндекс.ƒирект
        }

        // ¬спомогательный метод дл€ установки приватного пол€ через рефлексию
        private void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(obj, value);
        }
    }
        
    
}