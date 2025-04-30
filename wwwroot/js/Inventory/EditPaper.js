document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const Name = document.getElementById("Name");
    const Type = document.querySelector("select[name='Type']");
    const Size = document.querySelector("select[name='Size']");
    const Colored = document.querySelector("select[name='Colored']");
    const Weight = document.querySelector("select[name='Weight']");
    const ReorderPoint = document.getElementById("ReorderPoint")

    const setError = (input, errorMsg) => {
        const parent = input.closest(".inputBox");
        const errorSpan = parent ? parent.querySelector(".error") : null;
        if (errorSpan) {
            errorSpan.innerText = errorMsg;
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
        const arabicRegex = /^[\u0600-\u06FF\s]+$/;

        if (!arabicRegex.test(Name.value.trim())) {
            setError(Name, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(Name);
        }

        if (!Weight || Weight.value.trim() === "") {
            setError(Weight, "برجاء اختيار الوزن");
            valid = false;
        } else {
            setSuccess(Weight);
        }

        if (!Size || Size.value.trim() === "") {
            setError(Size, "برجاء اختيار حجم الورق");
            valid = false;
        } else {
            setSuccess(Size);
        }

        if (!Colored || Colored.value.trim() === "") {
            setError(Colored, "برجاء اختيار لون الورق");
            valid = false;
        } else {
            setSuccess(Colored);
        }

        if (!Type || Type.value.trim() === "") {
            setError(Type, "برجاء اختيار نوع الورق");
            valid = false;
        } else {
            setSuccess(Type);
        }

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
            setError(ReorderPoint, "برجاء إدخال قيمة رقمية صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }

        return valid;
    }

    myform.addEventListener("submit", function (e) {
        e.preventDefault();

        if (validate()) {
            this.submit();
        }
    });

    // التعامل مع TempData للرسالة والوظيفة
    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const hasSuccessMessage = tempDataElement?.value === 'true';
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

    if (hasSuccessMessage) {
        setTimeout(() => {
            const redirectUrl = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
            window.location.href = redirectUrl;
        }, 3000);
    }
});
