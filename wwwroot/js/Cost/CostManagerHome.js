function toggleDropdown(element) {
    let submenu = element.querySelector("ul");
    let allSubmenus = document.querySelectorAll(".dropdown > li ul");

    // إغلاق جميع القوائم المفتوحة
    allSubmenus.forEach(menu => {
        if (menu !== submenu) {
            menu.style.display = "none";
        }
    });

    // تبديل عرض القائمة المحددة
    submenu.style.display = (submenu.style.display === "block") ? "none" : "block";
}