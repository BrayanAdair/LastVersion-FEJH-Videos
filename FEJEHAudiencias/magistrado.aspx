<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador.Master" AutoEventWireup="true" CodeBehind="magistrado.aspx.cs" Inherits="FEJEHAudiencias.magistrado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <!--Main Navigation-->
<header class="colorSidebar">
  <!-- Sidebar -->
<nav id="sidebarMenu" class="collapse d-lg-block sidebar collapse colorSidebar">
  <div class="position-sticky colorSidebar" style="background-color:#78A898 !important">
    <div class="list-group list-group-flush mx-3 mt-4" style="background-color:#78A898 !important;">
      <a href="#" class="list-group-item list-group-item-action py-2 ripple colorSidebarActive" onclick="mostrarContenido('contenido-magistrados', this)">
        <i class=""></i><span>Magistrados</span>
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
      <ul class="navbar-nav ms-auto d-flex flex-row">
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
                           <a class="dropdown-item perfilOpciones" href="javascript:void(0)" onclick="cerrarSesion();">cerrar sesión</a>
                    </ul>
                </div>
            </div>
      </ul>
    </div>
    <!-- Container wrapper -->
  </nav>
  <!-- Navbar -->
</header>
<!--INICIO Relacionar magistrados a videos-->
    


<script type="text/javascript">
    function checkBoxClicked(checkbox) {
        alert("checkBoxClicked function called!"); // Mostrar alerta cuando la función se llama
        var checkboxes = document.getElementsByName(checkbox.name);
        // Activa el checkbox que se activó.
        checkbox.checked = true;
        // Desactiva cualquier otro checkbox que ya esté activo.
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i] != checkbox && checkboxes[i].checked) {
                checkboxes[i].checked = false;
            }
        }
    }
 
</script>


<main style="margin-top: 8%;">

        <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="input-group grupoBusqueda divCentrado">
                <input id="Text1" type="text" class="form-control-sm inputBuscar" placeholder="Ingresa la CAUSA a buscar:" runat="server" />
                <div class="input-group-append">
                    <asp:Button ID="buscarCausa" Text="Busca la causa" CssClass="vincularBtn2" runat="server" OnClick="buscarCausa_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="table-responsive">
        <div class="table-wrapper">
            <div class="table-title">
                <div class="row">
                    <div class="col-sm-7">
                        <h2 class="textEditar">Selecciona una audiencia a <b class="enfasisSeccionTexto">vincular con el usuario</b></h2>
                    </div>
                </div>
            </div>
        <asp:GridView ID="gridViewAudiencias" runat="server" CssClass="table tablaDatosGeneral" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowDataBound="gridViewAudiencias_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBox" runat="server" CssClass="only-one" OnCheckedChanged="checkBox_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Tipo" HeaderText="Tipo de audiencia" />
                <asp:BoundField DataField="SalaDescrip" HeaderText="Sala" />
                <asp:BoundField DataField="FechaAudi" HeaderText="Fecha de audiencia" DataFormatString="{0:dd/MM/yy HH:mm}" />
                <asp:BoundField DataField="FechaCap" HeaderText="Fecha de captura" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="Titulo" HeaderText="Título de video" />
                <asp:BoundField DataField="VideoAsignado" HeaderText="Video Asignado" />
            </Columns>
        </asp:GridView>


            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="input-group grupoBusqueda divCentrado">
                        <input id="inputCURP" type="text" class="form-control-sm inputBuscar" placeholder="Ingresa la CURP a cincular:" runat="server" />
                        <div class="input-group-append">
                            <asp:Button ID="btnCURP" Text="Agrega al usuario" CssClass="vincularBtn2" runat="server" OnClick="btnCURP_Click" />
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    </div>


</main>
<!--FIN Relacionar magistrados a videos-->
    <script src="Scripts/jsViews/auth.js"></script>
    <script src="Scripts/jsViews/magistrado.js"></script>
    <script src="Scripts/Firma/FirmaLogin.js"></script>
    <script src="Scripts/jsViews/buscar.js"></script>

</asp:Content>