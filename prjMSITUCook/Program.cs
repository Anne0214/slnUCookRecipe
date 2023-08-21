using Coravel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using prjMSITUCook.Models;
using prjMSITUCook.Models.Authorization;
using prjMSITUCook.Models.Coravel;
using prjMSITUCook.Models.Hubs;
using prjMSITUCook.Models.Services;
using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Implement;
using prjMSITUCookServices.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt => 
{
    opt.Cookie.HttpOnly = true;
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddDbContext<UcookRecipeDatabaseContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("UCookConnection")
));
builder.Services.AddScoped<IRecipeService,RecipeService>();
builder.Services.AddScoped<IFollowService,FollowService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IIndexService, IndexService>();


//=====redis=====
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "UCookRedis"; //名稱
    options.Configuration = "localhost"; //本地端，要先下載memurai
});

//=====signal r=====
builder.Services.AddSignalR();
//====注入IUserIdProvide我指定的實作====
builder.Services.AddSingleton<IUserIdProvider, MemberIdProvider>();

//AspNetCore Authentication用戶驗證操作機制註冊DI(在controller範圍外使用方式)
builder.Services.AddHttpContextAccessor();

//自訂用戶登入資訊操作註冊DI
builder.Services.AddScoped<MemberInfoService>();
//=======AspNetCore Authentication全域範圍的驗證組態設定=======(全環境cookie套用)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    //未登入會自動轉到該網址
    option.LoginPath = new PathString("/Home/Login");

    //未授權角色時會自動移轉到此網址
    option.AccessDeniedPath = new PathString("/Home/NoRule");
});
//=======授權相關di=======
builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IAuthorizationHandler, MinimumArticleAuthorizationHandler>());
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

//=======授權=======
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeastPost3Article", policy => policy.Requirements.Add(new MinimumArticleRequirement(3)));
});
//自訂的基於policy的授權



//=======排程(coravel)=======
//只使用coravel的任務調度功能
builder.Services.AddScheduler();
//註冊我的調度任務
builder.Services.AddScoped<AgriProdcutJob>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
//=======AspNetCore Authentication用戶登入驗證操作機制使用=======
//執行順序不能顛倒
app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();
//=======AspNetCore Authentication用戶登入驗證操作機制使用=======


//=======配置coravel任務=======
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<AgriProdcutJob>().DailyAtHour(8).RunOnceAtStart(); //每天早上八點
    //scheduler.Schedule<YourCoravelJob2>().Hourly().Monday(); // 每周一每小时执行一次

});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
//====signal r====
app.MapHub<NotifyHub>("/notifyHub");

app.Run();
