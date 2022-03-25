<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="StateAddEdit.aspx.cs" Inherits="Net_Project6.Admin_Panel.State.StateAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">State Add Edit Page</h2>
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
                    *Country:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control dropdown-toggle"/>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Select Country" ControlToValidate="ddlCountryID" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
        
    <br />

            <div class="row">
                <div class="col-md-4">
                    *State Name:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtStateName" CssClass="form-control"/>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ErrorMessage="Enter State Name" ControlToValidate="txtStateName"  ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

    <br />

            <div class="row">
                <div class="col-md-4">
                    State Code:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtStateCode" CssClass="form-control"/>
                </div>
            </div>   

    <br />
    <div class="row">
        <div class="col-md-4">

        </div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />&nbsp&nbsp
            <asp:HyperLink runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" NavigateUrl="~/Admin Panel/State/List" />
        </div>
    </div>
</asp:Content>
