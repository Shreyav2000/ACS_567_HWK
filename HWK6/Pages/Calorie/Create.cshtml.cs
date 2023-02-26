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
using System.Text.Json;

namespace CalorieWebApp.Pages.Calories;
/// <summary>
/// Class to provide create functionality to the Create Item Page.
/// </summary>
public class CreateModel : PageModel
{
    public CalorieClass Calorie = new();
    public string errorMessage = "";
    public string successMessage = "";
    /// <summary>
    /// Performs HTTP Post to create a new calorie item.
    /// </summary>
    public async void OnPost()
    {
        Calorie.Name = Request.Form["Name"];
        Calorie.Quantity = int.Parse(Request.Form["Quantity"]);
        Calorie.Unit = Request.Form["Unit"];
        Calorie.Calories = int.Parse(Request.Form["Calories"]);

        if (Calorie.Name.Length == 0 || Calorie.Quantity == 0 || Calorie.Unit.Length == 0 || Calorie.Calories == 0)
            errorMessage = "Please enter all details";
        else
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize<CalorieClass>(Calorie, opt);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7248");

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var result = await client.PostAsync("Calorie", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                Console.WriteLine(resultContent);

                if (!result.IsSuccessStatusCode)
                    errorMessage = "Error adding";
                else
                    successMessage = "Successfully added";
            }
        }
    }
}
