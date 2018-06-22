using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YandexMapsOrganizationParser.Models.HomeViewModels;

namespace YandexMapsOrganizationParser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> GetOrganizations(GetOrganizationsDto getOrganizationsDto)
        {
            
            var res = await Kernel.YandexMapsApiClient.GetOrganizations(getOrganizationsDto.city, getOrganizationsDto.category);



           return Json(new { data = JsonConvert.SerializeObject(res) });
        }

    }
}