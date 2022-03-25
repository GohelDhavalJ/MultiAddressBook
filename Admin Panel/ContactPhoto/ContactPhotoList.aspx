<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ContactPhotoList.aspx.cs" Inherits="Net_Project6.Admin_Panel.ContactPhoto.ContactList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <h2 class="MainHeading">ContactPhoto List</h2>
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
                    <asp:HyperLink runat="server" ID="hlAddContactPhoto" Text="Add New Contact" NavigateUrl="~/Admin Panel/ContactPhoto/ContactPhotoAddEdit.aspx" CssClass="btn btn-info" ></asp:HyperLink>
                </div>
             <br />
            <br />
            <br />
            <asp:GridView runat="server" ID="gvContactPhoto"  CssClass="table table-hover table-responsive table-bordered table-hover"   AutoGenerateColumns="false" OnRowCommand="gvContactPhoto_RowCommand" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgContactPhotoPath" ImageUrl='<%# Eval("ContactPhotoPath") %>' Height="50px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnContact" Text="Delete" CssClass="btn btn-danger btn-sm" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" Id="hlContact" Text="Edit"  CssClass="btn btn-primary" NavigateUrl='<%# "~/Admin Panel/ContactPhoto/ContactPhotoAddEdit.aspx?ContactID="+Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ContactName" HeaderText="Name" />
                    <asp:BoundField DataField="ContactPhotoPath" HeaderText="Path" />
                    
                </Columns>
            </asp:GridView>
        </div>     
    </div>
</asp:Content>
