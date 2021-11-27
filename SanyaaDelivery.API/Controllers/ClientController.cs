using App.Global.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    [Authorize(Roles = "CustomerApp")]
    public class ClientController : APIBaseAuthorizeController
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(ClientT client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponseMessage("Client can't be null", App.Global.Eumns.ResponseStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.Add(client);
                if (affectedRecords > 0)
                {
                    return Created($"https://{HttpContext.Request.Host}/api/client/get/{client.ClientId}",
                        HttpResponseDtoFactory<ClientT>.CreateSuccessResponse(client, App.Global.Eumns.ResponseStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
            }
        }

        [HttpGet("Get/{clientId}")]
        public async Task<ActionResult<RequestT>> Get(int clientId)
        {
            try
            {
                ClientT client = await clientService.GetById(clientId);
                if (client != null)
                {
                    return Ok(HttpResponseDtoFactory<ClientT>.CreateSuccessResponse(client));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<ClientT>.CreateErrorResponse());
            }

        }

    }
}
