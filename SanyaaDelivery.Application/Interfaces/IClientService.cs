﻿using SanyaaDelivery.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientService
    {
        ClientDto GetAllClients();
    }
}