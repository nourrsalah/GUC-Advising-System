<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hyperlink12.aspx.cs" Inherits="WebApplication1.Hyperlink12" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="semesterTextBox">Current semester:</label>
            <asp:TextBox ID="semesterTextBox" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnDeleteSlot" runat="server" Text="Delete Slot" OnClick="SubmitButton_Click" />
        </div>
    </form>
</body>
</html>
