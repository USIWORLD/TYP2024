<%@ Page Title="" Language="C#" MasterPageFile="~/VetMaster.Master" AutoEventWireup="true" CodeBehind="VetDashboard.aspx.cs" Inherits="Farmacy_App.VetDashboard" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManage2r" runat="server"></asp:ScriptManager>
    <div class="background">
        <main id="main" class="main">
            <asp:Label ID="HealthyCows" Style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="HighRiskCows" Style="color: #ffffff00;" runat="server"></asp:Label>
            <asp:Label ID="ExposedCows" Style="color: #ffffff00;" runat="server"></asp:Label>
            <div class="pagetitle">
                <h1 style="color: #ffffff;">Dashboard</h1>
                <nav>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="VetDashboard.aspx">Veterinarian</a></li>
                        <li class="breadcrumb-item active">Dashboard</li>
                    </ol>
                </nav>
            </div>
            <!-- End Page Title -->
            <section class="section dashboard">

                <div class="row">
                    <!-- Left side columns -->
                    <div class="col-lg-12">
                        <div class="row">

                            <div class="col-md-12" style="display: inline-block;">
                                <div class="card info-card sales-card">
                                    <div class="card-body">
                                        <h5 class="card-title">Farm Stats</h5>

                                        <div class="col-lg-3" style="display: inline-block;">
                                            <div class="d-flex align-items-center">
                                                <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                    <i class="bi bi-heart-fill"></i>
                                                </div>
                                                <div class="ps-3">
                                                    <h6>Healthy: <asp:Label ID="lblH" runat="server"></asp:Label></h6>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-3" style="display: inline-block;">
                                            <div class="d-flex align-items-center">
                                                <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                    <i class="bi bi-heart-half"></i>
                                                </div>
                                                <div class="ps-3">
                                                    <h6>Exposed: <asp:Label ID="lblEx" runat="server"></asp:Label></h6>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-3" style="display: inline-block;">
                                            <div class="d-flex align-items-center">
                                                <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                    <i class="bi bi-shield-exclamation"></i>
                                                </div>
                                                <div class="ps-3">
                                                    <h6>High Risk: <asp:Label ID="lblHR" runat="server"></asp:Label></h6>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-1.5" style="display: inline-block;">
                                            <div class="d-flex align-items-center">
                                                <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                    <i class="bi bi-shield-plus"></i>
                                                </div>
                                                <div class="ps-3">
                                                    <h6>FMD +ve: <asp:Label ID="lblFMD" runat="server"></asp:Label></h6>

                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                </div>
                            </div>

                            <!-- Tasks -->
                            <div class="col-xxl-6 col-md-6">
                                <div class="card info-card sales-card">
                                    <div class="card-body pb-0">
                                        <h5 class="card-title">Health Tracker</h5>

                                        <div id="trafficChart" style="min-height: 400px;" class="echart"></div>

                                        <script>
                                            document.addEventListener("DOMContentLoaded", () => {
                                                const chart = echarts.init(document.querySelector("#trafficChart"));

                                                const option = {
                                                    tooltip: {
                                                        trigger: 'item'
                                                    },
                                                    legend: {
                                                        top: '5%',
                                                        left: 'center'
                                                    },
                                                    series: [{
                                                        name: 'Access From',
                                                        type: 'pie',
                                                        radius: ['40%', '70%'],
                                                        avoidLabelOverlap: false,
                                                        label: {
                                                            show: false,
                                                            position: 'center'
                                                        },
                                                        emphasis: {
                                                            label: {
                                                                show: true,
                                                                fontSize: '18',
                                                                fontWeight: 'bold'
                                                            }
                                                        },
                                                        labelLine: {
                                                            show: false
                                                        },
                                                        data: [
                                                            {
                                                                value:  <%= lblEx.Text %>,
                                                            name: 'Exposed'
                                                        },
                                                        {
                                                            value: <%= lblH.Text %>,
                                                            name: 'Healthy'
                                                        },
                                                        {
                                                            value: <%= lblHR.Text %>,
                                                            name: 'High Risk'
                                                        },
                                                        {
                                                            value:  <%= lblFMD.Text %>,
                                                            name: 'FMD Confirmed'
                                                        }
                                                    ]
                                                }]
                                            };

                                            chart.setOption(option)

                                            // Event listener for clicks on chart segments
                                            chart.on('click', function (params) {
                                                if (params.name === 'High Risk') {
                                                    window.location.href = 'HighRisk.aspx';
                                                } else if (params.name === 'FMD Confirmed') {
                                                    window.location.href = 'medicationAdmin.aspx';
                                                }
                                            })
                                        })
                                        </script>
                                    </div>


                                </div>
                            </div>
                            <!-- End Tasks -->

                            <!-- Right side columns -->
                            <div class="col-lg-6">
                                <div class="card info-card sales-card">
                                    <div class="card-body">
                                        <h5 class="card-title">Medicines <span>| Available</span></h5>
                                        <div class="card info-card sales-card">
                                            <div class="d-flex align-items-center">
                                                <a href="MedicationManagement.aspx">
                                                <div class="card-icon rounded-circle d-flex align-items-center justify-content-center">
                                                    <i class="bi bi-shield-plus"></i>
                                                </div></a>
                                                <div class="ps-3">
                                                    <a href="MedicationManagement.aspx">
                                                    <h6>
                                                        <asp:Label ID="lblMed" runat="server"></asp:Label>
                                                        Medicines</h6>
                                                        </a>
                                                </div>
                                                    
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card info-card sales-card">
                                    <div class="card-body">
                                        <h5 class="card-title">Farm Appointments</h5>

                                        <div class="d-flex align-items-center">
                                            <div class="ps-3">
                                                <h6 class="card-title">Dates:</h6>
                                                
                                                <div runat="server" id="VisitContent">
                                                     <%--<h6 style="color: #0043a6;">26-08-2024 <span>R300.00</span></h6>--%>
                                                </div>

                                            </div>
                                        </div>
                                        <p id="P1" runat="server"></p>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <p id="err" runat="server"></p>

                </div>

            </section>

        </main>

        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

                                   <script>
                                       function showCustomAlert(message) {
                                           Swal.fire({
                                               title: 'Alert',
                                               text: message,
                                               width: '450px',
                                               position: 'top',
                                               padding: '0.3em',  // Custom padding                                          
                                               confirmButtonText: 'Ok',
                                               confirmButtonColor: '#3085d6',  // Default SweetAlert blue color
                                               customClass: 'custom-swal',
                                           });

                                       }
    </script>
    
<style>
    .custom-swal .swal2-html-container {
        font-size: 15px;  /* Adjust the size as needed */
    }
</style>

        <!-- End #main -->

        <!-- ======= Footer ======= -->
        <footer id="footer" class="footer">
            <div class="copyright">
                &copy; Copyright <strong><span>NiceAdmin</span></strong>. All Rights Reserved
            </div>
            <div class="credits">
                <!-- All the links in the footer should remain intact. -->
                <!-- You can delete the links only if you purchased the pro version. -->
                <!-- Licensing information: https://bootstrapmade.com/license/ -->
                <!-- Purchase the pro version with working PHP/AJAX contact form: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/ -->
                Designed by <a href="https://bootstrapmade.com/">BootstrapMade</a>
            </div>
        </footer>
        <!-- End Footer -->

        <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>
    </div>
    <!-- Vendor JS Files -->
    <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/chart.js/chart.umd.js"></script>
    <script src="assets/vendor/echarts/echarts.min.js"></script>
    <script src="assets/vendor/quill/quill.js"></script>
    <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
    <script src="assets/vendor/tinymce/tinymce.min.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>
    <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>
    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <!-- Farm Visualizer JS File -->
    <script src="assets/js/scripts.js"></script>
</asp:Content>
