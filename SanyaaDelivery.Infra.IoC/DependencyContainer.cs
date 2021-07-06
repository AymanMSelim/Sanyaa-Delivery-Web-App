﻿using Microsoft.Extensions.DependencyInjection;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Repositories;
using System;

namespace SanyaaDelivery.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<ClientT>, ClientRepository>();
            services.AddScoped<IRepository<RequestT>, OrderRepository>();
            services.AddScoped<IRepository<LoginT>, EmployeeApplicationAccountRepository>();
            services.AddScoped<IRepository<EmployeeT>, EmployeeRepository>();
            services.AddScoped<IRepository<DepartmentEmployeeT>, EmpDeptRepository>();
            services.AddScoped<IRepository<SystemUserT>, SystemUserRepository>();



            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmployeeAppAccountService, EmployeeAppAccountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmpDeptService, EmpDeptService>();
            services.AddScoped<ISystemUserService, SystemUserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}