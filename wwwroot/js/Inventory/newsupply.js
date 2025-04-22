document.addEventListener("DOMContentLoaded", function () {
    const myform = document.getElementById("myform");
    const supplyName = document.getElementById("supplyName");
    const Quantity = document.getElementById("Quantity");
    const Price = document.getElementById("Price");
    const ReorderPoint = document.getElementById("ReorderPoint");
    const TotalBalance = document.getElementById("TotalBalance");


    function validate() {
        let valid = true;

        if (supplyName.value.trim() === "") {
            setError(supplyName, "برجاء إدخال اسم المستلزم");
            valid = false;
        } else {
            let arabicRegex = /^[\u0600-\u06FF\s]+$/;
            if (!arabicRegex.test(supplyName.value.trim())) {
                setError(supplyName, "يجب أن يحتوي الاسم على حروف عربية فقط");
                valid = false;
            } else {
                setSuccess(supplyName);
            }
        }

        //if (Quantity.value.trim() === "" || isNaN(parseFloat(Quantity.value)) || parseFloat(Quantity.value) <= 0) {
        //    setError(Quantity, "برجاء إدخال كمية المستلزم بشكل صحيح");
        //    valid = false;
        //} else {
        //    setSuccess(Quantity);
        //}

        //if (Price.value.trim() === "" || isNaN(parseFloat(Price.value)) || parseFloat(Price.value) <= 0) {
        //    setError(Price, "برجاء إدخال سعر المستلزم بشكل صحيح");
        //    valid = false;
        //} else {
        //    setSuccess(Price);
        //}

        if (ReorderPoint.value.trim() === "" || isNaN(ReorderPoint.value) || parseFloat(ReorderPoint.value) <= 0) {
            setError(ReorderPoint, "برجاء إدخال قيمة أكبر من الصفر");
            valid = false;
        } else {
            setSuccess(ReorderPoint);
        }

        //if (TotalBalance.value.trim() === "" || isNaN(TotalBalance.value) || parseFloat(TotalBalance.value) <= 0) {
        //    setError(TotalBalance, "برجاء إدخال قيمة أكبر من الصفر");
        //    valid = false;
        //} else {
        //    setSuccess(TotalBalance);
        //}

        return valid;
    }

    function setError(input, errorMsg) {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = errorMsg;
        inputBox.classList.add("error");
    }

    function setSuccess(input) {
        const inputBox = input.parentElement;
        const errorParagraph = inputBox.querySelector(".error");
        errorParagraph.innerText = "";
        inputBox.classList.remove("error");
    }

    myform.addEventListener("submit", function (e) {
        e.preventDefault(); // Always prevent default first

        if (!validate()) {
            // Validation failed
            return false;
        } else {
            // Validation successful - submit the form programmatically
            this.submit();
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

//});

