<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WorkShopSeminarForm.aspx.cs" Inherits="ResearchAcademicUnit.WorkShopSeminarForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid #000000;
        }

        table td {
            padding: 4px;
        }

        .auto-style2 {
            text-align: left;
        }

        input[type=text] {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 100px auto; padding: 2%">
        <div class="TitleDiv">
            نموذج طلب اشتراك في ورشة عمل/ندوة علمية
        </div>
        <div>

            <table class="auto-style1" dir="rtl" border="1">
                <tr>
                    <td>اسم الباحث الرئيس:</td>
                    <td class="auto-style2">Principal Investigation</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>أسماء الباحثين المشاركين (  إن وجد )</td>
                    <td class="auto-style2">The names of researchers involved</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>الرتبة الأكاديمية</td>
                    <td class="auto-style2">Academic Rank</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblDegree" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>الكلية</td>
                    <td class="auto-style2">Faculty</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblFaculty" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>القسم العلمي</td>
                    <td class="auto-style2">Department</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblDept" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>تاريخ تقديم الطلب</td>
                    <td class="auto-style2">Application Date</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span><%= DateTime.Now.Date.ToString("dd-MM-yyyy") %></span>
                    </td>
                </tr>
                <tr>
                    <td>اسم الندوة/ورشة العمل</td>
                    <td class="auto-style2">Conference Name</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>تاريخ انعقاد الندوة/ورشة العمل</td>
                    <td class="auto-style2">Conference Location</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="date" />
                    </td>
                </tr>
                <tr>
                    <td>مكان انعقاد الندوة/ورشة العمل</td>
                    <td class="auto-style2">Conference Date</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>طبيعة المساهمة في الندوة/ورشة العمل</td>
                    <td class="auto-style2">Attendance</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">بحث مقبول</asp:ListItem>
                            <asp:ListItem Value="2">حضور فقط</asp:ListItem>
                        </asp:CheckBoxList>

                    </td>
                </tr>
                <tr>
                    <td>عنوان البحث</td>
                    <td class="auto-style2">Title of the Accepted Paper</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>نبذة مختصرة عن البحث</td>
                    <td class="auto-style2">Summary of the Paper</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>الدعم المطلوب</td>
                    <td class="auto-style2">Requested Fund</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>المرفقات</td>
                    <td class="auto-style2">Appendix</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn" style="padding:10px"/>
        </div>
    </div>
</asp:Content>
