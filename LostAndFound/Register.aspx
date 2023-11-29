<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="LostAndFound.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <title>Register</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
    <style>
 *{
        box-sizing: border-box;
    }
    body
{ background-color:rosybrown }

    input[type=text], input[type=email], input[type=number], input[type=select], input[type=date], input[type=select], input[type=password], input[type=tel] {
        width: 45%;
        padding: 12px;
        border: 1px solid rgb(168, 166, 166);
        border-radius: 4px;
        resize: vertical;
    }

    textarea {
        width: 45%;
        padding: 12px;
        border: 1px solid rgb(168, 166, 166);
        border-radius: 4px;
        resize: vertical;
    }

    input[type=radio], input[type=checkbox] {
        width: 1%;
        padding-left: 0%;
        border-radius: 4px;
        resize: vertical;
    }

    h1 {
        font-family: 'Segoe UI';
        font-size: medium;
        font-style: normal;
        font-weight: bold;
        color: brown;
        text-align: center;
        text-decoration: underline;
    }

    label {
        padding: 12px 12px 12px 0;
        display: inline-block;
    }

    input[type=submit] {
        background-color: #4CAF50;
        color: white;
        padding: 12px 20px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        float: left;
    }

        input[type=submit]:hover {
            background-color: #32a336;
        }

    .container {
        border-radius: 5px;
        background-color: #f2f2f2;
        padding: 20px;
    }

    .col-10 {
        float: left;
        width: 10%;
        margin-top: 6px;
    }

    .col-90 {
        float: left;
        width: 90%;
        margin-top: 15px;
    }

    .row:after {
        content: "";
        display: table;
        clear: both;
    }

    @media screen and (max-width: 600px) {
        .col-10, .col-90, input[type=submit] {
            width: 100%;
            margin-top: 0;
        }
    }
    .col-15 {
        float: left;
        width: 15%;
        margin-top: 6px;
    }

    .verify-btn {
        padding: 10px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        width: 80%; /* Adjust the width as needed */
        display: inline-block;
        text-align: center;
    }

    #verifyIcon {
        margin-left: 5px; /* Adjust the spacing between button and icon */
        font-size: 20px; /* Adjust the icon size as needed */
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
         <h1>Register</h1>
    <div class="container">
    <div class="row">
        <div class="col-10">
            <label for="name">Name:</label>
        </div>
        <div class="col-90">
            <asp:TextBox ID="txtname" runat="server" placeholder="Enter your name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtname" ErrorMessage="Name required" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="row">
    <div class="col-10">
        <label for="email">Email:</label>
    </div>
    <div class="col-30">
        <asp:TextBox ID="txtemail" runat="server" placeholder="Enter your Email"></asp:TextBox>
        <asp:RequiredFieldValidator ID="req2" runat="server" ControlToValidate="txtemail" ErrorMessage="Email Required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="REG1" runat="server" ControlToValidate="txtemail" ForeColor="Red" ErrorMessage="Email pattern is not correct" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>


    </div>

    <div class="col-15">
        <asp:Button ID="send" runat="server" Text="SendOTP" BackColor="Green" ForeColor="White" OnClick="send_Click"/>
    </div>
         <div class="col-30">
        <asp:TextBox ID="txtotp" runat="server" placeholder="Enter otp"></asp:TextBox>
    </div>   
           <div class="col-15">
       <asp:Button ID="verify" runat="server" Text="Verify" BackColor="Green" ForeColor="White" OnClick="verify_Click" CssClass="verify-btn"/>
    <i id="verifyIcon" runat="server" class="fas"></i>
    </div> 
</div>

    <div class="row">
    <div class="col-10">
        <label for="password">Password:</label>
    </div>
    <div class="col-90">
        <asp:TextBox ID="txtpass" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox>

    </div>
</div>
    <div class="row">
        <div class="col-10">
            <label for="mobile">Mobile Number:</label>
        </div>
        <div class="col-90">
            <asp:TextBox ID="txtmobile" runat="server" placeholder="Enter Mobile Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="requiredValidator" runat="server" ControlToValidate="txtmobile"
    ErrorMessage="Mobile number is required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="reg" runat="server" ControlToValidate="txtmobile" ErrorMessage="Plz Enter Correct Mobile" ForeColor="Red" ValidationExpression="^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$"></asp:RegularExpressionValidator>

        </div>
    </div>
     <div class="row">
        <asp:Button ID="register" runat="server" Text="Register" style="background-color: black; color: #fff; border: 1px solid #007bff;" OnClick="register_Click"/>
    </div>
   </div>
</div>
        <asp:Label ID="lbl" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
