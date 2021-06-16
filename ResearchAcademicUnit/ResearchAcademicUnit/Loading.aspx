<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Loading.aspx.cs" Inherits="ResearchAcademicUnit.Loading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
                .progress {
            /*display: flex;*/
            /*height: 10rem;*/
            overflow: hidden;
            position: absolute;
            z-index: 100;
            width: 100%;
            height: 100%;
            background-color: black;
            opacity: 0.5;
            font-size: .75rem;
            border-radius: .25rem;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="progress">
                <img src="images/loading.gif" style="height: 100px" />please wait...
            </div>
        <div style="position: fixed; top: 50%; left: 20%; background-color: green; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="msgDiv" visible="false">
    </div>


        <div style="position: fixed; top: 50%; left: 20%; background-color: green; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="Div1" visible="false">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
