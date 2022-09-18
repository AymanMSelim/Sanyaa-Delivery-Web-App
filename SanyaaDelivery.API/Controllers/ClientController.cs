using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class ClientController : APIBaseAuthorizeController
    {
        private readonly IClientService clientService;
        private readonly CommonService commonService;
        private readonly IClientSubscriptionService clientSubscriptionService;

        public ClientController(IClientService clientService, CommonService commonService, IClientSubscriptionService clientSubscriptionService)
        {
            this.clientService = clientService;
            this.commonService = commonService;
            this.clientSubscriptionService = clientSubscriptionService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> Add(ClientT client)
        {
            try
            {
                if (client == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Client can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.Add(client);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{clientId}")]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> Get(int clientId)
        {
            try
            {
                ClientT client = await clientService.GetAsync(clientId);
                if (client != null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(client));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> Update(ClientT client)
        {
            try
            {
                if (client == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateNotFoundResponse());
                }
                int affectedRecords = await clientService.UpdateAsync(client);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> UpdateBranch(int clientId, int brnachId)
        {
            try
            {
                var client = await clientService.GetAsync(clientId);
                if (client == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateNotFoundResponse());
                }
                client.BranchId = brnachId;
                int affectedRecords = await clientService.UpdateAsync(client);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> UpdateBranchByCity(int cityId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Client id is null"));
                }
                int affectedRecords = await clientService.UpdateBranchByCityAsync(clientId.Value, cityId);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientT>>> UpdateBranchByRegion(int regionId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Client id is null"));
                }
                int affectedRecords = await clientService.UpdateBranchByRegionAsync(clientId.Value, regionId);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<AddressT>>> AddAddress(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddAddress(address);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateSuccessResponse(address, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<AddressT>>> UpdateAddress(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.UpdateAddress(address);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateSuccessResponse(address, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }
        
        [HttpPost("DeleteAddress/{addressId}")]
        public async Task<ActionResult<OpreationResultMessage<AddressT>>> DeleteAddress(int addressId)
        {
            try
            {
                return await clientService.DeleteAddress(addressId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<AddressT>>> SetDefaultAddress(int addressId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.SetDefaultAddressAsync(addressId, clientId.Value);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("AddPhone")]
        public async Task<ActionResult<OpreationResultMessage<ClientPhonesT>>> AddPhone(ClientPhonesT phone)
        {
            try
            {
                if (phone == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateErrorResponseMessage("Phone can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddPhone(phone);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateSuccessResponse(phone, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ClientPhonesT>>> Updatehone(ClientPhonesT phone)
        {
            try
            {
                if (phone == null)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateErrorResponseMessage("Phone can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.UpdatePhone(phone);
                if (affectedRecords > 0)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateSuccessResponse(phone, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAddress/{addressId}")]
        public async Task<ActionResult<OpreationResultMessage<AddressT>>> GetAddress(int addressId)
        {
            try
            {
                var address = await clientService.GetAddress(addressId);
                if (address != null)
                {
                    return Ok(OpreationResultMessageFactory<AddressT>.CreateSuccessResponse(address));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetPhone/{clientPhoneId}")]
        public async Task<ActionResult<OpreationResultMessage<ClientPhonesT>>> GetPhone(int clientPhoneId)
        {
            try
            {
                var phone = await clientService.GetPhone(clientPhoneId);
                if (phone != null)
                {
                    return Ok(OpreationResultMessageFactory<ClientPhonesT>.CreateSuccessResponse(phone));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<AddressT>>>> GetAddressList(int? clientId = null, bool getDeleted = false)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var addressList = await clientService.GetAddressListAsync(clientId.Value, getDeleted);
                if (addressList != null)
                {
                    return Ok(OpreationResultMessageFactory<List<AddressT>>.CreateSuccessResponse(addressList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<AddressT>>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<ClientPhonesT>>>> GetPhoneList(int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var phoneList = await clientService.GetPhoneList(clientId.Value);
                if (phoneList != null)
                {
                    return Ok(OpreationResultMessageFactory<List<ClientPhonesT>>.CreateSuccessResponse(phoneList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ClientPhonesT>>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<List<ClientSubscriptionT>>>> Subscripe(int subscriptionId, int? clientId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.Subscripe(subscriptionId, clientId.Value);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<List<ClientSubscriptionT>>>> UnSubscripe(int subscriptionId, int? clientId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UnSubscripe(subscriptionId, clientId.Value);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ClientSubscriptionT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<List<ClientT>>>> UpdateName(string name, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UpdateNameAsync(clientId.Value, name);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<List<ClientT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<List<ClientT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ClientT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<List<ClientT>>>> UpdateEmail(string email, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UpdateEmailAsync(clientId.Value, email);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<List<ClientT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<List<ClientT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ClientT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentSubscription>>>> GetSubcriptionList(int? clientId = null, int? departmentId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId, true, true);
                var subscriptionList = clientSubscriptionList.Select(d => d.Subscription).ToList();
                List<DepartmentSubscription> departmentSubscriptionList = subscriptionList.Select(d => new DepartmentSubscription(d)).ToList();
                departmentSubscriptionList.ForEach(d => d.Department = null);
                departmentSubscriptionList.ForEach(d => d.ClientSubscriptionT = null);
                return Ok(OpreationResultMessageFactory<List<DepartmentSubscription>>.CreateSuccessResponse(departmentSubscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSubscription>>(ex));
            }
        }
    }
}
