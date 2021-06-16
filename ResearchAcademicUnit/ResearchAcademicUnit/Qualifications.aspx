<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="Qualifications.aspx.cs" Inherits="ResearchAcademicUnit.Qualifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="div" id="qualDiv" runat="server">
                <div class="div">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div id="bscDetDiv" runat="server">
                                    <table style="width: 99%">
                                        <tr>
                                            <td colspan="3">
                                                        <div class="sec">
                                                            <asp:Label ID="lblBsc" CssClass="lblTitle" runat="server" Text="البكالوريوس"></asp:Label>
                                                        </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtBUni" runat="server" placeholder="الجامعة"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" ControlToValidate="txtBUni" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtBCollege" runat="server" placeholder="الكلية"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" ControlToValidate="txtBCollege" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlBCountry" runat="server" CssClass="ChosenSelector">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ControlToValidate="ddlBCountry" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlBYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" ControlToValidate="ddlBYear" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtBMajor" runat="server" placeholder="التخصص العام"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" ControlToValidate="txtBMajor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtBMinor" runat="server" placeholder="التخصص الدقيق"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" ControlToValidate="txtBMinor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>


                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlBGrade" runat="server" CssClass="ChosenSelector">
                                                    <asp:ListItem Value="0">التقدير</asp:ListItem>
                                                    <asp:ListItem Value="1">ممتاز</asp:ListItem>
                                                    <asp:ListItem Value="2">جيد جدا</asp:ListItem>
                                                    <asp:ListItem Value="3">جيد</asp:ListItem>
                                                    <asp:ListItem Value="4">مقبول</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" ControlToValidate="ddlBGrade" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%"></td>
                                            <td style="width: 33%"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>

                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="masterDetDiv" runat="server">
                                    <table style="width: 99%">
                                        <tr>
                                            <td colspan="3">
                                                        <div class="sec">
                                                            <asp:Label ID="lblMaster" CssClass="lblTitle" runat="server" Text="الماجستير"></asp:Label>
                                                        </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtMUni" runat="server" placeholder="الجامعة"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfMUni" runat="server" ErrorMessage="*" ControlToValidate="txtMUni" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtMCollege" runat="server" placeholder="الكلية"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfMCollege" runat="server" ErrorMessage="*" ControlToValidate="txtMCollege" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlMCountry" runat="server" CssClass="ChosenSelector">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfMCountry" runat="server" ErrorMessage="*" ControlToValidate="ddlMCountry" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlMYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfMYear" runat="server" ErrorMessage="*" ControlToValidate="ddlMYear" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>

                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtMMajor" runat="server" placeholder="التخصص العام"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfMMajor" runat="server" ErrorMessage="*" ControlToValidate="txtMMajor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtMMinor" runat="server" placeholder="التخصص الدقيق"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfMMinor" runat="server" ErrorMessage="*" ControlToValidate="txtMMinor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlMGrade" runat="server" CssClass="ChosenSelector">
                                                    <asp:ListItem Value="0">التقدير</asp:ListItem>
                                                    <asp:ListItem Value="1">ممتاز</asp:ListItem>
                                                    <asp:ListItem Value="2">جيد جدا</asp:ListItem>
                                                    <asp:ListItem Value="3">جيد</asp:ListItem>
                                                    <asp:ListItem Value="4">مقبول</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfMGrade" runat="server" ErrorMessage="*" ControlToValidate="ddlMGrade" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%"></td>
                                            <td style="width: 33%"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="PhdDetDiv" runat="server">
                                    <table style="width: 99%">
                                        <tr>
                                            <td colspan="3">
                                                <div class="sec">
                                                    <asp:Label ID="lblPhd" CssClass="lblTitle" runat="server" Text="الدكتوراة"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtPUni" runat="server" placeholder="الجامعة"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfPUni" runat="server" ErrorMessage="*" ControlToValidate="txtBUni" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtPCollege" runat="server" placeholder="الكلية"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfPCollege" runat="server" ErrorMessage="*" ControlToValidate="txtPCollege" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlPCountry" runat="server" CssClass="ChosenSelector">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfPCountry" runat="server" ErrorMessage="*" ControlToValidate="ddlPCountry" Display="Dynamic" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:DropDownList ID="ddlPYear" runat="server" CssClass="ChosenSelector"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfPYear" runat="server" ErrorMessage="*" ControlToValidate="ddlPYear" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtPMajor" runat="server" placeholder="التخصص العام"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfPMajor" runat="server" ErrorMessage="*" ControlToValidate="txtPMajor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%">
                                                <asp:TextBox ID="txtPMinor" runat="server" placeholder="التخصص الدقيق"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfPMinor" runat="server" ErrorMessage="*" ControlToValidate="txtPMinor" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlPGrade" runat="server" CssClass="ChosenSelector">
                                                    <asp:ListItem Value="0">التقدير</asp:ListItem>
                                                    <asp:ListItem Value="1">ممتاز</asp:ListItem>
                                                    <asp:ListItem Value="2">جيد جدا</asp:ListItem>
                                                    <asp:ListItem Value="3">جيد</asp:ListItem>
                                                    <asp:ListItem Value="4">مقبول</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfPGrade" runat="server" ErrorMessage="*" ControlToValidate="ddlPGrade" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 33%"></td>
                                            <td style="width: 33%"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="sec">
                    <asp:Button ID="btnPrev" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnPrev_Click" Text="السابق" Width="100px" />
                    <asp:Button ID="btnSaveCert" runat="server" Text="حفظ" CssClass="btn" Width="100px" Height="40px" OnClick="btnSaveCert_Click" CausesValidation="False" />
                    <asp:Button ID="btnNext" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnNext_Click" Text="التالي" Width="100px" />
                </div>
            </div>
            <div class="divTitle">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <center>
                    <asp:Label runat="server" Visible="false" Text="تم التخزين بنجاح" CssClass="lblMsg" ID="lblMsg"></asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False" ></asp:Timer>
                        </center>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script> 
        function pageLoad() {
            $('.ChosenSelector').chosen({ width: "80%" });
        }
    </script>

</asp:Content>
