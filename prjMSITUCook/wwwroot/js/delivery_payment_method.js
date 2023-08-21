// 當送貨方式或付款方式選擇改變時觸發
function showPageContent() {
    // 取得送貨方式和付款方式的選擇
    var deliveryMethod = document.getElementById("delivery-method").value;
    var paymentMethod = document.getElementById("payment-method").value;

    // 顯示相對應的內容
    if (deliveryMethod === "home_delivery") {
        document.getElementById("宅配").classList.remove('hide')
        document.getElementById("711取貨不付款").classList.add('hide')
        if (paymentMethod === "credit-card") {
            document.getElementById("信用卡").classList.remove('hide')
            document.getElementById("銀行轉帳").classList.add('hide')
            document.getElementById("LINEPAY").classList.add('hide')
        } else if (paymentMethod === "transfer") {
            document.getElementById("信用卡").classList.add('hide')
            document.getElementById("銀行轉帳").classList.remove('hide')
            document.getElementById("LINEPAY").classList.add('hide')
        } else if (paymentMethod === "linepay") {
            document.getElementById("信用卡").classList.add('hide')
            document.getElementById("銀行轉帳").classList.add('hide')
            document.getElementById("LINEPAY").classList.remove('hide')
        }

    } else if (deliveryMethod === "711_delivery") {
        document.getElementById("宅配").classList.add('hide')
        document.getElementById("711取貨不付款").classList.remove('hide')
        if (paymentMethod === "credit-card") {
            document.getElementById("信用卡").classList.remove('hide')
            document.getElementById("銀行轉帳").classList.add('hide')
            document.getElementById("LINEPAY").classList.add('hide')
        } else if (paymentMethod === "transfer") {
            document.getElementById("信用卡").classList.add('hide')
            document.getElementById("銀行轉帳").classList.remove('hide')
            document.getElementById("LINEPAY").classList.add('hide')
        } else if (paymentMethod === "linepay") {
            document.getElementById("信用卡").classList.add('hide')
            document.getElementById("銀行轉帳").classList.add('hide')
            document.getElementById("LINEPAY").classList.remove('hide')
        }

    }
};




//// 在頁面載入完成後設定事件監聽
//window.onload = function () {
//    // 監聽送貨方式和付款方式的選擇改變事件
//    document.getElementById("delivery-method").addEventListener("change", showPageContent);
//    document.getElementById("payment-method").addEventListener("change", showPageContent);

//    // 一開始就執行一次顯示相對應的內容
//    showPageContent();
//};

