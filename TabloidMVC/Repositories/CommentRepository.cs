﻿using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetAllComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT c.Id AS CommentId, c.Subject, c.Content, c.CreateDateTime,
                              p.Id AS PostId, p.Title,
                              u.Id AS UserId, u.DisplayName
                         FROM Comment c
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                         ORDER BY c.CreateDateTime DECS;";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        Comment comment = new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CommentId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };

                       comment.Post = new Post()
                       {
                           Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                           Title = reader.GetString(reader.GetOrdinal("Title"))
                       };

                        comment.UserProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                        };


                    comments.Add(comment);
                    }
                    
                    reader.Close();

                    return comments;
                }
            }
        }



        public List<Comment> GetCommentsByPostId(int postId)
        { 
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id AS CommentId, c.PostId, c.UserProfileId, c.Subject, c.Content, c.CreateDateTime,
                            p.Id AS PostId, p.Title
                        FROM Comment c
                        LEFT JOIN Post p ON c.PostId = p.id
                        WHERE PostId = @postId
                        ORDER BY c.CreateDateTime DESC
                    ";

                    cmd.Parameters.AddWithValue("@postId", postId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Comment> comments = new List<Comment>();

                    while (reader.Read())
                    {
                        Comment comment = new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                        };

                        comment.Post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Title = reader.GetString(reader.GetOrdinal("Title"))
                        };

                        comment.UserProfile = new UserProfile()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                        };

                        comments.Add(comment);
                    }
                    reader.Close();
                    return comments;
                }
            }
        }








    }
}
