using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IRepository<CleaningSubscribersT>, SubscribeRepository>();
            services.AddScoped<IRepository<AccountT>, AccountRepository>();
            services.AddScoped<IRepository<AccountRoleT>, AccountRoleRepository>();
            services.AddScoped<IRepository<AccountTypeT>, AccountTypeRepository>();
            services.AddScoped<IRepository<RoleT>, RoleRepository>();



            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmployeeAppAccountService, EmployeeAppAccountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmpDeptService, EmpDeptService>();
            services.AddScoped<ISystemUserService, SystemUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICleaningSubscriberService, CleaningSubscriberService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IAccountRoleService, AccountRoleService>();
            services.AddScoped<IRoleService, RoleService>();

        }
    }
}
