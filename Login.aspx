<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
    body {
        font-family: Arial, sans-serif;
        margin: 20px;
        padding: 0;
    }
    .login-container {
        width: 300px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 10px;
        background-color: #f9f9f9;
    }
    .login-container h2 {
        text-align: center;
    }
    .form-group {
        margin-bottom: 15px;
    }
    label {
        display: block;
        margin-bottom: 5px;
        font-weight: bold;
    }
    input[type="text"], input[type="password"] {
        width: 100%;
        padding: 8px;
        margin: 5px 0;
        box-sizing: border-box;
    }
    .button-container {
        text-align: center;
    }
    .error-label {
        text-align: center;
        color: red;
        margin-top: 10px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>User Login</h2>
            <asp:Label ID="ErrorLabel" runat="server" CssClass="error-label" Text=""></asp:Label>
            <div class="form-group">
                <label for="username">Username:</label>
                <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="email">Email:</label>
                <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="password">Password:</label>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div class="button-container">
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
