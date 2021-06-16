<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CollegeAbstract.aspx.cs" Inherits="ResearchAcademicUnit.CollegeAbstract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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

        .ddlShow {
            display: block;
        }

        /*.grd {
            text-align: center;
            -moz-border-radius: 20px;
            border-radius: 20px;
            Border:1px solid black;
        }
         
        .grd tr td{
            padding:8px;
        }*/

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
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" class="printDiv">
        <div class="headerDiv" style="float: right; box-sizing: border-box; padding: 10px; width: 100%">
            <div class="ddlShow">
                <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                    <div class="lblDiv" style="width: 25%">اسم الكلية</div>
                    <div class="lblContentDiv" style="width: 74%; text-align: right">
                        <asp:DropDownList ID="ddlResearcher" AutoPostBack="true" OnSelectedIndexChanged="ddlResearcher_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddlDept" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
            <div class="print-only">
                <div style="float: right; margin: 0 auto 0px auto; width: 100%; box-sizing: border-box">
                    <div class="lblDiv" style="width: 25%">اسم الكلية</div>
                    <div class="lblContentDiv" style="width: 74%; text-align: right">
                        <asp:Label ID="lblCollegeInfo" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both"></div>
        <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
            <asp:Label ID="lblMsg" runat="server" Text="لا يوجد معلومات حاليا"></asp:Label>
        </div>
        <div class="headerDiv" style="padding: 20px; box-sizing: border-box" runat="server" id="dataDiv" visible="false">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                Width="100%" HeaderStyle-Height="40px"
                CssClass="grd">
                <Columns>
                    <asp:BoundField DataField="row_num" HeaderText="الرقم" />
                    <asp:BoundField DataField="REName" HeaderText="اسم الباحث" />
                    <asp:BoundField DataField="RCount" HeaderText="عدد الأبحاث العلمية" />
                    <asp:BoundField DataField="CCount" HeaderText="عدد مشاركات المؤتمرات" />
                    <asp:BoundField DataField="PCount" HeaderText="عدد النشاطات التأليفية" />
                    <asp:BoundField DataField="Total" HeaderText="المجموع" />
                </Columns>
            </asp:GridView>
            <div style="clear: both"></div>
        </div>
    </div>
    <script>
        $('.ChosenSelector').chosen({ width: "25%" });
    </script>

</asp:Content>
