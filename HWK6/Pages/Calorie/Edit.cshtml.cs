using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CalorieRestAPI.Controllers;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Data;
using CalorieRestAPI;

namespace CalorieWebApp.Pages.Calories
{
    /// <summary>
    /// Page model class for the Edit page
    /// </summary>
    public class EditModel : PageModel
    {
        public CalorieClass Calorie = new();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public async Task OnGetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7248");
                var responseTask = await client.GetAsync("Calorie/" +id);

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsStringAsync();
                    Calorie = JsonConvert.DeserializeObject<CalorieClass>(readTask);
                }
            }
        }
        /// <summary>
        /// HTTP Post method to edit the calorie with the specified id
        /// </summary>
        /// <param name="id">The id of the calorie to edit</param>
        public async Task OnPostAsync()
        {
            Calorie.Id = int.Parse(Request.Form["Id"]);
            Calorie.Name = Request.Form["Name"];
            Calorie.Quantity = int.Parse(Request.Form["Quantity"]);
            Calorie.Unit = Request.Form["Unit"];
            Calorie.Calories = int.Parse(Request.Form["Calories"]);

            if (Calorie.Name.Length == 0 || Calorie.Quantity == 0 || Calorie.Unit.Length == 0 || Calorie.Calories == 0)
                ErrorMessage = "Please enter all details";
            else
            {
                var json = JsonConvert.SerializeObject(Calorie);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7248");

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var result = await client.PutAsync("Calorie/"+ Calorie.Id, content);
                    string resultContent = await result.Content.ReadAsStringAsync();

                    if (!result.IsSuccessStatusCode)
                        ErrorMessage = "Error updating calorie";
                    else
                        SuccessMessage = "Successfully updated calorie";
                }
            }
        }
    }
}

