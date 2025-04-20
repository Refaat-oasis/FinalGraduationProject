document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".Employee-form");

    form.addEventListener("submit", function (event) {
        let isValid = true;

        // Get form elements
        const employeeUserName = document.querySelectorAll(".place")[0];
        const employeeName = document.querySelectorAll(".place")[1];
        const employeePassword = document.querySelectorAll(".place")[2];

        // Get error elements
        const userNameError = document.querySelectorAll(".error")[0];
        const nameError = document.querySelectorAll(".error")[1];
        const passwordError = document.querySelectorAll(".error")[2];

        // Reset error messages
        userNameError.style.display = "none";
        nameError.style.display = "none";
        passwordError.style.display = "none";

        // Username validation
        if (employeeUserName.value.trim() === "") {
            userNameError.innerText = "الرجاء إدخال اسم المستخدم";
            userNameError.style.display = "block";
            isValid = false;
        }

        // Name validation
        if (employeeName.value.trim() === "") {
            nameError.innerText = "يجب عليك إدخال اسمك";
            nameError.style.display = "block";
            isValid = false;
        }

        // Password validation (only numbers)
        if (employeePassword.value.trim() !== "") {  // Only validate if password field is not empty
            if (employeePassword.value.length < 6) {
                passwordError.innerText = "يجب أن تكون كلمة المرور 6 أحرف على الأقل";
                passwordError.style.display = "block";
                isValid = false;
            } else if (!/^\d+$/.test(employeePassword.value)) {
                passwordError.innerText = "يجب أن تحتوي كلمة المرور على أرقام فقط";
                passwordError.style.display = "block";
                isValid = false;
            }
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

