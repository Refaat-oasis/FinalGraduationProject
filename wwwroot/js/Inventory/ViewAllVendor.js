function searchVendors() {
    var input, filter, table, tr, td, i, j, txtValue;
    input = document.getElementById("searchInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("vendorTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows (skip the header row)
    for (i = 1; i < tr.length; i++) {
        tr[i].style.display = "none"; // Hide the row by default
        td = tr[i].getElementsByTagName("td");

        // Loop through all columns in the current row
        for (j = 0; j < td.length; j++) {
            if (td[j]) {
                txtValue = td[j].textContent || td[j].innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = ""; // Show the row if a match is found
                    break; // Exit the inner loop if a match is found
                }
            }
        }
    }
}