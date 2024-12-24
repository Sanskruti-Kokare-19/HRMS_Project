<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="HrmsTeam2.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>HRMS Management</title>
    <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="../assets/img/favicon.png" />
    <!--     Fonts and icons     -->
    <link href="https://fonts.googleapis.com/css?family=Inter:300,400,500,600,700,800" rel="stylesheet" />
    <!-- Nucleo Icons -->
    <link href="https://demos.creative-tim.com/soft-ui-dashboard/assets/css/nucleo-icons.css" rel="stylesheet" />
    <link href="https://demos.creative-tim.com/soft-ui-dashboard/assets/css/nucleo-svg.css" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <script src="https://kit.fontawesome.com/42d5adcbca.js" crossorigin="anonymous"></script>
    <!-- CSS Files -->
    <link id="pagestyle" href="../assets/css/soft-ui-dashboard.css?v=1.1.0" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container position-sticky z-index-sticky top-0">
  <div class="row">
                    <div class="col-12">
                        <!-- Navbar -->
                        <nav class="navbar navbar-expand-lg blur blur-rounded top-0 z-index-3 shadow position-absolute my-3 py-2 start-0 end-0 mx-4">
                            <div class="container-fluid pe-0">
                                <a class="navbar-brand font-weight-bolder ms-lg-0 ms-3 " href="../pages/dashboard.html">HRMS-T2
                                </a>
                                <button class="navbar-toggler shadow-none ms-2" type="button" data-bs-toggle="collapse" data-bs-target="#navigation" aria-controls="navigation" aria-expanded="false" aria-label="Toggle navigation">
                                    <span class="navbar-toggler-icon mt-2">
                                        <span class="navbar-toggler-bar bar1"></span>
                                        <span class="navbar-toggler-bar bar2"></span>
                                        <span class="navbar-toggler-bar bar3"></span>
                                    </span>
                                </button>
                                <div class="collapse navbar-collapse" id="navigation">
                                    <ul class="navbar-nav mx-auto ms-xl-auto me-xl-7">
                                        <li class="nav-item">
                                            <asp:HyperLink ID="HyperLink1" runat="server" class="nav-link d-flex align-items-center me-2 active">
                                                <i class="fa fa-chart-pie opacity-6  me-1"></i>
                                                Dashboard
                                            </asp:HyperLink>
                                        </li>
                                        <li class="nav-item">
                                            <asp:HyperLink ID="HyperLink2" runat="server" class="nav-link d-flex align-items-center me-2 active">
                                                <i class="fa fa-chart-pie opacity-6  me-1"></i>
                                                Profile
                                            </asp:HyperLink>
                                        </li>
                                        <li class="nav-item">
                                            <asp:HyperLink ID="HyperLink3" runat="server" class="nav-link d-flex align-items-center me-2 active" NavigateUrl="~/SignUp.aspx">
                                                <i class="fa fa-chart-pie opacity-6  me-1"></i>
                                                SignUp      
                                            </asp:HyperLink>
                                        </li>
                                        <li class="nav-item">
                                            <asp:HyperLink ID="HyperLink4" runat="server" class="nav-link d-flex align-items-center me-2 active" NavigateUrl="~/SignIn.aspx">
                                                <i class="fa fa-chart-pie opacity-6  me-1"></i>
                                                SignIn
                                            </asp:HyperLink>
                                        </li>
                                    </ul>
                                   
                                </div>
                            </div>
                        </nav>
                        <!-- End Navbar -->
                    </div>
                </div>
            </div>
            <main class="main-content  mt-0">
                <section>
                    <div class="page-header min-vh-75">
                        <div class="container">
                            <div class="row">
                                <div class="col-xl-4 col-lg-5 col-md-6 d-flex flex-column mx-auto">
                                    <div class="card card-plain mt-8">
                                        <div class="card-header pb-0 text-left bg-transparent">
                                            <h3 class="font-weight-bolder text-info text-gradient">Welcome back</h3>
                                            <p class="mb-0">Enter your email and password to sign in</p>
                                        </div>
                                        <div class="card-body">
                                            <div role="form">
                                                <label>Email</label>
                                                <div class="mb-3">
                                                    <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Email or Username"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username or Email is Required*" ForeColor="Red" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                                </div>
                                                <label>Password</label>
                                                <div class="mb-3">
                                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter valid Password" ForeColor="Red" ControlToValidate="TextBox2"></asp:RequiredFieldValidator> 
                                                </div>
                                                <div class="form-check form-switch">
                                                    <input class="form-check-input" type="checkbox" id="rememberMe" />
                                                    <asp:Label ID="Label1" runat="server" Text="Remember me" class="form-check-label"></asp:Label>
                                                    <%--<label  for="rememberMe"></label>--%>
                                                </div>
                                                <div class="text-center">
                                                    <asp:Button ID="Button1" runat="server" Text="Sign in" class="btn bg-gradient-info w-100 mt-4 mb-0" OnClick="Button1_Click" />
                                                    <%--<button type="button" >Sign in</button>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer text-center pt-0 px-lg-2 px-1">
                                            <p class="mb-4 text-sm mx-auto">
                                                Don't have an account?
                                                <asp:HyperLink ID="HyperLink5" runat="server" class="text-info text-gradient font-weight-bold" NavigateUrl="~/SignUp.aspx">Sign up</asp:HyperLink> 
                                            </p>
                                                <%--<a href="javascript:;" >Sign up</a>--%>
                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="oblique position-absolute top-0 h-100 d-md-block d-none me-n8">
                                        <div class="oblique-image bg-cover position-absolute fixed-top ms-auto h-100 z-index-0 ms-n6" style="background-image: url('../assets/img/curved-images/curved6.jpg')"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
            <!-- -------- START FOOTER 3 w/ COMPANY DESCRIPTION WITH LINKS & SOCIAL ICONS & COPYRIGHT ------- -->
            <footer class="footer py-5">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mb-4 mx-auto text-center">
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">Company
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">About Us
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">Team
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">Products
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">Blog
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-5 me-3 mb-sm-0 mb-2">Pricing
                            </a>
                        </div>
                        <div class="col-lg-8 mx-auto text-center mb-4 mt-2">
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-4 me-4">
                                <span class="text-lg fab fa-dribbble"></span>
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-4 me-4">
                                <span class="text-lg fab fa-twitter"></span>
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-4 me-4">
                                <span class="text-lg fab fa-instagram"></span>
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-4 me-4">
                                <span class="text-lg fab fa-pinterest"></span>
                            </a>
                            <a href="javascript:;" target="_blank" class="text-secondary me-xl-4 me-4">
                                <span class="text-lg fab fa-github"></span>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-8 mx-auto text-center mt-1">
                            <p class="mb-0 text-secondary">
                                Copyright ©
                                <script>
                                    document.write(new Date().getFullYear())
                                </script>
                                Soft by Creative Tim.
       
                            </p>
                        </div>
                    </div>
                </div>
            </footer>
            <!-- -------- END FOOTER 3 w/ COMPANY DESCRIPTION WITH LINKS & SOCIAL ICONS & COPYRIGHT ------- -->
            <!--   Core JS Files   -->
            <script src="../assets/js/core/popper.min.js"></script>
            <script src="../assets/js/core/bootstrap.min.js"></script>
            <script src="../assets/js/plugins/smooth-scrollbar.min.js"></script>
            <script>
                var win = navigator.platform.indexOf('Win') > -1;
                if (win && document.querySelector('#sidenav-scrollbar')) {
                    var options = {
                        damping: '0.5'
                    }
                    Scrollbar.init(document.querySelector('#sidenav-scrollbar'), options);
                }
            </script>
            <!-- Github buttons -->
            <script src="https://buttons.github.io/buttons.js"></script>
            <!-- Control Center for Soft Dashboard: parallax effects, scripts for the example pages etc -->
            <%--<script src="../assets/js/soft-ui-dashboard.min.js?v=1.1.0"></script>--%>

            <!-- Nepcha Analytics (nepcha.com) -->
            <!-- Nepcha is a easy-to-use web analytics. No cookies and fully compliant with GDPR, CCPA and PECR. -->
            <script data-site="YOUR_DOMAIN_HERE" src="https://api.nepcha.com/js/nepcha-analytics.js"></script>
        </div>
    </form>
</body>
</html>
