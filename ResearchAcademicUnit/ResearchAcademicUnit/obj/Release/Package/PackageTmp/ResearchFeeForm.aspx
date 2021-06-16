<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResearchFeeForm.aspx.cs" Inherits="ResearchAcademicUnit.ResearchFeeForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <style>
        td {
            padding: 5px;
        }

        .progress {
            /*display: flex;*/
            /*height: 10rem;*/
            overflow: hidden;
            position: fixed;
            z-index: 100;
            width: 100%;
            height: 100%;
            background-color: black;
            opacity: 0.5;
            font-size: .75rem;
            border-radius: .25rem;
            top: 0;
            left: 0;
        }
        /*.progress img{
        position:relative;
        top:50%;
        left:50%;
    }*/

        /*.h {
            display: block;
        }*/

        @media print {
            input[type=text] {
                border: none;
                background-color: transparent;
                padding: 0px;
                font-size: 26px;
                word-wrap: break-word;
            }

                input[type=text].h {
                    display: none;
                }

            td.h {
                display: none;
            }

            div.h {
                display: none;
            }

            body {
                font-size: 20px;
                background-color: white;
            }

            .header {
                position: fixed;
                top: 0px;
                width: 100%;
            }

            .footer {
                position: fixed;
                bottom: 0px;
                width: 100%;
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

        .auto-style1 {
            width: 119px;
        }

        .auto-style2 {
            width: 158px;
        }

        .auto-style3 {
            width: 229px;
        }
    </style>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blanck";
        }
    </script>
    <script language="javascript">
        function printcoverdiv(printpage) {
            var headstr = "<html><head><title></title></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>
     <script language="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        $(document).ready(function() {
        window.history.pushState(null, "", window.location.href);        
        window.onpopstate = function() {
            window.history.pushState(null, "", window.location.href);
        };
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: white; width: 100%; height: 100%; position: absolute; top: 0; left: 0; z-index: -1" class="print-only"></div>
    <div class="header print-only" style="text-align: center">
        <img src="images/meu.png" style="width: 560px" />
        <div style="font-family: 'Khalid Art'; font-size: 26px">
            عمادة الدراسات العليا والبحث العلمي
        </div>
        <div style="font-family: 'Tw Cen MT'; font-size: 20px">
            Deanship of Graduate Studies & Scientific Research
        </div>
        <hr />
    </div>
    <%--<asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>--%>


    <div style="position: fixed; top: 50%; left: 20%; background-color: green; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="msgDiv" visible="false">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
                <%--<asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>--%>
                <asp:Button ID="btnOk" runat="server" Text="عودة" OnClick="btnOk_Click" CssClass="btn" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%--        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUpload" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lnkDownLoad" />
            <asp:AsyncPostBackTrigger ControlID="btnDirDecision" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnDeanDecision" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReDirector" EventName="Click"/>
            <asp:AsyncPostBackTrigger ControlID="btnReDean" EventName="Click" />
            
        </Triggers>
    </asp:UpdatePanel>--%>
    <div class="showdiv" id="reqid" runat="server"></div>
    <div style="margin: 150px auto; padding: 2%">
        <div class="TitleDiv">
            نموذج طلب دعم رسوم نشر بحث علمي
        </div>
        <div>
            <p style="background-color: #7f7f7f; color: white">معلومات الباحث الرئيسي</p>
            <table border="1" style="width: 100%; border-style: inset; border-spacing: 0">
                <tr>
                    <td>اسم الباحث<asp:HiddenField ID="hfResearchId" runat="server" />
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblRName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>الكلية<asp:Label ID="lblSaderNo" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFaculty" runat="server"></asp:Label>
                    </td>
                    <td>القسم</td>
                    <td>
                        <asp:Label ID="lblDept" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>الرتبة الأكاديمية</td>
                    <td>
                        <asp:Label ID="lblDegree" runat="server"></asp:Label></td>
                    <td>الرقم الوظيفي</td>
                    <td>
                        <asp:Label ID="lblJobId" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>البريد الإلكتروني (لغايات متابعة حالة الطلب)<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="خطأ بالبريد الالكتروني" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtEmail" runat="server" Style="width: 100%;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>الرقم الوطني البحثي
                        <br />
                        <a href="http://resn.hcst.gov.jo" target="_blank">http://resn.hcst.gov.jo</a>
                        (حقل اجباري)
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtHcst" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtHcst" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>الرقم العالمي - ORCID ID
                        <br />
                        <a href="http://orcid.org" target="_blank">http://orcid.org</a>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtOrcid" runat="server" Style="width: 100%;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>الرقم العالمي - Researcher ID
                        <br />
                        <a href="https://www.researcherid.com" target="_blank">www.researcherid.com</a>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtResearcherId" runat="server" Style="width: 100%;"></asp:TextBox>
                </tr>
                <tr>
                    <td>الرابط الإلكتروني للباحث على موقع 
                        <a href="https://scholar.google.com" target="_blank">Google Scholar</a>
                        (حقل اجباري)
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtGSLink" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtGSLink" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>الرابط الإلكتروني للباحث على بوابة البحث
                        (Research Gate)<br />
                        من خلال موقع 
                        <a href="https://www.researchgate.net" target="_blank">www.researchgate.net</a>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtRGLink" runat="server" Style="width: 100%;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <p style="background-color: #7f7f7f; color: white">معلومات البحث</p>
            <table border="1" style="width: 100%; border-style: inset; border-spacing: 0">
                <tr>
                    <td style="width: 200px">عنوان البحث</td>
                    <td>
                        <asp:TextBox ID="txtRTitle" runat="server" Style="width: 100%;" class="h"></asp:TextBox>
                        <div id="rtDiv" runat="server" class="print-only" style="text-align: center"></div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtRTitle" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <div id="rTitleDiv" runat="server" class="print-only"></div>
                    </td>
                </tr>
                <tr>
                    <td>تاريخ القبول</td>
                    <td>
                        <asp:TextBox ID="txtAcceptDate" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtAcceptDate" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="h">أسماء المشاركين - حسب ورودها في البحث</td>
                    <td colspan="3" class="h">
                        <asp:TextBox ID="txtPartReName" runat="server" autpcomplete="off" Style="width: 80%;"></asp:TextBox>
                        <div class="h" style="float: left">
                            <asp:LinkButton ID="lnkAddPartR" runat="server" OnClick="lnkAddPartR_Click" ValidationGroup="add" Width="15%">إضافة <i class="material-icons h">add</i></asp:LinkButton>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ValidationGroup="add" ControlToValidate="txtPartReName"></asp:RequiredFieldValidator>

                    </td>
                </tr>
            </table>
            <table style="width: 100%; border-style: inset; border-spacing: 0">
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="grd" Caption="أسماء المشاركين (حسب ورودها بالبحث)">
                            <Columns>
                                <asp:BoundField HeaderText="#" DataField="Serial" />
                                <asp:BoundField HeaderText="الاسم" DataField="ReName" />
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء" HeaderStyle-CssClass="h" ControlStyle-CssClass="h" ItemStyle-CssClass="h">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <p style="background-color: #7f7f7f; color: white">معلومات المجلة</p>
            <table border="1" style="width: 100%; border-style: inset; border-spacing: 0">
                <tr>
                    <td class="auto-style1">اسم المجلة</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtMagName" runat="server" Style="width: 100%;"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtMagName" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">رقم ISSN للمجلة</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtMagISSN" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtMagISSN" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style2">اسم الناشر</td>
                    <td>
                        <asp:TextBox ID="txtPubName" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPubName" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">سنة تأسيس المجلة
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtMagYear" runat="server" Style="width: 100%;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtMagYear" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>

                    <td class="auto-style2">مفهرسة في قواعد البيانات</td>

                    <td>
                        <asp:TextBox ID="txtDBIndex" runat="server" Style="width: 100%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtDBIndex" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>

                </tr>
            </table>
        </div>
        <div style="margin: 2% auto">
            <table border="1" style="width: 100%; border-style: inset; border-spacing: 0">
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="قيمة الدعم المطلوب"></asp:Label></td>
                    <td>

                        <asp:TextBox ID="txtFeeValue" runat="server" Style="width: 30%;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtFeeValue" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFeeValue" ErrorMessage="إدخال ارقام فقط" ValidationExpression="^\d+\.?\d*$" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="ChosenSelector" Style="min-width: 150px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlCurrency" InitialValue="0" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div style="font-family: 'Khalid Art'; page-break-before: always; margin: 200px auto; font-size: 30px" class="print-only">

            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblReqName" runat="server" Text=""></asp:Label></td>
                    <td colspan="2">
                        <asp:Label ID="lblDirName" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblReqNameSig" runat="server" Text=""></asp:Label></td>
                    <td>
                        <asp:Label ID="lblDirNameSig" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblReqDate" runat="server" Text=""></asp:Label></td>
                    <td style="text-align: left">
                        <asp:Label ID="lblDirDate" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

            <p style="background-color: #7f7f7f; color: white; margin: 0">تنسيب عميد الكلية</p>
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div id="deanDecDiv" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="deanDecDivSig" runat="server"></div>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblDeanDecDate" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

            <p style="background-color: #7f7f7f; color: white; margin: 0">رئيس قسم البحث العلمي</p>

            التدقيق<br />
            البحث مرفق
                        - البحث يتضمن اسم الجامعة
                        - البحث يتضمن رسالة الشكر
                        - ورقة فهرسة المجلة ضمن قاعدة بيانات Scopus 
                        - إيصال دفع الرسوم
                        - ورقة قبول البحث في المجلة

            التوصية<br />
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div id="RDirDecDiv" runat="server"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="RDirDecDivSig" runat="server"></div>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

            <p style="background-color: #7f7f7f; color: white; margin: 0">توصية عميد الدراسات العليا والبحث العلمي</p>
            <div id="RdeanDecDiv" runat="server"></div>
            <div id="RdeanDecDivSig" runat="server"></div>
            <div style="text-align: left">
                <asp:Label ID="lblRDeanDecDate" runat="server" Text=""></asp:Label>
            </div>
            <p style="background-color: #7f7f7f; color: white; margin: 0">قرار الأستاذ الدكتور رئيس الجامعة</p>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        أوافق على دعم نشر البحث بقيمة (.......................)</td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox2" runat="server" />
                        لا أوافق</td>
                </tr>
                <tr>
                    <td>التوقيع.............................</td>
                    <td style="text-align: left">التاريخ.............................</td>
                </tr>
            </table>
        </div>

        <div class="footer print-only">
            <hr />
            <div style="float: left; width: 30%">
                <img src="images/newfooter.png" width="450px" />
            </div>
            <div style="float: left; text-align: left">
                <div>F176, Rev. c</div>
                <div>Ref.: Deans' Council Session (16/2017-2018), Decision No. 04</div>
                <div>Date: 23/12/2017</div>
            </div>
        </div>


        <div id="UploadDiv" runat="server" style="margin: 2% auto" class="h">
            <p style="background-color: #7f7f7f; color: white">
                الوثائق المطلوبة تحميلها
                    <asp:LinkButton ID="lnkDownLoad" runat="server" OnClick="lnkDownLoad_Click"><i class="material-icons">file_download</i></asp:LinkButton>
            </p>
            <table border="1" style="width: 100%; border-style: inset; border-spacing: 0; text-align: right">
                <tr>
                    <td>
                        <asp:Label ID="lblff1" Visible="false" runat="server" Text="ورقة فهرسة المجلة ضمن قواعد البيانات سكوبس"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
                        <asp:RequiredFieldValidator ID="reqFile1" Visible="false" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="FileUpload1" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ErrorMessage="ملفات PDF فقط" ForeColor="Red" ValidationExpression="^.*\.(pdf|PDF)$" ControlToValidate="FileUpload1"></asp:RegularExpressionValidator>
                        <asp:LinkButton ID="lnkViewF1" runat="server" OnCommand="lnkViewF1_Command" CommandArgument="F1" OnClientClick="window.document.forms[0].target='_blank';" Visible="False">عرض</asp:LinkButton>
                        <a href="#" runat="server" id="A1" visible="false">عرض</a>
                        <asp:Label ID="lblf1" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>نسخة من البحث موضح به اسم الجامعة ورسالة الشكر الخاصة بدعم رسوم نشر بحث علمي</td>
                    <td>
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="FileUpload2" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <%--                                <asp:RadioButtonList ID="rdReDir" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">نعم</asp:ListItem>
                                        <asp:ListItem Value="2">لا</asp:ListItem>
                                        <asp:ListItem Value="3">معلق</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ControlToValidate="rdReDir" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator></td>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic" ErrorMessage="ملفات PDF فقط" ForeColor="Red" ValidationExpression="^.*\.(pdf|PDF)$" ControlToValidate="FileUpload2"></asp:RegularExpressionValidator>
                        <asp:LinkButton ID="lnkViewF2" runat="server" OnCommand="lnkViewF1_Command" CommandArgument="F2" OnClientClick="SetTarget()" Visible="False">عرض</asp:LinkButton>
                        <a href="#" runat="server" id="A2" visible="false" target="_blank">عرض</a>
                        <asp:Label ID="lblf2" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>ورقة قبول البحث في المجلة</td>
                    <td>
                        <asp:FileUpload ID="FileUpload3" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="FileUpload3" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic" ErrorMessage="ملفات PDF فقط" ForeColor="Red" ValidationExpression="^.*\.(pdf|PDF)$" ControlToValidate="FileUpload3"></asp:RegularExpressionValidator>
                        <asp:LinkButton ID="lnkViewF3" runat="server" OnCommand="lnkViewF1_Command" CommandArgument="F3" OnClientClick="SetTarget()" Visible="False">عرض</asp:LinkButton>
                        <a href="#" runat="server" id="A3" visible="false" target="_blank">عرض</a>
                        <asp:Label ID="lblf3" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>إيصال قيمة رسوم النشر المدفوعة</td>
                    <td>
                        <asp:FileUpload ID="FileUpload4" runat="server" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="FileUpload4" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ErrorMessage="ملفات PDF فقط" ForeColor="Red" ValidationExpression="^.*\.(pdf|PDF)$" ControlToValidate="FileUpload4"></asp:RegularExpressionValidator>
                        <asp:LinkButton ID="lnkViewF4" runat="server" OnCommand="lnkViewF1_Command" CommandArgument="F4" OnClientClick="SetTarget()" Visible="False">عرض</asp:LinkButton>
                        <a href="#" runat="server" id="A4" visible="false" target="_blank">عرض</a>
                        <asp:Label ID="lblf4" runat="server" Visible="false"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>فاتورة استلام المجلة لرسوم النشر</td>
                    <td>
                        <asp:FileUpload ID="FileUpload5" runat="server" ClientIDMode="Static" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Medium" ControlToValidate="FileUpload5" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic" ErrorMessage="ملفات PDF فقط" ForeColor="Red" ValidationExpression="^.*\.(pdf|PDF)$" ControlToValidate="FileUpload5"></asp:RegularExpressionValidator>
                        <asp:LinkButton ID="lnkViewF5" runat="server" OnCommand="lnkViewF1_Command" CommandArgument="F5" OnClientClick="SetTarget()" Visible="False">عرض</asp:LinkButton>
                        <a href="#" runat="server" id="A5" visible="false" target="_blank">عرض</a>
                        <asp:Label ID="lblf5" runat="server" Visible="false"></asp:Label>

                    </td>
                </tr>
            </table>
        </div>
        <div class="h">
            <asp:UpdatePanel ID="DataPanel" runat="server">
                <ContentTemplate>
                    <div style="z-index: 99; width: 100%; height: 100%; position: fixed; top: 0; left: 0" id="Div1" runat="server" visible="false">
                        <div style="text-align: center; width: 50%; height: 200px; background-color: #f5f5f5; color: black; border: 3px outset darkred; z-index: 99; font-size: large; opacity: 1; position: fixed; top: 40%; left: 20%">
                            <asp:Label ID="Label1" runat="server" Text="تم الارسال بنجاح"></asp:Label><br />
                            <br />
                            <asp:Button ID="btnDone" Style="border-radius: 10px; border: 3px outset" runat="server" Text="رجوع" OnClick="btnDone_Click" CssClass="btn" CausesValidation="false" />
                        </div>
                    </div>
                    <div style="z-index: 99; width: 100%; height: 100%; position: fixed; top: 0; left: 0" id="ConfirmDiv" runat="server" visible="false">
                        <div style="text-align: center; width: 50%; height: 200px; background-color: #f5f5f5; color: black; border: 3px outset darkred; z-index: 99; font-size: large; opacity: 1; position: fixed; top: 40%; left: 20%">
                            <asp:Label ID="Label91" runat="server" Text="هل أنت متأكد من ارسال الطلب؟" Font-Size="X-Large"></asp:Label><br />
                            <br />
                            <asp:Button ID="btnConfirm" runat="server" Text="نعم" OnClick="btnConfirm_Click" Style="border-radius: 10px; border: 3px outset" CssClass="btn" CausesValidation="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="إلغاء" OnClick="btnCancel_Click" Style="border-radius: 10px; border: 3px outset" CssClass="btn" CausesValidation="false" />
                        </div>
                    </div>

                    <div style="margin: 2% auto; text-align: center">
                        <asp:Button ID="btnUpload" runat="server" CssClass="btn" OnClick="btnUpload_Click" Text="تخزين" Width="200px" Height="40px" />
                        <asp:Button ID="btnSubmit" OnClientClick="window.document.forms[0].target='';" runat="server" Text="ارسال الطلب" OnClick="btnSubmit_Click" CssClass="btn" Width="150px" Height="40px" ValidationGroup="FinalSubmit" />
                    </div>




                    <div id="DirectorDiv" runat="server" visible="false" style="width: 48%; float: right; display: inline; margin: 1%">
                        <p style="background-color: #7f7f7f; color: white">تنسيب رئيس القسم</p>
                        <table>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDirNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="40" placeholder="ملاحظات رئيس القسم"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtDirNotes" ForeColor="Red" Font-Size="Medium" ValidationGroup="director" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>

                        <asp:Button ID="btnDirDecisionOk" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من تحويل الطلب للعميد؟')" runat="server" Text="ارسال" CssClass="btn" Width="150px" Height="40px" OnClick="btnDirDecision_Click" ValidationGroup="director" />

                        <asp:Button ID="btnDirDecisionNo" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من إغلاق المعاملة؟')" runat="server" Text="يعاد" CssClass="btn" Width="150px" Height="40px" OnClick="btnDirDecision_Click" ValidationGroup="director" />

                        <asp:Button ID="btnDirDecisionPost" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من إعادة المعاملة للباحث؟')" runat="server" Text="تعليق" CssClass="btn" Width="150px" Height="40px" OnClick="btnDirDecision_Click" ValidationGroup="director" />
                    </div>

                    <div id="DeanDiv" runat="server" visible="false" style="width: 48%; float: right; display: inline; margin: 1%">
                        <p style="background-color: #7f7f7f; color: white">تنسيب عميد الكلية</p>
                        <table>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDeanNotes" runat="server" TextMode="MultiLine" Rows="5" Columns="40" placeholder="ملاحظات عميد الكلية"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtDeanNotes" ForeColor="Red" Font-Size="Medium" ValidationGroup="dean" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                            </tr>
                        </table>

                        <asp:Button ID="btnDeanDecisionOK" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من الارسال؟')" runat="server" Text="ارسال" CssClass="btn" Width="150px" Height="40px" OnClick="btnDeanDecision_Click" ValidationGroup="dean" />

                        <asp:Button ID="btnDeanDecisionNo" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من الارسال؟')" runat="server" Text="يعاد" CssClass="btn" Width="150px" Height="40px" OnClick="btnDeanDecision_Click" ValidationGroup="dean" />

                        <asp:Button ID="btnDeanDecisionPost" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من الارسال؟')" runat="server" Text="تعليق" CssClass="btn" Width="150px" Height="40px" OnClick="btnDeanDecision_Click" ValidationGroup="dean" />

                    </div>

                    <div id="ReDirectorDiv" runat="server" visible="false" style="width: 48%; float: right; display: inline; margin: 1%">
                        <p style="background-color: #7f7f7f; color: white">التحقق من بيانات بحث علمي - رئيس قسم البحث العلمي</p>
                        <table>
                            <tr>
                                <td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtyearmonth" runat="server" placeholder="سنة وشهر القبول"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" ControlToValidate="txtyearmonth" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                            <td>
                                                <asp:TextBox ID="txtFeeV" runat="server" placeholder="قيمة رسوم المجلة"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" ControlToValidate="txtFeeV" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBoxList ID="chkFiles" runat="server">
                                                    <asp:ListItem Value="1">البحث مرفق</asp:ListItem>
                                                    <asp:ListItem Value="2">البحث يتضمن اسم الجامعة</asp:ListItem>
                                                    <asp:ListItem Value="3">البحث يتضمن رسالة الشكر</asp:ListItem>
                                                    <asp:ListItem Value="4">ورقة فهرسة المجلة ضمن قاعدة بيانات Scopus</asp:ListItem>
                                                    <asp:ListItem Value="5">إيصال دفع الرسوم</asp:ListItem>
                                                    <asp:ListItem Value="6">ورقة قبول البحث في المجلة </asp:ListItem>
                                                    <asp:ListItem Value="7">فاتورة استلام المجلة لرسوم النشر </asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>فهرسة سكوبس
                                                </td>
                                            </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rdCheck" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdCheck_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">ورقة بحثية</asp:ListItem>
                                                    <asp:ListItem Value="2">بحث منشور في مؤتمر</asp:ListItem>
                                                    <asp:ListItem Value="3">فصل في كتاب</asp:ListItem>
                                                    <asp:ListItem Value="4">مراجعة</asp:ListItem>
                                                    <asp:ListItem Value="5">مسح قصير</asp:ListItem>
                                                    <asp:ListItem Value="6">تصحيح</asp:ListItem>
                                                    <asp:ListItem Value="7">مقدمة</asp:ListItem>
                                                    <asp:ListItem Value="8">رسالة</asp:ListItem>
                                                    <asp:ListItem Value="9">ملحوظة</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" ControlToValidate="rdCheck" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="vertical-align:top">
                                                <asp:RadioButtonList ID="rdQuarter" runat="server" Visible="false">
                                                    <asp:ListItem Value="1">Q1</asp:ListItem>
                                                    <asp:ListItem Value="2">Q2</asp:ListItem>
                                                    <asp:ListItem Value="3">Q3</asp:ListItem>
                                                    <asp:ListItem Value="4">Q4</asp:ListItem>
                                                    <asp:ListItem Value="5">None Q's</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*" ControlToValidate="rdQuarter" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>فهرسة Calrivate Analytics
                                                <asp:CheckBoxList ID="chkClarivate" runat="server">
                                                    <asp:ListItem Value="1">Arts and Humanities Citation Index (AHCI)</asp:ListItem>
                                                    <asp:ListItem Value="2">Science Citation Index Expanded(SCIE)</asp:ListItem>
                                                    <asp:ListItem Value="3">Social Sciences Citation Index (SSCI)</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtReDirector" runat="server" TextMode="MultiLine" Rows="5" Columns="40" placeholder="توصية رئيس قسم البحث العلمي"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" ControlToValidate="txtReDirector" ForeColor="Red" Font-Size="Medium" ValidationGroup="redir" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtReDirector" Display="Dynamic" ErrorMessage="*" Font-Size="Medium" ForeColor="Red" ValidationGroup="redir1"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <div id="ReDirSigDiv" runat="server" visible="false">
                            <p>رئيس قسم البحث العلمي : أشرف الطراونة</p>
                            <p>
                                <img src="images/tarawneh_sig.jpeg" width="128px" />
                            </p>
                            <p>
                                <asp:Label ID="lblReDirDate" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <asp:Button ID="btnReDirectorOk" runat="server" Text="ارسال" CssClass="btn" Width="150px" Height="40px" OnClick="btnReDirector_Click" ValidationGroup="redir" />
                        <asp:Button ID="btnReDirectorNo" OnClientClick="window.document.forms[0].target='';" runat="server" Text="يعاد" CssClass="btn" Width="150px" Height="40px" OnClick="btnReDirector_Click" ValidationGroup="redir1" />
                        <asp:Button ID="btnReDirectorPost" OnClientClick="window.document.forms[0].target='';" runat="server" Text="تعليق" CssClass="btn" Width="150px" Height="40px" OnClick="btnReDirector_Click" ValidationGroup="redir1" />
                    </div>

                    <div id="ReDeanDiv" runat="server" visible="false" style="width: 48%; float: right; display: inline; margin: 1%">
                        <p style="background-color: #7f7f7f; color: white">توصية عميد الدراسات العليا والبحث العلمي</p>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdReDeanDecision1" runat="server" GroupName="DeanDecision" OnCheckedChanged="rdReDeanDecision1_CheckedChanged" Text="التوصية بالموافقة" Checked="True" />
                                </td>

                                <td>&nbsp;<asp:TextBox ID="txtReDeanValue" runat="server" placeholder="القيمة" Width="100px" onkeypress="return isNumberKey(event)" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txtReDeanValue" ForeColor="Red" Font-Size="Medium" ValidationGroup="redean" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCurValue" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlCurValue_SelectedIndexChanged">
                                        <asp:ListItem Value="1">دينار أردني</asp:ListItem>
                                        <asp:ListItem Value="2">دولار أمريكي</asp:ListItem>
                                        <asp:ListItem Value="3">يورو أوروبي</asp:ListItem>
                                        <asp:ListItem Value="4">أخرى</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCurOther" runat="server" Visible="false"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtReDeanYear" runat="server" placeholder="العام الجامعي"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="txtReDeanYear" ForeColor="Red" Font-Size="Medium" ValidationGroup="redean" Display="Dynamic" Visible="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdReDeanDecision2" runat="server" GroupName="DeanDecision" OnCheckedChanged="rdReDeanDecision2_CheckedChanged" Text="عدم الموافقة" /></td>
                                <td>
                                    <asp:Label ID="lblReDeanDec2" runat="server" Text="التوصية بعدم الموافقة على دعم رسوم نشر كون المجلة غير مفهرسة بقاعدة بيانات Scopus" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdReDeanDecision3" runat="server" GroupName="DeanDecision" OnCheckedChanged="rdReDeanDecision3_CheckedChanged" Text="رئيس قسم البحث العلمي لمراجعة" /></td>
                                <td>
                                    <asp:TextBox ID="txtReDean" runat="server" TextMode="MultiLine" Rows="5" Columns="40" placeholder="ملاحظات عميد الدراسات العليا والبحث العلمي"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ControlToValidate="txtReDean" ForeColor="Red" Font-Size="Medium" ValidationGroup="redean" Display="Dynamic" Visible="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div id="Div2" runat="server">
                                        <p>عميد الدراسات العليا والبحث العلمي : أ.د. سلام خالد المحادين</p>
                                        <p>
                                            <%--<img src="signature/1357.jpg" width="128px" />--%>
                                        </p>
                                        <p>
                                            <asp:Label ID="lblReDeanDate" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnReDeanOk" OnClientClick="window.document.forms[0].target='';return confirm('هل أنت متأكد من الارسال؟')" runat="server" Text="ارسال" CssClass="btn" Width="150px" Height="40px" OnClick="btnReDean_Click" ValidationGroup="redean" />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:PostBackTrigger ControlID="btnUpload" />
                    <asp:PostBackTrigger ControlID="btnReDirectorOk" />
                    <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDirDecisionOk" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeanDecisionOK" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnReDeanOk" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdateProgress ID="prog1" runat="server" AssociatedUpdatePanelID="DataPanel">
        <ProgressTemplate>
            <div style="position: fixed; top: 0; left: 0; background-color: black; opacity: 0.5; z-index: 100; width: 100%; height: 100%">
                <img src="images/loading.gif" width="256px" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "20%" });
        }
    </script>
</asp:Content>
