using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CashFreePayment
{
    public class CashFreePaymentRequest : IRequest<CashFreePaymentResponse>
    {
            public string? Order_Id { get; set; }
            public double? Order_Amount { get; set; }
            public string? Order_Currency { get; set; }
            public string? Order_Note { get; set; }
            public CustomerDetails? Customer_Details { get; set; }
    }
    
}