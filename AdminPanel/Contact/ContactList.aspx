<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanel_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
   <div class="container-fluid">
       <div class="Contact">
        <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" Text="" runat="server" EnableViewState="false"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>Contact List</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAdd" runat="server" NavigateURL="~/AdminPanel/Contact/ContactAddEdit.aspx" CssClass="btn btn-lg btn-success">Add Contact</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
          
            <asp:GridView ID="gvContact" runat="server" OnRowCommand="gvContact_RowCommand">
                <columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-success btn-sm" NavigateUrl='<%# "~/AdminPanel/Contact/ContactAddEdit.aspx?ContactID=" + Eval("ContactID").ToString() %>'>Edit</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactID" HeaderText="ID" />
                    <asp:BoundField DataField="CountryName" HeaderText="Country" />
                    <asp:BoundField DataField="StateName" HeaderText="State" />
                    <asp:BoundField DataField="CityName" HeaderText="City" />
                    <asp:BoundField DataField="ContactCategoryName" HeaderText="Contact Category" />
                    <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                    <asp:BoundField DataField="ContactNo" HeaderText="Number" />
                    <asp:BoundField DataField="WhatsAppNo" HeaderText="WhatsApp Number" />
                    <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Age" HeaderText="Age" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                    <asp:BoundField DataField="FacebookID" HeaderText="Facebook ID" />
                    <asp:BoundField DataField="LinkedINID" HeaderText="LinkedIn ID" />
                </columns>
            </asp:GridView>
        </div>
    </div>
   </div>
   </div>
</asp:Content>

