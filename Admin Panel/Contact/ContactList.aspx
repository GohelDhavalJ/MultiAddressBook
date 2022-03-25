<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ContactList.aspx.cs" Inherits="Net_Project6.Admin_Panel.Contact.ContactList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
     <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">Contact List</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label runat="server" ID="lblDisplay" EnableViewState="False"></asp:Label>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12">
                    <asp:HyperLink runat="server" ID="hlAddContact" Text="Add New Contact" NavigateUrl="~/Admin Panel/Contact/Add" CssClass="btn btn-info"></asp:HyperLink>
                </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="gvContact" runat="server" CssClass="table table-hover table-responsive table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="gvContact_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnContact" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            
                            <asp:HyperLink runat="server" ID="btnEdit" Text="Edit" CssClass="btn btn-primary" NavigateUrl='<%# "~/Admin Panel/Contact/Edit/" + Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="CountryName" HeaderText="Country"/>
                    <asp:BoundField DataField="StateName" HeaderText="State"/>
                    <asp:BoundField DataField="CityName" HeaderText="City"/>
                    <asp:BoundField DataField="ContactCategoryName" HeaderText="ContactCategory"/>
                    <asp:BoundField DataField="ContactName" HeaderText="ContactName"/>
                    <asp:BoundField DataField="ContactNo" HeaderText="ContactNo"/>
                    <asp:BoundField DataField="Email" HeaderText="Email"/>
                    <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" DataFormatString="{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="Address" HeaderText="Address"/>
                </Columns>
            </asp:GridView>
        </div>      
    </div>
</asp:Content>
