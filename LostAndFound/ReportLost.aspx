<%@ Page Title="" Language="C#" MasterPageFile="~/HomeLayout.Master" AutoEventWireup="true" CodeBehind="ReportLost.aspx.cs" Inherits="LostAndFound.ReportLost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
     <style>
        * {
            box-sizing: border-box;
        }

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
            font-family: Arial;
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
            margin-top: 6px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>Report Lost Form</h1>
    <asp:HiddenField ID="hdn" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-10">
                <label for="lname">Enter product Name:</label>
            </div>
            <div class="col-90">
                <asp:TextBox ID="txtname" runat="server" placeholder="Enter product name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtname" ErrorMessage="Enter Product Name" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-10">
                <label for="ldesc">Description:</label>
            </div>
            <div class="col-90">
                <asp:TextBox ID="txtLDesc" runat="server" TextMode="MultiLine" Rows="4" placeholder="Enter Description" MaxLength="255"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLDesc" ErrorMessage="Enter Description" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        
        <div class="row">
            <div class="col-10">
                <label for="llostAt">Lost At:</label>
            </div>
            <div class="col-90">
                <asp:TextBox ID="txtLLostAt" runat="server" placeholder="Enter Lost At" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLLostAt" ErrorMessage="This is required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>
        
       <div class="row">
            <div class="col-10">
                <label for="ldate">lost Date:</label>
            </div>
<div class="col-90">
    <asp:TextBox ID="txtldate" runat="server" Width="200px" TextMode="Date"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtldate" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtldate" ErrorMessage="You can't enter future date" ClientValidationFunction="validateDate" ForeColor="Red"></asp:CustomValidator>
</div>
        </div>
        
        
        <div class="row">
            <div class="col-10">
                <label for="image">Upload Image:</label>
            </div>
            <div class="col-90">
                <asp:FileUpload ID="fileUpload" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fileUpload" ErrorMessage="This is Required" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
        </div>



        <div class="row">
            <asp:Button ID="btnUpdate" runat="server" Text="Save Data" OnClick="btnUpdate_Click"/>
        </div>
       
    </div>
    <script type="text/javascript">
    function validateDate(sender, args) {
        var enteredDate = new Date(args.Value);
        var currentDate = new Date();

        // Set the time part of the current date to midnight
        currentDate.setHours(23, 59, 59, 999);

        if (enteredDate > currentDate) {
            args.IsValid = false;
        } else {
            args.IsValid = true;
        }
    }
    </script>
</asp:Content>
