<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="Net_Project6.FileUpload" %>

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
        <div>
            <h2>Select File To Upload</h2><br />
            <asp:FileUpload ID="fuFile" runat="server" /><br />
            <asp:Button runat="server" ID="btnUpload" Text="Upload"  CssClass="btn-info" OnClick="btnUpload_Click"/><br />
            <asp:Label runat="server" ID="lblDisplay" EnableViewState="false"></asp:Label><br />
            <asp:Button runat="server" ID="btnDelete" Text="Delete"  CssClass="btn-btn-danger" OnClick="btnDelete_Click" /><br />
        </div>
        
    </form>
</body>
</html>
