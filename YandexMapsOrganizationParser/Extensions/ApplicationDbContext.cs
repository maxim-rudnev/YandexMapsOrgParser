using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using YandexMapsOrganizationParser.Models;

namespace YandexMapsOrganizationParser.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static AnonUser InitAnonUser(this ApplicationDbContext db, string userHostAddress, string userHostName, string userAgent, string browser)
        {

            var anonUser = db.AnonUsers.FirstOrDefault(x => x.UserHostAddress == userHostAddress &&
                                                            x.UserHostName == userHostName &&
                                                            x.Browser == browser &&
                                                            x.UserAgent == userAgent);

            if (anonUser == null)
            {
                anonUser = new AnonUser()
                {
                    UserHostAddress = userHostAddress,
                    UserHostName = userHostName,
                    UserAgent = userAgent,
                    Browser = browser,
                    RequstsLeft = Globals.DefaultRequestLeft
                };

                db.AnonUsers.Add(anonUser);

                db.SaveChanges();
            }

            return anonUser;
        }

        public static AnonUser FindAnonUser(this ApplicationDbContext db, HttpContextBase httpContext)
        {

            AnonUser res = null;

            res = db.FindUser(httpContext.Request.UserHostAddress,
                            httpContext.Request.UserHostName,
                            httpContext.Request.UserAgent,
                            httpContext.Request.Browser.Browser);


            return res;
        }

        public static AnonUser FindUser(this ApplicationDbContext db, string userHostAddress, string userHostName, string userAgent, string browser)
        {

            AnonUser res = null;

            res = db.AnonUsers.FirstOrDefault(x => x.UserHostAddress == userHostAddress &&
                                                            x.UserHostName == userHostName &&
                                                            x.Browser == browser &&
                                                            x.UserAgent == userAgent);
            

            return res;
        }
    }
}