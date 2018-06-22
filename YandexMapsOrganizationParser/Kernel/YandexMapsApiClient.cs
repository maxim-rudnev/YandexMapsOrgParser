using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace YandexMapsOrganizationParser.Kernel
{
    public class YandexMapsApiClient
    {

        public static async Task<List<Company>> GetOrganizations(string city, string category)
        {
            List<Company> res = new List<Company>();

            using (var httpClient = new HttpClient())
            {

                string apiRequestUri = string.Format("https://search-maps.yandex.ru/v1/?text={0}&type=biz&lang=ru_RU&apikey={1}&results=500",
                                                    Uri.EscapeDataString( category + "," + city),
                                                    Uri.EscapeDataString("7ec44f0e-e27b-4cf9-9868-7139430038de"));

                HttpResponseMessage response = await httpClient.GetAsync(apiRequestUri);

                response.EnsureSuccessStatusCode();

                string text = await response.Content.ReadAsStringAsync();

                int resultsCount = (int)JsonConvert.DeserializeObject<dynamic>(text)["properties"]["ResponseMetaData"]["SearchResponse"]["found"];
                dynamic JsonResponse = JsonConvert.DeserializeObject<dynamic>(text)["features"];
                
                foreach (var company in JsonResponse)
                {
                    Company c = new Company();

                    c.Name = company["properties"]["CompanyMetaData"].name;
                    c.Address = company["properties"]["CompanyMetaData"].address;

                    try
                    {
                        foreach (var phone in company["properties"]["CompanyMetaData"].Phones)
                        {
                            //c.Phones.Add((string)phone["formatted"]);

                            c.Phones += (string)phone["formatted"] + " ";
                        }
                    }
                    catch { }

                    res.Add(c);
                }

            }


            return res;
        }
    }
}