using PizzaApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PizzaApplication.Controllers
{
    public class OrderController : Controller
    {
        PizzaStoreContext context = new PizzaStoreContext();

        [Authorize]
        public ActionResult Index()
        {
            Basket basket = new Basket(context);
            if (Request["submit"] == "Apply Voucher")
            {
                if (Request["voucherCode"] != null)
                {
                    ViewBag.VoucherMessage = basket.applyVoucher(Request["voucherCode"]);
                }
            }
            else if (Request["collectionDelivery"] != null)
            {
                basket.setDeliveryCollection(Request["collectionDelivery"]);
            }
            basket = new Basket(context);
            ViewBag.Basket = basket;
            return View();
        }

        [Authorize]
        public ActionResult SaveOrder()
        {
            Basket basket = new Basket(context);
            basket.saveOrder(User.Identity.GetUserId());
            return RedirectToAction("Index", "Pizza");
        }

        [Authorize]
        public ActionResult RetriveOrder()
        {
            Basket basket = new Basket(context);
            basket.retriveOrder(User.Identity.GetUserId());
            return RedirectToAction("Index", "Pizza");
        }

        [Authorize]
        public ActionResult PlaceOrder()
        {
            Basket basket = new Basket(context);
            if (basket.pizzasInBasket.Count <= 0)
            {
                return RedirectToAction("Index", "Pizza");
            }
            basket.submitOrder(User.Identity.GetUserId());
            return View();
        }

        [HttpPost]
        public ActionResult UpdateBasket()
        {
            Basket basket = new Basket(context);
            if (Request["submit"] == "Add To Basket")
            {
                basket.addToBasket(Request["size"], Int32.Parse(Request["item.PizzaId"].ToString()), Request["toppings"]);
            }
            else if (Request["submit"] == "Remove")
            {
                basket.removeOrderFromBasket(Int32.Parse(Request["OrderPizzaId"].ToString()));
            }
            if (Request["requestUrl"] == "/Order" || Request["requestUrl"] == "/Order/Index")
            {
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Pizza");
        }
    }
}