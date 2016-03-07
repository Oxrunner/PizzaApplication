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

        [Authorize]
        public ActionResult Index()
        {
            Basket basket = new Basket();
            if (Request["collectionDelivery"] != null)
            {
                basket.setDeliveryCollection(Request["collectionDelivery"]);
            }
            ViewBag.Basket = basket;
            return View();
        }

        [Authorize]
        public ActionResult SaveOrder()
        {
            Basket basket = new Basket();
            basket.saveOrder(User.Identity.GetUserId());
            return RedirectToAction("Index", "Pizza");
        }

        [Authorize]
        public ActionResult ApplyVoucher()
        {
            Basket basket = new Basket();
            if (Request["voucherCode"] != null)
            {
                basket.applyVoucher(Request["voucherCode"]);
                basket = new Basket();
            }
            ViewBag.Basket = basket;
            return View("Index");
        }

        [Authorize]
        public ActionResult RetriveOrder()
        {
            Basket basket = new Basket();
            basket.retriveOrder(User.Identity.GetUserId());
            return RedirectToAction("Index", "Pizza");
        }

        [Authorize]
        public ActionResult PlaceOrder()
        {
            Basket basket = new Basket();
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
            Debug.Write(Request["requestUrl"]+"\n\n\n");
            Basket basket = new Basket();
            if (Request["submit"] == "Add To Basket")
            {
                basket.addToBasket(Request["size"], Int32.Parse(Request["item.PizzaId"].ToString()));
            }
            else if (Request["submit"] == "Remove")
            {
                basket.removeOrderFromBasket(Int32.Parse(Request["pizza.OrderPizzaId"].ToString()));
            }
            if (Request["requestUrl"] == "/Order" || Request["requestUrl"] == "/Order/Index")
            {
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("Index", "Pizza");
        }
    }
}