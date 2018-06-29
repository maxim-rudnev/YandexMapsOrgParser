using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YandexMapsOrganizationParser.Models
{
    public class PaymentNotification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        

        public string notification_type { get; set; }

        public string operation_id { get; set; }

        public string label { get; set; }

        public string datetime { get; set; }

        public decimal amount { get; set; }

        public decimal withdraw_amount { get; set; }

        public string sender { get; set; }

        public string sha1_hash { get; set; }

        public string currency { get; set; }

        public bool codepro { get; set; }
    }
}