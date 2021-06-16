<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HelpDeskIT.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="images/favicon.png" rel="icon" />
    <title>شاشة الدخول - عمادة الدراسات العليا والبحث العلمي - جامعة الشرق الأوسط</title>
    <!-- Bootstrap core CSS -->
    <link href="lib/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />--%>
    <!--external css-->
    <link href="css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/style-responsive.css" rel="stylesheet" />
</head>
<body>
    <div style="background-color: black; opacity: 0.5; position: fixed; width: 100%; height: 100%; z-index: -1;"></div>
    <div id="login-page">
        <div class="container">
            <form class="form-login" runat="server" >
                <h2 class="form-login-heading">دخول</h2>
                <div class="login-wrap">
                    <asp:UpdatePanel ID="upLogin" runat="server">
                        <ContentTemplate>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <input type="text" data-mask="099999999" class="form-control" id="txtUserId" runat="server" placeholder="User ID" autocomplete="off" autofocus="autofocus" required="required"/>
                            <br />
                            <input type="password" class="form-control" id="txtUserPW" runat="server" placeholder="Password" required="required"  autocomplete="off"/>
                            <label class="checkbox">
                                <span class="pull-right">
                                    <%--<a data-toggle="modal" href="login.html#myModal">نسيت كلمة المرور</a>--%>
                                </span>
                            </label>
                            <br />
                            <asp:LinkButton ID="lnkLogin" runat="server" class="btn btn-theme btn-block" OnClick="btnLogin_Click"><i class="fa fa-lock"></i> دخــــــول</asp:LinkButton>
                            <%--<asp:Button ID="btnLogin" runat="server" Text="دخول" OnClick="btnLogin_Click" class="btn btn-theme btn-block" />--%>
                            <hr />
                            <div class="alert alert-danger alert-dismissable text-right" runat="server" id="errMsg" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkLogin" EventName="Click"/>
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <!-- Modal -->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Forgot Password ?</h4>
                            </div>
                            <div class="modal-body">
                                <p>Enter your e-mail address below to reset your password.</p>
                                <input type="text" name="email" placeholder="Email" autocomplete="off" class="form-control placeholder-no-fix" />
                            </div>
                            <div class="modal-footer">
                                <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                                <button class="btn btn-theme" type="button">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal -->
            </form>
        </div>
        <div>
            asdasdasdad
        </div>
    </div>
    <!-- js placed at the end of the document so the pages load faster -->
    <script src="lib/jquery/jquery.min.js"></script>
    <script src="lib/bootstrap/js/bootstrap.min.js"></script>
    <!--BACKSTRETCH-->
    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="lib/jquery.backstretch.min.js"></script>
    <script src="jquery-mask-input/jquery.mask.min.js"></script>
    <script src="lib/form-validation-script.js"></script>
    <script>
        $.backstretch("images/meu.jpg", {
            speed: 500
        });
    </script>
</body>
</html>
