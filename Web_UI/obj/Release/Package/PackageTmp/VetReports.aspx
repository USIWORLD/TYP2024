<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="VetReports.aspx.cs" Inherits="Farmacy_App.VetReports" Async="true" %>

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
            <h1>Report</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Home</a></li>
                    <li class="breadcrumb-item active" style="color: #0043a6;">Weekly Report</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">

                <div class="col-lg-8">

                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Animals to examine today <span>|
                                <asp:Label ID="lblVisitDate" runat="server"></asp:Label></span></h6>

                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                        <asp:TableHeaderRow Style="font-weight: normal;">
                                            <asp:TableHeaderCell Style="font-weight: normal;">Animal_Tag</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Animal Name</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Gender</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;"> Breed</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Colour</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Weight</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Height</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">HealthStatus</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Age</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Kraal</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                    <p id="P2" runat="server" visible="true"></p>
                                </div>
                            </div>


                            <!-- Line Chart -->
                            <!--div id="lineChart" style="min-height: 400px;" class="echart"></!--div>

                                            <script>
                                                document.addEventListener("DOMContentLoaded", () => {
                                                    echarts.init(document.querySelector("#lineChart")).setOption({
                                                        xAxis: {
                                                            type: 'category',
                                                            data: ['Kraal 1', 'Kraal 2', 'Kraal 3', 'Kraal 4', 'Kraal 5']
                                                        },
                                                        yAxis: {
                                                            type: 'value'
                                                        },
                                                        series: [{
                                                            data: [5, 8, 8, 8, 2, 0, 2],
                                                            type: 'line',
                                                            smooth: true
                                                        }]
                                                    });
                                                });
                                            </script-->
                            <!-- End Line Chart -->




                        </div>
                    </div>
                    <p id="err" runat="server" visible="true"></p>

                    <%--<div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Medication Usage graph | <span>Current Month</span></h5>
                                <div id="barChart" style="min-height: 400px;" class="echart"></div>

                                <script>
                                    document.addEventListener("DOMContentLoaded", () => {
                                        echarts.init(document.querySelector("#barChart")).setOption({
                                            xAxis: {
                                                type: 'category',
                                                data: ['Bio-Cevit', 'Bio-Ade', 'Bio-Tylo.', 'Bio-DOC', 'Bioxide', 'Biodine'],
                                                name: 'Medicine'
                                            },
                                            yAxis: {
                                                type: 'value',
                                                name: 'No. of Medicines Used'
                                            },
                                            series: [{
                                                data: [10, 4, 2, 6, 1, 2],
                                                type: 'bar'
                                            }]
                                        });
                                    });
                                </script>
                            </div>
                        </div>
                    </div>--%>

                    <div class="col-xxl-6 col-md-4">
                        <div class="card info-card revenue-card">

                            <div class="card-body">
                                <h6 class="card-title" style="color: #0043a6;">
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    <span>| This Month</span></h6>

                                <div class="d-flex align-items-center">
                                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                        <i class="bi bi-house-door" style="color: #0043a6; font-size: 30px; padding-top: 5px;"></i>
                                    </div>
                                    <div class="ps-3">
                                        <h5 class="card-title"><span>Total Animals</span></h5>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>


                    <div class="col-xxl-6 col-md-4">
                        <div class="card info-card revenue-card">

                            <div class="card-body">
                                <h6 class="card-title" style="color: #0043a6;">
                                    <asp:Label ID="lblFMD" runat="server"></asp:Label>
                                    <span>| Animals</span></h6>

                                <div class="d-flex align-items-center">
                                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                        <i class="bi bi-heart-half" style="color: #0043a6; font-size: 30px; padding-top: 5px;"></i>
                                    </div>
                                    <div class="ps-3">
                                        <h5 class="card-title"><span>FMD Confirmed</span></h5>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-xxl-6 col-md-4">
                        <div class="card info-card revenue-card">

                            <div class="card-body">
                                <h6 class="card-title" style="color: #0043a6;">
                                    <asp:Label ID="lblRecovered" runat="server"></asp:Label>
                                    <span>| Animals</span></h6>

                                <div class="d-flex align-items-center">
                                    <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                        <i class="bi bi-heart-fill" style="color: #0043a6; font-size: 30px; padding-top: 5px;"></i>
                                    </div>
                                    <div class="ps-3">
                                        <h5 class="card-title"><span>Fully Recovered</span></h5>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


                <div class="col-lg-4">

                    <div class="col-xxl-6 col-md-12">
                        <div class="card info-card sales-card">

                            <div class="card-body">
                                <h5 class="card-title">FMD Confirmed Animals</h5>
                                 <div class="panel-body">
                                     <div class="table-responsive">
                                         <asp:Table ID="medTable" runat="server" class="table">
                                             <asp:TableHeaderRow Style="font-weight: normal;">
                                                 <asp:TableHeaderCell Style="font-weight: normal;">Animal Tag</asp:TableHeaderCell>
                                                 <asp:TableHeaderCell Style="font-weight: normal;">Kraal</asp:TableHeaderCell>
                                                 <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                             </asp:TableHeaderRow>
                                         </asp:Table>
                                         <p id="P3" runat="server" visible="true"></p>
                                     </div>
                                 </div>
                                <p id="error" style="color: #ffff;" runat="server"></p>

                            </div>

                        </div>
                    </div>

                    <div class="col-xxl-6 col-md-12">
                        <div class="card info-card sales-card">

                            <div class="card-body">
                                <h5 class="card-title">Other Appointments</h5>

                                <div class="d-flex align-items-center">
                                    <div class="ps-3" id="VisitContent" runat="server">
                                    </div>
                                </div>
                                <p id="P1" runat="server"></p>
                            </div>

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

