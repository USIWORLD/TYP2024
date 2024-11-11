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

                            <h2 style="text-align: center; color: royalblue;">Health Status Graph</h2>
                            <br />
                            <div style="display: flex; justify-content: center; align-items: center; margin-bottom: 20px;">
                                <div style="width: 80%; display: flex; flex-direction: column;">
                                    <div style="display: flex; justify-content: flex-end; margin-bottom: -3px; margin-right: 120px;">
                                        <%--<asp:DropDownList ID="ddlKraal" runat="server" CssClass="card-desc" Style="border-radius: 2px; width: 20%; height: 25px; margin-right: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlWeight_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Text="Kraal 1" />
                                            <asp:ListItem Value="2" Text="Kraal 2" />
                                            <asp:ListItem Value="3" Text="Kraal 3" />
                                            <asp:ListItem Value="4" Text="Kraal 4" />
                                        </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlWeight" runat="server" CssClass="card-desc" Style="border-radius: 2px; width: 20%; height: 25px; margin-right: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlWeight_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="Weight" />
                                            <asp:ListItem Value="0-100" Text="0-100" />
                                            <asp:ListItem Value="100-200" Text="100-200" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlAge" runat="server" CssClass="card-desc" Style="border-radius: 2px; width: 20%; height: 25px; margin-right: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlAge_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="Age" />
                                            <asp:ListItem Value="0-10" Text="0-10" />
                                            <asp:ListItem Value="10-20" Text="10-20" />
                                            <asp:ListItem Value="20-30" Text="20-30" />
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="Gender" runat="server" CssClass="card-desc" Style="border-radius: 2px; width: 20%; height: 25px;" AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="Gender" />
                                            <asp:ListItem Value="Male" Text="Male" />
                                            <asp:ListItem Value="Female" Text="Female" />
                                        </asp:DropDownList>
                                    </div>
                                    <div id="ChartContainer2" style="width: 100%; height: 450px; margin: 0 auto;">
                                        <canvas id="ChartTwo"></canvas>
                                    </div>
                                </div>
                            </div>

                            <!-- Additional Space Below the Graph -->
                            <div style="margin-top: 40px;"></div>



                            <!-- Hidden fields to store animal data -->
                            <asp:HiddenField ID="AnimalIds" runat="server" />
                            <asp:HiddenField ID="HealthStatus" runat="server" />
                            <asp:HiddenField ID="AnimalNames" runat="server" />


                            <div class="col-md-6">
                                <div style="display: none;">
                                    <asp:Label ID="costAllo" runat="server"></asp:Label>
                                    <asp:Label ID="Cost" runat="server"></asp:Label>
                                    <asp:Label ID="hvar" runat="server"></asp:Label>
                                    <asp:Label ID="SanCost" runat="server"></asp:Label>
                                    <asp:Label ID="sanvar" runat="server"></asp:Label>
                                    <asp:Label ID="medcost" runat="server"></asp:Label>
                                    <asp:Label ID="medvar" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <script>
                            document.addEventListener('DOMContentLoaded', function () {
                                // Get values from hidden fields
                                var animalIds = document.getElementById('<%= AnimalIds.ClientID %>').value;
        var healthStatus = document.getElementById('<%= HealthStatus.ClientID %>').value;
        var animalNames = document.getElementById('<%= AnimalNames.ClientID %>').value;

        // Parse data into arrays
        var ids = animalIds.split(",");
        var statuses = healthStatus.split(",");
        var names = animalNames.split(",");

        // Create a mapping of health statuses to numbers and colors
        var statusMapping = {
            'FMD Confirmed': { value: 5, color: 'rgba(212, 16, 2)' }, // Red
            'Healthy': { value: 4, color: 'rgba(14, 173, 9)' }, // Green
            'HighRisk': { value: 3, color: 'rgba(250, 243, 32)' }, // Yellow
            'Exposed': { value: 2, color: 'rgba(18,33,201)' }, // Blue
            'Unknown': { value: 1, color: 'rgba(138, 134, 134)' } // Grey
        };

        // Convert Health Status to numbers and colors based on the mapping
        var numericStatuses = statuses.map(function (status) {
            return statusMapping[status]?.value || 0; // Default to 0 for unknown statuses
        });

        var pointColors = statuses.map(function (status) {
            return statusMapping[status]?.color || 'rgba(201, 203, 207, 1)'; // Default color
        });

        // Create a line chart
        var ctx = document.getElementById('ChartTwo').getContext('2d');
        var chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: names, // Animal names
                datasets: [{
                    label: 'Health Status',
                    data: numericStatuses, // Mapped health statuses
                    backgroundColor: 'rgba(75, 192, 192, 0.2)', // Light green for area under the line
                    borderColor: 'rgba(75, 192, 192, 1)', // Dark green for line
                    borderWidth: 2,
                    pointRadius: 5, // Size of data point markers
                    pointHoverRadius: 7, // Size of data point on hover
                    pointBackgroundColor: pointColors, // Colors for points based on status
                    pointBorderColor: 'rgba(0, 0, 0, 1)', // Optional: border color for points
                    pointBorderWidth: 1 // Optional: border width for points
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: false,
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                const value = tooltipItem.raw;
                                return Object.keys(statusMapping).find(key => statusMapping[key].value === value) || 'Unknown';
                            }
                        }
                    }
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Animal Names',
                            color: '#08061c', // Set y-axis label color
                            font: {
                                size: 17 // Set the font size
                            },
                        },
                        ticks: {
                            color: '#08061c', // Set color for x-axis ticks
                            autoSkip: false
                        },
                    },
                    y: {
                        title: {
                            display: true, // Display y-axis title
                            text: 'Health Status',
                            color: '#08061c', // Set y-axis label color
                            font: {
                                size: 17 // Set the font size
                            },
                        },
                        beginAtZero: true,
                        min: 0, // Set minimum value to create extra space at the bottom
                        max: 6, // Set maximum value to create extra space above the highest status (5 for FMD Confirmed)
                        ticks: {
                            color: '#08061c', // Set color for x-axis ticks
                            autoSkip: false,
                            stepSize: 1,
                            callback: function (value) {
                                return Object.keys(statusMapping).find(key => statusMapping[key].value === value) || '';
                            }
                        }
                    }
                }
            }
        });
    });
                        </script>


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

                    <div class="col-xxl-6 col-md-10">
                        <div class="card info-card sales-card">

                            <div class="card-body">
                                <h5 class="card-title">FMD Confirmed Cows</h5>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:Table ID="medTable" runat="server" class="table">
                                            <asp:TableHeaderRow Style="font-weight: normal;">
                                                <asp:TableHeaderCell Style="font-weight: normal;">Cow Name</asp:TableHeaderCell>
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

                    <div class="col-xxl-6 col-md-10">
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

