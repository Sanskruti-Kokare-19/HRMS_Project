<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HRMS.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
    <style>
    /* General reset */
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        padding: 0;
        background-color: #f4f4f9;
        color: #333;
    }

    .form-container {
        max-width: 400px;
        margin: 50px auto;
        background: #fff;
        border-radius: 8px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        padding: 20px 30px;
        box-sizing: border-box;
    }

    .form-container h2 {
        text-align: center;
        margin-bottom: 20px;
        color: #4CAF50;
    }

    .info-label {
        display: block;
        text-align: center;
        margin-bottom: 20px;
        color: #555;
        font-size: 14px;
    }

    select, input[type="date"], textarea, button {
        width: 100%;
        padding: 10px;
        margin: 10px 0;
        border: 1px solid #ddd;
        border-radius: 5px;
        box-sizing: border-box;
        font-size: 14px;
    }

    select:focus, input[type="date"]:focus, textarea:focus, button:focus {
        outline: none;
        border-color: #4CAF50;
        box-shadow: 0 0 5px rgba(76, 175, 80, 0.3);
    }

    textarea {
        resize: none;
    }

    button {
        background-color: #4CAF50;
        color: white;
        font-weight: bold;
        border: none;
        cursor: pointer;
        transition: background-color 0.3s ease;
    }

    button:hover {
        background-color: #45a049;
    }

    button:active {
        background-color: #3e8e41;
    }

    #LeaveStatusLabel {
        display: block;
        text-align: center;
        margin-top: 15px;
        font-size: 14px;
        color: #f44336;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Leave Management</h2>
           
            <asp:Label ID="lblLeaveTypeHeading" runat="server" Text="LEAVE TYPE:" CssClass="info-label" Style="font-weight: bold;"></asp:Label><br />
            <asp:DropDownList ID="LeaveTypeDropDown" runat="server">
                <asp:ListItem Text="CL" Value="CL"></asp:ListItem>
                <asp:ListItem Text="PL" Value="PL"></asp:ListItem>
                <asp:ListItem Text="ML" Value="ML"></asp:ListItem>
                <asp:ListItem Text="SL" Value="SL"></asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="lblDateHeading" runat="server" Text="DATE:" CssClass="info-label" Style="font-weight: bold;"></asp:Label><br />

            <asp:Label ID="lblStartDate" runat="server" Text="From:"></asp:Label>
            <asp:TextBox ID="StartDateTextBox" runat="server" TextMode="Date" Placeholder="Start Date"></asp:TextBox>

            <asp:Label ID="lblEndDate" runat="server" Text="To:"></asp:Label>
            <asp:TextBox ID="EndDateTextBox" runat="server" TextMode="Date" Placeholder="End Date"></asp:TextBox>

            <asp:TextBox ID="ReasonTextBox" runat="server" TextMode="MultiLine" Rows="4" Placeholder="Reason"></asp:TextBox>
            <asp:Button ID="SubmitLeaveButton" runat="server" Text="Submit Leave" OnClick="SubmitLeaveButton_Click" />
            <asp:Label ID="LeaveStatusLabel" runat="server"></asp:Label>

            <asp:Label ID="Label1" runat="server" CssClass="info-label" Text=""></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="CL: " CssClass="info-label"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="PL: " CssClass="info-label"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="ML: " CssClass="info-label"></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="SL: " CssClass="info-label"></asp:Label>
        </div>
    </form>
</body>
</html>
