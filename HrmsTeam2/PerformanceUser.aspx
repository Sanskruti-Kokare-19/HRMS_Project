<%@ Page Title="" Language="C#" MasterPageFile="~/User.Master" AutoEventWireup="true" CodeBehind="PerformanceUser.aspx.cs" Inherits="HrmsTeam2.PerformanceUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-content-center mb-4 py-4 me-3">
        <div class="form-container">
            
            <div class="form-group">
                <label for="q1">Describe your biggest accomplishment in the past year:</label>
                <asp:TextBox ID="q1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q2">What areas do you believe you can improve in?</label>
                <asp:TextBox ID="q2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q3">How well do you feel you met your goals for the previous year?</label>
                <asp:TextBox ID="q3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q4">How effectively do you collaborate with your colleagues?</label>
                <asp:TextBox ID="q4" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q5">Do you feel your current workload is manageable?</label>
                <asp:TextBox ID="q5" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q6">Do you have any suggestions for improving your work environment?</label>
                <asp:TextBox ID="q6" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q7">What are your career aspirations within the company?</label>
                <asp:TextBox ID="q7" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q8">Do you feel you receive adequate training and support?</label>
                <asp:TextBox ID="q8" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q9">How satisfied are you with your current role?</label>
                <asp:TextBox ID="q9" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="q10">Please provide any additional feedback you wish to share:</label>
                <asp:TextBox ID="q10" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <!-- Submit Button -->
        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary mt-3 w-100" OnClick="Button1_Click" Enabled="false" /> 
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>        
    </div>
</asp:Content>
