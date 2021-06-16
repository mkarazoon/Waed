<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="Agreement.aspx.cs" Inherits="ResearchAcademicUnit.Agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: red;
        }
    </style>
    <style type="text/css">
        .printDiv {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 15px;
        }
    </style>
        <style>
            table, th, td {
                padding: 5px;
            }

            table {
                border-spacing: 10px;
            }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <article style="width:98%">
            <div class="sec" id="printDiv">
                <p style="font-weight:700">
                    <span>ملف النتاج البحثي
                    </span>
                </p>
                </div>
                <p>
                    أعضاء الهيئة الأكاديمية الكرام
                </p>
                <p>
                    تهديكم عمادة الدراسات العليا والبحث العلمي أطيب الأمنيات ونرجوا التكرّم بتعبئة ملف النتاج البحثي الإلكتروني والذي أعد <span class="auto-style1"><strong>لرصد كافة النتاجات البحثية خلال آخر 5 سنوات (2014-2019) </strong></span>راجيين قبل البدء الإنتباه إلى الملاحظات التالية :
                </p>
                <p>
                        أولاً : هذا النموذج متاح ابتداءاً من يوم <span class="auto-style1"><strong>الاثنين الموافق (16-12-2019</strong><span"></span">
                        </span><strong><span class="auto-style1">) ولغاية&nbsp; نهاية يوم السبت الموافق (28-12-2019</span></strong><span class="auto-style1"><span"><strong></strong></span">
                        </span></span></span><span class="auto-style1"><strong>) </strong></span>وبعدها لن يكون هناك إمكانية للدخول . لنتكمن نحن من إنجاز ما هو مطلوب اعتماداً على البيانات المدخلة. لذا نؤكد على ضرورة استكماله ضمن الفترة المحددة أعلاه .
                </p>
                        <p>
                            ثانياً : يمكنك في <span class="auto-style1"><strong>أي وقت ضمن الفترة المسموحة أن تُعدّل أو تستكمل </strong></span>ما قمت بإدخاله.
                        </p>
                            <p>
                                ثالثأ : نود أن نلفت عنايتكم بضرورة تحضير <span class="auto-style1"><strong>الملفات التالية </strong></span>قبل البدء بعملية الإدخال لأنك ستحتاج رفعها في مرحلة تحميل ملفات النتاج البحثي
                            </p>
                            <p class="auto-style1">
                                <strong>ملف واحد مجمّع على شكل (</strong><span><strong>PDF</strong></span><strong>) يتضمن الصفحة الأولى (العنوان، الملخص) من كل مما يلي (إن وجد): </strong>
                            </p>
                            <table>
                                <tr>
                                    <td>1. كافة الأبحاث المنشورة في Scopus.</td>
                                    <td>2. كافة الأبحاث المنشورة بقواعد أخرى.</td>
                                    <td>3. كافة أوراق المؤتمرات داخل الجامعة.</td>
                                </tr>
                                <tr>
                                    <td>4. كافة أوراق المؤتمرات داخل الأردن.</td>
                                    <td>5. كافة أوراق المؤتمرات الإقليمية (الدول العربية).</td>
                                    <td>6. كافة أوراق المؤتمرات الدولية.</td>
                                </tr>
                                <tr>
                                    <td>7. كافة الكتب أو (الفصول) ذات الناشر الدولي.</td>
                                    <td>8. كافة الكتب أو (الفصول) ذات الناشر المحلي.</td>
                                    <td>9. كافة الكتب أو (الفصول) ذات الناشر الإقليمي.</td>
                                </tr>
                                <tr>
                                    <td>10. كافة شهادات الورش (حضور/مشاركة).</td>
                                    <td>11. كافة شهادات الندوات (حضور/مشاركة).</td>
                                    <td>12. كافة شهادات الدورات (حضور/مشاركة).</td>
                                </tr>
                                <tr>
                                    <td>13. كافة شهادات التميز.</td>
                                </tr>
                                <tr>
                                    <td>ملاحظة: في حال مواجهة أي مشاكل تقنية يرجى مراجعة دائرة تكنولوجيا المعلومات</td>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="chkAgree" runat="server" Text="أقر أنا عضو الهيئة التدريسية بأني قرأت كافة التعليمات السابقة"
                        Font-Bold="true" Font-Size="Large" AutoPostBack="true" OnCheckedChanged="chkAgree_CheckedChanged" ForeColor="Red"/>
                                    </td>
                                </tr>
                            </table>
            </article>
                <div class="sec" runat="server" visible="false" id="AgreeDiv">
                    <asp:Button ID="btnTrainSave" runat="server" ValidationGroup="save" Text="ادخال المعلومات"  OnClick="btnTrainSave_Click" Width="100px" Height="40px" CssClass="btn" style="direction: ltr" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
