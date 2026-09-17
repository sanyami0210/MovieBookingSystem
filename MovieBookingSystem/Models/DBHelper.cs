using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieBookingSystem.Models
{
    public class DBHelper
    {
        private readonly string connectionString;

        public DBHelper()
        {
            connectionString =
                ConfigurationManager
                .ConnectionStrings["conn"]
                .ConnectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // =========================
        // CATEGORY
        // =========================

        public bool AddCategory(CategoryModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_AddCategory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_Type",
                    model.Cat_Type);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> list =
                new List<SelectListItem>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_GetCategories", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SelectListItem
                        {
                            Value =
                                reader["Cat_ID"].ToString(),

                            Text =
                                reader["Cat_Type"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // =========================
        // MOVIE
        // =========================

        public bool AddMovie(MovieModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_AddMovie", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Movie_name",
                    model.Movie_name);

                cmd.Parameters.AddWithValue(
                    "@Release_Date",
                    model.Release_Date);

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    model.Cat_ID);

                cmd.Parameters.AddWithValue(
                    "@rate",
                    model.rate);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<MovieModel> GetMovies()
        {
            List<MovieModel> movies =
                new List<MovieModel>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_GetMovies", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new MovieModel
                        {
                            Movie_ID =
                                Convert.ToInt32(
                                    reader["Movie_ID"]),

                            Movie_name =
                                reader["Movie_name"].ToString(),

                            Release_Date =
                                Convert.ToDateTime(
                                    reader["Release_Date"]),

                            Cat_ID =
                                Convert.ToInt32(
                                    reader["Cat_ID"]),

                            Cat_Type =
                                reader["Cat_Type"].ToString(),

                            rate =
                                Convert.ToDecimal(
                                    reader["rate"])
                        });
                    }
                }
            }

            return movies;
        }

        public List<MovieModel> GetMoviesByCategory(int catId)
        {
            List<MovieModel> movies =
                new List<MovieModel>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetMoviesByCategory",
                       con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    catId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new MovieModel
                        {
                            Movie_ID =
                                Convert.ToInt32(
                                    reader["Movie_ID"]),

                            Movie_name =
                                reader["Movie_name"].ToString(),

                            Release_Date =
                                Convert.ToDateTime(
                                    reader["Release_Date"]),

                            Cat_ID =
                                Convert.ToInt32(
                                    reader["Cat_ID"]),

                            rate =
                                Convert.ToDecimal(
                                    reader["rate"])
                        });
                    }
                }
            }

            return movies;
        }

        public List<SelectListItem> GetMoviesForCategory(
            int catId)
        {
            List<SelectListItem> list =
                new List<SelectListItem>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetMoviesForCategory",
                       con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    catId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SelectListItem
                        {
                            Value =
                                reader["Movie_ID"].ToString(),

                            Text =
                                reader["Movie_name"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        // =========================
        // BOOKING
        // =========================

        public bool AddBooking(BookingModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_AddBooking",
                       con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    model.User_ID);

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    model.Cat_ID);

                cmd.Parameters.AddWithValue(
                    "@Movie_ID",
                    model.Movie_ID);

                cmd.Parameters.AddWithValue(
                    "@no_of_Tickets",
                    model.no_of_Tickets);

                con.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // =========================
        // LOGIN
        // =========================

        public UserModel Login(LoginModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_LoginUser",
                       con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@User_Name",
                    model.User_Name);

                cmd.Parameters.AddWithValue(
                    "@User_password",
                    model.User_password);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserModel
                        {
                            User_ID =
                                Convert.ToInt32(
                                    reader["User_ID"]),

                            User_Name =
                                reader["User_Name"].ToString(),

                            Email_ID =
                                reader["Email_ID"].ToString(),

                            City =
                                reader["City"].ToString(),

                            PhoneNo =
                                reader["PhoneNo"].ToString()
                        };
                    }
                }
            }

            return null;
        }
    }
}