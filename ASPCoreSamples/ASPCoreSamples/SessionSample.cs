using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ASPCoreSamples
{
    public class SessionSample
    {
        private const string SessionVisits = nameof(SessionVisits);
        private const string SessionTimeCreated = nameof(SessionTimeCreated);

        public static async Task SessionAsync(HttpContext context)
        {
            int visits = context.Session.GetInt32(SessionVisits) ?? 0;
            string timeCreated = context.Session.GetString(SessionTimeCreated) ?? string.Empty;
            if (string.IsNullOrEmpty(timeCreated))
            {
                timeCreated = DateTime.Now.ToString("t", CultureInfo.InvariantCulture);
                context.Session.SetString(SessionTimeCreated, timeCreated);
            }
            DateTime timeCreated2 = DateTime.Parse(timeCreated);
            context.Session.SetInt32(SessionVisits, ++visits);

            await context.Response.WriteAsync(HtmlEncoder.Default.Encode(
                $"Session内Visits的数量：{visits}" +
                $"创建于{timeCreated2:T}；"+
                $"当前时间：{DateTime.Now:T}"
                ));
        }

    }
}
