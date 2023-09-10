<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="FEJEHAudiencias.Administrador1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--Main Navigation-->
<header class="colorSidebar">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        function consultarAudiencias() {
            // Obtener el valor del campo de texto
            var causa = document.getElementById("CausaTextBox").value;

            // Realizar cualquier acción adicional aquí, como llamar a una función de servidor para la consulta
            // Por ejemplo, podrías usar AJAX para enviar la causa al servidor y obtener los resultados.

            // Aquí te doy un ejemplo simple de cómo mostrar la causa en una alerta:
            alert("Causa ingresada: " + causa);
        }
    </script>
  <!-- Sidebar -->
<nav id="sidebarMenu" class="collapse d-lg-block sidebar collapse colorSidebar">
  <div class="position-sticky colorSidebar" style="background-color:#78A898 !important">
    <div class="list-group list-group-flush mx-3 mt-4" style="background-color:#78A898 !important;">
      <a href="#" class="list-group-item list-group-item-action py-2 ripple colorSidebar" onclick="mostrarContenido('contenido-datos', this)">
        <i class=""></i><span>Datos</span>
      </a>
      <a href="#" class="list-group-item list-group-item-action py-2 ripple colorSidebar" onclick="mostrarContenido('contenido-editar', this)">
        <i class=""></i><span>Editar</span>
      </a>
      <a href="#" class="list-group-item list-group-item-action py-2 ripple colorSidebar" onclick="mostrarContenido('contenido-subir-videos', this)">
        <i class=""></i><span>Subir Videos</span>
      </a>
       <a href="#" class="list-group-item list-group-item-action py-2 ripple colorSidebar" onclick="mostrarContenido('contenido-magistrado', this)">
        <i class=""></i><span>Asignar Magistrado</span>
      </a>
      <a href="Busqueda.aspx" class="list-group-item list-group-item-action py-2 ripple colorSidebar">
        <i class=""></i><span>Buscar</span>
      </a>
    </div>
  </div>
</nav>
  <!-- Navbar -->
  <nav id="main-navbar" class="navbar navbar-expand-lg navbar-light bg-white fixed-top" style="background-color: #78A898!important;">
    <!-- Container wrapper -->
    <div class="container-fluid colorSidebar">
      <!-- Toggle button -->
    <button
        style="color:white"
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#sidebarMenu"
      aria-controls="sidebarMenu"
      aria-expanded="false"
      aria-label="Toggle navigation">
      <span class="navbar-toggler-icon" style="color:white"></span> <!-- Agrega el icono del botón de alternancia -->
    </button>
      <!-- Brand -->
      <a class="navbar-brand" href="#">
        <img
          src="assets/logoaudiencias.png"
          height="55"
          alt="Logo poder Judicial"
          loading="lazy"
        />
      </a>
      <!-- Right links -->
      
        <!-- Avatar -->
             <div class="d-flex align-items-center">
                <div class="dropdown">
                    <a class="dropdown-toggle d-flex align-items-center hidden-arrow" href="#" id="navbarDropdownMenuAvatar"
                       role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <img src="assets/perfil.png" class="rounded-circle " height="45"
                             alt="imagen de perfil" loading="lazy" />
                    </a>
                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdownMenuAvatar">
                        <li>
                            <a class="dropdown-item perfilOpciones" href="#" data-bs-toggle="modal" data-bs-target="#modalUsuario">perfil</a>
                        </li>
                        <li>
                            <asp:LinkButton ID="btnLogout" runat="server" Text="Cerrar Sesión" OnClick="btnCerrarSesion" CssClass="btn bg-transparent border border-0" />
                        </li>
                    </ul>
                </div>
            </div>
    </div>
    <!-- Container wrapper -->
  </nav>
  <!-- Navbar -->
</header>
<!--Main Navigation-->
<!--Main layout-->
<main style="margin-top: 58px;">
  <div class="container pt-4 contenedor ">
      <!-- Inicio DATOS -->
    <div class="contenido-pantalla" id="contenido-datos">
     <div class="container">
