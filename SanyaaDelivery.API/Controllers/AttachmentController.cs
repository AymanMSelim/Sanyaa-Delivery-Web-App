using App.Global.DTOs;
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
		[HttpPost]
		public async Task<ActionResult<OpreationResultMessage<AttachmentT>>> AttachImageToCart(int? clientId = null)
		{
            try
            {
                List<IFormFile> fileList = Request.Form.Files.ToList();
                if (fileList == null || fileList.Count == 0)
                {
                    return Ok(OpreationResultMessageFactory<AttachmentT>.CreateNotFoundResponse("No files found"));
                }
                clientId = commonService.GetClientId(clientId);
                var cart = await commonService.GetClientCartAsync(clientId);
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
                return Ok(OpreationResultMessageFactory<AttachmentT>.CreateSuccessResponse(null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
           
		}

        [HttpPost("Delete/{attachmentId}")]
        public async Task<ActionResult<OpreationResultMessage<AttachmentT>>> Delete(int attachmentId)
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
                return Ok(OpreationResultMessageFactory<AttachmentT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AttachmentT>.CreateExceptionResponse(ex));
            }
        }
    }
}
