using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
