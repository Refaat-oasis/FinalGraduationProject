document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const InkName = document.getElementById("InkName");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const Colored = document.getElementById("Colored");
    const ExpireDate = document.getElementById("ExpireDate");

    const setError = (input, errorMsg) => {
        const inputBox = input.parentElement;
        let errorParagraph = inputBox.querySelector(".error");

        if (!errorParagraph) {
            errorParagraph = document.createElement("p");
            errorParagraph.classList.add("error");
            inputBox.appendChild(errorParagraph);
        }

        errorParagraph.innerText = errorMsg;
        inputBox.classList.remove("success");
        inputBox.classList.add("error");
    };

    const setSuccess = (input) => {
        const inputBox = input.parentElement;
        let errorParagraph = inputBox.querySelector(".error");

        if (errorParagraph) {
            errorParagraph.innerText = "";
        }

        inputBox.classList.remove("error");
        inputBox.classList.add("success");
    };

    function validate() {
        let valid = true;

        if (InkName.value.trim() === "") {
            setError(InkName, "برجاء إدخال اسم الحبر");
            valid = false;
        } else {
            setSuccess(InkName);
        }

        if (Quantity.value.trim() === "" || isNaN(Quantity.value) || parseInt(Quantity.value) <= 0) {
            setError(Quantity, "برجاء إدخال كمية الحبر");
            valid = false;
        } else {
            setSuccess(Quantity);
        }

        if (Price.value.trim() === "" || isNaN(Price.value) || parseFloat(Price.value) <= 0) {
            setError(Price, "برجاء إدخال سعر الحبر");
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
            setError(ExpireDate, "برجاء إدخال تاريخ انتهاء صلاحية الحبر");
            valid = false;
        } else {
            setSuccess(ExpireDate);
        }

        return valid;
    }

    myform.addEventListener("submit", function (e) {
        e.preventDefault(); 

        if (!validate()) {
            return false; 
        } else {
            this.submit(); 
        }
    });

    // ✅ التحقق من TempData والقيام بعملية إعادة التوجيه
    if (tempDataSuccess && tempDataSuccess.value === "true") {
        setTimeout(function () {
            window.location.href = " / Inventory / ViewAllInk ";
        }, 3000); // 3 ثواني
    }
});
