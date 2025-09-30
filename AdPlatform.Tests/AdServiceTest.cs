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
                new AdsPlatform { Name = "������.������", Locations = new List<string> {  "/ru" } } ,
                new AdsPlatform { Name = "���������� �������",Locations = new List<string>{ "/ru/svrd/revda", "/ru/svrd/pervik" }  },
                new AdsPlatform { Name = "������ ��������� ���������", Locations = new List<string>{ "/ru/msk", "/ru/permobl", "/ru/chelobl" } },
                new AdsPlatform { Name = "������ �������", Locations = new List<string>{ "/ru/svrd" } }
            };
            service.SetTestData(platforms);
            // ������������� ������ � ������ ����� ���������
            SetPrivateField(service, "_platforms", platforms);

            // Act
            var resultForMsk = service.FindPlatformsForLocation("/ru/msk");
            var resultForSvrD = service.FindPlatformsForLocation("/ru/svrd");
            var resultForRevda = service.FindPlatformsForLocation("/ru/svrd/revda");
            var resultForRu = service.FindPlatformsForLocation("/ru");

            // Assert
            // ��� /ru/msk
            Assert.Contains("������.������", resultForMsk);
            Assert.Contains("������ ��������� ���������", resultForMsk);
            Assert.Equal(2, resultForMsk.Count);

            // ��� /ru/svrd
            Assert.Contains("������.������", resultForSvrD);
            Assert.Contains("������ �������", resultForSvrD);
            Assert.Equal(2, resultForSvrD.Count);

            // ��� /ru/svrd/revda
            Assert.Contains("������.������", resultForRevda);
            Assert.Contains("���������� �������", resultForRevda);
            Assert.Contains("������ �������", resultForRevda);
            Assert.Equal(3, resultForRevda.Count);

            // ��� /ru
            Assert.Contains("������.������", resultForRu);
            Assert.Single(resultForRu); // ������ ������.������
        }

        // ��������������� ����� ��� ��������� ���������� ���� ����� ���������
        private void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName,
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(obj, value);
        }
    }
        
    
}