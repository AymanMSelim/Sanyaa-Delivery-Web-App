﻿using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class TransactionController : APIBaseAuthorizeController
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService, CommonService commonService) : base(commonService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet("Get/{transactionId}")]
        public async Task<ActionResult<Result<TransactionT>>> Get(int transactionId)
        {
            try
            {
                var transaction = await transactionService.GetAsync(transactionId);
                return base.Ok(ResultFactory<TransactionT>.CreateSuccessResponse(transaction, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TransactionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<TransactionT>>> Add(TransactionT transaction)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                transaction.SystemUserId = userId.HasValue ? userId.Value : 1;
                var affectedRows = await transactionService.AddAsync(transaction);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<TransactionT>.CreateSuccessResponse(transaction, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<TransactionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TransactionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<TransactionT>>> Update(TransactionT transaction)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int? userId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                transaction.SystemUserId = userId.HasValue ? userId.Value : 1;
                var affectedRows = await transactionService.UpdateAsync(transaction);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<TransactionT>.CreateSuccessResponse(transaction, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<TransactionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TransactionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Delete/{transactionId}")]
        public async Task<ActionResult<Result<TransactionT>>> Delete(int transactionId)
        {
            try
            {
                var affectedRows = await transactionService.DeletetAsync(transactionId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<TransactionT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<TransactionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TransactionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<TransactionT>>>> GetList(sbyte? referenceType = null, string referenceId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isCanceled = null)
        {
            try
            {
                var list = await transactionService.GetListAsync(referenceType, referenceId, startDate, endDate, isCanceled);
                return Ok(ResultFactory<List<TransactionT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<TransactionT>.CreateExceptionResponse(ex));
            }
        }
    }
}
