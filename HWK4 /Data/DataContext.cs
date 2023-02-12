using CalorieRestAPIMySQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CalorieRestAPIMySQL.Data
{
/// <summary>
/// DataContext class is a DbContent implementation for CalorieRestAPIMySQL application.
/// It is used for database operations related to Calorie entity.
/// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options used for setting up the context</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the  calorie entity
        /// </summary>
        public DbSet<Calorie> Calorie { get; set; }
    }
}