document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const CustomerName = document.getElementById("CustomerName");
    const Address = document.getElementById("Address");
    const Email = document.getElementById("Email");
    const Phone = document.getElementById("Phone");

    function setError(input, message) {
        const inputBox = input.closest('.inputBox');
        const errorDisplay = inputBox.querySelector('.error');
        inputBox.classList.add('invalid');
        errorDisplay.innerText = message;
        errorDisplay.style.display = "block";
    }

    function setSuccess(input) {
        const inputBox = input.closest('.inputBox');
        const errorDisplay = inputBox.querySelector('.error');
        inputBox.classList.remove('invalid');
        errorDisplay.innerText = "";
        errorDisplay.style.display = "none";
    }

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
        e.preventDefault();
        if (!validate()) {
            return false;
        } else {
            this.submit();
        }
    });

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const hasSuccessMessage = tempDataElement ? tempDataElement.value === 'true' : false;
    const jobRole = jobRoleElement ? parseInt(jobRoleElement.value) : 0;

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

    const errorNotification = document.getElementById('serverErrorNotification');
    if (errorNotification) {
        errorNotification.style.display = 'block';
        setTimeout(() => {
            errorNotification.style.display = 'none';
        }, 3000);
    }

    const successNotification = document.getElementById('serverSuccessNotification');
    if (successNotification) {
        successNotification.style.display = 'block';
        setTimeout(() => {
            successNotification.style.display = 'none';
        }, 3000);
    }

});
   
