<%@ Page Title="" Language="C#" MasterPageFile="~/NavMenu.Master" AutoEventWireup="true" CodeBehind="TaskManager.aspx.cs" Inherits="Farmacy_App.TaskManager" Async="true" %>

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
    <!--<div id="page-wrapper">
  <main class='project'>
    <div class='project-info'>
      <h1>Task Manager</h1> 
      
    </div>
    <div class='project-tasks'>-->
    <main id="main" class="main">
        <section class="section dashboard">
            <div class="row">
                <div class="col-lg-10">
                    <div class="row">
                        <div class="col-xxl-4 col-md-12">
                            <h2 style="color: #0043a6">Task Management</h2>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <span>Available  Task</span>
                                    <asp:Button class='project-participants__add' ID="btnAddTask" runat="server" OnClick="btnAddTask_Click" Text=" + " Style="float: right" />
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:Table ID="aTable" runat="server" class="table table-striped table-bordered table-hover">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Task_Name</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;"> Description</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;"> Category</asp:TableHeaderCell>
                                                <asp:TableHeaderCell Style="font-weight: normal;">Action</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                        <p id="err" runat="server" visible="true"></p>
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
                                    <a href="#" onclick="filterTable('Category')" class="card-desc">By Category</a><br />
                                    <a href="#" onclick="filterTable('Task-Name')" class="card-desc">By Task Name</a><br />
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

    <!--  <div class='project-column'>
        <div class='project-column-heading'>
          <h2 class='project-column-heading__title'>Task Ready</h2><button class='project-column-heading__options'></button>
        </div>
        <div id="taskList" runat="server">

        </div>
      </div>
      <div class='project-column'><div class='project-column-heading'>
          <h2 class='project-column-heading__title'>In Progress</h2><button class='project-column-heading__options'></button>
        </div>

          <div class='project-column'>
        <div id="taskdiv" class='task' draggable='false' runat="server">
          <div class='task__tags'><a href="EditTask.aspx"><span class='task__tag task__tag--copyright' href="EditTask.aspx">Feed the Cows</span></a><button class='task__options'></button></div>
          <br />
            <h5>Assigned To: Jonas</h5>
          <div class='task__stats'>
            <span><time datetime="2021-11-24T20:00:00"><i class="fas fa-flag"></i>Nov 24</time></span>
            <span><i class="fas fa-comment"></i>3</span>
          </div>
        </div>
      </div>
        
        </div>
      <div class='project-column'><div class='project-column-heading'>
          <h2 class='project-column-heading__title'>Done</h2><button class='project-column-heading__options'></button>
        </div>
        
         <div class='project-column'>
        <div class='task' draggable='false'>
          <div class='task__tags'><a href="EditTask.aspx"><span class='task__tag task__tag--illustration'>Administer Medication</span></a><button class='task__options'></button></div>
          <br />
            <h5>Assigned To: Jonas</h5>
          <div class='task__stats'>
            <span><time datetime="2021-11-24T20:00:00"><i class="fas fa-flag"></i>Nov 24</time></span>
            <span><i class="fas fa-comment"></i>3</span>
          </div>
        </div>
      </div>
        
        </div>
      
    </div>
  </main>
  <aside class='task-details'>
    <div class='tag-progress'>
      <h2>Task Progress</h2>
      <div class='tag-progress'>
        <p>Feeding Cows <span>3/8</span></p>
        <progress class="progress progress--copyright" max="8" value="3"> 3 </progress>
      </div>
      <div class='tag-progress'>
        <p>Medicine Administration <span>6/10</span></p>
        <progress class="progress progress--illustration" max="10" value="6"> 6 </progress>
      </div>
      <div class='tag-progress'>
        <p>Reporting <span>2/7</span></p>
        <progress class="progress progress--design" max="7" value="2"> 2 </progress>
      </div>
    </div>
  </aside>-->
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
