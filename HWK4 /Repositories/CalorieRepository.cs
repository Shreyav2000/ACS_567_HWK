using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CalorieRestAPI;
using HWK4.Models;

namespace CalorieRestAPIMySQL.Repositories
{
    /// <summary>
    /// Repository class that implements the ICalorieRepository Interface
    /// </summary>
    public class CalorieRepository : ICalorieRepository
    {

        private DataContext _context;

        /// <summary>
        /// Constructor for CalorieRepository
        /// </summary>
        /// <param name="context"> Data Context for the repository</param>

        public CalorieRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a Collection of all Calorie objects
        /// </summary>
        /// <param name="id"> The id of the Calorie object to retrieve </param>
        /// <returns>The Calorie object with the specified id </returns>
        public ICollection<Calorie> GetCalories()
        {
            return _context.Calorie.ToList();
        }
        public Calorie GetCalorie(int id)
        {
            return _context.Calorie.Where(c => c.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Determines if a calorie object with a Specified id exists
        /// </summary>
        /// <param name="id"> The id of the Calorie object to check for </param>
        /// <returns>True if the calorie object exists, false otherwise</returns>
        public bool CalorieExists(int id)
        {
            return _context.Calorie.Any(c => c.Id == id);
        }
        /// <summary>
        /// Creates a bew calorie object
        /// </summary>
        /// <param name="Calorie"> The calorie object to create</param>
        /// <returns> True if the calorie object was created, false otherwise</returns>
        public bool CreateCalorie(Calorie Calorie)
        {
            _context.Calorie.Add(Calorie);
            return Save();
        }

        /// <summary>
        /// Updates an existing Calorie Object
        /// </summary>
        /// <param name="Calorie"> The calorie object to update</param>
        /// <returns> True if the calorie object was updated, false otherwise</returns>
        public bool UpdateCalorie(Calorie Calorie)
        {
            _context.Calorie.Update(Calorie);
            return Save();
        }

        /// <summary>
        /// Delete a calorie object
        /// </summary>
        /// <param name="Calorie"> The calorie object to delete </param>
        /// <returns>True if the calorie object was deleted, false otherwise</returns>
        public bool DeleteCalorie(Calorie Calorie)
        {
            _context.Calorie.Remove(Calorie);
            return Save();
        }

        /// <summary>
        /// Saves any changes made to the database
        /// </summary>
        /// <returns> true if the changes were saved, false otherwise</returns>
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        /// <summary>
        /// GetCalorieAnalysis calculates and returns the total, average , mamximum and minimum calories
        /// of all the calories tsored in the database
        /// </summary>
        /// <returns> Returns a instance of CalorieAnalysis, which contains the analysis string and a dictionary
        /// that maps the name of each analysis to its corresponding value</returns>

        public CalorieAnalysis GetCalorieAnalysis()
        {
            //retrieve all calories from the database
            var calories = GetCalories();

            //calculate the total, average, minimum and maximum calories`
            var totalCalories = calories.Sum(c => c.Calories);
            var averageCalories = (int)calories.Average(c => c.Calories);
            var minCalories = calories.Min(c => c.Calories);
            var maxCalories = calories.Max(c => c.Calories);
            var analysisData = new Dictionary<string, int>
        {
            { "Total calories", totalCalories },
            { "Average calories", averageCalories },
             { "Min calories", minCalories }, 
               { "Max calories", maxCalories }
        };
            var analysis = $"Total calories: {totalCalories}. Average calories: {averageCalories}. Minimum Calories: {minCalories}. Maximum Calories: {maxCalories}";
            return new CalorieAnalysis { Analysis = analysis, AnalysisData = analysisData };
        }


    }



}

    





