<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptionDescrytion.aspx.cs" Inherits="Net_Project6.Admin_Panel.EncryptionDescrytion" %>

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
        <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">Encryption Decryption</h2>
        </div>
   </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:TextBox runat="server" ID="txtEncryption"></asp:TextBox>
            <br />
            <asp:Button ID="btnEncryption" runat="server" Text="Encrypt" OnClick="btnEncryption_Click" />
        </div>

        <div class="col-md-12">
            <asp:Label runat="server" ID="lblEncryption" EnableViewState="False" ForeColor="Green"></asp:Label>
        </div>
   </div>
    <br />
        <br />
        <br />
        <br />
        <br />
    <div class="row">
        <div class="col-md-12">
            <asp:TextBox runat="server" ID="txtDecryption"></asp:TextBox>
            <br />
            <asp:Button ID="btnDecryption" runat="server" Text="Decrypt" OnClick="btnDecryption_Click" />
        </div>

        <div class="col-md-12">
            <asp:Label runat="server" ID="lblDecryption" EnableViewState="False" ForeColor="Green"></asp:Label>
        </div>
   </div>
    
    </form>
</body>
</html>
