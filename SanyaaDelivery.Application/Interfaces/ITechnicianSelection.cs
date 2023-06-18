using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ITechnicianSelection
    {
        Task<Result<T>> SelectAsync<T>(RequestT request);
    }
}