<div class="contenedorBuscar">
    <div class="row">
        <div class="col d-flex justify-content-center align-items-end">
            <div class="input-group grupoBusqueda">
                <input id="inputNumeroCausa" type="text" class="form-control-sm form-control inputModales inputBuscar" placeholder="Numero de Causa:" runat="server" />
                <div class="input-group-append">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Resto del código HTML -->

        <div class="row">

            <div class="divTitulo">
                <h3>Datos del inputado</h3>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-4 d-flex justify-content-center ">
                <div id="divDelitos" runat="server" ></div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-4">
               <div class="datosVictImpu col" id="divVictimas" runat="server"></div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-4">
             <div class="datosVictImpu col" id="divImputados" runat="server"></div>
            </div>
           


        </div>
         <!-- INICIO Tabla de datos de audiencia -->
 <div class="container">
    <div class="row">
        <div class="divTitulo">
            <h3>Listado de audiencias registradas</h3>
        </div>
       <asp:GridView ID="gridViewAudiencias" runat="server" CssClass="table tablaDatosGeneral" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField DataField="Tipo" HeaderText="Tipo de audiencia" />
                <asp:BoundField DataField="SalaDescrip" HeaderText="Sala" />
                <asp:BoundField DataField="FechaAudi" HeaderText="Fecha de audiencia" DataFormatString="{0:dd/MM/yy HH:mm}" />
                <asp:BoundField DataField="FechaCap" HeaderText="Fecha de captura" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            </Columns>
        </asp:GridView>

    </div>
     <asp:GridView ID="gridViewInfoVideos" runat="server" CssClass="table tablaDatosGeneral" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowDataBound="gridViewInfoVideos_RowDataBound">
    <Columns>
        <asp:BoundField DataField="IdSolicitudVideo" HeaderText="ID Solicitud" />
        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha de Solicitud" DataFormatString="{0:dd/MM/yy HH:mm}" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" />
        <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobación" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
        <asp:HyperLinkField DataTextField="VideoURL" HeaderText="URL de Video" />
    </Columns>
</asp:GridView>

