using Stripe;

public class PaymentService : IPaymentService
{
    public PaymentService()
    {
        StripeConfiguration.ApiKey = "sk_test_51QLb3dCtgNr9CP7s7rsLhcGqWLcyRmerfiGwBDxLHmivYQGtsMPej5vc0i6zO8pjxHIsaH2JGaNXVAGyOrh3ceJx00uFzTeWIa";
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