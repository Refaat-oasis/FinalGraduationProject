document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const supplyName = document.getElementById("supplyName");
    const quantity = document.getElementById("Quantity");
    const price = document.getElementById("Price");

    myform.addEventListener("submit", function (e) {
        if (!validate()) {
            e.preventDefault(); // يمنع إرسال الفورم إذا كان هناك خطأ
        }
    });

    function validate() {
        let valid = true;

        if (supplyName.value.trim() === "") {
            setError(supplyName, "برجاء إدخال اسم المستلزم");
            valid = false;
        } else {
            setSuccess(supplyName);
        }

        if (quantity.value.trim() === "" || isNaN(parseFloat(quantity.value)) || parseFloat(quantity.value) <= 0) {
            setError(quantity, "برجاء إدخال كمية المستلزم بشكل صحيح");
            valid = false;
        } else {
            setSuccess(quantity);
        }

        if (price.value.trim() === "" || isNaN(parseFloat(price.value)) || parseFloat(price.value) <= 0) {
            setError(price, "برجاء إدخال سعر المستلزم بشكل صحيح");
            valid = false;
        } else {
            setSuccess(price);
        }

        return valid;
    }

    function setError(input, errorMsg) {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = errorMsg;
        inputBox.classList.add("error");
    }

    function setSuccess(input) {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = "";
        inputBox.classList.remove("error");
    }
});