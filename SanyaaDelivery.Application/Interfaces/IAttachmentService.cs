using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAttachmentService
    {
        Task<AttachmentT> GetAsync(int id);
        Task<int> AddAsync(AttachmentT attachment);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteByReferenceAsync(int type, string referenceId);
        Task<List<AttachmentT>> GetListAsync(int type, string referenceId);
        Task<int> SaveFileAsync(Stream stream, int type, string referenceId);
        Task<int> DeleteFileAsync(AttachmentT attachment);


    }
}
