using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Webcuoiky.Models;


namespace Webcuoiky.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        BanHangDB db = new BanHangDB();
        List<cart> cartList = new List<cart>();
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (TempData["cart"] != null)
            {
                int total_price = 0;
                List<cart> li = TempData["cart"] as List<cart>;
                foreach(var item in li)
                {
                    total_price += item.total;
                }
                TempData["total"] = total_price;
            }
            TempData.Keep();
            return View(db.producttables.OrderByDescending(x => x.productid).ToList());
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(tbl_user model)
        {
            using (var context = new BanHangDB())
            {
                context.tbl_user.Add(model);
                context.SaveChanges();
                bool isValid = context.tbl_user.Any(x => x.name == model.name && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.name, false);
                    var data = context.tbl_user.Where(x => x.name == model.name && x.password == model.password);
                    int id = data.FirstOrDefault().id;
                    TempData["userid"] = id;
                    TempData.Keep();
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new BanHangDB())
            {
                bool isValid = context.tbl_user.Any(x => x.name == model.name && x.password == model.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.name, false);
                    var data = context.tbl_user.Where(x => x.name == model.name && x.password == model.password);
                    int id;
                    id = data.FirstOrDefault().id;
                    System.Web.HttpContext.Current.Session["id_user"] = id;
                    TempData["userid"] = id;
                    TempData.Keep();
                    System.Diagnostics.Debug.WriteLine(System.Web.HttpContext.Current.Session["id_user"]);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            return View();

        }




        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult addtocart(int? id)
        {
             producttable producttable = db.producttables.Where(x => x.productid== id).SingleOrDefault();
            return View(producttable);
        }

        [HttpPost]
        public ActionResult addtocart(producttable pi, string qty, int id)
        {
            producttable producttable = db.producttables.Where(x => x.productid == id).SingleOrDefault();

            cart cart = new cart();
            cart.pro_id = producttable.productid;
            cart.price = producttable.productprice;
            cart.qty = Convert.ToInt32(qty);
            cart.total = cart.price* cart.qty;
            cart.pro_name= producttable.productname;
            if (TempData["cart"] == null)
            {
                cartList.Add(cart);
                TempData["cart"] = cartList;
            }
            else
            {
                List<cart> li = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach(cart item in li)
                {
                    if(item.pro_id == producttable.productid)
                    {
                        item.qty += cart.qty;
                        item.total += cart.total;
                        flag = 1;
                    }

                }
                if (flag == 0)
                {
                    li.Add(cart);
                }
                TempData["cart"] = li;
            }
            TempData.Keep();
            return RedirectToAction("Index");
        }

        public ActionResult remove(int? id)
        {
            List<cart> li = TempData["cart"] as List<cart>;
            cart cart= li.Where(x=>x.pro_id == id).SingleOrDefault();
            li.Remove(cart);
            int new_total = 0;
            foreach(var item in li)
            {
                new_total += item.total;
            }
            TempData["total"] = new_total;
            if(li.Count == 0)
            {
                TempData.Remove("cart");
            }
            return RedirectToAction("checkout");
        }

        public ActionResult checkout()
        {
            TempData.Keep();
            return View();
        }

        [HttpPost]
        public ActionResult checkout(tbl_order o)
        {
            int userid;
            userid = (int)TempData["userid"];

            List<cart> li = TempData["cart"] as List<cart>;
            billtable bill = new billtable();
            
            bill.user_id = userid;
            bill.date = System.DateTime.Now.ToString();
            bill.total_bill = (int)TempData["total"];
            db.billtables.Add(bill);
            System.Diagnostics.Debug.WriteLine(bill.user_id);
            db.SaveChanges();
            foreach (var item in li)
            {
               
                tbl_order od = new tbl_order();
                od.productid = item.pro_id;
                od.billid = bill.billid;
                od.date= System.DateTime.Now.ToString();
                od.quantity = item.qty;

                od.total = item.total;
          
                db.tbl_order.Add(od);
                db.SaveChanges();
            }
            TempData.Remove("total");
            TempData.Remove("cart");
            return Redirect("Dashboard");
        }


    }
}