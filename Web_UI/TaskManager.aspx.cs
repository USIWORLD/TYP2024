using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Farmacy_App.AnimalManagement;

namespace Farmacy_App
{
    public partial class TaskManager : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        bool first = true;
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (first)
            {
                await PopulateTasksAsync();
                first = false;
            }

        }

        private async Task PopulateTasksAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetTasks");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<taskInfo>>(responseBody);

                foreach (var task in tasks)
                {

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(task.Task_Name.ToString()));
                    newRow.Cells.Add(CreateCell(task.Description.ToString()));
                    newRow.Cells.Add(CreateCell(task.Category.ToString()));


                    TableCell cell = new TableCell();

                    Panel panel = new Panel();
                    panel.CssClass = "button-panel";
                    panel.Attributes["style"] = "margin-bottom:5px;";

                    Button btnEdit = new Button
                    {
                        Text = "Edit",
                        CssClass = "btn btn-primary",
                        CommandArgument = task.Id.ToString(),
                        Width = 100,

                    };

                    btnEdit.Command += btnEdit_Command_Click;


                    //Delete button

                    Button btnDelete = new Button
                    {
                        Text = "Delete",
                        CssClass = "btn btn-danger",
                        CommandArgument = task.Id.ToString(),
                        Width = 100,
                    };

                    btnDelete.Command += btnDelete_Command_Click;

                    cell.Controls.Add(btnEdit);
                    cell.Controls.Add(panel);
                    cell.Controls.Add(btnDelete);



                    newRow.Cells.Add(cell);

                    aTable.Rows.Add(newRow);
                }


            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }

        }

        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }



        protected void btnEdit_Command_Click(object sender, CommandEventArgs e)
        {
            try
            {
                Button btnEdit = (Button)sender;
                int id = Convert.ToInt32(btnEdit.CommandArgument);
                Response.Redirect($"EditTask.aspx?id={id}", false);
            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }

        protected void btnDelete_Command_Click(object sender, CommandEventArgs e)
        {
            try
            {
                Button btnDelete = (Button)sender;
                int id = Convert.ToInt32(btnDelete.CommandArgument);

                foreach (TableRow row in aTable.Rows)
                {
                    if (row.Cells.Count > 0 && row.Cells[0].Text == id.ToString())
                    {
                        aTable.Rows.Remove(row);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }

        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTask.aspx", false);
        }

        public class taskInfo
        {

            public int Id { get; set; }


            public string Task_Name { get; set; }


            public string Description { get; set; }


            public string Category { get; set; }

        }

    }
}