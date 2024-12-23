﻿using Ecommerce.OrderProcessing.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    //[Authorize(Roles = "customer,admin")]
    [HttpPost("create-payment-intent")]
    public IActionResult CreatePaymentIntent([FromBody] PaymentIntentRequest request)
    {
        var paymentIntent = _paymentService.CreatePaymentIntent(request.Amount, request.Currency, request.PaymentMethodTypes);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }
}
