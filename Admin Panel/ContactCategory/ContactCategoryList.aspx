<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ContactCategoryList.aspx.cs" Inherits="Net_Project6.Admin_Panel.ContactCategory.ContactCategoryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">Contact Category List</h2>
        </div>
       
        <div class="row">
            <div class="col-md-12">
                <asp:Label runat="server" ID="lblDisplay" EnableViewState="False"></asp:Label>
            </div>
        </div>
      <br /> 
    </div>
    <div class="row">
        <div class="col-md-12">
             <div class="col-md-12">
                    <asp:HyperLink runat="server" ID="hlAddContactCategory" Text="Add New Contact Category" NavigateUrl="~/Admin Panel/ContactCategory/Add" CssClass="btn btn-info"></asp:HyperLink>
                </div>
            <br />
            <br />
            <br />
            <asp:GridView ID="gvContactCategory" runat="server" CssClass="table table-hover table-responsive table-bordered table-hover"  AutoGenerateColumns="false" OnRowCommand="gvConatctCategory_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactCategoryID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="btnEdit" Text="Edit" CssClass="btn btn-primary" NavigateUrl='<%# "~/Admin Panel/ContactCategory/Edit/" + Eval("ContactCategoryID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ContactCategoryName" HeaderText="Name" />
                </Columns>
            </asp:GridView>
        </div>      
    </div>
</asp:Content>
