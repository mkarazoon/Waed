<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Researcher.aspx.cs" Inherits="ResearchAcademicUnit.Researcher" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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

            .printDiv{
                background-color:#f5f5f5;
            }
            .curDiv{
                color:black;
            }
            .lbl{
                display:block;
            }
            .ddlShow{
            display:none;
        }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <div dir="rtl" class="printDiv">
        <div class="contentDiv">
            <div style="float: right; box-sizing: border-box; padding: 10px; width: 12%">
                <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                    <div class="lblDiv">الرقم البحثي</div>
                </div>
                <div style="clear: both"></div>
                <div style="float: right; margin: 10px auto 0px auto; width: 100%; box-sizing: border-box">
                    <div class="lblContentDiv">
                        <asp:Label ID="lblRNo" runat="server" Text="" Style="font-size: 100%; box-sizing: border-box"></asp:Label>
                    </div>
                </div>
            </div>
            <div style="float: right; box-sizing: border-box; border-right: 1px solid #8A91A1; padding: 10px; width: 88%">
                <div style="float: right; margin: 0 auto 20px auto; width: 100%; box-sizing: border-box">
                    <div style="width: 50%; float: right">
                        <div class="lblDiv" style="width: 30%;">اسم الباحث</div>
                        <div class="lblContentDiv" style="width: 65%; text-align: right">
                            <div class="ddlShow">
                                <asp:DropDownList ID="ddlCollege" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="ChosenSelector" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlResearcher" Width="45%" AutoPostBack="true" OnSelectedIndexChanged="ddlResearcher_SelectedIndexChanged"
                                CssClass="ChosenSelector" runat="server"
                                Style="font-size: 100%; box-sizing: border-box">
                            </asp:DropDownList>
                                </div>
                            <div class="lbl" runat="server" id="nameDiv">
                            <asp:Label ID="lblRName" runat="server" ></asp:Label>
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
            </div>
            <div style="clear: both"></div>
        </div>
        <div style="clear: both"></div>
        <div class="contentDiv" style="width: 48%; float: right; padding: 10px">
            <div style="width: 100%">
                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0px auto 10px auto">الأداء البحثي</div>
                <div style="width: 95%; margin: 0 auto 10px auto">
                    <table dir="rtl" style="width: 100%; height: 300px; border-spacing: 0px; text-align: center;
                                            border-collapse: collapse; border-color: gray; border: 1px solid" border="1">
                        <tr style="background-color: #7f7f7f;color:white">
                            <td colspan="2" rowspan="2" style="word-wrap:break-word;">النشاطات البحثية</td>
                            <td rowspan="2" style="word-wrap:break-word;">عدد النشاطات البحثية</td>
                            <td colspan="2" style="word-wrap:break-word;">التشاركية البحثية</td>
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
            <div style="width:95%;margin: 5px auto 5px auto">
                <asp:Chart ID="Chart1" runat="server" SuppressExceptions="True" Style="width: 100%;height:180px;" EnableViewState="True" Visible="False">
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
            <div style="width: 100%;">
                <div class="lblDiv" style="width: 74.5%; box-sizing: border-box; margin: 0 auto 10px auto;border-radius:0px">عدد الأبحاث العام الحالي</div>
                <div class="lblContentDiv" style="width: 24.5%; border-radius:0px" id="currentDiv" runat="server">
                    <asp:Label ID="lblCurrentR" runat="server" Text=""></asp:Label>
                </div>
                <div style="clear: both"></div>
            </div>
            <div style="clear: both"></div>
            <div runat="server" id="curDiv" style="width:100%" class="curDiv">
            </div>
        </div>
        <div style="clear: both"></div>

        <div class="contentDiv" style="width: 30%; float: right; padding: 10px">

            <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto">التنوع والشمولية البحثية</div>
            <div style="margin: 5px auto 5px auto">
                <asp:Chart ID="Chart2" runat="server" Height="187px" Style="width: 100%" Visible="False">
                    <Series>
                        <asp:Series Name="serie" ChartType="Pie" IsValueShownAsLabel="True" LabelFormat="#.#%"
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
                        <asp:Legend Alignment="Center"  DockedToChartArea="ChartArea1" HeaderSeparator="Line"
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

        <div class="contentDiv" style="width: 67%; float: left; padding: 10px">
            <div class="lblDiv" style="margin: 0 auto 10px auto">الجودة البحثية</div>
            <div style="float: right; margin: 0 auto 10px auto; width: 100%; box-sizing: border-box">
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">مؤشر الإنتاجية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblProductivity" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">معدل الاستشهاد</div>
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
                    <div class="lblContentDiv" style="width: 45%;margin-bottom: 10px">
                        <asp:Label ID="lblParticipatPer" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
<%--                <div style="clear: both"></div>--%>

                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">نسبة النشر للعام الحالي/الجامعة</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblPublishAvgUni" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>
                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">نسبة النشر للعام الحالي/الكلية</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblPublishAvgCol" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>

                <div style="width: 33%; float: right">
                    <div class="lblDiv" style="width: 45%;">نسبة النشر للعام الحالي/القسم</div>
                    <div class="lblContentDiv" style="width: 45%;">
                        <asp:Label ID="lblPublishAvgDept" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="clear: both"></div>
                </div>

            </div>
        </div>


        <div style="clear: both"></div>

    </div>
    <script>
        $('.ChosenSelector').chosen(
            { width: "45%" });
    </script>

</asp:Content>
