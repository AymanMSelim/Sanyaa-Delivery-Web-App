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

        [HttpPost("AddAddress")]
        public async Task<ActionResult> AddAddress(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest(HttpResponseDtoFactory<AddressT>.CreateErrorResponseMessage("Address can't be null", App.Global.Eumns.ResponseStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddAddress(address);
                if (affectedRecords > 0)
                {
                    return Created($"https://{HttpContext.Request.Host}/api/client/getaddress/{address.AddressId}",
                        HttpResponseDtoFactory<AddressT>.CreateSuccessResponse(address, App.Global.Eumns.ResponseStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return BadRequest(HttpResponseDtoFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<AddressT>.CreateErrorResponse());
            }
        }

        [HttpPost("AddPhone")]
        public async Task<ActionResult> AddPhone(ClientPhonesT phone)
        {
            try
            {
                if (phone == null)
                {
                    return BadRequest(HttpResponseDtoFactory<ClientPhonesT>.CreateErrorResponseMessage("Phone can't be null", App.Global.Eumns.ResponseStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddPhone(phone);
                if (affectedRecords > 0)
                {
                    return Created($"https://{HttpContext.Request.Host}/api/client/getphone/{phone.ClientPhoneId}",
                        HttpResponseDtoFactory<ClientPhonesT>.CreateSuccessResponse(phone, App.Global.Eumns.ResponseStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return BadRequest(HttpResponseDtoFactory<ClientPhonesT>.CreateErrorResponse());
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<ClientPhonesT>.CreateErrorResponse());
            }
        }

        [HttpGet("GetAddressList/{clientId}")]
        public async Task<ActionResult<HttpResponseDto<List<AddressT>>>> GetAddressList(int clientId)
        {
            try
            {
                var addressList = await clientService.GetAddressList(clientId);
                if (addressList != null)
                {
                    return Ok(HttpResponseDtoFactory<List<AddressT>>.CreateSuccessResponse(addressList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<AddressT>>.CreateErrorResponse());
            }

        }

        [HttpGet("GetPhoneList/{clientId}")]
        public async Task<ActionResult<HttpResponseDto<List<ClientPhonesT>>>> GetPhoneList(int clientId)
        {
            try
            {
                var phoneList = await clientService.GetPhoneList(clientId);
                if (phoneList != null)
                {
                    return Ok(HttpResponseDtoFactory<List<ClientPhonesT>>.CreateSuccessResponse(phoneList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<ClientPhonesT>>.CreateErrorResponse());
            }

        }

    }
}
