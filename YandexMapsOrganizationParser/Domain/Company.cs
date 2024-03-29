﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YandexMapsOrganizationParser.Domain
{
    public class Company
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }        

        public string Category { get; set; }

        public string Country { get; set; }

        public string Time { get; set; }

        public string Links { get; set; }

        //public List<string> Phones { get; set; } = new List<string>();

        public string Phones { get; set; }

    }
}