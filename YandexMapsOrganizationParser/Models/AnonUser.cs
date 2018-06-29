using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YandexMapsOrganizationParser.Models
{
    // Анонимный пользователь - пользователь который не авторизовался и работает в бесплатном режиме, 
    // по умолчанию количество бесплатных запросов - 10
    public class AnonUser
    {
        [Key, Column(Order = 0)]
        public string UserHostAddress { get; set; }

        [Key, Column(Order = 1)]
        public string UserHostName { get; set; }

        [Key, Column(Order = 2)]
        public string Browser { get; set; }

        [Key, Column(Order = 3)]
        public string UserAgent { get; set; }


        public int RequstsLeft { get; set; } = Globals.DefaultRequestLeft;
        
    }
}