<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FullReport.aspx.cs" Inherits="ResearchAcademicUnit.FullReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function printFullR(divName, flag) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
            document.location.href = document.URL;// "PrintStudSeatsFinal.aspx";
            if (flag == 1) {
                __doPostBack("btnApply");
            }
        }
    </script>
    <style>

        .grd {
    text-align: center;
    color: black;
    Border: 1px solid black;
    font-size:24px;
}

    .grd th {
        padding: 8px;
        background-color:#7f7f7f;
        color:white;

    }

        #printDiv1 {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 15px;
        }

        #SecondPage {
            font-size: 1vw;
        }

        .lbl {
            display: none;
        }

        div.page {
            page-break-after: always;
            page-break-inside: avoid;
            background-color: transparent;
        }

        .contChart {
            width: 100%;
            height: 380px;
            display: inline-block;
            float: left;
        }



        @media print {
            body {
                background-color: white;
            }

            /* visible when printed */

            .curDiv {
                color: black;
            }

            .lbl {
                display: block;
                direction: ltr;
                background-color: #7f7f7f;
                color: white;
            }

            .contentDiv2 {
                display: none;
            }

            #printDiv1 {
                background-color: white;
            }

            .contChart{
                width:50%;
            }
        }
    </style>
    <style>
        .ol {
            position: relative;
            display: block;
            margin: 75px auto 50px auto;
            height: 4px;
            /*background: #921A1D;*/
            /*width:100%;*/
        }

            /* ---- Timeline elements ---- */

            .ol li {
                position: relative;
                top: -18px;
                /*left: 20px;*/
                display: inline-block;
                float: left;
                width: 200px;
                /*transform: rotate(-15deg);*/
                font: bold 20px arial;
                padding-left: 30px;
            }

                .ol li::before {
                    content: "";
                    position: absolute;
                    top: -18px;
                    left: -29px;
                    display: block;
                    width: 50px;
                    height: 50px;
                    border: 1px solid #fcb131;
                    border-radius: 50px;
                    background: #fcb131;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <asp:UpdatePanel ID="getDataPanel" runat="server">
        <ContentTemplate>
            <div class="contentDiv2" style="direction: ltr; width: 98%; margin: 0 auto 0 auto">
                <asp:Label ID="lblP" runat="server" Text="Period"></asp:Label>
                <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector" OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlFromYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlFromMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlToYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlToMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                <asp:Button ID="btnApply" runat="server" Text="Get Data" CssClass="btn" OnClick="btnApply_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlFromYear" />
            <asp:PostBackTrigger ControlID="btnApply" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="printDiv1" style="width: 95%; margin: 1% auto 1% auto;direction:ltr">
        <div id="FirstPage" class="page" style="font-size: 4vw; text-align: center">
            <img src="images/meu.png" style="width: 40vw" />
            <p>Deanship of Graduate studies and Scientific Research</p>
            <p>Scientific Research Department</p>
            <p style="background-color: #7f7f7f; color: white">MEU SCOPUS RESEARCH</p>
            <div class="lbl">
                <asp:Label ID="lblPeriod" runat="server" Text="Label"></asp:Label>
            </div>
        </div>

        <div id="SecondPage" class="page" style="overflow: no-display">
            <div style="clear: both"></div>
            <div style="direction: ltr; width: 100%">
                <div style="float: left; width: 15%">
                    <img src="images/Middle_East_University_logo.png" />
                </div>
                <div style="font-size: 4vw; text-align: center; background-color: #7f7f7f; color: white; float: left; width: 84%">
                    MEU RESEARCH CARD
                </div>
                <div style="clear: both"></div>
            </div>

            <div class="contentDiv" style="width: 48%; float: right; padding: 10px">
                <div style="width: 100%">
                    <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0px auto 10px auto;font-size: 24px;">Research Performance</div>
                    <div style="width: 95%; margin: 0 auto 10px auto">
                        <table style="width: 100%; height: 300px;font-size:24px; border-spacing: 0px; text-align: center; border-collapse: collapse; border-color: gray" border="1">
                            <tr style="background-color: #7f7f7f; color: white">
                                <td colspan="2" rowspan="2">Research Activities</td>
                                <td rowspan="2">No of Research Activiteis</td>
                                <td colspan="2">Single | Co-Author</td>
                            </tr>
                            <tr style="background-color: #7f7f7f; color: white">
                                <td class="auto-style2">Single Author</td>
                                <td class="auto-style1">Co-Author</td>
                            </tr>
                            <tr>
                                <td rowspan="3">Scientific Research</td>
                                <td class="auto-style3">Article</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Review Paper</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">Editorial</td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="background-color: white">
                                <td colspan="2">Total</td>
                                <td>
                                    <asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label14" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label15" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">Conference</td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label18" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label19" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="background-color: white">
                                <td rowspan="3">Books</td>
                                <td class="auto-style3">Book Chapter</td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label22" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label23" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="background-color: white">
                                <td class="auto-style3">Book</td>
                                <td>
                                    <asp:Label ID="Label25" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label26" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label27" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="background-color: white">
                                <td class="auto-style3">Translated Book</td>
                                <td>
                                    <asp:Label ID="Label29" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label30" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label31" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">Total</td>
                                <td>
                                    <asp:Label ID="Label33" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style2">
                                    <asp:Label ID="Label34" runat="server" Text=""></asp:Label></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label35" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr style="background-color: white">
                                <td colspan="2">Overall</td>
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
                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto;font-size: 24px;">Continuity</div>
                <div style="margin: 5px auto 5px auto; box-sizing: border-box;">
                    <div id="chart_div" class="contChart"></div>
                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>
                </div>
                <div>
                    <div class="lblDiv" style="width: 74.5%; box-sizing: border-box; border-radius: 0px; margin: 0 auto 10px auto;font-size: 24px;">Total</div>
                    <div class="lblContentDiv" style="width: 24.5%; border-radius: 0px; color: white;font-size: 24px;" id="currentDiv" runat="server">
                        <asp:Label ID="lblCurrentR" runat="server" Text=""></asp:Label>
                    </div>
                    
                </div>
                <div style="clear: both"></div>
                <div runat="server" id="curDiv">
                </div>
            </div>
            <div style="clear: both"></div>

            <div class="contentDiv1">

                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto;font-size: 24px;">Diversity</div>
                    <div id="piechartDiversity" style="width: 100%; height: 300px; float: left"></div>
            </div>

            <div class="contentDiv1">

                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto;font-size: 24px;">Research Comprehensiveness</div>
                    <div id="piechartComp" style="width: 100%; height: 300px; float: left"></div>
            </div>

            <div class="contentDiv1">

                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto;font-size: 24px;">Clasification</div>
                    <div id="piechartRTypes" style="width: 100%; height: 300px; float: left; display: inline-block"></div>
            </div>

            <div class="contentDiv1">
                <div class="lblDiv" style="width: 100%; box-sizing: border-box; margin: 0 auto 10px auto;font-size: 24px;">Faculty Performance</div>
                    <div id="piechartDiversity1" style="width: 100%; height: 300px; float: left"></div>
            </div>
            <div style="clear: both"></div>
        </div>
        
        <div id="ThirdPage" class="page">
            <div style="direction: ltr; width: 100%">
                <div style="float: left; width: 15%">
                    <img src="images/Middle_East_University_logo.png" />
                </div>
                <div style="font-size: 4vw; text-align: center; background-color: #7f7f7f; color: white; float: left; width: 84%">
                    MEU RESEARCH
                </div>
                <div style="clear: both"></div>
            </div>
            <div style="clear: both"></div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
                Width="100%" OnRowDataBound="GridView1_RowDataBound" CssClass="grd">
                <Columns>
                    <asp:BoundField HeaderText="#" DataField="row_num" HeaderStyle-Width="4%" />
                    <asp:BoundField HeaderText="Title" DataField="ReTitle" HeaderStyle-Width="30%" />
                    <asp:BoundField HeaderText="Research Type" DataField="ReType" HeaderStyle-Width="7%" />
                    <asp:BoundField HeaderText="Year" DataField="ReYear" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="Journal" DataField="Magazine" HeaderStyle-Width="15%" />
                    <asp:BoundField HeaderText="Source Type" DataField="SourceType" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="Quarter" DataField="MClass" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="Cite Score" DataField="CitationAvg" HeaderStyle-Width="5%" />
                    <asp:BoundField HeaderText="رمز البحث" DataField="ReId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="Researcher(s)">
                        <ItemTemplate>
                            <div runat="server" id="ReInfoDiv">
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div id="Top10PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 20% auto;  width: 50%">
                QUARTER 1
                <br />
                TOP 10
            </p>
        </div>
        <div id="Top10PageContent" class="page" runat="server">
        </div>

        <div id="Quarter1PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto;  width: 50%">
                QUARTER 1
            </p>
        </div>

        <div id="Quarter1PageContent" class="page" runat="server">
        </div>

        <div id="Quarter2PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto;  width: 50%">
                QUARTER 2
            </p>
        </div>

        <div id="Quarter2PageContent" class="page" runat="server">
        </div>

        <div id="Quarter3PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto;  width: 50%">
                QUARTER 3
            </p>
        </div>

        <div id="Quarter3PageContent" class="page" runat="server">
        </div>

        <div id="Quarter4PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto; width: 50%">
                QUARTER 4
            </p>
        </div>

        <div id="Quarter4PageContent" class="page" runat="server">
        </div>

        <div id="Quarter5PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto; width: 50%">
                Non Qs
            </p>
        </div>

        <div id="Quarter5PageContent" class="page" runat="server">
        </div>

        <div id="Quarter6PageCover" class="page" style="font-size: 8vw; text-align: center">
            <p style="margin: 30% auto; width: 50%">
                Conferences
            </p>
        </div>

        <div id="Quarter6PageContent" class="page" runat="server">
        </div>


    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <%--<input type="button" id="btnPrint" runat="server" style="margin-left: 10px; margin-right: 10px" class="btn" onclick="printFullR('printDiv1', 1);" value="    طباعة    " />--%>
    <script>
        $('.ChosenSelector').chosen({ width: "10%" });
    </script>

</asp:Content>
