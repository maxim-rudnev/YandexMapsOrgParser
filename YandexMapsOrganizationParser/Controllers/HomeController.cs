using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YandexMapsOrganizationParser.Models;
using YandexMapsOrganizationParser.Models.HomeViewModels;
using YandexMapsOrganizationParser.Extensions;
using YandexMapsOrganizationParser.Kernel;
using YandexMapsOrganizationParser.Domain;
using Microsoft.AspNet.Identity;

namespace YandexMapsOrganizationParser.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            var indexVM = new IndexVM();

            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                var anonUser = _db.InitAnonUser(HttpContext.Request.UserHostAddress,
                                    HttpContext.Request.UserHostName,
                                    HttpContext.Request.UserAgent,
                                    HttpContext.Request.Browser.Browser);

                indexVM.ReqLeft = anonUser.RequstsLeft;


            }


            return View(indexVM);
        }

        



        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> GetOrganizations(GetOrganizationsDto getOrganizationsDto)
        {
            dynamic res = new System.Dynamic.ExpandoObject();


            if (User.Identity.IsAuthenticated)
            {
                string userid = User.Identity.GetUserId();
                var user = _db.Users.FirstOrDefault(x => x.Id == userid);

                if (user.RequestsLeft > 0)
                {
                    res.data = await Kernel.YandexMapsApiClient.GetOrganizations(getOrganizationsDto.city, getOrganizationsDto.category);
                    user.RequestsLeft--;

                    _db.SaveChanges();

                    res.ReqLeft = user.RequestsLeft;
                    res.success = true;
                }
                else
                {
                    res.data = new List<Company>();
                    res.ReqLeft = 0;
                    res.success = false;
                    
                }
            }
            else
            {
                var user = _db.FindAnonUser(HttpContext);

                if (user.RequstsLeft > 0)
                {
                    res.data = await Kernel.YandexMapsApiClient.GetOrganizations(getOrganizationsDto.city, getOrganizationsDto.category);
                    user.RequstsLeft--;
                    res.ReqLeft = user.RequstsLeft;

                    _db.SaveChanges();

                    res.success = true;
                }
                else
                {
                    res.data = new List<Company>();
                    res.ReqLeft = 0;
                    res.success = false;
                }
            }

           return Json(new { data = JsonConvert.SerializeObject(res.data),
                             ReqLeft = res.ReqLeft,
                             success = res.success});
        }

        

    }
}