using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Farmacy_App
{
    public partial class Sanitation : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        private static List<Kraal> kraals = new List<Kraal>(); // Store fetched kraals here

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await PopulateKraalsAsync(); // Fetch kraal data on first page load
            }
        }

        private async Task PopulateKraalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetKraals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                kraals = JsonConvert.DeserializeObject<List<Kraal>>(responseBody); // Store the fetched kraals

                // Prepare data for chart
                List<string> kraalLabels = new List<string>(); // Kraal names
                List<decimal> totalSizeData = new List<decimal>(); // Kraal size
                List<int> sanitizedData = new List<int>(); // Size sanitized
                List<int> unsanitizedData = new List<int>(); // Size unsanitized
                List<List<string>> animalsPerKraal = new List<List<string>>(); // Store animals per kraal


                StringBuilder build = new StringBuilder(); // To capture kraals that aren't fully sanitized
                StringBuilder build2 = new StringBuilder(); // To capture kraals that aren't fully sanitized


                foreach (var kraal in kraals)
                {
                    if (kraal.Sanitizer == 0)
                    {
                        build.AppendLine($"<span style='color: red;'>{kraal.Name},</span>");
                    }

                    if (kraal.SizeSanitized == 0 && kraal.Sanitizer != 0)
                    {
                        build2.AppendLine($"<span style='color: red;'>{kraal.Name},</span>");
                    }

                    // Add kraal data to respective lists for chart
                    kraalLabels.Add(kraal.Name);
                    totalSizeData.Add(kraal.Size);
                    sanitizedData.Add(kraal.SizeSanitized);
                    unsanitizedData.Add(kraal.SizeUnsanitized);

                    HttpResponseMessage animalResponse = await client.GetAsync($"http://localhost:7041/api/Farmacy/GetAnimals");
                    animalResponse.EnsureSuccessStatusCode();

                    string animalResponseBody = await animalResponse.Content.ReadAsStringAsync();
                    List<Animal> allAnimals = JsonConvert.DeserializeObject<List<Animal>>(animalResponseBody);

                    // Filter animals by kraal ID
                    List<Animal> kraalAnimals = allAnimals.Where(a => a.Kraal == kraal.Id).ToList();

                    List<string> animalNames = kraalAnimals.Select(a => a.Name).ToList();
                    animalsPerKraal.Add(animalNames);



                    // Create a new table row for each kraal
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(kraal.Name));  // Kraal name
                    newRow.Cells.Add(CreateCell(kraal.Size.ToString())); // Kraal size
                    newRow.Cells.Add(CreateCell(kraal.Sanitizer.ToString())); // Sanitizer allocated
                    newRow.Cells.Add(CreateCell(kraal.SizeSanitized.ToString())); // Size sanitized
                    newRow.Cells.Add(CreateCell(kraal.SizeUnsanitized.ToString())); // Size unsanitized

                    aTable.Rows.Add(newRow); // Add row to table
                }

                if (build.Length > 0)
                {
                    recommend.Text = "*The following kraals: " + build.ToString() +
                                     " have not been sanitized. We recommend " +
                                     "<a href='KraalSanitizer.aspx'>allocating a sanitizer</a>.";
                }


                if (build2.Length > 0)
                {
                    recommend.Text = "*The following kraals: " + build2.ToString() + " has not been sanitized. We recommend allocating a task to sanitize it.";
                }

                // Set up the chart data
                var chartData = new
                {
                    labels = kraalLabels,
                    totalSize = totalSizeData,
                    sanitized = sanitizedData,
                    unsanitized = unsanitizedData,
                    animals = animalsPerKraal
                };

                string jsonChartData = JsonConvert.SerializeObject(chartData);
                ClientScript.RegisterStartupScript(this.GetType(), "chartData", $"var chartData = {jsonChartData};", true);
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

        // Helper method to create a new table cell
        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }

        // Kraal class to reflect Kraal data structure

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
        }

        public class Kraal
        {
            public int Id { get; set; }
            public string Name { get; set; } // Kraal name
            public int Size { get; set; } // Kraal size
            public int Sanitizer { get; set; } // Sanitizer allocated
            public int SizeSanitized { get; set; } // Area sanitized
            public int SizeUnsanitized { get; set; } // Area unsanitized
        }
    }
}
