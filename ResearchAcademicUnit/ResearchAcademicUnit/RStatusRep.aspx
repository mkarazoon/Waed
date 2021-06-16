<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RStatusRep.aspx.cs" Inherits="ResearchAcademicUnit.RStatusRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body {
            height: 100%;
            margin: 0;
        }

        .page {
            page-break-after: always;
            /*page-break-inside: avoid;*/
            background-color: #e8ebeb;
            text-align: center;
            direction: ltr;
            width: 98%;
            /*height: 100%;*/
            margin: 0 auto;
            font-size: 18px;
            /*padding:5px;*/
            font-family: Calibri;
            /*display:block;*/
        }

        .tit {
            background-color: #d9971a;
            padding: 5px;
            margin: 10px auto;
            color: white;
            font-size: 40px;
            font-family: Calibri;
        }

        .progress {
            overflow-y: auto;
            position: fixed;
            z-index: 100;
            width: 100%;
            height: 100%;
            background-color: black;
            opacity: 0.5;
            font-size: .75rem;
            border-radius: .25rem;
            direction: ltr;
        }

            .progress img {
                position: relative;
                top: 10%;
                left: 40%;
                height: 100px;
            }

        .chart {
            width: 100%;
            height: 100%;
        }

        @media print {
            body {
                background-color: white;
            }

            .contentDiv2 {
                display: none;
            }

            .chart {
                zoom: 1.3;
                margin: 3px;
            }

            .grd {
                font-size: 24px;
            }

            .print {
                zoom: 2;
            }

            .zoom1-2 {
                zoom: 1.5;
            }

            .zoom1-3 {
                zoom: 1.3;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true"
        runat="server">
        <ProgressTemplate>
            <div class="progress">
                please wait...
                <img src="images/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>

    <%--    <asp:UpdatePanel ID="getDataPanel" runat="server">
        <ContentTemplate>--%>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <div class="contentDiv2" style="direction: ltr; width: 98%; margin: 0 auto 0 auto; border: 1px solid">
        <asp:Label ID="lblP" runat="server" Text="Period"></asp:Label>
        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector" OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlFromYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlFromMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlToYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlToMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:Button ID="btnApply" runat="server" Text="Apply" OnClick="btnApply_Click" />
    </div>

    <div id="AllDivs" runat="server" visible="false">
        <div class="page print">
            <div style="margin: 0px auto;">
                <img src="images/Middle_East_University_logo.png" style="width: 256px" />
            </div>
            <div style="font-size: 50px; font-weight: bold">
                DEANSHIP OF GRADUATE STUDIES AND SCIENTIFIC RESEARCH
            </div>
            <div style="font-size: 50px; font-weight: bold">
                SCIENTIFIC RESEARCH DEPARTMENT
            </div>
            <div class="tit" style="width: 70%">
                MEU RESEARCH STATUS
            </div>
            <div style="margin-top: 50px; font-size: 20px; font-weight: bold; text-align: left; color: white; width: 100%">
                <table>
                    <tr>
                        <td rowspan="2" style="padding: 25px; background-color: #5a5f5e; width: 9%; text-align: center;">Report Limitation
                        </td>
                        <td style="margin-bottom: 10px; background-color: #5a5f5e; padding: 10px; width: 85%">All papers in this report are published in Scopus
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-bottom: 0px; background-color: #5a5f5e; padding: 10px">The Report Period :
                            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="page">
            <div class="tit">MEU RESEARCH STATUS OVERALL</div>
            <table style="width: 100%">
                <tr>
                    <td style="height: 400px; margin: 0 auto;" colspan="2">
                        <div id="chart_div" class="chart"></div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 49%; height: 400px; margin: 0 auto;">
                        <div id="chart_div1" class="chart"></div>
                    </td>
                    <td style="width: 49%; height: 400px; margin: 0 auto;">
                        <div id="chart_div2" class="chart"></div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
        <div class="page">
            <div class="tit">MEU RESEARCH STATUS ACCORDING TO FACULTIES</div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                    OnDataBound="GridView1_DataBound" Width="100%" CssClass="grd">
                    <HeaderStyle BackColor="#921A1D" ForeColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="FACULTY" DataField="faculty" />
                        <asp:BoundField HeaderText="INSTRUCTOR" DataField="inst" />
                        <asp:BoundField HeaderText="PUBLISHED PAPER" DataField="PubPaper" />
                        <asp:BoundField HeaderText="ACCEPTED PAPER" DataField="AccPaper" />
                        <asp:BoundField HeaderText="FACULTY STATUS" DataField="FStatus" />
                        <asp:BoundField HeaderText="SHORTAGE" DataField="SHORTAGE" />
                        <asp:BoundField HeaderText="PUBLISHED PERCENTAGE" DataField="PubPer" DataFormatString="{0:F2} %" HtmlEncode="false" />
                        <asp:BoundField HeaderText="INSTRUCTORS ACHIEVED TARGET" DataField="InstAch" />
                        <asp:BoundField HeaderText="PERCENTAGE OF INSTRUCTOR ACHIEVED TARGET" DataField="InstAchPer" DataFormatString="{0:F2} %" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </div>
            <div style="margin: 10px auto 10px auto;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 49%; height: 450px; margin: 0 auto;">
                            <div id="chart_div3" class="chart"></div>
                        </td>
                        <td style="width: 49%; height: 450px; margin: 0 auto;">
                            <div id="chart_div4" class="chart"></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="ArtDiv" class="page">
            <div class="tit">Arts and Sciences</div>
            <asp:GridView ID="grdArt" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="grd"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="MediaDiv" class="page zoom1-3">
            <div class="tit">Media</div>
            <asp:GridView ID="grdMedia" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="grd"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        
        <div id="BusinessDiv" class="page zoom1-3">
            <div class="tit">Business</div>
            <asp:GridView ID="grdBusiness" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="BusinessDiv1" class="page zoom1-3">
            <div class="tit">Business</div>
            <asp:GridView ID="grdBusiness1" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>

        <div id="LawDiv" class="page zoom1-2">
            <div class="tit">Law</div>
            <asp:GridView ID="grdLaw" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="PharmacyDiv" class="page zoom1-3">
            <div class="tit">Pharmacy</div>
            <asp:GridView ID="grdPharmacy" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="EduSciDiv" class="page zoom1-3">
            <div class="tit">Educational Sciences</div>
            <asp:GridView ID="grdEduSci" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="ArchDesignDiv" class="page zoom1-3">
            <div class="tit">Architecture and Design</div>
            <asp:GridView ID="grdArchDesign" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="EngineeringDiv" class="page zoom1-3">
            <div class="tit">Engineering</div>
            <asp:GridView ID="grdEngineering" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
        <div id="ITDiv" class="page zoom1-2">
            <div class="tit">Information Technology</div>
            <asp:GridView ID="grdIT" runat="server" AutoGenerateColumns="false" Width="100%"
                OnRowDataBound="grd_RowDataBound" OnDataBound="grdArt_DataBound" CssClass="grd"
                HeaderStyle-BackColor="#921A1D" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="REngName" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="PubFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="PubCoAuth" />
                    <asp:BoundField HeaderText="FIRST/SINGLE" DataField="AccptedFirstAuth" />
                    <asp:BoundField HeaderText="CO-AUTHOR" DataField="AccptedCoAuth" />
                    <asp:BoundField DataField="AuthorStatus" HeaderStyle-CssClass="hidden" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <%--        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlFromYear" />
            <asp:AsyncPostBackTrigger ControlID="btnApply" EventName="Click"/>
        </Triggers>
    </asp:UpdatePanel>--%>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "10%" });
        }
    </script>

</asp:Content>
