<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MasterReportView.aspx.cs" Inherits="ResearchAcademicUnit.MasterReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table1 {
            width: 100%;
            max-width: 100%;
            text-align: right;
            border: 1px solid black;
            font-family: 'Khalid Art';
        }

        table th {
            background-color: #e6e6e6;
            border: 1px solid black;
            font-size: 18px;
        }

        table td {
            font-family: 'Simplified Arabic';
            font-size: 16px;
        }

        table td, table th {
            padding: 2px;
        }

        .div {
            margin: 10px;
            font-family: 'Khalid Art';
            text-align: center;
            font-size: 18px;
        }

        input[type=text] {
            display: inline;
            width: 100%;
            font-family: 'Simplified Arabic' !important;
        }

        .table, .table td {
            margin: 5px auto;
            font-family: 'Khalid Art';
            text-align: center;
            font-size: 20px;
            width: 50%;
            background-color: #e6e6e6;
        }

        @media print {
            body{
                background-color:white;
            }
        }
    </style>
    <script language="javascript" type="text/javascript">
        function test1(divName, flag) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            document.location.href = document.URL;// "PrintStudSeatsFinal.aspx";
            if (flag == 1) {
                __doPostBack("btn");
            }
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="button" id="btnPrint" runat="server" style="margin-left: 10px; margin-right: 10px" class="btn" onclick="test1('printDiv1', 1);" value="    طباعة    "/>

    <div id="printDiv1">
        <div style="text-align: center; margin-top: 10px">
            <img src="images/MEU.png" style="width: 240px" />
            <div style="font-family: 'Khalid Art'; font-size: 22px">
                عمادة الدراسات العليا والبحث العلمي
            </div>
            <div style="font-family: 'Tw Cen MT'; font-size: 18px">
                Deanship of Graduate Studies & Scientific Research
            </div>
            <hr />
        </div>

        <div class="div" id="divprint" runat="server">
            <div class="div">
                <table class="table">
                    <tr>
                        <td>التقرير الأسبوعي لمتابعة لقاء المشرف</td>
                    </tr>
                    <tr>
                        <td>رقم التقرير 
                        <asp:Label ID="lblReportId" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

            </div>

            <div class="div" style="text-align: left; font-size: 18px">
                التاريخ : 
        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                <div style="clear: both"></div>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">معلومات الطالب
                        </th>
                    </tr>
                    <tr>
                        <td>اسم الطالب (رباعي):</td>
                        <td>
                            <asp:Label ID="lblStudName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>الرقم الجامعي:</td>
                        <td>
                            <asp:Label ID="lblStudId" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>الكلية:</td>
                        <td>
                            <asp:Label ID="lblStudFaculty" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>التخصص:</td>
                        <td>
                            <asp:Label ID="lblStudMajor" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">معلومات المشرف
                        </th>
                    </tr>
                    <tr>
                        <td>اسم المشرف:</td>
                        <td colspan="3">
                            <asp:Label ID="lblSupName" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>الكلية:</td>
                        <td>
                            <asp:Label ID="lblSupFaculty" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>القسم:</td>
                        <td>
                            <asp:Label ID="lblSupDpt" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">عنوان الرسالة باللغة المكتوبة فيها:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="thesisTitleDiv" runat="server"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">ما تم إنجازه من قبل الطالب:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="studReqDiv" runat="server"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">المطلوب من الطالب للأسبوع التالي:
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="NextWeekDiv" runat="server"></div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <table class="table1">
                    <tr>
                        <th colspan="4">آلية التواصل
                        </th>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBoxList ID="chkMethod" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Moodle</asp:ListItem>
                                <asp:ListItem Value="2">Zoom</asp:ListItem>
                                <asp:ListItem Value="3">Facebook</asp:ListItem>
                                <asp:ListItem Value="4">Youtube</asp:ListItem>
                                <asp:ListItem Value="5">Google Classroom</asp:ListItem>
                                <asp:ListItem Value="6">Whatsup</asp:ListItem>
                                <asp:ListItem Value="7">Microsoft Teams</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div">
                <div style="float: right; width: 49%; text-align: center">
                    توقيع الطالب<br />
                    <asp:Label ID="lblStudSign" runat="server" Text=""></asp:Label>
                </div>
                <div style="float: right; width: 49%; text-align: center">
                    توقيع المشرف<br />
                    <asp:Label ID="lblSuperSign" runat="server" Text=""></asp:Label>
                </div>
                <div style="clear: both"></div>
            </div>
        </div>

        <div style="width: 100%; text-align: left;position:fixed;bottom:0">
            <hr />
            <table dir="ltr" style="width: 100%">
                <tr>
                    <td>
                        <img src="images/qs.jpg" width="250px" /></td>
                    <td>F579, Rev. a<br />
                        Ref.: Deans' Council Session (07-2019/2020), Decision No.: 09<br />
                        Date: 16/11/2019
                    </td>
                </tr>
            </table>


        </div>
    </div>

</asp:Content>
