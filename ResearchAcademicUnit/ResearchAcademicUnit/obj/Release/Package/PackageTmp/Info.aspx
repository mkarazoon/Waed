<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="ResearchAcademicUnit.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .printDiv {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 15px;
        }

        .lbl {
            display: none;
        }
        /*.grd{
            text-align:center;
            Border:1px solid black;
        }
                .grd tr td{
            padding:8px;
            
        }*/

        .ddlShow {
            display: block;
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

            .grd tr td{
                text-align: center;
                color: black;
            }
            .btn{
                display:none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" class="printDiv">
        <div class="contentDiv" style="width: 99.5%;">
            <div style="float: right; box-sizing: border-box; padding: 10px; width: 12%">
                <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                    <div class="lblDiv">الرقم البحثي</div>
                </div>
                <div style="clear: both"></div>
                <div style="float: right; margin: 10px auto 0px auto; width: 100%; box-sizing: border-box">
                    <div class="lblContentDiv">
                        <asp:Label ID="lblRNo" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div style="float: right; box-sizing: border-box; border-right: 1px solid #8A91A1; padding: 10px; width: 88%">
                <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                    <div style="width: 50%; float: right">
                        <div class="lblDiv" style="width: 24.5%;">اسم الباحث</div>
                        <div class="lblContentDiv" style="width: 74.5%; text-align: right">
                            <div class="ddlShow">
                                <asp:DropDownList ID="ddlCollege" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="ChosenSelector" ></asp:DropDownList>
                                <asp:DropDownList ID="ddlResearcher" AutoPostBack="true" OnSelectedIndexChanged="ddlResearcher_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                                <asp:Button ID="Button1" runat="server" Text="طباعة الكل" OnClick="Button1_Click" CssClass="btn" />
                            </div>
                            <div class="lbl" runat="server" id="nameDiv">
                                <asp:Label ID="lblRName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <div style="width: 25%; float: right">
                        <div class="lblDiv" style="width: 45%;">الرتبة الأكاديمية</div>
                        <div class="lblContentDiv" style="width: 45%;">
                            <asp:Label ID="lblRDegree" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <div style="width: 25%; float: right">
                        <div class="lblDiv" style="width: 45%;">تاريخ التعيين</div>
                        <div class="lblContentDiv" style="width: 45%;">
                            <asp:Label ID="lblRHireDate" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="clear: both"></div>
                    </div>
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
                        <div class="lblDiv" style="width: 45%;">تصنيف الباحث</div>
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
                <div style="clear: both"></div>
            </div>
            <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear: both"></div>

            <div style="clear: both"></div>
        </div>
        <div class="contentDiv" style="padding: 20px" id="infoDiv" runat="server" visible="false">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                Width="100%"
                CssClass="grd">

                <Columns>
                    <asp:BoundField HeaderText="الرقم" DataField="row_num" HeaderStyle-Width="4%" />
                    <asp:BoundField HeaderText="عنوان النشاط البحثي" DataField="ReTitle" HeaderStyle-Width="30%" />
                    <asp:BoundField HeaderText="نوع النشاط" DataField="ReType" HeaderStyle-Width="7%" />
                    <asp:BoundField HeaderText="مستوى النشاط" DataField="ReLevel" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="سنة النشر" DataField="ReYear" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="حالة البحث" DataField="ReStatus" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="الاستشهادات" DataField="ReCitation" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="التشاركية البحثية" DataField="ReParticipate" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="عنوان المصدر البحثي" DataField="Magazine" HeaderStyle-Width="15%" />
                    <asp:BoundField HeaderText="نوع المصدر" DataField="SourceType" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="التصنيف" DataField="MClass" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="معدل الاستشهاد" DataField="CitationAvg" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="دعم داخلي" DataField="InSupport" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="مكافأة داخلية" DataField="Reward" HeaderStyle-Width="5%" />
                </Columns>
            </asp:GridView>
        </div>
        <div style="clear: both"></div>
    </div>
    

    <script>
        $('.ChosenSelector').chosen({ width: "35%" });
    </script>
</asp:Content>
