document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    if (!myform) {
        console.error("Form not found!");
        return;
    }

    const elements = {
        Name: document.getElementById("Name"),
        Quantity: document.getElementById("Quantity"),
        ReorderPoint: document.getElementById("ReorderPoint"),
        Price: document.getElementById("Price")
    };

    //for (const [key, element] of Object.entries(elements)) {
    //    if (!element) {
    //        console.error(`Element with ID '${key}' not found!`);
    //        return;
    //    }
    //}

    const setError = (input, errorMsg) => {
        const inputBox = input.parentElement;
        const errorElement = inputBox.querySelector(".error");
        if (errorElement) {
            errorElement.innerText = errorMsg;
            inputBox.classList.add("error");
        }
    };

    const setSuccess = (input) => {
        const inputBox = input.parentElement;
        const errorElement = inputBox.querySelector(".error");
        if (errorElement) {
            errorElement.innerText = "";
            inputBox.classList.remove("error");
        }
    };

    function validate() {
        let valid = true;
        if (!elements.Name.value) {
            setError(elements.Name, "برجاء إدخال اسم قطعه الغيار");
            valid = false;
        } else {
            setSuccess(elements.Name);
        }
        if (!elements.Price.value) {
            setError(elements.Price, "برجاء إدخال السعر");
            valid = false;
        } else if (isNaN(elements.Price.value) || parseFloat(elements.Price.value) <= 0) {
            setError(elements.Price, "يجب إدخال سعر أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.Price);
        }

        if (!elements.Quantity.value) {
            setError(elements.Quantity, "برجاء إدخال الكميه");
            valid = false;
        } else if (!elements.Quantity.value || isNaN(elements.Quantity.value) || parseFloat(elements.Quantity.value) <= 0) {
            setError(elements.Quantity, "يجب إدخال كمية صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.Quantity);
        }
        if (!elements.ReorderPoint.value) {
            setError(elements.ReorderPoint, "برجاء إدخال نقطه إعاده الشراء");
            valid = false;
        } else if (!elements.ReorderPoint.value || isNaN(elements.ReorderPoint.value) || parseFloat(elements.ReorderPoint.value) <= 0) {
            setError(elements.ReorderPoint, "يجب إدخال قيمة صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.ReorderPoint);
        }
        return valid;
    }

    myform.addEventListener("submit", function (e) {
        e.preventDefault();
        if (validate()) {
            this.submit();
        }
    });

    Object.values(elements).forEach(element => {
        element.addEventListener("input", function () {
            if (this.value.trim() !== "") {
                setSuccess(this);
            }
        });
    });

    const tempDataElement = document.getElementById('tempDataSuccess');
    const jobRoleElement = document.getElementById('hdnJobRole');

    const hasSuccessMessage = tempDataElement ? tempDataElement.value === 'true' : false;
    const jobRole = jobRoleElement ? parseInt(jobRoleElement.value) : 0;

    console.log("Success message exists:", hasSuccessMessage);
    console.log("Job role:", jobRole);

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














