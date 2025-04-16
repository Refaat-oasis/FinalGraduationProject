document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const InkName = document.getElementById("InkName");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const Colored = document.getElementById("Colored");
    const TotalBalance = document.getElementById("TotalBalance");
    const ReorderPoint = document.getElementById("ReorderPoint");


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

        if (InkName.value.trim() === "") {
            setError(InkName, "برجاء إدخال اسم الحبر");
            valid = false;
        } else {
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(InkName.value.trim())) {
                setError(InkName, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(InkName);
            }
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

        if (TotalBalance.value.trim() === "" || isNaN(TotalBalance.value) || parseFloat(TotalBalance.value) <= 0) {
            setError(TotalBalance, "برجاء إدخال القيمة المتاحه للحبر");
            valid = false;
        } else {
            setSuccess(TotalBalance);
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
            setError(ReorderPoint, "برجاء إدخال نقطة إعادة الطلب");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
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
            myform.submit();
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








