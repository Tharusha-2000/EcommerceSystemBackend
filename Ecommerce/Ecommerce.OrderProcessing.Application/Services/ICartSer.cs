﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    public interface ICartSer
    {
        Task<ActionResult<IEnumerable<Cart>>> GetCarts();
        Task<ActionResult<Cart>> GetCartById(int cartId);
        Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId);
        Task<ActionResult<Cart>> PostCart(Cart cart);
        Task<ActionResult> PutCart(int cartId, Cart cart);
        Task<ActionResult> DeleteCart(int cartId);
    }
}