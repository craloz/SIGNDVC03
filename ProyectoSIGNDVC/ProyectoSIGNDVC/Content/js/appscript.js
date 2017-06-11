function addTr() {
    var table = document.getElementById("tablaCargas");
    var row = table.insertRow(table.rows.lenght);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    cell1.setAttribute("contenteditable", "true");
    cell2.setAttribute("contenteditable", "true");
    cell3.setAttribute("contenteditable", "true");
    cell4.setAttribute("contenteditable", "true");
    cell5.setAttribute("contenteditable", "true");
    cell1.innerHTML = "Nombre"
    cell2.innerHTML = "Apellido"
    cell3.innerHTML = "XXXXXXX"
    cell4.innerHTML = "<select><option>M</option><option>F</option></select>"
    cell5.innerHTML = "<input type='date'>"
}