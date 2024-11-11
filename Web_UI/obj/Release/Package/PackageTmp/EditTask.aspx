<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Farmacy_App.EditTask" async ="true"%>
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
    <main id="main" class="main">
        <div class="pagetitle">
            <h1>Edit Task</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Edit Task</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

           <section class="section">
        <div class="row">

            <div class="col-lg-12">

                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Edit in the task details below:</h5>

                        <!-- General Form Elements -->
                        <form>
                          <div class="col-md-5 col-sm-12 col-xs-12">
                              <div class="form-group" >
                                    <label for="inputCity5" class="form-label" style="font-weight:normal;">Task Name</label>
                                    <asp:TextBox class="form-control" id="TaskName"   runat="server" ></asp:TextBox>
                                </div>
                           

                                 <div class="form-group">
                                       <label class="form-label" style="font-weight:normal;">Task Description</label>
                                       <asp:TextBox class="form-control" TextMode="MultiLine" ID="description"   runat="server" Style="height:110px;" ></asp:TextBox>
                                   </div>
                             </div>

                            <div class="col-md-6">
                                
                                <div class="col-sm-10">
                                    <label class="form-label" style="font-weight:normal;">Task Category</label>
                                    <asp:DropDownList class="form-control" ID="ddlCategory" runat="server">
                                    <asp:ListItem Text="Animal Management" Value="AnimalManagement"></asp:ListItem>
                                    <asp:ListItem Text="Health and Veterinary Care" Value="HealthAndVeterinaryCare"></asp:ListItem>
                                    <asp:ListItem Text="Breeding and Reproduction" Value="BreedingAndReproduction"></asp:ListItem>
                                    <asp:ListItem Text="Feeding and Nutrition" Value="FeedingAndNutrition"></asp:ListItem>
                                    <asp:ListItem Text="Cleaning and Maintenance" Value="CleaningAndMaintenance"></asp:ListItem>
                                    <asp:ListItem Text="Administrative and Record Keeping" Value="AdministrativeAndRecordKeeping"></asp:ListItem>
                                    <asp:ListItem Text="FMD Management" Value="FMDManagement"></asp:ListItem>
                                    <asp:ListItem Text="Research and Development" Value="ResearchAndDevelopment"></asp:ListItem>
                                    <asp:ListItem Text="Training and Education" Value="TrainingAndEducation"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-10"><br />
                                <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" OnClick="BtnSubmit_Click" Text="Submit Task" />
                                <label id="response" runat="server"></label>
                            </div>
                            <p ID="error" runat="server"></p>
                        </form>
                    </div>
                </div>
            </div>
          </div>

     
    </section>
</main>

    <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

  <!-- Vendor JS Files -->
  <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
  <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets/vendor/chart.js/chart.umd.js"></script>
  <script src="assets/vendor/echarts/echarts.min.js"></script>
  <script src="assets/vendor/quill/quill.js"></script>
  <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
  <script src="assets/vendor/tinymce/tinymce.min.js"></script>
  <script src="assets/vendor/php-email-form/validate.js"></script>

  <!-- Template Main JS File -->
  <script src="assets/js/main.js"></script>
</asp:Content>
