<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateAddEdit.aspx.cs" Inherits="AdminPanel_State_StateAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
   <div class="State1">
        <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-4">
                    Country :
                </div>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlCountryID" runat="server" CssClass="form-control" Width="550px"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

        <div class="row">
        <div class="col-md-2">
            State Name :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtStateName" runat="server" CssClass="form-control" Placeholder="Enter State Name"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            State Code :
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="txtStateCode" runat="server" CssClass="form-control" Placeholder="Enter State Code"></asp:TextBox>
        </div>
    </div>
   
    <div class="row">
        <div class="col-md-2 col-lg-push-2">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-sm btn-default" OnClick="btnSave_Click" />
        </div>
        <div class="col-md-1 col-lg-push-1">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click" />
        </div>
    </div>
   </div>
</asp:Content>

