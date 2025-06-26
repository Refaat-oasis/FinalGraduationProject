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


        if (Colored.value.trim() === "") {
            setError(Colored, "برجاء إدخال لون الحبر");
            valid = false;
        } else {
            setSuccess(Colored);
        }

       
        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) < 0) {
            setError(ReorderPoint, "برجاء عدم إدخال قيم سالبه");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }


        return valid;
    }



    myform.addEventListener("submit", function (e) {
        e.preventDefault(); 

        if (!validate()) {
            return false;
        } else {
            myform.submit();
        }
    });

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const hasSuccessMessage = tempDataElement ? tempDataElement.value === 'true' : false;
    const jobRole = jobRoleElement ? parseInt(jobRoleElement.value) : 0;

    console.log("Success message exists:", hasSuccessMessage);
    console.log("Job role:", jobRole);

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








