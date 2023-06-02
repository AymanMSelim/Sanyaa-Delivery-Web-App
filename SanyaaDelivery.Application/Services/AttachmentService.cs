using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IRepository<AttachmentT> repo;

        public AttachmentService(IRepository<AttachmentT> repo)
        {
            this.repo = repo;
        }
        public Task<int> AddAsync(AttachmentT attachment)
        {
            repo.AddAsync(attachment);
            return repo.SaveAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            repo.DeleteAsync(id);
            return repo.SaveAsync();
        }

        public async Task<int> DeleteByReferenceAsync(int type, string referenceId)
        {
            var list = await repo.Where(d => d.AttachmentType == type && d.ReferenceId == referenceId).ToListAsync();
            if(list == null)
            {
                return 1;
            }
            foreach (var item in list)
            {
                await repo.DeleteAsync(item.AttachmentId);
            }
            return await repo.SaveAsync();
        }

        public Task<int> DeleteFileAsync(AttachmentT attachment)
        {
            throw new NotImplementedException();
        }

        public Task<AttachmentT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public async Task<AttachmentBytesDto> GetBytesAsync(int id)
        {
            var attachment = await repo.GetAsync(id);
            var file = await System.IO.File.ReadAllBytesAsync($"wwwroot{attachment.FilePath.Replace("/", @"\")}");
            return new AttachmentBytesDto
            {
                Id = attachment.AttachmentId,
                File = file,
                FileExtention = Path.GetExtension(attachment.FileName),
                FileName = attachment.FileName
            };
        }

        public async Task<Stream> GetStreamAsync(int id)
        {
            var attachment = await repo.GetAsync(id);
            var file = await System.IO.File.ReadAllBytesAsync($"wwwroot{attachment.FilePath.Replace("/", @"\")}");
            return new MemoryStream(file);
        }

        public Task<List<AttachmentT>> GetListAsync(int type, string referenceId)
        {
            return repo.Where(d => d.AttachmentType == type && d.ReferenceId == referenceId).ToListAsync();
        }

        public async Task<AttachmentT> SaveFileAsync(byte[] data, int type, string referenceId, string extension, string folder = "Attachment", string domain = "")
        {
            var folderPath = $@"wwwroot\{folder}\{referenceId}\{type}";
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }
            var uniqueFileName = Guid.NewGuid().ToString();
            var filePath = folderPath + $@"\{uniqueFileName}.{extension}";
            await File.WriteAllBytesAsync(filePath, data);
            var attachment = new AttachmentT
            {
                AttachmentType = type,
                CreationTime = DateTime.Now,
                FileName = $"{uniqueFileName}.{extension}",
                FilePath = $@"{domain}/{folder}/{referenceId}/{type}/{uniqueFileName}.{extension}",
                ReferenceId = referenceId
            };
            await repo.AddAsync(attachment);
            await repo.SaveAsync();
            return attachment; 
        }
    }
}
