using System;
using Newtonsoft.Json;

namespace Azure.SignalR.Server.Messages
{
    public class MessageDto
    {
        public string Body { get; set; }
        public string GroupName { get; set;}
    }
}