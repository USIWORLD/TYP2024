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
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using static Farmacy_App.weeklyreport;

namespace Farmacy_App
{
    public partial class to_do : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await PopulateAnimalsAsync();
                
            }


        }


        private async Task PopulateAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetTodoList");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<ToDoList>>(responseBody);

                foreach (var animal in animals)
                {
                   
                        TableRow newRow = new TableRow();
                        newRow.Cells.Add(CreateCell(animal.TaskDate.ToString("yyyy/MM/dd")));
                        newRow.Cells.Add(CreateCell(animal.Title.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Notes.ToString()));
                        
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

        //protected async void BtnSubmit_Click(object sender, EventArgs e)
        //{

        //    var json = JsonConvert.SerializeObject();
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");


        //    HttpResponseMessage updateresponse = await client.PutAsync($"https://http://localhost:7041/api/Farmacy/UpdateTodoList", content);


        //    if (updateresponse.IsSuccessStatusCode)
        //    {
        //        string responseBody = await updateresponse.Content.ReadAsStringAsync();
        //        var updatedTodo = JsonConvert.DeserializeObject<ToDoList>(responseBody);

        //            err.InnerText = "ToDo List Edited Sucessesfully";


        //    }
        //    else
        //    {

        //        err.InnerText = "Error while editing the list data.";
        //    }
       
        //}

        public class ToDoList
        {
            public int Id { get; set; }

            public string Notes { get; set; }

            public string Title { get; set; }

            public DateTime TaskDate { get; set; }
        }


    }
}