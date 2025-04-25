document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    if (!myform) {
        console.error("Form not found!");
        return;
    }

    // تحديد العناصر
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

    // التحقق من وجود العناصر
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

    // التحقق من الحقول المطلوبة
    function validate() {
        let valid = true;

        // اسم الورق (عربي فقط)
        if (!elements.paperName.value.trim()) {
            setError(elements.paperName, "مطلوب إدخال اسم الورق");
            valid = false;
        } else if (!/^[\u0600-\u06FF\s]+$/.test(elements.paperName.value)) {
            setError(elements.paperName, "يجب أن يحتوي الاسم على حروف عربية فقط");
            valid = false;
        } else {
            setSuccess(elements.paperName);
        }

        // المقاس (يجب اختياره)
        if (elements.Size.value === "none") {
            setError(elements.Size, "مطلوب اختيار مقاس الورق");
            valid = false;
        } else {
            setSuccess(elements.Size);
        }

        // الوزن (يجب اختياره)
        if (elements.Weight.value === "none") {
            setError(elements.Weight, "مطلوب اختيار وزن الورق");
            valid = false;
        } else {
            setSuccess(elements.Weight);
        }

        // الكمية (رقم موجب)
        if (!elements.Quantity.value || isNaN(elements.Quantity.value) || parseFloat(elements.Quantity.value) <= 0) {
            setError(elements.Quantity, "يجب إدخال كمية صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.Quantity);
        }

        // اللون (يجب اختياره)
        if (elements.Colored.value === "none") {
            setError(elements.Colored, "مطلوب اختيار لون الورق");
            valid = false;
        } else {
            setSuccess(elements.Colored);
        }

        // القيمة المتاحة (رقم موجب)
        if (!elements.TotalBalance.value || isNaN(elements.TotalBalance.value) || parseFloat(elements.TotalBalance.value) <= 0) {
            setError(elements.TotalBalance, "يجب إدخال قيمة صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.TotalBalance);
        }

        // نقطة إعادة الطلب (رقم موجب)
        if (!elements.ReorderPoint.value || isNaN(elements.ReorderPoint.value) || parseFloat(elements.ReorderPoint.value) <= 0) {
            setError(elements.ReorderPoint, "يجب إدخال قيمة صحيحة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.ReorderPoint);
        }

        // السعر (رقم موجب)
        if (!elements.Price.value || isNaN(elements.Price.value) || parseFloat(elements.Price.value) <= 0) {
            setError(elements.Price, "يجب إدخال سعر صحيح أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(elements.Price);
        }

        return valid;
    }

    // إضافة حدث الإرسال
    myform.addEventListener("submit", function (e) {
        e.preventDefault();
        if (validate()) {
            this.submit();
        }
    });

    // إضافة أحداث التحقق عند تغيير القيم
    Object.values(elements).forEach(element => {
        element.addEventListener("input", function () {
            if (this.value.trim() !== "") {
                setSuccess(this);
            }
        });
    });
});


//document.addEventListener("DOMContentLoaded", function () {
//    const myform = document.getElementById("myform");
//    const paperName = document.getElementById("paperName");
//    const Type = document.getElementById("Type");
//    const Weight = document.getElementById("Weight");
//    const Quantity = document.getElementById("Quantity");
//    const Price = document.getElementById("Price");
//    const Colored = document.getElementById("Colored");
//    const ReorderPoint = document.getElementById("ReorderPoint");
//    const TotalBalance = document.getElementById("TotalBalance");



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

//    function validate() {
//        let valid = true;

//        if (paperName.value.trim() === "") {
//            setError(paperName, "برجاء إدخال اسم الورق");
//            valid = false;
//        } else {
//            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
//            if (!arabicRegex.test(paperName.value.trim())) {
//                setError(paperName, "يجب أن يحتوي الاسم على حروف عربية فقط");
//                valid = false;
//            } else {
//                setSuccess(paperName);
//            }
//        }

//        if (Type.value.trim() === "") {
//            setError(Type, "برجاء إدخال نوع الورق");
//            valid = false;
//        } else {
//            setSuccess(Type);
//        }

//        if (Weight.value.trim() === "" || isNaN(Weight.value) || parseFloat(Weight.value) <= 0) {
//            setError(Weight, "برجاء إدخال وزن الورق ");
//            valid = false;
//        } else {
//            setSuccess(Weight);
//        }

//        //if (Quantity.value.trim() === "" || isNaN(Quantity.value) || parseInt(Quantity.value) <= 0) {
//        //    setError(Quantity, "برجاء إدخال كمية الورق  ");
//        //    valid = false;
//        //} else {
//        //    setSuccess(Quantity);
//        //}

//        //if (Price.value.trim() === "" || isNaN(Price.value) || parseFloat(Price.value) <= 0) {
//        //    setError(Price, "برجاء إدخال سعر الورق ");
//        //    valid = false;
//        //} else {
//        //    setSuccess(Price);
//        //}

//        if (Colored.value.trim() === "") {
//            setError(Colored, "برجاء إدخال لون الورق");
//            valid = false;
//        } else {
//            setSuccess(Colored);
//        }

//        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
//            setError(ReorderPoint, "برجاء إدخال قيمة أكبر من الصفر");
//            valid = false;
//        } else {
//            setSuccess(ReorderPoint);
//        }

//        //if (TotalBalance.value.trim() === "" || isNaN(TotalBalance.value) || parseFloat(TotalBalance.value) <= 0) {
//        //    setError(TotalBalance, "برجاء إدخال قيمة أكبر من الصفر");
//        //    valid = false;
//        //} else {
//        //    setSuccess(TotalBalance);
//        //}

//        return valid;
//    }

//    myform.addEventListener("submit", function (e) {
//        e.preventDefault(); // Always prevent default first

//        if (!validate()) {
//            // Validation failed
//            return false;
//        } else {
//            // Validation successful - submit the form programmatically
//            this.submit();
//        }
//    });
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