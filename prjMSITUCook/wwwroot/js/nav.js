// $(".nav-notification-tab li").click(switch_nav_notification_tab);
$(".nav-notification-tab li").mouseenter(switch_nav_notification_tab);


function switch_nav_notification_tab() {
    //切換通知列表的tab
    $(this).addClass("active").siblings().removeClass("active");


    if($(this).text() =="互動"){
        $("#notification-new").removeClass("no-display").siblings().addClass("no-display");

    }

    if ($(this).text() == '追蹤') {

        $("#notification-follow").removeClass("no-display").siblings().addClass("no-display");
    }
    
}