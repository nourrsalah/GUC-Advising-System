<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink15.aspx.cs" Inherits="WebApplication1.Hyperlink15" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <label for="txtPaymentID">Payment ID:</label>
            <asp:TextBox ID="txtPaymentID" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
