<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="HRMS.Notification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Notification</title>
</head>
<body>
    <form id="form1" runat="server">
          <div>
            <h2>Send Notification</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label><br /><br />
            
            <label for="ddlUsers">Select Users to Notify:</label><br />
            <asp:DropDownList ID="ddlUsers" runat="server" DataTextField="UserEmail" DataValueField="UserEmail" AppendDataBoundItems="true" Multiple="true">
            </asp:DropDownList>
            <br /><br />
            
            <label for="txtNotification">Notification Message:</label><br />
            <asp:TextBox ID="txtNotification" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox><br /><br />
            
            <asp:Button ID="btnSend" runat="server" Text="Send Notification" OnClick="btnSend_Click" />
        </div>
    </form>
</body>
</html>
