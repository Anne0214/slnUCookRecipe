
//得到當前搜尋的參數
var queryStr = location.search.split("?")[1];

var arr = queryStr.split("&");

var parameter = {}

for (var i = 0; i < arr.length; i++) {
    var g = arr[i].split("=");
    parameter[g[0]] = g[1]
}
console.log(parameter)

//刷新程序
function RefreshByOrder() {
    console.log(1)
    const SortMode = $("#order").val();
    const uri = "@Url.Content("~/Recipe/_RecipeListPartialView")" + `/?recipename=${parameter["RecipeName"]}&foodnames=${parameter["FoodNames"]}&amount=${parameter["Amount"]}&costtime=${parameter["CostTime"]}&page=1&sortmode=${SortMode}`
    $.ajax({
        url: uri,
        type: "GET",
        success: (data) => { $("#d-result").html(data); },
        error: (error) => { alert("加載失敗") }
    })
}

//改select的order觸發刷新，此處要用動態繫結，因為select在partial view那裏
$(document).on("change", "#order", function () {
    RefreshByOrder();

})


        ////非同步更新function
        //async function RefreshByOrder(){
        //    const SortMode = $("#order").val();
        //    const uri = "@Url.Content("~/Recipe/_RecipeListPartialView")" + `/?recipename=${parameter["RecipeName"]}&foodnames=${parameter["FoodNames"]}&amount=${parameter["Amount"]}&costtime=${parameter["CostTime"]}&page=1&sortmode=${SortMode}`
        //    const response = await fetch(uri)
        //    $(".list").html(response);
        //}

        ////當order選單更換時，刷新_RecipeListPartialView
        //$("#order").change(RefreshByOrder)