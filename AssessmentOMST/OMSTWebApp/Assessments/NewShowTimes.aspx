<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewShowTimes.aspx.cs" Inherits="OMSTWebApp.Assessments.NewShowTimes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ShowTimes added during Assessment</h1>
    <h3>These are showtimes currently on file. They are displayed in descending 
        order of StartDate.
    </h3>
    <asp:GridView ID="ShowTimeList" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ShowTimeListODS" AllowPaging="True" CssClass="table">
        <Columns>
            <asp:BoundField DataField="ShowTimeID" HeaderText="ShowTimeID" SortExpression="ShowTimeID"></asp:BoundField>
            <asp:BoundField DataField="MovieIDTitle" HeaderText="MovieID (Title)" SortExpression="MovieIDTitle"></asp:BoundField>
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate"></asp:BoundField>
            <asp:BoundField DataField="TheatreIDTheatreNumber" HeaderText="TheatreID (TheatreNumber)" SortExpression="TheatreIDTheatreNumber"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ShowTimeListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="ShowTimes_ListByStartTimes" 
        TypeName="OMSTSystem.BLL.SchedulingController">
    </asp:ObjectDataSource>
</asp:Content>
