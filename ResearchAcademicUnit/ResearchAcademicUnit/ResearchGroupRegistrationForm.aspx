<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResearchGroupRegistrationForm.aspx.cs" Inherits="ResearchAcademicUnit.ResearchGroupRegistrationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 70%;
            border-collapse: collapse;
            border: 1px solid #000000;
            margin: 0 auto 0 auto;
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
            نموذج تسجيل مجموعة بحثية
        </div>
        <div>
            <table class="auto-style1" dir="rtl" border="1">
                <tbody>
                    <tr>
                        <td>
                            <p>اسم المجموعة البحثية (المجال العلمي) </p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Group Name (Scientific Field)</strong> </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>اسم الباحث الرئيس: </p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Principal Investigator</strong> </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>الاسم باللغة العربية: </p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Full Name (English) </strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>الرقم الوظيفي</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>MEU ID</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <p>المرتبة العلمية</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Academic Rank</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>الكلية </p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>College</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>القسم</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Department</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>التخصص العام</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>General Field</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>التخصص الدقيق</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Subspecialty Field</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>رقم الجوال</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>Mobile No</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>البريد الالكتروني</p>
                        </td>
                        <td class="auto-style2">
                            <p><strong>E-mail</strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p></p>
            <table class="auto-style1" style="text-align: center" dir="rtl">
                <tr>
                    <td>أعضاء المجموعة البحثية</td>
                    <td class="auto-style2">Research group members</td>
                </tr>
                <tr>
                    <td>أعضاء هيئة التدريس</td>
                    <td class="auto-style2"><strong>1.  Co – Investigator</strong></td>
                </tr>
            </table>
            <p></p>
            <table class="auto-style1" style="text-align: left" dir="ltr" border="1">
                <tbody>
                    <tr>
                        <td>
                            <strong>1.</strong>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                        <td>
                            <strong>Academic Rank:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Subsp. Field:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>College:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Dept.:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Signature:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <strong>2.</strong>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>
                        <td>
                            <strong>Academic Rank:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Subsp. Field:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>College:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Dept.:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Signature:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <strong>3.</strong>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>
                        <td>
                            <strong>Academic Rank:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Subsp. Field:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>College:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Dept.:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Signature:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <strong>4.</strong>

                        </td>
                        <td>
                            <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>
                        <td>
                            <strong>Academic Rank:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Subsp. Field:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>College:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>Dept.:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Signature:</strong></td>
                        <td>
                            <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong></td>
                        <td colspan="4">
                            <asp:TextBox ID="TextBox39" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>

            <p></p>
            <table class="auto-style1" style="text-align: center" dir="rtl">
                <tr>
                    <td>طلبة دراسات عليا</td>
                    <td class="auto-style2"><strong>2.  Graduate Students</strong></td>
                </tr>
            </table>
            <p></p>

            <table class="auto-style1" style="text-align: left" dir="ltr" border="1">
                <tbody>
                    <tr>
                        <td>
                            <strong>1.</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox40" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong>
                        </td>

                        <td>
                            <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>2.</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <strong>E-mail:</strong>
                        </td>

                        <td>
                            <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p></p>
            <table class="auto-style1" dir="rtl" border="1">
                <tbody>
                    <tr>
                        <td>
                            <p>مشاريع بحثية مدعمة حاليا </p>
                        </td>
                        <td>
                            <strong>Current Research Funds</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="TextBox44" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p></p>
            <p></p>
            <p></p>
            <p></p>
            <p></p>
            <table class="auto-style1" dir="rtl" border="1">
                <tbody>
                    <tr>
                        <td>
            مؤشرات الأداء
                        </td>
                        <td>
             <strong>Performance Indicators</strong>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <p>تلتزم المجموعة البحثية بنشر عدد من الأوراق العلمية (Proceeding &amp; Activators)، بحسب العقد المبرم مع الباحث الرئيس للمجموعة، في المجالات المدرجة على قوائم معهد المعلومات العلمي ISI خلال مدة العقد.</p>
                        </td>
                        <td class="auto-style2">
                            <p>The Research group is committed to publish number of research Articles (According to the contract signed by PI) in the institute of Scientific information (ISI) list during contract period.</p>
                            <p><strong>List of Recent publication (ISI):</strong></p>
                            <p><strong>Please attach the list of recent publications </strong>(Over the last 2 years. Please, indicate ISI Impact Factor when appropriate):</p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p></p>
            <table class="auto-style1" dir="rtl" border="1">
                <tbody>
                    <tr>
                        <td>
                            <p>الباحث الرئيس:</p>
                        </td>
                        <td class="auto-style2">
                            <p>Principal Investigator</p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            التوقيع
                        </td>
                        <td class="auto-style2">
                            Signature
                        </td>
                    </tr>
                    <tr>
                        <td width="624">
                            التاريخ
                        </td>
                        <td class="auto-style2">
                            Date
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn" Style="padding: 10px" />
        </div>
    </div>
</asp:Content>
