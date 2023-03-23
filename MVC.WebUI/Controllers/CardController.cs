using MVC.WebUI.Entity;
using MVC.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.WebUI.Controllers
{
    public class CardController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Card
        public ActionResult Index()
        {
            return View(GetCard());
        }

        public ActionResult AddToCard(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCard().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }


        public ActionResult RemoveFromCard(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCard().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public Card GetCard()
        {
            Card card = (Card)Session["Card"];

            if (card == null)
            {
                card = new Card();
                Session["Card"] = card;
            }
            return card;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCard());
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var card = GetCard();

            if(card.CardLines.Count == 0)
            {
                ModelState.AddModelError("ProductError", "There are no products in your cart.");
            }
            
            // Yarın siparişi veritabına kayıt etme bölümünü yapmayo unutma.

            if(ModelState.IsValid)
            {
                SaveOrder(card, entity);
                card.Clear();
                return View("Completed");
            }
            else
            {
                return View(entity);
            }
        }

        private void SaveOrder(Card card, ShippingDetails entity)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = card.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.Username = User.Identity.Name;

            order.AddressTitle = entity.AddressTitle;
            order.Address = entity.Address;
            order.City = entity.City;
            order.District = entity.District;
            order.PostCode = entity.PostCode;
            order.OrderLines = new List<OrderLine>();

            foreach (var pr in card.CardLines)
            {
                OrderLine orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;

                order.OrderLines.Add(orderline);
            }

            db.Orders.Add(order);
            db.SaveChanges();

        }
    }
}