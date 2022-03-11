<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="AdminPanel_Country_CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
   

    <div class="container-fluid">
        <div class="Country">
        <div class="row">
        <div class="col-md-12">
            <h2>Country List</h2>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink ID="hlAdd" runat="server" NavigateURL="~/AdminPanel/Country/CountryAddEdit.aspx" CssClass="btn btn-lg btn-success">Add Country</asp:HyperLink>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
           
            <asp:GridView ID="gvCountry" runat="server" OnRowCommand="gvCountry_RowCommand" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("CountryID").ToString() %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server"  CssClass="btn btn-success btn-sm" NavigateUrl='<%# "~/AdminPanel/Country/CountryAddEdit.aspx?CountryID=" + Eval("CountryID").ToString() %>'>Edit</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CountryID" HeaderText="ID" />
                    <asp:BoundField DataField="CountryName" HeaderText="Country" />
                    <asp:BoundField DataField="CountryCode" HeaderText="Code" />
                </Columns>
            </asp:GridView><br />
        </div>
    </div>
   </div>
    </div>

</asp:Content>

