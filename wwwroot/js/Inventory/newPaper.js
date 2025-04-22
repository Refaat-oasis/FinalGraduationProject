document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const paperName = document.getElementById("paperName");
    const Type = document.getElementById("Type");
    const Weight = document.getElementById("Weight");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const Colored = document.getElementById("Colored");
    const ReorderPoint = document.getElementById("ReorderPoint");
    const TotalBalance = document.getElementById("TotalBalance");



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
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(paperName.value.trim())) {
                setError(paperName, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(paperName);
            }
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

        //if (Quantity.value.trim() === "" || isNaN(Quantity.value) || parseInt(Quantity.value) <= 0) {
        //    setError(Quantity, "برجاء إدخال كمية الورق  ");
        //    valid = false;
        //} else {
        //    setSuccess(Quantity);
        //}

        //if (Price.value.trim() === "" || isNaN(Price.value) || parseFloat(Price.value) <= 0) {
        //    setError(Price, "برجاء إدخال سعر الورق ");
        //    valid = false;
        //} else {
        //    setSuccess(Price);
        //}

        if (Colored.value.trim() === "") {
            setError(Colored, "برجاء إدخال لون الورق");
            valid = false;
        } else {
            setSuccess(Colored);
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
            setError(ReorderPoint, "برجاء إدخال قيمة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }

        //if (TotalBalance.value.trim() === "" || isNaN(TotalBalance.value) || parseFloat(TotalBalance.value) <= 0) {
        //    setError(TotalBalance, "برجاء إدخال قيمة أكبر من الصفر");
        //    valid = false;
        //} else {
        //    setSuccess(TotalBalance);
        //}

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
    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    // Get values with proper fallbacks
    const hasSuccessMessage = tempDataElement ? tempDataElement.value === 'true' : false;
    const jobRole = jobRoleElement ? parseInt(jobRoleElement.value) : 0;

    console.log("Success message exists:", hasSuccessMessage);
    console.log("Job role:", jobRole);

    // Mapping of job roles to their respective URLs
    const jobRoleRoutes = {
        0: "/employee/AdminHome",
        1: "/employee/inventoryManager",
        2: "/employee/inventoryClerk",
        3: "/employee/TechnicalManager",
        4: "/employee/technicalClerk",
        5: "/employee/CostManager",
        6: "/employee/costClerk"
    };

    if (hasSuccessMessage) {
        setTimeout(function () {
            const redirectUrl = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
            window.location.href = redirectUrl;
        }, 3000);
    }
});