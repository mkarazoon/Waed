<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FeeSetting.aspx.cs" Inherits="ResearchAcademicUnit.FeeSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentDiv" runat="server" id="Div3" style="width: 48%; float: right; height: auto">
        <div style="padding: 20px; box-sizing: border-box">
            <div style="margin-bottom: 20px">
                <div class="lblDiv">
                    الصلاحيات
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div class="lblDiv" style="width: 25%">
                <asp:Label ID="Label6" runat="server" Text="مستوى الصلاحية" Font-Size="12px"></asp:Label>
            </div>
            <div>
                <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                    <asp:DropDownList ID="ddlAuthLevel" runat="server" CssClass="ChosenSelector1" Width="95%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAuthLevel_SelectedIndexChanged">
                        <asp:ListItem Value="0">تحديد مستوى الصلاحية</asp:ListItem>
                        <asp:ListItem Value="4">رئيس الجامعة</asp:ListItem>
                        <asp:ListItem Value="5">نائب رئيس الجامعة</asp:ListItem>
                        <asp:ListItem Value="1">عميد</asp:ListItem>
                        <asp:ListItem Value="2">رئيس قسم</asp:ListItem>
                        <asp:ListItem Value="3">مساعد اداري</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="UploadA" ControlToValidate="ddlAuthLevel"></asp:RequiredFieldValidator>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div>
                <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                    <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="ChosenSelector1" Width="95%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div>
                <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="ChosenSelector1" Width="95%">
                    </asp:DropDownList>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div>
                <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                    <asp:DropDownList ID="ddlName" runat="server" CssClass="ChosenSelector1" Width="95%">
                    </asp:DropDownList>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div>
                <div class="lblContentDiv" style="width: 74%; margin-bottom: 10px">
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="البريد الإلكتروني"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="خطأ بالبريد الالكتروني" Display="Dynamic"
                        ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div style="box-sizing: border-box; text-align: center; margin-top: 10px">
                <asp:Button ID="btnSaveAuth" OnClientClick="return confirm('هل أنت متأكد من تحميل البيانات؟ سيتم حذف البيانات السابقة؟')" ValidationGroup="UploadA" Style="margin-left: 20px; margin-right: 20px" runat="server" Text="حفظ" OnClick="btnSaveAuth_Click" CssClass="btn" />
            </div>
        </div>
        </div>
    <div class="contentDiv" runat="server" id="Div1" style="width: 48%; float: right; height: auto">
        <div style="padding: 20px; box-sizing: border-box">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                CssClass="grd" OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="صاحب الصلاحية" />
                    <asp:BoundField DataField="PrivName" HeaderText="اسم الصلاحية" />
                    <asp:BoundField DataField="College" HeaderText="الكلية" />
                    <asp:BoundField DataField="deptname" HeaderText="القسم" />
                    <asp:BoundField DataField="PrivTo" HeaderText="الرقم الوظيفي" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <script>
        $('.ChosenSelector').chosen({ width: "40%" });
        $('.ChosenSelector1').chosen({ width: "95%", rtl: true });
    </script>

</asp:Content>
