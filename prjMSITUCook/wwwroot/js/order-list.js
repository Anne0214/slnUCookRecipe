// ***頁面加載執行的動作***
document.addEventListener('DOMContentLoaded', function () {
    // 訂單按鈕及優惠券按鈕
    var ordersTab = document.getElementById('nav-orders-tab');
    var couponsTab = document.getElementById('nav-coupons-tab');

    // 訂單頁面及優惠券頁面
    var ordersContent = document.getElementById('nav-orders');
    var couponsContent = document.getElementById('nav-coupons');

    // 點擊訂單按鈕，要顯示訂單頁面
    ordersTab.addEventListener('click', function (event) {
        event.preventDefault(); //取消默認
        // 激活訂單按鈕，取消激活優惠券按鈕
        ordersTab.classList.add('active');
        couponsTab.classList.remove('active');
        // 顯示訂單頁面
        ordersContent.classList.add('show', 'active');
        couponsContent.classList.remove('show', 'active');
    });

    // 點擊優惠券按鈕，要顯示優惠券頁面
    couponsTab.addEventListener('click', function (event) {
        event.preventDefault(); // 取消默認
        // 激活優惠券按鈕，取消激活訂單按鈕
        ordersTab.classList.remove('active');
        couponsTab.classList.add('active');
        // 顯示優惠券頁面
        ordersContent.classList.remove('show', 'active');
        couponsContent.classList.add('show', 'active');
    });
});


// 在領取按鈕的 click 事件中處理請求和 Toast 顯示
// $('#liveToastBtn').click(function () {
//     const couponCode = $('#couponId').val();

//     fetch('/Shopping/OrderAndCouponList', {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify({ couponCode: couponCode })
//     })
//         .then(response => response.json())
//         .then(data => {
//             switch (data.result) {
//                 case 'Success':
//                     showToast('領取成功！', 'toast-success');
//                     break;
//                 case 'NotFound':
//                     showToast('查無此優惠券。', 'toast-warning');
//                     break;
//                 case 'NotAvailable':
//                     showToast('不在領取時間內。', 'toast-warning');
//                     break;
//                 case 'AlreadyReceived':
//                     showToast('您已領取過此優惠券。', 'toast-warning');
//                     break;
//                 case 'Expired':
//                     showToast('此優惠券已過期。', 'toast-danger');
//                     break;
//                 default:
//                     showToast('領取失敗。', 'toast-danger');
//             }
//         })
//         .catch(error => {
//             showToast('領取失敗。', 'toast-danger');
//         });
// });

// // 顯示 Toast 的函數
// function showToast(message, toastType) {
//     const liveToast = new bootstrap.Toast(document.getElementById('liveToast'));
//     const toastBody = document.getElementById('couponStatus');
//     toastBody.textContent = message;
//     toastBody.className = `toast-body ${toastType}`;
//     liveToast.show();
// }



////***切換"可使用"及"已失效"優惠券
//$('#usableLink').on('click', function () {
//    // 顯示 "可使用" 優惠券
//    $('#usableCoupons').show();
//    // 隱藏 "已失效" 優惠券
//    $('#expiredCoupons').hide();
//    $('#usedCoupons').hide();
//});
//$('#usedLink').on('click', function () {
//    // 顯示 "可使用" 優惠券
//    $('#usedCoupons').show();
//    // 隱藏 "已失效" 優惠券
//    $('#expiredCoupons').hide();
//    $('#usableCoupons').hide();
//});

//$('#expiredLink').on('click', function () {
//    // 隱藏 "可使用" 優惠券
//    $('#usableCoupons').hide();
//    // 顯示 "已失效" 優惠券
//    $('#expiredCoupons').show();
//    $('#usedCoupons').hide();
//});

