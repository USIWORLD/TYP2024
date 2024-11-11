<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="AddAnimal.aspx.cs" Inherits="Farmacy_App.AddAnimal" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!-- BOOTSTRAP STYLES-->
 <link href="assets/css/bootstrap.css" rel="stylesheet" />
 <!-- FONTAWESOME STYLES-->
 <link href="assets/css/font-awesome.css" rel="stylesheet" />
 <!-- MORRIS CHART STYLES-->
 <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
 <!-- CUSTOM STYLES-->
 <link href="assets/css/custom.css" rel="stylesheet" />
 <!-- MAIN STYLE -->
 <link href="assets/css/style1.css" rel="stylesheet" />
    <link href="assets/css/style.css" rel="stylesheet" />
 <!-- GOOGLE FONTS-->
 <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <H2 style="color: #0043a6;">Animal Management</H2>
        <div class="panel panel-default" style="color: #1c1a1a" >
            <div class="panel-heading" style="color: #0043a6" >
               Add New Animals
            </div>
            <div class="panel-body">

     
    
     <Label style="color: #0043a6;font-weight:normal;">Please fill in the form below to add a new animal:</Label>
    <div class="row">

        <div class="col-md-5 col-sm-12 col-xs-12">
            
            
         <div class="form-group">
            <label  style="font-weight:normal;">Name</label>
            <asp:TextBox class="form-control" id="name" runat="server"></asp:TextBox>
            </div>

           <div class="form-group">
            <label   style="font-weight:normal;">Animal Gender</label>
            <asp:DropDownList class="form-control" ID="ddlGender" runat="server">
               
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
            </asp:DropDownList>
            </div>

            <div class="form-group">
             <label  style="font-weight:normal;"> Animal Breed</label>
             <asp:DropDownList class="form-control"  ID="ddlSACowBreed" runat="server">
              
            <asp:ListItem Text="Nguni" Value="Nguni"></asp:ListItem>
            <asp:ListItem Text="Bonsmara" Value="Bonsmara"></asp:ListItem>
            <asp:ListItem Text="Afrikaner" Value="Afrikaner"></asp:ListItem>
            <asp:ListItem Text="Hereford" Value="Hereford"></asp:ListItem>
            </asp:DropDownList>
            </div>

             <div class="form-group">
             <label  style="font-weight:normal;"> Animal Colour</label>
             <asp:DropDownList class="form-control" ID="ddlCowColor" runat="server">
              
            <asp:ListItem Text="Brown" Value="Brown"></asp:ListItem>
            <asp:ListItem Text="Black" Value="Black"></asp:ListItem>
            <asp:ListItem Text="White" Value="White"></asp:ListItem>
            <asp:ListItem Text="Mixed" Value="Mixed"></asp:ListItem>
            <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
             </asp:DropDownList>

             </div>

                    <div class="form-group">
    <label  style="font-weight:normal;">Animal Health Status</label>
     <asp:DropDownList class="form-control" ID="ddlHealthStatus" runat="server">
      <asp:ListItem Text="Unknown" Value="Unknown"></asp:ListItem>
    <asp:ListItem Text="Healthy" Value="Healthy"></asp:ListItem>
    <asp:ListItem Text="Exposed" Value="Exposed"></asp:ListItem>
    <asp:ListItem Text="HighRisk" Value="HighRisk"></asp:ListItem>
</asp:DropDownList>
</div>


              
       
        </div>

    <div class="col-md-5 col-sm-12 col-xs-12">
  
         <div class="form-group">
                <label style="font-weight:normal;"> Animal Weight(in kg)</label>
                <asp:TextBox class="form-control" id="weight"   runat="server" ></asp:TextBox>
            </div>

        <div class="form-group">
            <label style="font-weight:normal;">Date Acquired</label>
            <asp:TextBox class="form-control" id="date" runat="server"></asp:TextBox>
        </div>


         <div class="form-group">
            <label  style="font-weight:normal;">Animal Age(Month)</label>
            <asp:TextBox class="form-control" id="Age" runat="server"></asp:TextBox>
            </div>
        

         <div class="form-group">
            <label  style="font-weight:normal;">Kraal</label>
             <asp:DropDownList class="form-control" ID="ddlKraal" runat="server">
             <asp:ListItem Text="Arrival" Value="0"></asp:ListItem>
            <asp:ListItem Text="One" Value="1"></asp:ListItem>
            <asp:ListItem Text="Two" Value="2"></asp:ListItem>
            <asp:ListItem Text="Three" Value="3"></asp:ListItem>
            <asp:ListItem Text="Quarantine" Value="4"></asp:ListItem>
             <asp:ListItem Text="FMD confirmed" Value="5"></asp:ListItem>
                
        </asp:DropDownList>
        </div>
        </div>
         

     </div>   
            <asp:Button ID="newID"  runat ="server" Text="Submit" OnClick="AddAnimal_Click" class="btn btn-primary"/> 
            <asp:Label ID="error" runat="server"></asp:Label>
            <asp:Literal ID="litMessage" runat="server" />

            <asp:Label ID="added" runat="server" ></asp:Label>
    </div>

</div>

</div>
    
           
           



      <!-- JQUERY SCRIPTS -->
  <script src="assets/js/jquery-1.10.2.js"></script>
  <!-- BOOTSTRAP SCRIPTS -->
  <script src="assets/js/bootstrap.min.js"></script>
  <!-- METISMENU SCRIPTS -->
  <script src="assets/js/jquery.metisMenu.js"></script>
  <!-- MORRIS CHART SCRIPTS -->
  <script src="assets/js/morris/raphael-2.1.0.min.js"></script>
  <script src="assets/js/morris/morris.js"></script>
  <!-- CUSTOM SCRIPTS -->
  <script src="assets/js/custom.js"></script>
</asp:Content>
