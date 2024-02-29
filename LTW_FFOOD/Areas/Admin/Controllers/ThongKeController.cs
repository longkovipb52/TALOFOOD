    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using LTW_FFOOD.Models;
    namespace LTW_FFOOD.Areas.Admin.Controllers
    {
        public class ThongKeController : Controller
        {
        // GET: Admin/ThongKe

        public ActionResult thongke()
        {
            using (var db = new LTWEB_FFoodEntities())
            {
                var accounts = db.Account.ToList();
                var billinfos = db.Bill_info.ToList();

                List<KhachHangDonHangViewModel> models = new List<KhachHangDonHangViewModel>();

                foreach (var account in accounts)
                {
                    var accountBillInfos = billinfos.Where(b => b.id_account == account.account_id);
                    var model = new KhachHangDonHangViewModel
                    {
                        TenKhachHang = account.username,
                        DiaChi = account.address,
                        SoDienThoai = account.phone,
                        SoLuongHang = accountBillInfos.Sum(b => b.count),
                        TongTien = accountBillInfos.Sum(b => b.price * b.count)
                    };
                    models.Add(model);
                }

                return View(models);
            }
        }


    }
}