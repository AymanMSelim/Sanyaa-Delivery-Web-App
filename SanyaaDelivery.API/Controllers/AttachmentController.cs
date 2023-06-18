using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.API.Dto;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
   
    public class AttachmentController : APIBaseAuthorizeController
    {
        private readonly IHostingEnvironment hostEnvironment;
        private readonly CommonService commonService;
        private readonly IAttachmentService attachmentService;

        public AttachmentController(IHostingEnvironment hostEnvironment, CommonService commonService, IAttachmentService attachmentService) : base(commonService)
        {
            this.hostEnvironment = hostEnvironment;
            this.commonService = commonService;
            this.attachmentService = attachmentService;
        }
		[HttpPost("AttachImageToCart")]
		public async Task<ActionResult<Result<AttachmentT>>> AttachImageToCart(int? clientId = null)
		{
            try
            {
                List<IFormFile> fileList = Request.Form.Files.ToList();
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return Ok(ResultFactory<AttachmentT>.ReturnClientError());
                }
                if (fileList.IsEmpty() || !commonService.IsFileValid(fileList[0]))
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessageFD("File not valid"));
                }
                var cartId = await commonService.GetCurrentClientCartIdAsync(clientId);
                if (cartId.IsNull())
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessageFD("Cart not found"));
                }
                var fileExtention = System.IO.Path.GetExtension(fileList[0].FileName);
                fileExtention = fileExtention.Replace(".", "");
                var attachment = await attachmentService.SaveFileAsync(commonService.ConvertFileToByteArray(fileList[0]),
                    (int)Domain.Enum.AttachmentType.CartImage, cartId.ToString(), fileExtention);
                attachment.FilePath = $"{hostEnvironment.WebRootPath}{attachment.FilePath}";
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
           
		}

        [HttpPost("Delete")]
        public async Task<ActionResult<Result<object>>> Delete(IntIdDto model)
        {
            try
            {
                var affectedRows =  await attachmentService.DeleteAsync(model.Id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCartAttachmentList")]
        public async Task<ActionResult<Result<List<AttachmentT>>>> GetCartAttachmentList(int? clientId)
        {
            try
            {
                var attachmentList = new List<AttachmentT>();
                var cartId = await commonService.GetCurrentClientCartIdAsync(clientId);
                if (cartId.IsNotNull())
                {
                    attachmentList = await attachmentService.GetListAsync(((int)Domain.Enum.AttachmentType.CartImage), cartId.ToString());
                }
                foreach (var item in attachmentList)
                {
                    item.FilePath = $"{hostEnvironment.WebRootPath}{item.FilePath}";
                }
                return Ok(ResultFactory<List<AttachmentT>>.CreateSuccessResponse(attachmentList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AttachmentT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddLandingScreenImage")]
        public async Task<ActionResult<Result<AttachmentT>>> AddLandingScreenImage()
        {
            try
            {
                if (Request.Form.Files== null || Request.Form.Files.Count == 0)
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessage("No file found"));
                }
                var file = Request.Form.Files[0];
                if (commonService.IsFileValid(file) == false)
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessage("File not valid"));
                }
                var fileExtention = System.IO.Path.GetExtension(file.FileName);
                fileExtention = fileExtention.Replace(".", "");
                var byteArray = commonService.ConvertFileToByteArray(file);
                var attachment = await attachmentService.SaveFileAsync(byteArray, (int)Domain.Enum.AttachmentType.AppLandingItem,
                    "LandingScreen", fileExtention, "Public");
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachment, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }


        [HttpPost("Add")]
        public async Task<ActionResult<Result<AttachmentT>>> Add(AddAttachmentDto model)
        {
            try
            {
                var fileExtention = System.IO.Path.GetExtension(model.FileName);
                fileExtention = fileExtention.Replace(".", "");
                var attachment = await attachmentService.SaveFileAsync(model.File, model.ReferenceType,
                    model.ReferenceId, fileExtention);
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachment, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddForm")]
        public async Task<ActionResult<Result<AttachmentT>>> AddForm([FromForm]AddFormAttachmentDto model)
        {
            try
            {
                if (commonService.IsFileValid(model.File) == false)
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessage("File not valid"));
                }
                var fileExtention = System.IO.Path.GetExtension(model.FileName);
                fileExtention = fileExtention.Replace(".", "");
                var bytes = commonService.ConvertFileToByteArray(model.File);
                var attachment = await attachmentService.SaveFileAsync(bytes, model.ReferenceType,
                    model.ReferenceId, fileExtention);
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachment, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddEmployeeAttachment")]
        public async Task<ActionResult<Result<AttachmentT>>> AddEmployeeAttachment([FromForm] AddEmployeeAttachmentDto model)
        {
            try
            {
                var employeeId = commonService.GetEmployeeId(model.EmployeeId);
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<EmpOptionalAttachmentIndexDto>.ReturnEmployeeError());
                }
                if (commonService.IsFileValid(model.File) == false)
                {
                    return Ok(ResultFactory<AttachmentT>.CreateErrorResponseMessage("File not valid"));
                }
                model.EmployeeId = employeeId;
                var fileExtention = System.IO.Path.GetExtension(model.File.FileName);
                fileExtention = fileExtention.Replace(".", "");
                var byteArray = commonService.ConvertFileToByteArray(model.File);
                var attachment = await attachmentService.SaveFileAsync(byteArray, model.Type,
                    model.EmployeeId, fileExtention);
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachment, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<AttachmentT>>>> GetList(int? referenceType = null, string referenceId = null)
        {
            try
            {
                var attachmentList = await attachmentService.GetListAsync(referenceType, referenceId);
                return Ok(ResultFactory<List<AttachmentT>>.CreateSuccessResponse(attachmentList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AttachmentT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<Result<AttachmentT>>> Get(int id)
        {
            try
            {
                var attachmentList = await attachmentService.GetAsync(id);
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(attachmentList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetBytes/{id}")]
        public async Task<ActionResult<Result<AttachmentBytesDto>>> GetBytes(int id)
        {
            try
            {
                var attachment = await attachmentService.GetBytesAsync(id);
                return Ok(ResultFactory<AttachmentBytesDto>.CreateSuccessResponse(attachment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentBytesDto>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetStream/{id}")]
        public async Task<IActionResult> GetStream(int id)
        {
            try
            {
                var stream = await attachmentService.GetStreamAsync(id);
                // Set the content type and length headers
                Response.Headers.Add("Content-Type", "application/octet-stream");
                Response.ContentLength = stream.Length;
                var header = new MediaTypeHeaderValue("application/octet-stream");
                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("GetEmpOptionalAttachmentIndex/{employeeId?}")]
        public async Task<ActionResult<Result<EmpOptionalAttachmentIndexDto>>> GetEmpOptionalAttachmentIndex(string employeeId = null)
        {
            try
            {
                employeeId = commonService.GetEmployeeId(employeeId);
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<EmpOptionalAttachmentIndexDto>.ReturnEmployeeError());
                }
                var index = await attachmentService.GetEmpOptionalAttachmentIndexAsync(employeeId);
                foreach (var item in index.ItemList)
                {
                    if (string.IsNullOrEmpty(item.FilePath)) { continue; }
                    item.FileUrl = $"{commonService.GetHost()}{item.FilePath}";
                }
                return Ok(ResultFactory<EmpOptionalAttachmentIndexDto>.CreateSuccessResponse(index));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmpOptionalAttachmentIndexDto>.CreateExceptionResponse(ex));
            }
        }




    }
}
