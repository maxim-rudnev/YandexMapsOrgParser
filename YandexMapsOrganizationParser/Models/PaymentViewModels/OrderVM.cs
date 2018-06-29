using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YandexMapsOrganizationParser.Models.PaymentViewModels
{
    public class OrderVM
    {
        public string OrderId { get; set; }

        public decimal Sum { get; set; }
    }
}