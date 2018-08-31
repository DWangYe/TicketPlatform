using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketPlatform.Web.Models
{
    /// <summary>
    /// 码表
    /// </summary>
    public class BaseDictionary
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }

    }
}
