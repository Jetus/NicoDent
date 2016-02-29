<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centeringWrapper" style="width:200px">
        
        <div class="accountHeader" style="width:100%; text-align:center;">
            <img runat="server" src="~/Content/Images/NicoDent.png" alt="NicoDent" height="100" />
            <h2>Вхід у систему</h2>            
        </div>        
        <p>Будь ласка, вкажіть ваші дані</p>
        <div>
            <dx:ASPxLabel ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="Ім'я користувача:" />
            <div class="form-field">
                <dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px">
                    <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                        <RequiredField ErrorText="Ім'я користувача є обов'язковим для заповнення" IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </div>
            <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль:" />
            <div class="form-field">
                <dx:ASPxTextBox ID="tbPassword" runat="server" Password="true" Width="200px">
                    <ValidationSettings ValidationGroup="LoginUserValidationGroup">
                        <RequiredField ErrorText="Пароль є обов'язковим для заповнення" IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </div>
            <div style="width:100%; text-align:center;">
                <dx:ASPxButton ID="btnLogin" runat="server" Text="Вхід" ValidationGroup="LoginUserValidationGroup"
                    OnClick="btnLogin_Click">
                </dx:ASPxButton>
            </div>
        </div>            
    </div>
</asp:Content>