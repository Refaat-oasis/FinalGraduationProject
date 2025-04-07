document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector(".Employee-form");
    form.addEventListener("submit", function (event) {
        let isValid = true;
        let employeeUserName = document.querySelectorAll(".place")[0];
        let employeeName = document.querySelectorAll(".place")[1];
        let employeePassword = document.querySelectorAll(".place")[2];
        let jobRole = document.querySelector("#options");
        let userNameError = document.querySelectorAll(".error")[0];
        let nameError = document.querySelectorAll(".error")[1];
        let passwordError = document.querySelectorAll(".error")[2];
        let jobRoleError = document.querySelectorAll(".error")[3];
        userNameError.innerText = "";
        nameError.innerText = "";
        passwordError.innerText = "";
        jobRoleError.innerText = "";
        if (employeeName.value.trim() === "") {
            nameError.innerText = "رجاء إدخال اسمك";
            isValid = false;
        }
        if (employeeUserName.value.trim() === "") {
            userNameError.innerText = " رجاءإدخال اسم المستخدم";
            isValid = false;
        }
        if (employeePassword.value.trim() === "") {
            passwordError.innerText = "رجاء إدخال كلمة المرور الخاصة بك";
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

    if (tempDataSuccess && tempDataSuccess.value === "true") {
        setTimeout(function () {
            window.location.href = " / Admin / ViewAllEmployee ";
        }, 3000); // 3 ثواني
    }
});