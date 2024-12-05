using Ecommerce.ReviewAndRating.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Ecommerce.ReviewAndRating.Application.Services
{
    public class InterServiceCommunication : IInterServiceCommunication
    {
        private readonly HttpClient _httpClient;
       
        public InterServiceCommunication(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            
        }

        public async Task<List<OrderDto>> GetOrdersByIdsAsync(List<int> orderIds)
        {
            var apiUrl = "https://localhost:7242/api/GetOrdersById/batch"; 

            var response = await _httpClient.PostAsJsonAsync(apiUrl, orderIds);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<OrderDto>>();
            }

            throw new ApplicationException($"Failed to fetch orders. Status code: {response.StatusCode}");
        }

        public async Task<List<UserDto>> GetUsersByIdsAsync(List<int> userIds)
        {
            var apiUrl = "http://localhost:8080/api/GetUsersByIds/batch";

            var response = await _httpClient.PostAsJsonAsync(apiUrl, userIds);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<UserDto>>();
            }

            throw new ApplicationException($"Failed to fetch users. Status code: {response.StatusCode}");
        }


         public async Task<List<int>> GetProductIdFromOrderServicesAsync(int orderId)
         {

             // base URL of the OrderProduct service
             var orderServiceBaseUrl = "https://localhost:7242/api/OrderProduct";
             var url = $"{orderServiceBaseUrl}/byOrder/{orderId}";

             var response = await _httpClient.GetAsync(url);

             if (!response.IsSuccessStatusCode)
                 throw new HttpRequestException($"Failed to fetch product IDs for order ID {orderId}. Status code: {response.StatusCode}");

             var orderProducts = await response.Content.ReadFromJsonAsync<List<OrderProductDto>>();

             if (orderProducts == null || !orderProducts.Any())
                 throw new InvalidOperationException($"No products found for order ID {orderId}.");

             // Extract and return the product IDs
             return orderProducts.Select(op => op.ProductId).ToList();
         }

    }
}
