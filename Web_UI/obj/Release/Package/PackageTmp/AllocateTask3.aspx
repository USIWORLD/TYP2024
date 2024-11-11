<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="AllocateTask3.aspx.cs" Inherits="Farmacy_App.AllocateTask3" Async="true" %>

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
         <script src = "https://unpkg.com/sweetalert/dist/sweetalert.min.js" ></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <main id="main" class="main">
        <div class="pagetitle">
            <h1>Task Allocation</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="dashboard.aspx">Home</a></li>
                    <li class="breadcrumb-item active">Allocate Task</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">

                <div class="col-lg-6">

                    <div class="card">
                        <div class="card-body">
                            <h6 class="card-title"></h6>

                            <div class="col-md-12 col-sm-12 col-xs-12">

                                <div class="form-group">
                                    <label style="font-weight: normal;">Task Name</label>
                                    <asp:DropDownList class="form-control" ID="ddlId" AutoPostBack="False" OnSelectedIndexChanged="ddlId_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label style="font-weight: normal;">Available Workers</label>
                                    <asp:DropDownList class="form-control" ID="ddlname" runat="server"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label style="font-weight: normal;">Date</label>
                                    <asp:TextBox class="form-control" ID="date" runat="server"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                 <asp:Label ID="lblfeed" runat="server" Text="Feed Amount:" Visible="False"></asp:Label>
                                  <asp:TextBox ID="feed"   class="form-control"  runat="server" Visible="False"></asp:TextBox>
                                  </div>

                                <div class="form-group">
                                 <asp:Label ID="lblsanitize" runat="server" Text="Size to Sanitize" Visible="False"></asp:Label>
                                  <asp:TextBox ID="sanitize"   class="form-control"  runat="server" Visible="False"></asp:TextBox>
                                  </div>

                                <div class="form-group">
                                     <asp:Label ID="lblKraal" runat="server" Text="Kraal" Visible="False"></asp:Label>
                                         <asp:DropDownList class="form-control" ID="ddlKraal" runat="server" visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlKraal_SelectedIndexChanged">
    
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                                             </asp:DropDownList>
                                    </div>

                                <asp:Button ID="newID" class="btn btn-primary" runat="server" Text="Allocate" OnClick="AllocateTask_Click" />
                                <p id="err" runat="server"></p>
                                <asp:Literal ID="litMessage" runat="server" />

                                <asp:Label ID="added" runat="server" Text=""></asp:Label>
                                <asp:HiddenField ID="hfCurrentPage" runat="server" Value="1" />


                            </div>


                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-body">
                                <div class="panel-body">
                                    <h4 style="color:royalblue;font-size: 18px;font-weight: bold;">Select One or Multiple animals for each task</h4>
                                    <div class="table-responsive">
                                        <asp:Table ID="ATable" runat="server" class="table table-striped table-bordered table-hover">
                                            <asp:TableHeaderRow Style="font-weight: normal;">
                                                <asp:TableHeaderCell Style="font-weight: normal;">Name</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">HealthStatus</asp:TableHeaderCell>
                                                  <asp:TableHeaderCell Style="font-weight: normal;">Recommended Task</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>

                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                          <div id="pagination" style="margin-top: 20px;"></div>

                                        <p id="P1" runat="server" visible="true"></p>
                                    </div>
                                </div>
                               <!--<div class="col-md-12 col-sm-12 col-xs-12">
                           <div class="form-group">
                               <label style="font-weight: normal;">Select Animal</label>
                               <asp:DropDownList class="form-control" ID="DropDownList1" runat="server"></asp:DropDownList>
                           </div>-->
                            </div>
                        </div>


                    </div>
                </div>

       
        </section>
    </main>


    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <script>
    const rowsPerPage = 4; // Show 4 rows per page
    let currentPage = 1;
    const table = document.getElementById("<%= ATable.ClientID %>");
    const rows = table.getElementsByTagName("tr");
    const totalRows = rows.length - 1; // Exclude header row
    const totalPages = Math.ceil(totalRows / rowsPerPage);

    function showPage(page) {
        currentPage = page;
  // Hide all data rows (excluding header)
        for (let i = 1; i < rows.length; i++) {
            rows[i].style.display = 'none'; // Hide all data rows
        }

        // Determine the start and end indices based on the current page
        const start = (page - 1) * rowsPerPage + 1; // Start from 1 to skip header row
        const end = start + rowsPerPage - 1; // Determine end index

        // Show the rows for the current page
        for (let i = start; i <= end; i++) {
            if (i < rows.length) { // Ensure we don't go out of bounds
                rows[i].style.display = ''; // Show the row
            }
        }

        updatePagination(); // Update pagination buttons
        document.getElementById("<%= hfCurrentPage.ClientID %>").value = page;

    }

        // Initialize pagination after animal data is populated
        function initPagination() {
            const rows = document.getElementById("<%= ATable.ClientID %>").getElementsByTagName("tr");
    const totalRows = rows.length - 1; // Exclude header row
    const totalPages = Math.ceil(totalRows / rowsPerPage);

    // Update pagination variables
    window.totalRows = totalRows;
    window.totalPages = totalPages;

    // Call showPage with the initial page value
    showPage(document.getElementById("<%= hfCurrentPage.ClientID %>").value);
        }


        function updatePagination() {
        const pagination = document.getElementById("pagination");
        pagination.innerHTML = ""; // Clear existing buttons

        for (let i = 1; i <= totalPages; i++) {
            const button = document.createElement("button");
            button.textContent = i;
            button.className = "pagination-button";
            button.style.margin = "5px";
            button.style.padding = "5px 10px";
            button.style.border = "1px solid #007bff";
            button.style.backgroundColor = (i === currentPage) ? "#007bff" : "white";
            button.style.color = (i === currentPage) ? "white" : "#007bff";
            button.style.borderRadius = "5px";
            button.style.cursor = "pointer";

            // Add click event to show the respective page
            button.onclick = (function (page) {
                return function () {
                    showPage(page);
                };
            })(i);

            pagination.appendChild(button);
        }
    }

    // Initialize pagination on page load
    window.onload = function() {
        showPage(document.getElementById("<%= hfCurrentPage.ClientID %>").value);
    };
    </script>

          
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

                                <script>
                                    function showCustomAlert(message) {
                                        Swal.fire({
                                            title: 'Alert',
                                            text: message,
                                            width: '350px',
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
                                    font-size: 14px;  /* Adjust the size as needed */
                                }
                            </style>

 
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
