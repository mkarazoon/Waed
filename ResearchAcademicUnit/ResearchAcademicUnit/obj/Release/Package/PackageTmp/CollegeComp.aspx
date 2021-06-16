<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CollegeComp.aspx.cs" Inherits="ResearchAcademicUnit.CollegeComp" %>

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

        .printDiv {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 10px;
            border-radius:15px;
        }
        .lbl{
            display:none;
        }

        .ddlShow{
            display:block;
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
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" class="printDiv">
<%--        <div class="headerDiv" style="float: right; box-sizing: border-box; padding: 10px; width: 100%">
            <div style="float: right; margin: 0 auto 10px auto; width: 100%; box-sizing: border-box">
                <div>
                    <div class="lblDiv" style="width:100%;box-sizing:border-box;margin:10px auto 10px auto">الدعم والمكافأت</div>
                    <div style="clear: both"></div>
                    <div class="lblDiv" style="width:15%">عدد الأبحاث</div>
                    <div class="lblContentDiv" style="width:13%" id="RDiv" runat="server">
                        <asp:Label ID="lblRSupportPer" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="lblDiv" style="width:15%">عدد المؤتمرات</div>
                    <div class="lblContentDiv" style="width:13%" id="CDiv" runat="server">
                        <asp:Label ID="lblCSupportPer" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="lblDiv" style="width:15%">عدد النشاطات التأليفية</div>
                    <div class="lblContentDiv" style="width:13%" id="PDiv" runat="server">
                        <asp:Label ID="lblPSupportPer" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: both"></div>--%>
        <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
            <asp:Label ID="lblMsg" runat="server" Text="لا يوجد معلومات حاليا"></asp:Label>
        </div>
        <div class="headerDiv" style="padding: 20px; box-sizing: border-box" runat="server" id="dataDiv" visible="false">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                Width="100%" HeaderStyle-Height="40px"
                CssClass="grd"
                OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField DataField="College" HeaderText="اسم الكلية" />
                    <asp:BoundField DataField="EmpCount" HeaderText="عدد أعضاء هيئة التدريس" />
                    <asp:BoundField DataField="total" HeaderText="عدد الأبحاث للكلية" />
                    <asp:BoundField DataField="CollegePer" HeaderText="نسبة الأداء البحثي للكلية" DataFormatString="{0:P0}"/>
                    <asp:BoundField DataField="RName" HeaderText="الباحث" />
                    <asp:BoundField DataField="Ptotal" HeaderText=" عدد الأبحاث المنشورة للباحث" />
                    <asp:BoundField DataField="Atotal" HeaderText=" عدد الأبحاث المقبولة للنشر للباحث" />
                </Columns>
            </asp:GridView>
            <div style="clear: both"></div>
        </div>
        <p class="print-only">
            <asp:Label ID="lblNote" runat="server" Text=""></asp:Label></p>
    </div>
</asp:Content>
