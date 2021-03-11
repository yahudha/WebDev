<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="ChurchApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            User ID : <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <br />

            Password  : <asp:TextBox ID="txtPasword" runat="server"></asp:TextBox>

            <br />

            <asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click"></asp:Button>

        </div>
    </form>
</body>
</html>
