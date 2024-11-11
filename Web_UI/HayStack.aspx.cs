using Newtonsoft.Json;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    namespace Farmacy_App
{
    public partial class HayStack : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string address = "http://localhost:7041/api/Farmacy/GetAnimals";
                await PopulateAnimalsAsync(address);
            }
        }


        private async Task PopulateAnimalsAsync(string end)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(end);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);


                StringBuilder build = new StringBuilder();
                StringBuilder buildTwo = new StringBuilder();


                foreach (var animal in animals)
                {
                    if (animal.New_Weight == 0)
                    {
                        animal.New_Weight = animal.A_Weight;
                    }

                    animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;

                    if (animal.Actual_Cost == 0)
                    {
                        animal.Actual_Cost = animal.Cost;

                    }
                    else
                    {
                        animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;
                    }

                    if (animal.UpdateHayNeeded > 5)
                    {
                        build.AppendLine($"<span style='color: red;'>{animal.Name}</span>");


                    }

                    if (animal.UpdateHayNeeded < 0)
                    {
                        buildTwo.AppendLine($"<span style='color: red;'>{animal.Name}</span>");

                    }

                    if (build.Length > 0)
                    {
                        recommend.Text = "*The following Cow's: " + build.ToString() + " have exceeded their allowance. We recommend moving these animals to a separate kraal,to monitor their eating habits  .";
                        recommend2.Text = "*The following Cow's: " + buildTwo.ToString() + " have not been fully consuming their allocated haystack. We recommend doing a health check on them.";

                    }




                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                    newRow.Cells.Add(CreateCell(animal.New_Weight.ToString("0")));
                    newRow.Cells.Add(CreateCell(animal.HayNeeded.ToString()));
                    newRow.Cells.Add(CreateCell(animal.UpdateHayNeeded.ToString("0.0")));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.Actual_Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.varience.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));

                    newRow.Attributes["data-health-status"] = animal.HealthStatus;
                    newRow.Attributes["data-gender"] = animal.Gender;
                    newRow.Attributes["data-breed"] = animal.Breed;
                    newRow.Attributes["data-kraal"] = animal.Kraal.ToString();

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    if (animal.varience < 0)
                    {
                        varience.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        varience.ForeColor = System.Drawing.Color.Green;
                    }

                    newRow.Cells.Add(varience);

                    aTable.Rows.Add(newRow);
                }

                decimal totalHayNeeded = animals.Sum(animal => animal.UpdateHayNeeded + animal.HayNeeded);
                decimal totalcost = animals.Sum(animal => animal.Actual_Cost);

                // Set the total hay needed in a label
                total.Text = $"{totalHayNeeded:F2} kg";
                cost.Text = $"R{totalcost:F2}";

            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
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

        protected async void btnShowAll_Click(object sender, EventArgs e)
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);


                StringBuilder build = new StringBuilder();
                StringBuilder buildTwo = new StringBuilder();


                foreach (var animal in animals)
                {
                    if (animal.New_Weight == 0)
                    {
                        animal.New_Weight = animal.A_Weight;
                    }

                    animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;

                    if (animal.Actual_Cost == 0)
                    {
                        animal.Actual_Cost = animal.Cost;

                    }
                    else
                    {
                        animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;
                    }

                    if (animal.UpdateHayNeeded > 5)
                    {
                        build.AppendLine($"<span style='color: red;'>{animal.Name}</span>");


                    }

                    if (animal.UpdateHayNeeded < 0)
                    {
                        buildTwo.AppendLine($"<span style='color: red;'>{animal.Name}</span>");

                    }

                    if (build.Length > 0)
                    {
                        recommend.Text = "*The following Cow's: " + build.ToString() + " have exceeded their allowance. We recommend moving these animals to a separate kraal.";
                        recommend2.Text = "*The following Cow's: " + buildTwo.ToString() + " have not been fully consuming their allocated haystack. We recommend doing a health check on them.";

                    }




                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                    newRow.Cells.Add(CreateCell(animal.New_Weight.ToString("0")));
                    newRow.Cells.Add(CreateCell(animal.HayNeeded.ToString()));
                    newRow.Cells.Add(CreateCell(animal.UpdateHayNeeded.ToString("0.0")));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.Actual_Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.varience.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));

                    newRow.Attributes["data-health-status"] = animal.HealthStatus;
                    newRow.Attributes["data-gender"] = animal.Gender;
                    newRow.Attributes["data-breed"] = animal.Breed;
                    newRow.Attributes["data-kraal"] = animal.Kraal.ToString();

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    if (animal.varience < 0)
                    {
                        varience.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        varience.ForeColor = System.Drawing.Color.Green;
                    }

                    newRow.Cells.Add(varience);

                    aTable.Rows.Add(newRow);
                }

                decimal totalHayNeeded = animals.Sum(animal => animal.UpdateHayNeeded + animal.HayNeeded);
                decimal totalcost = animals.Sum(animal => animal.Actual_Cost);

                // Set the total hay needed in a label
                total.Text = $"{totalHayNeeded:F2} kg";
                cost.Text = $"R{totalcost:F2}";

            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }

        public class Animal
        {
            public int Id { get; set; }

            public string Name { get; set; }

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
            public string AppetiteStatus { get; set; }
            public decimal New_Weight { get; set; }
            public decimal HayNeeded => A_Weight * 0.3M;
            public decimal UpdateHayNeeded => New_Weight * 0.3M - HayNeeded;
            public decimal Cost => HayNeeded * 10.0M;

            public decimal Actual_Cost;
            public decimal varience => Cost - Actual_Cost;

        }

        protected async void ddlHealthStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlHealthStatus.SelectedValue;

            switch (selectedValue)
            {
                case "Healthy":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetHealthyAnimals");
                    break;
                case "Exposed":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetExposedAnimals");
                    break;
                case "HighRisk":
                    await PopulateAnimalsAsync("http://localhost:7041/api/Farmacy/GetHighRiskAnimals");
                    break;
            }
        }
    }
}