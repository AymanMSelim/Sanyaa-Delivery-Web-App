using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using App.Global.DateTimeHelper;
using System.Threading.Tasks;
using SanyaaDelivery.Infra.Data;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeAppAccountService : IEmployeeAppAccountService
    {
        private readonly IRepository<LoginT> loginRepository;
        private readonly IRepository<MessagesT> messageRepository;
        private readonly IUnitOfWork unitOfWork;

        public EmployeeAppAccountService(IRepository<LoginT> loginRepository, IRepository<MessagesT> messageRepository, IUnitOfWork unitOfWork)
        {
            this.loginRepository = loginRepository;
            this.messageRepository = messageRepository;
            this.unitOfWork = unitOfWork;
        }
        
        public Task<LoginT> Get(string id)
        {
            return loginRepository.GetAsync(id);
        }

        public bool IsActive(string id)
        {
            var empLogin = Get(id).Result;
            return empLogin.LoginAccountState;
        }

        public bool IsOnline(string id)
        {
            return loginRepository.GetAsync(id).Result.LastActiveTimestamp > DateTime.Now.AddMinutes(-3);
        }

        public DateTime LastSeenTime(string id)
        {
            return loginRepository.GetAsync(id).Result.LastActiveTimestamp.Value;
        }

        public Task<int> Update(LoginT login)
        {
            loginRepository.Update(login.EmployeeId, login);
            return loginRepository.SaveAsync();
        }
        public async Task<int> ActiveAccountAsync(string id)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var login = await loginRepository.GetAsync(id);
                if (login.LoginAccountState)
                {
                    return (int)App.Global.Enums.ResultStatusCode.Success;
                }
                login.LoginAccountState = true;
                loginRepository.Update(login.EmployeeId, login);
                await messageRepository.AddAsync(new MessagesT
                {
                    EmployeeId = id,
                    Body = "تم تفعيل الحساب الخاص بكم",
                    IsRead = 0,
                    Title = "تفعيل الحساب",
                    MessageTimestamp = DateTime.Now.EgyptTimeNow(),
                });
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
                unitOfWork.RollBack();
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
            
        }

    }
}
