<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ViewPerformance.aspx.cs" Inherits="HrmsTeam2.ViewPerformance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    HRMS-T2
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-column justify-content-center mb-4 py-4 me-3">
        <asp:Label ID="Label1" runat="server" Text="Select User" class="text-lg font-weight-bold "></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

    <div class="row">
            <div class="col-12">
                <div class="card mb-4 me-3">
                    <div class="card-header pb-0">
                        <h6>Projects table</h6>
                    </div>
                    <div class="card-body px-0 pt-0 pb-2">
                        <div class="table-responsive p-0">
                            <div class="grid-container p-4">
                                <asp:GridView ID="GridView1" runat="server" 
                                    CssClass="table align-items-center justify-content-center mb-0"
                                    AutoGenerateColumns="False" 
                                    EmptyDataText="No reviews found for selected user.">
                                    <Columns>
                                        <asp:BoundField DataField="Q_Text" HeaderText="Question" 
                                            ItemStyle-CssClass="text-sm" 
                                            HeaderStyle-CssClass="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7" />
                                        <asp:BoundField DataField="answerText" HeaderText="Answer" 
                                            ItemStyle-CssClass="text-sm" 
                                            HeaderStyle-CssClass="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2" />
                                        <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted Date" 
                                            DataFormatString="{0:MM/dd/yyyy}"
                                            ItemStyle-CssClass="text-sm" 
                                            HeaderStyle-CssClass="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
