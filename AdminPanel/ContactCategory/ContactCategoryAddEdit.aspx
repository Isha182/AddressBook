<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">

   <div class="ContactCategory1">
        <div class="row">
        <div class="col-md-8">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            Contact Category Name : 
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtContactCatName" runat="server" CssClass="form-control" Placeholder="Enter Contact Category Name"></asp:TextBox>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-2 col-lg-push-2">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-group-sm btn-success" OnClick="btnSave_Click" />
        </div>
        <div class="col-md-1 col-lg-push-1">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>
   </div>

</asp:Content>


