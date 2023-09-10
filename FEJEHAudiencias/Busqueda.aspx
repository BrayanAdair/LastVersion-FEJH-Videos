<%@ Page Title="" Language="C#" MasterPageFile="~/Busqueda.Master" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="FEJEHAudiencias.Busqueda1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
        <ContentTemplate>
            <div class="container">
    <div class="row">
        <div class="col-sm-12 col-md-4 col-lg-6">
            <svg width="400px" height="346.41px" viewBox="0 0 400 346.41">
                <path d="M20 0 L380 173.205 L20 346.41 Z" style="fill: #737d6f; stroke: #737d6f; stroke-linejoin: round; stroke-width: 7px;" />
                <image xlink:href="./assets/ConsejoLogo.png" x="4" y="50" width="260" height="240" />
            </svg>
        </div>
        <div class="col-sm-12 col-md-8 col-lg-6">
            <div class="text-center">
            
                <!-- aqui tabla datta -->

            <div class="row">
                <div class="input-group mb-3">
                    <input type="text" id="curpInput" class="form-control" placeholder="Ingresa tu CURP para filtrar videos" aria-label="Recipient's username" aria-describedby="basic-addon2" runat="server" />
                    <div class="input-group-append">
                        <button id="btnFiltrar" class="btn btn-outline-secondary" runat="server" onserverclick="btnFiltrar_Click">Filtrar</button>
                    </div>
                </div>
            </div>

            <div class="row">
                <div id="divTablaVideos" runat="server"></div>
            </div>



    



<%--
<input type="button" value="Verificar Autenticación" onclick="verificarAutenticacion()">--%>

<div class="row">
    <div class="tn-container BotonBusqueda">
        <a class="cssbuttons-io-button" id="busquedaBtn" href="#" onclick="irvideo(); return false;">
            Buscar Video
            <div class="icon">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24">
                    <path fill="none" d="M0 0h24v24H0z"></path>
                    <path fill="currentColor" d="M16.172 11l-5.364-5.364 1.414-1.414L20 12l-7.778 7.778-1.414-1.414L16.172 13H4v-2z"></path>
                </svg>
            </div>
        </a>
    </div>
</div>


                <!-- fin boton buscar ir videos -->

            </div>
        </div>
    </div>
    
</div>

        </ContentTemplate>
   
</asp:Content>
