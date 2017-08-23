<header>@Usuario.GetUsuarioNombres(Session["usuario"].ToString())</header>
                                <small> Usuario <i style="color:red;">@if (Session["usuario"] != null)
                                {@Session["usuario"].ToString()  }</i></small>
                               <!-- <header>Carlos Lozano</header>
                                <small> Usuario <i style="color:red;">E-0001</i></small>-->