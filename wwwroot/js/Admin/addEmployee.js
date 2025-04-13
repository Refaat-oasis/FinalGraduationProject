document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".Employee-form");
    form.addEventListener("submit", function (event) {
        let isValid = true;

        let employeeID = document.querySelectorAll(".place")[0];
        let employeeUserName = document.querySelectorAll(".place")[1];
        let employeeName = document.querySelectorAll(".place")[2];
        let employeePassword = document.querySelectorAll(".place")[3];
        let jobRole = document.querySelector("#options");
        let employeeIDError = document.querySelectorAll(".error")[0];
        let userNameError = document.querySelectorAll(".error")[1];
        let nameError = document.querySelectorAll(".error")[2];
        let passwordError = document.querySelectorAll(".error")[3];
        let jobRoleError = document.querySelectorAll(".error")[4];
        employeeIDError.innerText = "";
        userNameError.innerText = "";
        nameError.innerText = "";
        passwordError.innerText = "";
        jobRoleError.innerText = "";

        if (employeeID.value.trim() === "") {
            employeeIDError.innerText = "رجاء إدخال الرقم القومي للموظف";
            isValid = false;
        }
        if (employeeName.value.trim() === "") {
            nameError.innerText = "رجاء إدخال اسم الموظف";
            isValid = false;
        }
        if (employeeUserName.value.trim() === "") {
            userNameError.innerText = " رجاءإدخال اسم المستخدم";
            isValid = false;
        }
        if (employeePassword.value.trim() === "") {
            passwordError.innerText = "رجاء إدخال كلمة المرور الخاصة بالموظف";
            passwordError.style.display = "block";
            isValid = false;
        } else if (employeePassword.value.length < 6) {
            passwordError.innerText = "يجب أن تكون كلمة المرور 6 أحرف على الأقل";
            passwordError.style.display = "block";
            isValid = false;
        }
        if (jobRole.value === "") {
            jobRoleError.innerText = "رجاء تحديد دور وظيفي";
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