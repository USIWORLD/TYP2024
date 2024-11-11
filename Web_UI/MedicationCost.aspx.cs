using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Farmacy_App.weeklyreport;
using System.Text;

namespace Farmacy_App
{
    public partial class MedicationCost : System.Web.UI.Page
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
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetMedications");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Medication>>(responseBody);

                HttpResponseMessage responseC = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAllocatedMeds");
                responseC.EnsureSuccessStatusCode();

                string responseBodyC = await responseC.Content.ReadAsStringAsync();
                var med = JsonConvert.DeserializeObject<List<MedAllo>>(responseBodyC);

                foreach( var item in animals)
                {
                    item.Allocation = med.Where(alloc => alloc.Med_Id == item.Id).ToList();
                }

                var labels = new List<string>();
                var totalSizeData = new List<int>();
                var quantityUsedData = new List<int>();

                StringBuilder build = new StringBuilder();

                foreach (var animal in animals)
                {
                    var animalIds = (animal.Allocation != null && animal.Allocation.Any())
                             ? string.Join(" ", animal.Allocation.Select(a => a.AnimalId))
                             : "No Allocation";

                    var arrayAnimal = animalIds.Split(' ');


                    if (animal.Allocation == null || !animal.Allocation.Any())
                    {
                        animal.var = 0;
                        animal.EndCost = animal.Cost;
                        animal.var = 0;
                        animal.QuantityEdit = 0;


                        if (animal.Quantity == 0)
                        {
                          
                            animal.EndCost = 0;
                            animal.var = animal.Cost;

                        }

                    }
                    else
                    {
                        if (arrayAnimal.Count() == 1)
                        {
                            animal.var = 0;
                            animal.QuantityEdit = animal.Quantity;
                            animal.EndCost = animal.Cost;

                        }
                        else if (arrayAnimal.Count() == 2 )
                        {
                            animal.QuantityEdit = 2;
                            animal.EndCost = animal.Cost * animal.QuantityEdit;
                            animal.var = animal.Cost - animal.EndCost;
                               

                        }
                        else
                        {
                            animal.var = 0;
                            animal.EndCost = animal.Cost;

                        }

                    }
                    labels.Add(animal.Name);
                    totalSizeData.Add(animal.Quantity);
                    quantityUsedData.Add(animal.QuantityEdit);

                    // Create chart data object
                    

                if (animal.QuantityEdit == 0)
                    {
                        build.AppendLine($"<span style='color: red;'>{animal.Name},</span>");

                    }

                    if (build.Length > 0)
                    {
                        recommend.Text = "*The following medications: " + build.ToString() + " are not being used. We recommend reducing their quantity";

                    }



                    //ASP Table
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Dosage.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString()));
                    newRow.Cells.Add(CreateCell(animalIds.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Quantity.ToString()));

                    newRow.Cells.Add(CreateCell(animal.QuantityEdit.ToString()));
                    newRow.Cells.Add(CreateCell(animal.EndCost.ToString()));
                    newRow.Cells.Add(CreateCell(animal.var.ToString()));

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    if (animal.var == 0 || animal.EndCost == 0)
                    {
                        varience.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        varience.ForeColor = System.Drawing.Color.Red;
                    }

                  

  
                    aTable.Rows.Add(newRow);
                }

                int totalIntedned = animals.Sum(animal => animal.Cost);
                decimal totalcost = animals.Sum(animal => animal.EndCost);
                int Over = (int)(totalIntedned - totalcost);


                total.Text = $"R{totalIntedned}";
                cost.Text = $"R{totalcost}";
                over.Text = $"R{Over}";

                var chartData = new
                {
                    labels = labels,
                    totalSize = totalSizeData,
                    used = quantityUsedData
                };

                // Serialize to JSON and pass to client-side
                string jsonChartData = JsonConvert.SerializeObject(chartData);
                ClientScript.RegisterStartupScript(this.GetType(), "chartData", $"var chartData = {jsonChartData};", true);

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

    
             
        public class Medication
        {
           
            public int Id { get; set; }

          
            public string Name { get; set; }

            public string Dosage { get; set; }



            public string Frequency { get; set; }

           
            public string Category { get; set; }

            public int Cost { get; set; }

            public int EndCost { get; set; }

            public int QuantityEdit { get; set; }

            public int Quantity { get; set; }


            public string Type { get; set; }

            public int var { get; set; }



            public string AnimalId { get; set; }

            public List<MedAllo> Allocation { get; set;}


        }

        public class MedAllo
        {
            public int Id { get; set; }

            public int Med_Id { get; set; }

         
            public string User_Id { get; set; }
            [Required]
            public string Status { get; set; }

            [Required]
            public DateTime Date { get; set; }


            public string AnimalId { get; set; }

        }


        }
    }