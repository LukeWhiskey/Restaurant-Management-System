// Import required modules and namespaces
using MenuRMS;
using MySql.Data.MySqlClient;

namespace Migrations
{
    public class Migration
    {
        private readonly string connectionString = "Server=127.0.0.1; Port=3306; Database=rms; Uid=root; Pwd=#######";

        // Method to insert menu items data into the database
        public void InsertDataMenuItems()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Read the data from the CSV file
                using (StreamReader reader = new StreamReader("C:/Users/MCFox/OneDrive/Desktop/OOP (Object Oriented Programming)/Assignment 1/MenuItems.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        // Extract values from the CSV line
                        string course = values[0];
                        string name = values[1];
                        decimal price = decimal.Parse(values[2]);
                        bool availability = bool.Parse(values[3]);
                        string ingredients = values[4].Replace('/', ',');
                        string category = values[5].Replace('/', ',');
                        string description = values[6].Replace('/', ',');
                        TimeSpan startTime = TimeSpan.Parse(values[7]);
                        TimeSpan endTime = TimeSpan.Parse(values[8]);
                        decimal volume = decimal.Parse(values[9]);
                        decimal alcPerc = decimal.Parse(values[10]);

                        // Truncate values if they exceed the maximum length allowed in the database
                        if (course.Length > 15)
                        {
                            name = name.Substring(0, 15);
                        }
                        if (name.Length > 30)
                        {
                            name = name.Substring(0, 30);
                        }

                        // Create the SQL command to insert the menu item
                        var menuItemCommand = new MySqlCommand
                        {
                            CommandText = "INSERT INTO menuItems (Course, Name, Price, Availability, Ingredients, Category, Description, StartTime, EndTime, Volume, AlcPerc) VALUES (@Course, @Name, @Price, @Availability, @Ingredients, @Category, @Description, @StartTime, @EndTime, @Volume, @AlcPerc)"
                        };

                        // Set the parameters for the SQL command
                        menuItemCommand.Parameters.AddWithValue("@Course", course);
                        menuItemCommand.Parameters.AddWithValue("@Name", name);
                        menuItemCommand.Parameters.AddWithValue("@Price", price);
                        menuItemCommand.Parameters.AddWithValue("@Availability", availability);
                        menuItemCommand.Parameters.AddWithValue("@Ingredients", ingredients);
                        menuItemCommand.Parameters.AddWithValue("@Category", category);
                        menuItemCommand.Parameters.AddWithValue("@Description", description);
                        menuItemCommand.Parameters.AddWithValue("@StartTime", startTime);
                        menuItemCommand.Parameters.AddWithValue("@EndTime", endTime);
                        menuItemCommand.Parameters.AddWithValue("@Volume", volume);
                        menuItemCommand.Parameters.AddWithValue("@AlcPerc", alcPerc);

                        int rowsAffected = 0;
                        // Check if the menu item already exists in the database
                        using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM menuItems WHERE Name = @Name", connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert the menu item if it doesn't already exist
                        if (rowsAffected == 0)
                        {
                            using (var insertCommand = new MySqlCommand(menuItemCommand.CommandText, connection))
                            {
                                foreach (var parameter in menuItemCommand.Parameters)
                                {
                                    insertCommand.Parameters.Add(parameter);
                                }
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        menuItemCommand.Parameters.Clear();
                    }
                }

                connection.Close();
            }
        }

        // Method to insert category data into the database
        public void InsertDataCategory()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Read the data from the CSV file
                using (StreamReader reader = new StreamReader("C:/Users/MCFox/OneDrive/Desktop/OOP (Object Oriented Programming)/Assignment 1/MenuItems2.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        // Extract values from the CSV line
                        string name = values[0];
                        string description = values[1];

                        // Truncate the value if it exceeds the maximum length allowed in the database
                        if (name.Length > 30)
                        {
                            name = name.Substring(0, 30);
                        }

                        // Create the SQL command to insert the category
                        var categoryCommand = new MySqlCommand
                        {
                            CommandText = "INSERT INTO category (Name, Description) VALUES (@Name, @Description)"
                        };

                        categoryCommand.Parameters.AddWithValue("@Name", name);
                        categoryCommand.Parameters.AddWithValue("@Description", description);

                        int rowsAffected = 0;
                        // Check if the category already exists in the database
                        using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM category WHERE Name = @Name", connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert the category if it doesn't already exist
                        if (rowsAffected == 0)
                        {
                            using (var insertCommand = new MySqlCommand(categoryCommand.CommandText, connection))
                            {
                                foreach (var parameter in categoryCommand.Parameters)
                                {
                                    insertCommand.Parameters.Add(parameter);
                                }
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        categoryCommand.Parameters.Clear();
                    }
                }
                connection.Close();
            }
        }

