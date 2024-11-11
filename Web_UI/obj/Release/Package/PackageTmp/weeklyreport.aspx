    <%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="weeklyreport.aspx.cs" Inherits="Farmacy_App.weeklyreport" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Working version of https://dribbble.com/shots/14552329--Exploration-Task-Management-Dashboard -->
    <title>Weekly Report</title>
    <!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.css" rel="stylesheet"/>
    <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet"/>
    <!-- MORRIS CHART STYLES-->
    <link href="assets/js/morris/morris-0.4.3.min.css" rel="stylesheet"/>
    <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet"/>
    <!-- MAIN STYLE -->
    <link href="assets/css/style1.css" rel="stylesheet"/>
    <link href="assets/css/style.css" rel="stylesheet"/>
    <!-- GOOGLE FONTS-->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <main id="main" class="main">
    <div class="pagetitle">
      <h1>Monthly Report</h1>
      <nav>
        <ol class="breadcrumb">
          <li class="breadcrumb-item"><a href="dashboard.aspx">Manager</a></li>
          <li class="breadcrumb-item active">Report</li>
        </ol>
      </nav>
    </div>
    <!-- End Page Title -->

    <section class="section">
      <div class="row">
        <div class="col-lg-12">
          <!-- Current Month Report -->
          <div class="card">
            <div class="card-body">
              <h6 class="card-title">Current Month</h6>
                <h6 class="card-title" style="margin: 0; text-align: right; margin-top: -60px;">
                    <a href="AddBudget.aspx" style="text-decoration: none; color: inherit;">Allocate Budget</a>
                    </h6>
                <h2  style="text-align: center;color:royalblue">Budget Graph</h2>
              <div id="ChartContainer" style="width: 900px; height: 400px; margin: 0 auto; text-decoration-color:inherit">
                <canvas id="Chart"></canvas>
              </div>
            </div>

             <%-- <div class="col-md-6">
                <div class="table-responsive">
                  <table class="table table-bordered" style="width: 50%; margin: 0 auto;margin-right:12px">
                    <thead>
                      <tr>
                        <th>Health Status</th>
                        <th>Count</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <td>Healthy</td>
                        <td><asp:Label ID="lblH" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                        <td>Exposed</td>
                        <td><asp:Label ID="lblE" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                        <td>HighRisk</td>
                        <td><a href="HighRisk.aspx"><asp:Label ID="lblHR" style="color:#DC3545" runat="server"></asp:Label></a></td>
                      </tr>
                      <tr>
                        <td>FMDConfirmed</td>
                        <td><a href="FMD.aspx"><asp:Label ID="lblFMD" style="color:#DC3545" runat="server"></asp:Label></a></td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>--%>
              <br />
              <br />
       
                                

   <h2 style="text-align: center; color: royalblue;">Health Status Graph</h2><br />
