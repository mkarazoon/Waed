<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupportAwardInfo.aspx.cs" Inherits="ResearchAcademicUnit.SupportAwardInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .grd {
            text-align: center;
            color: black;
            Border: 1px solid black;
        }

            .grd tr td {
                padding: 8px;
            }

        .hidden {
            display: none;
        }

        .printDiv {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 1%;
            border-radius: 15px;
        }

        .filter {
            width: 20%;
            float: right;
            /*border: 5px outset #921a1d;
            border-top-right-radius: 10px;
            border-bottom-right-radius: 10px;*/
            padding: 1%;
            direction: rtl;
        }


        .cont {
            margin: 1% auto 1% auto;
            width: 90%;
        }

        .left-div {
            float: left;
            width: 75%;
            padding: 1%;
        }

        @media print {
            body {
                background-color: white;
            }

            .grd {
                page-break-after: always;
            }

                .grd tr td {
                    text-align: center;
                    color: black;
                    padding: 5px;
                }

            .filter {
                display: none;
            }

            .left-div {
                float: none;
                width: 100%;
            }

            .btn {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cont">
        <div class="TitleDiv">
            تقرير الدعم والمكافأة
        </div>
        <div style="width: 90%; margin: 0 auto">
            <div class="filter">
                <asp:Label ID="Label1" runat="server" Text="نوع التقرير"></asp:Label><br />
                <asp:DropDownList ID="ddlType" runat="server" CssClass="ChosenSelector">
                    <asp:ListItem Value="-1">حدد الطلب</asp:ListItem>
                    <asp:ListItem Value="0">طلب دعم رسوم نشر بحث علمي</asp:ListItem>
                    <asp:ListItem Value="1">طلب مكافأة على نشر بحث علمي</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlType" Display="Dynamic" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblP" runat="server" Text="الفترة"></asp:Label><br />
                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector" OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlFromYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <br />
                <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlFromMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <br />
                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlToYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <br />
                <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlToMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                <asp:Button ID="btnApply" runat="server" Text="بحث" OnClick="btnApply_Click" CssClass="btn" />
                <asp:Button ID="Button1" runat="server" Text="طباعة" OnClick="Button1_Click" CssClass="btn" />
                <asp:Button ID="btnExport" runat="server" Text="اكسل" OnClick="btnExport_Click" CssClass="btn"/>
                <asp:Button ID="btnAll" runat="server" Text="جميع الابحاث" OnClick="btnAll_Click" CssClass="btn" CausesValidation="false"/>
            </div>
            <div class="print-only">
                <asp:Label ID="Label2" runat="server" Text="نوع التقرير"></asp:Label>
                <asp:Label ID="lblType" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="Label3" runat="server" Text="الفترة"></asp:Label>
                <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
            </div>
            <div class="left-div">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="لا يوجد بيانات لهذه الفترة"
                    Width="100%" CssClass="grd" ShowFooter="true">
                    <Columns>
                        <asp:BoundField HeaderText="#" DataField="row_num" HeaderStyle-Width="2%" />
                        <asp:BoundField HeaderText="عنوان البحث" DataField="ReTitle" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" HeaderStyle-Width="30%" />
                        <asp:BoundField HeaderText="القيمة" DataField="FeeValue" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="الشهر" DataField="Month" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="السنة" DataField="Year" HeaderStyle-Width="10%" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                <%--        <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="margin-bottom:20px">
            <asp:Label ID="Label1" runat="server" Text="عرض حسب" Style="margin-right: 30px;"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" CssClass="ChosenSelector">
                <asp:ListItem Value="0">عرض العام الحالي</asp:ListItem>
                <asp:ListItem Value="1">عرض كل الابحاث</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="طباعة الكل" OnClick="Button1_Click" CssClass="btn" />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ForeColor="Black"
            Width="100%"
            OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
            AllowPaging="True" CssClass="grd">
            <Columns>
                <asp:BoundField HeaderText="الرقم" DataField="row_num" HeaderStyle-Width="4%" />
                <asp:BoundField HeaderText="رمز البحث" DataField="ReId" HeaderStyle-Width="4%" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:BoundField HeaderText="عنوان النشاط البحثي" DataField="ReTitle" HeaderStyle-Width="30%" />
                <asp:TemplateField HeaderText="الباحثين والجامعات" HeaderStyle-Width="50%">
                    <ItemTemplate>
                        <div runat="server" id="ReInfoDiv" style="text-align: left; direction: ltr">
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
            </div>
            <div style="clear: both"></div>
        </div>
    </div>
    <script>
        $('.ChosenSelector').chosen({ width: "100%" });
    </script>

</asp:Content>
