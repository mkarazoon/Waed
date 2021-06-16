<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CoverPageReDept.aspx.cs" Inherits="ResearchAcademicUnit.CoverPageReDept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @media print {
            body {
                background-color: white;
                font-size: 30px;
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
    <div id="CoverDiv" style="font-size: 18px">
        <div style="text-align: center; margin-top: 30px">
            <img src="images/MEU.png" style="width: 360px" />
            <div style="font-family: 'Khalid Art'; font-size: 26px">
                عمادة الدراسات العليا والبحث العلمي
            </div>
            <div style="font-family: 'Tw Cen MT'; font-size: 20px">
                Deanship of Graduate Studies & Scientific Research
            </div>
            <hr />
        </div>
        <div style="text-align: left; padding: 0px 50px 20px; font-size: 16px; font-family: 'Simplified Arabic'">
            <asp:Label ID="lblFacultyNo" runat="server" Text=""></asp:Label>
            <div class="showdiv" id="reqid" runat="server"></div>
            <br />
            <asp:Label ID="lblCoverDate" runat="server" Text=""></asp:Label>
        </div>
        <div style="text-align: right; font-family: 'Khalid Art'; font-weight: bold; padding: 0px 50px 20px; font-size: 16px">
            الاستاذ الدكتور علاء الدين توفيق الحلحولي المحترم،،<br />
<%--            <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />--%>
            رئيس الجامعة<br />
            تحية طيبة وبعد،،<br />
        </div>
        <div style="text-align: right; font-family: 'Simplified Arabic'; padding: 0px 50px 20px; font-size: 18px; text-indent: 50px;" id="FParagDiv" runat="server">
        </div>
        <div style="text-align: center; font-family: Arial; font-weight: bold; padding: 0px 50px 20px; font-size: 18px" id="SParagDiv" runat="server">
        </div>
        <div style="text-align: right; font-family: 'Simplified Arabic'; font-weight: bold; padding: 0px 50px 20px; font-size: 18px">
            والذي تم نشره في المجلة التالية:
        </div>
        <div style="text-align: center; font-family: 'Simplified Arabic'; padding: 0px 50px 20px; font-size: 18px" id="TParagDiv" runat="server">
        </div>
        <div style="text-align: center; font-family: 'Khalid Art'; font-weight: bold; padding: 0px 50px 20px; font-size: 18px">
            وتفضلوا بقبول فائق التقدير والإحترام،،
        </div>
        <div style="position: relative; float: left; text-align: center; font-family: 'Khalid Art'; font-weight: bold; padding: 0px 50px 20px; font-size: 18px">
            <asp:Label ID="lblPos" runat="server" Text=""></asp:Label>
<%--            ق.أ.عميد الدراسات العليا والبحث العلمي--%>
            <div>
                <br />
                <%--<img src="signature/1357.jpg" style="width: 128px; z-index: -1" />--%>
            </div>
            <%--<img src="signature/sign_gs.jpg" style="position: absolute; width: 128px; z-index: -1; top: 0; left: 20%" />--%>
            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
            
        </div>
        <div style="position: relative; float: right; text-align: right; font-family: 'Khalid Art'; font-weight: bold; padding: 0px 50px 20px; font-size: 18px">
            نسخة:<br />
            <asp:Label ID="lblType" runat="server" Text=""></asp:Label><br />
            - ملف رئيس الجامعة<br />
            - الصادر الداخلي
        </div>
        <div style="position: absolute; bottom: 0px; width: 100%">
            <hr />
            <img src="images/newfooter.png" width="250px" />
        </div>
    </div>
</asp:Content>
