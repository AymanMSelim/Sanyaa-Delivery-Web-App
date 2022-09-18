using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IWorkingAreaService
    {
        
        Task<WorkingAreaT> Get(int id);
        Task<List<WorkingAreaT>> GetList(string govName, string cityName, string regionName);
        Task<List<ValueWithIdDto>> GetGovList(string searchValue);
        Task<List<ValueWithIdDto>> GetCityList(string searchValue);
        Task<List<ValueWithIdDto>> GetRegionList(string searchValue);
        Task<List<ValueWithIdDto>> GetCityByGovList(string govName, string searchValue);
        Task<List<ValueWithIdDto>> GetRegionByCityList(string cityName, string searchValue);
        Task<int> Add(WorkingAreaT workingArea);
        Task<int> Update(WorkingAreaT workingArea);
    }
}
