//數量器用

$("#minus_btn").click(function () {

    var count = $("#num").val();
    count--;
    $("#num").val(count);
})

$("#add_btn").click(function () {

    var count = $("#num").val();
    count++;
    $("#num").val(count);
})

//tab
$(".tab a").click(function (event) {
    event.preventDefault();
    $(this).addClass("active").siblings().removeClass("active");
    $($(this).attr("href")).show().siblings().hide();

})

//slider
$(".slider-nav>img").click(
    function () {
        var img = $(this).attr("src")
        $(this).addClass("active").siblings().removeClass("active");
        $(".slider-for img").fadeOut(300, function () {
            $(".slider-for img").attr("src", img);
            $(".slider-for img").fadeIn(300);
        });

    });