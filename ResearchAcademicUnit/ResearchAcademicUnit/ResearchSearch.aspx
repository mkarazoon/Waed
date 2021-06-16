<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ResearchSearch.aspx.cs" Inherits="ResearchAcademicUnit.ResearchSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
    <link href="css/materialdesignicons.min.css" rel="stylesheet" />
    <%--<link href="css/mono.css" rel="stylesheet" />--%>
    <style>
        table {
            width: 100%;
            border-spacing: 5px 5px;
        }

            table input[type=text] {
                width: 100%;
                display: inline;
                box-sizing: border-box;
            }

        .validcss {
            color: red;
            font-size: large;
            font-weight: bold;
        }

        .auto-style1 {
            font-size: xx-large;
        }

        .auto-style2 {
            font-size: x-large;
        }

        .td {
            width: 16.67%;
            /*text-align: center;*/
        }

            .td span {
                background-color: #921a1d !important;
                color: white;
                /*text-align: right;*/
                /*float:right;*/
                margin: auto;
                padding: 10px 30px 10px 30px;
                border-top-left-radius: 15px;
                border-top-right-radius: 15px;
                font-size: large;
            }

        .divNewStyle {
            border: 5px outset #921a1d;
        }

        .auto-style3 {
            height: 159px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="margin: 0px auto; padding: 2%;">
        <asp:UpdatePanel ID="upAbstract" runat="server">
            <ContentTemplate>
                <div id="UpdateDiv" class="headerDiv">
                    <table style="width: 100%; border-collapse: collapse; border-spacing: 0">
                        <tr>
                            <td class="td"><span><i class="material-icons">search</i></span></td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="lnk" CausesValidation="false">استعلام عن الابحاث</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="searchDiv" runat="server">
                                    <asp:TextBox ID="txtSearch" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    <asp:LinkButton ID="lnkClearSearch" runat="server" ToolTip="مسح" OnClick="lnkClearSearch_Click" CausesValidation="false"><span><i class="material-icons">clear</i></span></asp:LinkButton>
                                    <asp:LinkButton ID="lnkMainSearch" runat="server" ToolTip="بحث" OnClick="lnkMainSearch_Click" CausesValidation="false"><span><i class="material-icons">search</i></span></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div runat="server" id="AllResearchDiv" class="divNewStyle" visible="false">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn" Text="تنزيل" OnClick="Button1_Click" Visible="false" CausesValidation="false" />
                                    <asp:GridView ID="GridView1" runat="server" EmptyDataText="لا يوجد معلومات حاليا"
                                        Caption="معلومات الابحاث" AutoGenerateColumns="false" CssClass="grd">
                                        <Columns>
                                            <asp:BoundField HeaderText="رمز الباحث" DataField="ReId" />
                                            <asp:BoundField HeaderText="عنوان البحث" DataField="ReTitle" />
                                            <asp:BoundField HeaderText="ملخص البحث" DataField="ReAbstract" />
                                            <asp:TemplateField HeaderText="ملخص البحث">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbstract" runat="server" Text='<%# Eval("ReAbstract1").ToString() %>'></asp:Label>
                                                    <asp:LinkButton ID="lnkMore" runat="server" OnClick="lnkMore_Click" CausesValidation="false">more</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="نوع النشاط" DataField="ReType" />
                                            <asp:BoundField HeaderText="مستوى النشاط" DataField="ReLevel" />
                                            <asp:BoundField HeaderText="السنة" DataField="ReYear" />
                                            <asp:BoundField HeaderText="الشهر" DataField="ReMonth" />
                                            <asp:BoundField HeaderText="حالة البحث" DataField="ReStatus" />
                                            <asp:BoundField HeaderText="استشهادات البحث" DataField="ReCitation" />
                                            <asp:BoundField HeaderText="التشاركية" DataField="ReParticipate" />
                                            <asp:BoundField HeaderText="حالة الفريق البحثي" DataField="TeamType" />
                                            <asp:BoundField HeaderText="الباحثين مع الجامعات" DataField="Aff_Auther" />
                                            <asp:BoundField HeaderText="قطاعات البحث ومجالاته" DataField="ReSector" />
                                            <asp:BoundField HeaderText="المجال البحثي" DataField="ReField" />
                                            <asp:BoundField HeaderText="عنوان المجلة" DataField="Magazine" />
                                            <asp:BoundField HeaderText="ISSN" DataField="ISSN" />
                                            <asp:BoundField HeaderText="نوع المصدر البحثي" DataField="SourceType" />
                                            <asp:BoundField HeaderText="الناشر" DataField="Author" />
                                            <asp:BoundField HeaderText="تصنيف المجلة" DataField="MClass" />
                                            <asp:BoundField HeaderText="Top 10" DataField="TopMag" />
                                            <asp:BoundField HeaderText="معدل الاستشهاد" DataField="CitationAvg" />
                                            <asp:BoundField HeaderText="قطاع المجلة" DataField="MagVector" />
                                            <asp:BoundField HeaderText="مجال المجلة" DataField="MagField" />
                                            <asp:BoundField HeaderText="دعم داخلي" DataField="InSupport" />
                                            <asp:BoundField HeaderText="اسم المدعوم" DataField="" />
                                            <asp:BoundField HeaderText="مكافأة داخلية" DataField="Reward" />
                                            <asp:BoundField HeaderText="اسم صاحب المكافأة" DataField="" />
                                            <asp:BoundField HeaderText="دعم خارجي" DataField="OutSupport" />
                                            <asp:BoundField HeaderText="الكلي" DataField="TotalR" />
                                            <asp:BoundField HeaderText="الداخلي" DataField="TotalRIn" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="رمز الباحث" DataField="RID" />
                                                            <asp:BoundField HeaderText="اسم الباحث" DataField="RaName" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
            </Triggers>
        </asp:UpdatePanel>


    </div>

    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "100%" });

        }
    </script>
</asp:Content>
