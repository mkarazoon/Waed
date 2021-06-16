<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CourseRegistration.aspx.cs" Inherits="ResearchAcademicUnit.CourseRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="width: 30%; margin: 20px auto">
        <asp:Label ID="Label4" runat="server" Text="قائمة بأسماء حضور النشاط البحثي  "></asp:Label>
        <asp:DropDownList ID="ddlCourseName" runat="server" CssClass="ChosenSelector" Width="150px"
            AutoPostBack="true" OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnExport" runat="server" Text="اكسل" OnClick="btnExport_Click" Visible="false" CssClass="btn" Width="100px" />
        <div style="display:none;float:left">
        <asp:GridView ID="GridView5" runat="server" CssClass="grd" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                <asp:BoundField DataField="CourseName_Level" HeaderText="اسم النشاط البحثي" />
                <asp:BoundField DataField="Details" HeaderText="التاريخ والوقت" />
                <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                <asp:BoundField DataField="RCollege" HeaderText="الكلية" />
                <asp:BoundField DataField="RegisterDate" HeaderText="تاريخ التسجيل" DataFormatString="{0:dd-MM-yyyy}" />
            </Columns>
        </asp:GridView></div>
    </div>
    <div style="width: 30%; margin: 20px auto">
        <asp:Label ID="Label5" runat="server" Text="قائمة بأسماء مقيمي النشاط البحثي  "></asp:Label>
        <asp:DropDownList ID="ddlCName" runat="server" CssClass="ChosenSelector" Width="150px"
            AutoPostBack="true" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="اكسل" OnClick="Button1_Click" Visible="false" CssClass="btn" Width="100px" />
        <div style="display:none;float:left">
        <asp:GridView ID="GridView6" runat="server" CssClass="grd" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                <asp:BoundField DataField="EvalR" HeaderText="التقييم" />
                <asp:BoundField DataField="EvalDate" HeaderText="تاريخ التقييم" DataFormatString="{0:dd-MM-yyyy}"/>
            </Columns>
        </asp:GridView>
            </div>
    </div>

    <asp:UpdatePanel ID="upPanel1" runat="server">
        <ContentTemplate>
            <div style="margin-top: 30px; margin-right: 50px; margin-left: 30px">

                <div style="margin: 50px;" id="CurrentDiv" runat="server">
                    <div>
                        <asp:Label ID="lblTitle" runat="server" Text="الطلبات الحالية"></asp:Label>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" CssClass="grd" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="courseid" HeaderText="رقم الدورة" />
                            <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                            <asp:BoundField DataField="CourseName_Level" HeaderText="اسم النشاط البحثي" />
                            <asp:BoundField DataField="Details" HeaderText="التاريخ والوقت" />
                            <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                            <asp:BoundField DataField="RCollege" HeaderText="الكلية" />
                            <asp:BoundField HeaderText="التفاصيل" DataField="TargetCollege" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="RegisterDate" HeaderText="تاريخ التسجيل" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                            <asp:TemplateField HeaderText="الكليات">
                                <ItemTemplate>
                                    <div id="colNameDiv" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAgree" OnClick="lnkAgree_Click" runat="server" CausesValidation="false" ToolTip="مع الموافقة" Style="margin: 0 5px"><i class="material-icons" style="color: green">person_add</i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDisAgree" OnClick="lnkDisAgree_Click" runat="server" CausesValidation="false" ToolTip="رفض" Style="margin: 0 5px"><i class="material-icons" style="color: red">thumb_down</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin: 50px" id="AgreeDiv" runat="server">
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="المقبولون بالدورات"></asp:Label>
                    </div>

                    <asp:GridView ID="GridView2" runat="server" CssClass="grd" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="courseid" HeaderText="رقم الدورة" />
                            <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                            <asp:BoundField DataField="CourseName_Level" HeaderText="اسم النشاط البحثي" />
                            <asp:BoundField DataField="Details" HeaderText="التاريخ والوقت" />
                            <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                            <asp:BoundField DataField="RCollege" HeaderText="الكلية" />
                            <asp:BoundField HeaderText="التفاصيل" DataField="TargetCollege" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                            <asp:TemplateField HeaderText="الكليات">
                                <ItemTemplate>
                                    <div id="colNameDiv" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RegisterDate" HeaderText="تاريخ التسجيل" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" runat="server" CausesValidation="false" ToolTip="مع الموافقة" Style="margin: 0 5px"><i class="material-icons" style="color: green">close</i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkAbsWE" OnClick="lnkAbsWE_Click" runat="server" CausesValidation="false" ToolTip="غياب بعذر" Style="margin: 0 5px"><i class="material-icons" style="color: red">visibility</i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkAbsWOE" OnClick="lnkAbsWOE_Click" runat="server" CausesValidation="false" ToolTip="غياب بدون عذر" Style="margin: 0 5px"><i class="material-icons" style="color: red">visibility_off</i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

                <div style="margin: 50px" id="disAgreeDiv" runat="server">
                    <div>
                        <asp:Label ID="Label2" runat="server" Text="عدم المقبولين بالدورات"></asp:Label>
                    </div>

                    <asp:GridView ID="GridView3" runat="server" CssClass="grd" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="courseid" HeaderText="رقم الدورة" />
                            <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                            <asp:BoundField DataField="CourseName_Level" HeaderText="اسم النشاط البحثي" />
                            <asp:BoundField DataField="Details" HeaderText="التاريخ والوقت" />
                            <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                            <asp:BoundField DataField="RCollege" HeaderText="الكلية" />
                            <asp:BoundField HeaderText="التفاصيل" DataField="TargetCollege" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                            <asp:TemplateField HeaderText="الكليات">
                                <ItemTemplate>
                                    <div id="colNameDiv" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RegisterDate" HeaderText="تاريخ التسجيل" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" runat="server" CausesValidation="false" ToolTip="مع الموافقة" Style="margin: 0 5px"><i class="material-icons" style="color: green">close</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

                <div style="margin: 50px" id="AbsDiv" runat="server">
                    <div>
                        <asp:Label ID="Label3" runat="server" Text="الغياب عن الدورة"></asp:Label>
                    </div>

                    <asp:GridView ID="GridView4" runat="server" CssClass="grd" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="courseid" HeaderText="رقم الدورة" />
                            <asp:BoundField DataField="JobId" HeaderText="الرقم الوظيفي" />
                            <asp:BoundField DataField="CourseName_Level" HeaderText="اسم النشاط البحثي" />
                            <asp:BoundField DataField="Details" HeaderText="التاريخ والوقت" />
                            <asp:BoundField DataField="RaName" HeaderText="الاسم" />
                            <asp:BoundField DataField="RCollege" HeaderText="الكلية" />
                            <asp:BoundField HeaderText="التفاصيل" DataField="TargetCollege" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField DataField="Status" HeaderText="حالة الطلب" />
                            <asp:TemplateField HeaderText="الكليات">
                                <ItemTemplate>
                                    <div id="colNameDiv" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RegisterDate" HeaderText="تاريخ التسجيل" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" runat="server" CausesValidation="false" ToolTip="مع الموافقة" Style="margin: 0 5px"><i class="material-icons" style="color: red">close</i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnExport" EventName="Click"/>--%>
        </Triggers>
    </asp:UpdatePanel>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "80%" });
        }
    </script>

</asp:Content>
