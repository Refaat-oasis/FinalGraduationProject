document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const InkName = document.getElementById("InkName");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const Colored = document.getElementById("Colored");
    const ExpireDate = document.getElementById("ExpireDate");




    myform.addEventListener("submit", function (e) {
        if (!validate()) {
            e.preventDefault();
        }
    });

    function validate() {
        let valid = true;

        if (InkName.value.trim() === "") {
            setError(InkName, "برجاء إدخال اسم الحبر");
            valid = false;
        } else {
            setSuccess(InkName);
        }

        if (Quantity.value.trim() === "" || isNaN(Quantity.value) || Quantity.value <= 0) {
            setError(Quantity, "برجاء إدخال كمية الحبر  ");
            valid = false;
        } else {
            setSuccess(Quantity);
        }

        if (Price.value.trim() === "" || isNaN(Price.value) || Price.value <= 0) {
            setError(Price, "برجاء إدخال سعر الحبر ");
            valid = false;
        } else {
            setSuccess(Price);
        }

        if (Colored.value.trim() === "") {
            setError(Colored, "برجاء إدخال لون الحبر");
            valid = false;
        } else {
            setSuccess(Colored);
        }

        if (ExpireDate.value.trim() === "") {
            setError(ExpireDate, "برجاء إدخال تاريخ انتهاء صلاحيه الحبر");
            valid = false;
        } else {
            setSuccess(ExpireDate);
        }
        return valid;
    }

    const setError = (input, errorMsg) => {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = errorMsg;
        inputBox.classList.remove("success");
        inputBox.classList.add("error");
    };

    const setSuccess = (input) => {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = "";
        inputBox.classList.remove("error");
        inputBox.classList.add("success");
    };
});