<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UniAbstract.aspx.cs" Inherits="ResearchAcademicUnit.UniAbstract" %>

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
        <asp:Button ID="btnApply" runat="server" Text="Apply" OnClick="btnApply_Click"/>
        <asp:Button ID="btnAll" runat="server" Text="All" OnClick="btnApply_Click" CausesValidation="false" />
    </div>

    <div dir="rtl" class="printDiv">
        <div class="ddlShow">
            <div class="headerDiv" style="float: right; box-sizing: border-box; padding: 10px; width: 100%">
                <div style="float: right; margin: 0 auto 10px auto; width: 100%; box-sizing: border-box">
                    <div>
                        <div class="lblDiv" style="width: 99.5%; box-sizing: border-box; margin: 10px auto 10px auto">الدعم والمكافأت ( العام الحالي )</div>
                        <div style="clear: both"></div>
                        <div class="lblDiv" style="width: 35.5%">عدد الأبحاث و النشاطات التأليفية الحاصلة على دعم أو مكافأة</div>
                        <div class="lblContentDiv" style="width: 14%" id="RDiv" runat="server">
                            <asp:Label ID="lblRSupportPer" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="lblDiv" style="width: 35.5%">عدد المؤتمرات الحاصلة على دعم أو مكافأة</div>
                        <div class="lblContentDiv" style="width: 14%" id="CDiv" runat="server">
                            <asp:Label ID="lblCSupportPer" runat="server" Text=""></asp:Label>
                        </div>
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
                CssClass="grd"
                OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:BoundField DataField="College" HeaderText="اسم الكلية" />
                    <asp:BoundField DataField="Dept" HeaderText="اسم القسم" />
                    <asp:BoundField DataField="RCount" HeaderText="البحوث العلمية" />
                    <asp:BoundField DataField="CCount" HeaderText="المؤتمرات" />
                    <asp:BoundField DataField="PCount" HeaderText="النشاطات التأليفية" />
                    <asp:BoundField DataField="total" HeaderText="العدد الكلي للقسم" />
                </Columns>
            </asp:GridView>
            <div style="clear: both"></div>
        </div>
    </div>
        <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "20%" });

        }
    </script>

</asp:Content>
