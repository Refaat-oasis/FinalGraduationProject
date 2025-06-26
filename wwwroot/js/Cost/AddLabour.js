document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const LabourId = document.getElementById("LabourId");
    const Name = document.getElementById("Name");
    const Price = document.getElementById("Price");

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

        //if (LabourId.value.trim() === "") {
        //    setError(LabourId, "برجاء إدخال الرقم التعريفي للعامل");
        //    valid = false;
        //} else {
        //    setSuccess(LabourId);
        //}

        if (Name.value.trim() === "") {
            setError(Name, "برجاء إدخال اسم العامل");
            valid = false;
        } else {
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(Name.value.trim())) {
                setError(Name, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(Name);
            }
        }


        if (Price.value.trim() === "" || isNaN(Price.value) || parseFloat(Price.value) <= 0) {
            setError(Price, "برجاء إدخال سعر العمليه العماليه ");
            valid = false;
        } else {
            setSuccess(Price);
        }

        if (Price.value.trim() === "") {
            setError(Price, "برجاء إدخال سعر العملية العمالية");
            valid = false;
        } else if (parseFloat(Price.value) <= 0) {
            setError(Price, "يجب إدخال قيمة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(Price);
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