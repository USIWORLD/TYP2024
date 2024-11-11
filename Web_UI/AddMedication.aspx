<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="AddMedication.aspx.cs" Inherits="Farmacy_App.AddMedication" Async="true" %>
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
     <H2 style="color: #0043a6;">Animal Medication Management</H2>
     <div class="panel panel-default" style="color: #1c1a1a" >
         <div class="panel-heading" style="color: #0043a6" >
            Add New Medication
         </div>
         <div class="panel-body">

  
 
  <Label style="color: #0043a6;font-weight:normal;">Please fill in the form below to add a new medications:</Label>
 <div class="row">

     <div class="col-md-5 col-sm-12 col-xs-12">
  

            <div class="form-group">
              <label style="font-weight:normal;"> Name</label>
              <asp:TextBox class="form-control" id="name"   runat="server" ></asp:TextBox>
          </div>

            <div class="form-group">
              <label style="font-weight:normal;"> Dosage</label>
              <asp:TextBox class="form-control" id="dosage"   runat="server" ></asp:TextBox>
          </div>

          <div class="form-group">
          <label  style="font-weight:normal;"> Frequency</label>
          <asp:DropDownList class="form-control" ID="ddlfreq" runat="server">
           
         <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
         <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
         <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
          </asp:DropDownList>

          </div>

          <div class="form-group">
              <label  style="font-weight:normal;">Category</label>
               <asp:DropDownList class="form-control" ID="dllcategory" runat="server">
 
              <asp:ListItem Text="Injection" Value="Injection"></asp:ListItem>
              <asp:ListItem Text="Liquid" Value="Liquid"></asp:ListItem>
              <asp:ListItem Text="Tablet" Value="Tablet"></asp:ListItem>
          </asp:DropDownList>
          </div>

           <div class="form-group">
            <label  style="font-weight:normal;">Type</label>
             <asp:DropDownList class="form-control" ID="dlltype" runat="server">
 
            <asp:ListItem Text="Disinfectants" Value="Disinfectants"></asp:ListItem>
            <asp:ListItem Text="Antibiotic" Value="Antibiotic"></asp:ListItem>
            <asp:ListItem Text="ImmuneBooster" Value="ImmuneBooster"></asp:ListItem>
        </asp:DropDownList>
        </div>
         <asp:Button ID="newID"  runat ="server" Text="Submit" OnClick="AddAnimal_Click" class="btn btn-primary"/> 
    <asp:Label ID="error" runat="server"></asp:Label>
         </div>
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
