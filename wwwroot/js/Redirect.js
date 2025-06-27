
console.log("redirect is working");
        document.addEventListener("DOMContentLoaded", function () {
            const tempDataElement = document.getElementById('tempDataSuccess');
        const jobRoleElement = document.getElementById('hdnJobRole');

        const hasSuccessMessage = tempDataElement?.value === 'true';
        const jobRole = parseInt(jobRoleElement?.value || "0");

        console.log("Success message exists:", hasSuccessMessage);
        console.log("Job role:", jobRole);

        const jobRoleRoutes = {
            0: "/employee/AdminHome",
        1: "/employee/inventoryManager",
        2: "/employee/inventoryClerk",
        3: "/employee/TechnicalManager",
        4: "/employee/technicalClerk",
        5: "/employee/CostManager",
        6: "/employee/costClerk",
        7: "/employee/Accountingmanager",
        8: "/employee/AccountingClerk"
            };

        if (hasSuccessMessage) {
            setTimeout(function () {
                const redirectUrl = jobRoleRoutes[jobRole] || "/Employee/LoginPage";
                window.location.href = redirectUrl;
            }, 3000);
            }
        });
 
