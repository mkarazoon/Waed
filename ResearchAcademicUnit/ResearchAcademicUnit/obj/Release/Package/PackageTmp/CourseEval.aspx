<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CourseEval.aspx.cs" Inherits="ResearchAcademicUnit.CourseEval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            border-collapse: collapse;
            font-size: 10.0pt;
            font-family: Calibri, sans-serif;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background: #DDDDDD;
        }

        .auto-style2 {
            width: 427.5pt;
            border-collapse: collapse;
            font-size: 10.0pt;
            font-family: Calibri, sans-serif;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            margin-left: -2.3pt;
            background: #EEECE1;
        }

        .auto-style4 {
            font-weight: bold;
        }

                @media print {

            /* visible when printed */


            .ddlShow {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="printDiv">
        <div style="align-items:center; width: 100%" class="print-only">
            <center>
            <img src="images/new_Log.png"  style="width: 358px"/>
                </center>
        </div>
    <div style="margin:30px">

        <div align="center">
            <table border="1" cellpadding="0" cellspacing="0" class="auto-style1" dir="rtl" style="mso-border-alt: double windowtext 1.5pt; mso-yfti-tbllook: 480; mso-padding-alt: 0in 5.4pt 0in 5.4pt; mso-table-dir: bidi">
                <tr style="mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes;
      height:33.6pt">
                    <td style="width: 293.15pt; border: double windowtext 1.5pt; background: #EAEAEA; padding: 0in 5.4pt 0in 5.4pt; height: 33.6pt" width="391">
                        <p align="center" class="MsoNormal" dir="RTL">
                            <span lang="AR-JO" style="font-size:18.0pt;font-family:&quot;Khalid Art bold&quot;;mso-ascii-font-family:
      Arial;mso-hansi-font-family:Arial;color:black;mso-bidi-language:AR-JO">استبانة </span><span lang="AR-SA" style="font-size:18.0pt;font-family:&quot;Khalid Art bold&quot;;
      mso-ascii-font-family:Arial;mso-hansi-font-family:Arial;color:black">تقييم </span><span lang="AR-SA" style="font-size:18.0pt;font-family:&quot;Khalid Art bold&quot;;mso-ascii-font-family:
      Tahoma;mso-hansi-font-family:Tahoma">ورشة عمل</span><span lang="AR-SA" style="font-size:18.0pt;font-family:&quot;Khalid Art bold&quot;;mso-no-proof:yes">/ دورة تدريبية</span><span lang="AR-JO" style="font-size:18.0pt;font-family:&quot;Khalid Art bold&quot;;
      mso-bidi-language:AR-JO"><o:p></o:p></span></p>
                    </td>
                </tr>
                <tr>
                    <td id="title" runat="server"></td>
                </tr>
            </table>
        </div>

    </div>
    <div style="margin:30px">

        تهدف هذه الاستبانة إلى تقييم عضو هيئة التدريس للورشة التدريبية بغرض التحسين والتطوير، حيث يقوم عضو هيئة التدريس باختيار التقييم المناسب، علماً بأن كل رقم من الأرقام المقابلة تمثل تقديراً على النحو الآتي:
    </div>
    <div style="margin:30px">
        <table style="border:1px solid black;border-spacing:0;width:100%;text-align:center" border="1">
            <tr>
                <td>الرقم</td>
                <td>5</td>
                <td>4</td>
                <td>3</td>
                <td>2</td>
                <td>1</td>
            </tr>
            <tr>
                <td>التقدير</td>
                <td>ممتاز</td>
                <td>جيدجدا</td>
                <td>جيد</td>
                <td>مقبول</td>
                <td>ضعيف</td>
            </tr>
        </table>
    </div>
    <div id="trainerDet" runat="server" style="margin:30px">
        <asp:Label ID="Label1" runat="server" Text="معلومات عامة"></asp:Label>

        <br />
            <table style="border:1px solid black;border-spacing:0;width:100%;" border="1">
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label2" runat="server" Text="الاسم:" style="font-weight: 700"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label3" runat="server" Text="الجنس:" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblSex" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label4" runat="server" Text="الرتبة العلمية:" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblDegree" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label11" runat="server" Text="الكلية"></asp:Label></td>
                    <td style="padding:5px">
                        <asp:Label ID="lblCollege" runat="server" Text="Label"></asp:Label></td>
                    <td style="padding:5px">
                        <asp:Label ID="Label5" runat="server" Text="القسم:" CssClass="auto-style4"></asp:Label></td>
                    <td style="padding:5px">
                        <asp:Label ID="lblDept" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label6" runat="server" Text="التخصص الدقيق:" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblMinor" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label7" runat="server" Text="تاريخ التعيين بالجامعة:" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblHDate" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label8" runat="server" Text="عنوان الورشة التدريبية" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblCourseTitle" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td style="padding:5px">
                        <asp:Label ID="Label9" runat="server" Text="اسم المدرب:" CssClass="auto-style4"></asp:Label></td>
                    <td colspan="3" style="padding:5px">
                        <asp:Label ID="lblTrainerName" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>

    </div>
    <div style="margin:30px">
        <table style="border:1px solid black;border-spacing:0;width:100%" border="1" >
            <tr>
            <th style="width:2%">
               # 
            </th>
                <th style="width:38%">
                    فقرات التقييم

                </th>
            
                <th style="width:10%">
            
            </th>
            </tr>
            <tr>
                <td style="text-align:center">1</td>
                <td>محتوى البرنامج التدريبي (النظري والتطبيقي).</td>
                <td>
                    <asp:radiobuttonlist ID="rd1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd1" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">2</td>
                <td>سلامة وسلاسة اللغة التي كتبت بها المادة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd2" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd2" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">3</td>
                <td>تنظيم وعرض محتوى المادة العلمية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd3" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd3" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">4</td>
                <td>إدارة الورشة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd4" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd4" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">5</td>
                <td>وضوح أهداف الورشة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd5" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd5" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">6</td>
                <td>مستوى المادة العلمية المقدمة.</td>
                <td>
                    <asp:radiobuttonlist ID="rd6" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd6" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">7</td>
                <td>مدة ورشة العمل.</td>
                <td>
                    <asp:radiobuttonlist ID="rd7" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd7" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">8</td>
                <td>وقت انعقاد ورشة العمل.</td>
                <td>
                    <asp:radiobuttonlist ID="rd8" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd8" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">9</td>
                <td>مدى ارتباط محتوى ورشة العمل بأعمالنا.</td>
                <td>
                    <asp:radiobuttonlist ID="rd9" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd9" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">10</td>
                <td>توافر الأمثلة العملية والمهنية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd10" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd10" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">11</td>
                <td>سهولة فهم المادة ومتابعتها.</td>
                <td>
                    <asp:radiobuttonlist ID="rd11" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd11" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">12</td>
                <td>مكان انعقاد الورشة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd12" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd12" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">13</td>
                <td>تنوع الأنشطة المقدمة.</td>
                <td>
                    <asp:radiobuttonlist ID="rd13" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd13" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">14</td>
                <td>مناسبة الموضوعات لمستويات المتدربين.</td>
                <td>
                    <asp:radiobuttonlist ID="rd14" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd14" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">15</td>
                <td>حداثة المادة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd15" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd15" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">16</td>
                <td>جاذبية الإخراج العام للمادة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd16" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd16" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">17</td>
                <td>توافر التجهيزات والوسائل اللازمة لإنجاح الورشة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd17" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd17" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">18</td>
                <td>الخدمات المساندة (الضيافة، الاستراحة، ... الخ).</td>
                <td>
                    <asp:radiobuttonlist ID="rd18" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd18" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">19</td>
                <td>طريقة تقييم المتدربين في الورشة.</td>
                <td>
                    <asp:radiobuttonlist ID="rd19" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd19" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            <tr>
                <td style="text-align:center">20</td>
                <td>التقييم العام للورشة التدريبية.</td>
                <td>
                    <asp:radiobuttonlist ID="rd20" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                    </asp:radiobuttonlist>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" 
                        Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="rd20" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
            <div style="border:none">
                -
                ما هي عناصر القوة في الورشة التدريبية؟
                    <br />
                    <asp:Label ID="lblQ21" runat="server" CssClass="print-only"></asp:Label>
                
                    <asp:TextBox ID="txtQ21" runat="server" TextMode="MultiLine" Rows="5" Columns="25" CssClass="ddlShow"></asp:TextBox>
            </div>
            <div style="border:none;border-bottom:none">
                -
                -ما هي عناصر الضعف في الورشة التدريبية؟<br />
                    <asp:Label ID="lblQ22" runat="server" CssClass="print-only"></asp:Label>
                
                    <asp:TextBox ID="txtQ22" runat="server" TextMode="MultiLine" Rows="5" Columns="25" CssClass="ddlShow"></asp:TextBox>
            </div>
        <div class="print-only">
            <asp:Label ID="lblEvalC" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
        </div>
            <div style="text-align:center">
                    <asp:Button ID="Button1" runat="server" Width="200px" Height="50px" Text="ارسال التقييم" CssClass="btn" OnClick="Button1_Click"/>
            </div>
    </div>
    </div>
</asp:Content>
