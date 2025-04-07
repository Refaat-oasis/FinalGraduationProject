document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const paperName = document.getElementById("paperName");
    const Type = document.getElementById("Type");
    const Weight = document.getElementById("Weight");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const Colored = document.getElementById("Colored");

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

    function validate() {
        let valid = true;

        if (paperName.value.trim() === "") {
            setError(paperName, "برجاء إدخال اسم الورق");
            valid = false;
        } else {
            setSuccess(paperName);
        }

        if (Type.value.trim() === "") {
            setError(Type, "برجاء إدخال نوع الورق");
            valid = false;
        } else {
            setSuccess(Type);
        }

        if (Weight.value.trim() === "" || isNaN(Weight.value) || parseFloat(Weight.value) <= 0) {
            setError(Weight, "برجاء إدخال وزن الورق ");
            valid = false;
        } else {
            setSuccess(Weight);
        }

        if (Quantity.value.trim() === "" || isNaN(Quantity.value) || parseInt(Quantity.value) <= 0) {
            setError(Quantity, "برجاء إدخال كمية الورق  ");
            valid = false;
        } else {
            setSuccess(Quantity);
        }

        if (Price.value.trim() === "" || isNaN(Price.value) || parseFloat(Price.value) <= 0) {
            setError(Price, "برجاء إدخال سعر الورق ");
            valid = false;
        } else {
            setSuccess(Price);
        }

        if (Colored.value.trim() === "") {
            setError(Colored, "برجاء إدخال لون الورق");
            valid = false;
        } else {
            setSuccess(Colored);
        }

        return valid;
    }

    myform.addEventListener("submit", function (e) {
        e.preventDefault(); // Always prevent default first

        if (!validate()) {
            // Validation failed
            return false;
        } else {
            // Validation successful - submit the form programmatically
            this.submit();
        }
    });

    if (tempDataSuccess && tempDataSuccess.value === "true") {
        setTimeout(function () {
            window.location.href = " / Inventory / ViewAllPaper ";
        }, 3000); // 3 ثواني
    }
});