
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebFarmApplication.Login" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
        
<head runat="server">
   <meta charset="UTF-8">
  <title>Farmacy Login</title>
  <meta />
</head>

<!-- partial:index.partial.html -->
<!DOCTYPE html>
<html lang="en">
<head>
  <!-- Design by foolishdeveloper.com -->
    <title>Farmacy Login</title>
 
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
    width: 430px;
    height: 520px;
    position: absolute;
    transform: translate(-50%,-50%);
    left: 50%;
    top: 50%;
	background-image: url('cows.jfif');
    background-size: cover;
	height: 100%;
	width: 100%;
}
.background .shape{
    height: 100px;
    width: 200px;
    position: absolute;
    border-radius: 50%;
}

form{
    height: 480px;
    width: 470px;
    background-color: rgba(255,255,255,0.13);
    top: 50%;
    left: 50%;
    border-radius: 10px;
    backdrop-filter: blur(10px);
    border: 2px solid rgba(255,255,255,0.1);
    box-shadow: 0 0 40px rgba(8,7,16,0.6);
    padding: 30px 25px;
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
button{
display: block;
	margin: auto;
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
  margin-top: 30px;
  display: flex;
}
.social div{
  background: red;
  width: 150px;
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
    margin-left: 450px;

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
    <form runat ="server">
        <h3>Login</h3>

        <label for="username">Username</label>
        <asp:TextBox ID="username" runat="server" placeholder="ID"></asp:TextBox>

        <label for="password">Password</label>
        <asp:TextBox ID="password" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>

        <asp:Button ID="LoginButton" runat="server" Text="Log In" OnClick="LoginButton_Click" />

        <div class="social">
          <p>Don't have an account? <a href="Register.aspx">Register</a> </p>
        </div>
         <p id="err" runat="server" visible="true" > </p>

    </form>
   </div>
</body>
</html>
<!-- partial -->
  
</body>
</html>

