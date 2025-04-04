<script>
    $(document).ready(function () {
        function validateForm() {
            let isValid = true;

            // التحقق من إدخال الرقم القومي للموظف
            let employeeId = $('input[name="RequisiteOrderDTO.EmployeeId"]').val().trim();
            if (employeeId === "") {
                isValid = false;
                alert("برجاء إدخال الرقم القومي للموظف");
            }

            // التحقق من اختيار الطلبيّة
            let jobOrderId = $('select[name="RequisiteOrderDTO.JobOrderId"]').val();
            if (!jobOrderId) {
                isValid = false;
                alert("برجاء اختيار الطلبيّة");
            }

            // التحقق من إدخال الأصناف بشكل صحيح
            $('.item-row').each(function () {
                let itemType = $(this).find('.item-type').val();
                let itemId = $(this).find('.item-id').val();
                let quantity = $(this).find('input[type="number"]').val();

                if (!itemType) {
                    isValid = false;
                    alert("برجاء اختيار النوع ");
                    return false;
                }

                if (!itemId) {
                    isValid = false;
                    alert("برجاء اختيار الصنف ");
                    return false;
                }

                if (!quantity || quantity <= 0) {
                    isValid = false;
                    alert("يجب أن تكون الكمية أكبر من الصفر");
                    return false;
                }
            });

            return isValid;
        }

        // منع إرسال النموذج إذا لم يكن التحقق ناجحًا
        $('form').submit(function (event) {
            if (!validateForm()) {
        event.preventDefault(); // منع الإرسال
            }
        });
    });
</script>
