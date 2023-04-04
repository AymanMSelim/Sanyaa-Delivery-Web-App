using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IRepository<TranslatorT> repo;

        public static List<TranslatorT> TranslationList { get; set; }
        public TranslationService(IRepository<TranslatorT> repo)
        {
            this.repo = repo;
            TranslationList = repo.DbSet.ToList();
            App.Global.Translation.Translator.TranslationList = TranslationList.Select(d => new App.Global.Translation.Translation
            {
                Key = d.Key,
                LangId = d.LangId,
                Value = d.Value
            }).ToList();
        }

        public string Translate(string key)
        {
            var translation = TranslationList.FirstOrDefault(d => d.Key.ToLower() == key.ToLower());
            if (translation.IsNull())
            {
                return key;
            }
            else
            {
                return translation.Value;
            }
        }

        public List<TranslatorT> GetList()
        {
            return repo.DbSet.ToList();
        }

        public Task<List<TranslatorT>> GetListAsync(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return repo.DbSet.ToListAsync();
            }
            else
            {
                return repo.Where(d => d.Key.Contains(searchValue))
                    .ToListAsync();
            }
        }


        public async Task<int> AddAsync(TranslatorT translator)
        {
            await repo.AddAsync(translator);
            var affectedRows = await repo.SaveAsync();
            TranslationList.Add(translator);
            App.Global.Translation.Translator.TranslationList.Add(new App.Global.Translation.Translation
            {
                Key = translator.Key,
                LangId = translator.LangId,
                Value = translator.Value
            });
            return affectedRows;
        }

        public Task<int> UpdateAsync(TranslatorT translator)
        {
            var trans = TranslationList.FirstOrDefault(d => d.TranslatorId == translator.TranslatorId);
            if (trans.IsNotNull())
            {
                trans.Key = translator.Key;
                trans.Value = translator.Value;
            }
            App.Global.Translation.Translator.TranslationList.RemoveAll(d => d.Key.ToLower() == translator.Key);
            App.Global.Translation.Translator.TranslationList.Add(new App.Global.Translation.Translation
            {
                Key = translator.Key,
                LangId = translator.LangId,
                Value = translator.Value
            });
            repo.Update(translator.TranslatorId, translator);
            return repo.SaveAsync();
        }
    }
}
