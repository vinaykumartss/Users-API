namespace App.EnglishBuddy.Application.Features.UserFeatures.CashFreePayment;

public sealed record CashFreePaymentResponse
{
    public int? Cf_Order_Id { get; set; }
    public DateTime Created_At { get; set; }
    public CustomerDetails? Customer_Details { get; set; }
    public string? Entity { get; set; }
    public decimal? Order_Amount { get; set; }
    public string? Order_Currency { get; set; }
    public DateTime Order_Expiry_Time { get; set; }
    public string? Order_Id { get; set; }
    public OrderMeta? Order_Meta { get; set; }
    public string? Order_Note { get; set; }
    public List<object>? Order_Splits { get; set; }
    public string? Order_Status { get; set; }
    public object? Order_Tags { get; set; }
    public string? Payment_Session_Id { get; set; }
    public Payments? Payments { get; set; }
    public Refunds? Refunds { get; set; }
    public Settlements? Settlements { get; set; }
    public object? Terminal_Data { get; set; }
}

public class CustomerDetails
{
    public string? Customer_Id { get; set; }
    public string? Customer_Name { get; set; }
    public string? Customer_Email { get; set; }
    public string? Customer_Phone { get; set; }
}

public class OrderMeta
{
    public object? Return_Url { get; set; }
    public object? Notify_Url { get; set; }
    public object? Payment_Methods { get; set; }
}

public class Payments
{
    public string? url { get; set; }
}

public class Refunds
{
    public string? url { get; set; }
}
public class Settlements
{
    public string? Url { get; set; }
}

