using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Services
{
    public class NotificatonService : INotificatonService
    {
        public async void Send(string token, string title, string body)
        {

            var res = await FirebaseAdmin.Messaging.FirebaseMessaging.DefaultInstance.SendAsync(new FirebaseAdmin.Messaging.Message
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                }
            });
        }
    }
}
