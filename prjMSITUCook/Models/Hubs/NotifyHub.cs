using Microsoft.AspNetCore.SignalR;

namespace prjMSITUCook.Models.Hubs
{
    public class NotifyHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            UserHandler.ConnectedIds.Add(Context.UserIdentifier);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (UserHandler.ConnectedIds.Any(x => x == Context.UserIdentifier))
            {
                UserHandler.ConnectedIds.Remove(Context.UserIdentifier);
            }
            return base.OnDisconnectedAsync(exception);
        }
        //我追蹤了某人或按讚了某人的文章，某人如果在線上會收到通知
        public async Task IFollowOrLikeSomeone(string whom)
        {
            //確認是否在線上
            if (UserHandler.ConnectedIds.Any(x => x == whom)) {
                await Clients.User(whom).SendAsync("GetNewNotification");
            }
        }
        //我取消追蹤了某人或取消按讚了某人的文章，某人如果在線上會收到事件
        public async Task IUnFollowOrCancelLikeSomeone(string whom)
        {
            //確認是否在線上
            if (UserHandler.ConnectedIds.Any(x => x == whom))
            {
                await Clients.User(whom).SendAsync("RefreshNotification");
            }
        }


    }
    public static class UserHandler
    {
        public static List<string> ConnectedIds = new List<string>();
    }
}

