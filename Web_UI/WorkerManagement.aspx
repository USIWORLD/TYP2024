<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="WorkerManagement.aspx.cs" Inherits="Farmacy_App.WorkerManagement" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Farmacy Management System</title>
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
        <section class="section dashboard">
            <div class="row">
                <div class="col-lg-10">
                    <div class="row">
                        <div class="col-xxl-4 col-md-12">
                            <h2 style="color: #0043a6">Worker Management</h2>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span>Available Workers Today</span>
                                    <asp:Button class='project-participants__add' ID="btnAddWorker" runat="server" OnClick="btnAddWorker_Click" Text=" + " Style="float: right" />
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Name</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Surname</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Email</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Availability</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>

                                            </asp:TableHeaderRow>

                                        </asp:Table>
                                        <p id="error" runat="server"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--End of right side-->
                <div class="col-lg-2">
                    <div>
                        <br /><br /><br />
                    </div>
                    <div class="card">

                        <div class="card-body">
                            <h5 class="card-title">Filter</h5>

                            <div class="d-flex align-items-center">
                                <div class="ps-3">
                                    <a href="#" onclick="filterTable('all')" class="card-desc">Show All</a><br />
                                    <a href="#" onclick="filterTable('task')" class="card-desc">By Task</a><br />
                                </div>
                            </div>

                        </div>


                    </div>
                </div>
            </div>
            <script>
        function filterTable(category) {
            var rows = document.querySelectorAll('#aTable');
            rows.forEach(function(row) {
                if (category === 'all') {
                    row.classList.remove('hidden');
                } else {
                    if (row.getAttribute('data-category') === category) {
                        row.classList.remove('hidden');
                    } else {
                        row.classList.add('hidden');
                    }
                }
            });
        }
    </script>
        </section>
    </main>



    <%--<div class="container">
        <h2>Worker Management Center</h2>
        <br />
        <div class="panel panel-default">
            <div class="panel-heading">
                Available Workers Today
            </div>
            <div class="panel-body">
                <div class="table-responsive"> <!-- JQUERY SCRIPTS -->
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
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Worker ID</th>
                                <th>Allocated Tasks</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                                <td>100090</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Jacob</td>
                                <td>Thornton</td>
                                <td>@fat</td>
                                <td>100090</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Larry</td>
                                <td>the Bird</td>
                                <td>@twitter</td>
                                <td>100090</td>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                                <td>100090</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Jacob</td>
                                <td>Thornton</td>
                                <td>@fat</td>
                                <td>100090</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Larry</td>
                                <td>the Bird</td>
                                <td>@twitter</td>
                                <td>100090</td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <h3>Add a worker</h3>
        <Label>Please fill in the form below to add a new worker:</Label>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12">
            <div class="form-group">
                <label>Worker ID</label>
                <input class="form-control" id="WorkerID" />
                <label>Worker Email</label>
                <input class="form-control" id="WorkerEmail" />
                <label>Worker Contact Number</label>
                <input class="form-control" id="ContactNum" />
            </div>
        </div>
        <div class="col-md-5 col-sm-12 col-xs-12">
            <div class="form-group">
                <label>Worker Name</label>
                <input class="form-control" id="WorkerName" />
                <label>Worker Surname</label>
                <input class="form-control" id="WorkerSurname" />
                <label>Worker Job Title</label>
                <input class="form-control" id="JobTitle" />
            </div>
            </div>
        </div>
        <asp:Button class="btn btn-primary" runat="server" Text="Submit" />
    </div>--%>

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
