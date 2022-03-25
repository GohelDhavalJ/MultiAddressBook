<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ContactPhotoAddEdit.aspx.cs" Inherits="Net_Project6.Admin_Panel.ContactPhoto.ContactPhotoAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">ContactPhoto  Add Edit Page</h2>
        </div>
   </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <asp:Label runat="server" ID="lblMessage" EnableViewState="False" ForeColor="Green"></asp:Label>
        </div>
   </div>
    <br />
    
    <div class="row">
        <div class="col-md-4">
            *Contact Name:
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" />
        </div>
        <div class="col-md-4">
             <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ErrorMessage="Enter ContactName" ControlToValidate="txtContactName" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4">
            Upload Photo:
        </div>
        <div class="col-md-4">
            <asp:FileUpload ID="fuContactPhotoPath" runat="server" />
            <asp:Image  runat="server"  ID="imgPhoto" Height="30px"/>
            <%--<asp:HiddenField ID="hfContactPhotoPath" runat="server"/> --%>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-4">

        </div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click"  />
            <asp:HyperLink runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" NavigateUrl="~/Admin Panel/ContactPhoto/ContactPhotoList.aspx"  />
        </div>
    </div>
</asp:Content>
