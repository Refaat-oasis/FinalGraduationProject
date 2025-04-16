document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const CustomerName = document.getElementById("CustomerName");
    const Address = document.getElementById("Address");
    const Email = document.getElementById("Email");
    const Phone = document.getElementById("Phone");


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

        if (CustomerName.value.trim() === "") {
            setError(CustomerName, "برجاء إدخال اسم العميل");
            valid = false;
        } else {
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(CustomerName.value.trim())) {
                setError(CustomerName, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(CustomerName);
            }
        }

        if (Address.value.trim() === "") {
            setError(Address, "برجاء إدخال عنوان العميل");
            valid = false;
        } else if (!/^[\u0621-\u064A0-9\s]+$/.test(Address.value.trim())) {
            setError(Address, "برجاء إدخال عنوان صحيح");
            valid = false;
        } else {
            setSuccess(Address);
        }

        if (Email.value.trim() === "") {
            setError(Email, "برجاء إدخال الإيميل الإلكتروني الخاص بالعميل");
            valid = false;
        } else if (!Email.value.includes("@")) {
            setError(Email, "برجاء إدخال إيميل إلكتروني صحيح ");
            valid = false;
        } else {
            setSuccess(Email);
        }

        if (Phone.value.trim() === "") {
            setError(Phone, "برجاء إدخال رقم الهاتف الخاص بالعميل");
            valid = false;
        } else if (!/^\d+$/.test(Phone.value.trim())) {
            setError(Phone, "برجاء إدخال رقم هاتف صحيح");
            valid = false;
        } else {
            setSuccess(Phone);
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
