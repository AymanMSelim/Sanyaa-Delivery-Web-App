using App.Global.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class FawryController : APIBaseController
    {
        private readonly IFawryService fawryService;
        private readonly IConfiguration configuration;
        private readonly IOrderService orderService;

        public FawryController(IFawryService fawryService, IConfiguration configuration, IOrderService orderService)
        {
            this.fawryService = fawryService;
            this.configuration = configuration;
            this.orderService = orderService;
        }

        [HttpGet("GenerateRefNumber/{employeeId}")]
        public async Task<ActionResult<App.Global.Models.Fawry.FawryRefNumberResponse>> GenerateRefNumber(string employeeId, bool includeAllUnPaid)
        {
            List<Domain.Models.RequestT> requests;
           
            App.Global.Models.Fawry.FawryRequest fawryRequest = new App.Global.Models.Fawry.FawryRequest
            {
                Amount = 10.12f,
                ChargeItems = new List<App.Global.Models.Fawry.FawtyChargeItem>
                {
                    new App.Global.Models.Fawry.FawtyChargeItem
                    {
                        Description = "Des",
                        ItemId = "1",
                        Price = 10.12f,
                        Quantity = 1
                    }
                },
                Description = "Des",
                CustomerEmail = "ayman.mohaned5100@gmail.com",
                CustomerMobile = "01090043513",
                CustomerName = "Ayman Selim",
                CustomerProfileId = 2264,
                CurrencyCode = "EGP",
                Language = "ar-eg",
                MerchantCode = configuration["FawryMarchantCode"].ToString(),
                MerchantRefNum = 66666,
                PaymentExpiry = (long)DateTime.Now.AddDays(3).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                PaymentMethod = App.Global.Eumns.FawryPaymentMethod.PAYATFAWRY.ToString(),
            };
            fawryService.SetFawryRequest(fawryRequest, configuration["FawrySecurityCode"].ToString());
            var result = await fawryService.GetRefNumberAsync();
            return Ok(result);
        }

        //[HttpGet("GenerateRefNumberForRequest/{requestId}")]
        //public async Task<ActionResult<App.Global.Models.Fawry.FawryRefNumberResponse>> GenerateRefNumberForRequest(int requestId)
        //{

        //}


    }
}
