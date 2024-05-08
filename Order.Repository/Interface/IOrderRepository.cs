using Order.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> SaveAll();

        Task<bool> AddMasterDetail(OrderViewModel order);

        Task<bool> UpdateMasterDetail(OrderViewModel order);

        Task<bool> DeleteMasterDetail(int masterId);

        Task<IReadOnlyList<OrderMasterDTO>> FetchMaster();

        Task<OrderDetailDTO> GetSingleOrderDetail(int detailId);

        Task<OrderViewModel> getSingleOrder(int orderId);
    }
}
