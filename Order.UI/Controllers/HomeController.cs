using Microsoft.AspNetCore.Mvc;
using Order.Repository.Interface;
using Order.UI.Models;
using Order.ViewModel;
using System.Diagnostics;
using System.Text.Json.Nodes;

namespace Order.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderRepository _repo;

        public HomeController(ILogger<HomeController> logger, IOrderRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FetchOrders()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var model = await _repo.FetchMaster();

            return Json(new
            {
                draw = draw,
                recordsFiltered = model.Count,
                recordsTotal = model.Count,
                data = model
            });
        }

        public async Task<IActionResult> getSingleOrderDetail(int id)
        {
            var OrderDetail = await _repo.GetSingleOrderDetail(id);

            return Json(new {error = false, result = OrderDetail});
        }

        public async Task<IActionResult> getSingleOrder(int orderId)
        {
            var OrderDetail = await _repo.getSingleOrder(orderId);

            return Json(new { error = false, result = OrderDetail });
        }

        [HttpPost("add-order")]
        public async Task<IActionResult> AddAndUpdateOrder(OrderViewModel order)
        {
            try
            {
                if(order.phoneNumber == null)
                {
                    return Json(new { error = true, message = "Phone number is required" });
                }

                if (order.customerName == null)
                {
                    return Json(new { error = true, message = "Customer name is required" });
                }

                if (order.address == null)
                {
                    return Json(new { error = true, message = "Address is required" });
                }

                if(order.orderMasterId <=0)
                {
                    await _repo.AddMasterDetail(order);
                }

                if (order.orderMasterId > 0)
                {
                    await _repo.UpdateMasterDetail(order);
                }


                return Json(new { error = false, message = "" });
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.Message });
            }
        }

        public async Task<IActionResult> Update(OrderViewModel order)
        {
            try
            {
                await _repo.UpdateMasterDetail(order);

                return Json(new { error = false, message = "" });
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.Message });
            }
        }

        public async Task<IActionResult> deleteOrderItem(int id)
        {
            try
            {
                await _repo.DeleteMasterDetail(id);

                return Json(new { error = false, message = "" });
            }
            catch (Exception e)
            {
                return Json(new { error = true, message = e.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
