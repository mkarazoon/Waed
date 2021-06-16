<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UniversityInfo.aspx.cs" Inherits="ResearchAcademicUnit.UniversityInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*.grd {
            text-align: center;
            color: white;
            Border:1px solid black;
        }
                .grd tr td{
            padding:8px;
            
        }*/

        .hidden {
            display: none;
        }

        .printDiv {
            width: 99.5%;
            margin: 0 auto 0 auto;
            box-sizing: border-box;
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 15px;
        }

        @media print {
            .printDiv {
                background-color: #f5f5f5;
            }
            .grd{
                page-break-after: always;
            }
            .grd tr td{
                text-align: center;
                color: black;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" class="printDiv">

        <div id="msgDiv" runat="server" visible="false" class="headerDiv" style="padding: 20px; box-sizing: border-box">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                Width="100%"
                OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                AllowPaging="True" CssClass="grd" PagerSettings-PageButtonCount="100" PageSize="20">
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
                    <asp:BoundField HeaderText="ضمن أول 10 مجلات" DataField="TopMag" HeaderStyle-Width="5%" />
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
                    <asp:TemplateField HeaderText="الكلية">
                        <ItemTemplate>
                            <div runat="server" id="CollegeDiv">
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="القسم">
                        <ItemTemplate>
                            <div runat="server" id="DeptDiv">
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
    <asp:Button ID="Button1" runat="server" Text="طباعة الكل" OnClick="Button1_Click" CssClass="btn"/>
</asp:Content>
