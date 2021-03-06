﻿using System.Net;
using System.Threading.Tasks;
using TeleBotDotNet.Responses.Methods;

namespace TeleBotDotNet.Extensions
{
    public static class TeleBotExtensions
    {
        /// <summary>
        /// Download a file as a byte array using a GetFileResponse.
        /// </summary>
        public static byte[] DownloadFile(this TeleBot bot, GetFileResponse getFileResponse)
        {
            bot.Log.Info(nameof(DownloadFile));

            return DownloadFileAsync(bot, getFileResponse).Result;
        }

        /// <summary>
        /// Download a file as a byte array async using a GetFileResponse.
        /// </summary>
        public static async Task<byte[]> DownloadFileAsync(this TeleBot bot, GetFileResponse getFileResponse)
        {
            bot.Log.Info(nameof(DownloadFileAsync));

            if (string.IsNullOrEmpty(getFileResponse?.Result?.FilePath))
            {
                return null;
            }

            using (var client = new WebClient())
            {
                try
                {
                    return await client.DownloadDataTaskAsync($"{TeleBot.ApiUrl}/file/bot{bot.ApiToken}/{getFileResponse.Result.FilePath}");
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
