using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTW_FFOOD.Models
{
    public class HoaDonViewModel
    {
        public List<Bill> bills { get; set; }
        public Bill bill { get; set; }
        public List<Bill_info> billinfos { get; set; }
        public Bill_info billinfo { get; set; }
        public List<LTW_FFOOD.Models.Food> foods { get; set; }
        public LTW_FFOOD.Models.Food food { get; set; }
        public LTW_FFOOD.Models.Account account { get; set; }
        public List<Account> accounts { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItemCount / PageSize);
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public int SoLuongHang { get; set; }
        public decimal TongTien { get; set; }
    }
}