document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const Name = document.getElementById("Name");
    const ReorderPoint = document.getElementById("ReorderPoint"); 

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

        return valid;
    }

    myform.addEventListener("submit", function (e) {
        if (!validate()) {
            e.preventDefault();
        }
    });

    // ✅ التحقق من TempData والقيام بعملية إعادة التوجيه
    if (tempDataSuccess && tempDataSuccess.value === "true") {
        setTimeout(function () {
            window.location.href = " / Inventory / ViewAllInk ";
        }, 3000); // 3 ثواني
    }
});

