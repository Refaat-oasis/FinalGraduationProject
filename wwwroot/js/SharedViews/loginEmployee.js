document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const userName = document.getElementById("userName");
    const Password = document.getElementById("Password");

    myform.addEventListener("submit", function (e) {
        if (!validate()) {
            e.preventDefault();
        }
    });

    function validate() {
        let valid = true;

        if (userName.value.trim() === "") {
            setError(userName, "برجاء إدخال اسم المستخدم");
            valid = false;
        } else {
            setSuccess(userName);
        }

        if (Password.value.trim() === "") {
            setError(Password, "برجاء إدخال كلمة المرور");
            valid = false;
        } else {
            setSuccess(Password);
        }

        return valid;
    }

    const setError = (input, errorMsg) => {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.nextElementSibling;

        if (errorParagraph && errorParagraph.classList.contains("error")) {
            errorParagraph.innerText = errorMsg;
        }

        inputBox.classList.remove("success");
        inputBox.classList.add("error");
    };

    const setSuccess = (input) => {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.nextElementSibling;

        if (errorParagraph && errorParagraph.classList.contains("error")) {
            errorParagraph.innerText = "";
        }

        inputBox.classList.remove("error");
        inputBox.classList.add("success");
    };
});

// Toggle password visibility
document.addEventListener("DOMContentLoaded", function () {
    const passwordInput = document.getElementById("Password");
    const togglePassword = document.getElementById("togglePassword");

    togglePassword.addEventListener("click", function () {
        if (passwordInput.type === "password") {
            passwordInput.type = "text";
            togglePassword.classList.remove("fa-eye");
            togglePassword.classList.add("fa-eye-slash");
        } else {
            passwordInput.type = "password";
            togglePassword.classList.remove("fa-eye-slash");
            togglePassword.classList.add("fa-eye");
        }
    });
});








//document.addEventListener("DOMContentLoaded", function () {
//    const myform = document.getElementById("myform");
//    const userName = document.getElementById("userName");
//    const Password = document.getElementById("Password");

//    myform.addEventListener("submit", function (e) {
//        if (!validate()) {
//            e.preventDefault();
//        }
//    });

//    function validate() {
//        let valid = true;

//        if (userName.value.trim() === "") {
//            setError(userName, "برجاء إدخال اسم المستخدم");
//            valid = false;
//        } else {
//            setSuccess(userName);
//        }

//        if (Password.value.trim() === "") {
//            setError(Password, "برجاء إدخال كلمة المرور");
//            valid = false;
//        } else {
//            setSuccess(Password);
//        }

//        return valid;
//    }

//    const setError = (input, errorMsg) => {
//        const inputBox = input.parentElement;
//        const errorParagraph = inputBox.querySelector(".error");
//        errorParagraph.innerText = errorMsg;
//        inputBox.classList.remove("success");
//        inputBox.classList.add("error");
//    };

//    const setSuccess = (input) => {
//        const inputBox = input.parentElement;
//        const errorParagraph = inputBox.querySelector(".error");
//        errorParagraph.innerText = "";
//        inputBox.classList.remove("error");
//        inputBox.classList.add("success");
//    };
//});

//document.addEventListener("DOMContentLoaded", function () {
//    const passwordInput = document.getElementById("Password");
//    const togglePassword = document.getElementById("togglePassword");

//    togglePassword.addEventListener("click", function () {
//        if (passwordInput.type === "password") {
//            passwordInput.type = "text";
//            togglePassword.classList.remove("fa-eye");
//            togglePassword.classList.add("fa-eye-slash");
//        } else {
//            passwordInput.type = "password";
//            togglePassword.classList.remove("fa-eye-slash");
//            togglePassword.classList.add("fa-eye");
//        }
//    });
//});
