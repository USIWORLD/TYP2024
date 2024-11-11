<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="ConductReport.aspx.cs" Inherits="Farmacy_App.ConductReport" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Working version of https://dribbble.com/shots/14552329--Exploration-Task-Management-Dashboard -->
    <title>Task Manager</title>
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
            <h1>Vet Reporting</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="VetDashboard.aspx">Home</a></li>
                    <li class="breadcrumb-item active" style="color: #0043a6;">Vet Reporting</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">

                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Examining Animal:
                                <asp:Label ID="lblAID" runat="server"></asp:Label></h6>

                            <div class="row">

                                <div class="col-md-5 col-sm-12 col-xs-12">


                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Gender</label>
                                        <asp:DropDownList class="form-control" ID="ddlGender" runat="server">

                                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Breed</label>
                                        <asp:DropDownList class="form-control" ID="ddlSACowBreed" runat="server">

                                            <asp:ListItem Text="Nguni" Value="Nguni"></asp:ListItem>
                                            <asp:ListItem Text="Bonsmara" Value="Bonsmara"></asp:ListItem>
                                            <asp:ListItem Text="Afrikaner" Value="Afrikaner"></asp:ListItem>
                                            <asp:ListItem Text="Hereford" Value="Hereford"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Colour</label>
                                        <asp:DropDownList class="form-control" ID="ddlCowColor" runat="server">

                                            <asp:ListItem Text="Brown" Value="Brown"></asp:ListItem>
                                            <asp:ListItem Text="Black" Value="Black"></asp:ListItem>
                                            <asp:ListItem Text="White" Value="White"></asp:ListItem>
                                            <asp:ListItem Text="Mixed" Value="Mixed"></asp:ListItem>
                                            <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>


                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Weight</label>
                                        <asp:TextBox class="form-control" ID="weight" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-md-5 col-sm-12 col-xs-12">



                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Height</label>
                                        <asp:TextBox class="form-control" ID="height" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label style="font-weight: normal;">Date Acquired</label>
                                        <asp:TextBox class="form-control" ID="date" runat="server"></asp:TextBox>
                                    </div>


                                    <div class="form-group">
                                        <label style="font-weight: normal;">Animal Age</label>
                                        <asp:TextBox class="form-control" ID="Age" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label style="font-weight: normal;">Kraal</label>
                                        <asp:DropDownList class="form-control" ID="ddlKraal" runat="server">

                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="col-md-5 col-sm-12 col-xs-12">


                                    <div class="form-group">
                                        <label style="font-weight: normal;">Diagnose Animal by Status</label>

                                        <asp:DropDownList class="form-control" ID="ddlHealthStatus" runat="server">
                                            <asp:ListItem Text="Healthy" Value="Healthy"></asp:ListItem>
                                            <asp:ListItem Text="Exposed" Value="Exposed"></asp:ListItem>
                                            <asp:ListItem Text="HighRisk" Value="HighRisk"></asp:ListItem>
                                            <asp:ListItem Text="FMD Confirmed" Value="FMD Confirmed"></asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="AddAnimal_Click" class="btn btn-primary" />
                                    <p id="P1" runat="server"></p>
                                </div>
                                


                            </div>
                        </div>
                    </div>
                    <p id="err" runat="server" visible="true"></p>


                </div>


                <%--<div class="col-lg-4">

                    <div class="col-xxl-6 col-md-12">
                        <div class="card info-card sales-card">

                            <div class="card-body">
                                <h5 class="card-title">Prescribe Medication</h5>

                                <div class="form-group">
                                    <label style="font-weight: normal;">Select Medication</label>
                                    <div class="form-group">
                                    <label  style="font-weight:normal;">Select Medication</label>
                                     <asp:CheckBoxList ID="medCheckbox" runat="server">
                                    <asp:ListItem Text="Bio-Cevit" Value="Bio-Cevit"></asp:ListItem>
                                    <asp:ListItem Text="Bio-Ade+B.Complex" Value="Bio-Ade+B.Complex"></asp:ListItem>
                                    <asp:ListItem Text="Bio-Tylosin-Pc" Value="Bio-Tylosin-Pc"></asp:ListItem>
                                    <asp:ListItem Text="Bio-DOC" Value="Bio-DOC"></asp:ListItem>
                                         <asp:ListItem Text="Bioxide" Value="Bioxide"></asp:ListItem>
                                         <asp:ListItem Text="Biodine" Value="Biodine"></asp:ListItem>
                                </asp:CheckBoxList>


                                    <h5 class="card-title">Select Quantity(for each)</h5>
                                    <asp:DropDownList ID="quantity" runat="server">

                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--<asp:Button ID="Submit"  runat ="server" Text="Submit" OnClick="BtnSubmit_Click" class="btn btn-primary" />--%>

                                
                            </div>
                                    <p id="error" visible="false" runat="server"></p>
                        </div>
                    </div>



                </div>--%>

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

