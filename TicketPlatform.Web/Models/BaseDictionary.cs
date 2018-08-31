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
        public int Code { get; set; }
        public int Value { get; set; }
        public int Remark { get; set; }

    }
}
