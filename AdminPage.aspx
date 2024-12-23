<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="HRMS.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="LeaveRequestsGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="LeaveRequestsGrid_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                    <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                    <asp:BoundField DataField="TotalDays" HeaderText="Days" />
                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                    <asp:BoundField DataField="LeaveStatus" HeaderText="Status" />
                    <asp:BoundField DataField="AdminActionBy" HeaderText="Action By" />
                    <asp:BoundField DataField="AdminActionDate" HeaderText="Action Date" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="ApproveButton" runat="server" CommandName="Approve" CommandArgument='<%# Eval("LeaveId") %>' Text="Approve" />
                            <asp:Button ID="RejectButton" runat="server" Text="Reject" CommandName="Reject" CommandArgument='<%# Eval("LeaveId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
