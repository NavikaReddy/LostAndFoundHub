<%@ Page Title="" Language="C#" MasterPageFile="~/HomeLayout.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="LostAndFound.Chat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
        
        /* Styles for sender's message */
.sender-message {
    text-align: right;
    color: #ffffff;
    background-color: #007bff; /* Change the background color as needed */
    padding: 10px;
    margin-bottom: 10px;
    border-radius: 5px;
    float:right;
}

/* Styles for receiver's message */
.receiver-message {
    text-align: left;
    color: #000000;
    background-color: #e6e6e6; /* Change the background color as needed */
    padding: 10px;
    margin-bottom: 10px;
    border-radius: 5px;
    float:left;
}


        /* Left side styling (20% of page) */
        #leftSide {
            float: left;
            width: 20%;
            background-color: #f2f2f2;
            padding: 20px;
            overflow-y: auto; /* Add scrollbar for the user list */
            height: 75vh; /* Adjust the height as needed */
        }

        #leftSide a {
            color: #3366cc;
            display: block;
            margin-bottom: 10px;
        }

        /* Right side styling (80% of page) */
        #rightSide {
            width: 80%;
            padding: 20px;
            overflow-y: auto; /* Add scrollbar for the chat content */
            height: 71vh; /* Adjust the height as needed */
        }


        #Label1 {
    margin-bottom: 10px;
    position: fixed;
    top: 0;
    left: 40%; /* Adjust left position to match the width of the left side */
    width: 70%; /* Adjust width to match the width of the right side */
    background-color: #f2f2f2; /* Adjust background color as needed */
    z-index: 1; /* Ensure it appears above other elements */
}


        /* UpdatePanel1 styling */
        #UpdatePanel1 {
            background-color: #e6e6e6;
            padding: 20px;
        }

        /* TextBox1 and Button1 styling */
        #TextBox1 {
            display: block;
            margin-bottom: 10px;
            //width: calc(100% - 22px); /* Adjust the width as needed */
            height: 51px;
        }

        #Button1 {
            display: block;
            margin-top: 10px; /* Add space between the text box and the button */
        }
</style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="leftSide">
    <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" Height="1">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" ForeColor="Black" runat="server" Text='<%# Bind("name") %>' OnClick="LinkButton1_Click" CommandArgument='<%# Bind("uid") %>'></asp:LinkButton>
        </ItemTemplate>
    </asp:DataList>
        </div>
        <asp:Label ID="Label1" runat="server" class="fw-bold"></asp:Label>
    <div id="rightSide">
    <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>
<asp:DataList ID="DataList2" runat="server" RepeatDirection="Vertical" Height="1">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server"
            Text='<%# Bind("Message") %>'
            CssClass='<%# Eval("Sender").ToString().Equals(Session["name"].ToString(), StringComparison.OrdinalIgnoreCase) ? "sender-message" : "receiver-message" %>'
        ></asp:Label>
    </ItemTemplate>
</asp:DataList>

        </ContentTemplate>
    </asp:UpdatePanel>
<asp:PlaceHolder ID="ChatControls" runat="server">
                    <asp:TextBox ID="TextBox1" runat="server" width="400px" height="51px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click" />
                </asp:PlaceHolder>
    </div>
</asp:Content>
