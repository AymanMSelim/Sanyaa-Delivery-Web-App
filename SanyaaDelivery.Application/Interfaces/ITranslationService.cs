using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITranslationService
    {
        string Translate(string key);

        List<TranslatorT> GetList();
    }
}
