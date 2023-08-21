$("#search-target").change(
    function() {
        var value = $(this).val();
        if (value == 1) {
            $("#search-input").attr("placeholder", "請輸入食譜名稱");
        } else { $("#search-input").attr("placeholder", "請輸入食譜作者名稱"); }
    }
);