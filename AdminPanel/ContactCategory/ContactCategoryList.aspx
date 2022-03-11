<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
  <div class="container-fluid">
       <div class="ContactCategory">
        <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Category List</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAdd" runat="server" NavigateURL="~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx" CssClass="btn btn-lg btn-success">Add Contact Category</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
           
            <asp:GridView ID="gvContactCategory" runat="server" OnRowCommand="gvContactCategory_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactCategoryID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-success btn-sm" NavigateUrl='<%# "~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx?ContactCategoryID=" + Eval("ContactCategoryID").ToString() %>'>Edit</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactCategoryID" HeaderText="ID" />
                    <asp:BoundField DataField="ContactCategoryName" HeaderText="Contact Category" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
   </div>
  </div>
</asp:Content>

