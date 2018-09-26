using MenuPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MenuPlanner.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email,"提交邮件", 
                $"请点击链接并提交账户: <a href='{HtmlEncoder.Default.Encode(link)}'>链接</a>");
        }
    }
}
