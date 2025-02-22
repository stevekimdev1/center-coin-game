using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.WebRequestMethods;



namespace Program
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static async Task Main(string[] args)
        {
            string botToken = "7756670180ktvap6K0"; // 텔레그램 봇 토큰
            string chatId = "55297";//:5930"; // 메시지를 보낼 대상의 채팅 ID



            var messageText = "🐰 *새로운 게임 Welcome to BadBunny Tap2Earn!* 🥕\n\n" +
                "Hop into the paws of BadBunny, unlock treasures, and piece together forgotten tales!\n\n" +
                "👉 *Tap Into the Adventure* \n" +
                "Ready your fingers, slice those carrots, and earn your Airdrop XP!\n\n" +
                "*Shop 🛠️* – Boost your power and customize your adventure!\n" +
                "*Friends 🔪* – Recruit and earn 5,000 tokens per friend!\n" +
                "*Special Tasks 🐾* – Complete tasks to earn big rewards!\n\n" +
                "_Stay alert for Airdrop Alerts 🍄!_";

            await SendMessageAsync(botToken, chatId, messageText);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

        }

        static async Task SendMessageAsync(string botToken, string chatId, string messageText)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.telegram.org/bot{botToken}/sendMessage";

                var payload = new
                {
                    chat_id = chatId,
                    text = messageText,
                    reply_markup = new
                    {
                        inline_keyboard = new[]
                        {
                        new[]
                        {
                            new
                            {
                                text = "🚀 Start Game",
                                
                              //  web_app = new { url = "https://pryzngames.com/Games/TruthAndLiesWebGL/index.html" }
                                web_app = new { url = "https://centercoin.kr" }
                            }
                        }

                        //new[]
                        //{
                        //    new { text = "Follow us on X", url = "https://x.com/YOUR_PROFILE" },
                        //    new { text = "YouTube", url = "https://youtube.com/YOUR_CHANNEL" }
                        //}
                    }
                    }
                };

                string jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Message sent successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to send message: {response.StatusCode}");
                }
            }
        }


        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //}
    }
}