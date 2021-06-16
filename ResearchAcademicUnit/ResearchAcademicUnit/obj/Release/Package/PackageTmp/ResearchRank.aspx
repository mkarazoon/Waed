<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ResearchRank.aspx.cs" Inherits="ResearchAcademicUnit.ResearchRank" %>

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

            .contentDiv2 {
                display: none;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="contentDiv" style="background-color: #7f7f7f !important; color: white; direction: ltr; float: left; width: 48%; font-size: 2vw; font-weight: bold; text-align: center; margin: 1%">MEU SCIENTIFIC RESEARCH RANK</div>
        <div class="contentDiv" style="background-color: #fcb131 !important; color: white; direction: ltr; float: left; width: 40%; font-size: 2vw; font-weight: bold; text-align: center; margin: 1%">
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
        <div style="float: left; width: 5%; margin: 1%">
            <img src="images/Middle_East_University_logo.png" style="height: 4vw; width: 5vw" />
        </div>

        <div style="clear: both"></div>
    </div>
    <div class="contentDiv2" style="direction: ltr; width: 98%; margin: 0 auto 0 auto">
        <asp:Label ID="Label2" runat="server" Text="Period"></asp:Label>
        <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="ChosenSelector" OnSelectedIndexChanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlFromYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlFromMonth" runat="server" CssClass="ChosenSelector" AutoPostBack="true" OnSelectedIndexChanged="ddlFromMonth_SelectedIndexChanged"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="ddlFromMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="ChosenSelector" AutoPostBack="True" OnSelectedIndexChanged="ddlToYear_SelectedIndexChanged"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlToYear" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlToMonth" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:Label ID="Label3" runat="server" Text="Top of "></asp:Label>
        <asp:DropDownList ID="ddlTop" runat="server" CssClass="ChosenSelector">
            <asp:ListItem Value="0">Select Top</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
            <asp:ListItem Value="6">6</asp:ListItem>
            <asp:ListItem Value="7">7</asp:ListItem>
            <asp:ListItem Value="8">8</asp:ListItem>
            <asp:ListItem Value="9">9</asp:ListItem>
            <asp:ListItem Value="10">10</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlTop" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn" OnClick="btnApply_Click" />
    </div>
    <div class="contentDiv" style="text-align: center; width: 98%; margin: 0 auto 0 auto" id="printDiv">
        <div style="margin: 1% auto 1% auto; display: block">
            <div class="TitleDiv" style="width: 95%;">
                BEST OF RESEARCH
            </div>
            <div style="direction: ltr; width: 95%; margin: 1% auto 1% auto">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="grd" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Rank" DataField="Rank" />
                        <asp:BoundField HeaderText="Research Title" DataField="ReTitle" />
                        <asp:BoundField HeaderText="Quarter" DataField="MClass" />
                        <asp:BoundField HeaderText="Cite Score" DataField="CitationAvg" />
                        <asp:BoundField HeaderText="Author" DataField="Author" />
                        <asp:BoundField HeaderText="Faculty" DataField="College" />
                        <asp:BoundField HeaderText="Year" DataField="ReYear" />
                        <asp:BoundField HeaderText="Month" DataField="ReMonth" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <asp:GridView ID="grdBestRes" runat="server" AutoGenerateColumns="false" Visible="false" CssClass="grd" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Rank" DataField="Rank" />
                        <asp:BoundField HeaderText="Research Title" DataField="ReTitle" />
                        <asp:BoundField HeaderText="Quarter" DataField="MClass" />
                        <asp:BoundField HeaderText="Cite Score" DataField="CitationAvg" />
                        <asp:BoundField HeaderText="Author" DataField="Author" />
                        <asp:BoundField HeaderText="Faculty" DataField="College" />
                        <asp:BoundField HeaderText="Year" DataField="ReYear" />
                    </Columns>
                </asp:GridView>

                <div class="contentDiv2">
                    <asp:Button ID="btnBestR" runat="server" Text="Best Researchers Final List" OnClick="btnBestR_Click" CssClass="btn"></asp:Button>
                </div>

            </div>
            <div style="clear: both"></div>
        </div>

        <div>
            <div style="float: left; width: 48%">
                <div class="TitleDiv">
                    BEST OF AUTHORS
                </div>
                <div style="direction: ltr; width: 95%; margin: 1% auto 1% auto">
                    <center>
                    <asp:GridView ID="grdBestAuth" runat="server" CssClass="grd" AutoGenerateColumns="false" Width="95%">
                        <Columns>
                            <asp:BoundField HeaderText="Rank" DataField="Rank" />
                            <asp:BoundField HeaderText="Author" DataField="Author" />
                            <asp:BoundField HeaderText="Faculty" DataField="College" />
                            <asp:BoundField HeaderText="# OF Paper(s)" DataField="PaperCount" />
                            <asp:BoundField HeaderText="q1" DataField="q1" />
                            <asp:BoundField HeaderText="q2" DataField="q2" />
                            <asp:BoundField HeaderText="q3" DataField="q3" />
                            <asp:BoundField HeaderText="q4" DataField="q4" />
                            <asp:BoundField HeaderText="q5" DataField="q5" />
                            <asp:BoundField HeaderText="CAvg" DataField="CAvg" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="GridView1" runat="server" CssClass="grd" AutoGenerateColumns="false" Width="95%">
                        <Columns>
                            <asp:BoundField HeaderText="Rank" DataField="Rank" />
                            <asp:BoundField HeaderText="Author" DataField="Author" />
                            <asp:BoundField HeaderText="Faculty" DataField="College" />
                            <asp:BoundField HeaderText="# OF Paper(s)" DataField="PaperCount" />
                        </Columns>
                    </asp:GridView>

                        <div class="contentDiv2">
                        <asp:Button ID="btnFinalList" runat="server" Text="Final List" OnClick="btnFinalList_Click" Visible="false"  CssClass="btn"></asp:Button>
                            </div>
                        </center>
                </div>
            </div>
            <div style="float: right; width: 48%">
                <div class="TitleDiv">
                    FACULTIES RANK
                </div>
                <div style="direction: ltr; width: 95%; margin: 1% auto 1% auto">
                    <center>
                    <asp:GridView ID="grdCollegeRank" runat="server" CssClass="grd" AutoGenerateColumns="false" Width="95%">
                        <Columns>
                            <asp:BoundField HeaderText="Rank" DataField="Rank" />
                            <asp:BoundField HeaderText="Faculty" DataField="College" />
                            <asp:BoundField HeaderText="# OF Paper(s)" DataField="PaperCount" />
                        </Columns>
                    </asp:GridView>
                        </center>
                </div>
            </div>
            <div style="clear: both"></div>
        </div>
    </div>

    <script>
        $('.ChosenSelector').chosen({ width: "10%" });
    </script>

</asp:Content>
