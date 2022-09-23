using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class CartController : APIBaseAuthorizeController
    {
        private readonly ICartService cartService;
        private readonly IServiceService serviceService;
        private readonly CommonService commonService;
        private readonly IGeneralSetting generalSetting;
        private readonly IDayWorkingTimeService dayWorkingTimeService;

        public CartController(ICartService cartService, IServiceService serviceService, 
            CommonService commonService, IGeneralSetting generalSetting, IDayWorkingTimeService dayWorkingTimeService)
        {
            this.cartService = cartService;
            this.serviceService = serviceService;
            this.commonService = commonService;
            this.generalSetting = generalSetting;
            this.dayWorkingTimeService = dayWorkingTimeService;
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartDetailsT>>> AddService(int serviceId, int? clientId = null, int? serviceQuantity = 1)
        {
            bool isViaApp = false;
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                    isViaApp = true;
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponseMessage("Client id not is null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var service = await serviceService.GetAsync(serviceId, true);
                if (service.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateNotFoundResponse("Service not found"));
                }
                var cart = await cartService.AddOrReturnExistAsync(clientId.Value, service.Department.DepartmentSub0Navigation.Department.DepartmentId, isViaApp);
                if(service.Department.DepartmentSub0Navigation.Department.DepartmentId != cart.DepartmentId)
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponseMessage("Department mismatched", App.Global.Enums.OpreationResultStatusCode.Mismatched));
                }
                var cartDetails = new CartDetailsT { ServiceId = serviceId, ServiceQuantity = serviceQuantity, CreationTime = DateTime.Now, CartId = cart.CartId };
                var affectedRows = await cartService.AddDetailsAsync(cartDetails);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateSuccessResponse(cartDetails, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{cartId}")]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> Get(int cartId)
        {
            try
            {
                var cart = await cartService.GetAsync(cartId);
                return base.Ok(OpreationResultMessageFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> GetCartByClientId(int? clientId = null, bool getCartDetails = false)
        {
            try
            {
                bool isViaApp = commonService.IsViaApp();
                if (isViaApp)
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateNotFoundResponse("Client not found"));
                }
                var cart = await cartService.GetByClientIdAsync(clientId.Value, isViaApp, getCartDetails);
                return base.Ok(OpreationResultMessageFactory<CartT>.CreateSuccessResponse(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet()]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> GetCustomAppCart(int? clientId = null, int? cityId = null)
        {
            try
            {
                var cart = await commonService.GetClientCartAsync();
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponseMessage(null, App.Global.Enums.OpreationResultStatusCode.EmptyData));
                }
                var cartDto = await cartService.GetCartForAppAsync(cart.CartId, cityId);
                return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> Add(CartT cart)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var affectedRows = await cartService.AddAsync(cart);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> Update(CartT cart)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var affectedRows = await cartService.UpdateAsync(cart);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> UpdateQuantity(int detailsId, int newQuantity)
        {
            try
            {
                var cartDetails = await cartService.GetDetailsAsync(detailsId);
                cartDetails.ServiceQuantity = newQuantity;
                var affectedRows = await cartService.UpdateDetailsAsync(cartDetails);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cartDetails.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartDetailsT>>> AddUpdateService(int serviceId, int newQuantity, int? clientId = null)
        {
            try
            {
                bool isViaApp = commonService.IsViaApp();
                if (isViaApp)
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponse());
                }
                var service = await serviceService.GetAsync(serviceId, true);
                if (service.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateNotFoundResponse("Service not found"));
                }
                var cart = await cartService.AddOrReturnExistAsync(clientId.Value, service.Department.DepartmentSub0Navigation.Department.DepartmentId, isViaApp);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponseMessage("No cart fount"));
                }
                if (service.Department.DepartmentSub0Navigation.Department.DepartmentId != cart.DepartmentId)
                {
                    if (cart.CartDetailsT.HasItem())
                    {
                        return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponseMessage("Department mismatched", App.Global.Enums.OpreationResultStatusCode.Mismatched));
                    }
                    else
                    {
                        cart.DepartmentId = service.Department.DepartmentSub0Navigation.Department.DepartmentId;
                        await cartService.UpdateAsync(cart);
                    }
                }
                var cartServiceDetails = await cartService.AddUpdateServiceAsync(cart.CartId, serviceId, newQuantity);
                if (cartServiceDetails.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateSuccessResponse(cartServiceDetails, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    cartServiceDetails.Cart = null;
                    cartServiceDetails.Service = null;
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateSuccessResponse(cartServiceDetails, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> DeleteService(int serviceId, int? clientId = null)
        {
            try
            {
                bool isViaApp = commonService.IsViaApp();
                if (isViaApp)
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
                var cart = await cartService.GetByClientIdAsync(clientId.Value, isViaApp);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.DeletetByServiceIdAsync(cart.CartId, serviceId);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> UpdateNote(string note = null, int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.UpdateNoteAsync(cart.CartId, note);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("CancelPromocode/{clientId?}")]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> CancelPromocode(int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.CancelPromocodeAsync(cart.CartId);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ChangeUsePointStatus/{clientId?}")]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> ChangeUsePointStatus(int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.ChangeUsePointStatusAsync(cart.CartId);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> ApplyPromocode(string promocode, int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateNotFoundResponse("Cart not found"));
                }
                var result = await cartService.ApplyPromocodeAsync(cart.CartId, promocode);
                if (result.IsSuccess)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Delete/{cartId}")]
        public async Task<ActionResult<OpreationResultMessage<CartT>>> Delete(int cartId)
        {
            try
            {
                var affectedRows = await cartService.DeletetAsync(cartId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDetails/{id}")]
        public async Task<ActionResult<OpreationResultMessage<CartDto>>> DeleteDetails(int id)
        {
            try
            {
                var affectedRows = await cartService.DeletetDetailsAsync(id);
                if (affectedRows >= 0)
                {
                    var cart = await commonService.GetClientCartAsync();
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetDetailsListByClientId/{clientId?}")]
        public async Task<ActionResult<OpreationResultMessage<List<CartDetailsT>>>> GetDetailsListByClientId(int? clientId = null)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? accountType = App.Global.JWT.TokenHelper.GetAccountType(identity);
                if (accountType.HasValue && GeneralSetting.CustomerAccountTypeId == accountType.Value)
                {
                    clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<CartDetailsT>.CreateNotFoundResponse("Client not found"));
                }
                var list = await cartService.GetDetailsListByClientIdAsync(clientId.Value);
                return Ok(OpreationResultMessageFactory<List<CartDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<string>>>> ValidateCart(int? clientId = null)
        {
            try
            {
                var cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<string>>.CreateNotFoundResponse("Cart not found"));
                }
                return Ok(OpreationResultMessageFactory<List<string>>.CreateSuccessResponse(new List<string> { "Note1", "Note2", "Note3"}));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<string>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<ReservationAvailableTimeDto>>>> GetReservationAvailableTimes(DateTime? selectedTime, int? clientId = null)
        {
            try
            {
                var cart = await commonService.GetClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<ReservationAvailableTimeDto>>.CreateNotFoundResponse("Cart not found"));
                }
                if (selectedTime.IsNull())
                {
                    selectedTime = DateTime.Now;
                }
                var list = await dayWorkingTimeService.GetReservationAvailableTimes(cart.DepartmentId, selectedTime.Value);
                return Ok(OpreationResultMessageFactory<List<ReservationAvailableTimeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<ReservationAvailableTimeDto>>.CreateExceptionResponse(ex));
            }
        }

    }
}
