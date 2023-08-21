
const author = `<input type="text" class="form-control" placeholder="請輸入作者名稱" id="AuthorName" name="AuthorName">`

const recipe = `<input type="text" class="form-control" placeholder="輸入食譜名稱"  id="RecipeName" name="RecipeName">
                <input type="text" class="form-control" placeholder="食材名稱，請以空白隔開"  id="FoodNames" name="FoodNames">`
const advanced_search = `<button class="btn-advanced-search" type="button" data-bs-toggle="collapse" data-bs-target="#collapseAdvanceSearch" aria-expanded="false" aria-controls="collapseAdvanceSearch">
            進階搜尋▾
        </button>
        <div class="collapse" id="collapseAdvanceSearch">
            <div class="row">
                <div class="col-6">
                    <label for="Amount" class="form-label">份量</label>
                    <select id="Amount" name="Amount" class="form-select">
                        <option value="all">不限</option>
                        <option value="1">1人</option>
                        <option value="2">2人</option>
                        <option value="3">3人</option>
                        <option value="4">4人</option>
                        <option value="5">5人</option>
                        <option value="6">6人</option>
                        <option value="7">7人</option>
                        <option value="8">8人</option>
                        <option value="9">9人</option>
                        <option value="10">10人</option>
                    </select>
                </div>
                <div class="col-6">
                    <label for="time" class="form-label">時間</label>
                        <select id="CostTime" name="CostTime" class="form-select">
                        <option value="all">不限</option>
                        <option value="5">5分鐘</option>
                        <option value="10">10分鐘</option>
                        <option value="15">15分鐘</option>
                        <option value="20">20分鐘</option>
                        <option value="30">30分鐘</option>
                        <option value="45">45分鐘</option>
                        <option value="60">60分鐘</option>
                        <option value="90">90分鐘</option>
                        <option value="120">120分鐘</option>
                        <option value="180+">180+分鐘</option>
                    </select>
                </div>
            </div>
        </div>`


$("#search-target").change(function () {
    $("#button-addon2").attr("disabled", true);
    const target = $("#search-target").val()
    //UI改變
    $("#search-input").find("input").remove();


    if (target == 1) {
        $("#search-input").after(advanced_search);
        $("#search-input").prepend(recipe);
        $("form").attr("action", "/Recipe/SearchResult");

    } else {
        $("#search-input").next(".btn-advanced-search").remove();
        $("#search-input").next("#collapseAdvanceSearch").remove();
        $("#search-input").prepend(author);
        $("form").attr("action", "/Recipe/SearchAuthorResult");
    }
    //修改form的目標:<form method="get" action="/Recipe/SearchResult">

})


//如果輸入框input  change 後，任一有內容，才可以able按鈕，否則disable
$(document).on('change', "#FoodNames", CheckRecipeSearchInputValue)
$(document).on('change', "#RecipeName", CheckRecipeSearchInputValue)
$(document).on('change', "#AuthorName", function () {
    if ($("#search-target").val() == 2) {
        const authorName = $("#AuthorName").val();
        if (authorName.length === 0) {
            $("#button-addon2").attr("disabled", true);
        } else {
            $("#button-addon2").removeAttr('disabled')
        }
    }
})

function CheckRecipeSearchInputValue() {
    if ($("#search-target").val() == 1) {
        const foodNames = $("#FoodNames").val();
        const recipeName = $("#RecipeName").val();

        if (foodNames.length === 0 & recipeName.length === 0) {
            $("#button-addon2").attr("disabled", true);
        } else {
            $("#button-addon2").removeAttr('disabled')
        }
    }
}