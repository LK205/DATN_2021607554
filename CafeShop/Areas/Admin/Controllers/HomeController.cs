using CafeShop.Models;
using CafeShop.Models.DTOs;
using CafeShop.Config;
using Microsoft.AspNetCore.Mvc;
using CafeShop.Repository;
using System;

namespace CafeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        AccountRepository _accRepo = new AccountRepository();
        public IActionResult Index()
        {
            Account acc = _accRepo.GetByID(HttpContext.Session.GetInt32("AccountId") ?? 0);
            if (acc == null || acc.Role < 2)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Account = acc;

            DateTime currentDate = DateTime.Now;
            DateTime dateStart = currentDate.AddDays(-30);
            ViewBag.DateStart = dateStart.ToString("yyyy-MM-dd");
            ViewBag.DateEnd = currentDate.ToString("yyyy-MM-dd");

            ViewBag.Month = currentDate.ToString("yyyy-MM");
            ViewBag.Year = currentDate.ToString("yyyy");
            ViewBag.Month1 = currentDate.ToString("MM");
            return View();
        }

        public JsonResult GetTopSale(int topSale, DateTime dateStart, DateTime dateEnd)
        {
            dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, 0,0,0);
            dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day,23,59,59);
            List<ProductDto> data = SQLHelper<ProductDto>.ProcedureToList("spGetTop4BestSale", new string[] { "@topSale", "@DateStart", "@DateEnd" }, new object[] {topSale, dateStart, dateEnd});
            return Json(data);
        }

        public JsonResult GetHardestToSell(int topSale, DateTime dateStart, DateTime dateEnd)
        {
            dateStart = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, 0, 0, 0);
            dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, 23, 59, 59);
            List<ProductDto> data = SQLHelper<ProductDto>.ProcedureToList("spGetHardestToSell", new string[] { "@topSale", "@DateStart", "@DateEnd" }, new object[] { topSale, dateStart, dateEnd });
            return Json(data);
        }


        public JsonResult GetPuchase(int month, int year)
        {
            List<PuchaseDto> data = SQLHelper<PuchaseDto>.ProcedureToList("spGetTotalPuchase", new string[] { "@Month", "@Year" }, new object[] { month, year });
            return Json(data);
        }

        public JsonResult GetAllInformationOrder()
        {
            OrderHomeDTO data = SQLHelper<OrderHomeDTO>.ProcedureToModel("spGetTotalOrderForMessage", new string[] { }, new object[] { });
            return Json(data);
        }
    }

}
