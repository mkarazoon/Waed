<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CollegeInfo.aspx.cs" Inherits="ResearchAcademicUnit.CollegeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*.grd {
            text-align: center;
            -moz-border-radius: 20px;
            border-radius: 20px;
            Border:1px solid black;
        }
                .grd tr td{
            padding:8px;
            
        }*/

        .hidden {
            display: none;
        }

        @media print {

            /* visible when printed */

            .printDiv {
                background-color: #f5f5f5;
            }

            .curDiv {
                color: black;
            }

            .lbl {
                display: block;
            }

            .ddlShow {
                display: none;
            }

            .grd tr td {
                color: black;
            }

            .btn {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentDiv" style="width: 98%; margin-top: 20px">
        <div style="float: right; box-sizing: border-box; padding: 10px; width: 100%">
            <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                <div class="ddlShow">
                    <div id="collegediv" runat="server">
                    <div class="lblDiv" style="width: 15%;">اسم الكلية</div>
                    <div class="lblContentDiv" style="width: 44%; text-align: right">
                        <asp:DropDownList ID="ddlResearcher" AutoPostBack="true" OnSelectedIndexChanged="ddlResearcher_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddlDept" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                        <asp:Button ID="Button1" runat="server" Text="طباعة الكل" OnClick="Button1_Click" CssClass="btn" Visible="False" />
                    </div>
                        </div>
                    <div class="lblDiv" style="width: 15%;">عرض البيانات</div>
                    <div class="lblContentDiv" style="width: 25%; text-align: right">
                        <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="ChosenSelector" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                            <asp:ListItem Value="0">اختيار...</asp:ListItem>
                            <asp:ListItem Value="ReType">نوع النشاط</asp:ListItem>
                            <asp:ListItem Value="ReYear" Selected="True">سنة النشر</asp:ListItem>
                            <asp:ListItem Value="ReCitation">الاستشهادات</asp:ListItem>
                            <asp:ListItem Value="MClassInt">التصنيف</asp:ListItem>
                            <asp:ListItem Value="CitationAvg">معدل الاستشهادات</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSortOrder" runat="server" CssClass="ChosenSelector" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSortOrder_SelectedIndexChanged">
                            <asp:ListItem Value="ASC">تصاعدي</asp:ListItem>
                            <asp:ListItem Value="DESC" Selected="True">تنازلي</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <%--                <div style="clear: both"></div>
                <div class="print-only">
                    <div style="float: right; margin: 0 auto 0px auto; width: 100%; box-sizing: border-box">
                        <div class="lblDiv" style="width: 25%">اسم الكلية</div>
                        <div class="lblContentDiv" style="width: 74%; text-align: right">
                            <asp:Label ID="lblCollegeInfo" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div style="clear: both"></div>
            <div style="float: right; margin: 0 auto 0px auto; width: 100%; box-sizing: border-box">
                <div style="width: 25%; float: right">
                    <div class="lblDiv" style="width: 45%;">الكلية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblRCollege" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 25%; float: right">
                    <div class="lblDiv" style="width: 45%;">القسم</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblRDept" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 25%; float: right">
                    <div class="lblDiv" style="width: 45%;">تصنيف الكلية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblROrder" runat="server" Text="غير متاح حاليا"></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 25%; float: right">
                    <div class="lblDiv" style="width: 45%;">التاريخ</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
            </div>
        </div>
        <div style="clear: both"></div>
    </div>
    <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box" dir="rtl">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <div class="contentDiv" style="padding: 20px; width: 98%" id="infoDiv" runat="server" visible="false">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
            Width="100%" 
            OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
            CssClass="grd" AllowPaging="True">
            <Columns>
                <asp:BoundField HeaderText="الرقم" DataField="row_num" HeaderStyle-Width="4%" />
                <asp:BoundField HeaderText="عنوان النشاط البحثي" DataField="ReTitle" HeaderStyle-Width="30%" />
                <asp:BoundField HeaderText="نوع النشاط" DataField="ReType" HeaderStyle-Width="7%" />
                <asp:BoundField HeaderText="مستوى النشاط" DataField="ReLevel" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="سنة النشر" DataField="ReYear" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="الاستشهادات" DataField="ReCitation" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="التشاركية البحثية" DataField="ReParticipate" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="عنوان المصدر البحثي" DataField="Magazine" HeaderStyle-Width="15%" />
                <asp:BoundField HeaderText="نوع المصدر" DataField="SourceType" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="التصنيف" DataField="MClass" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="معدل الاستشهاد" DataField="CitationAvg" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="دعم داخلي" DataField="InSupport" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="مكافأة داخلية" DataField="Reward" HeaderStyle-Width="5%" />
                <asp:BoundField HeaderText="رمز البحث" DataField="ReId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:TemplateField HeaderText="الباحث">
                    <ItemTemplate>
                        <div runat="server" id="ReInfoDiv">
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    

    <script>
        $('.ChosenSelector').chosen({ width: "40%" });
    </script>
</asp:Content>
