<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CoverPageRectorOffice.aspx.cs" Inherits="ResearchAcademicUnit.CoverPageRectorOffice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media print{
            body{
                background-color:white;
                font-size:30px;
                
            }
            .showdiv{
                display:none;
            }
        }
        @media screen
        {
            .showdiv{
                display:inline-block;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="newDiv" runat="server">
        
    </div>
<%--    <div id="CoverDiv" style="font-size:18px">
        <div style="text-align: center;margin-top:30px">
            <img src="images/MEU.png" style="width:360px"/>
        <div style="font-family: 'Khalid Art'; font-size: 26px">
            مكتب رئيس الجامعة
        </div>
        <div style="font-family: 'Agency FB'; font-size: 20px">
            President Office
        </div>
        <hr />
        </div>
        <div style="text-align: center;font-family:'Khalid Art';font-weight:bold;padding:50px 50px 20px;font-size:16px">
            قرار رقم (...-2020/2021)<br /><br />
            فبناءً على الصلاحيات المخولة لنا وما تقتضيه مصلحة العمل<br /><br />
            <u>تقــــــرر</u>
        </div>
        <div style="text-align: right;font-family:'Simplified Arabic';padding:0px 50px 20px;font-size:16px;text-indent: 50px;" id="FParagDiv" runat="server">
        </div>
        <div style="text-align: center;font-family:Simplified Arabic;font-weight:bold;padding:0px 50px 20px;font-size:16px" id="SParagDiv" runat="server">
        </div>
        <div style="text-align: center;font-family:'Simplified Arabic';padding:0px 50px 20px;font-size:16px" id="TParagDiv" runat="server">
            كون المجلة مفهرسة ضمن قاعدة بيانات <span style="font-family:'Simplified Arabic';font-size:16px">Scopus</span>.
        </div>
        <div style="text-align: center;font-family:'Khalid Art';font-weight:bold;padding:0px 50px 20px;font-size:16px">
            وتفضلوا بقبول فائق الإحترام...
        </div>
        <div style="text-align: left;font-family:'Khalid Art';font-weight:bold;padding:0px 50px 20px;font-size:16px">
            <asp:Label ID="lblPos" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br /><br />
            أ.د. علاء الدين توفيق الحلحولي
        </div>
        <div style="position: absolute;bottom: 0px;width:100%">
            <hr />
            <img src="images/footer.png" width="250px"/>
        </div>
    </div>--%>
</asp:Content>
