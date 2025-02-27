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
using System.Text.Json;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;



//"message":{ "message_id":3,"from":{ "id":5597895053,"is_bot":false,"first_name":"center coin_ official"
//,"username":"officialcentercoin","language_code":"ko"},
//"chat":{ "id":-1002295706262,"title":"bbbb","username":"dksxogus3453","type":"supergroup"},
//"date":1740282970,"text":"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"}}]}

namespace Program
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        //        {
        //"chat_id":"-1002166323850",
        //"text":"wGame\n\n id: {{strategy.order.id}} action: {{strategy.order.action}}\n\n 시간: {{timenow}} ",
        //"parse_mode":"HTML"
        //}

        //"message":{"message_id":11,"from":{"id":1087968824,"is_bot":true,"first_name":"Group",
        //"username":"GroupAnonymousBot"},
        //"sender_chat":{"id":-1002275129371,  <======================
        //"title":"RoomRoom",
        //"type":"supergroup"},"chat":{"id":-1002275129371,"title":"RoomRoom","type":"supergroup"},"date":1740271541,"text":"test\u2014\u2014\u2014\u2014"}}]}

        // "message":{"message_id":2,"from":{"id":5939411170,"is_bot":false,"first_name":"\uc7ac\uc601","last_name":"\uc591","language_code":"ko"},"chat":{"id":5939411170,"first_name":"\uc7ac\uc601","last_name":"\uc591","type":"private"},"date":1740272896,"text":"iiiiiiiiiiiiiiiiiiiiiiiiiiiii"}}]}

        // https://api.telegram.org/bot7656905376:AAGYB_pa0s_brGJ-yyZiP0zzm3Onpy1LQzg/getupdates
        // 안대표
        // https://api.telegram.org/bot8114311955:AAGjac5TGgmb8BDQaS1sBXFITOv4qOEe76s/getupdates


        // https://api.telegram.org/botYOUR_BOT_TOKEN/getUpdates

        // https://api.telegram.org/bot7656905376:AAGYB_pa0s_brGJ-yyZiP0zzm3Onpy1LQzg/sendMessage

        // string botToken = "7477332214:AAH2MBDRPc2aqeSg-0ya8Z7CJRd_61Wh624"; // 텔레그램 봇 토큰
        // string chatId = "-1002166323850"; // 메시지를 보낼 대상의 채팅 ID  552938097


        static async Task Main(string[] args)
        {
            // 안대표
            //string botToken = "8114311955:AAGjac5TGgmb8BDQaS1sBXFITOv4qOEe76s"; // 텔레그램 봇 토큰
            //string chatId = "-1002295706262"; // "5597895053"; // 메시지를 보낼 대상의 채팅 ID .            

            string botToken = "7656905376:AAGYB_pa0s_brGJ-yyZiP0zzm3Onpy1LQzg"; // 텔레그램 봇 토큰   
            string chatId = "-1002275129371";// "5939411170"; // 메시지를 보낼 대상의 채팅 ID  552938097              

            string imageUrl = "https://centercoin.kr/1.jpg"; // 이미지 URL

            var messageText = "🐰 *새로운 게임 Welcome to BadBunny Tap2Earn!* 🥕\n\n" +
                "Hop into the paws of BadBunny, unlock treasures, and piece together forgotten tales!\n\n" +
                "👉 *Tap Into the Adventure* \n" +
                "Ready your fingers, slice those carrots, and earn your Airdrop XP!\n\n" +
                "*Shop 🛠️* – Boost your power and customize your adventure!\n" +
                "*Friends 🔪* – Recruit and earn 5,000 tokens per friend!\n" +
                "*Special Tasks 🐾* – Complete tasks to earn big rewards!\n\n" +
                "_Stay alert for Airdrop Alerts 🍄!_";





            //SendMessageAsync(botToken,chatId, messageText);
            await uSendPhotoAsync(botToken, chatId, imageUrl, messageText);


            // 봇이 업데이트를 확인하도록 설정
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new Form1());
        }

        static async Task SendPhotoAsync(string botToken, string chatId, string imageUrl, string captionText)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.telegram.org/bot{botToken}/sendPhoto";

                var payload = new
                {
                    chat_id = chatId,
                    photo = imageUrl,
                    caption = captionText,
                    parse_mode = "Markdown",
                    reply_markup = new
                    {
                        inline_keyboard = new[]
                        {
                        new[]
                        {
                            new
                            {
                                text = "🚀 Start Game",
                                url = $"https://centercoin.kr/?chat_id={chatId}&username={{username}}"
                            }
                        },
                        new[]
                        {
                            new
                            {
                                text = "🚀 uGame Start Game",
                                url = $"https://centercoin.kr/ugame/?chat_id={chatId}&username={{username}}"
                            }
                        }
                    }
                    }
                };

                string jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // JSON 직렬화 오류 방지
                });

                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ 채널 메시지 전송 성공!");
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ 전송 실패: {response.StatusCode}");
                    Console.WriteLine($"📌 응답 내용: {errorResponse}");
                }
            }
        }

        static async Task uSendPhotoAsync(string botToken, string chatId, string imageUrl, string captionText)
        {
            using (HttpClient client = new HttpClient())
            {
                //    string url = $"https://api.telegram.org/bot{botToken}/sendMessage";
                string url = $"https://api.telegram.org/bot{botToken}/sendPhoto";

                var payload = new
                {
                    chat_id = chatId,
                    photo = imageUrl,
                    caption = captionText,
                    parse_mode = "Markdown",
                    reply_markup = new
                    {
                        inline_keyboard = new[]
                        {
                            new[]
                            {
                                new
                                {
                                    //기존 url = "https://..." → web_app = new { url = "https://..." }
                                    //이렇게 하면 텔레그램 내에서 웹페이지가 실행되며 창이 새로 열리지 않음!
                                    text = "🚀 Start Game",
                                    //web_app = new { url = "https://centercoin.kr/?chat_id={chatId}&username={{username}}" }
                                    // web_app = new { url = "https://centercoin.kr/?chat_id={chatId}" } // &username={{username}}" }
                                    // web_app = new { url = "https://centercoin.kr" } // WebView에서 실행됨
                                    web_app = new { url = "https://centercoin.kr/webapp" } // WebView에서 실행
                                }
                            },

                            //new[]   /setwebhook centercoin.kr
                            //{
                            //    new
                            //    {
                            //        text = "🚀 Start Game",
                            //        url = $"https://centercoin.kr/?chat_id={chatId}&username={{username}}"
                            //    }
                            //}

                            new[]
                            {
                                new
                                {
                                    text = "📢 Join Our Community",
                                   // web_app = new { url = "https://centercoin.kr/?chat_id={chatId}" } // &username={{username}}" }
                                    web_app = new { url = "https://centercoin.kr" } // &username={{username}}" }

                                }
                            }
                        }
                    }
                };



                string jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // JSON 직렬화 오류 방지
                });

                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ 채널 메시지 전송 성공!");
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ 전송 실패: {response.StatusCode}");
                    Console.WriteLine($"📌 응답 내용: {errorResponse}");
                }
            }
        }

        static async Task SendMessageAsync(string botToken, string chatId, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.telegram.org/bot{botToken}/sendMessage";

                var payload = new
                {
                    chat_id = chatId,
                    text = message,
                    parse_mode = "Markdown",
                    disable_web_page_preview = true, // 웹 페이지 미리보기 비활성화

                    reply_markup = new
                    {
                        inline_keyboard = new[]
                        {
                            new[]
                            {
                                new
                                {
                                   text = "🚀 Start Game",
                                    url = "https://centercoin.kr/?chat_id=" + chatId
                                }
                            }
                        }
                    }
                };

                string jsonPayload = JsonSerializer.Serialize(payload);
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("✅ 텍스트 메시지 전송 성공! (웹 미리보기 비활성화)");
                else
                    Console.WriteLine($"❌ 전송 실패: {response.StatusCode}");
            }
        }



        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //}
    }
}





