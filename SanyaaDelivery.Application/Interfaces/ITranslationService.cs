using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITranslationService
    {
        string Translate(string key);
        Task<List<TranslatorT>> GetListAsync(string searchValue);
        List<TranslatorT> GetList();
        Task<int> AddAsync(TranslatorT translator);
        Task<int> UpdateAsync(TranslatorT translator);
    }
}
