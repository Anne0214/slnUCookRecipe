// 食譜封面圖
$(document).on("click", "#preview-cover", function() {
    $('#RecipeCoverFile').click();
    $(document).on("change", "#RecipeCoverFile", function() {
        var fileObj = $("#RecipeCoverFile")[0];
        var img = document.getElementById('preview-cover');
        var reader = new FileReader();
        reader.onload = function(e) {
            if (reader.readyState === 2) {
                img.src = e.target.result;
                console.log(img.src);
            }
        }
        reader.readAsDataURL(fileObj.files[0]);
    });
});
//步驟圖上傳(1)

//$(document).on("click", "#preview-step1", function() {
//    $("#StepList_0__StepPictureFile").click();
//    $(document).on("change", "#StepList_0__StepPictureFile", function() {
//        var fileObj = $("#StepList_0__StepPictureFile")[0];
//        var img = document.getElementById('preview-step1');
//        var reader = new FileReader();
//        reader.onload = function(e) {
//            if (reader.readyState === 2) {
//                img.src = e.target.result;
//                console.log(img.src);
//            }
//        }
//        reader.readAsDataURL(fileObj.files[0]);
//    });
//});


//表單驗證
// 如果验证不通过禁止提交表单
function validate() {
    'use strict';
    window.addEventListener('load', function() {
        // 获取表单验证样式
        var forms = document.getElementsByClassName('needs-validation');
        // 循环并禁止提交
        var validation = Array.prototype.filter.call(forms, function(form) {
            form.addEventListener('submit', function(event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
};
validate();

//繫節圖片點擊的function
function OnImgClick(i) {
    const photo_id = `#preview-step${i}`
    const input_id = `input[name='StepList[${i}].StepPictureFile']`
    $(document).on("click", photo_id, function () {
        $(input_id).click();
        $(document).on("change", input_id, function () {
            var fileObj = $(input_id)[0];
            var img = document.getElementById(photo_id.replace(`#`, ``));
            var reader = new FileReader();
            reader.onload = function (e) {
                if (reader.readyState === 2) {
                    img.src = e.target.result;
                    console.log(img.src);
                }
            }
            reader.readAsDataURL(fileObj.files[0]);
        });
    });
}

//繫節所有步驟的img&input
const length = $(".step").length;
for (let i = 0; i < length; i++) {
    OnImgClick(i)
}


//大按鈕 : 產步驟div

$("#addStep").click(function (event) {
    event.preventDefault();
    //step_count是步驟的編號(外)
    let step_count = $(".step").length;
    step_count++
    const step = `<div class="step d-flex align-items-center">
                    <div style="width:200px;">
                        <img src="/Images/recipe/UploadImage.png" alt="步驟預覽圖" id="preview-step${step_count - 1}" style="width:200px;height:150px;object-fit:cover;border-radius:5px;" >
                        <input type="file" class="form-control" accept="image/png, image/x-png, image/jpeg, image/pjpeg" id="StepList[${step_count - 1}].StepPictureFile" name="StepList[${step_count - 1}].StepPictureFile" >
                    </div>
                    <div style="position:relative;" class="flex-fill">
                        <span class="number">${step_count}</span>
                        <span class="btns">
                            <i class="fa-solid fa-trash-can"></i>
                            <i class="fa-solid fa-plus"></i>
                        </span>
                        <textarea id="StepList[${step_count - 1}].StepDescription" name="StepList[${step_count - 1}].StepDescription" maxlength="200" minlength="5" class="form-control" rows="4" placeholder="請輸入步驟說明" required></textarea>
                    </div>
                `;

    $("#steps").append(step);
    //讓步驟list數增加
    OnImgClick(step_count-1)

})

//步驟刪除鈕的動態繫結
$(document).on('click', '.step .btns .fa-trash-can', function () {
    //如果步驟數已經是1就不可刪除
    if ($(".step").length == 1) {
        Swal.fire({
            icon: 'error',
            title: '只剩一個不能再刪啦...',
        })
        return;
    }

    //step:當前刪除步驟的編號
    let step_count = $(this).closest(".step").find(".number").text();
    step = parseInt(step_count, 10)

    ////解除要刪除的步驟的動態繫結
    //繫節所有步驟的img&input
    let count1 = $(".step").length;
    for (let i = 0; i < count1; i++) {
        $(document).off('click', `#preview-step${i}`);
    }

    //重新編號當前刪除編號的以下所有
    $(this).closest(".step").nextAll(".step").each(function () {

        $(this).find(".number").text(step_count)
        //圖片上傳preview
        $(this).find("img").attr('id', `preview-step${step_count - 1}`)

        //圖片上傳input
        $(this).find("input").attr('name', `StepList[${step_count - 1}].StepPictureFile`)
        $(this).find("input").attr('id', `StepList[${step_count - 1}].StepPictureFile`)
        //步驟說明
        $(this).find("textarea").attr('name', `StepList[${step_count - 1}].StepDescription`)
        $(this).find("textarea").attr('id', `StepList[${step_count - 1}].StepDescription`)
        step_count++
    })
    
    $(this).closest(".step").remove();


});

//步驟新增鈕的動態繫結
$(document).on('click', '.step .btns .fa-plus', function () {
    //當前點擊的步驟號碼(外層)
    let step_count = $(this).closest(".step").find(".number").text();
    step_count = parseInt(step_count, 10)
    //step_count要新增的步驟的編號
    step_count++
    console.log(step_count)
    const step = `<div class="step d-flex align-items-center">
                    <div style="width:200px;">
                         <img src="/Images/recipe/UploadImage.png" alt="步驟預覽圖" id="preview-step${step_count - 1}" style="width:200px;height:150px;object-fit:cover;border-radius:5px;" >
                         <input type="file" class="form-control" accept="image/png, image/x-png, image/jpeg, image/pjpeg" id="StepList[${step_count - 1}].StepPictureFile" name="StepList[${step_count - 1}].StepPictureFile" >
                    </div>
                    <div style="position:relative;" class="flex-fill">
                        <span class="number">${step_count}</span>
                        <span class="btns">
                            <i class="fa-solid fa-trash-can"></i>
                            <i class="fa-solid fa-plus"></i>
                        </span>
                                                <textarea id="StepList[${step_count - 1}].StepDescription" name="StepList[${step_count - 1}].StepDescription" maxlength="200" minlength="5" class="form-control" rows="4" placeholder="請輸入步驟說明" required></textarea>
                    </div>
                `;
    $(this).closest(".step").after(step); //新增步驟





    //重新編號新元素及其後面的元素
    $(this).closest(".step").nextAll(".step").each(function () {

        //步驟號碼
        $(this).find(".number").text(step_count)
        //圖片上傳preview
        $(this).find("img").attr('id', `preview-step${step_count - 1}`)
        //圖片上傳input
        $(this).find("input").attr('name', `StepList[${step_count - 1}].StepPictureFile`)
        $(this).find("input").attr('id', `StepList[${step_count - 1}].StepPictureFile`)
        //步驟說明
        $(this).find("textarea").attr('name', `StepList[${step_count - 1}].StepDescription`)
        $(this).find("textarea").attr('id', `StepList[${step_count - 1}].StepDescription`)
        step_count++
    })
    //////繫結步驟圖片上傳(移除step_count的再重新繫節)
    //const img_here = `#preview-step${step_count-1}`
    //$(document).off('click', `#preview-step${step_count - 1}`);

    //OnImgClick(step_count-1);

    //////重新細節圖片上傳:移除新增後最後一個的再重新細節
    //const last = $(".step").length - 1;//最後一個的編號
    //const img_last = `#preview-step${last}`
    //$(document).off('click', `#preview-step${last}`);
    //OnImgClick(last);

    let count1 = $(".step").length;
    for (let i = 0; i < count1; i++) {
        $(document).off('click', `#preview-step${i}`);
        OnImgClick(i)
    }


});

//產食材div


$("#addFood").click(function (event) {
    event.preventDefault();
    let food_count = $(".food").length;
    const food = ` <div class="food row">
                        <div class="col-6">
                            <input type="text" class="form-control" placeholder="名稱"  id="FoodList[${food_count}].FoodName" name="FoodList[${food_count}].FoodName" required/>
                        </div>
                        <div class="col-5">
                            <input type="text" class="form-control" placeholder="份量" id="FoodList[${food_count}].FoodAmount" name="FoodList[${food_count}].FoodAmount" required/>
                        </div>
                        <div style="font-size: 1.3rem;
                        cursor: pointer;" class="col-1">
                            <i class="fa-solid fa-trash-can"></i>
                        </div>
                        </div>`;
    $(".foods").append(food);

})

//食材刪除鈕的動態繫結
$(".foods").on('click', '.food .fa-trash-can', function () {
    //重新編號
    let count = 0;
    if ($(".food").length == 1) {
        Swal.fire({
            icon: 'error',
            title: '只剩一個不能再刪啦...',
        })
        return;
    }

    $(this).closest(".food").nextAll(".food").each(function (i, e) {
        //每個id、name減一
        //食物名稱

        count = parseInt($(this).find("input").eq(0).attr('name').split("[")[1].split("]")[0]); //拿到當前元素的數字
        $(this).find("input").eq(0).attr('name', `FoodList[${count - 1}].FoodName`);
        $(this).find("input").eq(0).attr('id', `FoodList[${count - 1}].FoodName`);

        //食物分量
        $(this).find("input").eq(1).attr('name', `FoodList[${count - 1}].FoodAmount`);
        $(this).find("input").eq(1).attr('id', `FoodList[${count - 1}].FoodAmount`);
    })

    //刪除
    $(this).closest(".food").remove();

})