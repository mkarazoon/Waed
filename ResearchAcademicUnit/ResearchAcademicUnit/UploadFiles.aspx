<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="UploadFiles.aspx.cs" Inherits="ResearchAcademicUnit.UploadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blanck";
        }
    </script>
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="containerDiv">
        <%--        <div>
            <div class="sec">
                <asp:Label ID="Label19" runat="server" Text="جميع الملفات التي تم تحميلها على النظام"></asp:Label>
            </div>
            <div>
                <asp:TreeView ID="TreeView1" runat="server" on ImageSet="XPFileExplorer" NodeIndent="15" ExpandDepth="0">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                    <LeafNodeStyle ImageUrl="~/images/pdf.png" />
                    <NodeStyle Font-Names="Droid Arabic Kufi" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
                <asp:Button ID="Button6" runat="server" Text="عرض الملف" CssClass="btn" OnClick="Button6_Click" />
                <br />
            </div>
        </div>--%>
        <div id="ResearchDiv" runat="server" visible="true">
            <div class="sec">
                <asp:Label ID="Label1" runat="server" Text="يمكن تحميل الأبحاث جميعها في ملفات منفصلة أو تحميل الصفحة الأولى من كل بحث مجتمعة في ملف واحد"></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="تخزين الملفات" OnClick="btnUpload_Click" CssClass="btn" Width="100px" Height="40px" Style="float: left" />
            </div>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label6" runat="server" Text="الأبحاث"></asp:Label>
                    </div>

                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluRScopus" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluRScopus" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />
                    <asp:Label ID="lblUp1" runat="server" Text=""></asp:Label>
                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image3" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل بحث (العنوان، الملخص)
                    </span>
                </div>
                    
                    </center>
                </div>
            </label>
            <%--<label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label7" runat="server" Text="أبحاث أخرى"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluROther" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf0" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluROther" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل بحث (العنوان، الملخص)
                    </span>
                </div>
                    </center>
                </div>
            </label>--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="30%">
                <Columns>
                    <asp:BoundField HeaderText="اسم الملف" DataField="FileName" />
                    <asp:BoundField HeaderText="المسار" DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>

        <div style="clear: both"></div>
        <div id="ConferenceDiv" runat="server" visible="true">

            <div class="sec">
                <asp:Label ID="Label2" runat="server" Text="ملفات المؤتمرات - ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل ورقة بحثية (العنوان، الملخص)"></asp:Label>
                <asp:Button ID="Button2" runat="server" Text="تخزين الملفات" OnClick="btnUpload_Click" CssClass="btn" Width="100px" Height="40px" Style="float: left" />
            </div>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label8" runat="server" Text="المؤتمرات"></asp:Label>
                    </div>

                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluCINU" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf1" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluCINU" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل ورقة بحثية (العنوان، الملخص)
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <%--            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label9" runat="server" Text="داخل الاردن"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluCINJo" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf2" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluCINJo" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image4" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل ورقة بحثية (العنوان، الملخص)
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label10" runat="server" Text="اقليمي - الدول العربية"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluCOutJo" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf3" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluCOutJo" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image5" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل ورقة بحثية (العنوان، الملخص)
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label11" CssClass="lblTitle" runat="server" Text="دولي"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluCNational" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf4" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluCNational" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image6" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل ورقة بحثية (العنوان، الملخص)
                    </span>
                </div>
                    </center>
                </div>
            </label>--%>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="30%">
                <Columns>
                    <asp:BoundField HeaderText="اسم الملف" DataField="FileName" />
                    <asp:BoundField HeaderText="المسار" DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div style="clear: both"></div>
        <div id="BookDiv" runat="server" visible="true">
            <div class="sec">
                <asp:Label ID="Label3" runat="server" Text="ملفات الكتب - ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل كتاب"></asp:Label>
                <asp:Button ID="Button3" runat="server" Text="تخزين الملفات" OnClick="btnUpload_Click" CssClass="btn" Width="100px" Height="40px" Style="float: left" />
            </div>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label12" CssClass="lblTitle" runat="server" Text="الكتب"></asp:Label>
                    </div>

                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluBNational" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf5" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluBNational" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image7" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل كتاب 
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <%--            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label13" CssClass="lblTitle" runat="server" Text="ناشر محلي"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluBLocal" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf6" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluBLocal" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image8" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل كتاب 
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label14" CssClass="lblTitle" runat="server" Text="ناشر إقليمي"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluBIn" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf7" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluBIn" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image9" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن الصفحة الأولى من كل كتاب 
                    </span>
                </div>
                    </center>
                </div>
            </label>--%>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="30%">
                <Columns>
                    <asp:BoundField HeaderText="اسم الملف" DataField="FileName" />
                    <asp:BoundField HeaderText="المسار" DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div style="clear: both"></div>
        <div id="ActiviteisDiv" runat="server" visible="true">
            <div class="sec">
                <asp:Label ID="Label4" runat="server" Text="ملفات شهادات حضور/مشاركة الورش - الندوات - الدورات - ملف واحد مجمّع على شكل (PDF) يتضمن شهادات الحضور أو المشاركة"></asp:Label>
                <asp:Button ID="Button4" runat="server" Text="تخزين الملفات" OnClick="btnUpload_Click" CssClass="btn" Width="100px" Height="40px" Style="float: left" />
            </div>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label15" runat="server" Text="ملفات الشهادات"></asp:Label>
                    </div>

                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluWorkShop" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf8" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluWorkShop" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image10" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن شهادات الحضور أو المشاركة
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <%--            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label16" runat="server" Text="شهادات الندوات (حضور/مشاركة)"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluSeminar" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf9" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluSeminar" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image11" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن شهادات الحضور أو المشاركة
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label17" CssClass="lblTitle" runat="server" Text="شهادات الدورات (حضور/مشاركة)"></asp:Label>
                    </div>

                    <span><strong>تحميل الملف</strong></span>
                    <asp:FileUpload ID="fluTrain" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf10" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluTrain" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image12" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن شهادات الحضور أو المشاركة
                    </span>
                </div>
                    </center>
                </div>
            </label>--%>
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="30%">
                <Columns>
                    <asp:BoundField HeaderText="اسم الملف" DataField="FileName" />
                    <asp:BoundField HeaderText="المسار" DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <div style="clear: both"></div>
        <div id="CertificateDiv" runat="server" visible="true">
            <div class="sec">
                <asp:Label ID="Label5" runat="server" Text="ملفات شهادات التميز - ملف واحد مجمّع على شكل (PDF) يتضمن شهادات التميز"></asp:Label>
                <asp:Button ID="Button5" runat="server" Text="تخزين الملفات" OnClick="btnUpload_Click" CssClass="btn" Width="100px" Height="40px" Style="float: left" />
            </div>
            <label style="cursor: pointer">
                <div class="divFile">
                    <div class="titlebg">
                        <asp:Label ID="Label18" CssClass="lblTitle" runat="server" Text="شهادات تميز"></asp:Label>
                    </div>
                    <span><strong>اختيار الملف</strong></span>
                    <asp:FileUpload ID="fluCertificate" runat="server" />
                    <asp:RegularExpressionValidator ID="regpdf11" ErrorMessage="تحميل ملفات PDF فقط" ControlToValidate="fluCertificate" ValidationExpression="^.*\.(pdf|PDF)$" runat="server" Display="Dynamic" ForeColor="Red" />

                    <br />
                    <center>
                <div class="tooltip" >
                    <asp:Image ID="Image13" runat="server" Height="16px" ImageUrl="~/images/questionmark.png" Width="16px" />
                    <span class="tooltiptext">ملف واحد مجمّع على شكل (PDF) يتضمن شهادات التميز
                    </span>
                </div>
                    </center>
                </div>
            </label>
            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="grd" Width="30%">
                <Columns>
                    <asp:BoundField HeaderText="اسم الملف" DataField="FileName" />
                    <asp:BoundField HeaderText="المسار" DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-Width="100px" HeaderText="الإجراء">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('هل أنت متأكد من حذف البيانات؟')" runat="server" CausesValidation="false" ToolTip="حذف" Style="margin: 0 5px"><i class="material-icons" style="color: #E34724">&#xE872;</i></asp:LinkButton>
                            <asp:LinkButton ID="lnkView" OnClick="lnkView_Click" runat="server" OnClientClick="SetTarget();" CausesValidation="false" ToolTip="عرض" Style="margin: 0 5px"><i class="material-icons" style="color: #FFC107">open_in_new</i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="clear: both"></div>
        <div class="sec">
            <asp:Button ID="btnPrev" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnPrev_Click" Text="السابق" Width="100px" />
            <asp:Button ID="btnNext" runat="server" CausesValidation="False" CssClass="btn" Height="40px" OnClick="btnNext_Click" Text="التالي" Width="100px" />
        </div>

        <div class="messageDiv success" runat="server" id="msgDiv" visible="false">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <center>
                    <asp:Label runat="server" Visible="false" Text="تم التخزين بنجاح" ID="lblMsg"></asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="False" ></asp:Timer>
                        </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
</asp:Content>
