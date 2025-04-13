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



        VendorNameError.innerText = "";
        VendorPhoneError.innerText = "";
        VendorAddressError.innerText = "";
        VendorEmailError.innerText = "";


        if (VendorName.value.trim() === "") {
            VendorNameError.innerText = "رجاء إدخال اسم المورد";
            isValid = false;
        }

        if (VendorPhone.value.trim() === "") {
            VendorPhoneError.innerText = "رجاء إدخال رقم المورد";
            VendorPhoneError.style.display = "block";
            isValid = false;
        } else if (VendorPhone.value.length < 11) {
            VendorPhoneError.innerText = "يجب ان الرقم لا يقل عن 11 رقم";
            VendorPhoneError.style.display = "block";
            isValid = false;
        }

        if (VendorAddress.value.trim() === "") {
            VendorAddressError.innerText = " رجاءإدخال عنوان المورد";
            isValid = false;
        }


        if (VendorEmail.value.trim() === "") {
            VendorEmailError.innerText = "  رجاء إدخال الايميل الخاص بالمورد";
            isValid = false;
        }


        if (!isValid) {
            event.preventDefault();
        }
    });

    if (tempDataSuccess && tempDataSuccess.value === "true") {
        setTimeout(function () {
            window.location.href = " / Inventory / ViewAllVendor ";
        }, 3000); // 3 ثواني
    }
});