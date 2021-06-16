<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CWSSupportForm.aspx.cs" Inherits="ResearchAcademicUnit.CWSSupportForm" %>

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
            width:30%;
        }

        input[type=text],input[type=number] {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 100px auto; padding: 2%">
        <div class="TitleDiv">
            نموذج دعم عقد مؤتمر/ ورشة عمل/ ندوة علمية
        </div>
        <div>

            <table class="auto-style1" dir="rtl" border="1">
                <tbody>
                    <tr>
                        <td class="auto-style2">
                            عنوان النشاط العلمي: 
                        </td>
                        <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>الفترة المنوي عقد النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>مكان عقد النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>مدة النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>عدد البحوث العلمية المرشحة:  (*مرفق البحوث العلمية)</p>
                        </td>
                        <td>
                            <input type="number" />

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>المشاركين الداخليين: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>المشاركين الخارجيين المقترحين: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>الدعم المالي المطلوب: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>الجهات الداعمة للنشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>نبذة مختصرة عن النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>الهدف من النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>الغاية من النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>المبررات النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <p>النتائج المرجوة تحقيقها من عقد النشاط العلمي: </p>
                        </td>
                        <td><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>


        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn" Style="padding: 10px" />
        </div>
    </div>
</asp:Content>
