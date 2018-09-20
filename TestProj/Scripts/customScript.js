function getNotValidTests() {
    document.forms["form0"].submit();
}
function hideFunction() {
        $(".myhidden").css("display", "none");
}
function sortTable(n, tableId) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById(tableId);
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

function myFunction(inputId, tableId) {
    var input, filter, table, tr, td, i;
    input = document.getElementById(inputId);
    filter = input.value.toUpperCase();
    table = document.getElementById(tableId);
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}


//function getNotValidTests() {
//    document.forms["form0"].submit();
//}
//function hideFunction() {
//    $(".myhidden").css("display", "none");
//}
//function sortTable(n) {
//    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
//    table = document.getElementById("myTable2");
//    switching = true;
//    dir = "asc";
//    while (switching) {
//        switching = false;
//        rows = table.rows;
//        for (i = 1; i < (rows.length - 1); i++) {
//            shouldSwitch = false;
//            x = rows[i].getElementsByTagName("TD")[n];
//            y = rows[i + 1].getElementsByTagName("TD")[n];
//            if (dir == "asc") {
//                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
//                    shouldSwitch = true;
//                    break;
//                }
//            } else if (dir == "desc") {
//                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
//                    shouldSwitch = true;
//                    break;
//                }
//            }
//        }
//        if (shouldSwitch) {
//            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
//            switching = true;
//            switchcount++;
//        } else {
//            if (switchcount == 0 && dir == "asc") {
//                dir = "desc";
//                switching = true;
//            }
//        }
//    }
//}

//function myFunction(inputId, tableId) {
//    var input, filter, table, tr, td, i;
//    input = document.getElementById(inputId);
//    filter = input.value.toUpperCase();
//    table = document.getElementById(tableId);
//    tr = table.getElementsByTagName("tr");

//    for (i = 0; i < tr.length; i++) {
//        td = tr[i].getElementsByTagName("td")[0];
//        if (td) {
//            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
//                tr[i].style.display = "";
//            } else {
//                tr[i].style.display = "none";
//            }
//        }
//    }
//}