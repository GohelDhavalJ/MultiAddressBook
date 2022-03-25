<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ContactManyList.aspx.cs" Inherits="Net_Project6.Admin_Panel.ContactMany.ContactManyList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">ContactMany List</h2>
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
                    <asp:HyperLink runat="server" ID="hlAddContactMany" Text="Add New Contact" NavigateUrl="~/Admin Panel/ContactMany/ContactManyAddEdit.aspx" CssClass="btn btn-info" ></asp:HyperLink>
                </div>
             <br />
            <br />
            <br />
            <asp:GridView runat="server" ID="gvContactMany"  CssClass="table table-hover table-responsive table-bordered table-hover"   AutoGenerateColumns="false" OnRowCommand="gvContactMany_RowCommand">
                <Columns>
                   
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnContact" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" Id="hlContact" Text="Edit"  CssClass="btn btn-primary" NavigateUrl='<%# "~/Admin Panel/ContactMany/ContactManyAddEdit.aspx?ContactID="+Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactID" HeaderText="ID" />
                    <asp:BoundField DataField="ContactName" HeaderText="Name" />
                    <asp:BoundField DataField="ContactCategoryName" HeaderText="ContactCategory Name" />
                    
                </Columns>
            </asp:GridView>
        </div>     
    </div>
</asp:Content>
