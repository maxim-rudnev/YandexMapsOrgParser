using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YandexMapsOrganizationParser.Models;
using YandexMapsOrganizationParser.Models.PaymentViewModels;

namespace YandexMapsOrganizationParser.Controllers
{
    public class PaymentController : Controller
    {
        ApplicationDbContext _db;

        public PaymentController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order(decimal sum)
        {
            Order order = _db.Orders.FirstOrDefault();

            if (order == null) order = new Order() { Sum = sum };

            OrderVM orderModel = new OrderVM { OrderId = order.Id, Sum = order.Sum };
            return View(orderModel);
            
        }

        [HttpGet]
        public string Paid()
        {
            return "<p>заказ оплачен</p>";
        }

        [HttpPost]
        public void Paid(string notification_type, string operation_id, string label, string datetime,
        decimal amount, decimal withdraw_amount, string sender, string sha1_hash, string currency, bool codepro)
        {
            string key = "eBy4PpMKwAWIR3o5Fm/V6yDg"; // секретный код
                                             // проверяем хэш
            string paramString = String.Format("{0}&{1}&{2}&{3}&{4}&{5}&{6}&{7}&{8}",
                notification_type, operation_id, amount, currency, datetime, sender,
                codepro.ToString().ToLower(), key, label);
            string paramStringHash1 = GetHash(paramString);
            // создаем класс для сравнения строк
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            // если хэши идентичны, добавляем данные о заказе в бд
            if (0 == comparer.Compare(paramStringHash1, sha1_hash))
            {
                PaymentNotification nt = new PaymentNotification();
                nt.notification_type = notification_type;
                nt.operation_id = operation_id;
                nt.label = label;
                nt.datetime = datetime;
                nt.amount = amount;
                nt.withdraw_amount = withdraw_amount;
                nt.sender = sender;
                nt.sha1_hash = sha1_hash;
                nt.currency = currency;
                nt.codepro = codepro;


                _db.Payments.Add(nt);
                _db.SaveChanges();


                //Order order = db.Orders.FirstOrDefault(o => o.Id == label);
                //order.Operation_Id = operation_id;
                //order.Date = DateTime.Now;
                //order.Amount = amount;
                //order.WithdrawAmount = withdraw_amount;
                //order.Sender = sender;
                //db.Entry(order).State = EntityState.Modified;
                //db.SaveChanges();
            }


            PaymentNotification nt1 = new PaymentNotification();
            nt1.notification_type = notification_type;
            nt1.operation_id = operation_id;
            nt1.label = label;
            nt1.datetime = datetime;
            nt1.amount = amount;
            nt1.withdraw_amount = withdraw_amount;
            nt1.sender = sender;
            nt1.sha1_hash = sha1_hash;
            nt1.currency = currency;
            nt1.codepro = codepro;


            _db.Payments.Add(nt1);
            _db.SaveChanges();


        }
        public string GetHash(string val)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] data = sha.ComputeHash(Encoding.Default.GetBytes(val));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}