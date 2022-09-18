using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
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

        public Task<List<AttachmentT>> GetListAsync(int type, string referenceId)
        {
            return repo.Where(d => d.AttachmentType == type && d.ReferenceId == referenceId).ToListAsync();
        }

        public Task<int> SaveFileAsync(Stream stream, int type, string referenceId)
        {
            return null;
        }
    }
}
