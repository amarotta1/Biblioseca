<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvailableBooks.aspx.cs" Inherits="Biblioseca.Web.AvailableBooks" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Libros Disponibles</h1>
        <asp:GridView ID ="GridViewAvailableBooks" runat="server" CssClass="table table-striped table-dark" BackColor="SkyBlue" AutoGenerateColumns="false">
           <Columns>
            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="labelID" runat="server" Text='<%# Eval("Id") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="Title" runat="server" Text='<%# Eval("title") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="Description" runat="server" Text='<%# Eval("description") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Stock">
                <ItemTemplate>
                    <asp:Label ID="Stock" runat="server" Text='<%# Eval("stock") %>'/>
                </ItemTemplate>      
            </asp:TemplateField>  
           </Columns>
        </asp:GridView>
    </div>  

</asp:Content>