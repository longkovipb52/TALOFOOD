using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Common;
using LTW_FFOOD.Models;
using PayPal.Api;

namespace LTW_FFOOD.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan

        public ActionResult GioHang()
        {
            if (Session["UserID"] == null)
            {
                // Chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login2", "DangNhap2");
            }
            if (Session["CartModel"] == null)
            {
                return View(new CartModel());
            }
            CartModel cart = Session["CartModel"] as CartModel;
            return View(cart);
        }

        public ActionResult Payment(string address)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login2", "DangNhap2");
            }
            else
            {
                var listcart = (CartModel)Session["CartModel"];
                Bill bill = new Bill();
                bill.ngaydat = DateTime.Now;
                bill.ngaygiao = DateTime.Now.AddDays(3);
                bill.id_account = (int)Session["UserID"];
                bill.address = address;
                bill.status = "Chưa xử lý";
                bill.address = address;
                int billid;
                using (var db = new LTWEB_FFoodEntities())
                {
                    db.Bill.Add(bill);
                    db.SaveChanges();
                    foreach (var item in listcart.Items)
                    {
                        Bill_info bill_Info = new Bill_info();
                        bill_Info.id_bill = bill.bill_id;
                        bill_Info.id_food = item.foods.food_id;
                        bill_Info.id_account = (int)Session["UserID"];
                        bill_Info.price = item.foods.price;
                        bill_Info.count = item._count;

                        db.Bill_info.Add(bill_Info);
                    }
                    db.SaveChanges();
                    billid = bill.bill_id;
                }
                Session["TempCart"] = (CartModel)Session["CartModel"];
                Session.Remove("CartModel");

                return RedirectToAction("Thanhtoanthanhcong");
            }
        }

        public ActionResult ThanhToanKhongCanDangNhap()
        {
            if (Session["CartModel"] == null)
            {
                return View(new CartModel());
            }
            CartModel cart = Session["CartModel"] as CartModel;
            return View(cart);

        }
        
        [HttpPost]
                
        public ActionResult ThanhToanKhongCanDangNhap(string foodname,string name, string phone, string email, string address, string subject)
        {
            
            
            if (Session["CartModel"] == null)
            {
                return View(new CartModel());
            }
            else
            {
                int soluong = 0;
                decimal thanhtien = 0;
                decimal total = 0;
                decimal gia = 0;
                var listcart = (CartModel)Session["CartModel"];
                foreach (var item in listcart.Items)
                {
                    foodname = item.foods.food_name;
                    soluong = item._count;
                    gia = item.foods.price;
                    thanhtien = (item.foods.price * item._count);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/template/neworder.html"));

                StringBuilder itemsContent = new StringBuilder();

                foreach (var item in listcart.Items)
                {
                    foodname = item.foods.food_name;
                    soluong = item._count;
                    gia = item.foods.price;
                    total += (gia * soluong);
                    thanhtien = (soluong * gia);

                    itemsContent.Append("<tr>");
                    itemsContent.Append("<td>" + foodname + "</td>");
                    itemsContent.Append("<td>" + soluong + "</td>");
                    itemsContent.Append("<td>" + gia + "</td>");
                    itemsContent.Append("<td>" + thanhtien + "</td>");
                    itemsContent.Append("</tr>");
                }

                content = content.Replace("{{CustomerName}}", name);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                content = content.Replace("{{Items}}", itemsContent.ToString());






                string toEmail = "longkovipb52@gmail.com";
                new MailHelper().SendMail(email, "Đơn hàng mới từ Talo Shop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Talo Shop", content);

                Session["TempCart"] = (CartModel)Session["CartModel"];
                Session.Remove("CartModel");

                return RedirectToAction("Thanhtoanthanhcong");
            }
        }

        public ActionResult thanhtoanthanhcong()
        {
          
                return View();
        }
        public ActionResult HienThiHoaDon()
        {
        
            int id_account = (int)Session["UserID"];
            using (var db = new LTWEB_FFoodEntities())
            {
                var bills = db.Bill.Where(b => b.id_account == id_account).ToList();
                var food = db.Food.ToList();
                var billinfo = db.Bill_info.Where(b => b.id_account == id_account).ToList();

                HoaDonViewModel model = new HoaDonViewModel
                {
                    bills = bills,
                    foods = food,

                    billinfos = billinfo
                    
                };
                return View(model);
            }
        }
        public ActionResult XacNhanDonHang()
        {
            
            if (Session["CartModel"] == null )
            {
                return RedirectToAction("GioHang");
            }
          
            return RedirectToAction("Payment");
        }
        public ActionResult FailureView()
        {
            return View();
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            if (Session["UserID"] == null)
            {
                // Chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "DangNhap");
            }
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/ThanhToan/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {

                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("thanhtoanthanhcong");
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
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

         
            CartModel cart = Session["CartModel"] as CartModel;
            var listcart = Session["CartModel"] as CartModel;
            Account account = new Account();
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc 
            foreach(var item in listcart.Items)
            {
                itemList.items.Add(new Item()
                {
                    name = item.foods.food_name,
                    currency = "USD",
                    price = Math.Round(item.foods.price / 23500).ToString(),
                    quantity = item._count.ToString(),
                    sku = item.foods.food_id.ToString()
                });
            }
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = listcart.Items.Sum(s =>Math.Round( s.foods.price / 23500) * s._count).ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = listcart.Items.Sum(s => Math.Round(s.foods.price / 23500) * s._count).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            Bill bill = new Bill();
            bill.ngaydat = DateTime.Now;
            bill.ngaygiao = DateTime.Now.AddDays(3);
            bill.id_account = (int)Session["UserID"];
            bill.status = "Chưa xử lý";
            bill.address = account.address;
            int billid;
            using (var db = new LTWEB_FFoodEntities())
            {
                db.Bill.Add(bill);
                db.SaveChanges();
                foreach (var item in listcart.Items)
                {
                    Bill_info bill_Info = new Bill_info();
                    bill_Info.id_bill = bill.bill_id;
                    bill_Info.id_food = item.foods.food_id;
                    bill_Info.id_account = (int)Session["UserID"];
                    bill_Info.price = item.foods.price;
                    bill_Info.count = item._count;
                    db.Bill_info.Add(bill_Info);
                }
                db.SaveChanges();
                billid = bill.bill_id;
            }
            Session["TempCart"] = (CartModel)Session["CartModel"];
            Session.Remove("CartModel");
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
       


    }

}
