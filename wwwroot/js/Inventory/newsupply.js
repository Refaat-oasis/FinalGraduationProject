document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const supplyName = document.getElementById("supplyName");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const ReorderPoint = document.getElementById("ReorderPoint");
    const TotalBalance = document.getElementById("TotalBalance");


    function validate() {
        let valid = true;

        if (supplyName.value.trim() === "") {
            setError(supplyName, "برجاء إدخال اسم المستلزم");
            valid = false;
        } else {
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(supplyName.value.trim())) {
                setError(supplyName, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(supplyName);
            }
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) < 0) {
            setError(ReorderPoint, "برجاء عدم إدخال قيم سالبه");
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

    // عند وجود خطأ في التحقق
    function setError(input, message) {
        const inputBox = input.closest('.inputBox');
        const errorDisplay = inputBox.querySelector('.error');
        inputBox.classList.add('invalid');
        errorDisplay.innerText = message;
        errorDisplay.style.display = "block"; // إظهار
    }

    function setSuccess(input) {
        const inputBox = input.closest('.inputBox');
        const errorDisplay = inputBox.querySelector('.error');
        inputBox.classList.remove('invalid');
        errorDisplay.innerText = "";
        errorDisplay.style.display = "none"; // إخفاء
    }


    myform.addEventListener("submit", function (e) {
        e.preventDefault(); 

        if (!validate()) {
            return false;
        } else {
            this.submit();
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


