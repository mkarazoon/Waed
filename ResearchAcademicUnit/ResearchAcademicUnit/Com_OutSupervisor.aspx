<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_OutSupervisor.aspx.cs" Inherits="ResearchAcademicUnit.Com_OutSupervisor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>
    <link href="plugins/dropify/css/dropify.min.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.3/js/bootstrap.min.js"></script>
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
    <div class="container mt-5 text-right">
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    المشرفين الخارجيين
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <ul class="nav nav-tabs" id="InfoTab">
            <li class="nav-item">
                <a class="nav-link active" data-toggle="tab" href="#NewInfoDiv" aria-selected="false">مشرف جديد</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" data-toggle="tab" href="#CurrentInfoDiv" aria-selected="false">المشرفين الحاليين</a>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active p-3" id="NewInfoDiv" role="tabpanel">
                <div class="col-12 mb-2">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-header mb-2">
                                معلومات المشرف الخارجي
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">الاسم الرباعي</label>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">التخصص</label>
                                    <asp:TextBox ID="txtMajor" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">الرتبة الأكاديمية</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="jsSelect col-12" Width="100%" required>
                                        <asp:ListItem Value="">اختر الرتبة الأكاديمية</asp:ListItem>
                                        <asp:ListItem Value="1">أستاذ</asp:ListItem>
                                        <asp:ListItem Value="2">أستاذ مشارك</asp:ListItem>
                                        <asp:ListItem Value="3">أستاذ مساعد</asp:ListItem>
                                        <asp:ListItem Value="4">أستاذ شرف</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row col-12">
                                <div class="card-header mb-2">
                                    مكان العمل الحالي
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">الجامعة</label>
                                    <asp:TextBox ID="txtUni" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">الكلية</label>
                                    <asp:TextBox ID="txtFaculty" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">القسم</label>
                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control col-12" Width="100%" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label class="col-12">الجنسية</label>
                                    <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNat_SelectedIndexChanged" CssClass="jsSelect col-12" required></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12" id="SSNDiv" runat="server" visible="true">
                                    <label class="col-12">الرقم الوطني للأردنيين</label>
                                    <asp:TextBox ID="txtSSN" runat="server" CssClass="form-control" Width="100%" required data-mask="0000000000" pattern="[1-9]{1}[0-9]{9}"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12" id="PassportDiv" runat="server" visible="false">
                                    <label class="col-12">جواز السفر لغير الأردنيين</label>
                                    <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" Width="100%" required></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 mb-2">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-header mb-2">
                                معلومات الكلية والقسم المراد الاشراف/المناقشة
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label>الكلية</label>
                                    <asp:DropDownList ID="ddlInFaculty" runat="server" CssClass="col-12 jsSelect" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlInFaculty_SelectedIndexChanged" required></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 col-lg-4 col-sm-12">
                                    <label>القسم</label>
                                    <asp:DropDownList ID="ddlInDept" runat="server" CssClass="col-12 jsSelect" Width="100%" required></asp:DropDownList>
                                </div>
                                <%--                        <div class="form-group col-md-4 col-lg-4 col-sm-12 mt-auto">
                            <label>السيرة الذاتية</label>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
