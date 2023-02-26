using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CalorieRestAPI.Controllers;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPIMySQL.Data;
using CalorieRestAPI;

namespace CalorieWebApp.Pages.Calories
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// List of all the calories retrieved from the RESTful API.
        /// </summary>

        public List<CalorieClass> Calories = new();

        /// <summary>
        /// Retrieves the list of all calories from the RESTful API.
        /// </summary>


        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7248");
                var responseTask = await client.GetAsync("Calorie");
               

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsStringAsync();
                    Calories = JsonConvert.DeserializeObject<List<CalorieClass>>(readTask);
                }
            }
        }
    }
}
