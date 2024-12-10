using MySql.Data.MySqlClient;
using System;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=your-server;port=your-port;database=your-database;user=your-user;password=your-password;sslmode=Required";
        
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\n--- Menu ---");
                    Console.WriteLine("1. Display all tables");
                    Console.WriteLine("2. Display data from a specific table");
                    Console.WriteLine("3. Insert values into tables");
                    Console.WriteLine("4. Perform a JOIN query");
                    Console.WriteLine("5. Perform a filtering query");
                    Console.WriteLine("6. Perform an aggregate query");
                    Console.WriteLine("7. Exit");

                    Console.Write("\nSelect an option (1-7): ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            SelectAllTables(connection);
                            break;
                        case "2":
                            SelectFromTable(connection);
                            break;
                        case "3":
                            InsertValuesToTables(connection);
                            break;
                        case "4":
                            SomeJoinFunction(connection);
                            break;
                        case "5":
                            SomeFilterFunction(connection);
                            break;
                        case "6":
                            SomeAggregateFunction(connection);
                            break;
                        case "7":
                            exit = true;
                            Console.WriteLine("Exiting program.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please select a number between 1 and 7.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void SelectAllTables(MySqlConnection connection)
    {
        string[] queries = {
            "SELECT * FROM Company;",
            "SELECT * FROM Income;",
            "SELECT * FROM Expense;",
            "SELECT * FROM Category;",
            "SELECT * FROM Transaction;"
        };

        try
        {
            foreach (string query in queries)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"\n--- {cmd.CommandText} ---");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i) + "\t");
                }
                Console.WriteLine();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "\t");
                    }
                    Console.WriteLine();
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SelectFromTable(MySqlConnection connection)
    {
        Console.WriteLine("Select a table to display data from (Company, Income, Expense, Category, Transaction): ");
        string table = Console.ReadLine();
        string query = $"SELECT * FROM {table};";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine($"\n--- {table} ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void InsertValuesToTables(MySqlConnection connection)
    {
        try
        {
            // Insert into Company table
            Console.WriteLine("Inserting into Company table...");
            string companyQuery = @"
                INSERT INTO Company (Name, RegistrationDate) VALUES 
                ('Tech Solutions', '2024-01-01'), 
                ('Innovate Inc', '2024-02-01');";
            MySqlCommand companyCmd = new MySqlCommand(companyQuery, connection);
            int companyRowsAffected = companyCmd.ExecuteNonQuery();
            Console.WriteLine($"{companyRowsAffected} row(s) inserted into Company.");

            // Insert into Category table
            Console.WriteLine("Inserting into Category table...");
            string categoryQuery = @"
                INSERT INTO Category (Name) VALUES 
                ('Marketing'), 
                ('Development');";
            MySqlCommand categoryCmd = new MySqlCommand(categoryQuery, connection);
            int categoryRowsAffected = categoryCmd.ExecuteNonQuery();
            Console.WriteLine($"{categoryRowsAffected} row(s) inserted into Category.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SomeJoinFunction(MySqlConnection connection)
    {
        string query = @"
            SELECT Transaction.ID, Company.Name AS CompanyName, Category.Name AS CategoryName, Transaction.Amount, Transaction.Date
            FROM Transaction
            INNER JOIN Company ON Transaction.CompanyID = Company.ID
            INNER JOIN Category ON Transaction.CategoryID = Category.ID;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Transactions with JOIN ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SomeFilterFunction(MySqlConnection connection)
    {
        string query = "SELECT * FROM Income WHERE Amount > 1000;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Income with Amount > 1000 ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SomeAggregateFunction(MySqlConnection connection)
    {
        string query = @"
            SELECT 
                SUM(Amount) AS TotalIncome, 
                AVG(Amount) AS AverageIncome 
            FROM Income;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine($"\nTotal Income: {reader["TotalIncome"]}");
                    Console.WriteLine($"Average Income: {reader["AverageIncome"]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
