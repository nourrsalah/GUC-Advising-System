<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllFunctionalities.aspx.cs" Inherits="WebApplication1.AllFunctionalities" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Functionalities</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HyperLink ID="hyperlink1" runat="server" Text="List all advisors in the system" NavigateUrl="~/Hyperlink1.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink2" runat="server" Text="List all students with their corresponding advisors in the system" NavigateUrl="~/Hyperlink2.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink3" runat="server" Text="List all pending requests" NavigateUrl="~/Hyperlink3.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink4" runat="server" Text="Add a new semester" NavigateUrl="~/Hyperlink4.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink5" runat="server" Text="Add a new course" NavigateUrl="~/Hyperlink5.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink6" runat="server" Text="Link instructor to a course in a specific slot" NavigateUrl="~/Hyperlink6.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink7" runat="server" Text="Link a student to an advisor" NavigateUrl="~/Hyperlink7.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink8" runat="server" Text="Link a student to a course with a specific instructor" NavigateUrl="~/Hyperlink8.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink9" runat="server" Text="View all details of instructors along with their assigned courses" NavigateUrl="~/Hyperlink9.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink10" runat="server" Text="Fetch all semesters along with their offered courses" NavigateUrl="~/Hyperlink10.aspx" />
        </div>
            <asp:HyperLink ID="hyperlink11" runat="server" Text=" Delete a course along with its related slots" NavigateUrl="~/Hyperlink11.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink12" runat="server" Text=" Delete a slot of a certain course if the course isn’t offered in the current semester" NavigateUrl="~/Hyperlink12.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink13" runat="server" Text=" Add makeup exam for a certain course" NavigateUrl="~/Hyperlink13.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink14" runat="server" Text=" View details for all payments along with their corresponding students " NavigateUrl="~/Hyperlink14.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink15" runat="server" Text=" Issue installments as per the number of installments for a certain payment " NavigateUrl="~/Hyperlink15.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink16" runat="server" Text="Update a student status based on his/her financial status" NavigateUrl="~/Hyperlink16.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink17" runat="server" Text="Fetch all details of active students" NavigateUrl="~/Hyperlink17.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink18" runat="server" Text="View all graduation plans along with their initiated advisors" NavigateUrl="~/Hyperlink18.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink19" runat="server" Text=" View all students transcript details" NavigateUrl="~/Hyperlink19.aspx" />
        </div>
        <div>
            <asp:HyperLink ID="hyperlink20" runat="server" Text="Fetch all semesters along with their offered courses" NavigateUrl="~/Hyperlink20.aspx" />
        </div>
        <div>


    </form>
</body>
</html>
