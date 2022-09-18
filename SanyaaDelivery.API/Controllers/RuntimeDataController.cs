using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
   
    public class RuntimeDataController : APIBaseAuthorizeController
    {
        private readonly IRuntimeDataService runtimeDataService;

        public RuntimeDataController(IRuntimeDataService runtimeDataService)
        {
            this.runtimeDataService = runtimeDataService;
        }
        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<RuntimeData>>> Get()
        {
            try
            {
                var data = await runtimeDataService.Get();
                if (data != null)
                {
                    return Ok(OpreationResultMessageFactory<RuntimeData>.CreateSuccessResponse(data));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RuntimeData>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, OpreationResultMessageFactory<RuntimeData>.CreateExceptionResponse(ex));
            }
          
        }
    }
}
