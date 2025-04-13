document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".Vendor-form");

    form.addEventListener("submit", function (event) {
        let isValid = true;

        let VendorName = document.querySelectorAll(".place")[0];
        let VendorPhone = document.querySelectorAll(".place")[1];
        let VendorAddress = document.querySelectorAll(".place")[2];
        let VendorEmail = document.querySelectorAll(".place")[3];

        let VendorNameError = document.querySelectorAll(".error")[0];
        let VendorPhoneError = document.querySelectorAll(".error")[1];
        let VendorAddressError = document.querySelectorAll(".error")[2];
        let VendorEmailError = document.querySelectorAll(".error")[3];

        // Clear previous errors
        VendorNameError.innerText = "";
        VendorPhoneError.innerText = "";
        VendorAddressError.innerText = "";
        VendorEmailError.innerText = "";

        // Name validation
        if (VendorName.value.trim() === "") {
            VendorNameError.innerText = "رجاء إدخال اسمك";
            isValid = false;
        }

        // Phone validation - only numbers
        if (VendorPhone.value.trim() === "") {
            VendorPhoneError.innerText = "رجاء إدخال رقمك";
            isValid = false;
        } else if (!/^\d+$/.test(VendorPhone.value)) {
            VendorPhoneError.innerText = "يجب إدخال أرقام فقط";
            isValid = false;
        } else if (VendorPhone.value.length < 11) {
            VendorPhoneError.innerText = "يجب أن لا يقل الرقم عن 11 رقم";
            isValid = false;
        }

        // Address validation
        if (VendorAddress.value.trim() === "") {
            VendorAddressError.innerText = "رجاء إدخال عنوانك";
            isValid = false;
        }

        // Email validation
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (VendorEmail.value.trim() === "") {
            VendorEmailError.innerText = "رجاء إدخال الإيميل";
            isValid = false;
        } else if (!emailRegex.test(VendorEmail.value)) {
            VendorEmailError.innerText = "رجاء إدخال إيميل صحيح (يجب أن يحتوي على @ و .)";
            isValid = false;
        }

        if (!isValid) {
            event.preventDefault();
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