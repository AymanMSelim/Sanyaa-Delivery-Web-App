using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeAppAccountService
    {
        /// <summary>
        /// Get employee mobile application account info
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Return an object of LoginT class</returns>
        Task<LoginT> Get(string id);

        /// <summary>
        /// Get the date of last seen on mobile application
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Return the last seen time</returns>
        DateTime LastSeenTime(string id);

        /// <summary>
        /// Get account state enable or blocked
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Return true if this account is active(not blocked)</returns>
        bool IsActive(string id);

        /// <summary>
        /// Get if this employee online right now on mobile applaction or not
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Return true of online</returns>
        bool IsOnline(string id);
    }
}