<div style="display: flex; justify-content: center; align-items: center; margin-bottom: 20px;">
    <div style="width: 80%; display: flex; flex-direction: column;">
        <div style="display: flex; justify-content: flex-end; margin-bottom: -3px; margin-right:120px;">
            <asp:DropDownList ID="ddlWeight" runat="server" CssClass="card-desc" style="border-radius: 2px; width: 10%; height: 25px; margin-right: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlWeight_SelectedIndexChanged">
                <asp:ListItem Value="" Text="Weight" />
                <asp:ListItem Value="0-100" Text="0-100" />
                <asp:ListItem Value="100-200" Text="100-200" />
            </asp:DropDownList>
            <asp:DropDownList ID="ddlAge" runat="server" CssClass="card-desc" style="border-radius: 2px; width: 10%; height: 25px; margin-right: 10px;" AutoPostBack="True" OnSelectedIndexChanged="ddlAge_SelectedIndexChanged">
                <asp:ListItem Value="" Text="Age" />
                <asp:ListItem Value="0-10" Text="0-10" />
                <asp:ListItem Value="10-20" Text="10-20" />
                <asp:ListItem Value="20-30" Text="20-30" />
            </asp:DropDownList>
            <asp:DropDownList ID="Gender" runat="server" CssClass="card-desc" style="border-radius: 2px; width: 10%; height: 25px;" AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
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
                <div style="display:none;">
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
              
                    <!-- Previous Month Report -->
                <%--    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title">Previous Month</h6>
                     <div id="chartContainer" style="width: 100%; height: 400px; overflow: hidden;">
                        <canvas id="budgetChart" style="display: block; max-width: 100%; max-height: 100%;"></canvas>
                    </div>

                        </div>
                           <div class="col-md-6">
                                <div class="table-responsive">
                                                 <div class="table-responsive">
                                              <table class="table table-bordered" style="width: 50%;">
                                        <thead>
                                            <tr>
                                                <th>Health Status</th>
                                                <th>Count</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>Healthy</td>
                                                <td>7</td>
                                            </tr>
                                            <tr>
                                                <td>Exposed</td>
                                                <td>0</td>
                                            </tr>
                                            <tr>
                                                <td>HighRisk</td>
                                                <td>0</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                                    </div>
                                                               </div>
                                                             
                                                            </div>
                                                               </div>
                

                                                        <asp:Button type="button" ID="btnQuestionnaire" class="btn btn-primary" runat="server" Text="View Questionnaire" OnClick="btnQuestionnaires_Click" Async="true" />--%>      
                                                        <p id="err" runat="server"></p>
                                                    </div>                                 </div>
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
                                 <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0"></script>

              
                         <script>
                             document.addEventListener('DOMContentLoaded', function () {
                            var costAllo = parseFloat(document.getElementById('<%= costAllo.ClientID %>').innerText) || 0;
                            var Cost = parseFloat(document.getElementById('<%= Cost.ClientID %>').innerText) || 0;
                            var SanCost = parseFloat(document.getElementById('<%= SanCost.ClientID %>').innerText) || 0;
                            var medcost = parseFloat(document.getElementById('<%= medcost.ClientID %>').innerText) || 0;

                                 var ctx = document.getElementById('Chart').getContext('2d');
                                 var urls = ['Haystack.aspx', 'Sanitation.aspx', 'MedicationCost.aspx'];

                            var budget = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: ['HayStack', 'Sanitation', 'Medication'],
                                    datasets: [
                                        {
                                            label: 'Budgeted (R)',
                                            data: [costAllo, 315, 1200],  // Budgeted values
                                            backgroundColor: 'RGB(3, 223, 252)',  // Greenish bars for budgeted
                                            borderColor: 'RGB(160, 177, 232)',
                                            borderWidth: 1,
                                            barThickness: 60,  // Increase thickness to fit the interval
                                            categoryPercentage: 0.8,  // Reduce space between categories
                                            barPercentage: 1.0  // Reduce space within the category
                                        },
                                        {
                                            label: 'Actual Spend (R)',
                                            data: [Cost, SanCost, medcost],  // Actual spend values
                                            backgroundColor: 'RGB(62, 89, 181)',  // Reddish bars for actual spend
                                            borderColor: 'RGB(160, 177, 232)',
                                            borderWidth: 1,
                                            barThickness: 60,  // Increase thickness to fit the interval
                                            categoryPercentage: 30.5,  // Reduce space between categories
                                            barPercentage: 1.0  // Reduce space within the category
                                        }
                                    ]
                                },
                                options: {
                                    responsive: true,
                                    maintainAspectRatio: false,
                                    scales: {
                                        x: {
                                            beginAtZero: true,
                                            title: {
                                                display: true,
                                                text: 'Categories',
                                                color: '#08061c',
                                                font: {
                                                    size: 15  // Set the font size
                                                }

                                            },
                                            ticks: {
                                                color: '#08061c',  // Set color for x-axis ticks
                                                autoSkip: false
                                            },
                                            grid: {
                                                display: true
                                            }
                                        },
                                        y: {
                                            beginAtZero: true,
                                            title: {
                                                display: true,
                                                text: 'Amount (R)',
                                                color: '#08061c' , // Set y-axis label color
                                                font: {
                                                    size: 15  // Set the font size
                                                }
                                            },
                                            ticks: {
                                                color: '#08061c',  // Set color for y-axis ticks
                                                stepSize: 400,
                                                max: 1200,
                                                min: 0
                                            },
                                            grid: {
                                                display: true
                                            }
                                        }
                                    },
                                    plugins: {
                                        datalabels: {
                                            color: '#333',
                                            font: {
                                                size: 12,
                                                weight: 'normal'
                                            },
                                            formatter: (value, context) => {
                                                return value;
                                            },
                                            anchor: 'end',
                                            align: 'top'
                                        },
                                        plugins: [ChartDataLabels]
                                    },

                                    onClick: (e, elements) => {
                                        if (elements.length > 0) {
                                            var index = elements[0].index;
                                            var url = urls[index];  // Get the corresponding URL

                                            // Redirect to the URL within the same tab
                                            window.location.href = url;
                                        }
                                    }
                                }
                            });
                        });
                        </script>



                        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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


