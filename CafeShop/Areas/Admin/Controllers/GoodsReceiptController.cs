using CafeShop.Config;
using CafeShop.Models.DTOs;
using CafeShop.Models;
using CafeShop.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CafeShop.Reposiory;
using System.Xml;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CafeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoodsReceiptController : Controller
    {
        AccountRepository _accRepo = new AccountRepository();
        MaterialRepository _materialRepo = new MaterialRepository();
        GoodsReceiptRepository _receipttRepo = new GoodsReceiptRepository();
        GoodsReceiptDetailRepository _receiptDetail = new GoodsReceiptDetailRepository();
        SupplierRepository _supplierRepo = new SupplierRepository();

        public IActionResult Index()
        {
            Account acc = _accRepo.GetByID(HttpContext.Session.GetInt32("AccountId") ?? 0);
            if (acc == null || acc.Role != 2)
            {
                return Redirect("/Home/Index");
            }

            DateTime currentDate = DateTime.Now;
            DateTime dateStart = new DateTime(currentDate.Year, currentDate.Month, 1);
            ViewBag.DateStart = dateStart.ToString("yyyy-MM-dd");
            ViewBag.DateEnd = dateStart.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            ViewBag.CurrentDate = currentDate.ToString("yyyy-MM-dd");


            List<int> withoutRoleAccount = new List<int>() { 1, 2 };
            List<Account> lstAccounts = _accRepo.GetAll().ToList();
            ViewBag.LstAccount = new SelectList(lstAccounts.Where(x => x.IsDelete != true && !withoutRoleAccount.Contains(x.Role)).ToList(), "Id", "FullName");
            ViewBag.Account = acc;

            List<Supplier> lstSupplier = _supplierRepo.GetAll().ToList();
            ViewBag.ListSupplier = new SelectList(lstSupplier.Where(x => x.IsDelete != true).ToList(), "Id", "SupplierName"); ;


            return View();
        }


        [HttpPost]
        public JsonResult GetAll([FromBody] GoodReceiptRequestDTO dto)
        {
            dto.DateStart = new DateTime(dto.DateStart.Year, dto.DateStart.Month, dto.DateStart.Day, 0, 0, 0);
            dto.DateEnd = new DateTime(dto.DateEnd.Year, dto.DateEnd.Month, dto.DateEnd.Day, 23, 59, 59);


            DataSet ds = LoadDataFromSP.GetDataSetSP("spGetAllGoodsReceipt", new string[] { "@PageNumber", "@Request", "@DateStart", "@DateEnd", "@AccountID" }
                                                                           , new object[] { dto.PageNumber, TextUtils.ToString(dto.Request), dto.DateStart, dto.DateEnd, dto.AccountID });

            var data = TextUtils.ConvertDataTable<GoodReceiptResponeDTO>(ds.Tables[0]);
            var totalCount = TextUtils.ConvertDataTable<PaginationDto>(ds.Tables[1]);

            return Json(new { data, totalCount }, new System.Text.Json.JsonSerializerOptions());
        }
        public JsonResult GetById(int Id)
        {
            return Json(_receipttRepo.GetByID(Id));
        }

        public async Task<JsonResult> CreateOrUpdate([FromBody] Unit data)
        {
            //Account acc = _accRepo.GetByID(HttpContext.Session.GetInt32("AccountId") ?? 0);
            //if (acc == null)
            //{
            //    return Json(new { status = 0, statusText = "Bạn đã hết phiên đăng nhập! Vui lòng đăng nhập lại!" });
            //}


            //if (string.IsNullOrWhiteSpace(data.UnitCode))
            //{
            //    return Json(new { status = 0, statusText = "Vui lòng nhập Mã topping!" });
            //}
            //else if (string.IsNullOrWhiteSpace(data.UnitName))
            //{
            //    return Json(new { status = 0, statusText = "Vui lòng nhập Tên topping!" });
            //}


            //bool isCheck = _unitRepo.GetAll().Any(x => x.Id != data.Id && x.UnitCode == data.UnitCode && x.IsDelete != true);
            //if (isCheck) return Json(new { status = 0, statusText = "Mã đơn vị đã được sử dụng! Hãy kiểm tra lại!", result = 0 });

            //Unit model = _unitRepo.GetByID(data.Id) ?? new Unit();

            //model.UnitCode = TextUtils.ToString(data.UnitCode);
            //model.UnitName = TextUtils.ToString(data.UnitName);
            //model.Note = TextUtils.ToString(data.Note);
            //model.IsDelete = false;


            //if (model.Id > 0)
            //{
            //    model.UpdatedDate = DateTime.Now;
            //    model.UpdatedBy = acc.FullName;
            //    _unitRepo.Update(model);
            //}
            //else
            //{
            //    model.CreatedDate = DateTime.Now;
            //    model.CreatedBy = acc.FullName;
            //    await _unitRepo.CreateAsync(model);
            //}
            return Json(new { status = 1, statusText = "", /*result = model */});
        }

        public async Task<JsonResult> Delete(int Id)
        {
            GoodsReceipt model = _receipttRepo.GetByID(Id) ?? new GoodsReceipt();
            if (model.Id <= 0) return Json(new { status = 0, message = "Không tìm thấy Đơn vị!" });

            model.IsDelete = true;
            _receipttRepo.Update(model);
            return Json(new { status = 1, message = "" });
        }
    }
}
