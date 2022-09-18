using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    
    public class CleaningSubscriberController : APIBaseAuthorizeController
    {
        private readonly ICleaningSubscriberService subscriberService;

        public CleaningSubscriberController(ICleaningSubscriberService subscriberService)
        {
            this.subscriberService = subscriberService;
        }

        [HttpGet("GetSubscribers")]
        public async Task<ActionResult<List<ClientT>>> GetSubscribers()
        {
            return await subscriberService.GetSubscribers();
        }

        [HttpGet("GetInfo/{clientId}")]
        public async Task<ActionResult<Cleaningsubscribers>> GetInfo(int clientId)
        {
            return await subscriberService.GetInfo(clientId);
        }

        [HttpPost("AddSubscriber")]
        public async Task<ActionResult> AddSubscribers(SubscribeDto subscribe)
        {
            var res = await subscriberService.AddSubscribe(subscribe.ClientId, subscribe.Package, subscribe.SystemUserId);
            if(res > 0)
            {
                return Ok(new { Message = "Opreation done successfully" });
            }
            else
            {
                return Ok();
            }
        }
    }
}
