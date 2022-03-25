<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="CityAddEdit.aspx.cs" Inherits="Net_Project6.Admin_Panel.City.CityAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">City Add Edit Page</h2>
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
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control dropdown-toggle" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged1" Width="100%"></asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Select Country" ControlToValidate="ddlCountryID" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

    <br />

            <div class="row">

                
                <div class="col-md-4">
                    *State:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlStateID" CssClass="form-control dropdown-toggle"/>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Select State" ControlToValidate="ddlStateID" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

    <br />


            <div class="row">
                <div class="col-md-4">
                    *City Name:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control"/>
                </div>
                <div class="col-md-4">
                    <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ErrorMessage="Enter City Name" ControlToValidate="txtCityName"  ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

    <br />


            <div class="row">
                <div class="col-md-4">
                    STDCode:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtSTDCode" CssClass="form-control"/>
                </div>
            </div>

    <br />


            <div class="row">
                <div class="col-md-4">
                    Pincode:
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPincode" CssClass="form-control"/>
                </div>
                <div class="col-md-4">
                    <asp:RegularExpressionValidator ID="revPincode" runat="server" ErrorMessage="Enter 6 Digit Pincode(Start with 1-9)" ForeColor="Red" ControlToValidate="txtPincode" ValidationExpression="^[1-9][0-9]{5}$"></asp:RegularExpressionValidator>
                </div>
            </div>
    <br />
     <div class="row">
        <div class="col-md-4">

        </div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />
            <asp:HyperLink runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger btn-sm" NavigateUrl="~/Admin Panel/City/List" />
        </div>
    </div>
</asp:Content>
