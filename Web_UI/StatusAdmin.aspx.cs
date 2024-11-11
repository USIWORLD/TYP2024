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
    public partial class medicationAdmin : System.Web.UI.Page
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
                HttpResponseMessage response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                foreach (var animal in animals)
                {
                    if(animal.HealthStatus.Equals("HighRisk") || animal.HealthStatus.Equals("FMD Confirmed"))
                    {
                        TableRow newRow = new TableRow();
                        newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Gender.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Breed.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Colour.ToString()));
                        newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Height.ToString()));
                        newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Age.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Kraal.ToString()));


                        Button btnEdit = new Button
                        {
                            Text = "Examine",
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
                Response.Redirect($"ConductReport.aspx?id={id}", false);


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

        public class Animal
        {
            public int Id { get; set; }


            public string Species { get; set; }


            public string Gender { get; set; }



            public string Breed { get; set; }


            public string Colour { get; set; }


            public decimal A_Weight { get; set; }


            public decimal Height { get; set; }


            public string HealthStatus { get; set; }


            public DateTime DateAcquired { get; set; }

            public int Age { get; set; }

            public int Kraal { get; set; }
        }
    }
}