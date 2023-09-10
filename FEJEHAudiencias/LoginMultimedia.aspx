
<%@ Page Title="" Language="C#" CodeFile="LoginMultimedia.aspx.cs" AutoEventWireup="true" CodeBehind="LoginMultimedia.aspx.cs" Inherits="FEJEHAudiencias.LoginMultimedia" %>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">


<%--css personal--%>
<link href="css/login.css" rel="stylesheet" />
    <link href="css/loginForm.css" rel="stylesheet" />
    <link href="css/LoginMultimedia.css" rel="stylesheet" />
<body class="degradado">
<!-- inicio header -->
    <nav class="navbar navbar-expand-lg degradado">
        <div class="container-fluid">
            <div class="row justify-content-start align-items-center">
                <div class="col-auto">
                    <a class="navbar-brand" href="#">
                        <img src="assets/logoaudiencias.png" class="LogoAudiencia img-fluid" />
                    </a>
                </div>
            </div>

        </div>
    </nav>
    <!-- final Header-->
    <section class="vh-100 "  >
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center align-items-center h-100" >
                <div class="col col-xl-10" >
                    <div class="card" style="border-radius: 1rem;" >
                        <div class="row g-0 d-flex justify-content-center align-items-center">
                            <div class="col-md-6 col-lg-5 d-block  text-center"   >
                                <img src="https://upload.wikimedia.org/wikipedia/commons/0/0b/PoderJudicialHgo.png"
                                    alt="login form" class="img-fluid" width="60%" />
                            </div>
                            <div class="col-md-6 col-lg-7 d-flex align-items-center"  style="background-color: #f1f1ef25;">
                                <div class="card-body p-4 p-lg-5 text-black">

                                    <form runat="server">

                                        <div class="d-flex align-items-center mb-3 pb-1">
                                            <i class="fas fa-cubes fa-2x me-3" style="color: #ff6219;"></i>
                                            <span class="h1 fw-bold mb-0">Tecnico Multimedia</span>
                                        </div>

                                        <h5 class="fw-normal mb-3 pb-3" style="letter-spacing: 1px;">Por favor, ingresa tu datos.</h5>

                                        <div class="form-outline mb-4">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg" style="border:1px solid #d9d9d9 !important;"></asp:TextBox>
                                            <label class="form-label" for="txtUsername">Ingresa tu usuario</label>
                                        </div>

                                        <div class="form-outline mb-4">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg" style="border:1px solid #d9d9d9 !important;"></asp:TextBox>
                                            <label class="form-label" for="txtPassword">Ingresa tu clave</label>
                                        </div>

                                        <div class="pt-1 mb-4" >
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-dark btn-lg btn-block" OnClick="btnLogin_Click"  />
                                            <asp:Label ID="lblMessage" runat="server" style="letter-spacing: 1px;  color:#000000; display:block"></asp:Label>
                                        </div>
                                        
                                        
                                    </form>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
<br />
<!-- inicio footer-->
  <footer class="footer__box" style="padding:1.5rem; box-shadow: -3px -4px 15px -1px rgba(0,0,0,0.34); background-image: linear-gradient(to left bottom, #1d1c1c, #282727, #343333, #403f3f, #4c4b4b);">
        <div class=" d-flex flex-wrap justify-content-around  align-items-center">
            <div class=" col-lg-auto col-12 text-center ">
                <i class="bi bi-geo-alt-fill socialMedia-icon  text-white" style="font-weight:bold;"></i>
                <h5  style="font-weight:bold;" class="font-weight-bold text-white ">Ubicacion</h5>
                <a href="https://goo.gl/maps/vZxaA7wkFTyqFNcU9" target="_blank" class="text-decoration-none ">
                    <p class="text-white">Kilómetro 84.5, Carretera México Pachuca </p>
                </a>
            </div>
            <div class=" col-lg-auto col-10 text-center">
                <i class="bi bi-telephone-fill socialMedia-icon text-white " style="font-weight:bold;"></i>
                <h5 style="font-weight:bold;" class="font-weight-bold text-white">Teléfono</h5>
                <p class="font-weight-bold text-white">(771) 71-7-90-00</p>
            </div>
            <div class="socialMedia-media col-lg-auto col-12 text-center">
                <a href="https://www.facebook.com/PoderJudicialHidalgo" target="_blank"><i class="bi bi-facebook text-white "></i></a>
                <a href="https://twitter.com/PJEHidalgo" target="_blank"><i class="bi bi-twitter text-white "></i></a>
                <a href="https://www.youtube.com/channel/UC8OUr4Kax8UKXcvVJ_YeoQw" target="_blank"><i class="bi bi-youtube text-white "></i></a>

            </div>
        </div>
    </footer>
</body>




<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>