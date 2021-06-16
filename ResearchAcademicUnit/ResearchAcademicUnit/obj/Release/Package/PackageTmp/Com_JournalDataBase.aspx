<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_JournalDataBase.aspx.cs" Inherits="ResearchAcademicUnit.Com_JournalDataBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>

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
        <ul class="nav nav-pills nav-justified mb" id="mytabs" runat="server">
            <li class="nav-item">
                <asp:LinkButton class="nav-link active" ID="lnkTab1" runat="server" OnClick="lnkTab1_Click">قواعد البيانات</asp:LinkButton>
            </li>
            <li class="nav-item">
                <asp:LinkButton class="nav-link" ID="lnkTab2" runat="server" OnClick="lnkTab2_Click">المسميات الوظيفية</asp:LinkButton>
            </li>
            <li class="nav-item">
                <asp:LinkButton class="nav-link" ID="lnkTab3" runat="server" OnClick="lnkTab3_Click">الصلاحيات</asp:LinkButton>
            </li>
        </ul>
        <div id="tab1" runat="server" class="mt-3">
            <div class="row col-12">
                <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
                <div class="col-sm-12 col-md-4 mb-2">
                    <div class="card-header mt-2 mb-2 text-center">
                        قواعد البيانات المعتمدة
                    </div>
                </div>
                <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
            </div>
            <div class="col-12 mb-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <label>اسم قاعدة البيانات</label>
                                <asp:TextBox ID="txtDBName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-12 col-md-3 mt-auto">
                                <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center"
                                    AutoGenerateEditButton="true" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating">
                                    <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="AutoId" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="DBName" HeaderText="اسم قاعدة البيانات" />
                                        <asp:BoundField DataField="Status" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" Visible="false" ToolTip="الغلاء اعتماد" OnClientClick="return confirm('هل أنت متأكد من إلغاء الاعتماد لقاعدة البيانات؟')"><i class="material-icons text-danger">cancel</i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkAdopted" runat="server" OnClick="lnkDelete_Click" Visible="false" ToolTip="اعتماد" OnClientClick="return confirm('هل أنت متأكد من اعتماد قاعدة البيانات؟')"><i class="material-icons text-info">check_circle</i></asp:LinkButton>
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
        <div id="tab2" runat="server" class="mt-3" visible="false">
            <div class="row col-12">
                <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
                <div class="col-sm-12 col-md-4 mb-2">
                    <div class="card-header mt-2 mb-2 text-center">
                        المسميات الوظيفية
                    </div>
                </div>
                <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
            </div>
            <div class="col-12 mb-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <label>المسمى الوظيفي - عربي</label>
                                <asp:TextBox ID="txtAName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-12 col-md-4">
                                <label>المسمى الوظيفي - انجليزي</label>
                                <asp:TextBox ID="txtEName" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-12 col-md-3 mt-auto">
                                <asp:Button ID="btnSaveJobTitle" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClick="btnSaveJobTitle_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center" AutoGenerateEditButton="true"
                                    OnRowEditing="GridView2_RowEditing" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowUpdating="GridView2_RowUpdating">
                                    <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="AutoId" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="JobTitleA" HeaderText="المسمى الوظيفي - عربي" />
                                        <asp:BoundField DataField="JobTitleE" HeaderText="المسمى الوظيفي - انجليزي" />
                                        <%--                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من الحذف')"><i class="material-icons text-danger">delete</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <asp:GridView ID="GridView4" runat="server"></asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tab3" runat="server" class="mt-3" visible="false">
            <div class="row col-12">
                <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
                <div class="col-sm-12 col-md-4 mb-2">
                    <div class="card-header mt-2 mb-2 text-center">
                        الصلاحيات
                    </div>
                </div>
                <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
            </div>
            <div class="col-12 mb-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <label>صلاحيات العمداء و رؤساء الأقسام من النظام </label>
                                <asp:Button ID="btnGetData" runat="server" Text="عرض" OnClick="btnGetData_Click" CssClass="btn btn-dark" formnovalidate="formnovalidate"/>
                                <%--<asp:Button ID="Button1" runat="server" Text="عرض" OnClick="Button1_Click" CssClass="btn btn-dark" formnovalidate="formnovalidate"/>--%>
                            </div>
                        </div>
<%--                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <asp:GridView ID="GridView5" runat="server"></asp:GridView>
                                </div>
                            </div>--%>
                    </div>
                </div>
            </div>
            <div class="col-12 mb-2">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <label>العمادة/الكلية/الدائرة</label>
                                <asp:DropDownList ID="ddlMainDept" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlMainDept_SelectedIndexChanged" required></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-12 col-md-4">
                                <label>القسم/الشعبة</label>
                                <asp:DropDownList ID="ddlSupDept" runat="server" CssClass="jsSelect col-12"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12 col-md-4">
                                <label>المسمى الوظيفي</label>
                                <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="jsSelect col-12" required></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-12 col-md-4">
                                <label>صاحب الصلاحية</label>
                                <asp:DropDownList ID="ddlPrivToName" runat="server" CssClass="jsSelect col-12" required></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-12 col-md-3 mt-auto">
                                <asp:Button ID="btnSavePriv" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClick="btnSavePriv_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center"
                                    OnDataBound="GridView3_DataBound">
                                    <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="PrivNo" HeaderText="رقم الصلاحية" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="PrivId" HeaderText="رقم المسمى الوظيفي" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="FacultyNo" HeaderText="رقم الكلية" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="FacultyName" HeaderText="اسم الكلية" />
                                        <asp:BoundField DataField="DeptId" HeaderText="رقم القسم" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="DeptName" HeaderText="اسم القسم" />
                                        <asp:BoundField DataField="JobTitleA" HeaderText="المسمى الوظيفي" />
                                        <asp:BoundField DataField="PrivTo" HeaderText="الرقم الوظيفي" />
                                        <asp:BoundField DataField="PrivToName" HeaderText="صاحب الصلاحية" />
                                        <%--                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من الحذف')"><i class="material-icons text-danger">delete</i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2500" OnTick="Timer1_Tick"></asp:Timer>
    </div>
    <script type="text/javascript">
        function LoginFail() {
            $('#exampleModal').modal();
        }
    </script>
    <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel" runat="server">تأكيد</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="form-group col-md-12">
                        <label for="example-text-input" class="col-form-label text-right">يوجد معلومات لهذه الصلاحية هل تريد التعديل عليها؟</label>
                        <%--<div class="col-sm-9">
                                <asp:TextBox ID="TextBox1" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="10" Width="100%" Style="resize: none"></asp:TextBox>
                            </div>--%>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="form-group col-md-12">
                        <button type="button" class="btn btn-success" data-dismiss="modal" onclick="DivB6()">نعم</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal">الغاء</button>
                        <asp:Button ID="btnUpdatePriv" runat="server" Text="تعديل صلاحية" OnClick="btnUpdatePriv_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $('.jsSelect').select2();

        function DivB6() {
                                var btnHidden = $('#<%= btnUpdatePriv.ClientID %>');
                                if (btnHidden != null) {
                                    btnHidden.click();
                                }
                            }
    </script>

</asp:Content>
