using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class TranslatorController : APIBaseAuthorizeController
    {
        private readonly ITranslationService translationService;

        public TranslatorController(ITranslationService translationService, CommonService commonService) : base(commonService)
        {
            this.translationService = translationService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<TranslatorT>>> Add(TranslatorT translator)
        {
            try
            {
                var affectedRows = await translationService.AddAsync(translator);
                return Ok(ResultFactory<TranslatorT>.CreateAffectedRowsResult(affectedRows, data: translator));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TranslatorT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<TranslatorT>>> Update(TranslatorT translator)
        {
            try
            {
                var affectedRows = await translationService.UpdateAsync(translator);
                return Ok(ResultFactory<TranslatorT>.CreateAffectedRowsResult(affectedRows, data: translator));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TranslatorT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<TranslatorT>>>> GetList(string searchValue)
        {
            try
            {
                var list = await translationService.GetListAsync(searchValue);
                return Ok(ResultFactory<List<TranslatorT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<TranslatorT>>.CreateExceptionResponse(ex));
            }
        }

    }
}