        //Insert menu data into the database
        public void InsertDataMenus()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (StreamReader reader = new StreamReader("C:/Users/MCFox/OneDrive/Desktop/OOP (Object Oriented Programming)/Assignment 1/MenuItems3.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(",");

                        string name = values[0];
                        TimeSpan startTime = TimeSpan.Parse(values[1]);
                        TimeSpan endTime = TimeSpan.Parse(values[2]);

                        // Truncate the value if it exceeds the maximum length allowed in the database
                        if (name.Length > 30)
                        {
                            name = name.Substring(0, 30);
                        }

                        var menusCommand = new MySqlCommand
                        {
                            CommandText = "INSERT INTO menus (Name, StartTime, EndTime) VALUES (@Name, @StartTime, @EndTime)"
                        };
                        menusCommand.Parameters.AddWithValue("@Name", name);
                        menusCommand.Parameters.AddWithValue("@StartTime", startTime);
                        menusCommand.Parameters.AddWithValue("@EndTime", endTime);

                        int rowsAffected = 0;
                        // Check if the category already exists in the database
                        using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM menus WHERE Name = @Name", connection))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert the category if it doesn't already exist
                        if (rowsAffected == 0)
                        {
                            using (var insertCommand = new MySqlCommand(menusCommand.CommandText, connection))
                            {
                                foreach (var parameter in menusCommand.Parameters)
                                {
                                    insertCommand.Parameters.Add(parameter);
                                }
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        menusCommand.Parameters.Clear();
                    }
                }
                connection.Close();
            }
        }

        // Method to retrieve menus from the database
        public Dictionary<string, Menu> getMenus()
        {
            Dictionary<string, Menu> menus = new Dictionary<string, Menu>();
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Name, StartTime, EndTime FROM menus";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString("Name");
                            string startTime = reader.GetString("StartTime");
                            string endTime = reader.GetString("EndTime");

                            // Create the corresponding menu object based on the menu name
                            if (name == "Breakfast")
                            {
                                var menu = new BreakfastMenu(name, startTime, endTime);
                                menus.Add(name, menu);
                            }
                            if (name == "Lunch")
                            {
                                var menu = new LunchMenu(name, startTime, endTime);
                                menus.Add(name, menu);
                            }
                            if (name == "Dinner")
                            {
                                var menu = new DinnerMenu(name, startTime, endTime);
                                menus.Add(name, menu);
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return menus;
        }

        // Method to retrieve menu items from the database
        public List<MenuItem> getMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var query = "SELECT * FROM MenuItems";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string course = reader.GetString("Course");
                            string name = reader.GetString("name");
                            decimal price = reader.GetDecimal("price");
                            bool availability = reader.GetBoolean("availability");
                            string ingredients = reader.GetString("ingredients");
                            string category = reader.GetString("category");
                            string description = reader.GetString("description");
                            string startTime = reader.GetString("startTime").Substring(0, 5);
                            string endTime = reader.GetString("endTime").Substring(0, 5);
                            int volume = reader.GetInt32("volume");
                            decimal alcPerc = reader.GetDecimal("alcPerc") * 100;

                            var ivalues = ingredients.Split(',');

                            List<string> ingredientsList = new List<string>();
                            List<string> categoriesList = new List<string>();

                            // Extract ingredients and categories from the database
                            foreach (var ingredient in ivalues)
                            {
                                ingredientsList.Add(ingredient);
                            }
                            var cvalues = category.Split(',');
                            foreach (var cat in cvalues)
                            {
                                categoriesList.Add(cat);
                            }

                            // Create the corresponding menu item object based on the course type
                            if (course == "Appetizer")
                            {
                                var dish = new Appetizer(name, price, availability, ingredientsList, categoriesList, description, startTime, endTime);
                                menuItems.Add(dish);
                            }
                            if (course == "Entre")
                            {
                                var dish = new Entre(name, price, availability, ingredientsList, categoriesList, description, startTime, endTime);
                                menuItems.Add(dish);
                            }
                            if (course == "Desert")
                            {
                                var dish = new Desert(name, price, availability, ingredientsList, categoriesList, description, startTime, endTime);
                                menuItems.Add(dish);
                            }
                            if (course == "Drink")
                            {
                                if (alcPerc > 0)
                                {
                                    var dish = new AlcDrink(name, price, availability, ingredientsList, categoriesList, description, volume, alcPerc, startTime, endTime);
                                    menuItems.Add(dish);
                                }
                                else
                                {
                                    var dish = new Drink(name, price, availability, ingredientsList, categoriesList, description, volume, startTime, endTime);
                                    menuItems.Add(dish);
                                }
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return menuItems;
        }
    }
}
