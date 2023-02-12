using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPI;
using HWK4.Models;

namespace CalorieRestAPIMySQL.Interfaces
{
    /// <summary>
    /// The interface for the Calorie Repository class.
    /// </summary>
    public interface ICalorieRepository
    {
        // <summary>
        /// Get all Calorie data stored in the database.
        /// </summary>
        /// <returns>A collection of Calorie objects.</returns>
        ICollection<Calorie> GetCalories();

        /// <summary>
        /// Get a specific Calorie data by its ID.
        /// </summary>
        /// <param name="id">The ID of the Calorie data to retrieve.</param>
        /// <returns>The Calorie data with the specified ID.</returns>
        Calorie GetCalorie(int id);

        /// <summary>
        /// Check if a Calorie data with the specified ID exists in the database.
        /// </summary>
        /// <param name="id">The ID of the Calorie data to check for existence.</param>
        /// <returns>True if the Calorie data exists, False otherwise.</returns>
        bool CalorieExists(int id);

        /// <summary>
        /// Create a new Calorie data in the database.
        /// </summary>
        /// <param name="Calorie">The Calorie data to create.</param>
        /// <returns>True if the Calorie data was successfully created, False otherwise.</returns>
        bool CreateCalorie(Calorie Calorie);

        /// <summary>
        /// Update an existing Calorie data in the database.
        /// </summary>
        /// <param name="Calorie">The updated Calorie data.</param>
        /// <returns>True if the Calorie data was successfully updated, False otherwise.</returns>
        bool UpdateCalorie(Calorie Calorie);

        /// <summary>
        /// Delete an existing Calorie data from the database.
        /// </summary>
        /// <param name="Calorie">The Calorie data to delete.</param>
        /// <returns>True if the Calorie data was successfully deleted, False otherwise.</returns>
        bool DeleteCalorie(Calorie Calorie);

        /// <summary>
        /// Save changes made to the Calorie data in the database.
        /// </summary>
        /// <returns>True if the changes were successfully saved, False otherwise.</returns>
        bool Save();

        /// <summary>
        /// Get an analysis of all the Calorie data stored in the database.
        /// </summary>
        /// <returns>An object containing the analysis of the Calorie data and its data.</returns>
        CalorieAnalysis GetCalorieAnalysis();
    }
}
