using App.Global.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services.TechnicianSelectionServices
{
    class BroadcastFavouriteTechnicianSelectionService : ITechnicianSelection
    {
        public Task<Result<T>> SelectAsync<T>(RequestT request)
        {
            throw new NotImplementedException();
        }
    }
}
