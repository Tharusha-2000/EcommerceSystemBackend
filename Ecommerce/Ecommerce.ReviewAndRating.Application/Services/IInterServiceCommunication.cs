using Ecommerce.ReviewAndRating.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Application.Services
{
    public interface IInterServiceCommunication
    {
        Task<List<OrderDto>> GetOrdersByIdsAsync(List<int> orderIds);

        Task<List<UserDto>> GetUsersByIdsAsync(List<int> userIds);

        Task<List<int>> GetProductIdFromOrderServicesAsync(int orderId);
    }
}
