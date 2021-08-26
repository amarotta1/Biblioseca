<%@ Page Title="Edit author" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Biblioseca.Web.Author.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Editar</h2>
    <div class="form-horizontal">
        <h4>Author</h4>
        <hr/>
        <asp:TextBox ID="textBoxFirstName" placeholder="First name" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxFirstNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El nombre es obligatorio" ControlToValidate="textBoxFirstName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:TextBox ID="textBoxLastName" placeholder="Lastname" runat="server" CssClass="form-control"/>
        <asp:RequiredFieldValidator ID="textBoxLastNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="El apellido es obligatorio" ControlToValidate="textBoxLastName" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br/>
        <asp:Button ID="buttonEditAuthor" runat="server" Text="Guardar" OnClick="ButtonEditAuthor_Click" CssClass="btn btn-primary"/>
    </div>
</asp:Content>
