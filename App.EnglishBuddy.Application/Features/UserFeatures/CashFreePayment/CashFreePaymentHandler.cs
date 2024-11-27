using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using cashfreepg.Model;
using cashfreepg.Interface;
using cashfreepg.Client;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CashFreePayment;

public sealed class CashFreePaymentHandler : IRequestHandler<CashFreePaymentRequest, CashFreePaymentResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CashFreePaymentHandler> _logger;
    public CashFreePaymentHandler(
        IMapper mapper,
        ILogger<CashFreePaymentHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CashFreePaymentResponse> Handle(CashFreePaymentRequest request, CancellationToken cancellationToken)
    {
         _logger.LogDebug($"Statring method {nameof(Handle)}");
        CashFreePaymentResponse response = new CashFreePaymentResponse();
        var customerDetails = new CFCustomerDetails(
                 customerId: request?.Customer_Details?.Customer_Id,
                 customerEmail: request?.Customer_Details?.Customer_Email,
                 customerPhone: request?.Customer_Details?.Customer_Phone
             );
        var orderMeta = new CFOrderMeta(
                        notifyUrl: $"https://merchant.in/pg/process_return?cf_id={request?.Order_Id}",
                        returnUrl: "https://merchant.in/pg/process_return?cf_id={order_id}"
                    );
        var cFOrderRequest = new CFOrderRequest(
                        orderAmount: request?.Order_Amount,
                        orderCurrency: request?.Order_Currency,
                        orderNote: "Some information about the order",
                        orderMeta: orderMeta,
                        customerDetails: customerDetails
                    );
        try
        {
            CFPaymentGateway apiInstance = CFPaymentGateway.getInstance;
            CFConfig cfConfig = getConfig();
            CFHeader cfHeader = getHeader();
            CFOrderResponse result = apiInstance.orderCreate(cfConfig, cFOrderRequest, cfHeader);
            if (result != null)
            {
                response.Customer_Details = request?.Customer_Details;
                response.Order_Currency = request?.Order_Currency;
                response.Order_Amount = result?.cfOrder?.OrderAmount;
                response.Order_Status = result?.cfOrder?.OrderStatus;
                response.Payment_Session_Id = result?.cfOrder?.PaymentSessionId;
                response.Order_Id= result?.cfOrder?.OrderId;
                response.Created_At = DateTime.UtcNow;
                response.Cf_Order_Id = result?.cfOrder?.CfOrderId;
            }
             _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception e)
        {
             _logger.LogError(e.Message);
            throw e;
        }
        return response;
    }
    public CFConfig getConfig()
    {
        var cfConfig = new CFConfig(CFEnvironment.SANDBOX, "2022-09-01", "TEST39617935f0ab4d3667bfb64cf3971693", "TEST261d2a33742d8cf0b3b11d4cdf16a098f5daf363");
        return cfConfig;
    }

    public CFHeader getHeader()
    {
        var cfHeader = new CFHeader("", "");
        return cfHeader;
    }
}