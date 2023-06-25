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
        private readonly ITranslationService translationService;
        private readonly CommonService commonService;
        private readonly IGeneralSetting generalSetting;
        private readonly IDayWorkingTimeService dayWorkingTimeService;
        private readonly IClientSubscriptionService clientSubscriptionService;

        public CartController(ICartService cartService, IServiceService serviceService, ITranslationService translationService,
            CommonService commonService, IGeneralSetting generalSetting, IDayWorkingTimeService dayWorkingTimeService,
            IClientSubscriptionService clientSubscriptionService) : base(commonService)
        {
            this.cartService = cartService;
            this.serviceService = serviceService;
            this.translationService = translationService;
            this.commonService = commonService;
            this.generalSetting = generalSetting;
            this.dayWorkingTimeService = dayWorkingTimeService;
            this.clientSubscriptionService = clientSubscriptionService;
        }

        [HttpPost("AddService")]
        public async Task<ActionResult<Result<CartDetailsT>>> AddService(int serviceId, int? clientId = null, int serviceQuantity = 1)
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
                    return Ok(ResultFactory<CartDetailsT>.CreateErrorResponseMessage("Client id not is null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var service = await serviceService.GetAsync(serviceId, true);
                if (service.IsNull())
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateNotFoundResponse("Service not found"));
                }
                var cart = await cartService.AddOrReturnExistAsync(clientId.Value, service.Department.DepartmentSub0Navigation.Department.DepartmentId, isViaApp);
                if(service.Department.DepartmentSub0Navigation.Department.DepartmentId != cart.DepartmentId)
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateErrorResponseMessage("Department mismatched", App.Global.Enums.ResultStatusCode.Mismatched));
                }
                var cartDetails = new CartDetailsT { ServiceId = serviceId, ServiceQuantity = serviceQuantity, CreationTime = DateTime.Now, CartId = cart.CartId };
                var affectedRows = await cartService.AddDetailsAsync(cartDetails);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateSuccessResponse(cartDetails, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{cartId}")]
        public async Task<ActionResult<Result<CartT>>> Get(int cartId)
        {
            try
            {
                var cart = await cartService.GetAsync(cartId);
                return base.Ok(ResultFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCartByClientId")]
        public async Task<ActionResult<Result<CartT>>> GetCartByClientId(int? clientId = null, bool getCartDetails = false)
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
                    return Ok(ResultFactory<CartDetailsT>.CreateNotFoundResponse("Client not found"));
                }
                var cart = await cartService.GetCurrentByClientIdAsync(clientId.Value, isViaApp, getCartDetails);
                return base.Ok(ResultFactory<CartT>.CreateSuccessResponse(cart));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCustomAppCart")]
        public async Task<ActionResult<Result<CartDto>>> GetCustomAppCart(int? clientId = null, int? cityId = null)
        {
            try
            {
                var client = await commonService.GetClient(clientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                var cart  = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    clientId = commonService.GetClientId(clientId);
                    cart = new CartT
                    {
                        ClientId = clientId.Value,
                        CreationTime = DateTime.Now,
                        IsViaApp = commonService.IsViaApp(),
                        DepartmentId = 10,
                    };
                    await cartService.AddAsync(cart);
                }
                var cartDto = await cartService.GetCartForAppAsync(cart.CartId, cityId);
                return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<CartT>>> Add(CartT cart)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var affectedRows = await cartService.AddAsync(cart);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<CartT>>> Update(CartT cart)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var affectedRows = await cartService.UpdateAsync(cart);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CartT>.CreateSuccessResponse(cart, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateQuantity")]
        public async Task<ActionResult<Result<CartDto>>> UpdateQuantity(int detailsId, int newQuantity)
        {
            try
            {
                var cart = await commonService.GetCurrentClientCartAsync(null, true);
                if (cart.DepartmentId == GeneralSetting.CleaningDepartmentId)
                {
                    if (cart.CartDetailsT.HasItem())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponseMessage("Can't add more than cleaning service to the cart", App.Global.Enums.ResultStatusCode.Failed, App.Global.Enums.ResultAleartType.FailedDialog));
                    }
                }
                var cartDetails = await cartService.GetDetailsAsync(detailsId);
                cartDetails.ServiceQuantity = newQuantity;
                var affectedRows = await cartService.UpdateDetailsAsync(cartDetails);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cartDetails.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddUpdateService")]
        public async Task<ActionResult<Result<CartDto>>> AddUpdateService(int serviceId, int newQuantity, int? clientId = null)
        {
            try
            {
                bool isViaApp = commonService.IsViaApp();
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<CartDto>.ReturnClientError());
                }
                var client = await commonService.GetClient(clientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                var result = await cartService.AddUpdateServiceAsync(clientId.Value, isViaApp, serviceId, newQuantity);
                if (result.IsSuccess)
                {
                    var cart = await commonService.GetCurrentClientCartAsync(clientId);
                    var customCart = await cartService.GetCartForAppAsync(cart.CartId);
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(customCart, App.Global.Enums.ResultStatusCode.Success, result.Message, App.Global.Enums.ResultAleartType.SuccessToast));
                }
                else
                {
                    return Ok(ResultFactory<CartDto>.CreateErrorResponseMessage(result.Message, resultAleartType: App.Global.Enums.ResultAleartType.FailedToast));
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteService")]
        public async Task<ActionResult<Result<CartDto>>> DeleteService(int serviceId, int? clientId = null)
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
                    return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                }
                var cart = await cartService.GetCurrentByClientIdAsync(clientId.Value, isViaApp);
                if (cart.IsNull())
                {
                    return Ok(ResultFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.DeletetByServiceIdAsync(cart.CartId, serviceId);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateNote")]
        public async Task<ActionResult<Result<CartT>>> UpdateNote(string? note = null, int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.UpdateNoteAsync(cart.CartId, note);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CartDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("CancelPromocode/{clientId?}")]
        public async Task<ActionResult<Result<CartDto>>> CancelPromocode(int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(ResultFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var affectedRows = await cartService.CancelPromocodeAsync(cart.CartId);
                if (affectedRows > 0)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ChangeUsePointStatus/{clientId?}")]
        public async Task<ActionResult<Result<CartDto>>> ChangeUsePointStatus(int? clientId = null)
        {
            try
            {
                CartT cart = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(ResultFactory<CartDto>.CreateNotFoundResponse("No cart fount"));
                }
                var result = await cartService.ChangeUsePointStatusAsync(cart.CartId);
                if (result.IsSuccess)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cart.CartId);
                    if (cartDto.IsNull())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
                }
                else
                {
                    return Ok(result.Convert(new CartDto()));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ApplyPromocode")]
        public async Task<ActionResult<Result<CartDto>>> ApplyPromocode(string promocode, int? clientId = null)
        {
            try
            {
                var cartId = await commonService.GetCurrentClientCartIdAsync(clientId);
                if (cartId.IsNull())
                {
                    return Ok(ResultFactory<CartDto>.CreateNotFoundResponse("Cart not found"));
                }
                var result = await cartService.ApplyPromocodeAsync(cartId.Value, promocode);
                if (result.IsSuccess)
                {
                    var cartDto = await cartService.GetCartForAppAsync(cartId.Value);
                    if (cartDto.IsNull())
                    {
                        return Ok(ResultFactory<CartDto>.CreateErrorResponse());
                    }
                    return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto, App.Global.Enums.ResultStatusCode.Success, "Promocode applied successfully", App.Global.Enums.ResultAleartType.SuccessToast));
                }
                else
                {
                    result.AleartType = ((int)App.Global.Enums.ResultAleartType.FailedDialog);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Delete/{cartId}")]
        public async Task<ActionResult<Result<CartT>>> Delete(int cartId)
        {
            try
            {
                var affectedRows = await cartService.DeleteAsync(cartId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CartT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CartT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDetails/{id}")]
        public async Task<ActionResult<Result<CartDto>>> DeleteDetails(int id)
        {
            try
            {
                var affectedRows = await cartService.DeletetDetailsAsync(id);
                if (affectedRows >= 0)
                {
                    var cartId = await commonService.GetCurrentClientCartIdAsync();
                    if (cartId.HasValue)
                    {
                        var cartDto = await cartService.GetCartForAppAsync(cartId.Value);
                        if (cartDto.IsNotNull())
                        {
                            return Ok(ResultFactory<CartDto>.CreateSuccessResponse(cartDto));
                        }
                    }
                }
                return Ok(ResultFactory<CartDto>.CreateErrorResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetDetailsListByClientId/{clientId?}")]
        public async Task<ActionResult<Result<List<CartDetailsT>>>> GetDetailsListByClientId(int? clientId = null)
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
                    return Ok(ResultFactory<CartDetailsT>.CreateNotFoundResponse("Client not found"));
                }
                var list = await cartService.GetDetailsListByClientIdAsync(clientId.Value, false);
                return Ok(ResultFactory<List<CartDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CartDetailsT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("ValidateCart")]
        public async Task<ActionResult<Result<List<string>>>> ValidateCart(int? clientId = null)
        {
            List<string> errorList = new List<string>();
            try
            {
                var client = await commonService.GetClient(clientId, true, true);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<string>>.CreateRequireRegisterResponse(new List<string> 
                    { 
                        translationService.Translate("You must register first")
                    }));
                }
                if (client.AddressT.IsEmpty())
                {
                    errorList.Add(translationService.Translate("You must add at least one address"));
                }
                else
                {
                    var defaultAddress = client.AddressT.FirstOrDefault(d => d.IsDefault);
                    if (defaultAddress.IsNull())
                    {
                        errorList.Add(translationService.Translate("You must select a default address address"));
                    }
                    else
                    {
                        if (defaultAddress.GovernorateId.IsNull() ||
                            defaultAddress.CityId.IsNull() ||
                            defaultAddress.RegionId.IsNull() ||
                            string.IsNullOrEmpty(defaultAddress.AddressStreet))
                        {
                            errorList.Add(translationService.Translate("You must complete your address"));
                        }
                    }
                }
                if (errorList.HasItem())
                {
                    return Ok(ResultFactory<List<string>>.CreateErrorResponse(errorList));
                }
                if (client.ClientPhonesT.IsEmpty())
                {
                    errorList.Add(translationService.Translate("You must add at least one mobile number"));
                }
                var cart = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNull())
                {
                    return Ok(ResultFactory<List<string>>.CreateNotFoundResponse("Cart not found"));
                }
                return Ok(ResultFactory<List<string>>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<string>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetReservationAvailableTimes")]
        public async Task<ActionResult<Result<List<ReservationAvailableTimeDto>>>> GetReservationAvailableTimes(DateTime? selectedTime, int? clientId = null)
        {
            int departmentId;
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return ResultFactory<List<ReservationAvailableTimeDto>>.ReturnClientError();
                }
                var cart = await commonService.GetCurrentClientCartAsync(clientId);
                if(cart is null)
                {
                    return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateErrorResponseMessageFD("No cart found"));
                }
                departmentId = cart.DepartmentId;
                if (selectedTime.IsNull())
                {
                    selectedTime = DateTime.Now;
                }
                var list = await dayWorkingTimeService.GetReservationAvailableTimesForCart(departmentId, selectedTime.Value);
                if (list.IsEmpty())
                {
                    return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateErrorResponseMessageFD("No available times found please select another day"));
                }
                return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ReservationAvailableTimeDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetReservationAvailableTimes1")]
        public async Task<ActionResult<Result<List<ReservationAvailableTimeDto>>>> GetReservationAvailableTimes1(DateTime? selectedTime, int? clientSubscriptionId = null, int? clientId = null, bool getAll = false)
        {
            int departmentId;
            try
            {
                if (clientSubscriptionId.IsNull())
                {
                    var cart = await commonService.GetCurrentClientCartAsync(clientId);
                    if (getAll == false && cart.IsNull())
                    {
                        return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateNotFoundResponse("Cart not found"));
                    }
                    departmentId = cart.DepartmentId;
                }
                else
                {
                    var clientSubsription = await clientSubscriptionService.GetAsync(clientSubscriptionId.Value, true);
                    departmentId = clientSubsription.Subscription.DepartmentId;
                }
                if (selectedTime.IsNull())
                {
                    selectedTime = DateTime.Now;
                }
                var list = await dayWorkingTimeService.GetReservationAvailableTimesForCart(departmentId, selectedTime.Value);
                if (list.IsEmpty())
                {
                    return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateErrorResponseMessageFD("No available times found please select another day"));
                }
                return Ok(ResultFactory<List<ReservationAvailableTimeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<ReservationAvailableTimeDto>>.CreateExceptionResponse(ex));
            }
        }
    }
}
