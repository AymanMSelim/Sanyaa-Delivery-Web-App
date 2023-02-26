using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Global.Firebase
{
    public class FirebaseMessaging
    {
        public static void Initalize(string jsonFilePath = "firebase.json")
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(jsonFilePath),
            });
        }

        public static async Task Send(string token, string title, string body)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
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
