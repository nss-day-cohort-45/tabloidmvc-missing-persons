using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config) { }
        public List<Category> GetAllCategories() //method we difined in ICategory interface
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Category"; //selecting these values from our Category database
                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>(); //assigning a new list type category object to varaiable 'categories'

                    while (reader.Read())
                    {
                        categories.Add(new Category() //adding a new Category object here
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return categories;
                }
            }
        }

        //geting the list of categories by their id
        public Category GetCategoryById(int id) 
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name]
                        FROM Category
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Category category = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        reader.Close();
                        return category;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        // this method is for adding a new category object
        public void AddCategory(Category category)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //this is what the user enters in name section in the form
                    cmd.CommandText = @"
                    INSERT INTO Category ([Name])
                    OUTPUT INSERTED.ID
                    VALUES (@name);
                ";

                    cmd.Parameters.AddWithValue("@name", category.Name); //and we will assign what the user entered in the name section as a new value for our new name

                    int id = (int)cmd.ExecuteScalar();

                    category.Id = id;
                }
            }
        }

        //this method is to updat/edit our category
        public void UpdateCategory(Category category)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //user can edit/update the name of the category
                    cmd.CommandText = @"
                            UPDATE Category
                            SET 
                                [Name] = @name
                            WHERE Id = @id";

                    //and we will set whatever the user entered for name as the new value for the name of the object the user selected 
                    cmd.Parameters.AddWithValue("@name", category.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Category
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", categoryId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
