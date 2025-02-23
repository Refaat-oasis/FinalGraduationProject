//<script>
//    const myform = document.getElementById("myform");
//    const userName = document.getElementById("userName");
//    const Password = document.getElementById("Password");

//    myform.addEventListener("submit", (e) => {
//        e.preventDefault();
//    if (validate()) {
//        window.location.href = " / Admin / AdminHome";
//        }
//    });

//    myform.addEventListener("reset", () => {
//        window.location.reload();
//    });

//    const setError = (input, errorMsg) => {
//        const inputBox = input.parentElement;
//    const errorParagraph = inputBox.querySelector(".error");
//    errorParagraph.innerText = errorMsg;
//    inputBox.classList.remove("success");
//    inputBox.classList.add("error");
//    };

//    const setSuccess = (input) => {
//        const inputBox = input.parentElement;
//    const errorParagraph = inputBox.querySelector(".error");
//    errorParagraph.innerText = "";
//    inputBox.classList.remove("error");
//    inputBox.classList.add("success");
//    };

//    const validate = () => {
//        let valid = true;

//    // validate name
//    if (userName.value.trim() === "") {
//        setError(userName, "برجاء إدخال الاسم");
//    valid = false;
//        } else {
//        setSuccess(userName);
//        }

//    // validate password
//    if (Password.value.trim() === "") {
//        setError(Password, "برجاء إدخال كلمه المرور");
//    valid = false;
//        } else {
//        setSuccess(Password);
//        }

//    return valid;
//    };
//</script>
