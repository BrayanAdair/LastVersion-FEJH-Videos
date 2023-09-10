<%@ Page Title="" Language="C#" MasterPageFile="~/MLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FEJEHAudiencias.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
 <%-- boostrap externo by me --%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
 <%-- scripts para firma electronica --%>
    <script src="Scripts/Firma/Firma.js" type="text/javascript"></script>
    <script src="Scripts/Firma/FirmaLogin.js?v=2.2" type="text/javascript"></script>
    <script src="Scripts/jsViews/validacion.js"></script>
    <script>


        var firma = new fielnet.Firma({
            subDirectory: "Scripts/Firma",
            controller: "Scripts/Firma/Controlador.ashx",
            ajaxAsync: false
        });
        $(function () {

            firma.readCertificate("FCertificado");
            firma.readPrivateKey("FLlavePrivada");

        });

    </script>
</asp:Content>
    <%-- content  inicio login --%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress runat="server" ID="idUpdateProgress"
        AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
        <ProgressTemplate>
            <div style="position: fixed; background-color: black; opacity: .5; width: 100%; height: 100%; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index:2000;">
                <%--<img style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); width:100px; height:100px;" id="idAjaxLoader" alt="Enviando ..." src="Imagenes/Generales/loading.gif" />--%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- inicio del login --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <%-- aqui solo es el titulo se puede mover si queremos --%>
            <div class="row mt-1">
                <div class="col align-self-center">
                    <div id="LemaPJEH" class="section-title text-center">
                        <h3 class="tituloLogin">UN PODER JUDICIAL HUMANO, ACCESIBLE, TRANSPARENTE Y EFICIENTE</h3>
                       

                    </div>
                </div>
            </div>
            <%-- todo lo que engloba al section se puede mover con CUIDADO--%>
            <section class="section-bg container">

            <div class="container  ">
                    <div class="row justify-content-center align-items-center">
                        <div class="col-md-6 col-lg-6 col-xl-4 d-flex justify-content-center">
                           <div class="cajaTecnico">
                            <img src="assets/logoaudienciasinferior.png" class="img-fluid" alt="Logo del poder judicial" />
                               <button class="TecnicoLogin" id="irLoginMultimedia"> ¿Eres Tecnico Multimedia?</button>
                            </div>
                        </div>
                        <div class="col-md-8 col-lg-6 col-xl-6 offset-xl-1 ">
                            <form action="Busqueda">
                            <div class="row section__container-responsive">
                                <!-- Certificate FILE -->
                                <div class="col-sm-6 d-flex justify-content-center">
                                    <div class="dropBG dragBG text-center" id="drag-file">
                                        <div data-droppable-image="1" class="tileGrp dragBorder" data-fileType="Signature Card">
                                            <div class="loaderTopWrap">
                                                <div class="loader-dots">
                                                    <span></span><span></span>
                                                    <span></span><span></span>
                                                    <span></span><span></span><span></span>
                                                </div>
                                            </div>
                                            <div><i class="icon ion-checkmark-circled"></i></div>
                                            <div class="tileGrp-content">
                                                <form action="FCertificado" class="image-select" data-droppable="2">
                                                    <h4 class="uploadTitle">Sube tu<br />certificado</h4>
                                                    <p class="oneLine">Arrastra aqui tu<br />archivo .cer</p>
                                                    <div class="dragText">----- o -----</div>
                                                    <!--upload button -->
                                                    <span id="name-file" class="text-center"></span>
                                                    <div class="button secondary">
                                                        <input required type="file" id="FCertificado" class="form-control-file inputfile" style="visibility:hidden;" data-droppable-input="1" />
                                                        <label for="FCertificado" class="upload">Click para subirlo</label>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- llave clave privada FILE-->
                                <div class="col-sm-6 d-flex justify-content-center">
                                    <div class="dropBG text-center" id="drag-file-2">
                                        <div data-droppable-image="1" class="tileGrp dragBorder" data-fileType="Signature Card">
                                            <div class="loaderTopWrap">
                                                <div class="loader-dots">
                                                    <span></span><span></span>
                                                    <span></span><span></span>
                                                    <span></span><span></span><span></span>
                                                </div>
                                            </div>
                                            <div class="upldCnfrmd"><i class="icon ion-checkmark-circled"></i></div>
                                            <div class="tileGrp-content">
                                                <form action="" class="image-select" data-droppable="1">
                                                    <h4 class="uploadTitle">Sube tu<br />clave privada</h4>
                                                    <p class="oneLine">Arrastra aqui tu<br />archivo .key</p>
                                                    <div class="dragText">----- o -----</div>
                                                    <!--upload button -->
                                                    <span id="name-file-2" class="text-span"></span>
                                                    <div class="button secondary">
                                                        <input required type="file" id="FLlavePrivada" class="form-control-file inputfile" data-droppable-input="1" />
                                                        <label for="FLlavePrivada" class="upload">Click para subirlo</label>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Password input -->
                                <div class="row">
                                    <div class="col-md-12 d-flex justify-content-center">
                                        <div class="col-md-12 form-group d-flex">
                                            <div class="form-control d-flex justify-content-center">
                                                <i class="bi bi-shield-lock" style="color:white;"></i>
                                               <asp:TextBox ID="TxtClaveAcceso" runat="server" CssClass="form-control password-input" TextMode="Password" placeholder="Ingresa tu clave de acceso" onkeydown="disableEnterKey(event)"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- button de ingresar -->
                                <div class="row justify-content-center">
                                    <div class="col-md-8 d-flex justify-content-center">
                                        <div class="tn-container">
                                            <!-- button de asp -->
                                            <!-- button asp -->
                                            <asp:Literal ID="BtnEntrar" runat="server" Text='
                                            <button class="cssbuttons-io-button Boton btn-ingresar" onclick="firmaLogeo(); return false;" style="height: 50px; width: 100%; font-family: Arial Black; font-size: 16px;">
                                                Ingresar Sesión
                                                <div class="icon">
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24">
                                                        <path fill="none" d="M0 0h24v24H0z"></path>
                                                        <path fill="currentColor" d="M16.172 11l-5.364-5.364 1.414-1.414L20 12l-7.778 7.778-1.414-1.414L16.172 13H4v-2z"></path>
                                                    </svg>
                                                </div>
                                            </button>' />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>

                        </div>
                    </div>
                </div>
                
    <%-- todo lo que engloba al section se puede mover con CUIDADO--%>
                <%-- modal --%>
      <!-- Button trigger modal -->



            </section>
        <%-- de aqui si podemos mover hasta abajo --%>
        
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- hasta aqui podemos modificar del login --%>
</asp:Content>
