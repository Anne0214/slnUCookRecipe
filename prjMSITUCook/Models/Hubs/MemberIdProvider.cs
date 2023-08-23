using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace prjMSITUCook.Models.Hubs
{
    public class MemberIdProvider : IUserIdProvider
    {

        string? IUserIdProvider.GetUserId(HubConnectionContext connection)
        {
            var id = connection.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return id;
        }

    }
}
