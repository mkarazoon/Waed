<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_SupervisorAdoptedReport.aspx.cs" Inherits="ResearchAcademicUnit.Com_SupervisorAdoptedReport" %>

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
                    تقرير ابحاث اعضاء هئية التدريس
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>

        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div id="tab1" runat="server" class="mt-3">
                        <div class="row">
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mb-2">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="row" id="AdoptedDiv" runat="server" visible="false">
                                            <div class="form-group col-sm-12 col-md-6">
                                                <asp:CheckBoxList ID="chkAdopted" runat="server" RepeatDirection="Horizontal" Width="100%" CssClass="custom-checkbox">
                                                    <asp:ListItem Value="1">مناقش</asp:ListItem>
                                                    <asp:ListItem Value="2">مشرف</asp:ListItem>
                                                    <asp:ListItem Value="3">تدريس</asp:ListItem>
                                                </asp:CheckBoxList>
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
                                                        <asp:BoundField DataField="ReId" HeaderText="رمز البحث" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="RTitle" HeaderText="اسم البحث" />
                                                        <asp:BoundField DataField="magname" HeaderText="اسم المجلة" />
                                                        <asp:BoundField DataField="DBTypeS" HeaderText="نوع المجلة" />
                                                        <asp:BoundField DataField="filepath" HeaderText="تصنيف المجلة" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="pyear" HeaderText="سنة النشر" />
                                                        <asp:BoundField DataField="ROrderS" HeaderText="ترتيب الباحث" />
                                                        <asp:BoundField DataField="RStatus" HeaderText="حالة البحث" />

                                                        <asp:TemplateField HeaderText="قاعدة البيانات">
                                                            <ItemTemplate>
                                                                <asp:ListBox ID="lstDB" runat="server" SelectionMode="Multiple" Visible="false" DataSourceID="SqlDataSource1" DataTextField="DBName" DataValueField="AutoId"></asp:ListBox>
                                                                <asp:Label ID="lblDB" runat="server" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الإجراء">
                                                            <ItemTemplate>
                                                                <a runat="server" href="#" id="localFile" target="_blank">عرض ملف</a>
                                                                <a runat="server" href="#" id="localLink" target="_blank">عرض رابط</a>
                                                                <%--                                                            <asp:LinkButton ID="lnkResAdopted" runat="server" OnClick="lnkResAdopted_Click" CssClass="btn btn-block btn-success">عرض ملف</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkResNotAdopted" runat="server" OnClick="lnkResNotAdopted_Click" CssClass="btn btn-block btn-success">عرض رابط</asp:LinkButton>--%>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.jsSelect').select2();
    </script>
</asp:Content>
