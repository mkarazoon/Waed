<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResearchProjectSupportForm.aspx.cs" Inherits="ResearchAcademicUnit.ResearchProjectSupportForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 90%;
            border-collapse: collapse;
            border: 1px solid #000000;
            margin:0 auto 0 auto;
        }

        table td {
            padding: 4px;
        }

        .auto-style2 {
            text-align:left;
        }

        .auto-style3 {
            width:30%;
        }

        input[type=text], input[type=number],input[type=date],input[type=email] {
            width: 100%;
        }

        .div{
            margin-bottom:1%;
        }
        .multiline {
            width: 100%;
            max-width: 100%;
            min-width: 100%;
            box-sizing: border-box;
            resize: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 100px auto; padding: 2%">
        <div class="TitleDiv">
            نموذج طلب دعم مشروع بحث علمي
        </div>
        <div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                    1. عنوان مشروع البحث
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    باللغة العربية
                            </td>
                            <td class="auto-style2">
                                    In English
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="4" class="multiline"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="4" class="multiline"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td class="auto-style3">
                                    2. المدة الزمنية لتنفيذ البحث
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td class="auto-style3">
                                    3. المبلغ المطلوب لتمويل البحث
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="5">
                                    4. الباحثون
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">باحث رئيسي</asp:ListItem>
                                    <asp:ListItem Value="2">باحث مشارك</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">
                                    الاسم
                            </td>
                            <td>
                                    الأول
                            </td>
                            <td >
                                    الأب
                            </td>
                            <td >
                                    الجد
                            </td>
                            <td >
                                    العائلة
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox60" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox61" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox62" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox63" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                    الرتبة العلمية
                            </td>
                            <td colspan="4">
                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">أستاذ</asp:ListItem>
                                    <asp:ListItem Value="2">أستاذ مشارك</asp:ListItem>
                                    <asp:ListItem Value="3">أستاذ مساعد</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    الكلية
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox64" runat="server"></asp:TextBox>

                            </td>
                            <td>
                                    القسم
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox65" runat="server"></asp:TextBox>

                            </td>
                            <td>
                                    التخصص الدقيق
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    تاريخ التعيين في الجامعة
                            </td>
                            <td colspan="4">
                                <input type="date" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    البريد الالكتروني
                            </td>
                            <td colspan="4">
                                <input type="email" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    الهاتف
                            </td>
                            <td>
                                    العمل
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox66" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                    الخلوي
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox67" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Button ID="Button1" runat="server" Text="إضافة" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                </div>
            </div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="4">
                                    ج. مساعدو بحث، فنيو مختبر ... إلخ.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    الاسم
                            </td>
                            <td >
                                    المؤهلات
                            </td>
                            <td >
                                    القسم والكلية
                            </td>
                            <td>
                                    
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <asp:TextBox ID="TextBox68" runat="server"></asp:TextBox></td>
                            <td >
                                <asp:TextBox ID="TextBox69" runat="server"></asp:TextBox></td>
                            <td >
                                <asp:TextBox ID="TextBox70" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="اضافة" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div>
                    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                </div>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                    5. ملخص مشروع البحث (150 كلمة)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    باللغة العربية
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" CssClass="multiline" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style2">
                                    In English
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" CssClass="multiline" TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                    6.
                                    هل سبق أن حصل الباحث الرئيس/الباحثون على دعم بحث؟
                        
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">نعم</asp:ListItem>
                                    <asp:ListItem Value="2">لا</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    عنوانات البحوث التي دعمت حتى تاريخه
                            </td>
                            <td colspan="2">
                                    تاريخ الدعم
                                    (من ... إلى)
                            </td>
                            <td>
                                    جهة الدعم
                            </td>
                            <td>
                                    قيمة الدعم
                            </td>
                            <td>
                                    اسم الباحث
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox71" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox72" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox73" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox74" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox75" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox76" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="إضافة" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="6">
                                    7. بيان بالبحوث المنشورة في السنوات الثلاث الأخيرة:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    عنوان البحث
                            </td>
                            <td>
                                    المجلة
                            </td>
                            <td>
                                    الصفحات
                            </td>
                            <td>
                                    السنة
                            </td>
                            <td>
                                    المجلد
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox77" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox78" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox79" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox80" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox81" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="إضافة" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    8. أهمية البحث:
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    9. أهداف البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    10. منهجية البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    11. الجهة (الجهات) التي يمكن أن تستفيد من البحث من
                        الناحية التنفيذية:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    12. عرض موجز وموثق للدراسات السابقة في موضوع البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    13. مساهمة كل باحث في مشروع البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>

                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    14. المهمات التي سيقوم بها مساعد الباحث، الفني ... إلخ
                        (إن وجد):
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    15. المعلومات والبيانات التي يحتاجها البحث وطريقة
                        الحصول عليها:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    16. المصادر والمراجع:
                               
                            </td>
                        </tr>
                        <tr>
                            <td width="558">
                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                
                                    17. المراحل الزمنية اللازمة لإنجاز البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                
                                    18. مخصصات المشروع
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                
                                    الميزانيــة
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    المدة الزمنية المقدرة لتنفيذ مشروع البحث
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                                
                                    شهر.
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="3">
                                
                                    احتياجات مشروع البحث:
                                
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="5" valign="middle">
                                
                                    1. مستلزمات
                                
                            </td>
                            <td>
                                
                                    البند<em></em>
                               
                            </td>
                            <td>
                                
                                    القيمة بالدينار الأردني
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (أ) الأجهزة والأدوات (ترفق بها قائمة)
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ب) المواد (ترفق بها قائمة)
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ج) برمجيات
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    المجموع
                               
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td rowspan="4" valign="middle">
                                
                                    2. سفر
                                
                            </td>
                            <td>
                                
                                    البند
                                
                            </td>
                            <td>
                                
                                    القيمة بالدينار الأردني
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (أ) الرحلات الداخلية
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ب) الرحلات الخارجية
                                
                                    (وفقا لتعليمات مجلس البحث العلمي)
                               
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    المجموع
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td rowspan="8" valign="middle">
                                
                                    3. متفرقات
                                
                            </td>
                            <td >
                                
                                    البند
                                
                            </td>
                            <td>
                                
                                    القيمة بالدينار الأردني
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                               
                                    (أ) قرطاسية
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ب) رسم
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ج) تصوير
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (د) طباعة
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (هـ) أجور عمال
                               
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (و) أجور نشر
                              
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                               
                                    المجموع
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td rowspan="8" valign="middle">
                                
                                    4. أخرى – يرجى التحديد
                                
                            </td>
                            <td>
                                
                                    البند
                                
                            </td>
                            <td>
                                
                                    القيمة بالدينار الأردني
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox39" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox40" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    المجموع
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox44" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td colspan="2">
                                
                                    ميزانية المشروع: (مجموع البنود 1- 4)
                               
                            </td>
                            <td >
                                <asp:TextBox ID="TextBox45" runat="server"></asp:TextBox>
                                     دينار.
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                
                                    الزمن المقدر لتنفيذ مشروع البحث
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                
                                    المدة الزمنية المقدرة لتنفيذ مشروع البحث:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox46" runat="server"></asp:TextBox>
                                
                                     شهر.
                                
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="6">
                                
                                    5. باحثون
                                
                            </td>
                            <td>
                                
                                    البند
                                
                            </td>
                            <td>
                                
                                    الزمن المقدر (ساعة/يوم)
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (أ) الرئيسي:
                               
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox47" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ب) مشارك:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox48" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                               
                                    (ج) مشارك:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox49" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (د) مشارك:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox50" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (هـ) مشارك:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox51" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td rowspan="5" valign="middle">
                                    6. مساعدون
                            </td>
                            <td >
                                
                                    القيمة المطلوبة
                                
                            </td>
                            <td>
                                
                                    الزمن المقدر (ساعة/يوم)<em></em>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (أ) مساعدو باحث:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox52" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ب) فنيون:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox53" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (ج) مهنيون:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox54" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                
                                    (د) رسامون:
                                
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox55" runat="server"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td rowspan="2">
                                    7. دعم خارجي
                            </td>
                            <td>
                                    الجهــة الداعمــة:
                                <asp:TextBox ID="TextBox56" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox57" runat="server"></asp:TextBox>
                                     دينار
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                    قيمة الدعم الخارجي: (ترفق وثيقة بمفرداتها)
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox58" runat="server"></asp:TextBox>
                                     (ساعة/شهر)
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="div">
                <table class="auto-style1" dir="rtl" border="1">
                    <tbody>
                        <tr>
                            <td>
                                    المبلغ الإجمالي المطلوب للدعم من عمادة الدراسات العليا
                        والبحث العلمي
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox59" runat="server"></asp:TextBox>
                                     دينار
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>


        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn" Style="padding: 10px" />
        </div>
    </div>
</asp:Content>
