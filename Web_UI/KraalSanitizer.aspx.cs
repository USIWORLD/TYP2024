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
using static Farmacy_App.AllocateTask3;

namespace Farmacy_App
{
    public partial class KraalSanitizer : System.Web.UI.Page
    {

        private static readonly HttpClient client = new HttpClient();
        private static List<Kraal> kraals = new List<Kraal>(); // Store fetched kraals here

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await PopulateTasksAsync();
                BindKraalDropdown();
            }
        }

        private async Task PopulateTasksAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetKraals");
                response.EnsureSuccessStatusCode();


                string responseBody = await response.Content.ReadAsStringAsync();
                kraals = JsonConvert.DeserializeObject<List<Kraal>>(responseBody); // Store the fetched kraals

                BindKraalGridView();

            }
            catch (Exception ex)
            {
                allocationResult.Text = $"An error occurred: {ex.Message}";
            }
        }


        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };

        }
    
        protected void KraalDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //wwweerreeww
        }


        private void BindKraalDropdown()
        {
            kraalDropdown.DataSource = kraals;
            kraalDropdown.DataTextField = "Name";
            kraalDropdown.DataValueField = "Id"; // Use ID as the value
            kraalDropdown.DataBind();
        }

        private void BindKraalGridView()
        {
            foreach (var kraal in kraals)
            {
                TableRow newRow = new TableRow();
                newRow.Cells.Add(CreateCell(kraal.Name));
                newRow.Cells.Add(CreateCell(kraal.Size.ToString()));
                newRow.Cells.Add(CreateCell(kraal.Sanitizer.ToString()));
                newRow.Cells.Add(CreateCell(kraal.SizeSanitized.ToString()));
                newRow.Cells.Add(CreateCell(kraal.SizeUnsanitized.ToString()));

                aTable.Rows.Add(newRow);
            }
        }

        protected async void AllocateSanitizerButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected kraal ID and sanitizer amount
                int selectedKraalId = int.Parse(kraalDropdown.SelectedValue);
                int amountToAllocate = int.Parse(sanitizerAmount.Text);

                // Find the selected kraal
                var selectedKraal = kraals.FirstOrDefault(k => k.Id == selectedKraalId);

                if (selectedKraal != null)
                {
                    // Log the current state of the selected Kraal for debugging
                    Console.WriteLine($"Selected Kraal: {selectedKraal.Name}");
                    Console.WriteLine($"Current Sanitizer: {selectedKraal.Sanitizer}, Amount to Allocate: {amountToAllocate}");

                    // Check if sanitizer is 0, allow allocation regardless of SizeSanitized and SizeUnsanitized
                    if (selectedKraal.Sanitizer == 0 || selectedKraal.SizeUnsanitized >= amountToAllocate)
                    {
                        // Allocate the sanitizer
                        selectedKraal.Sanitizer += amountToAllocate;

                        // Create the updated Kraal object
                        var updatedKraal = new
                        {
                            Id = selectedKraal.Id,
                            Name = selectedKraal.Name,
                            Size = selectedKraal.Size,
                            Sanitizer = selectedKraal.Sanitizer,
                            SizeSanitized = selectedKraal.SizeSanitized,
                            SizeUnsanitized = selectedKraal.SizeUnsanitized
                        };

                        // Serialize to JSON
                        var json = JsonConvert.SerializeObject(updatedKraal);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        // Make an API call to update the Kraal in the database
                        HttpResponseMessage response = await client.PutAsync($"http://localhost:7041/api/Farmacy/EditKraal/{selectedKraal.Id}", content);
                        response.EnsureSuccessStatusCode();

                        if(response.IsSuccessStatusCode)
                        {
                            Response.Redirect("Sanitation.aspx");
                        }

                        // Update result message and refresh the grid view
                        BindKraalGridView();
                    }
                    else
                    {
                        // Not enough unsanitized size available
                        allocationResult.Text = "Not enough unsanitized size available.";
                    }
                }
                else
                {
                    // Kraal not found
                    allocationResult.Text = "Kraal not found.";
                }
            }
            catch (Exception ex)
            {
                allocationResult.Text = $"An error occurred: {ex.Message}";
            }
        }


        public class Kraal
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Size { get; set; }
            public int Sanitizer { get; set; }
            public int SizeSanitized { get; set; }
            public int SizeUnsanitized { get; set; }
        }
    }
}