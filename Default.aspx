<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="Style1.css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="logowanieParent">
                    <div class="logowanie">
                        <div class="logowanieInput1"> 
                            <div class="logowanieInput2"> 
                                <asp:Label runat="server" ID="LabelLogowanie" Font-Size="Small">Podaj imie i nazwisko</asp:Label>
                                <asp:TextBox ValidationGroup="Group1" runat="server" ID="TextBox1" ToolTip="Poprawny identyfikator Hubert Makowski&#13;&#10;Niepoprawny identyfikator Marian kiepski"></asp:TextBox>
                            </div>
                            <div class="logowanieInput3">
                                <asp:CustomValidator Display="Dynamic" ValidationGroup="Group1" runat="server" ID="CustomValidator1" ValidateEmptyText="true" ErrorMessage="Isnieje uzytkownik o takiej nazwie" OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                                <asp:RequiredFieldValidator ValidationGroup="Group1" runat="server" ID="RequiredFieldValidator1" ControlToValidate="TextBox1" ErrorMessage="Imie i naziwsko nie moze byc puste">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ValidationGroup="Group1" ControlToValidate="TextBox1" ValidationExpression="[A-ZĄĘÓŚŁŻŹĆŃ][a-ząęóśłżźćń]{2,}\s[A-ZĄĘÓŚŁŻŹĆŃ][a-ząęóśłżźćń]{2,}" runat="server" ID="RegularExpressionValidator1" ErrorMessage="Niepoprawne imie i nazwisko">*</asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <br />
                        <asp:Button ValidationGroup="Group1" runat="server" ID="ButtonZatwierdz" Text="Zarejestruj" CausesValidation="true" OnClick="ButtonZatwierdz_Click"/>
                        <br />
                        <asp:ValidationSummary ValidationGroup="Group1" runat="server" ID="ValidationSummary1" DisplayMode="BulletList" />
                    </div>
                    <asp:SqlDataSource ID="sqldata1" runat="server" 
                        ConnectionString="<%$ConnectionStrings:BazaASP %>" 
                        ProviderName="System.Data.SqlClient" 
                        SelectCommand="Select Nazwa, DataUruchomienia as 'Data Uruchomienia', GodzinaUruchomienia as 'Godzina Uruchomienia', DrugaTabela.zlicz as 'Ilu uzytkownikow',
                                              LiczbaZmianRysunku as 'Ile zmian rysunku', LiczbaZmianPolozeniaRysunku as 'Ile zmian polozenia', 
                                              LiczbaZmianRozmiaru as 'Ile zmian rozmiaru', CONCAT(CzasMaksymanlny, ' s') as 'Czas Maksymalny',CONCAT(CzasMinimalny, ' s') as 'Czas Minimalny' 
                                       From Uzytkownicy, (Select Count(*) as zlicz From Uzytkownicy Where Aktywny='True') as DrugaTabela
                                       Where Nazwa=@Nazwa" 
                        UpdateCommand="Update Uzytkownicy Set Nazwa=@Nazwa Where Id=@Id">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="Nazwa" QueryStringField="Nazwa" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DetailsView ID="Det2" runat="server" DataSourceID="sqldata1">
                        <Fields>
                           
                        </Fields>
                    </asp:DetailsView>

                </div>
            </ContentTemplate>
            <Triggers>
                
                <asp:PostBackTrigger ControlID="ButtonZatwierdz"/>
            </Triggers>
        </asp:UpdatePanel>
        
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                <ContentTemplate>
                    <div class="parent">
                    <div class="leftdiv">
                        <div class="subdiv">
                            <asp:Label runat="server" style="color:blue; font-weight:bold">Aktualny uzytkownik</asp:Label>
                            <br />
                            <asp:Label runat="server" ID="LabelUser" style="color:red; font-weight:bold"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="LabelR" runat="server" style="color:blue; font-weight:bold">
                                Zmiana rysunku
                            </asp:Label>
                            <asp:Button Enabled="false" OnClick="ButtonPoprzedni_Click" runat="server" ID="ButtonPoprzedni" Text="Poprzedni" CssClass="B1"/>
                            <br />
                            <br />
                            <asp:Button OnClick="ButtonKolejny_Click" runat="server" ID="ButtonKolejny" Text="Kolejny" CssClass="B2" />
                        </div>
                        <div>
                            <p style="color:blue; font-weight:bold">Wybierz rozmiar rysunku</p>
                            <asp:RadioButtonList AutoPostBack="true" runat="server" ID="RadioButtonList1" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Selected="True">Maly rozmiar rysunku</asp:ListItem>
                                <asp:ListItem>Sredni rozmiar rysunku</asp:ListItem>
                                <asp:ListItem>Duzy rozmiar rysunku</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <asp:Label ID="Label1" runat="server" style="color:blue; font-weight:bold">Minimalny Czas Odpowiedzi</asp:Label>
                            <br />
                            <asp:Label ID="Label2" runat="server" style="color:red; font-weight:bold">0 ms</asp:Label>
                            <br />
                            <asp:Label ID="Label3" runat="server" style="color:blue; font-weight:bold">Maksymalny Czas Odpowiedzi</asp:Label>
                            <br />
                            <asp:Label ID="Label4" runat="server" style="color:red; font-weight:bold">0 ms</asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="ZerujButton" runat="server" Text="Zeruj Dane" OnClick="ZerujButton_Click"/>
                            <br />
                            <asp:Label ID="Label5" runat="server" style="color:blue; font-weight:bold">Dane uzytkownika</asp:Label>
                            <br />
                            <asp:UpdatePanel ID="NestedUpdatePanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" style="color:blue" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:DetailsView ID="DetailsView1" runat="server" BackColor="AntiqueWhite" DataSourceID="sqldata1">
                                        <Fields>
                                        </Fields>
                                    </asp:DetailsView>
                                    <asp:Timer runat="server" ID="Timer1" Interval="6000" OnTick="Timer1_Tick" Enabled="true"></asp:Timer>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>

                            </asp:UpdatePanel>

                        </div>
        
                    </div>
                    <div class="rightdiv">
                        <div id="gora">

                            <div id="pole1" class="pole" runat="server">
                                <asp:ImageButton OnClick="Image1_Click" ID="Image1" CssClass="maly" runat="server" ImageUrl="Rysunki/ryze.jpg" />
                            </div>
                            <div id="pole2" class="pole" runat="server">
            
                            </div>
                        </div>
                        <div id="pole3" class="pole" runat="server">
        
                        </div>
                        <div id="dol">
                            <div id="pole4" class="pole" runat="server">

                            </div>
                            <div id="pole5" class="pole" runat="server">

                            </div>
                        </div>
    
                    </div>
                    </div> 
                    
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="Image1"/> 
                    <asp:PostBackTrigger ControlID="DropDownList1"/>
                    <asp:PostBackTrigger ControlID="RadioButtonList1"/>
                    <asp:PostBackTrigger ControlID="ButtonKolejny"/>
                    <asp:PostBackTrigger ControlID="ButtonPoprzedni"/>
                   
                </Triggers>
               
            </asp:UpdatePanel>
            
         
    </form>
</body>
</html>
