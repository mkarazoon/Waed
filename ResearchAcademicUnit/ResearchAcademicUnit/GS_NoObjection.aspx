<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GS_NoObjection.aspx.cs" Inherits="ResearchAcademicUnit.GS_NoObjection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css">
    <script>
        function executeExample(errMsg, sentIcon) {
            Swal.fire({
                icon: sentIcon,
                text: errMsg,
                showConfirmButton: false,
                timer: 2500,
                timerProgressBar: true,

            })
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-right">
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    نموذج عدم الممانعة لمناقشة رسالة ماجستير
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12">
            <div class="card mb-2">
                <div class="card-body">
                    <div class="card-header mb-2">
                        معلومات الطالب
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12 col-md-4">
                            <label>الرقم الجامعي</label>
                            <asp:TextBox ID="txtStudId" runat="server" CssClass="form-control" required Width="100%" data-mask="000000000" pattern="[0-9]{9}" onblur="getData()" ReadOnly="true"></asp:TextBox>
                            <asp:Button ID="btnGetData" runat="server" Text="Button" OnClick="btnGetData_Click" formnovalidate="formnovalidate" Style="display: none" />
                            <script>
                                function getData() {
                                    var btnHidden = $('#<%= btnGetData.ClientID %>');
                                    if (btnHidden != null) {
                                        btnHidden.click();
                                    }
                                }
                            </script>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>اسم الطالب رباعي</label>
                            <asp:Label ID="lblStudName" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-2">
                            <label>المعدل التراكمي</label>
                            <asp:Label ID="lblStudAvg" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-2">
                            <label>حالة الطالب</label>
                            <asp:Label ID="lblStudStatus" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>الكلية</label>
                            <asp:Label ID="lblStudFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudFacultyNo" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>القسم</label>
                            <asp:Label ID="lblStudDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                            <asp:Label ID="lblStudDeptNo" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>التخصص</label>
                            <asp:Label ID="lblStudMajor" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mb-2">
                <div class="card-body">
                    <div class="card-header mb-2">
                        عنوان الرسالة
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12 col-md-6">
                            <label>باللغة العربية</label>
                            <asp:TextBox ID="txtThesisTitleOriginal" runat="server" Rows="5" Width="100%" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-12 col-md-6">
                            <label>باللغة الانجليزية</label>
                            <asp:TextBox ID="txtThesisTitleTranslate" runat="server" Rows="5" Width="100%" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mb-2" runat="server" id="buttonsDiv">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-md-3 col-sm-12">
                                <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="btn btn-primary" OnClick="btnSend_Click" />
                                <%--<button type="button" class="btn btn-gradient-purple" data-toggle="modal" data-animation="bounce" data-target="#exampleModal" formnovalidate="formnovalidate" runat="server" id="btnShowDec">التنسيب</button>--%>
                                <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2500" OnTick="Timer1_Tick"></asp:Timer>
                                <asp:Timer ID="Timer3" runat="server" Enabled="false" Interval="2500" OnTick="Timer3_Tick"></asp:Timer>
                            </div>
                        </div>

                    </div>
                </div>
            <div class="card mb-2" id="RegDiv" runat="server" visible="false">
                <div class="card-body">
                    <div class="card-header mb-2">
                        بيانات دائرة القبول والتسجيل
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <label>فصل التحاق الطالب بالجامعة</label>
                            <asp:Label ID="lblJoinSemester" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-4">
                            <label>من العام الجامعي</label>
                            <asp:Label ID="lblJoinYear" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-4">
                            <label>الساعات التي اجتازها الطالب بنجاح</label>
                            <asp:Label ID="lblHourSuccess" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group col-md-4">
                            <label>الفصل الذي انهى الطالب فيه المواد</label>
                            <asp:DropDownList ID="ddlFinishCourseSem" runat="server">
                                <asp:ListItem Value="1">الفصل الاول</asp:ListItem>
                                <asp:ListItem Value="2">الفصل الثاني</asp:ListItem>
                                <asp:ListItem Value="3">الفصل الثالث</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-4">
                            <label>من العام الجامعي</label>
                            <asp:TextBox ID="txtFinishCourseYesr" runat="server" type="number" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblNextYear" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-group col-md-4">
                            <label>عدد الفصول المؤجلة للطالب</label>
                            <asp:TextBox ID="txtPostpondSemCount" runat="server" type="number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>عدد الفصول التي انقطع عنها الطالب - بعذر</label>
                            <asp:TextBox ID="txtDisconnectSemCount" runat="server" type="number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>تاريخ اقرار الخطة وتعيين المشرف</label>
                            <asp:TextBox ID="txtAssginSupDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>التمديدات(الفصل/العام الجامعي)</label>
                            <asp:TextBox ID="txtExtendSemYear" runat="server" type="text" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>عدد الفصول التي امضاها الطالب في الجامعة</label>
                            <asp:TextBox ID="txtAllSems" runat="server" type="number" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <label>هل الطالب مسجل للفصل الحالي</label>
                            <asp:CheckBoxList ID="chkRegCurSem" runat="server" Width="100%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y"><span class="mr-2">نعم</span></asp:ListItem>
                                <asp:ListItem Value="N"><span class="mr-2">لا</span></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="form-group col-md-6">
                            <label>شروحات دائرة القبول والتسجيل</label>
                            <asp:TextBox ID="txtAdminRegNotes" runat="server" TextMode="MultiLine" Rows="9" Width="100%"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <label>يحق الطالب المناقشة</label>
                                    <asp:CheckBoxList ID="chkStudDiscussionStatus" runat="server" Width="100%" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Y"><span class="mr-2">نعم</span></asp:ListItem>
                                        <asp:ListItem Value="N"><span class="mr-2">لا</span></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                                <div class="form-group col-md-12">
                                    <label>الاسباب</label>
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Button ID="btnRegSend" runat="server" Text="ارسال" CssClass="btn btn-primary"
                                OnClick="btnRegSend_Click" />
                            <asp:Timer ID="Timer4" runat="server" OnTick="Timer4_Tick" Enabled="false" Interval="2500"></asp:Timer>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mb-2" id="FinDiv" runat="server" visible="false">
                <div class="card-body">
                    <div class="card-header mb-2">
                        بيانات دائرة الشؤون المالية
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12">
                            <label>تسوية كافة الأمور المالية التي تجيز للطالب مناقشة رسالة الماجستير</label>
                            <asp:CheckBoxList ID="chkStudFinanceStatus" runat="server" Width="100%" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y"><span class="mr-2">نعم</span></asp:ListItem>
                                <asp:ListItem Value="N"><span class="mr-2">لا</span></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Button ID="btnFinSend" runat="server" Text="ارسال" OnClick="btnFinSend_Click"
                                CssClass="btn btn-primary" />
                            <asp:Timer ID="Timer5" runat="server" OnTick="Timer5_Tick" Enabled="false" Interval="2500"></asp:Timer>
                        </div>
                    </div>
                </div>
            </div>
            <div id="SupervisorDiv" runat="server" visible="false">
                <div class="card mb-2">
                    <div class="card-body">
                        <div class="card-header mb-2">
                            قرار المشرف بصلاحية الرسالة للمناقشة
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label>اسم المشرف</label>
                                <asp:Label ID="lblSupervisorName" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group col-md-6">
                                <label>اسم المشرف المشارك<small> - ان وجد</small></label>
                                <asp:Label ID="Label1" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-2">
                    <div class="card-body">
                        <div class="card-header mb-2">
                            موعد المناقشة ومكانها ( مقترحا)
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class="col-12">تاريخ المناقشة</label>
                                <asp:TextBox ID="txtDiscussionDate" runat="server" CssClass="form-control col-12" onblur="getDay1()" ClientIDMode="Static" type="date"></asp:TextBox>
                                <script>
                                    function getDay1() {
                                        var value = document.getElementById('<%=txtDiscussionDate.ClientID%>').value;
                                        var dt = new Date(value);

                                        //var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                                        //var dayName = days[dt.getDay()];

                                        //var date = new Date(dateStr);
                                        var dayName= dt.toLocaleDateString("ar-Jo", { weekday: 'long' });


                                        $('#<%= txtDiscussionDay.ClientID %>').val(dayName);
                                    }
                                </script>
                            </div>
                            <div class="form-group col-md-4">
                                <label>يوم المناقشة</label>
                                <asp:TextBox ID="txtDiscussionDay" runat="server" CssClass="form-control" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label>المكان والزمان</label>
                                <asp:TextBox ID="txtPlaceTime" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <div class="card mb-2">
                            <div class="card-body">
                                <div class="card-header mb-2">
                                    لجنة المناقشة
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label>مكان العمل</label>
                                        <asp:DropDownList ID="ddlPlace" runat="server" OnSelectedIndexChanged="ddlPlace_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="jsSelect col-12">
                                            <asp:ListItem Value="0">اختيار مكان العمل</asp:ListItem>
                                            <asp:ListItem Value="IN">داخلي</asp:ListItem>
                                            <asp:ListItem Value="OUT">خارجي</asp:ListItem>
                                            <asp:ListItem Value="VIEW">مراقب الجلسة</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>الاسم</label>
                                        <asp:DropDownList ID="ddlName" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="col-12">الصفة</label>
                                        <asp:DropDownList ID="ddlDesc" runat="server" CssClass="jsSelect col-12" >
                                            <asp:ListItem Value="0">اختيار</asp:ListItem>
                                            <asp:ListItem Value="1">رئيس</asp:ListItem>
                                            <asp:ListItem Value="2">عضو</asp:ListItem>
                                            <asp:ListItem Value="3">عضو خارجي</asp:ListItem>
                                            <asp:ListItem Value="4">مراقب للجلسة</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>مكان العمل</label>
                                        <asp:TextBox ID="txtWorkPalce" runat="server" CssClass="form-control col-12" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>التخصص الدقيق</label>
                                        <asp:TextBox ID="txtMinor" runat="server" CssClass="form-control col-12" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>الرتبة العلمية</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="jsSelect col-12" Enabled="false">
                                            <asp:ListItem Value="0">اختيار</asp:ListItem>
                                            <asp:ListItem Value="1">استاذ</asp:ListItem>
                                            <asp:ListItem Value="2">استاذ مشارك</asp:ListItem>
                                            <asp:ListItem Value="3">استاذ مساعد</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4 mt-auto">
                                        <asp:Button ID="btnAddCommittee" runat="server" Text="اضافة" CssClass="btn btn-primary"
                                            OnClick="btnAddCommittee_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-12">

                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered text-center" Width="100%" EmptyDataText="لا يوجد معلومات حاليا"
                                            OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="SupId" HeaderText="الرقم" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="SupName" HeaderText="الاسم" />
                                                <asp:BoundField DataField="SupDesc" HeaderText="الصفة" />
                                                <asp:BoundField DataField="SupWorkPlace" HeaderText="مكان العمل" />
                                                <asp:BoundField DataField="SupMajor" HeaderText="التخصص الدقيق" />
                                                <asp:BoundField DataField="SupDegreeT" HeaderText="الرتبة العلمية" />
                                                <asp:BoundField DataField="PlaceT" HeaderText="داخلي/خارجي" />
                                                <asp:BoundField DataField="InsertedBy" HeaderText="أضيف بواسطة" />
                                                <asp:BoundField DataField="InsertedByDesc" HeaderText="صفته" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click"><i class="material-icons">delete</i></asp:LinkButton>
                                                        <asp:CheckBox ID="chkAdopted" runat="server" Visible="false"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <asp:Button ID="btnSendSupInfo" runat="server" Text="ارسال" CssClass="btn btn-primary"
                                            OnClick="btnSendSupInfo_Click" />
                                        <asp:Button ID="btnDirAgree" Visible="false" runat="server" Text="موافق" CssClass="btn btn-success" OnClick="btnDirAgree_Click" formnovalidate="formnovalidate" />
                                        <%--<asp:Button ID="btnDisAgree" Visible="false" runat="server" Text="غير موافق" CssClass="btn btn-danger" OnClick="btnDisAgree_Click" formnovalidate="formnovalidate" />--%>
                                        <asp:Timer ID="Timer6" runat="server" Enabled="false" OnTick="Timer6_Tick" Interval="2500"></asp:Timer>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendSupInfo" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
        </div>
        <div class="modal fade bs-pq-modal-lg text-right" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">تنسيب المسؤول</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row" id="decisionInfoDiv" runat="server">
                            <div class="form-group col-md-4">
                                <label>رقم القرار</label>
                                <asp:TextBox ID="txtDecNo" runat="server" CssClass="form-control col-12" type="number"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label>رقم الجلسة</label>
                                <asp:TextBox ID="txtDecMeetNo" runat="server" CssClass="form-control col-12" type="number"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label>تاريخ الجلسة</label>
                                <asp:TextBox ID="txtDecMeetDate" runat="server" CssClass="form-control col-12" type="date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <label>الملاحظات</label>
                                <asp:TextBox ID="txtDirNote" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtDirNote" ValidationGroup="dirdec"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="form-group col-12">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">اغلاق</button>
                                <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Enabled="false" Interval="2500"></asp:Timer>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function pageLoad(sender, args) {
                $('.jsSelect').select2();
            }
        </script>
        <script>
            $('.jsSelect').select2();
        </script>
</asp:Content>
