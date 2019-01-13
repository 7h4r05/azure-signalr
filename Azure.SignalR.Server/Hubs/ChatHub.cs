using System.Threading.Tasks;
using Azure.SignalR.Server.Messages;
using Microsoft.AspNetCore.Authorization;

namespace Azure.SignalR.Server.Hubs
{
    [Authorize(AuthenticationSchemes="SignalR")]
    public class Chat:Microsoft.AspNetCore.SignalR.Hub
    { 
         public Task Send(MessageDto message)
         {
             var connectionId = Context.ConnectionId;
             if(string.IsNullOrWhiteSpace(message.GroupName)){
                return Clients.AllExcept(new[] {connectionId}).SendCoreAsync("Send", new[] {message});
             }else{
                 return Clients.OthersInGroup(message.GroupName).SendCoreAsync("Send", new[] { message});
             }
         } 

         public Task Join(string groupName){
             return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
         }
     } 
}
