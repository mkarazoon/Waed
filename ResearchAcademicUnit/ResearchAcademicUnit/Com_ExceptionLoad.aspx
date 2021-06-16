<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_ExceptionLoad.aspx.cs" Inherits="ResearchAcademicUnit.Com_ExceptionLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-3.4.1.min.js"></script>
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
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
        <div class="row col-12">
            <div class="col-sm-12 col-md-4 mb-2 d-sm-none d-md-block"></div>
            <div class="col-sm-12 col-md-4 mb-2">
                <div class="card-header mt-2 mb-2 text-center">
                    استثناء مشرف
                </div>
            </div>
            <div class="col-md-4 mb-2 d-sm-none d-md-block"></div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        معلومات المشرف والطالب
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12 col-md-3">
                            <label>اسم عضو هيئة التدريس</label>
                            <asp:DropDownList ID="ddlSupName" runat="server" CssClass="jsSelect col-12" required></asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-12 col-md-3">
                            <label>اسم الطالب</label>
                            <asp:DropDownList ID="ddlStudName" runat="server" CssClass="jsSelect col-12" required></asp:DropDownList>
                        </div>
                        <div class="form-group col-sm-12 col-md-6">
                            <label>سبب الاستثناء</label>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>

                        <div class="form-group col-sm-12 col-md-3 mt-auto">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-12 mb-2">
            <div class="card">
                <div class="card-body">
                    <div class="card-header mb-2">
                        الاستثناءات الحالية
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center" OnRowDataBound="GridView1_RowDataBound">
                                <HeaderStyle CssClass="thead-light" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="AutoId" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="InstName" HeaderText="اسم المشرف" />
                                    <asp:BoundField DataField="StudName" HeaderText="اسم الطالب" />
                                    <asp:BoundField DataField="Reason" HeaderText="سبب الاستثناء" />
                                    <asp:BoundField DataField="ExceptionDate" HeaderText="تاريخ القرار" />
                                    <asp:BoundField DataField="Status" HeaderText="الحالة" />
                                    <asp:BoundField DataField="Status" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من الحذف')"><i class="material-icons text-danger">delete</i></asp:LinkButton>
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
    <script src="plugins/jquery-mask-input/jquery.mask.min.js"></script>

    <script>
        $('.jsSelect').select2();
    </script>

</asp:Content>
