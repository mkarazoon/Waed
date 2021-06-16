<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Com_JobTitles.aspx.cs" Inherits="ResearchAcademicUnit.Com_JobTitles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/sweet-alert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="plugins/sweet-alert2/sweetalert2.min.js"></script>
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
                            <label>المسمى الوظيفي - عربي</label>
                            <asp:TextBox ID="txtAName" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-12 col-md-4">
                            <label>المسمى الوظيفي - انجليزي</label>
                            <asp:TextBox ID="txtEName" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-sm-12 col-md-3 mt-auto">
                            <asp:Button ID="btnSave" runat="server" Text="حفظ" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-sm-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center">
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
                </div>
            </div>
        </div>

    </div>
    <script src="scripts/jquery-3.4.1.min.js"></script>

</asp:Content>
