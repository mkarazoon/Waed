<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questionnaire.aspx.cs" Inherits="ResearchAcademicUnit.Questionnaire" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table {
            direction: rtl;
            width: 100%;
            border-spacing: 0px;
            border: 1px solid lightgray;
            vertical-align: middle;
        }

        .hd {
            font-size: 20px;
            font-weight: bolder;
            padding: 10px;
            text-align: right;
            background-color: #f5f5f5;
            font-family: 'Khalid Art';
        }

        .ftd {
            width: 5%;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
        }

        .std {
            width: 33%;
            font-weight: bold;
            padding: 15px;
        }

        .txt {
            max-width: 100%;
            min-width: 100%;
            min-height: 30px;
            max-height: 100px;
            box-sizing: border-box;
        }

        .hdtd {
            width: 28%;
            font-weight: bold;
            padding: 15px;
        }

        .div {
            background-color: white;
            border-radius: 15px;
            margin: 1%;
            border: 2px outset;
            padding: 1%;
        }

        body {
            background-color: #f5f5f5;
        }

        .btn {
            border-radius: 6px;
            width: 80px;
            padding: 5px;
            color: white;
            background-color: #921A1D;
            font-size: small;
            font-family: 'Droid Arabic Kufi' !important;
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
            cursor: pointer;
            border: none;
        }

            .btn:hover {
                box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
                color: black;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="div" dir="rtl" style="text-align:center;font-family:'Khalid Art';background-color:gray;font-size:large;color:white" >
                    استطلاع اراء الهيئة الاكاديمية التدريسية والإدارية حول أداء قسم البحث العلمي/عمادة الدراسات العليا والبحث العلمي في جامعة الشرق الأوسط 
                </div>
                <div class="div" dir="rtl" style="font-family:'Khalid Art'">
                    عضو الهيئة الاكاديمية الموقر
انطلاقا من ترسيخ مبدأ الشفافية والاستدامة تحرص عمادة الدراسات العليا ممثلة بقسم البحث العلمي على استطلاع راي المجتمع البحثي العام الذي يعمل تحت مظلة جامعة الشرق الأوسط بأداء قسم البحث العلمي في جامعتنا من خلال مقياس الرضا البحثي التالي. نرجو التكرم استكمال المقياس ونعدكم باننا سنقوم وبعناية بتحليل كافة الاستجابات للخروج بتوصيات من شانها رفع سوية عمل القسم.

                </div>
                <div class="div">
                    <div class="hd">الجزء الأول: المعلومات العامة</div>
                    <table border="1">
                        <tr>
                            <td class="ftd">الكلية</td>
                            <td class="hdtd">
                                <asp:Label ID="lblCollege" runat="server" Text=""></asp:Label></td>
                            <td class="ftd">القسم</td>
                            <td class="hdtd">
                                <asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                            <td class="ftd">الرتبة الأكاديمية</td>
                            <td class="hdtd">
                                <asp:Label ID="lblAcdDegree" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </table>
                    <table border="1">
                        <tr>
                            <td class="hdtd">
                                <asp:RadioButtonList ID="rdSex" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="M">ذكر</asp:ListItem>
                                    <asp:ListItem Value="F">أنثى</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rdSex" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="hdtd">
                                <asp:TextBox ID="txtExp" runat="server" CssClass="txt" placeholder="عدد سنوات الخبرة في الجامعة" Width="20%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="txtExp" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="hdtd">
                                <asp:DropDownList ID="ddlPos" runat="server">
                                    <asp:ListItem Value="0">اختيار</asp:ListItem>
                                    <asp:ListItem Value="1">الادارة العليا</asp:ListItem>
                                    <asp:ListItem Value="2">عميد</asp:ListItem>
                                    <asp:ListItem Value="3">رئيس قسم</asp:ListItem>
                                    <asp:ListItem Value="4">عضو هيئة تدريس</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="ddlPos" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء الثاني: درجة الرضا البحثي في مجال التواصل (1 غير مقبول – 5 ممتاز)</div>

                    <table border="1">
                        <tr>
                            <td class="ftd">1</td>
                            <td class="std">درجة معرفتي بقسم البحث العلمي وماهية عمله</td>
                            <td>
                                <asp:RadioButtonList ID="rd1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">2</td>
                            <td class="std">كياسة ولباقة رئيس قسم البحث العلمي </td>
                            <td>
                                <asp:RadioButtonList ID="rd2" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd2" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">3</td>
                            <td class="std">استجابة قسم البحث العلمي لاستفسارات الباحثين في كافة الأوقات ومتابعة المواضيع البحثية العالقة</td>
                            <td>
                                <asp:RadioButtonList ID="rd3" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd3" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">4</td>
                            <td class="std">تمتع قسم البحث العلمي بالمعرفة والخبرة الكافية في مجال عمله</td>
                            <td>
                                <asp:RadioButtonList ID="rd4" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd4" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">5</td>
                            <td class="std">تقديم حلول بحثية للتحديات البحثية التي تواجه الباحثين والتواصل مع أي جهة ضمن المؤسسة لاستمرارية العمل البحثي </td>
                            <td>
                                <asp:RadioButtonList ID="rd5" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd5" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء الثالث: درجة شفافية وعدالة الممارسات البحثية (1 غير مقبول – 5 ممتاز)</div>

                    <table border="1">
                        <tr>
                            <td class="ftd">1</td>
                            <td class="std">شفافية ووضوح المعلومات البحثية المقدمة من قبل قسم البحث العلمي وذلك على مستوى الجامعة-الكلية والباحثين </td>
                            <td>
                                <asp:RadioButtonList ID="rd6" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd6" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">2</td>
                            <td class="std">ممارسات قسم البحث العلمي عادلة وواضحة وبعيدة عن الشخصنة</td>
                            <td>
                                <asp:RadioButtonList ID="rd7" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd7" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء الرابع: درجة الرضا عن التدريب البحثي (1 غير مقبول – 5 ممتاز)</div>

                    <table border="1">
                        <tr>
                            <td class="ftd">1</td>
                            <td class="std">يقدم قسم البحث العلمي دورات تدريبية بحثية انطلاقا من الاحتياجات التدريبية </td>
                            <td>
                                <asp:RadioButtonList ID="rd8" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd8" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">2</td>
                            <td class="std">تنوع التدريب البحثي من حيث النوع (دورات-لقاءات بحثية -ورش وغيرها) ومن حيث مكان التنفيذ (داخل حرم الجامعة – عن بعد) </td>
                            <td>
                                <asp:RadioButtonList ID="rd9" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd9" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">3</td>
                            <td class="std">اكسبني قسم البحث العلمي مهارات بحثية جديدة </td>
                            <td>
                                <asp:RadioButtonList ID="rd10" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd10" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء الخامس: درجة الرضا عن التحفيز البحثي (1 غير مقبول – 5 ممتاز)</div>

                    <table border="1">
                        <tr>
                            <td class="ftd">1</td>
                            <td class="std">تقدم عمادة الدراسات العليا/قسم البحث العلمي تحفيزا ماديا يشجع على النشر</td>
                            <td>
                                <asp:RadioButtonList ID="rd11" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd11" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">2</td>
                            <td class="std">تقدم عمادة الدراسات العليا/قسم البحث العلمي تحفيزا معنويا يشجع على النشر (التقارير البحثية التنافسية على مستوى الجامعة-الكلية والباحثين) </td>
                            <td>
                                <asp:RadioButtonList ID="rd12" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd12" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">3</td>
                            <td class="std">تقدم عمادة الدراسات العليا/قسم البحث العلمي مسابقات بحثية لأفضل بحث وباحث وكلية (ملاحظة سيتم تكريم الفائزين للعام الحالي مع بداية العام الأكاديمي ٢٠٢٠/٢٠٢١) </td>
                            <td>
                                <asp:RadioButtonList ID="rd13" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd13" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">4</td>
                            <td class="std">تقدم عمادة الدراسات العليا/قسم البحث العلمي دعم للمشاريع البحثية المقدمة من قبل الكليات </td>
                            <td>
                                <asp:RadioButtonList ID="rd14" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd14" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء السادس: درجة الرضا عن الخدمات البحثية (1 غير مقبول – 5 ممتاز)</div>

                    <table border="1">
                        <tr>
                            <td class="ftd">1</td>
                            <td class="std">يقدم قسم البحث العلمي التقارير البحثية التي تبين الأداء البحثي على مستوى الجامعة-الكليات والباحثين</td>
                            <td>
                                <asp:RadioButtonList ID="rd15" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd15" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ftd">2</td>
                            <td class="std">يقدم قسم البحث العلمي للباحثين استشارات وتوصيات بحثية بخصوص المجلات المفهرسة وحالتها وكذلك فهرسة الأبحاث في قاعدة البيانات المعتمدة بالجامعة </td>
                            <td>
                                <asp:RadioButtonList ID="rd16" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red" ControlToValidate="rd16" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div">
                    <div class="hd">الجزء السابع: مقترحات للتطوير البحثي</div>

                    <table border="1">
                        <tr>
                            <td class="std">
                                <asp:TextBox ID="txtuggestion" runat="server" CssClass="txt" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div" dir="rtl">
                    <asp:Button ID="btnSubmit" runat="server" Text="ارسال" CssClass="btn" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnIgnore" runat="server" Text="تخطي" CssClass="btn" OnClick="btnIgnore_Click" CausesValidation="false" />
                    <div style="background-color: lightseagreen; color: white; font-size: 30px; padding: 5px; margin: 10px; border-radius: 5px" runat="server" id="msg" visible="false">
                        <asp:Label ID="Label1" runat="server" Text="تم الارسال بنجاح"></asp:Label>
                    </div>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="5000"></asp:Timer>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
