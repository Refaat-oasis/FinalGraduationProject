document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("myform");
    const type = document.getElementById("Type");
    const colorInput = document.getElementById("Colored");
    const weightInput = document.getElementById("Weight");
    const sizeInput = document.getElementById("Size");

    const divColor = document.getElementById("colorInput");
    const divWeight = document.getElementById("weightInput");
    const divSize = document.getElementById("sizeInput");

    function toggleFields() {
        const selected = parseInt(type.value);
        divSize.style.display = selected === 0 ? "block" : "none";
        divWeight.style.display = selected === 1 ? "block" : "none";
        divColor.style.display = selected === 2 ? "block" : "none";
    }

    type.addEventListener("change", toggleFields);
    toggleFields(); // عند التحميل الأولي

    const setError = (input, msg) => {
        const parent = input.closest(".inputBox");
        const errorSpan = parent ? parent.querySelector(".error") : null;
        if (errorSpan) {
            errorSpan.innerText = msg;
            errorSpan.style.color = "red";
        }
        input.classList.add("error");
        input.classList.remove("success");
    };

    const setSuccess = (input) => {
        const parent = input.closest(".inputBox");
        const errorSpan = parent ? parent.querySelector(".error") : null;
        if (errorSpan) {
            errorSpan.innerText = "";
        }
        input.classList.remove("error");
        input.classList.add("success");
    };

    function validate() {
        let valid = true;
        const selectedType = parseInt(type.value);

        if (isNaN(selectedType)) {
            setError(type, "برجاء اختيار نوع المواصفة");
            valid = false;
        } else {
            setSuccess(type);
        }

        if (selectedType === 0) {
            if (!colorInput.value.trim()) {
                setError(colorInput, "برجاء إدخال اللون");
                valid = false;
            } else {
                setSuccess(colorInput);
            }
        } else if (selectedType === 1) {
            if (!weightInput.value.trim() || isNaN(weightInput.value)) {
                setError(weightInput, "برجاء إدخال وزن رقمي");
                valid = false;
            } else {
                setSuccess(weightInput);
            }
        } else if (selectedType === 2) {
            if (!sizeInput.value.trim()) {
                setError(sizeInput, "برجاء إدخال القياس");
                valid = false;
            } else {
                setSuccess(sizeInput);
            }
        }

        return valid;
    }

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        if (validate()) {
            form.submit();
        }
    });

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const hasSuccessMessage = tempDataElement ? tempDataElement.value === 'true' : false;
    const jobRole = jobRoleElement ? parseInt(jobRoleElement.value) : 0;

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
        setTimeout(() => {
            const redirectUrl = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
            window.location.href = redirectUrl;
        }, 3000);
    }
});