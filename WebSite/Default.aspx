<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <%-- DXCOMMENT: Configure ASPxGridView control --%>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="True"  ClientInstanceName="ASPxGridView1" Width="100%">
    <SettingsPager PageSize="32" />
    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="350" />
    <SettingsBehavior AllowFocusedRow="true" />
    <Paddings Padding="0px" />
    <Border BorderWidth="0px" />
    <BorderBottom BorderWidth="1px" />
    <%-- DXCOMMENT: Configure ASPxGridView's columns in accordance with datasource fields --%>
    <Columns>
        <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="UserId" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="LastActivityDate" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>


<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT * FROM [aspnet_Users]">
</asp:SqlDataSource>--%>

</asp:Content>