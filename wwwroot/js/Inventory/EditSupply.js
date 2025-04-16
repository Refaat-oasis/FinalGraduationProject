document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const Name = document.getElementById("Name");
    const ReorderPoint = document.getElementById("ReorderPoint");
    const TotalBalance = document.getElementById("TotalBalance");


    const setError = (input, errorMsg) => {
        const parent = input.parentElement;
        const errorSpan = parent.querySelector(".error");
        errorSpan.innerText = errorMsg;
        errorSpan.style.color = "red";
        input.classList.add("error");
    };

    const setSuccess = (input) => {
        const parent = input.parentElement;
        const errorSpan = parent.querySelector(".error");
        errorSpan.innerText = "";
        input.classList.remove("error");
        input.classList.add("success");
    };

    function validate() {
        let valid = true;

        let arabicRegex = /^[\u0600-\u06FF\s]+$/;
        if (!arabicRegex.test(Name.value.trim())) {
            setError(Name, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(Name);
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
            setError(ReorderPoint, "برجاء إدخال قيمة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }

        if (TotalBalance.value.trim() === "" || isNaN(TotalBalance.value) || parseFloat(TotalBalance.value) <= 0) {
            setError(TotalBalance, "برجاء إدخال قيمة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(TotalBalance);
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