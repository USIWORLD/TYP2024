<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebFarmApplication.Register"  Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Design by foolishdeveloper.com -->
    <title>Farmacy Registration</title>
 
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;500;600&display=swap" rel="stylesheet">
    <!--Stylesheet-->
    <style media="screen">
      *,
*:before,
*:after{
    padding: 0;
    margin: 0;
    box-sizing: border-box;
}
body{
    background-color: #080710;
}
.background{
    width: 480px;
    height: 520px;
    position: fixed;
    transform: translate(-50%,-50%);
    left: 50%;
    top: 50%;
	background-image: url('cows.jfif');
    background-size: cover;
	height: 100%;
	width: 100%;
}

form{
    height: 680px;
    width: 400px;
    background-color: rgba(255,255,255,0.13);
    top: 50%;
    left: 50%;
    border-radius: 10px;
    backdrop-filter: blur(10px);
    border: 2px solid rgba(255,255,255,0.1);
    box-shadow: 0 0 40px rgba(8,7,16,0.6);
    padding: 50px 35px;
}
form *{
    font-family: 'Poppins',sans-serif;
    color: #ffffff;
    letter-spacing: 0.5px;
    outline: none;
    border: none;
}
form h3{
    font-size: 32px;
    font-weight: 500;
    line-height: 42px;
    text-align: center;
}

label{
    display: block;
    margin-top: 30px;
    font-size: 16px;
    font-weight: 500;
}
input{
    display: block;
    height: 50px;
    width: 100%;
    background-color: rgba(255,255,255,0.07);
    border-radius: 3px;
    padding: 0 10px;
    margin-top: 8px;
    font-size: 14px;
    font-weight: 300;
}
::placeholder{
    color: #e5e5e5;
}
.name{
	
	width: 45%;
	float: left;
}
.type{
    width: 100%;
    float: left;
}
.surname{
	float: right;
	margin: 0px;
	width: 45%;
}
button{
	display: block;
	margin:  auto;
    margin-top: 50px;
    width: 50%;
    background-color: #ffffff;
    color: #080710;
    padding: 15px 0;
    font-size: 18px;
    font-weight: 600;
    border-radius: 5px;
    cursor: pointer;
}
.social{
  margin-top: 20px;
  display: flex;
}
.social div{
  background: red;
  width: 250px;
  border-radius: 3px;
  padding: 5px 10px 10px 5px;
  background-color: rgba(255,255,255,0.27);
  color: #eaf0fb;
  text-align: center;
}
.social div:hover{
  background-color: rgba(255,255,255,0.47);
}
.social .fb{
  margin-left: 25px;
}
.social i{
  margin-right: 4px;
}
.background .backText {
text-align: center;
margin-top: 25px;
filter: brightness(1.0);
color: #ffffff;
font-size: 50px;
}
.mainDiv{
    margin-top: 150px;
    margin-left: 550px;

}

    </style>

</head>
<body>
    <div class="mainDiv">
        <div class="background">
            <div class="shape"></div>
            <div class="shape"></div>
            <div class="backText">
                <h1>FARMACY</h1>
            </div>
        </div>

        <form runat="server" class="form">
            <h3>Register</h3>

            <label for="username">Provided Manager ID</label>
            <asp:TextBox ID="username" runat="server" placeholder="Manager ID" class="input-field" ></asp:TextBox>

            <div class="name">
                <label for="name">Name</label>
                <asp:TextBox ID="name" runat="server" placeholder="Name" class="input-field"></asp:TextBox>
            </div>

            <div class="surname">
                <label for="surname">Surname</label>
                <asp:TextBox ID="surname" runat="server" placeholder="Surname" class="input-field"></asp:TextBox>
            </div>

            <div class="name">
                <label for="email">Email</label>
                <asp:TextBox ID="email" runat="server" placeholder="Email" class="input-field"></asp:TextBox>
            </div>

            <div class="surname">
                <label for="password">Password</label>
                <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Password" class="input-field"></asp:TextBox>
            </div>

            <div class="type">
                <label for="usertype">User Type</label>
            <asp:TextBox ID="usertype" runat="server" placeholder="User Type" class="input-field"></asp:TextBox>

            <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButton_Click" class="button" />

            <div class="social">
                <p>Already Registered? <a href="Login.aspx">Sign in.</a></p>
            </div>

            <p id="err" runat="server"></p>
            </div>
            
        </form>
    </div>
</body>
</html>
