function addTr() {
    var table = document.getElementById("tablaCargas");
    filas = table.rows.length;
    var row = table.insertRow(filas);
    row.setAttribute("id", filas);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);
    //cell1.setAttribute("contenteditable", "true");
    //cell2.setAttribute("contenteditable", "true");
    //cell3.setAttribute("contenteditable", "true");
    //cell4.setAttribute("contenteditable", "true");
    //cell5.setAttribute("contenteditable", "true");
    //cell6.setAttribute("contenteditable","true");
    var cont = document.getElementById("tablaCargas").rows.length;
    cell1.innerHTML = "<input required class='form-control' type='text' placeholder='Nombre' name='nombrecarga" + cont + "' >";
    cell2.innerHTML = "<input required class='form-control' type='text' placeholder='Apellido' name='apellidocarga" + cont + "' >";
    cell3.innerHTML = "<input required class='form-control' type='text' placeholder='Cedula' name='cedulacarga" + cont + "' >";
    cell4.innerHTML = "<select required class='form-control' name='sexocarga" + cont +"' ><option value='M'>Masculino</option><option value='F'>Femenino</option></select>";
    cell5.innerHTML = "<input required class='form-control' type='date' name='fechanaccarga" + cont + "' >";
    cell6.innerHTML = "<input required class='form-control 'type='text' placeholder='Monto' name='montocarga" + cont + "' >";
    getRows();
}

function getRows()
{
    var element = document.getElementById("numfilas");
    element.setAttribute("value", document.getElementById("tablaCargas").rows.length);
}

function updateIndex(index) {
    var element = document.getElementById("index");
    element.setAttribute("value",index);
}

function eliminarUsuario(usuario) {
    if (confirm('Deseas Eliminar al Usuario: ' + usuario))
    {
        $.get("/Configuration/BorrarUsuario?"+usuario)
            .done(function (obj) {
                "borramos el usuario";
            });
    } else {
        alert("no eliminado")
    }
}