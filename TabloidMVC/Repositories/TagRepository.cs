//Purpose: To give data to the controller, and to update data 

using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;


namespace TabloidMVC.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration config) : base(config) { }

        public List<Tag> GetAllTags()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT t.Id, t.Name AS TagTitle
                    FROM Tag t
                    ";

                    var reader = cmd.ExecuteReader();

                    var tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(NewTagFromReader(reader));
                    }

                    reader.Close();

                    return tags;

                }
            }
        }

        public Tag GetTagById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name]
                    FROM Tag
                    WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        reader.Close();
                        return tag;
                    }
                    reader.Close();
                    return null;
                }
            }
        }


        private Tag NewTagFromReader(SqlDataReader reader)
        {
            return new Tag()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("TagTitle"))
            };
        }

        public void Edit(Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Tag
                    SET [Name] = @name
                    WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@name", tag.Name);
                    cmd.Parameters.AddWithValue("@id", tag.Id);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Add(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Tag  ( Name )
                    OUTPUT INSERTED.ID
                    VALUES ( @Name )
                    ";

                    cmd.Parameters.AddWithValue("@Name", tag.Name);

                    tag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
                


        public void Delete(int tagId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    DELETE FROM Tag
                    WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@id", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
