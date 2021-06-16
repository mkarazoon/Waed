<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AllRequests.aspx.cs" Inherits="ResearchAcademicUnit.AllRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 150px auto; padding: 2%">
        <div class="TitleDiv">
            نموذج طلب مكافأة على نشر بحث علمي
        </div>
        <div style="margin: 1% auto; text-align: center; width: 80%">
            <div>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Literal runat="server">اسم الباحث</asp:Literal><br />
                            <asp:TextBox runat="server" ID="searchBox" OnTextChanged="searchBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Literal runat="server">اسم الكلية</asp:Literal><br />
                            <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                <asp:ListItem Value="1">جميع الكليات</asp:ListItem>
                                <asp:ListItem Value="الاداب والعلوم">الاداب والعلوم</asp:ListItem>
                                <asp:ListItem Value="الحقوق">الحقوق</asp:ListItem>
                                <asp:ListItem Value="الاعمال">الاعمال</asp:ListItem>
                                <asp:ListItem Value="تكنولوجيا المعلومات">تكنولوجيا المعلومات</asp:ListItem>
                                <asp:ListItem Value="العلوم التربوية">العلوم التربوية</asp:ListItem>
                                <asp:ListItem Value="الهندسة">الهندسة</asp:ListItem>
                                <asp:ListItem Value="الاعلام">الاعلام</asp:ListItem>
                                <asp:ListItem Value="العمارة والتصميم">العمارة والتصميم</asp:ListItem>
                                <asp:ListItem Value="الصيدلة">الصيدلة</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Literal runat="server">نوع الطلب</asp:Literal>
                            <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector">
                                <asp:ListItem Value="1">جميع الطلبات</asp:ListItem>
                                <asp:ListItem Value="دعم">طلب دعم رسوم</asp:ListItem>
                                <asp:ListItem Value="مكافأة">طلب مكافأة</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>حالة الطلب</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector">
                                <asp:ListItem Value="1">جميع الطلبات</asp:ListItem>
                                <asp:ListItem Value="2">مكتمل</asp:ListItem>
                                <asp:ListItem Value="3">قيد الدراسة</asp:ListItem>
                                <asp:ListItem Value="4">قيد المعالجة</asp:ListItem>
                                <asp:ListItem Value="5">مغلق</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="GridView1" runat="server"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td style="padding:5px">جميع الطلبات</td></tr></table>'
                EmptyDataText="لا يوجد طلبات حاليا" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="noData"
                CssClass="grd" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="رقم الطلب" DataField="ReqId" HeaderStyle-Width="75px" />
                    <asp:BoundField HeaderText="تاريخ التقديم" DataField="ReqDate" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-Width="75px" />
                    <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" HeaderStyle-Width="200px" />
                    <%--<asp:TemplateField SortExpression="RaName">
                        <HeaderTemplate>
                            <asp:Literal runat="server">اسم الباحث</asp:Literal>
                            <asp:TextBox runat="server" ID="searchBox" OnTextChanged="searchBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="raname" Text='<%# Bind("RaName") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <%--<asp:TemplateField SortExpression="College">
                        <HeaderTemplate>
                            <asp:Literal runat="server">اسم الكلية</asp:Literal><br />
                            <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                <asp:ListItem Value="0">حدد الكلية</asp:ListItem>
                                <asp:ListItem Value="1">جميع الكليات</asp:ListItem>
                                <asp:ListItem Value="الاداب والعلوم">الاداب والعلوم</asp:ListItem>
                                <asp:ListItem Value="الحقوق">الحقوق</asp:ListItem>
                                <asp:ListItem Value="الاعمال">الاعمال</asp:ListItem>
                                <asp:ListItem Value="تكنولوجيا المعلومات">تكنولوجيا المعلومات</asp:ListItem>
                                <asp:ListItem Value="العلوم التربوية">العلوم التربوية</asp:ListItem>
                                <asp:ListItem Value="الهندسة">الهندسة</asp:ListItem>
                                <asp:ListItem Value="الاعلام">الاعلام</asp:ListItem>
                                <asp:ListItem Value="العمارة والتصميم">العمارة والتصميم</asp:ListItem>
                                <asp:ListItem Value="الصيدلة">الصيدلة</asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Literal runat="server" ID="College" Text='<%# Bind("College") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:BoundField HeaderText="اسم الكلية" DataField="College" HeaderStyle-Width="150px" />

                    <%--<asp:TemplateField SortExpression="RequestType">
                        <HeaderTemplate>
                            <asp:Literal runat="server">نوع الطلب</asp:Literal>
                            <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector">
                                <asp:ListItem Value="0">نوع الطلب</asp:ListItem>
                                <asp:ListItem Value="1">جميع الطلبات</asp:ListItem>
                                <asp:ListItem Value="دعم">طلب دعم رسوم</asp:ListItem>
                                <asp:ListItem Value="مكافأة">طلب مكافأة</asp:ListItem>
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("RequestType").ToString()=="S"?"طلب دعم رسوم نشر بحث علمي":(Eval("RequestType").ToString()=="T"?"طلب مكافأة على نشر بحث علمي":(Eval("RequestType").ToString()=="NS"?"طلب دعم رسوم نشر بحث علمي غير مكتمل":"طلب مكافأة على نشر بحث علمي غير مكتمل"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="نوع الطلب">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("RequestType").ToString()=="S"?"طلب دعم رسوم نشر بحث علمي":(Eval("RequestType").ToString()=="T"?"طلب مكافأة على نشر بحث علمي":(Eval("RequestType").ToString()=="NS"?"طلب دعم رسوم نشر بحث علمي غير مكتمل":"طلب مكافأة على نشر بحث علمي غير مكتمل"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField HeaderText="حالة الطلب" DataField="RequestFinalStatus" />
                    <asp:TemplateField HeaderText="عرض الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">visibility</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="متابعة الطلب">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFollowUp" runat="server" OnClick="lnkFollowUp_Click" Width="100%"><i class="material-icons" style="color: #E34724;margin-top:5px">fast_rewind</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="FollowUpDiv" visible="false" runat="server" style="position: fixed; top: 15%; left: 10%; overflow-y: visible; font-size: small; margin: 1% auto; text-align: center; width: 75%; height: auto; border: 5px outset; background-color: white">
            <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click"><i class="material-icons" style="color: #E34724;margin-top:5px">clear</i></asp:LinkButton>
            <asp:GridView ID="GridView2" runat="server" EmptyDataText="لا يوجد طلبات حاليا" EmptyDataRowStyle-CssClass="noData"
                Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#921a1d" style="color:white"><tr><td>الحركات التي تمت على الطلب</td></tr></table>'
                CssClass="grd" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="من" DataField="ReqFromName" />
                    <asp:BoundField HeaderText="إلى" DataField="RquToName" />
                    <asp:BoundField HeaderText="تاريخ الارسال" DataField="RequestDate" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField HeaderText="حالة الطلب" DataField="ReqStatus" />
                    <asp:BoundField HeaderText="ملاحظات" DataField="Notes" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "100%" });
        }
    </script>

</asp:Content>
