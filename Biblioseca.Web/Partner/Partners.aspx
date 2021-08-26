<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Partners.aspx.cs" Inherits="Biblioseca.Web.Partner.Partners" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Socios</h1>
        <asp:GridView ID ="GridViewPartners" runat="server" CssClass="table table-striped table-dark" BackColor="SkyBlue" AutoGenerateColumns="false">
           <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="labelID" runat="server" Text='<%# Eval("Id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="Name" runat="server" Text='<%# Eval("FirstName") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:Label ID="LastName" runat="server" Text='<%# Eval("LastName") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>  
           </Columns>
        </asp:GridView>
    </div>  

</asp:Content>
