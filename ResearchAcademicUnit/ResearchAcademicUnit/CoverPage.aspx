<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CoverPage.aspx.cs" Inherits="ResearchAcademicUnit.CoverPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media print{
            body{
                background-color:white;
                font-size:30px;
                
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="CoverDiv" style="font-size:18px">
        <div style="text-align: center;margin-top:50px">
            <img src="images/MEU.png" style="width: 280px" />
            <br />
            <asp:Label ID="lblFacultyPrint" runat="server" Text=""></asp:Label>
            <hr />
        </div>
        <div style="text-align: left;padding:0px 50px 20px;font-size:18px;font-family:'Simplified Arabic'">
            <asp:Label ID="lblFacultyNo" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblCoverDate" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align: right;font-family:'Khalid Art';font-weight:bold;padding:50px 50px 20px;font-size:18px;position:relative">
            الأستاذ الدكتور سلام خالد المحادين المحترم،،،<br />
            عميد عمادة الدراسات العليا والبحث العلمي،،<br />
            تحية طيبة وبعد،<br />
            <div  runat="server" visible="false" id="gsDivWared">
            <div style="position:absolute;top:0;left:0">
                <img src="signature/GS_Wared.png" style="width:256px"/>
            </div>
                <div style="position:absolute;top:75px;left:30px;color:blue">
                    <asp:Label ID="lblGSIn" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblGSDate" runat="server" Text=""></asp:Label>
                    </div>
                </div>
        </div>
        <div style="text-align: right;font-family:'Simplified Arabic';padding:0px 50px 20px;font-size:18px;text-indent: 50px;" id="FParagDiv" runat="server">
        </div>
        <div style="text-align: right;font-family:'Simplified Arabic';font-weight:bold;padding:0px 50px 20px;font-size:18px">
            عنوان البحث
        </div>
        <div style="text-align: center;font-family:Arial;font-weight:bold;padding:0px 50px 20px;font-size:18px" id="SParagDiv" runat="server">
        </div>
        <div style="text-align: right;font-family:'Simplified Arabic';font-weight:bold;padding:0px 50px 20px;font-size:18px">
            والذي تم نشره في المجلة:
        </div>
        <div style="text-align: center;font-family:'Simplified Arabic';padding:0px 50px 20px;font-size:18px" id="TParagDiv" runat="server">
        </div>
        <div style="text-align: center;font-family:'Khalid Art';font-weight:bold;padding:0px 50px 20px;font-size:18px">
            وتفضلوا بقبول فائق التقدير والإحترام،،
        </div>
        <div style="float: left;font-family:'Khalid Art';font-weight:bold;padding:0px 50px 20px;font-size:18px" id="SigDiv" runat="server">
        </div>
        <div style="position: absolute;bottom: 0px;width:100%;">
            <hr />
            <img src="images/qs.jpg" width="250px"/>
        </div>
    </div>
</asp:Content>
