﻿document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const Name = document.getElementById("Name");
    const ReorderPoint = document.getElementById("ReorderPoint");
    const AverageQuantity = document.getElementById("AverageQuantity");
    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

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


    function validate() {
        let valid = true;
        const arabicRegex = /^[\u0600-\u06FF\s]+$/;

        if (!arabicRegex.test(Name.value.trim())) {
            setError(Name, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(Name);
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) < 0) {
            setError(ReorderPoint, "برجاء إدخال قيمة أكبر من او تساوي الصفر");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }

        if (AverageQuantity.value.trim() === "" || isNaN(AverageQuantity.value) || parseFloat(AverageQuantity.value) < 0) {
            setError(AverageQuantity, "برجاء عدم إدخال قيم سالبه");
            valid = false;
        } else {
            setSuccess(AverageQuantity);
        }

        return valid;
    }

    myform.addEventListener("submit", function (e) {
        if (!validate()) {
            e.preventDefault();
        }
    });

    if (tempDataElement?.value === 'true') {
        const jobRole = parseInt(jobRoleElement?.value) || 0;
        const jobRoleRoutes = {
            0: "/employee/AdminHome",
            1: "/employee/inventoryManager",
            2: "/employee/inventoryClerk",
            3: "/employee/TechnicalManager",
            4: "/employee/technicalClerk",
            5: "/employee/CostManager",
            6: "/employee/costClerk"
        };

        setTimeout(() => {
            const redirectUrl = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
            window.location.href = redirectUrl;
        }, 3000);
    }
});