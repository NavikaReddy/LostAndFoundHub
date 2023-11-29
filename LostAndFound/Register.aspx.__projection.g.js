
/* BEGIN EXTERNAL SOURCE */

            $(document).ready(function () {
                var timerDuration = 20; // Timer duration in seconds
                var timerInterval;

                // Start the timer
                function startTimer() {
                    var timer = timerDuration;
                    timerInterval = setInterval(function () {
                        var minutes = Math.floor(timer / 60);
                        var seconds = timer % 60;
                        var timeString = ('0' + minutes).slice(-2) + ':' + ('0' + seconds).slice(-2);
                        $('#timer').text(timeString);
                        timer--;

                        if (timer < 0) {
                            clearInterval(timerInterval);
                            $('#resend-btn').prop('disabled', false);
                        }
                    }, 1000);
                }

                // Resend button click event
                $('#resend-btn').click(function () {
                    $(this).prop('disabled', true);
                    startTimer();
                    // Send OTP request here
                });

                // Initial start of the timer
                startTimer();
            });
        </script

    <div class="row">
    <div class="col-10">
        <label for="password">Password:</label>
    </div>
    <div class="col-90">
        <asp:TextBox ID="txtpass" runat="server" placeholder="Enter Password"></asp:TextBox>
        <asp:TextBox ID="txtpwd" runat="server" placeholder="Enter Password"></asp:TextBox>

    </div>
</div>
    <div class="row">
        <div class="col-10">
            <label for="mobile">Mobile Number:</label>
        </div>
        <div class="col-90">
            <asp:TextBox ID="txtmobile" runat="server" placeholder="Enter Mobile Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="requiredValidator" runat="server" ControlToValidate="txtmobile"
    ErrorMessage="Mobile number is required" Display="Dynamic"></asp:RequiredFieldValidator>
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

/* END EXTERNAL SOURCE */
