using MovieBookingSystem.Models;
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
        // =========================================================
        // DATABASE CONNECTION
        // =========================================================

        private readonly string connectionString;

        public DBHelper()
        {
            connectionString =
                ConfigurationManager
                .ConnectionStrings["conn"]
                .ConnectionString;
        }

        // Common function for creating database connection
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }


        // =========================================================
        // CATEGORY FUNCTIONS
        // =========================================================

        // ---------------------------------------------------------
        // Add new movie category
        // Stored Procedure: sp_AddCategory
        // ---------------------------------------------------------
        public bool AddCategory(CategoryModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_AddCategory", con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_Type",
                    model.Cat_Type);

                con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ---------------------------------------------------------
        // Get all movie categories
        // Stored Procedure: sp_GetCategories
        // ---------------------------------------------------------
        public List<SelectListItem> GetCategories()
        {
            List<SelectListItem> list =
                new List<SelectListItem>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_GetCategories", con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new SelectListItem
                            {
                                Value =
                                    reader["Cat_ID"]
                                    .ToString(),

                                Text =
                                    reader["Cat_Type"]
                                    .ToString()
                            });
                    }
                }
            }

            return list;
        }


        // =========================================================
        // MOVIE FUNCTIONS
        // =========================================================

        // ---------------------------------------------------------
        // Add new movie
        // Stored Procedure: sp_AddMovie
        // ---------------------------------------------------------
        public bool AddMovie(MovieModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_AddMovie", con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

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

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ---------------------------------------------------------
        // Get all movies
        // Stored Procedure: sp_GetMovies
        // ---------------------------------------------------------
        public List<MovieModel> GetMovies()
        {
            List<MovieModel> movies =
                new List<MovieModel>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand("sp_GetMovies", con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MovieModel movie =
                            new MovieModel();

                        movie.Movie_ID =
                            Convert.ToInt32(
                                reader["Movie_ID"]);

                        movie.Movie_name =
                            reader["Movie_name"]
                            .ToString();

                        movie.Release_Date =
                            Convert.ToDateTime(
                                reader["Release_Date"]);

                        movie.Cat_ID =
                            Convert.ToInt32(
                                reader["Cat_ID"]);

                        // Cat_Type is returned by
                        // sp_GetMovies
                        if (reader["Cat_Type"] != DBNull.Value)
                        {
                            movie.Cat_Type =
                                reader["Cat_Type"]
                                .ToString();
                        }

                        movie.rate =
                            Convert.ToDecimal(
                                reader["rate"]);

                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }


        // ---------------------------------------------------------
        // Get movies according to selected category
        // Stored Procedure: sp_GetMoviesByCategory
        // ---------------------------------------------------------
        public List<MovieModel> GetMoviesByCategory(
            int catId)
        {
            List<MovieModel> movies =
                new List<MovieModel>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetMoviesByCategory",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    catId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MovieModel movie =
                            new MovieModel();

                        movie.Movie_ID =
                            Convert.ToInt32(
                                reader["Movie_ID"]);

                        movie.Movie_name =
                            reader["Movie_name"]
                            .ToString();

                        movie.Release_Date =
                            Convert.ToDateTime(
                                reader["Release_Date"]);

                        movie.Cat_ID =
                            Convert.ToInt32(
                                reader["Cat_ID"]);

                        movie.rate =
                            Convert.ToDecimal(
                                reader["rate"]);

                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }


        // ---------------------------------------------------------
        // Get movies for movie dropdown
        // based on selected category
        //
        // Stored Procedure:
        // sp_GetMoviesForCategory
        // ---------------------------------------------------------
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
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Cat_ID",
                    catId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new SelectListItem
                            {
                                Value =
                                    reader["Movie_ID"]
                                    .ToString(),

                                Text =
                                    reader["Movie_name"]
                                    .ToString()
                            });
                    }
                }
            }

            return list;
        }


        // ---------------------------------------------------------
        // Delete selected movie
        //
        // Stored Procedure:
        // sp_DeleteMovie
        //
        // Expected result:
        //  1  = deleted
        // -1  = movie has bookings
        // ---------------------------------------------------------
        public bool DeleteMovie(int movieId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_DeleteMovie",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Movie_ID",
                    movieId);

                con.Open();

                object result =
                    cmd.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                {
                    return false;
                }

                int resultValue =
                    Convert.ToInt32(result);

                return resultValue == 1;
            }
        }


        // =========================================================
        // BOOKING FUNCTIONS
        // =========================================================

        // ---------------------------------------------------------
        // Add new movie booking
        //
        // Stored Procedure:
        // sp_AddBooking
        //
        // Amount is calculated in the stored procedure:
        //
        // amount = movie rate * number of tickets
        // ---------------------------------------------------------
        public bool AddBooking(BookingModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_AddBooking",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

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

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ---------------------------------------------------------
        // Get all bookings belonging to logged-in user
        //
        // Stored Procedure:
        // sp_GetUserBookings
        // ---------------------------------------------------------
        public List<BookingModel> GetUserBookings(
            int userId)
        {
            List<BookingModel> list =
                new List<BookingModel>();

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetUserBookings",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    userId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookingModel booking =
                            new BookingModel();

                        booking.booking_ID =
                            Convert.ToInt32(
                                reader["booking_ID"]);

                        booking.User_ID =
                            Convert.ToInt32(
                                reader["User_ID"]);

                        booking.Cat_ID =
                            Convert.ToInt32(
                                reader["Cat_ID"]);

                        booking.Movie_ID =
                            Convert.ToInt32(
                                reader["Movie_ID"]);

                        booking.no_of_Tickets =
                            Convert.ToInt32(
                                reader["no_of_Tickets"]);

                        booking.amount =
                            Convert.ToDecimal(
                                reader["amount"]);

                        list.Add(booking);
                    }
                }
            }

            return list;
        }


        // ---------------------------------------------------------
        // Get one booking
        //
        // Important:
        // User_ID is also checked.
        //
        // This prevents one user from editing
        // another user's booking.
        //
        // Stored Procedure:
        // sp_GetBooking
        // ---------------------------------------------------------
        public BookingModel GetBooking(
            int bookingId,
            int userId)
        {
            BookingModel model = null;

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetBooking",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@booking_ID",
                    bookingId);

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    userId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model =
                            new BookingModel();

                        model.booking_ID =
                            Convert.ToInt32(
                                reader["booking_ID"]);

                        model.User_ID =
                            Convert.ToInt32(
                                reader["User_ID"]);

                        model.Cat_ID =
                            Convert.ToInt32(
                                reader["Cat_ID"]);

                        model.Movie_ID =
                            Convert.ToInt32(
                                reader["Movie_ID"]);

                        model.no_of_Tickets =
                            Convert.ToInt32(
                                reader["no_of_Tickets"]);

                        model.amount =
                            Convert.ToDecimal(
                                reader["amount"]);
                    }
                }
            }

            return model;
        }


        // ---------------------------------------------------------
        // Update existing booking
        //
        // Stored Procedure:
        // sp_UpdateBooking
        //
        // Amount is recalculated by the stored procedure.
        // ---------------------------------------------------------
        public bool UpdateBooking(
            BookingModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_UpdateBooking",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@booking_ID",
                    model.booking_ID);

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

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // ---------------------------------------------------------
        // Delete existing booking
        //
        // Stored Procedure:
        // sp_DeleteBooking
        //
        // User_ID is checked so users can only delete
        // their own bookings.
        // ---------------------------------------------------------
        public bool DeleteBooking(
            int bookingId,
            int userId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_DeleteBooking",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@booking_ID",
                    bookingId);

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    userId);

                con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }


        // =========================================================
        // LOGIN FUNCTIONS
        // =========================================================

        // ---------------------------------------------------------
        // Authenticate user
        //
        // Stored Procedure:
        // sp_LoginUser
        //
        // Returns:
        // UserModel if login is successful
        // null if username/password is incorrect
        // ---------------------------------------------------------
        public UserModel Login(LoginModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_LoginUser",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

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
                        UserModel user =
                            new UserModel();

                        user.User_ID =
                            Convert.ToInt32(
                                reader["User_ID"]);

                        user.User_Name =
                            reader["User_Name"]
                            .ToString();

                        user.Email_ID =
                            reader["Email_ID"]
                            .ToString();

                        user.City =
                            reader["City"]
                            .ToString();

                        user.PhoneNo =
                            reader["PhoneNo"]
                            .ToString();

                        return user;
                    }
                }
            }

            return null;
        }


        // =========================================================
        // USER / PROFILE FUNCTIONS
        // =========================================================

        // ---------------------------------------------------------
        // Get user profile
        //
        // Stored Procedure:
        // sp_GetUser
        // ---------------------------------------------------------
        public UserModel GetUser(int userId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_GetUser",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    userId);

                con.Open();

                using (SqlDataReader reader =
                       cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserModel user =
                            new UserModel();

                        user.User_ID =
                            Convert.ToInt32(
                                reader["User_ID"]);

                        user.User_Name =
                            reader["User_Name"]
                            .ToString();

                        user.Email_ID =
                            reader["Email_ID"]
                            .ToString();

                        user.City =
                            reader["City"]
                            .ToString();

                        user.PhoneNo =
                            reader["PhoneNo"]
                            .ToString();

                        return user;
                    }
                }
            }

            return null;
        }


        // ---------------------------------------------------------
        // Update user profile
        //
        // Stored Procedure:
        // sp_UpdateUser
        // ---------------------------------------------------------
        public bool UpdateUser(
            UserModel model)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd =
                   new SqlCommand(
                       "sp_UpdateUser",
                       con))
            {
                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@User_ID",
                    model.User_ID);

                cmd.Parameters.AddWithValue(
                    "@User_Name",
                    model.User_Name);

                cmd.Parameters.AddWithValue(
                    "@Email_ID",
                    model.Email_ID);

                cmd.Parameters.AddWithValue(
                    "@City",
                    model.City);

                cmd.Parameters.AddWithValue(
                    "@PhoneNo",
                    model.PhoneNo);

                con.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }
}