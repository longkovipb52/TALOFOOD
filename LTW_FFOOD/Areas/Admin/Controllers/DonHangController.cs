using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTW_FFOOD.Models;
using PagedList;

namespace LTW_FFOOD.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        LTWEB_FFoodEntities db = new LTWEB_FFoodEntities();
        // GET: Admin/DonHang
        public ActionResult donhang(int? page, int? pagesize, int? search)
        {
            if (Session["UserID"] == null)
            {
                // Chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "DangNhap");
            }
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 5;
            }
            var bill = search != null ? db.Bill.Where(a => a.bill_id == search).ToList() : db.Bill.ToList();
            return View(bill.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult EditDonHang(int idbill)
        {
            Bill bill = db.Bill.Where(b => b.bill_id == idbill).FirstOrDefault();
            return View(bill);
        }
        [HttpPost]
        public ActionResult EditDonHang(Bill bill)
        {
            Bill bills = db.Bill.Where(b => b.bill_id == bill.bill_id).FirstOrDefault();
            bills.bill_id = bill.bill_id;
            bills.ngaydat = bill.ngaydat;
            bills.ngaygiao = bill.ngaygiao;
            bills.id_account = bill.id_account;
            bills.address = bill.address;
            bills.status = bill.status;
            db.SaveChanges();
            return RedirectToAction("donhang");
        }
        
        public ActionResult DeleteDonHang(int idbill)
        {
            Bill bill = db.Bill.Where(b => b.bill_id == idbill).FirstOrDefault();
            return View(bill);
        }
        [HttpPost]
        public ActionResult DeleteDonHangConfirmed(int idbill)
        {
            // Tìm tất cả các dòng trong BillInfo liên quan đến Bill
            var billInfos = db.Bill_info.Where(b => b.id_bill == idbill).ToList();

            // Xóa tất cả các dòng tìm thấy
            foreach (var billInfo in billInfos)
            {
                db.Bill_info.Remove(billInfo);
            }

            // Lưu thay đổi
            db.SaveChanges();

            // Tìm và xóa Bill
            var bill = db.Bill.Where(b => b.bill_id == idbill).FirstOrDefault();
            if (bill != null)
            {
                db.Bill.Remove(bill);
                db.SaveChanges();
            }

            return RedirectToAction("donhang"); // hoặc trang bạn muốn chuyển hướng đến
        }


    }
}