<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusinessError.aspx.cs" Inherits="Biblioseca.Web.BusinessError" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron"  style="background-color:#FF6241">
        <h1>Error de Negocio</h1>
    <asp:TextBox ID="TextBox"  runat="server" Enabled="false" CssClass ="form-control text-uppercase" Font-Bold="true" BackColor ="#F6A999"></asp:TextBox>
 </div>
</asp:Content>
