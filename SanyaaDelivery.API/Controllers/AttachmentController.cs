using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
   
    public class AttachmentController : APIBaseAuthorizeController
    {
        private readonly IHostingEnvironment hostEnvironment;
        private readonly CommonService commonService;
        private readonly IAttachmentService attachmentService;

        public AttachmentController(IHostingEnvironment hostEnvironment, CommonService commonService, IAttachmentService attachmentService)
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
                if (fileList == null || fileList.Count == 0)
                {
                    return Ok(ResultFactory<AttachmentT>.CreateNotFoundResponse("No files found"));
                }
                clientId = commonService.GetClientId(clientId);
                var cart = await commonService.GetCurrentClientCartAsync(clientId);
                var folderPath = $@"{hostEnvironment.WebRootPath}\Attachment\{Domain.Enum.AttachmentType.CartImage.ToString()}\{cart.CartId}";
                if (Directory.Exists(folderPath) == false)
                {
                    Directory.CreateDirectory(folderPath);
                }
                foreach (var file in fileList)
                {
                    var extension = file.ContentType.Split('/')[1];
                    var uniqueFileName = Guid.NewGuid().ToString();
                    var uniqueFilePath = Path.Combine($@"{folderPath}\", $"{uniqueFileName}.{extension}");
                    using (var stream = System.IO.File.Create(uniqueFilePath))
                    {
                        await file.CopyToAsync(stream);
                        await attachmentService.AddAsync(new Domain.Models.AttachmentT
                        {
                            AttachmentType = ((int)Domain.Enum.AttachmentType.CartImage),
                            CreationTime = DateTime.Now,
                            FileName = $"{uniqueFileName}.{extension}",
                            FilePath = $@"{Request.Host.Host}/test/Attachment/{Domain.Enum.AttachmentType.CartImage}/{cart.CartId}/{uniqueFileName}.{extension}",
                            ReferenceId = cart.CartId.ToString()
                        });
                    }
                }
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
           
		}

        [HttpPost("Delete/{attachmentId}")]
        public async Task<ActionResult<Result<AttachmentT>>> Delete(int attachmentId)
        {
            try
            {
                var attachment = await attachmentService.GetAsync(attachmentId);
                var type = ((Domain.Enum.AttachmentType)attachment.AttachmentType).ToString();
                if (System.IO.File.Exists($@"{hostEnvironment.WebRootPath}\Attachment\{type}\{attachment.ReferenceId}\{attachment.FileName}"))
                {
                    System.IO.File.Delete($@"{hostEnvironment.WebRootPath}\Attachment\{type}\{attachment.ReferenceId}\{attachment.FileName}");
                }
                await attachmentService.DeleteAsync(attachmentId);
                return Ok(ResultFactory<AttachmentT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCartAttachmentList")]
        public async Task<ActionResult<Result<List<AttachmentT>>>> GetCartAttachmentList(int? clientId)
        {
            try
            {
                var attachmentList = new List<AttachmentT>();
                var cart = await commonService.GetCurrentClientCartAsync(clientId);
                if (cart.IsNotNull())
                {
                    attachmentList = await attachmentService.GetListAsync(((int)Domain.Enum.AttachmentType.CartImage), cart.CartId.ToString());
                }
                return Ok(ResultFactory<List<AttachmentT>>.CreateSuccessResponse(attachmentList, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
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

       


    }
}
