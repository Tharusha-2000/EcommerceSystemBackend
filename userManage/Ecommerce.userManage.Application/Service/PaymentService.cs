using Stripe;

public class PaymentService : IPaymentService
{
    public PaymentService()
    {
        StripeConfiguration.ApiKey = "secret-key here";
    }

    public PaymentIntent CreatePaymentIntent(long amount, string currency, List<string> paymentMethodTypes)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = currency,
            PaymentMethodTypes = paymentMethodTypes,
        };
        var service = new PaymentIntentService();
        return service.Create(options);
    }
}