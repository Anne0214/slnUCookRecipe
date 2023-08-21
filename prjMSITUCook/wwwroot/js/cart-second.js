


//勾選隱私權才開啟button
const agreeTermsCheckbox = document.getElementById('agreeTerms');
const submitButton = document.getElementById('submitButton');

//一開始不開放點選
submitButton.disabled = true;

//監聽checkbox
agreeTermsCheckbox.addEventListener('change', function () {
    if (this.checked) {
        submitButton.disabled = false;
    } else {
        submitButton.disabled = true;
    }
});