<%--                <div class="col-12 mb-2">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-header mb-2">
                                مـعـلـومـات الأبـحـاث
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12 col-lg-12 col-sm-12">
                                    <label class="col-12">اسم البحث</label>
                                    <asp:TextBox ID="txtRTitle" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="حقل مطلوب" CssClass="alert-danger rounded p-1" ControlToValidate="txtRTitle"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6 col-lg-6 col-sm-12">
                                    <label class="col-12">اسم المجلة</label>
                                    <asp:TextBox ID="txtJournal" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="حقل مطلوب" CssClass="alert-danger rounded p-1" ControlToValidate="txtJournal"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6 col-lg-6 col-sm-12">
                                    <label class="col-12">تاريخ النشر/قبول النشر</label>
                                    <asp:TextBox ID="txtPubDate" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="حقل مطلوب" CssClass="alert-danger rounded p-1" ControlToValidate="txtPubDate"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6 col-lg-6 col-sm-12">
                                    <label class="col-12">تصنيف المجلة</label>
                                    <asp:TextBox ID="txtJournalClass" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="حقل مطلوب" CssClass="alert-danger rounded p-1" ControlToValidate="txtJournalClass"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6 col-lg-6 col-sm-12">
                                    <label class="col-12">حالة البحث</label>
                                    <asp:DropDownList ID="ddlRStatus" runat="server" CssClass="col-12 jsSelect" Width="100%">
                                        <asp:ListItem Value="0">اختيار</asp:ListItem>
                                        <asp:ListItem Value="Pub">منشور</asp:ListItem>
                                        <asp:ListItem Value="Acc">مقبول للنشر</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="حقل مطلوب" CssClass="alert-danger rounded p-1" ControlToValidate="ddlRStatus" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-3 col-md-3 col-lg-3 col-sm-12">
                                    <asp:LinkButton ID="lnkAddResearch" runat="server" Text="إضافة بحث" OnClick="lnkAddResearch_Click" CssClass="btn btn-primary btn-block"></asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12 col-lg-12 col-sm-12">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" Width="100%"
                                        OnRowDataBound="GridView1_RowDataBound">
                                        <HeaderStyle CssClass="thead-light" />
                                        <Columns>
                                            <asp:BoundField DataField="AutoId" HeaderText="رقم" />
                                            <asp:BoundField DataField="RTitle" HeaderText="اسم البحث" />
                                            <asp:BoundField DataField="Journal" HeaderText="اسم المجلة" />
                                            <asp:BoundField DataField="PubDate" HeaderText="تاريخ النشر/قبول النشر" />
                                            <asp:BoundField DataField="JournalClass" HeaderText="تصنيف المجلة" />
                                            <asp:BoundField DataField="RStatus" HeaderText="حالة البحث" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" CssClass="btn btn-danger btn-block" CausesValidation="false" OnClientClick="return confirm('هل أنت متأكد من حذف البحث؟')"><i class="material-icons">delete_forever</i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" CssClass="btn btn-primary btn-block" CausesValidation="false"><i class="material-icons">edit</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-12 mb-2">
                    <div class="row">
                        <%--<div class="col-md-6 col-sm-12 mb-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-header mb-2">
                                        يجب إرفاق الصفحة الأولى من الأبحاث المنشورة لكل بحث بملف واحد فقط وأن لا يتجاوز حجمه 10M
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <input type="file" id="uploadResearch" class="dropify" runat="server" data-height="30" data-allowed-file-extensions="pdf" accept="application/pdf" data-max-file-size="10M" data-default-file="" required />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-6 col-sm-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-header mb-2">
                                        السيرة الذاتية
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-12">
                                            <input type="file" id="File1" class="dropify" runat="server" data-height="30" data-allowed-file-extensions="pdf" accept="application/pdf" data-max-file-size="10M" data-default-file="" required />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3">
                            <asp:Button ID="btnSubmit" runat="server" Text="ارسال" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Visible="true" CausesValidation="false" />
                            <asp:Timer ID="Timer1" runat="server" Enabled="false" OnTick="Timer1_Tick" Interval="2500"></asp:Timer>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane p-3" id="CurrentInfoDiv" role="tabpanel">
                <div class="row">
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group col-md-12">
                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered text-center"
                                    Width="100%" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="AutoId" HeaderText="رقم المشرف"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                        <asp:BoundField DataField="RName" HeaderText="اسم المشرف" />
                                        <asp:BoundField DataField="RMajor" HeaderText="التخصص" />
                                        <asp:BoundField DataField="RDegreeT" HeaderText="الدرجة الاكاديمية" />
                                        <asp:BoundField DataField="RUni" HeaderText="مكان العمل" />
                                        <asp:BoundField DataField="CVPath" HeaderText="السيرة الذاتية" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="RFilePath" HeaderText="الابحاث" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href='<%# Eval("CVPath") %>' runat="server" id="lnkCV" target="_blank" title="عرض السيرة الذاتية"><i class="material-icons text-success">assignment_ind</i></a>
                                                <a href='<%# Eval("RFilePath") %>' runat="server" id="A1" target="_blank" title="عرض الابحاث"><i class="material-icons text-info">book</i></a>
                                                <asp:LinkButton ID="lnkRDelete" runat="server" OnClick="lnkRDelete_Click" formnovalidate="formnovalidate" CausesValidation="false"><i class="material-icons text-danger">delete</i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--        <script type="text/javascript">
        function DeleteRInfo() {
            $('#PayInfo').modal();
        }
    </script>

    <div class="modal fade" id="DeleteRInfo" tabindex="-1" aria-labelledby="NewUserLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="NewUserLabel">طريقة التبرع</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <ul class="nav nav-tabs" role="tablist" id="InfoTab">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#NewInfoDiv" role="tab" aria-selected="false">متبرع جديد</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#CurInfoDiv" role="tab" aria-selected="false">متبرع موجود</a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active p-3" id="NewInfoDiv" role="tabpanel">
                            
                            <div class="row" id="totalDiv" runat="server">
                                <div class="form-group col-md-6">
                                    <label>الاسم الرباعي</label>
                                    <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>رقم الجوال</label>
                                    <asp:TextBox ID="txtNewMobile" placeholder="05xxxxxxxx" runat="server" CssClass="form-control" data-mask="0500000000"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>كلمة المرور</label>
                                    <asp:TextBox ID="txtNewPW" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>تأكيد كلمة المرور</label>
                                    <asp:TextBox ID="txtNewConfirmPW" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane p-3" id="CurInfoDiv" role="tabpanel">
                            <div class="row" id="Div1" runat="server">
                                <div class="form-group col-md-6">
                                    <label>الاسم الرباعي</label>
                                    <asp:TextBox ID="txtCurName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>رقم الجوال</label>
                                    <asp:TextBox ID="txtCurMobile" placeholder="05xxxxxxxx" runat="server" CssClass="form-control" data-mask="0500000000"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ul class="nav nav-tabs" role="tablist" id="ContTab">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#CashDiv" role="tab" aria-selected="false">تحويل بنكي</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#EPayDiv" role="tab" aria-selected="false">دفع الكتروني</a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active p-3" id="CashDiv" role="tabpanel">
                            <div class="row">
                                <asp:CheckBox ID="chk1" runat="server" Text="تحويل بنكي"/>
                                <div class="form-group col-md-4">
                                    <label>حدد البنك</label>
                                    <asp:DropDownList ID="ddlBankInfo" runat="server" CssClass="jsSelect col-12" required></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane p-3" id="EPayDiv" role="tabpanel">
                            <asp:CheckBox ID="chk2" runat="server" Text="دفع الكتروني"/>
                            <div class="form-group col-md-4"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-6">
                            <asp:Button ID="btnCashPay" runat="server" Text="تبرع" CssClass="btn btn-gradient-primary" OnClick="btnPay_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>


    <script src="plugins/dropify/js/dropify.min.js"></script>
    <script src="plugins/jquery-mask-input/jquery.mask.min.js"></script>
    <script>
        $('.jsSelect').select2();
        $('.dropify').dropify();
    </script>
</asp:Content>
