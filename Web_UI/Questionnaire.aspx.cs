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

namespace Farmacy_App
{
    public partial class Questionnaire : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();

        bool first = true;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                await PopulateQuestionnairesAsync();
                first = false;
            }

        }

        private async Task PopulateQuestionnairesAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetQuestionnaires");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Questionaire>>(responseBody);

                foreach (var animal in animals)
                {

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.DateAcquired.ToString("yyyy/MM/dd")));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_One)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Two)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Three)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Four)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Five)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Six)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Seven)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Eight)));
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Nine)));                
                    newRow.Cells.Add(CreateCell(Change(animal.Q_Ten)));
                    newRow.Cells.Add(CreateCell(animal.U_ID.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cid.ToString())); 


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

        public string Change(int answer)
        {
            if (answer ==  0) 
            {
                return "No";
            }else
            {
                return "Yes";
            }
        }


        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }

        public class Questionaire
        {
            public int Id { get; set; }

            public DateTime DateAcquired { get; set; }


            public int Q_One { get; set; }


            public int Q_Two { get; set; }


            public int Q_Three { get; set; }


            public int Q_Four { get; set; }

            public int Q_Five { get; set; }

            public int Q_Six { get; set; }

            public int Q_Seven { get; set; }

            public int Q_Eight { get; set; }

            public int Q_Nine { get; set; }

            public int Q_Ten { get; set; }

            public string U_ID { get; set; }

            public int Cid { get; set; }
        }

        }
    }