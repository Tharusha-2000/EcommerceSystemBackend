using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    public interface IOrderProductSer
    {
        Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts();
        Task<ActionResult<OrderProduct>> GetOrderProductById(int orderProductId);
        Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProductsByOrderId(int orderId);
        Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct);
        Task<ActionResult> PutOrderProduct(int orderProductId, OrderProduct orderProduct);
        Task<ActionResult> DeleteOrderProduct(int orderProductId);
    }
}
