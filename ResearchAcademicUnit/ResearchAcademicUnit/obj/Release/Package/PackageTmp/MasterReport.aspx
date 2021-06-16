<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="MasterReport.aspx.cs" Inherits="ResearchAcademicUnit.MasterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            width: 100%;
            max-width: 100%;
            text-align: right;
            border: 1px solid black;
            font-family: 'Khalid Art';
        }

            table th {
                background-color: #e6e6e6;
                border: 1px solid black;
                font-size: 20px;
            }

            table td {
                font-family: 'Simplified Arabic';
                font-size: 18px;
            }

            table td, table th {
                padding: 8px;
            }

        .div {
            margin: 30px;
            font-family: 'Khalid Art';
            text-align: center;
            font-size: 30px;
        }

        input[type=text] {
            display: inline;
            width: 100%;
            font-family: 'Simplified Arabic' !important;
        }

        .table, .table td {
            margin: 30px auto;
            font-family: 'Khalid Art';
            text-align: center;
            font-size: 30px;
            width: 50%;
            background-color: #e6e6e6;
        }

        @media print {
            .hide {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>

            <div class="div" id="divprint" runat="server">
                <div class="div">
                    <table class="table">
                        <tr>
                            <td>التقرير الأسبوعي لمتابعة لقاء المشرف</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                رقم التقرير 
                    <asp:DropDownList ID="ddlReportNo" runat="server" CssClass="ChosenSelector1">
                        <asp:ListItem Value="0"></asp:ListItem>
                        <asp:ListItem Value="1"></asp:ListItem>
                        <asp:ListItem Value="2"></asp:ListItem>
                        <asp:ListItem Value="3"></asp:ListItem>
                        <asp:ListItem Value="4"></asp:ListItem>
                        <asp:ListItem Value="5"></asp:ListItem>
                        <asp:ListItem Value="6"></asp:ListItem>
                        <asp:ListItem Value="7"></asp:ListItem>
                        <asp:ListItem Value="8"></asp:ListItem>
                        <asp:ListItem Value="9"></asp:ListItem>
                        <asp:ListItem Value="10"></asp:ListItem>
                        <asp:ListItem Value="11"></asp:ListItem>
                        <asp:ListItem Value="12"></asp:ListItem>
                        <asp:ListItem Value="13"></asp:ListItem>
                        <asp:ListItem Value="14"></asp:ListItem>
                        <asp:ListItem Value="15"></asp:ListItem>
                        <asp:ListItem Value="16"></asp:ListItem>
                        <asp:ListItem Value="17"></asp:ListItem>
                        <asp:ListItem Value="18"></asp:ListItem>
                        <asp:ListItem Value="19"></asp:ListItem>
                        <asp:ListItem Value="20"></asp:ListItem>
                    </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="ddlReportNo" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>

                </div>
                <div>
                    <div runat="server" id="whoInsertedDiv" class="div" style="text-align: right; float: right">
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="تعبئة الطلب من قبل"></asp:Label>
                        <asp:DropDownList ID="ddlOption" runat="server" CssClass="ChosenSelector">
                            <asp:ListItem Value="1">طالب</asp:ListItem>
                            <asp:ListItem Value="2">المشرف</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="div" style="text-align: left; float: left">
                        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        التاريخ : 
        <asp:TextBox ID="txtReportDate" runat="server" Width="200px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="خطأ بالتاريخ" ForeColor="Red" Font-Bold="true" Font-Size="Medium" ControlToValidate="txtReportDate" Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtReportDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="clear: both"></div>
                    </div>
                </div>
                <div class="div">
                    <table border="1">
                        <tr>
                            <th colspan="4">معلومات الطالب
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                اسم الطالب (رباعي):</td>
                            <td>
                                <asp:TextBox ID="txtStudName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtStudName" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                الرقم الجامعي:</td>
                            <td>
                                <asp:TextBox ID="txtStudId" runat="server" onkeypress="return isNumberKey(event)" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtStudId" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>الكلية:</td>
                            <td>
                                <asp:DropDownList ID="ddlStudFaculty" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                            </td>
                            <td>التخصص:</td>
                            <td>
                                <asp:TextBox ID="txtStudMajor" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <table>
                        <tr>
                            <th colspan="4">معلومات المشرف
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                اسم المشرف:</td>
                            <td colspan="3">
                                <asp:TextBox Width="100%" ID="txtSupervisorName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtSupervisorName" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>الكلية:</td>
                            <td>
                                <asp:DropDownList ID="ddlSuperFaculty" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlSuperFaculty_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>القسم:</td>
                            <td>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <table>
                        <tr>
                            <th colspan="4">
                                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                عنوان الرسالة باللغة المكتوبة فيها:
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtThesisTitle" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtThesisTitle" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <table>
                        <tr>
                            <th colspan="4">
                                <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                ما تم إنجازه من قبل الطالب:
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtStudAchievment" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="مطلوب" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtStudAchievment" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <table>
                        <tr>
                            <th colspan="4">المطلوب من الطالب للأسبوع التالي:
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtStudRequired" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <table>
                        <tr>
                            <th colspan="4">آلية التواصل
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:CheckBoxList ID="chkMethod" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
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
                <asp:Button ID="btnSend" runat="server" CssClass="btn" Text="إرسال" OnClick="btnSend_Click" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "70%" });
            $('.ChosenSelector1').chosen({ width: "100px" });
        }
    </script>

</asp:Content>
