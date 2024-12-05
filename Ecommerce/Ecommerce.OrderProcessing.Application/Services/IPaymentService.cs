using Stripe;

public interface IPaymentService
{
    PaymentIntent CreatePaymentIntent(long amount, string currency, List<string> paymentMethodTypes);
}