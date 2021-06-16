<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_SupervisorAdopted.aspx.cs" Inherits="ResearchAcademicUnit.Com_SupervisorAdopted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
    <%--<script src="scripts/jquery-3.4.1.min.js"></script>--%>
    <link href="plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="plugins/select2/select2.min.js"></script>
    <link href="plugins/dropify/css/dropify.min.css" rel="stylesheet" />
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
    <div class="container mt-3 text-right">
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    اعتماد مشرف
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <ul class="nav nav-pills nav-justified mb" id="mytabs" runat="server">
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link active" ID="lnkTab1" runat="server" OnClick="lnkTab1_Click">اعتماد مناقش/مشرف</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton class="nav-link" ID="lnkTab2" runat="server" OnClick="lnkTab2_Click">حالة المناقشين/المشرفين</asp:LinkButton>
                        </li>
                    </ul>
                    <div id="tab1" runat="server" class="mt-3">
                        <div class="row">
                            <div class="form-group col-md-4 col-sm-12">
                                <asp:RadioButtonList ID="rdType" runat="server" AutoPostBack="true" CssClass="custom-radio col-12" OnSelectedIndexChanged="rdType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="100%">
                                    <asp:ListItem Value="IN" Selected="True">داخلي</asp:ListItem>
                                    <asp:ListItem Value="OUT">خارجي</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row" id="InDiv" runat="server">
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الكلية</label>
                                            <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">القسم</label>
                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الاسم</label>
                                            <asp:DropDownList ID="ddlRName" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlRName_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>الرتبة الأكاديمية</label>
                                            <asp:Label ID="lblDegree" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>التخصص الدقيق</label>
                                            <asp:Label ID="lblMinor" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" id="OutDiv" runat="server" visible="false">
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الاسم</label>
                                            <asp:DropDownList ID="ddlOutName" runat="server" CssClass="jsSelect col-12" AutoPostBack="true" OnSelectedIndexChanged="ddlOutName_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الجامعة</label>
                                            <asp:Label ID="lblOutUni" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الكلية</label>
                                            <asp:Label ID="lblOutFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">القسم</label>
                                            <asp:Label ID="lblOutDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">التخصص</label>
                                            <asp:Label ID="lblOutMajor" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4 col-sm-12">
                                            <label class="col-12">الرتبة الأكاديمية</label>
                                            <asp:Label ID="lblOutDegree" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>الجنسية</label>
                                            <asp:Label ID="lblNat" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>الكلية المراد الاشراف بها</label>
                                            <asp:Label ID="lblFaculty" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>القسم المراد الاشراف به</label>
                                            <asp:Label ID="lblDept" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>حالة المشرف</label>
                                            <asp:Label ID="lblRStatus" runat="server" Text="" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 col-sm-12">
                                            <label>السيرة الذاتية</label>
                                            <a href="#" target="_blank" id="ViewCV" runat="server" class="btn btn-info" disabled="true">عرض</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row" id="AdoptedDiv" runat="server" visible="true">
                                        <div class="form-group col-sm-12 col-md-6">
                                            <asp:CheckBoxList ID="chkAdopted" runat="server" RepeatDirection="Horizontal" Width="100%" CssClass="custom-checkbox">
                                                <asp:ListItem Value="1">مناقش</asp:ListItem>
                                                <asp:ListItem Value="2">مشرف</asp:ListItem>
                                                <asp:ListItem Value="3">تدريس</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="form-group col-sm-12 col-md-6">
                                            <asp:Button ID="btnAdopted" runat="server" Text="يعتمد" CssClass="btn btn-success" OnClick="btnAdopted_Click" />
                                            <asp:Button ID="btnNotAdopted" runat="server" Text="لا يعتمد" CssClass="btn btn-danger" OnClick="btnNotAdopted_Click" />
                                            <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="2500" OnTick="Timer1_Tick"></asp:Timer>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="form-group col-md-12 col-sm-12">
                                            <label>أبحاث عضو هيئة التدريس</label>
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false"
                                                OnRowDataBound="GridView1_RowDataBound">
                                                <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField DataField="ReId" HeaderText="رمز البحث" />
                                                    <asp:BoundField DataField="RTitle" HeaderText="اسم البحث" />
                                                    <asp:BoundField DataField="magname" HeaderText="اسم المجلة" />
                                                    <asp:BoundField DataField="DBTypeS" HeaderText="نوع المجلة" />
                                                    <asp:BoundField DataField="globaldbi" HeaderText="تصنيف المجلة" />
                                                    <asp:BoundField DataField="pyear" HeaderText="سنة النشر" />
                                                    <asp:BoundField DataField="ROrderS" HeaderText="ترتيب الباحث" />
                                                    <asp:BoundField DataField="RStatus" HeaderText="حالة البحث" />
                                                    <asp:TemplateField HeaderText="قاعدة البيانات">
                                                        <ItemTemplate>
                                                            <asp:ListBox ID="lstDB" runat="server" SelectionMode="Multiple" DataSourceID="SqlDataSource1" DataTextField="DBName" DataValueField="AutoId"></asp:ListBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الإجراء">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkResAdopted" runat="server" OnClick="lnkResAdopted_Click" CssClass="btn btn-block btn-success">اعتماد</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkResNotAdopted" runat="server" OnClick="lnkResNotAdopted_Click" CssClass="btn btn-block btn-danger">إلغاء اعتماد</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RConStr %>" SelectCommand="SELECT * FROM Adopted_JournalList"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div id="tab2" runat="server" class="p-3" visible="false">
                        <div class="col-12 mb-2">
                            <div class="card">
                                <div class="card-body">
                                    <div class="card-header mt-2 mb-2 text-center">
                                        اسماء المشرفين/المناقشين المعتمدين
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-12">
                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false"
                                                OnDataBound="GridView2_DataBound">
                                                <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField DataField="RaName" HeaderText="اسم عضو هيئة التدريس" />
                                                    <asp:BoundField DataField="College" HeaderText="الكلية" />
                                                    <asp:BoundField DataField="Dept" HeaderText="القسم" />
                                                    <asp:BoundField DataField="Status" HeaderText="الحالة" />
                                                    <asp:BoundField DataField="AdoptedDate" HeaderText="التاريخ" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من الحذف')"><i class="material-icons text-danger">delete</i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="JobId" HeaderText="الحالة" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PLace" HeaderText="الحالة" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
