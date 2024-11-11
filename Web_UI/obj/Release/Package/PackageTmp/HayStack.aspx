<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="HayStack.aspx.cs" Inherits="Farmacy_App.HayStack" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
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
        <section class="section dashboard" id="header">
            <div class="row">
                <!-- Main Content Column -->
                <div class="col-lg-10">
                    <div class="card">
                        <div class="card-body">
                            <h2 style="color: #0043a6">HayStack Management</h2>
                            <div class="panel panel-default"></div>
                            <div class="panel-body">
                                <canvas id="Chart" runat="server"></canvas>

                                <div class="table-responsive" style="display:none">
                                    <!-- Filtering doesn't work here -->
                                    <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                        <asp:TableHeaderRow Style="font-weight: normal;">
                                            <asp:TableHeaderCell Style="font-weight: normal;">Animal Tag</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Initial Weight(kg)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Month-End Weight(kg)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">HayStack Required(kg)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Hay OverAllocation (kg)</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Allocated Cost</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Month-End Cost</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Variance</asp:TableHeaderCell>
                                            <asp:TableHeaderCell Style="font-weight: normal;">Name</asp:TableHeaderCell>

                                        </asp:TableHeaderRow>
                                    </asp:Table>
                                    
                                    <label style="color: #0043a6; display: inline-block; margin-right: 20px;" visible="false">Total Hay Required:</label>
                                    <asp:TextBox runat="server" ID="total" style="display: inline-block; border-radius:4px;margin-right: 40px;"></asp:TextBox>

                                    <label style="color: #0043a6; display: inline-block; margin-right: 20px;" visible="false">Total Expenditure:</label>
                                    <asp:TextBox runat="server" ID="cost" style="display: inline-block;border-radius:4px;"></asp:TextBox>
                                    <br />
                                    <br />
                                    <p id="err" runat="server"></p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Label runat="server" ID="Label1" style="color: #0043a6;font-weight: 400;font-size: 1.2em;">Recommendations:</asp:Label>

                    <div class="card" style="background-color:transparent;align-content:flex-start">
                        <div class="card-body">
                            <asp:Label runat="server" ID="recommend2" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>
                            <asp:Label runat="server" ID="recommend" style="color: black; display: block; font-size: 1.1em; font-weight: 400; padding: 5px 0; width: auto;"></asp:Label>
                            <a href="AllocateTask3.aspx" class="btn btn-primary">Allocate Task</a>
                        </div>
                    </div>
                </div>

                <!-- Filter Column -->
                <div class="col-lg-2">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title" style="margin-top:2px;width:100px">Filter</h5>
                            <div class="d-flex flex-column">
                                <asp:Button ID="btnShowAll" runat="server" style="border-radius: 4px" CssClass="btn btn-primary" Text="Show All" OnClick="btnShowAll_Click" />
                                <br />
                                <asp:DropDownList ID="ddlHealthStatus" runat="server" CssClass="card-desc" style="border-radius: 4px" AutoPostBack="True" OnSelectedIndexChanged="ddlHealthStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="Health Status" />
                                    <asp:ListItem Value="Healthy" Text="Healthy" />
                                    <asp:ListItem Value="Exposed" Text="Exposed" />
                                    <asp:ListItem Value="HighRisk" Text="HighRisk" />
                                </asp:DropDownList>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            function extractTableData() {
                var table = document.getElementById('<%= aTable.ClientID %>');
            var labels = [];
            var allocatedCosts = [];
            var monthEndCosts = [];

            for (var i = 1; i < table.rows.length; i++) { // Skip header row
                var cells = table.rows[i].cells;
                labels.push(cells[cells.length - 1].innerText); // Take the last column
                var allocatedCost = parseFloat(cells[5].innerText.replace(/[^0-9.-]+/g, "").trim()) / 100;
                var monthEndCost = parseFloat(cells[6].innerText.replace(/[^0-9.-]+/g, "").trim()) / 100;

                // Extract New_Weight instead of A_Weight
                var weight = parseFloat(cells[2].innerText.replace(/[^0-9.-]+/g, "").trim());

                allocatedCosts.push(allocatedCost);
                monthEndCosts.push(monthEndCost);
            }

            return { labels: labels, allocatedCosts: allocatedCosts, monthEndCosts: monthEndCosts };
        }

        var chartData = extractTableData();

        if (chartData.labels.length > 0 && chartData.allocatedCosts.length > 0 && chartData.monthEndCosts.length > 0) {
            var ctx = document.getElementById('<%= Chart.ClientID %>').getContext('2d');
            var dataChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: chartData.labels,
                    datasets: [
                        {
                            label: 'Allocated Cost (R)',
                            data: chartData.allocatedCosts,
                            backgroundColor: 'RGB(59, 204, 105)',
                            borderColor: 'RGB(59, 204, 105)',
                            borderWidth: 1
                        },
                        {
                            label: 'Month-End Cost (R)',
                            data: chartData.monthEndCosts,
                            backgroundColor: 'RGB(245, 88, 99)',
                            borderColor: 'RGB(245, 88, 99)',
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            beginAtZero: true,
                            title: {
                                display: true,
                                color: '#08061c',
                                text: 'Animals',
                                font: {
                                    size: 14,
                                }
                            }
                        },
                        y: {
                            beginAtZero: true,
                            title: {
                                display: true,
                                color: '#08061c',
                                text: 'Cost per Animal (R)',
                                font: {
                                    size: 14,
                                }
                            }
                        }
                    }
                }
            });
        }
    });
    </script>
</asp:Content>
