<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BotService.aspx.cs" Inherits="LostAndFound.BotService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Website with a Bot</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <script type="text/javascript">
            (function () {
                var scriptElement = document.createElement('script');
                scriptElement.type = 'text/javascript';
                scriptElement.async = false;
                scriptElement.src = '/BotService.aspx?Get=Script';
                (document.getElementsByTagName('head')[0] || 
                 document.getElementsByTagName('body')[0]).appendChild(scriptElement);
            })();
        </script>
    </form>
</body>
</html>
