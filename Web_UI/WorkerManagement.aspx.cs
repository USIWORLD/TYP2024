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

namespace Farmacy_App
{
    public partial class WorkerManagement : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (first)
            {
                await PopulateWorkerAsync();
                first = false;
            }
        }

        private async Task PopulateWorkerAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetWorkers");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var workers = JsonConvert.DeserializeObject<List<Worker>>(responseBody);
                Random random = new Random();

                foreach (var worker in workers)
                {

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(worker.Name.ToString()));
                    newRow.Cells.Add(CreateCell(worker.Surname.ToString()));
                    newRow.Cells.Add(CreateCell(worker.Email.ToString()));

                    string availability = random.Next(2) == 0 ? "Free" : "Not Free";
                    newRow.Cells.Add(CreateCell(availability));

                    TableCell cell = new TableCell();

                    Panel panel = new Panel();
                    panel.CssClass = "button-panel";
                    panel.Attributes["style"] = "display:flex; flex-direction:column;gap:15px;";

                    Button btnEdit = new Button
                    {
                        Text = "Edit",
                        CssClass = "btn btn-primary",
                        CommandArgument = worker.Id.ToString(),
                        CommandName = "Edit",
                        Width = 70,

                    };

                    LiteralControl control = new LiteralControl("&nbsp;&nbsp");

                    btnEdit.Command += btnEdit_Command_Click;


                    //Delete button

                    Button btnDelete = new Button
                    {
                        Text = "Delete",
                        CssClass = "btn btn-danger",
                        CommandArgument = worker.Id.ToString(),
                        CommandName = "Delete",

                    };

                    btnDelete.Command += btnDelete_Command_Click;


                    cell.Controls.Add(btnEdit);
                    cell.Controls.Add(control);
                    cell.Controls.Add(btnDelete);

                    cell.Controls.Add(panel);
                    newRow.Cells.Add(cell);

                    aTable.Rows.Add(newRow);
                }


            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
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
                string id = btnEdit.CommandArgument;
                Response.Redirect($"EditWorker.aspx?id={id}", false);
            }
            catch (Exception ex)
            {

                error.InnerText = $"Error: {ex.Message}";

            }
        }

        protected void btnDelete_Command_Click(object sender, CommandEventArgs e)
        {
            try
            {
                Button btnDelete = (Button)sender;
                string id = btnDelete.CommandArgument;

                foreach (TableRow row in aTable.Rows)
                {
                    if (row.Cells.Count > 0 && row.Cells[0].Text == id)
                    {
                        aTable.Rows.Remove(row);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                error.InnerText = $"Error: {ex.Message}";

            }
        }


        protected void btnAddWorker_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddWorker.aspx");
        }

        public class Worker
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Email { get; set; }
        }

    }
}