document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    if (!myform) return;

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const elements = {
        Name: document.getElementById("Name"),
        Size: document.querySelector("select[name='Size']"),
        Colored: document.querySelector("select[name='Colored']"),
        Weight: document.querySelector("select[name='Weight']"),
        ReorderPoint: document.getElementById("ReorderPoint")
    };

    for (const [key, element] of Object.entries(elements)) {
        if (!element) {
            console.error(`Element ${key} not found`);
            return;
        }
    }

    const setError = (input, errorMsg) => {
        const parent = input.closest(".inputBox");
        const errorSpan = parent?.querySelector(".error");
        if (errorSpan) {
            errorSpan.textContent = errorMsg;
            input.classList.add("error");
        }
    };

    const setSuccess = (input) => {
        const parent = input.closest(".inputBox");
        const errorSpan = parent?.querySelector(".error");
        if (errorSpan) {
            errorSpan.textContent = "";
            input.classList.remove("error");
        }
    };

    function validate() {
        let valid = true;
        const arabicRegex = /^[\u0600-\u06FF\s]+$/;

        if (!arabicRegex.test(elements.Name.value.trim())) {
            setError(elements.Name, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(elements.Name);
        }

        const requiredFields = {
            Weight: "برجاء اختيار الوزن",
            Size: "برجاء اختيار المقاس",
            Colored: "برجاء اختيار اللون"
        };

        for (const [field, message] of Object.entries(requiredFields)) {
            if (!elements[field].value.trim()) {
                setError(elements[field], message);
                valid = false;
            } else {
                setSuccess(elements[field]);
            }
        }

        if (!elements.ReorderPoint.value.trim() ||
            isNaN(elements.ReorderPoint.value) ||
            parseFloat(elements.ReorderPoint.value) <= 0) {
            setError(elements.ReorderPoint, "برجاء إدخال قيمة صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.ReorderPoint);
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
            console.log("Redirecting to:", redirectUrl); 
            window.location.href = redirectUrl;
        }, 3000);
    }
});
