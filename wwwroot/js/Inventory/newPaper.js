document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    if (!myform) {
        console.error("Form not found!");
        return;
    }

    const elements = {
        paperName: document.getElementById("paperName"),
        Size: document.getElementById("Size"),
        Weight: document.getElementById("Weight"),
        Quantity: document.getElementById("Quantity"),
        Colored: document.getElementById("Colored"),
        TotalBalance: document.getElementById("TotalBalance"),
        ReorderPoint: document.getElementById("ReorderPoint"),
        Price: document.getElementById("Price")
    };

    for (const [key, element] of Object.entries(elements)) {
        if (!element) {
            console.error(`Element with ID '${key}' not found!`);
            return;
        }
    }

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

        if (!elements.paperName.value.trim()) {
            setError(elements.paperName, "مطلوب إدخال اسم الورق");
            valid = false;
        } else if (!/^[\u0600-\u06FF\s]+$/.test(elements.paperName.value)) {
            setError(elements.paperName, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(elements.paperName);
        }

        if (elements.Size.value === "none") {
            setError(elements.Size, "مطلوب اختيار مقاس الورق");
            valid = false;
        } else {
            setSuccess(elements.Size);
        }

        if (elements.Weight.value === "none") {
            setError(elements.Weight, "مطلوب اختيار وزن الورق");
            valid = false;
        } else {
            setSuccess(elements.Weight);
        }

        //if (!elements.Quantity.value || isNaN(elements.Quantity.value) || parseFloat(elements.Quantity.value) <= 0) {
        //    setError(elements.Quantity, "يجب إدخال كمية صحيحة أكبر من الصفر");
        //    valid = false;
        //} else {
        //    setSuccess(elements.Quantity);
        //}

        if (elements.Colored.value === "none") {
            setError(elements.Colored, "مطلوب اختيار لون الورق");
            valid = false;
        } else {
            setSuccess(elements.Colored);
        }

        //if (!elements.TotalBalance.value || isNaN(elements.TotalBalance.value) || parseFloat(elements.TotalBalance.value) <= 0) {
        //    setError(elements.TotalBalance, "يجب إدخال قيمة صحيحة أكبر من الصفر");
        //    valid = false;
        //} else {
        //    setSuccess(elements.TotalBalance);
        //}

        if (!elements.ReorderPoint.value || isNaN(elements.ReorderPoint.value) || parseFloat(elements.ReorderPoint.value) <= 0) {
            setError(elements.ReorderPoint, "يجب إدخال قيمة صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.ReorderPoint);
        }

        //if (!elements.Price.value || isNaN(elements.Price.value) || parseFloat(elements.Price.value) <= 0) {
        //    setError(elements.Price, "يجب إدخال سعر صحيح أكبر من الصفر");
        //    valid = false;
        //} else {
        //    setSuccess(elements.Price);
        //}

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














