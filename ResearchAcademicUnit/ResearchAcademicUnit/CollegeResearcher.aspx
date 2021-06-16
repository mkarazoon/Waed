<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CollegeResearcher.aspx.cs" Inherits="ResearchAcademicUnit.CollegeResearcher" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .selectCollege {
            float: right;
            margin: 0 auto 20px auto;
            width: 100%;
            box-sizing: border-box;
            display:block;
        }
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

        @media print {

            /* visible when printed */
            .selectCollege{
                display:none;
            }
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
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <div class="contentDiv" style="width: 98%; margin-top: 20px">
        <div style="float: right; box-sizing: border-box; padding: 10px; width: 100%">
            <div class="selectCollege" id="collegeDiv" runat="server">
                <div style="width: 100%; float: right">
                    <div class="lblDiv" style="width: 24.5%;">اسم الكلية</div>
                    <div class="lblContentDiv" style="width: 74.5%;text-align:right">
                        <asp:DropDownList ID="ddlResearcher" AutoPostBack="true" OnSelectedIndexChanged="ddlResearcher_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="ddlDept" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="ChosenSelector" runat="server"></asp:DropDownList>
                    </div>
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
    <div style="clear: both"></div>
    <div style="width: 98%; margin: 0 auto 0 auto">
        <div class="contentDiv" style="width: 48%; float: right; padding: 10px">
            <div style="width: 100%">
                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0px auto 10px auto">الأداء البحثي</div>
                <div style="width: 95%; margin: 0 auto 10px auto">
                    <table style="width: 100%; height: 300px; border-spacing: 0px; text-align: center; border-collapse: collapse; border-color: gray" border="1">
                        <tr style="background-color: #7f7f7f;color:white">
                            <td colspan="2" rowspan="2">النشاطات البحثية</td>
                            <td rowspan="2">عدد النشاطات البحثية</td>
                            <td colspan="2">التشاركية البحثية</td>
                        </tr>
                        <tr style="background-color: #7f7f7f;color:white">
                            <td class="auto-style2">النشاطات الفردية</td>
                            <td class="auto-style1">النشاطات الجماعية</td>
                        </tr>
                        <tr>
                            <td rowspan="3">البحوث العلمية</td>
                            <td class="auto-style3">بحث علمي</td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style3">بحث تاريخي</td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="auto-style3">افتتاحية مجلة</td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr style="background-color: white">
                            <td colspan="2">مجموع البحوث العلمية</td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label14" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label15" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">مشاركة مؤتمر</td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label18" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label19" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr style="background-color: white">
                            <td rowspan="3">النشاطات التأليفية</td>
                            <td class="auto-style3">وحدة</td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label22" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label23" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr style="background-color: white">
                            <td class="auto-style3">كتاب</td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label26" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label27" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr style="background-color: white">
                            <td class="auto-style3">ترجمة</td>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label30" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label31" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">مجموع النشاطات التأليفية</td>
                            <td>
                                <asp:Label ID="Label33" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label34" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label35" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr style="background-color: white">
                            <td colspan="2">المجموع الكلي</td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style2">
                                <asp:Label ID="Label38" runat="server" Text=""></asp:Label></td>
                            <td class="auto-style1">
                                <asp:Label ID="Label39" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="clear: both"></div>
        </div>

        <div class="contentDiv" style="width: 48%; float: left; padding: 10px">
            <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto">الاستمرارية البحثية</div>
            <div style="margin: 5px auto 5px auto">
                <asp:Chart ID="Chart1" runat="server" SuppressExceptions="True" Height="250px" Style="width: 100%" EnableViewState="True" Visible="False">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Line" IsValueShownAsLabel="True" BorderWidth="3" Font="Microsoft Sans Serif, 10pt, style=Bold"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY Enabled="False">
                                <MajorGrid Enabled="False" />
                                <MajorTickMark Enabled="False" />
                            </AxisY>
                            <AxisX Interval="1" TextOrientation="Rotated90">
                                <MajorGrid Enabled="False" />
                                <MajorTickMark Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <div id="chart_div" style="width: 100%; height: 250px;display:inline-block"></div>
                <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
            </div>
            <div runat="server" id="colDiv">
                <div class="lblDiv" style="width: 74.5%; border-radius: 0px; box-sizing: border-box; margin: 0 auto 10px auto">عدد الأبحاث العام الحالي</div>
                <div class="lblContentDiv" style="width: 24.5%; border-radius: 0px" id="currentDiv" runat="server">
                    <asp:Label ID="lblCurrentR" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div style="clear: both"></div>
            <div runat="server" id="coldeptDiv" visible="false" style="width:100%">
                <div style="width: 49.5%; float: right">
                    <div class="lblDiv" style="width: 74%;border-radius:0px; box-sizing: border-box; margin: 0 auto 10px auto">عدد الأبحاث العام الحالي للكلية</div>
                    <div class="lblContentDiv" style="width: 24%;border-radius:0px;" id="currentDiv1" runat="server">
                        <asp:Label ID="lblCurrentRCollege" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 49.5%; float: right">
                    <div class="lblDiv" style="width: 74%;border-radius:0px; box-sizing: border-box; margin: 0 auto 10px auto">عدد الأبحاث العام الحالي للقسم</div>
                    <div class="lblContentDiv" style="width: 24%;border-radius:0px;" id="currentDiv2" runat="server">
                        <asp:Label ID="lblCurrentRDept" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="clear: both"></div>
            </div>
            <div style="clear: both"></div>
            <div runat="server" id="curDiv">
            </div>
            <%--            <div>
                <div class="lblDiv" style="width: 70%; box-sizing: border-box; margin: 0 auto 10px auto">عدد الأبحاث المتوقع للعام القادم</div>
                <div class="lblContentDiv" style="width: 23%">
                    <asp:Label ID="lblNextR" runat="server" Text="1"></asp:Label>
                </div>
            </div>--%>
        </div>
    </div>
    <div style="clear: both"></div>
    <div style="width: 98%; margin: 0 auto 0 auto">
        <div class="contentDiv" style="width: 30%; float: right; padding: 10px">
            <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto">التنوع والشمولية البحثية</div>
            <div style="margin: 5px auto 5px auto">
                <asp:Chart ID="Chart2" runat="server" Height="187px" Style="width: 100%" Visible="False">
                    <Series>
                        <asp:Series Name="serie" ChartType="Pie" IsValueShownAsLabel="True" LabelFormat="#.##%"
                            XValueType="String" YValueType="Double" Legend="Legend1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX Interval="1" TextOrientation="Stacked">
                            </AxisX>
                            <Area3DStyle Enable3D="True" IsClustered="True" />
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Alignment="Center" DockedToChartArea="ChartArea1" HeaderSeparator="Line"
                            IsDockedInsideChartArea="False" Name="Legend1" Enabled="True" LegendStyle="Table" Font="Arial, 10pt" IsTextAutoFit="False">
                        </asp:Legend>
                    </Legends>
                </asp:Chart>
                <div id="piechartDiversity" style="width: 100%; height: 187px; float: left"></div>
            </div>
            <div runat="server" visible="false" id="per1">
                <div class="lblDiv" style="width: 75%; box-sizing: border-box; margin: 0 auto 10px auto">نسبة التنوع البحثي</div>
                <div class="lblContentDiv" style="width: 17%; float: left">
                    <asp:Label ID="lblVariantRPer" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div runat="server" visible="false" id="per2">
                <div class="lblDiv" style="width: 75%; box-sizing: border-box; margin: 0 auto 10px auto">نسبة الشمولية البحثية</div>
                <div class="lblContentDiv" style="width: 17%; float: left">
                    <asp:Label ID="lblComprehensiveRPer" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>

        <%--        <div class="contentDiv" style="width: 62%; float: left; padding: 10px">--%>
        <div class="contentDiv" style="width: 67%; float: left; padding: 10px">
            <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto">الجودة البحثية</div>
            <div style="float: right; margin: 0 auto 10px auto; width: 100%; box-sizing: border-box">
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">مؤشر الإنتاجية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblProductivity" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">مجموع الاستشهاد</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblEAvg" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">متوسط نسبة الدعم والمكافآت</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblSupportPer" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
            </div>
            <div style="clear: both"></div>
            <div style="float: right; margin: 0 auto 10px auto; width: 100%; box-sizing: border-box">
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">نسبة التشاركية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblParticipatPer" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">نسبة النشر للعام الحالي للكلية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblPublishAvgCollege" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div runat="server" visible="false" id="deptDiv">
                    <div style="width: 33%; float: right">
                        <div class="lblDiv" style="width: 45%;">نسبة النشر للعام الحالي للقسم</div>
                        <div class="lblContentDiv" style="width: 45%;">
                            <asp:Label ID="lblPublishAvgDept" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="clear: both"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div style="clear: both"></div>


    <script>
        $('.ChosenSelector').chosen({ width: "25%" });
    </script>
</asp:Content>
