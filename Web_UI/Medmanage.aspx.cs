using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacy_App
{
    public partial class Medmanage : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                await PopulateAnimalsAsync();
                first = false;
            }


        }


        private async Task PopulateAnimalsAsync()
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetMedications");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Medication>>(responseBody);

                foreach (var animal in animals)
                {
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Dosage.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Frequency.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Category.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Quantity.ToString()));

                    newRow.Cells.Add(CreateCell(animal.Type.ToString()));
                    newRow.Cells.Add(CreateCell("R" + animal.Cost.ToString()));

                    Button btnEdit = new Button
                    {
                        Text = "Edit",
                        CssClass = "btn btn-primary",
                        CommandArgument = animal.Id.ToString(),
                        Width = 100,

                    };


                    btnEdit.Click += btnEdit_Command_Click;

                    TableCell cell = new TableCell();


                    cell.Controls.Add(btnEdit);


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

        protected void btnEdit_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnEdit = (Button)sender;
                int id = Convert.ToInt32(btnEdit.CommandArgument);
                Response.Redirect($"EditMedication.aspx?id={id}", false);


            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }


        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }

        public class Medication
        {
            public int Id { get; set; }


            public string Name { get; set; }


            public string Dosage { get; set; }



            public string Frequency { get; set; }


            public string Category { get; set; }

            public string Type { get; set; }

            public decimal Cost { get; set; }

            public int Quantity { get; set; }


        }

        protected void btnAddAnimal_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddMedication.aspx", false);
        }
    }
}