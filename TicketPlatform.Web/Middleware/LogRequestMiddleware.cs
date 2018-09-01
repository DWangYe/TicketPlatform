using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketPlatform.Web.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ILogger _logger_db;
        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetLogger("RequestLogger");
            //_logger_db= LogManager.GetLogger("DbLogger");
        }

        public async Task Invoke(HttpContext context)
        {
            string format = "请求ID：{0}\r\n" +
                "请求RemoteIpAddress：{1}\r\n" +
                "请求Method：{2}\r\n" +
                "请求Path：{3}\r\n" +
                "请求Body：{4}\r\n" +
                "请求QueryString：{5}\r\n" +
                "请求Scheme：{6}\r\n";
            _logger.Info(format,
                context.Connection.Id,
                context.Connection.RemoteIpAddress,
                context.Request.Method,
                context.Request.Path,
                "",
                context.Request.QueryString.HasValue?context.Request.QueryString.Value:"",
                CollectionToString(context.Request.Cookies));
            //数据库记录
            //_logger_db.Info("测试增加log");
            await _next(context);
        }

        private string CollectionToString(IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            StringBuilder re = new StringBuilder();
            foreach (var keyValue in keyValues)
            {
                re.Append(keyValue.Key + "=" + keyValue.Value + "&");
            }
            return re.ToString();
        }
    }
}