</div>


        <!-- FIN tabla de datos audiencias  -->
     </div>
    </div>
      <!-- Inicio EDITAR -->
    <div class="contenido-pantalla" id="contenido-editar" style="display: none;">
      <div class="container contenedorEditar">
          
               

          <!-- EDITAR tablas -->
                     <div class="row">
              <h3 class="editarTitulo">Editar</h3>
              <div class="table-responsive">
                    <div class="table-wrapper">
                      <div class="table-title">
                        <div class="row">
                          <div class="col-sm-6">
                            <h2 class="textEditar">Modifica los datos de un <b class="enfasisSeccionTexto">Video</b></h2>
                          </div>
                          
                        </div>
                      </div>
                      <table class="table table-striped table-hover">
                        <thead>
                          <tr>
                          
                           <th>Titulo</th>
                            <th>Descripcion</th>
                            <th>Fecha de grabacion</th>
                            <th>Numero de expediente</th>
                            <th>Usuario</th>
                            <th>Video asignado</th>
                              <th>Hora Inicio</th>
                              <th>Hora Fin</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <div class="tablaDatosGeneral" id="divInfoVideosEditar" runat="server" style="text-align:center;"></div>
                           
                          </tr>		
                        </tbody>
                      </table>
                    </div>
                  </div>        
      
       
            
          </div>
           <!-- inicio modal par EDITAR -->
                    <div class="modal fade modal-redondo" id="modalEditar" tabindex="-1" role="dialog" aria-labelledby="modalEditarlLabel" aria-hidden="true">
                    <div class="modal-dialog justify-content-center align-items-center" role="document">
                        <div class="modal-content modalTamanoAdmin">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalEditarLabel">Edita video seleccionado</h5>
                            </div>
                       <div class="modal-body">
                       
                            <div>
                                <form id="updateForm" class="form-group">
                                    <div class="form-group">
                                        <label for="IdUpdateVideo" class="col-form-label">Ingrese el Id de su video:</label>
                                        <input type="text" id="inputIdUpdateVideo" name="IdUpdateVideo" runat="server" class="form-control inputModales" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-form-label">Columna:</label>
                                        <asp:DropDownList ID="dvColumnaSelected" runat="server" name="Columna" class="form-control inputModales">
                                            <asp:ListItem Text="Selecciona" Value="" ></asp:ListItem>
                                            <asp:ListItem Text="Titulo" Value="Titulo" ></asp:ListItem>
                                            <asp:ListItem Text="Url" Value="Url"></asp:ListItem>
                                            <asp:ListItem Text="Descripcion" Value="Descripcion" ></asp:ListItem>
                                            <asp:ListItem Text="Video asignado" Value="VideoAsignado" ></asp:ListItem>
                                            <asp:ListItem Text="Hora Inicio - Incluye (: - )" Value="HoraInicio" ></asp:ListItem>
                                            <asp:ListItem Text="Hora Fin - Incluye (: - )" Value="HoraFin" ></asp:ListItem>
                                         </asp:DropDownList>
                                        <input type="text" id="dvColumna" runat="server" readonly style="display: none;" class="form-control inputModales" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-form-label">Nuevo Valor:</label>
                                        <input type="text" id="inputUpdateVideo" runat="server" class="form-control inputModales"/>
                                    </div>
                                </form>

                            </div>
                           
                       
                        </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                                <asp:Button ID='Button2' class='btn btn-success' runat='server' Text='Editar' OnClick='BtnUpdateInfoVideo_Click' />
                            </div>
                        </div>
                    </div>
                </div>
                    <!-- final modal para EDITAR -->
          <!-- ELIMINAR tablas -->
            <div class="row">
              <h3 class="editarTitulo">Eliminar</h3>
              <div class="table-responsive">
                    <div class="table-wrapper">
                      <div class="table-title">
                        <div class="row">
                          <div class="col-sm-6">
                            <h2 class="textEditar">Elimina por ID  <b class="enfasisSeccionTexto">Video</b></h2>
                          </div>
                            
                        </div>
                      </div>
                        <div class="form d-flex mb-2 justify-content-end ">
                            <input type="number" class="form-control" id="inputIdEliminarVideo" placeholder="Ingresa id para eliminar" runat="server" style="width:250px; border:1px solid #dfdbdb !important; margin:0 2px !important; "/>
                            <a href="#" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#modalBorrar" style="margin:0 !important;"><i class="material-icons">&#xE15C;</i> <span>Eliminar</span></a>						
                        </div>
                        <br />
                      <table class="table table-striped table-hover">
                        <thead>
                          <tr>
                              
                              
                            <th>IdVideos</th>
                            <th>Titulo</th>
                            <th>Descripcion</th>
                            <th>Fecha de grabacion</th>
                            <th>Numero de expediente</th>
                            <th>Usuario</th>
                            <th>Video asignado</th>
                              <th>Hora Inicio</th>
                              <th>Hora Fin</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            
                              <div class="tablaDatosGeneral" id="divInfoVideosEliminar" runat="server" style="text-align:center;"></div>
                                  

                            
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>        
     <!-- inicio modal par BORRAR -->
                    <div class="modal fade modal-redondo" id="modalBorrar" tabindex="-1" role="dialog" aria-labelledby="modalBorrarlLabel" aria-hidden="true">
                    <div class="modal-dialog justify-content-center align-items-center" role="document">
                        <div class="modal-content modalTamanoAdmin">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalBorrarLabel">Se borrara el video</h5>
                            </div>
                       <div class="modal-body">
                        
                         <div class="row justify-content-md-center">
                           <svg xmlns="http://www.w3.org/2000/svg" width="70" height="70" fill="#d53343" class="bi bi-trash" viewBox="0 0 16 16">
                              <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z"/>
                              <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z"/>
                            </svg>


                        </div>        
                        <div class="row modalDivCentrado">
                            <h3 class="">¡Estas a punto de borrar este video!</h3>
                            <b class="bTextoModal">¿Seguro que quiere eliminar este video?</b>
                        </div>
                        </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                                <asp:Button ID='Button1' class='btn btn-success' runat='server' Text='Eliminar' OnClick='BtnEliminarIdVideo_Click' />
                                 
                                
                            </div>
                            
                        </div>
                    </div>
                </div>
                    <!-- final modal para BORRAR -->
          </div>
         
        
      </div>
    </div>




      <!-- Inicio Subir Videos -->
    <div class="contenido-pantalla" id="contenido-subir-videos" style="display: none;">
          <div class="container contenedorEditar">
              <h3 class="editarTitulo">Subir URL del video</h3>
        <div class="row">
            <!-- INICIO STEPS BOOSTRAP SUBIR VIDEOS -->
            <!-- MultiStep Form -->
        <div class="container-fluid" id="grad1">
        <div class="row justify-content-center mt-0">
        <div class="col-11 col-sm-9 col-md-9 col-lg-9 text-center p-0 mt-3 mb-2">
            <div class="card px-0 pt-4 pb-0 mt-3 mb-3">
                <h2><strong>Sube el video </strong></h2>
                <p>Busca la audiencia y relacionala al video</p>
                <div class="row">
                    <div class="col-md-12 mx-0">
                        <div id="msform">
                            <!-- progressbar -->
                            <ul id="progressbar">
                                <li class="active" id="account"><strong>Buscar</strong></li>
                                <li id="personal"><strong>Relacionar</strong></li>
                                <li id="confirm"><strong>Fianlizado</strong></li>
                            </ul>
                            <!-- fieldsets -->
                            <fieldset>

                                <div class="form-card">
                                    <span>El numero de causa que ingresaste al inicio debe coincidir con el tipo de audiencia:</span>
                                    <div class="form-group">
                                        <label class="col-form-label">Selecciona el tipo de audiencia:</label>
                                        <asp:DropDownList ID="ddlTipoAudiencia" runat="server" class="form-control inputModales">
                                        </asp:DropDownList>
                                        <label class="col-form-label">Selecciona quien sube el video:</label>
                                        <asp:DropDownList ID="juzgadoAsignado" runat="server" class="form-control inputModales">
                                        </asp:DropDownList>
                                        <span></span>

                                    </div>
                                </div>
                                <input type="button" name="next" class="next action-button" value="Relacionar video" id="mensajeVerificacion" runat="server" />
                            </fieldset>

                            <fieldset>
                                <div class="form-card">
                                    <h4>Antes de avanzar verifica si la siguiente informacion corresponde con tu peticion</h4>
                                    <asp:Label ID="lblNotification" runat="server" Text="" />
                                    <div id="lblNotifications" runat="server"></div>
                                    <h2 class="fs-title">Llena la informacion del video</h2>
                                    
                                    
                                    <label>Ingresa el titulo del video</label>
                                    <input type="text" id="tvdTitulo" runat="server" />
                                    
                                    <label>Ingresa el Url del video</label>
                                    <input type="text" id="tvdUrl" runat="server" />
                                    
                                    <label>Ingresa el Descripcion del video</label>
                                    <input type="text" id="tvdDescripcion" runat="server" />
                                    
                                    <label>Ingresa el Fecha de Grabacion del video</label>
                                    <input id="tvdFechaGrabacion" type="datetime-local" class="form-control inputModales"  runat="server" />
                                    
                                    <label>Ingresa el Fecha Incio del video</label>

                                    <input id="tvdHoraInicio" type="datetime-local" class="form-control inputModales"  runat="server" />
                                    <label>Ingresa el Fecha Fin del video</label>
                                    <input id="tvdHoraFin" type="datetime-local" class="form-control inputModales"  runat="server" />

                                   
                                </div>
                                <input type="button" name="previous" class="previous action-button-previous" value="Regresar" />
                                 <asp:Button ID="btndIdRegAudiencia" runat="server" Text="Subir Video" OnClick="btnIdRegAudiencia" class="next action-button" />
                            </fieldset>



                            <fieldset>
                                <div class="form-card">
                                    <h2 class="fs-title text-center">¡ Se ha subido el video !</h2>
                                    <br>
                                    <br>
                                    <div class="row justify-content-center">
                                        <div class="col-3">
                                            <img src="https://img.icons8.com/color/96/000000/ok--v2.png" class="fit-image">
                                        </div>
                                    </div>
                                    <br>
                                    <br>
                                    <div class="row justify-content-center">
                                        <div class="col-7 text-center">
                                            <h5>Se ha subido  el video </h5>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <!-- Modal -->
                                   <!-- Nuevo Modal -->

 
                            <%--modal--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
            <!-- FIN STEPS BOOSTRAP SUBIR VIDEOS -->
        </div>
          </div>
    </div>
      <!-- Inicio Magistrados -->
   <div class="contenido-pantalla" id="contenido-magistrado" style="display: none;">
     <div class="container contenedorEditar">
              <h3 class="editarTitulo">Asigna a un magistrado</h3>
        <div class="row justify-content-center">
            <div class="row">
                <div class="col">
                    <div class="alert divAlertaMagistrado alert-success alert-dismissible fade show d-flex flex-row justify-content-center align-items-center " role="alert">
                        <strong>NOTA: </strong> Recuerda ingresar el email asociado a la firma electrónica digital del magistrado para que pueda ver los videos relacionados a él.
                        <button type="button" class="close border-0 bg-transparent text-success d-flex " data-dismiss="alert" aria-label="Close">
                            <span class="spanXMagistrado" aria-hidden="true" style="font-size:30px; align-items:start;">&times;</span>
                        </button>
                    </div>

                </div>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="input-group mb-3">
                      <asp:TextBox ID="emailMag" CssClass="form-control inutMagistrado" placeholder="EMAIL del magistrado" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;
                  <div class="input-group-append">
                    <asp:Button ID="btnAsignarMag" CssClass="btn btn-success" runat="server" Text="Asignar" OnClick="ActualizarMagistrado" />
                  </div>
                </div>
            </div>
        </div>
      </div>
    </div>
      <!-- Fin Magistrados -->
  </div>
   
    
    <script src="Scripts/jsViews/administrador.js"></script>
</main>
    <script>
        function handleSelectionChange() {
            var tipoAudiSelect = document.getElementById('selectTipoAudi');
            var salaSelect = document.getElementById('selectSalaCapsu');

            var selectedTipoAudi = tipoAudiSelect.value;
            var selectedSala = salaSelect.value;

            if (selectedTipoAudi && selectedSala) {
                if (selectedTipoAudi !== selectedSala) {
                    alert('Error: El tipo de audiencia y la sala no coinciden.');
                    tipoAudiSelect.value = '';
                    salaSelect.value = '';
                }
            }

        }

        var selectElement = document.getElementById("<%= dvColumnaSelected.ClientID %>");
        var inputElement = document.getElementById("<%= dvColumna.ClientID %>");

        selectElement.addEventListener("change", (e) => {
            var selectedValue = selectElement.value;
            inputElement.value = selectedValue;
        });






    </script>
    


<!--Main layout-->
</asp:Content>