// PHP 예제
/*
 
<? php
$chat_id = $_GET['chat_id'] ?? 'Unknown';
$username = $_GET['username'] ?? 'Unknown';

echo "User ID: $chat_id <br>";
echo "Username: $username";
?>







 Node.js (Express)

const express = require('express');
const app = express();

app.get('/', (req, res) => {
    const chatId = req.query.chat_id || 'Unknown';
    const username = req.query.username || 'Unknown';
    
    res.send(`User ID: ${chatId}, Username: ${username}`);
});

app.listen(3000, () => console.log('Server running on port 3000'));





🔹 방법 2: Telegram Deep Linking 활용 (start 파라미터)
Telegram의 Deep Linking을 활용하면, 유저가 Start Game 버튼을 눌렀을 때 Telegram 봇이 chat_id와 username을 저장한 후, 게임 서버로 전달할 수 있습니다.

✅ 1. "Start Game" 버튼 URL 변경
https://t.me/YOUR_BOT?start=unique_code

✅ 2. 사용자가 버튼 클릭 시, Telegram 봇에서 chat_id와 username 저장
C#에서 getUpdates API를 활용하여 사용자의 정보를 가져온 후, 게임 서버로 넘겨줄 수 있습니다.

csharp
복사
편집
static async Task GetUserInfoAsync(string botToken, string chatId)
{
    using (HttpClient client = new HttpClient())
    {
        string url = $"https://api.telegram.org/bot{botToken}/getChat?chat_id={chatId}";

        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseBody);
    }
}
✅ 3. 게임 서버로 chat_id와 username 전송
https://your-game-url.com/?chat_id={chat_id}&username={username}
이 URL을 Telegram 봇이 직접 호출하여 게임 서버에 전달하면 됩니다.


📌 결론
1️⃣ 간단한 방법 (방법 1)

URL에 chat_id와 username을 직접 포함해 전달.
서버에서 GET 요청으로 chat_id 수신 가능.
빠르고 간편하지만, 보안 이슈 존재 (URL에 chat_id 노출됨).
2️⃣ 안전한 방법 (방법 2)

Telegram start 파라미터를 이용하여, 유저가 봇을 시작하면 chat_id 저장 후 서버로 전달.
보안성이 높지만, 봇을 직접 운영해야 함.
사용 목적에 따라 선택하면 됩니다! 😊

*/




