<%@ Page Title="" Language="C#" MasterPageFile="~/Busqueda.Master" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="FEJEHAudiencias.Videos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ContentTemplate>
<div class="container">
    <div class="row justify-content-center align-content-start">
        <div class="col-md-7">
        <div class="box__video w-100">
            <video class="video" id="video" controls preload="none" controlsList="nodownload">
                <source id="mp4" src="<%= VideoUrl %>" type="video/mp4">
                <p>El navegador no soporta el tipo de video.</p>
            </video>
        </div>
            <div class="box__description">
                <h2>Descripcion del Video</h2>
               <p><%= DescripcionVideo %></p>
            </div>
        </div>
        <div class="col-md-5">
            <div class="box__info">
                <h2>Datos del Video</h2>
                <div class="content__info">
                    <h3 class="">Titulo del Video</h3>
                    <p><%= TipoVideo %></p>
                </div>
                <div class="content__info">
                    <h3>Fecha de Audiencia</h3>
                     <p><%= FechaAudiencia %></p>
                </div>
                <div class="content__info">
                    <h3>Juez</h3>
                    <p><%= Juez %></p>
                </div>
                <div class="content__info">
                    <h3>Horario de Inicio</h3>
                    <p><%= HoraInicio %></p>
                </div>
                <div class="content__info">
                    <h3>Horario de Fin</h3>
                    <p><%= HoraFin %></p>
                </div>
            </div>
        </div>
    </div>
</div>
    </ContentTemplate>
</asp:Content>
