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
    public partial class VetCost : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (first)
                {
                    await PopulateAnimalsAsync();
                    first = false;
                }
            }
        }


        private async Task PopulateAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetVisits");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<VetVisits>>(responseBody);

                foreach( VetVisits animal in animals )
                {
                   
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.VisitDate.ToString("yyyy/MM/dd")));
                    newRow.Cells.Add(CreateCell(animal.AnimalID.ToString()));
                    newRow.Cells.Add(CreateCell(animal.AnimalID.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString()));
                    

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    //if (animal.Varience < 0)
                    //{
                    //    varience.ForeColor = System.Drawing.Color.Red;
                    //}
                    //else
                    //{
                    //    varience.ForeColor = System.Drawing.Color.Green;
                    //}

                    newRow.Cells.Add(varience);
                    aTable.Rows.Add(newRow);
                }

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


        public class VetVisits
         {
            public VetVisits() { }

            [Key]
            public int Visitkey { get; set; }

            [Required]
            public DateTime VisitDate { get; set; }

            [Required]
            public int AnimalID { get; set; }

            [Required]
            public string Availability { get; set; }

            public decimal Cost { get; set; }

            public string purpose { get; set; }

          }

    }
}