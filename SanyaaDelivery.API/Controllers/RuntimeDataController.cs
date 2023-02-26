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
        [HttpGet("Get")]
        public async Task<ActionResult<Result<RuntimeData>>> Get()
        {
            try
            {
                var data = await runtimeDataService.Get();
                if (data != null)
                {
                    return Ok(ResultFactory<RuntimeData>.CreateSuccessResponse(data));
                }
                else
                {
                    return Ok(ResultFactory<RuntimeData>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ResultFactory<RuntimeData>.CreateExceptionResponse(ex));
            }
          
        }
    }
}