<%--<!-- Canvas element for the chart -->

<!-- Canvas element for the chart -->


<!-- Canvas element for the chart -->

                            <script>
                                document.addEventListener('DOMContentLoaded', function () {
                                    var ctx = document.getElementById('budgetChart').getContext('2d');

                                    var urls = ['Haystack.aspx', 'Sanitation.aspx', 'MedicationCost.aspx'];

                                    var budget = new Chart(ctx, {
                                        type: 'bar',
                                        data: {
                                            labels: ['Animal Feed', 'Sanitation', 'Medication'],
                                            datasets: [
                                                {
                                                    label: 'Budgeted (R)',
                                                    data: [1000, 315, 1100],  // Static budgeted values
                                                    backgroundColor: 'RGB(100, 126, 209)',  // Greenish bars for budgeted
                                                    borderColor: 'RGB(160, 177, 232)',
                                                    borderWidth: 1,
                                                    barThickness: 60,  // Adjust bar thickness
                                                    categoryPercentage: 0.8,  // Reduce space between categories
                                                    barPercentage: 1.0  // Reduce space within the category
                                                },
                                                {
                                                    label: 'Actual Spend (R)',
                                                    data: [1000, 315, 1100],  // Static actual spend values
                                                    backgroundColor: 'RGB(62, 89, 181)',  // Reddish bars for actual spend
                                                    borderColor: 'RGB(160, 177, 232)',
                                                    borderWidth: 1,
                                                    barThickness: 60,  // Adjust bar thickness
                                                    categoryPercentage: 0.8,  // Reduce space between categories
                                                    barPercentage: 1.0  // Reduce space within the category
                                                }
                                            ]
                                        },
                                        options: {
                                            responsive: true,
                                            maintainAspectRatio: false,  // Ensure it fits within the card container
                                            scales: {
                                                x: {
                                                    beginAtZero: true,
                                                    title: {
                                                        display: true,
                                                        text: 'Categories',
                                                        color: '#08061c',
                                                        font: {
                                                            size: 15  // Font size for x-axis title
                                                        }
                                                    },
                                                    ticks: {
                                                        color: '#08061c',  // Color for x-axis ticks
                                                        autoSkip: false
                                                    },
                                                    grid: {
                                                        display: true
                                                    }
                                                },
                                                y: {
                                                    beginAtZero: true,
                                                    title: {
                                                        display: true,
                                                        text: 'Amount (R)',
                                                        color: '#08061c', // Color for y-axis title
                                                        font: {
                                                            size: 15  // Font size for y-axis title
                                                        }
                                                    },
                                                    ticks: {
                                                        color: '#08061c',  // Color for y-axis ticks
                                                        stepSize: 400,
                                                        max: 1200,
                                                        min: 0
                                                    },
                                                    grid: {
                                                        display: true
                                                    }
                                                }
                                            },
                                            plugins: {
                                                datalabels: {
                                                    color: '#333',
                                                    font: {
                                                        size: 12,
                                                        weight: 'normal'
                                                    },
                                                    formatter: (value, context) => {
                                                        return value;
                                                    },
                                                    anchor: 'end',
                                                    align: 'top'
                                                }
                                            },
                                            onClick: (e, elements) => {
                                                if (elements.length > 0) {
                                                    var index = elements[0].index;
                                                    var url = urls[index];  // Get the corresponding URL

                                                    // Redirect to the URL within the same tab
                                                    window.location.href = url;
                                                }
                                            }
                                        }
                                    });
                                });
                            </script>--%>
<%--  --%>
</asp:Content>
