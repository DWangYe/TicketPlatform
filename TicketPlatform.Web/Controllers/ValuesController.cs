using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace TicketPlatform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger logger;

        public ValuesController(ILogger logger)
        {
            this.logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            for (int i = 0; i < 100; i++)
            {
                logger.Info(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ",日志记录测试");
                logger.Debug(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ",日志记录测试");
                logger.Error(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ",日志记录测试");
                logger.Fatal(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + ",日志记录测试");
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
