<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="NewResearch.aspx.cs" Inherits="ResearchAcademicUnit.NewResearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <link href="css/materialdesignicons.min.css" rel="stylesheet" />
    <%--<link href="css/mono.css" rel="stylesheet" />--%>
    <style>
        table {
            width: 100%;
            border-spacing: 5px 5px;
        }

            table input[type=text] {
                width: 100%;
                display: inline;
                box-sizing: border-box;
            }

        .validcss {
            color: red;
            font-size: large;
            font-weight: bold;
        }

        .auto-style1 {
            font-size: xx-large;
        }

        .auto-style2 {
            font-size: x-large;
        }

        .td {
            width: 16.67%;
            /*text-align: center;*/
        }

            .td span {
                background-color: #921a1d !important;
                color: white;
                /*text-align: right;*/
                /*float:right;*/
                margin: auto;
                padding: 10px 30px 10px 30px;
                border-top-left-radius: 15px;
                border-top-right-radius: 15px;
                font-size: large;
            }

        .divNewStyle {
            border: 5px outset #921a1d;
        }

        .auto-style3 {
            height: 159px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TitleDiv">
        إضافة بحث
    </div>
    <asp:UpdateProgress ID="prog1" runat="server" AssociatedUpdatePanelID="upAbstract">
        <ProgressTemplate>
            <div style="position: fixed; top: 0; left: 0; background-color: black; opacity: 0.5; z-index: 100; width: 100%; height: 100%">
                <img src="images/loading.gif" style="position: fixed; top: 30%; left: 45%; width: 256px" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div style="margin: 0px auto; padding: 2%;">
        <asp:UpdatePanel ID="upAbstract" runat="server">
            <ContentTemplate>
                <div id="UpdateDiv" class="headerDiv">
                    <table style="width: 100%; border-collapse: collapse; border-spacing: 0">
                        <tr>
                            <td class="td"><span>1</span></td>
                            <td class="td"><span>2</span></td>
                            <td class="td"><span>3</span></td>
                            <td class="td"><span>4</span></td>
                            <td class="td"><span>5</span></td>
                            <td class="td"><span><i class="material-icons">search</i></span></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkUpload" runat="server" OnClick="lnkUpload_Click" CssClass="lnk" CausesValidation="false">
                                تحميل البيانات من سكوبس
                                </asp:LinkButton>
                            </td>
                            <td class="td">
                                <asp:LinkButton ID="lnkMainUpdate" runat="server" OnClick="lnkMainUpdate_Click" CssClass="lnk" CausesValidation="false">
                                مقبول للنشر إلى منشور
                                </asp:LinkButton>
                            </td>
                            <td class="td">
                                <asp:LinkButton ID="lnkNotExistData" runat="server" OnClick="lnkNotExistData_Click" CssClass="lnk" CausesValidation="false">
                                الابحاث الجديدة على سكوبس
                                </asp:LinkButton>
                            </td>
                            <td class="td">
                                <asp:LinkButton ID="lnkSupport" runat="server" OnClick="lnkSupport_Click" CssClass="lnk" CausesValidation="false">الدعم</asp:LinkButton>
                                <%--<asp:Button ID="btnSupportAward" runat="server" Text="الدعم والمكافأة" CssClass="lnk" CausesValidation="false" OnClick="btnSupportAward_Click" />--%>
                            </td>
                            <td class="td">
                                <asp:LinkButton ID="lnkReward" runat="server" OnClick="lnkReward_Click" CssClass="lnk" CausesValidation="false">المكافأة</asp:LinkButton>
                            </td>
                            <td class="td">
                                <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="lnk" CausesValidation="false">استعلام عن الابحاث</asp:LinkButton>

                            </td>

                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <div id="searchDiv" runat="server" visible="false">
                                    <asp:TextBox ID="txtSearch" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:LinkButton ID="lnkClearSearch" runat="server" ToolTip="مسح" OnClick="lnkClearSearch_Click" CausesValidation="false"><span><i class="material-icons">clear</i></span></asp:LinkButton>
                                    <asp:LinkButton ID="lnkMainSearch" runat="server" ToolTip="بحث" OnClick="lnkMainSearch_Click" CausesValidation="false"><span><i class="material-icons">search</i></span></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div runat="server" id="uploadDiv" class="divNewStyle" visible="false">
                                    <div style="padding: 10px; box-sizing: border-box; width: 50%">
                                        <div style="margin-bottom: 20px">
                                            <div class="lblDiv">معلومات الأبحاث</div>
                                            <div style="clear: both"></div>
                                        </div>
                                        <div>
                                            <div class="lblDiv" style="width: 25%">
                                                <asp:Label ID="lbl" runat="server" Text="تحديد الملف المراد تحميله"></asp:Label>
                                            </div>
                                            <div class="lblContentDiv" style="width: 74%">
                                                <asp:RadioButtonList ID="rdType" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">ملف المجلات</asp:ListItem>
                                                    <asp:ListItem Value="2">ملف الابحاث</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="rdType" ErrorMessage="*" Display="Dynamic" Font-Size="Medium" ForeColor="#F7BA00"></asp:RequiredFieldValidator>
                                                <asp:FileUpload ID="FileUpload1" runat="server" Style="padding-right: 20px; width: 100%; box-sizing: border-box" Font-Bold="True" Font-Size="Medium" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="*" Display="Dynamic" Font-Size="Medium"
                                                    ForeColor="#F7BA00" ControlToValidate="FileUpload1" ValidationGroup="UploadI"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                                    ControlToValidate="FileUpload1" ValidationGroup="UploadI"
                                                    ErrorMessage="ملفات اكسل فقط"
                                                    ValidationExpression="(.*\.([Xx][Ll][Ss]|[Xx][Ll][Ss][Xx]|[Xx][Ll][Ss][Bb]|[Cc][Ss][Vv])$)" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                            <div style="clear: both"></div>
                                            <div style="box-sizing: border-box; margin-top: 10px; text-align: center">
                                                <asp:Button ID="btnUpload" ValidationGroup="UploadI"
                                                    Style="margin-left: 20px; margin-right: 20px" runat="server" Text="تحميل" OnClick="btnUpload_Click" CssClass="btn" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div runat="server" id="UpdateRStatusDiv" class="divNewStyle" visible="false">
                                    <asp:GridView ID="grdUpdateRStatus" runat="server" EmptyDataText="لا يوجد معلومات حاليا" AutoGenerateColumns="false" CssClass="grd">
                                        <Columns>
                                            <asp:BoundField HeaderText="رمز البحث" DataField="EID" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="عنوان البحث" DataField="RTitle" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdateStatus" CausesValidation="false" runat="server" OnClick="lnkUpdateStatus_Click" ToolTip="تعديل"><i class="material-icons" style="color: #E34724">border_color</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="auto-style3">
                                <div runat="server" id="NotExistDataDiv" class="divNewStyle">
                                    <asp:GridView ID="grdNotExistData" runat="server" EmptyDataText="لا يوجد معلومات حاليا" AutoGenerateColumns="false" CssClass="grd"
                                        OnRowDataBound="grdNotExistData_RowDataBound">
                                        <Columns>
                                            <asp:BoundField HeaderText="رمز البحث" DataField="EID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                                            <asp:BoundField HeaderText="عنوان البحث على سكوبس" DataField="RTitle" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" />
                                            <asp:BoundField HeaderText="رمز البحث" DataField="AutoidResearch" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                                            <asp:BoundField HeaderText="تاريخ القبول" DataField="AcceptedDate" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" />
                                            <%--<asp:BoundField HeaderText="عنوان البحث في قاعدة البيانات" DataField="ReTitle" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" />--%>
                                            <asp:TemplateField HeaderText="عنوان البحث في قاعدة البيانات"  ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("ReTitle") %>'></asp:Label><br />
                                                    <asp:LinkButton ID="lnkUpdatetitle" runat="server" ToolTip="تحديث عنوان البحث" OnClick="lnkUpdatetitle_Click" Visible="false" CausesValidation="false"><i class="material-icons" style="color: #E34724">border_color</i></asp:LinkButton>
                                                    <div runat="server" id="updateTitleDiv" visible="false">
                                                        <asp:TextBox ID="txtUpdateTitle" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                                        <asp:Button ID="btnUpdateTitle" runat="server" Text="تخزين" OnClick="btnUpdateTitle_Click" CssClass="btn"/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="4%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkShowInfo" CausesValidation="false" Visible="false" runat="server" OnClick="lnkShowInfo_Click" ToolTip="تعديل"><i class="material-icons" style="color: #E34724">border_color</i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkAdd" CausesValidation="false" Visible="false" runat="server" OnClick="lnkAdd_Click" ToolTip="إضافة"><i class="material-icons" style="color: #E34724">add</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div runat="server" id="AllResearchDiv" class="divNewStyle" visible="false">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn" Text="تنزيل" OnClick="Button1_Click" Visible="false" CausesValidation="false" />
                                    <%--<asp:Button ID="btnExport" runat="server" CssClass="btn" OnClick="btnExport_Click" Text="عرض" CausesValidation="false" />--%>
                                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="لا يوجد معلومات حاليا"
                                        Caption="معلومات الابحاث" AutoGenerateColumns="false" CssClass="grd">
                                        <Columns>
                                            <asp:BoundField HeaderText="رمز الباحث" DataField="ReId" />
                                            <asp:BoundField HeaderText="عنوان البحث" DataField="ReTitle" />
                                            <asp:BoundField HeaderText="ملخص البحث" DataField="ReAbstract" />
                                            <asp:TemplateField HeaderText="ملخص البحث">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbstract" runat="server" Text='<%# Eval("ReAbstract1").ToString() %>'></asp:Label>
                                                    <asp:LinkButton ID="lnkMore" runat="server" OnClick="lnkMore_Click" CausesValidation="false">more</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="نوع النشاط" DataField="ReType" />
                                            <asp:BoundField HeaderText="مستوى النشاط" DataField="ReLevel" />
                                            <asp:BoundField HeaderText="السنة" DataField="ReYear" />
                                            <asp:BoundField HeaderText="الشهر" DataField="ReMonth" />
                                            <asp:BoundField HeaderText="حالة البحث" DataField="ReStatus" />
                                            <asp:BoundField HeaderText="استشهادات البحث" DataField="ReCitation" />
                                            <asp:BoundField HeaderText="التشاركية" DataField="ReParticipate" />
                                            <asp:BoundField HeaderText="حالة الفريق البحثي" DataField="TeamType" />
                                            <asp:BoundField HeaderText="الباحثين مع الجامعات" DataField="Aff_Auther" />
                                            <asp:BoundField HeaderText="قطاعات البحث ومجالاته" DataField="ReSector" />
                                            <asp:BoundField HeaderText="المجال البحثي" DataField="ReField" />
                                            <asp:BoundField HeaderText="عنوان المجلة" DataField="Magazine" />
                                            <asp:BoundField HeaderText="ISSN" DataField="ISSN" />
                                            <asp:BoundField HeaderText="نوع المصدر البحثي" DataField="SourceType" />
                                            <asp:BoundField HeaderText="الناشر" DataField="Author" />
                                            <asp:BoundField HeaderText="تصنيف المجلة" DataField="MClass" />
                                            <asp:BoundField HeaderText="Top 10" DataField="TopMag" />
                                            <asp:BoundField HeaderText="معدل الاستشهاد" DataField="CitationAvg" />
                                            <asp:BoundField HeaderText="قطاع المجلة" DataField="MagVector" />
                                            <asp:BoundField HeaderText="مجال المجلة" DataField="MagField" />
                                            <asp:BoundField HeaderText="دعم داخلي" DataField="InSupport" />
                                            <asp:BoundField HeaderText="اسم المدعوم" DataField="" />
                                            <asp:BoundField HeaderText="مكافأة داخلية" DataField="Reward" />
                                            <asp:BoundField HeaderText="اسم صاحب المكافأة" DataField="" />
                                            <asp:BoundField HeaderText="دعم خارجي" DataField="OutSupport" />
                                            <asp:BoundField HeaderText="الكلي" DataField="TotalR" />
                                            <asp:BoundField HeaderText="الداخلي" DataField="TotalRIn" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="رمز الباحث" DataField="RID" />
                                                            <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" CausesValidation="false" runat="server" OnClick="lnkEdit_Click" ToolTip="تعديل"><i class="material-icons" style="color: #E34724">border_color</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="ResearchInfoDiv" class="headerDiv" runat="server" visible="false">
                    <table>
                        <tr>
                            <td colspan="6" class="auto-style1"><strong style="font-size: x-large">معلومات البحث
                            </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtReID"></asp:RequiredFieldValidator>رقم البحث على سكوبس EID</td>
                            <td>
                                <asp:TextBox ID="txtReID" runat="server" AutoPostBack="false" OnTextChanged="txtReID_TextChanged" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtEID" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>عنوان البحث</td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" Width="100%" AutoPostBack="false" OnTextChanged="txtTitle_TextChanged"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtAbstract"></asp:RequiredFieldValidator>ملخص البحث</td>
                            <td>
                                <asp:TextBox ID="txtAbstract" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlType" InitialValue="0"></asp:RequiredFieldValidator>
                                نوع النشاط البحثي</td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد نوع النشاط البحثي</asp:ListItem>
                                    <asp:ListItem Value="بحوث علمية">بحوث علمية</asp:ListItem>
                                    <asp:ListItem Value="مؤتمر علمي">مؤتمر علمي</asp:ListItem>
                                    <asp:ListItem Value="نشاط تأليفي">نشاط تأليفي</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlLevel" InitialValue="0"></asp:RequiredFieldValidator>
                                مستوى النشاط</td>
                            <td>
                                <asp:DropDownList ID="ddlLevel" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlStatus" InitialValue="0"></asp:RequiredFieldValidator>
                                حالة البحث
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد حالة البحث</asp:ListItem>
                                    <asp:ListItem Value="منشور">منشور</asp:ListItem>
                                    <asp:ListItem Value="مقبول للنشر">مقبول للنشر</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlYear" InitialValue="0"></asp:RequiredFieldValidator>
                                السنة</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlMonth" InitialValue="0"></asp:RequiredFieldValidator>
                                الشهر</td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtReCitation"></asp:RequiredFieldValidator>
                                استشهادات البحث
                            </td>
                            <td>
                                <asp:TextBox ID="txtReCitation" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlParticipate" InitialValue="0"></asp:RequiredFieldValidator>
                                التشاركية</td>
                            <td>
                                <asp:DropDownList ID="ddlParticipate" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد نوع التشاركية</asp:ListItem>
                                    <asp:ListItem Value="منفرد">منفرد</asp:ListItem>
                                    <asp:ListItem Value="مشترك">مشترك</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlTeamType" InitialValue="0"></asp:RequiredFieldValidator>
                                حالة الفريق البحثي</td>
                            <td>
                                <asp:DropDownList ID="ddlTeamType" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد حالة الفريق البحثي</asp:ListItem>
                                    <asp:ListItem Value="فردي">فردي</asp:ListItem>
                                    <asp:ListItem Value="دولي">دولي</asp:ListItem>
                                    <asp:ListItem Value="محلي">محلي</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtAffliation"></asp:RequiredFieldValidator>
                                الباحثين مع الجامعات</td>
                            <td>
                                <asp:TextBox ID="txtAffliation" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="auto-style1"><strong style="font-size: x-large">معلومات المصدر البحثي
                            </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlJurnalSector" InitialValue="0"></asp:RequiredFieldValidator>
                                قطاع المجلة
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJurnalSector" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlJurnalSector" InitialValue="0"></asp:RequiredFieldValidator>
                                مجال المجلة
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJurnalField" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlJurnalField_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtRCitationAvg"></asp:RequiredFieldValidator>
                                معدل الاستشهاد</td>
                            <td>
                                <asp:TextBox ID="txtRCitationAvg" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--                                <asp:RequiredFieldValidator Visible="false" ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlSector" InitialValue="0"></asp:RequiredFieldValidator>
                                قطاعات البحث ومجالاته--%>

                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSector" Visible="false" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <%--                                <asp:RequiredFieldValidator Visible="false" ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlSourceField" InitialValue="0"></asp:RequiredFieldValidator>
                                المجال البحثي--%>

                            </td>
                            <td>
                                <asp:DropDownList Visible="false" ID="ddlSourceField" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                            </td>
                            <td>
                                <%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlSourcetype" InitialValue="0"></asp:RequiredFieldValidator>
                                نوع المصدر البحثي--%>
                            </td>
                            <td>
                                <%--                                <asp:DropDownList ID="ddlSourcetype" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد نوع المصدر البحثي</asp:ListItem>
                                    <asp:ListItem Value="دار نشر">دار نشر</asp:ListItem>
                                    <asp:ListItem Value="مجلة">مجلة</asp:ListItem>
                                    <asp:ListItem Value="مؤتمر علمي">مؤتمر علمي</asp:ListItem>
                                </asp:DropDownList>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtJournalName"></asp:RequiredFieldValidator>
                                عنوان المجلة
                            </td>
                            <td>
                                <asp:TextBox ID="txtJournalName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtISSN"></asp:RequiredFieldValidator>
                                ISSN
                            </td>
                            <td>
                                <asp:TextBox ID="txtISSN" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtPublisher"></asp:RequiredFieldValidator>
                                الناشر
                            </td>
                            <td>
                                <asp:TextBox ID="txtPublisher" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlRSourceType" InitialValue="0"></asp:RequiredFieldValidator>
                                نوع المصدر البحثي
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRSourceType" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد المصدر البحثي</asp:ListItem>
                                    <asp:ListItem Value="مجلة">مجلة</asp:ListItem>
                                    <asp:ListItem Value="مؤتمر علمي">مؤتمر علمي</asp:ListItem>
                                    <asp:ListItem Value="دار نشر">دار نشر</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlClass" InitialValue="0"></asp:RequiredFieldValidator>
                                تصنيف المجلة
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد تصنيف المجلة</asp:ListItem>
                                    <asp:ListItem Value="1">الربع الأول</asp:ListItem>
                                    <asp:ListItem Value="2">الربع الثاني</asp:ListItem>
                                    <asp:ListItem Value="3">الربع الثالث</asp:ListItem>
                                    <asp:ListItem Value="4">الربع الرابع</asp:ListItem>
                                    <asp:ListItem Value="6">غير متاح</asp:ListItem>
                                    <asp:ListItem Value="5">Non Qs</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlTop10" InitialValue="0"></asp:RequiredFieldValidator>
                                ضمن أول 10 مجلات
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTop10" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="لا">لا</asp:ListItem>
                                    <asp:ListItem Value="نعم">نعم</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6" class="auto-style2"><strong>معلومات الدعم والمكافأة
                            </strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlSupport" InitialValue="0"></asp:RequiredFieldValidator>
                                دعم داخلي
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSupport" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlSupport_SelectedIndexChanged">
                                    <asp:ListItem Value="0">حدد</asp:ListItem>
                                    <asp:ListItem Value="لا">لا</asp:ListItem>
                                    <asp:ListItem Value="نعم">نعم</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSupportRe" runat="server" CssClass="ChosenSelector" Visible="false"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlReward" InitialValue="0"></asp:RequiredFieldValidator>
                                مكافأة داخلية
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReward" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlReward_SelectedIndexChanged">
                                    <asp:ListItem Value="0">حدد</asp:ListItem>
                                    <asp:ListItem Value="لا">لا</asp:ListItem>
                                    <asp:ListItem Value="نعم">نعم</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlRewardRe" runat="server" CssClass="ChosenSelector" Visible="false"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="txtOutSupport" InitialValue="0"></asp:RequiredFieldValidator>
                                دعم خارجي
                            </td>
                            <td>
                                <asp:TextBox ID="txtOutSupport" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="auto-style2"><strong>الباحثين المشاركين بالبحث
                            </strong>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label3" runat="server" Text="الباحثين حسب ورودهم في البحث"></asp:Label>
                            </td>
                            <td colspan="2">
                                <div id="ResearchNamesScopusDiv" runat="server" visible="false" dir="ltr">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlSupport" InitialValue="0"></asp:RequiredFieldValidator>
                                العدد الكلي
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAllR" runat="server" CssClass="ChosenSelector">
                                    <asp:ListItem Value="0">حدد</asp:ListItem>
                                    <asp:ListItem Value="لا">لا</asp:ListItem>
                                    <asp:ListItem Value="نعم">نعم</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*" CssClass="validcss" ControlToValidate="ddlReward" InitialValue="0"></asp:RequiredFieldValidator>
                                العدد الداخلي
                            </td>
                            <td>

                                <asp:DropDownList ID="ddlINR" runat="server" CssClass="ChosenSelector">
                                </asp:DropDownList>




                            </td>
                            <td>
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlINRName" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlINRName_SelectedIndexChanged"></asp:DropDownList>
                                        <div id="duplicateRDiv" runat="server" visible="false" style="color: red">
                                            تم إضافة الباحث مسبقا
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upGrd" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="لا يوجد معلومات حاليا"
                                            Caption="معلومات الباحثين الداخليين المشاركين بالبحث" AutoGenerateColumns="false" CssClass="grd">
                                            <Columns>
                                                <asp:BoundField HeaderText="رمز الباحث" DataField="RId" />
                                                <asp:BoundField HeaderText="اسم الباحث-عربي" DataField="RaName" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelRIn" CausesValidation="false" runat="server" OnClick="lnkDelRIn_Click" ToolTip="حــذف" OnClientClick="return confirm('هل أنت متأكد من حذف معلومات الباحث؟')"><i class="material-icons" style="color: #E34724">clear</i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="8" align="center">
                                <asp:Button ID="btnNew" runat="server" Text="جديد" CssClass="btn" OnClick="btnNew_Click" CausesValidation="false" />
                                <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn" OnClick="btnSave_Click" />



                            </td>
                        </tr>
                    </table>
                </div>
                <div style="z-index: 99; width: 100%; height: 100%; position: fixed; top: 0; left: 0" id="ConfirmDiv" runat="server" visible="false">
                    <div style="text-align: center; width: 50%; height: 200px; background-color: #f5f5f5; color: black; border: 3px outset darkred; z-index: 99; font-size: large; opacity: 1; position: fixed; top: 40%; left: 20%">
                        <asp:Label ID="Label91" runat="server" Text="هل أنت متأكد من تحميل البيانات؟" Font-Size="X-Large"></asp:Label><br />
                        <br />
                        <asp:Button ID="btnConfirm" runat="server" Text="نعم" OnClick="btnConfirm_Click" Style="border-radius: 10px; border: 3px outset" CssClass="btn" CausesValidation="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="إلغاء" OnClick="btnCancel_Click" Style="border-radius: 10px; border: 3px outset" CssClass="btn" CausesValidation="false" />
                    </div>
                </div>
                <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="3000" OnTick="Timer1_Tick"></asp:Timer>
                <div class="alert alert-success alert-icon" role="alert" visible="false" id="alertMsgSuccess" runat="server" style="font-family: Calibri; margin-top: 20px">
                    <i class="mdi mdi-checkbox-marked-outline"></i>
                    <asp:Label ID="Label1" runat="server" Text="تم تحميل البيانات بنجاح"></asp:Label>
                </div>
                <div class="alert alert-danger alert-icon" role="alert" visible="false" id="alertErr" runat="server" style="font-family: Calibri; margin-top: 20px">
                    <i class="mdi mdi-checkbox-marked-outline"></i>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
                <div id="copyDiv" runat="server">
                </div>

                <div style="position: fixed; top: 50%; left: 20%; background-color: green; text-align: center; width: 50%; padding: 50px; color: white" runat="server" id="msgDiv" visible="false">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMsg" runat="server" Text="تم التخزين بنجاح"></asp:Label><br />
                            <asp:Button ID="btnOk" runat="server" Text="عودة" OnClick="btnOk_Click" CssClass="btn" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


    </div>

    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "100%" });

        }
    </script>
</asp:Content>
