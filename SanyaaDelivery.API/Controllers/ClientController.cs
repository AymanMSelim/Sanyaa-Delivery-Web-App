using App.Global.DTOs;
using App.Global.ExtensionMethods;
using AutoMapper;
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
        private readonly IClientPointService clientPointService;
        private readonly IMapper mapper;

        public ClientController(IClientService clientService, CommonService commonService, 
            IClientSubscriptionService clientSubscriptionService, IClientPointService clientPointService, IMapper mapper)
        {
            this.clientService = clientService;
            this.commonService = commonService;
            this.clientSubscriptionService = clientSubscriptionService;
            this.clientPointService = clientPointService;
            this.mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<ClientT>>> Add(ClientT client)
        {
            try
            {
                if (client == null)
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessage("Client can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.Add(client);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{clientId?}")]
        public async Task<ActionResult<Result<ClientT>>> Get(int? clientId, bool includePhone = false, bool includeAddress = false)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return ResultFactory<ClientT>.ReturnClientError();
                }
                ClientT client = await clientService.GetAsync(clientId.Value, includePhone, includeAddress);
                return Ok(ResultFactory<ClientT>.CreateSuccessResponse(client));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<ClientT>>>> GetList(string searchValue)
        {
            try
            {
                var clientList = await clientService.GetListAsync(searchValue);
                return Ok(ResultFactory<List<ClientT>>.CreateSuccessResponse(clientList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientT>>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<ClientT>>> Update(ClientT client)
        {
            try
            {
                if (client == null)
                {
                    return Ok(ResultFactory<ClientT>.CreateNotFoundResponse());
                }
                int affectedRecords = await clientService.UpdateAsync(client);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateBranch")]
        public async Task<ActionResult<Result<ClientT>>> UpdateBranch(int clientId, int brnachId)
        {
            try
            {
                var client = await clientService.GetAsync(clientId);
                if (client == null)
                {
                    return Ok(ResultFactory<ClientT>.CreateNotFoundResponse());
                }
                client.BranchId = brnachId;
                int affectedRecords = await clientService.UpdateAsync(client);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateSuccessResponse(client, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateBranchByCity")]
        public async Task<ActionResult<Result<ClientT>>> UpdateBranchByCity(int cityId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessage("Client id is null"));
                }
                int affectedRecords = await clientService.UpdateBranchByCityAsync(clientId.Value, cityId);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateBranchByRegion")]
        public async Task<ActionResult<Result<ClientT>>> UpdateBranchByRegion(int regionId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessage("Client id is null"));
                }
                int affectedRecords = await clientService.UpdateBranchByRegionAsync(clientId.Value, regionId);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddAddress")]
        public async Task<ActionResult<Result<AddressT>>> AddAddress(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddAddress(address);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<AddressT>.CreateSuccessResponse(address, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AddressT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddAddess")]
        public async Task<ActionResult<Result<AddressT>>> AddAddess(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddAddress(address);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<AddressT>.CreateSuccessResponse(address, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AddressT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateAddress")]
        public async Task<ActionResult<Result<AddressT>>> UpdateAddress(AddressT address)
        {
            try
            {
                if (address == null)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.UpdateAddress(address);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<AddressT>.CreateSuccessResponse(address, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }
        
        [HttpPost("DeleteAddress/{addressId}")]
        public async Task<ActionResult<Result<AddressT>>> DeleteAddress(int addressId)
        {
            try
            {
                return await clientService.DeleteAddress(addressId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("SetDefaultAddress")]
        public async Task<ActionResult<Result<AddressT>>> SetDefaultAddress(int addressId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.SetDefaultAddressAsync(addressId, clientId.Value);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<AddressT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<AddressT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AddressT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("AddPhone")]
        public async Task<ActionResult<Result<ClientPhonesT>>> AddPhone(ClientPhonesT phone)
        {
            try
            {
                if (phone == null)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateErrorResponseMessage("Phone can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.AddPhone(phone);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateSuccessResponse(phone, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdatePhone")]
        public async Task<ActionResult<Result<ClientPhonesT>>> UpdatePhone(ClientPhonesT phone)
        {
            try
            {
                if (phone == null)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateErrorResponseMessage("Phone can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await clientService.UpdatePhone(phone);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateSuccessResponse(phone, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeletePhone/{phoneId}")]
        public async Task<ActionResult<Result<ClientPhonesT>>> DeletePhone(int phoneId)
        {
            try
            {
                return await clientService.DeletePhone(phoneId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAddress/{addressId}")]
        public async Task<ActionResult<Result<AddressT>>> GetAddress(int addressId)
        {
            try
            {
                var address = await clientService.GetAddress(addressId);
                if (address != null)
                {
                    return Ok(ResultFactory<AddressT>.CreateSuccessResponse(address));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetPhone/{clientPhoneId}")]
        public async Task<ActionResult<Result<ClientPhonesT>>> GetPhone(int clientPhoneId)
        {
            try
            {
                var phone = await clientService.GetPhone(clientPhoneId);
                if (phone != null)
                {
                    return Ok(ResultFactory<ClientPhonesT>.CreateSuccessResponse(phone));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ClientPhonesT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetAddressList")]
        public async Task<ActionResult<Result<List<AddressT>>>> GetAddressList(int? clientId = null, bool? excludeDeleted = true)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var addressList = await clientService.GetAddressListAsync(clientId.Value, excludeDeleted);
                return Ok(ResultFactory<List<AddressT>>.CreateSuccessResponse(addressList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AddressT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetPhoneList")]
        public async Task<ActionResult<Result<List<ClientPhonesT>>>> GetPhoneList(int? clientId = null, bool getDeleted = false)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var phoneList = await clientService.GetPhoneListAsync(clientId.Value, getDeleted);
                return Ok(ResultFactory<List<ClientPhonesT>>.CreateSuccessResponse(phoneList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientPhonesT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Subscripe")]
        public async Task<ActionResult<Result<List<ClientSubscriptionT>>>> Subscripe(int subscriptionId, int? clientId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.Subscripe(subscriptionId, clientId.Value);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientSubscriptionT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UnSubscripe")]
        public async Task<ActionResult<Result<List<ClientSubscriptionT>>>> UnSubscripe(int subscriptionId, int? clientId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UnSubscripe(subscriptionId, clientId.Value);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientSubscriptionT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateName")]
        public async Task<ActionResult<Result<List<ClientT>>>> UpdateName(string name, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UpdateNameAsync(clientId.Value, name);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<List<ClientT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(ResultFactory<List<ClientT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateEmail")]
        public async Task<ActionResult<Result<List<ClientT>>>> UpdateEmail(string email, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var affectedRows = await clientService.UpdateEmailAsync(clientId.Value, email);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<List<ClientT>>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(ResultFactory<List<ClientT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ClientT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetSubcriptionList")]
        public async Task<ActionResult<Result<List<DepartmentSubscription>>>> GetSubcriptionList(int? clientId = null, int? departmentId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.CreateErrorResponseMessage("Client Id can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId, true, true);
                var subscriptionList = clientSubscriptionList.Select(d => d.Subscription).ToList();
                List<DepartmentSubscription> departmentSubscriptionList = subscriptionList.Select(d => new DepartmentSubscription(d)).ToList();
                departmentSubscriptionList.ForEach(d => d.Department = null);
                departmentSubscriptionList.ForEach(d => d.ClientSubscriptionT = null);
                return Ok(ResultFactory<List<DepartmentSubscription>>.CreateSuccessResponse(departmentSubscriptionList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSubscription>>(ex));
            }
        }

        [HttpGet("IsHaveMultipleAddressesOrPhones")]
        public async Task<ActionResult<Result<bool>>> IsHaveMultipleAddressesOrPhones(int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<ClientSubscriptionT>.ReturnClientError());
                }
                var addressList = await clientService.GetAddressListAsync(clientId.Value);
                var phoneList = await clientService.GetPhoneListAsync(clientId.Value);
                if ((addressList.HasItem() && addressList.Count > 1) || (phoneList.HasItem() && phoneList.Count > 1))
                {
                    return Ok(ResultFactory<bool>.CreateSuccessResponse(true, App.Global.Enums.ResultStatusCode.True));
                }
                else 
                {
                    return Ok(ResultFactory<bool>.CreateErrorResponse(false, App.Global.Enums.ResultStatusCode.False));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSubscription>>(ex));
            }
        }

        [HttpGet("GetPointList")]
        public async Task<ActionResult<Result<List<ClientPointDto>>>> GetPointList(int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<List<ClientPointT>>.ReturnClientError());
                }
                var list = await clientPointService.GetListAsync(clientId.Value);
                var mapList = mapper.Map<List<ClientPointDto>>(list);
                return ResultFactory<List<ClientPointDto>>.CreateSuccessResponse(mapList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ClientPointDto>>(ex));
            }
        }

        [HttpPost("AddPoint")]
        public async Task<ActionResult<Result<ClientPointT>>> AddPoint(ClientPointT clientPoint)
        {
            try
            {
                int affectedRows = 0;
                clientPoint.SystemUserId = commonService.GetSystemUserId();
                if (clientPoint.PointType == (int)Domain.Enum.ClientPointType.Add)
                {
                    affectedRows = await clientPointService.AddAsync(clientPoint);
                }
                else
                {
                    affectedRows = await clientPointService.WithdrawAsync(clientPoint);
                }
                return ResultFactory<ClientPointT>.CreateAffectedRowsResult(affectedRows, data: clientPoint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientPointT>(ex));
            }
        }

        [HttpPost("DeletePoint")]
        public async Task<ActionResult<Result<ClientPointT>>> DeletePoint(int clientPointId)
        {
            try
            {
                var affectedRows = await clientPointService.DeletetAsync(clientPointId);
                return ResultFactory<ClientPointT>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ClientPointT>(ex));
            }
        }

    }
}
