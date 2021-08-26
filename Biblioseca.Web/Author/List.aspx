<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Biblioseca.Web.Author.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="jumbotron">
        <h1>Autores</h1>
        <asp:GridView ID ="GridViewAuthors" runat="server" CssClass="table table-striped table-dark" 
                        BackColor="SkyBlue" AutoGenerateColumns="false" DataKeyNames="Id"
                        OnRowDeleting="GridViewAuthors_Deleting" OnRowEditing="GridViewAuthors_Editing">
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
            <asp:CommandField ButtonType="Link" ShowEditButton="true"  ControlStyle-CssClass ="btn btn-primary"/>          
            <asp:CommandField ButtonType="Link"  ShowDeleteButton="true" ControlStyle-CssClass ="btn btn-danger"/>
           </Columns>
        </asp:GridView>

      <asp:Button id="createAuthor"  Text="Crear nuevo autor" OnClick="CreateBtn_Click" runat="server"/>
    </div>  

</asp:Content>
