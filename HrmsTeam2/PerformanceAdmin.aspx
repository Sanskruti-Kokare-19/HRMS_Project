<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PerformanceAdmin.aspx.cs" Inherits="HrmsTeam2.PerformanceAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    HRMS_T2
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-content-center mb-4 py-4 me-3">
        <asp:Label ID="Label1" runat="server" Text="Select User" class="text-lg font-weight-bold "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
            <asp:ListItem>User</asp:ListItem>
            <asp:ListItem>Admin</asp:ListItem>
        </asp:DropDownList>

        <asp:Button ID="Button1" runat="server" Text="Send" class="btn btn-primary mt-3 w-100" OnClick="Button1_Click" />

        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    </div>
</asp:Content>
