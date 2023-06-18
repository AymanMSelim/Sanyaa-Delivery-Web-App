using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Enum;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
namespace SanyaaDelivery.Application.Services
{
    public class ClientPointService : IClientPointService 
    { 
        private readonly IRepository<ClientPointT> repo;
        private readonly IClientService clientService;
        private readonly IUnitOfWork unitOfWork;

        public ClientPointService(IRepository<ClientPointT> repo, IClientService clientService, IUnitOfWork unitOfWork)
        {
            this.repo = repo;
            this.clientService = clientService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(ClientPointT clientPoint)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                clientPoint.CreationDate = DateTime.Now.EgyptTimeNow();
                clientPoint.PointType = ((sbyte)ClientPointType.Add); //Add
                await repo.AddAsync(clientPoint);
                await clientService.AddPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
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

        public async Task<int> WithdrawAsync(ClientPointT clientPoint)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                clientPoint.CreationDate = DateTime.Now.EgyptTimeNow();
                clientPoint.PointType = ((sbyte)ClientPointType.Withdraw); //Withdraw
                await repo.AddAsync(clientPoint);
                await clientService.WidthrawPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
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


        public async Task<int> DeletetAsync(int id)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var clientPoint = await GetAsync(id);
                await repo.DeleteAsync(id);
                if(clientPoint.PointType == (sbyte)ClientPointType.Add)
                {
                    await clientService.WidthrawPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
                }
                else
                {
                    await clientService.AddPointAsync(clientPoint.ClientId, clientPoint.Points.Value);
                }
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
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

        public async Task<int> DeletetByRequestIdAsync(int id)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var clientPoint = await repo.Where(d => d.RequestId == id).FirstOrDefaultAsync();
                if (clientPoint.IsNull()) 
                {
                    return (int)App.Global.Enums.ResultStatusCode.Success;
                }
                return await DeletetAsync(clientPoint.ClientPointId);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
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


        public Task<ClientPointT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public Task<List<ClientPointT>> GetListAsync(int clientId, ClientPointType? type = null)
        {
            var query = repo.Where(d => d.ClientId == clientId);
            if(type != null)
            {
                query = query.Where(d => d.PointType == ((sbyte)type));
            }
            query = query.OrderByDescending(d => d.ClientPointId);
            return query.ToListAsync();  
        }

        public async Task<int> UpdateAsync(ClientPointT clientPoint)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var oldPoint = await GetAsync(clientPoint.ClientPointId);
                if (clientPoint.Points > oldPoint.Points)
                {
                    await clientService.AddPointAsync(clientPoint.ClientId, clientPoint.Points.Value - oldPoint.Points.Value);
                }
                if (oldPoint.Points > clientPoint.Points)
                {
                    await clientService.WidthrawPointAsync(clientPoint.ClientId, oldPoint.Points.Value - clientPoint.Points.Value);
                }
                repo.Update(clientPoint.ClientPointId, clientPoint);
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
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

        public async Task<int> GetClientPointAsync(int clientId)
        {
            var addedPoints = await repo.DbSet.Where(d => d.ClientId == clientId && d.PointType == ((sbyte)ClientPointType.Add))
                .SumAsync(d => d.Points.Value);
            var withdrawPoints = await repo.DbSet.Where(d => d.ClientId == clientId && d.PointType == ((sbyte)ClientPointType.Withdraw))
                .SumAsync(d => d.Points.Value);
            return addedPoints - withdrawPoints;
        }
    }
}
