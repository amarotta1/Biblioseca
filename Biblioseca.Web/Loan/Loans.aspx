<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="Biblioseca.Web.Loans" %>
<%@ Import Namespace="Biblioseca.Web" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Prestamos</h1>
        <asp:GridView ID ="GridViewLoans" runat="server" CssClass="table table-striped table-dark" BackColor="SkyBlue"
            AutoGenerateColumns="false">
            <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="labelID" runat="server" Text='<%# Eval("Id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Socio">
                <ItemTemplate>
                    <asp:Label ID="labelSocio" runat="server" Text='<%# String.Format("{0} {1}", Eval("partner.FirstName"),  Eval("partner.LastName"))%>'/>
                </ItemTemplate>      
            </asp:TemplateField>      
            <asp:TemplateField HeaderText="Libro">
                <ItemTemplate>
                    <asp:Label ID="labelLibro" runat="server" Text='<%# Eval("book.title")%>'/>
                </ItemTemplate>      
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha del prestamo">
                <ItemTemplate>
                    <asp:Label ID="labelDateAt" runat="server" Text='<%# Eval("initialDate") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Devuelto el">
                <ItemTemplate>
                    <asp:Label ID="labelReturnedAt" runat="server" Text='<%# Eval("returnedDate") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
     <asp:HyperLink ID="Create" NavigateUrl='<%#Pages.Loans.Create%>' runat="server">Nuevo prestamo</asp:HyperLink>     
     
    </div>  

</asp:Content>
