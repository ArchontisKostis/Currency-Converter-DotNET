<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LabCurConv.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1><strong>Converter Application</strong></h1>
            <p>
                &nbsp;</p>

            <div>
                <h2><strong>Currency Converter</strong></h2>
                <p>
                    Convert:
                </p>

                <p>
                    <asp:TextBox ID="CurrOneInput" runat="server" Text="10"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="CurrenciesDropdown1" runat="server" AutoPostBack="True"></asp:DropDownList>
                </p>

                <p>
                    to
                </p>

                <p>
                    <asp:TextBox ID="CurrTwoInout" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="CurrenciesDropdown2" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:Button ID="CurrencyConvertBtn" runat="server" Text="Convert" Width="289px" OnClick="CurrencyConvertBtn_Click" />
                </p>
            </div>

            <br /> <hr />

            <div>
                <h2><strong>Temperature Converter</strong></h2>
                <p>
                    
                    <asp:TextBox ID="TempInput" runat="server" Text="10"></asp:TextBox> 
                    <asp:DropDownList ID="TemperatureFormatDropdown" runat="server"></asp:DropDownList> &nbsp;&nbsp;

                    <br />

                    <strong>Result:</strong> &nbsp; <asp:Label ID="TempConversionResultLbl" runat="server" Text=""></asp:Label>

                    <br />

                    <asp:Button ID="TempConvertBtn" runat="server" Text="Convert" Width="289px" OnClick="TempConvertBtn_Click" />
                </p>
            </div>
        </div>
    </form>
</body>
</html>
