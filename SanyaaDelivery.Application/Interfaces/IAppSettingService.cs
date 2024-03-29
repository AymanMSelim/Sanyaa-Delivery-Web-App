﻿using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAppSettingService
    {
        Task<AppSettingT> Get(string key);
        Task<List<AppSettingT>> GetListAsync();
    }
}
