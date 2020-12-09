<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TheatreScheduler.aspx.cs" Inherits="OMSTWebApp.Assessments.TheatreScheduler" %>
<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Movie Scheduler</h1>
    <div class="row">
        <blockquote class="alert alert-info">
            Steps to operate<br />
            <ol>
                <li>Select the movie location for scheduling</li>
                <li>Select the movie theatre for scheduling</li>
                <li>Enter the show date for scheduling (ex 2018/04/23)</li>
                <li>Press Prepare Theatres</li>
            </ol>
            This will result in a display of the theatre at the selected location. The row represents
            a theatre. The theatre will show 4 scheduling slots for movies. For each scheduling slot
            you will select a movie and enter the time for the movie to begin in the textbox (textmode).
            The time is in hours and minutes with either AM or PM selected. Once the theatre has
            had its slots set (movie and time), the user will press the Schedule Movies button.<br /><br />
            BLL Scheduling rules (<i>you</i> <u><b>must</b></u> enforce these in your BLL code):<br />
            <ul>
                <li> all scheduling slots are are filled for the theatres</li>
                <%--<li> movies can be showing in multiple theatres on the same date independent of time</li>--%>
                <li> 20 mintues between all movie start/end times</li>
                <li> No movie "overlaps" - movie cannot start in a theatre on that DATE if the TIME of 
                    the previous movie has not "ended" (including the 20 minutes above)</li>
                <li> No movies can start earlier than 11 AM</li>
                <li> No movies can end after 11 PM</li>
            </ul> 
        </blockquote>
    </div>
    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
     <div class="row">
       <div class="col-md-12">
           <asp:Label ID="Label1" runat="server" Text="Location" 
               AssociatedControlID="LocationDropDown" />
           <asp:DropDownList ID="LocationDropDown" runat="server" 
               AppendDataBoundItems="true" Height="26px"
               DataSourceID="LocationDataSource" 
               DataTextField="DisplayText" 
                               DataValueField="DisplayValue"
                AutoPostBack="true" >
               <asp:ListItem Value="0" Text="[Select a location]" />
           </asp:DropDownList>&nbsp;&nbsp;

           <asp:Label ID="Label3" runat="server" Text="Theatre"></asp:Label>
           <asp:DropDownList ID="TheatreNumbers" runat="server" 
               DataSourceID="TheatreDataSource" 
               DataTextField="DisplayText" 
               DataValueField="DisplayValue">
           </asp:DropDownList>&nbsp;&nbsp;
           <asp:Label ID="Label2" runat="server" Text="Date" AssociatedControlID="ShowDate" />
           <asp:TextBox ID="ShowDate" runat="server" TextMode="Date" Text="2018-04-23" />&nbsp;&nbsp;
           <asp:LinkButton ID="PrepareTheatres" runat="server" 
               CssClass="btn btn-secondary" Text="Prepare Theatres" />

            <asp:ObjectDataSource runat="server" ID="LocationDataSource" 
               OldValuesParameterFormatString="original_{0}" 
               SelectMethod="ListAllLocations" 
               TypeName="OMSTSystem.BLL.SchedulingController">
           </asp:ObjectDataSource>
             <asp:ObjectDataSource runat="server" ID="TheatreDataSource" 
               OldValuesParameterFormatString="original_{0}" 
               SelectMethod="ListTheatres" 
               TypeName="OMSTSystem.BLL.SchedulingController">
               <SelectParameters>
                   <asp:ControlParameter ControlID="LocationDropDown" 
                       PropertyName="SelectedValue" 
                       Name="locationID" 
                       Type="Int32"></asp:ControlParameter>
                </SelectParameters>
           </asp:ObjectDataSource>

        </div>
    </div>
     <div class="row">
       <div class="col-md-12">
           <asp:LinkButton ID="ScheduleMovies" runat="server" 
               CssClass="btn btn-primary" Text="Schedule Movies"
                OnClick="ScheduleMovies_Click" />
           <asp:GridView ID="BookingsGridView" runat="server"
               CssClass="table table-condensed"
               ItemType="OMSTSystem.ViewModels.KeyValueOption<int>"
               AutoGenerateColumns="false" DataSourceID="TheatreScheduleSource">
               <Columns>
                   <asp:BoundField DataField="DisplayText" HeaderText="Theatre No" />
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:HiddenField ID="TheatreID" runat="server" 
                               Value='<%# Item.DisplayValue %>' />
                           <asp:DropDownList ID="MovieSlot_1" runat="server" 
                               AppendDataBoundItems="true" 
                               DataSourceID="MovieDataSource" 
                               DataTextField="DisplayText" 
                               DataValueField="DisplayValue">
                               <asp:ListItem Value="0" Text="[Select a movie (# min)]" />
                           </asp:DropDownList>
                           <br />
                           <asp:TextBox ID="StartTimeSlot_1" runat="server" TextMode="Time" />
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:DropDownList ID="MovieSlot_2" runat="server" 
                               AppendDataBoundItems="true" 
                               DataSourceID="MovieDataSource" 
                               DataTextField="DisplayText" 
                               DataValueField="DisplayValue">
                               <asp:ListItem Value="0" Text="[Select a movie (# min)]" />
                           </asp:DropDownList>
                           <br />
                           <asp:TextBox ID="StartTimeSlot_2" runat="server" TextMode="Time" />
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:DropDownList ID="MovieSlot_3" runat="server" 
                               AppendDataBoundItems="true" 
                               DataSourceID="MovieDataSource" 
                               DataTextField="DisplayText" 
                               DataValueField="DisplayValue">
                               <asp:ListItem Value="0" Text="[Select a movie (# min)]" />
                           </asp:DropDownList>
                           <br />
                           <asp:TextBox ID="StartTimeSlot_3" runat="server" TextMode="Time" />
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <ItemTemplate>
                           <asp:DropDownList ID="MovieSlot_4" runat="server" 
                               AppendDataBoundItems="true" 
                               DataSourceID="MovieDataSource" 
                               DataTextField="DisplayText" 
                               DataValueField="DisplayValue">
                               <asp:ListItem Value="0" Text="[Select a movie (# min)]" />
                           </asp:DropDownList>
                           <br />
                           <asp:TextBox ID="StartTimeSlot_4" runat="server" TextMode="Time" />
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
               <EmptyDataTemplate>
                   The theatre has movies already scheduled for that date.
               </EmptyDataTemplate>
           </asp:GridView>
           <asp:ObjectDataSource runat="server" ID="TheatreScheduleSource" 
               OldValuesParameterFormatString="original_{0}" 
               SelectMethod="GetTheatre" 
               TypeName="OMSTSystem.BLL.SchedulingController">
               <SelectParameters>
                   <asp:ControlParameter ControlID="LocationDropDown" 
                       PropertyName="SelectedValue" 
                       Name="locationID" 
                       Type="Int32"></asp:ControlParameter>
                    <asp:ControlParameter ControlID="TheatreNumbers" 
                       PropertyName="SelectedValue" 
                       Name="theatreID" 
                       Type="Int32"></asp:ControlParameter>

                   <asp:ControlParameter ControlID="ShowDate" 
                       PropertyName="Text" 
                       Name="schedulingdate" 
                       Type="DateTime"></asp:ControlParameter>
               </SelectParameters>
           </asp:ObjectDataSource>
           <asp:ObjectDataSource runat="server" ID="MovieDataSource" 
               OldValuesParameterFormatString="original_{0}" 
               SelectMethod="ListMovies" TypeName="
               OMSTSystem.BLL.SchedulingController">
           </asp:ObjectDataSource>
       </div>
    </div>
</asp:Content>
