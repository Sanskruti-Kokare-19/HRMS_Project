<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="HRMS.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="notificationRepeater" runat="server">
    <HeaderTemplate>
        <table>
            <tr>
                <th>Message</th>
                <th>Date</th>
                <th>Time</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Eval("NotificationMessage") %></td>
            <td><%# Eval("NotificationDate", "{0:yyyy-MM-dd}") %></td>
            <td><%# Eval("NotificationTime", "{0:hh\\:mm\\:ss}") %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
        </div>
    </form>
</body>
</html>
