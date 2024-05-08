using Microsoft.EntityFrameworkCore;
using Order.DBContext;
using Order.Domain;
using Order.Repository.Interface;
using Order.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddMasterDetail(OrderViewModel order)
        {
            var result = false;

            try
            {

                var orderMaster = new OrderMaster()
                {
                    Address = order.address,
                    CustomerName = order.customerName,
                    OrderDate = DateTime.Now,
                    PhoneNumber = order.phoneNumber,
                };

                await _context.OrderMasters.AddAsync(orderMaster);

                await _context.SaveChangesAsync();

                var orderId = orderMaster.OrderMasterId;

                if (order.orderDetails.Count > 0)
                {
                    var details = order.orderDetails.Select(x => new OrderDetail()
                    {
                        Amount = x.amount,
                        OrderMasterId = orderId,
                        ProductName = x.productName,
                        Quantity = x.quantity,

                    });

                    await _context.OrderDetails.AddRangeAsync(details);
                }
                result = await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {

                throw;
            }

            return result;
        }

        public async Task<bool> UpdateMasterDetail(OrderViewModel order)
        {
            var result = false;
            try
            {
                var masterRecord = _context.OrderMasters.Find(order.orderMasterId);
                if (masterRecord != null)
                {
                    masterRecord.CustomerName = order.customerName;
                    masterRecord.Address = order.address;
                    masterRecord.PhoneNumber = order.phoneNumber;
                }

                var details = _context.OrderDetails.Where(x => x.OrderMasterId == order.orderMasterId).ToList();
                if (details.Any())
                {
                    _context.OrderDetails.RemoveRange(details);
                }

                if (order.orderDetails.Count > 0)
                {
                    var detail = order.orderDetails.Select(x => new OrderDetail()
                    {
                        OrderMasterId = order.orderMasterId.Value,
                        Amount = x.amount,
                        ProductName = x.productName,
                        Quantity = x.quantity,
                    });
                    await _context.OrderDetails.AddRangeAsync(detail);
                }
                result = await SaveAll();
            }
            catch (Exception e)
            {

                throw;
            }
            return result;



        }

        public async Task<bool> DeleteMasterDetail(int masterId)
        {
            var detailRecord = _context.OrderDetails.Where(x => x.OrderMasterId == masterId).ToList();
            if (detailRecord.Any())
            {
                _context.OrderDetails.RemoveRange(detailRecord);
            }
            var masterReord = _context.OrderMasters.Find(masterId);
            if (masterReord is not null)
            {
                _context.OrderMasters.Remove(masterReord);
            }
            return await SaveAll();
        }

        public async Task<IReadOnlyList<OrderMasterDTO>> FetchMaster()
        {
            return await _context.OrderMasters.Select(x => new OrderMasterDTO
            {
                address = x.Address,
                customerName = x.CustomerName,
                orderDate = x.OrderDate,
                orderMasterId = x.OrderMasterId,
                phoneNumber = x.PhoneNumber,
            }).ToListAsync();
        }

        public async Task<OrderDetailDTO> GetSingleOrderDetail(int detailId)
        {
            return await _context.OrderDetails.AsNoTracking()
                .Where(x => x.OrderDetailId == detailId)
                .Select(x => new OrderDetailDTO
                {
                    amount = x.Amount,
                    orderDetailId = x.OrderDetailId,
                    productName = x.ProductName,
                    quantity = x.Quantity,
                }).SingleOrDefaultAsync();
        }

        public async Task<OrderViewModel> getSingleOrder(int orderId)
        {
            var model = await (from ord in _context.OrderMasters
                         where ord.OrderMasterId == orderId
                         select new OrderViewModel()
                         {
                             address = ord.Address,
                             customerName = ord.CustomerName,
                             orderMasterId = ord.OrderMasterId,
                             phoneNumber = ord.PhoneNumber,
                         }).SingleOrDefaultAsync();

            if (model != null)
            {
                model.orderDetails = await (from od in _context.OrderDetails
                                      where od.OrderMasterId == model.orderMasterId
                                      select new OrderDetailDTO()
                                      {
                                          amount = od.Amount,
                                          orderDetailId = od.OrderDetailId,
                                          productName = od.ProductName,
                                          quantity = od.Quantity,
                                      }).ToListAsync();
            }

            return model;
        }
    }
}
