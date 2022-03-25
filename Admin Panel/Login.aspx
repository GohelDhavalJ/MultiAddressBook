<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Net_Project6.Admin_Panel.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/CSS/MyCSS.css" rel="stylesheet" />
    <script src="~/Content/JS/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row col-md-12 header header A:hover" >
                <div class="col-md-2 img-responsive" style="float:left">
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Content/Images/Darshan Universitylogo.png" Height="42px" Width="90px" />
                </div>
            <div class="col-md-12">
                
            </div>
         </div>

        <div class="row">
            <div class="col-md-12">
                 <h1 class="MainHeading">Login Page</h1>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:Label runat="server" ID="lblDisplay" EnableViewState="False" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
                <div class="col-md-4">
                    User Name:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUserNameLogin" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                     <asp:RequiredFieldValidator ID="rfvUserNameLogin" runat="server" ErrorMessage="Enter UserName" ControlToValidate="txtUserNameLogin" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                </div>
        </div>
        <br />

        <div class="row">
                <div class="col-md-4">
                    Password:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPasswordLogin" CssClass="form-control" TextMode="Password" />
                </div>
                <div class="col-md-4">
                     <asp:RequiredFieldValidator ID="rfvPasswordLogin" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPasswordLogin" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                </div>
        </div>
        <br />

        <div class="row">
                <div class="col-md-4">

                </div>
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-primary btn-sm" OnClick="btnLogin_Click" ValidationGroup="Login"/>
                </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-12">
                <h1 class="MainHeading">New Registration</h1>
            </div>

        </div>

        <div class="row">
                <div class="col-md-4">
                    User Name:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtUserNameRegistration" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                     <asp:RequiredFieldValidator ID="rfvUserNameRegistration" runat="server" ErrorMessage="Enter UserName" ControlToValidate="txtUserNameRegistration" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                </div>
        </div>
        <br />

         <div class="row">
                <div class="col-md-4">
                    Password:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPasswordRegistration" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                     <asp:RequiredFieldValidator ID="rfvPasswordRegistration" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtPasswordRegistration" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                </div>
        </div>
        <br />

        <div class="row">
                <div class="col-md-4">
                    Display Name:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDisplayNameRegistration" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                     <asp:RequiredFieldValidator ID="rfvDisplayNameRegistration" runat="server" ErrorMessage="Enter Display Name" ControlToValidate="txtDisplayNameRegistration" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                </div>
        </div>
        <br />

        <div class="row">
                <div class="col-md-4">
                    MobileNo :
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtMobileNoRegistration" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <asp:RegularExpressionValidator ID="revContactNoRegistration" runat="server" ControlToValidate="txtMobileNoRegistration" ErrorMessage="Enter Valid ContactNo(10 digit)" ForeColor="Red" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-4">
                    Email:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEmailRegistration" CssClass="form-control"/>
                </div>
                <div class="col-md-4">
                       <asp:RegularExpressionValidator ID="revEmailRegistration" runat="server" ControlToValidate="txtEmailRegistration" ErrorMessage="Enter Valid Email Address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                </div>
        </div>
        <br />

        <div class="row">
                <div class="col-md-4">

                </div>
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnRegister" Text="Submit" CssClass="btn btn-primary btn-sm" ValidationGroup="Submit" OnClick="btnRegister_Click"/>
                </div>
        </div>

        <br />

        <div class="row col-md-12 footer">
                
                <div class="col-md-12" style="text-align:center;color:red; ">
                       <h4>Dhaval Gohel | 190540107069 | 190540107069@darshan.ac.in</h4>
                    <hr />
                </div>
                
                <div class="col-md-12">
                    <div class="col-md-2 img-responsive">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Content/Images/du-logo-horizontal-white.svg" Height="54px" Width="100px" />
                    </div>
                    <div class="col-md-10">
                        <p>
                          Darshan University - having the foundation of its first Institution laid in 2009, and later on, established and consolidated under Gujarat Private Universities (Second Amendment) Act of 2021. It is one of the leading institutes within Saurashtra that offers Diploma, UG, and PG Courses.
                        </p> 
                   </div>

                </div>
        </div>
       
       
    </div>
    </form>
</body>
</html>
