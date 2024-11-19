using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    public interface IOrderSer
    {
        Task<ActionResult<IEnumerable<Order>>> GetOrders();
        Task<ActionResult<Order>> GetOrderById(int orderId);
        Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId);
        Task<ActionResult<Order>> PostOrder(Order order);
        Task<ActionResult> PutOrder(int orderId, Order order);
        Task<ActionResult> DeleteOrder(int orderId);
    }
}
