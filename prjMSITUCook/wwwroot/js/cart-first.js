//// 在表單提交後導向到第二頁
//document.getElementById("myForm").addEventListener("submit", function (event) {
//    // 取消表單的預設提交行為
//    event.preventDefault();

//    // 導向第二頁的 URL
//    var nextPageURL = "BuyerInfo.cshtml";

//    // 在同一視窗中開啟第二頁
//    window.location.href = nextPageURL;
//});


/***plusProduct左右 */

//const cardBox = document.getElementById("cardBox");
//const addCardGroup = document.querySelector(".addCardGroup");
//const sectionCard = document.querySelectorAll(".addSectionCard");
//const buttonRight = document.getElementById("buttonRight");
//const buttonLeft = document.getElementById("buttonLeft");
//let scrollAmount = 0;
//let step = sectionCard[0].offsetWidth + 20; // 20 是 margin-left 和 margin-right 的總和
//let numCardsToShow = 4; // 每次點擊顯示的卡片數量

//// 右箭頭按鈕點擊事件
//buttonRight.addEventListener("click", () => {
//    // 計算可滑動的總位移
//    const maxScrollAmount = (sectionCard.length - numCardsToShow) * step;

//    // 確認位移值不超過可滑動的總位移
//    if (scrollAmount < maxScrollAmount) {
//        scrollAmount += step;
//    }

//    // 移動 sectionCard 容器
//    addCardGroup.style.transform = `translateX(-${scrollAmount}px)`;
//});

//// 左箭頭按鈕點擊事件
//buttonLeft.addEventListener("click", () => {
//    // 確認位移值不小於 0
//    if (scrollAmount > 0) {
//        scrollAmount -= step;
//    }

//    // 移動 sectionCard 容器
//    addCardGroup.style.transform = `translateX(-${scrollAmount}px)`;
//});

