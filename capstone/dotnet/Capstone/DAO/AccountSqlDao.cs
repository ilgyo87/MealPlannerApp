using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Accounts GetAccount(string username)
        {
            Accounts account = new Accounts();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.first_name, a.last_name, a.address, a.zipcode, a.state, a.phone_number, a.email, a.user_id, u.username " +
                                                    "FROM accounts a JOIN users u on a.user_id = u.user_id " +
                                                    "WHERE username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        account = AccountFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return account;
        }

        //public IList<Accounts> GetAllAccountsList()
        //{
        //    IList<Accounts> accounts = new List<Accounts>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.user_id, u.username " +
        //                                            "FROM accounts a JOIN users u ON a.user_id = u.user_id ", conn);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Accounts account = AccountFromReader(reader);
        //                accounts.Add(account);
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return accounts;
        //}

        //public IList<Accounts> GetAllAccountsListButMe(string username)
        //{
        //    IList<Accounts> accounts = new List<Accounts>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT a.account_id, a.user_id, u.username " +
        //                                            "FROM accounts a JOIN users u ON a.user_id = u.user_id " +
        //                                            "WHERE u.username <> @username", conn);
        //            cmd.Parameters.AddWithValue("@username", username);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Accounts account = AccountFromReader(reader);
        //                accounts.Add(account);
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return accounts;
        //}

        public Accounts PostAccount(Accounts account)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO accounts (user_id, first_name, last_name, address, zipcode, state, phone_number, email) " +
                                                "OUTPUT INSERTED.account_id " +
                                                "VALUES (@user_id, @first_name, @last_name, @address, @zipcode, @state, @phone_number, @email);", conn);
                    cmd.Parameters.AddWithValue("@user_id", account.UserId);
                    cmd.Parameters.AddWithValue("@first_name", account.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", account.LastName);
                    cmd.Parameters.AddWithValue("@address", account.Address);
                    cmd.Parameters.AddWithValue("@zipcode", account.Zipcode);
                    cmd.Parameters.AddWithValue("@state", account.State);
                    cmd.Parameters.AddWithValue("@phone_number", account.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", account.Email);
                    cmd.ExecuteScalar();
             
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return null;

            }
            return account;
        }

        public Accounts UpdateAccount(Accounts account)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlText = "UPDATE accounts SET first_name = @first_name, last_name = @last_name, address = @address, zipcode = @zipcode, state = @state, phone_number = @phone_number, email = @email  " +
                                     "FROM accounts WHERE user_id = @user_id;";
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@first_name", account.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", account.LastName);
                    cmd.Parameters.AddWithValue("@address", account.Address);
                    cmd.Parameters.AddWithValue("@zipcode", account.Zipcode);
                    cmd.Parameters.AddWithValue("@state", account.State);
                    cmd.Parameters.AddWithValue("@phone_number", account.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", account.Email);
                    cmd.Parameters.AddWithValue("@user_id", account.UserId);
                    cmd.ExecuteNonQuery();
         
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return account;
        }

        private Accounts AccountFromReader(SqlDataReader reader)
        {
            Accounts account = new Accounts();
            account.AccountId = Convert.ToInt32(reader["account_id"]);
            account.UserId = Convert.ToInt32(reader["user_id"]);
            account.FirstName = Convert.ToString(reader["first_name"]);
            account.LastName = Convert.ToString(reader["last_name"]);
            account.Address = Convert.ToString(reader["address"]);
            account.Zipcode = Convert.ToInt32(reader["zipcode"]);
            account.State = Convert.ToString(reader["state"]);
            account.PhoneNumber = Convert.ToInt32(reader["phone_number"]);
            account.Username = Convert.ToString(reader["username"]);

            return account;
        }
    }
}