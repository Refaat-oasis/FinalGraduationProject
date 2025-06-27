document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    if (!myform) {
        console.error("Form not found!");
        return;
    }

    const elements = {
        paperName: document.getElementById("paperName"),
        Size: document.querySelector("[name='Size']"), 
        Weight: document.querySelector("[name='Weight']"),
        Quantity: document.getElementById("Quantity"),
        Colored: document.querySelector("[name='Colored']"),
        ReorderPoint: document.getElementById("ReorderPoint"),
        Price: document.getElementById("Price")
    };

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

        if (!elements.paperName.value.trim()) {
            setError(elements.paperName, "مطلوب إدخال اسم الورق");
            valid = false;
        } else if (!/^[\u0600-\u06FF\s]+$/.test(elements.paperName.value)) {
            setError(elements.paperName, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(elements.paperName);
        }

        if (elements.Size.value === "none") {
            setError(elements.Size, "مطلوب اختيار مقاس الورق");
            valid = false;
        } else {
            setSuccess(elements.Size);
        }

        if (elements.Weight.value === "none") {
            setError(elements.Weight, "مطلوب اختيار وزن الورق");
            valid = false;
        } else {
            setSuccess(elements.Weight);
        }

        if (elements.Colored.value === "none") {
            setError(elements.Colored, "مطلوب اختيار لون الورق");
            valid = false;
        } else {
            setSuccess(elements.Colored);
        }

        if (!elements.ReorderPoint.value || isNaN(elements.ReorderPoint.value) || parseFloat(elements.ReorderPoint.value) < 0) {
            setError(elements.ReorderPoint, "يجب إدخال قيمة صحيحة أكبر من او تساوي الصفر");
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

    Object.values(elements).forEach(element => {
        if (element) {
            element.addEventListener("input", function () {
                if (this.value.trim() !== "") {
                    setSuccess(this);
                }
            });
        }
    });

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    if (tempDataElement && tempDataElement.value === 'true') {
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

        console.log("Redirecting to:", jobRoleRoutes[jobRole] || "/Employee/LoginPage");

        setTimeout(() => {
            window.location.href = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
        }, 3000);
    }
});