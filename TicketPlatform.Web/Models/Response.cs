using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TicketPlatform.Web.Models
{
    public enum ResponseStatus
    {
        failure = 0,
        success = 1
    }
    public class BaseResponse
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; } = ResponseStatus.failure;

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string TotalRecord { get; set; }
    }

    [Serializable]
    [DataContract]
    public class BaseResponse<T>:BaseResponse
    {
        [DataMember]
        public T Data { get; set; }
    }
}
