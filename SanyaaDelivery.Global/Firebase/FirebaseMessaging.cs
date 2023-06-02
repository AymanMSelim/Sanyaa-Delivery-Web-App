using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Firebase
{
    public class FirebaseMessaging
    {
        private static FirebaseApp clientApp;
        private static FirebaseApp empApp;

        public static void Initalize(string clientAppJsonPath, string employeeAppJsonPath)
        {
            clientApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(clientAppJsonPath)
            }, "ClientApp");

            empApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(employeeAppJsonPath)
            }, "EmployeeApp");
        }

        public static async Task SendToClientAsync(string token, string title, string body, string imageUrl = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            var msg = new FirebaseAdmin.Messaging.Message
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body,
                }
            };
            if (!string.IsNullOrEmpty(imageUrl))
            {
                msg.Notification.ImageUrl = $"https://api.sane3ydelivery.com{imageUrl}";
            }
            await FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(clientApp).SendAsync(msg);
        }

        public static async Task SendToEmpAsync(string token, string title, string body, string imageUrl = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            var msg = new FirebaseAdmin.Messaging.Message
            {
                Token = token,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body,
                }
            };
            if (!string.IsNullOrEmpty(imageUrl))
            {
                msg.Notification.ImageUrl = $"https://api.sane3ydelivery.com{imageUrl}";
            }
            await FirebaseAdmin.Messaging.FirebaseMessaging.GetMessaging(empApp).SendAsync(msg);
        }

    }
}
