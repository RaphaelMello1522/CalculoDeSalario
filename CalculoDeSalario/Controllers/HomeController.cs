using CalculoDeSalario.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PayPal.Api;
using System.Diagnostics;

namespace CalculoDeSalario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IToastNotification _toastNotification;
        IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, IConfiguration configuration, IToastNotification toastNotification)
        {
            _logger = logger;
            httpContextAccessor = context;
            _configuration = configuration;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            _toastNotification.AddInfoToastMessage("Bem vindo de volta!");

            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerId = "", string guid = "")
        {
            //Chamando a apiContext
            var ClientId = _configuration.GetValue<string>("Paypal:Key");
            var ClientSecret = _configuration.GetValue<string>("Paypal:Secret");
            var mode = _configuration.GetValue<string>("Paypal:mode");

            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientId, ClientSecret, mode);

            try
            {
                string payerId = PayerId;

                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Home/PaymentWithPaypal?";
                    var guidd = Convert.ToString((new Random()).Next(100000));
                    guid = guidd;

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    httpContextAccessor.HttpContext.Session?.SetString("payment", createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("PaymentFailed");
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;

                    return View("PaymentSuccess");
                }
            }
            catch (Exception ex)
            {
                return View("PaymentFailed");
            }

            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            this.payment = new Payment()
            {
                id = paymentId
            };

            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };

            itemList.items.Add(new Item()
            {
                name = "Item detail",
                currency = "BRL",
                price = "1.00",
                quantity = "1",
                sku = "asd"
            });

            var payer = new Payer()
            {
                payment_method = "paypal"
            };

            var redirectUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            //var details = new Details()
            //{
            //    tax = "1",
            //    shipping = "2",
            //    subtotal = "1"
            //};

            var amount = new Amount()
            {
                currency = "BRL",
                total = "1.00"
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction Description",
                invoice_number = Guid.NewGuid().ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            return this.payment.Create(apiContext);

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}