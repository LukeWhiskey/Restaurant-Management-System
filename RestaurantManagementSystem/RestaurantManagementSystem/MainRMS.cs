// Bring in required modules for the main script
using MenuRMS;
using Migrations;
using DbConnection.Test;

class RMS
{
    static void Main(string[] args)
    {
        // White box test to check if database connection is open and valid
        ConnectionStringTest connectionStringTest = new ConnectionStringTest();
        connectionStringTest.TestConnection();

        // Migrations to import data
        Migration insertdata = new Migration();
        insertdata.InsertDataMenuItems();
        insertdata.InsertDataCategory();
        insertdata.InsertDataMenus();
        var menus = insertdata.getMenus();
        foreach (Menu menu in menus.Values)
        {
            // Filter and add menu items based on their availability within the menu's time range
            var menuItems = insertdata.getMenuItems();
            foreach (MenuItem menuItem in menuItems)
            {
                TimeSpan menuItemStartTime = TimeSpan.Parse(menuItem.StartTime);
                TimeSpan menuItemEndTime = TimeSpan.Parse(menuItem.EndTime);
                TimeSpan menuStartTime = TimeSpan.Parse(menu.StartTime);
                TimeSpan menuEndTime = TimeSpan.Parse(menu.EndTime);

                if (menuItemEndTime > menuStartTime && menuItemStartTime < menuEndTime)
                {
                    menu.AddMenuItem(menuItem);
                }
            }
        }

        // Display menu options
        Console.WriteLine("Hello, This is the Restaurant Management System");
        Console.WriteLine("Check between either of the following which you would like to be listed");
        Console.WriteLine("1) Breakfast Menu");
        Console.WriteLine("2) Lunch Menu");
        Console.WriteLine("3) Dinner Menu");
        Console.WriteLine("4) Sort by Appetizers");
        Console.WriteLine("5) Sort by Entrees");
        Console.WriteLine("6) Sort by Desserts");
        Console.WriteLine("7) Sort by Drinks");
        Console.WriteLine("8) Sort by Alcoholic Drinks");
        Console.WriteLine("9) Sort by Dishes available between a specific time e.g. 10:00, 12:00");
        Console.WriteLine("10) Sort by Category");
        Console.WriteLine("11) List all Menus");
        Console.Write("--> ");

        // Read user input
        string input = Console.ReadLine();

        // Handle user input
        if (input == "1")
        {
            menus["Breakfast"].ListMenu();
        }
        else if (input == "2")
        {
            menus["Lunch"].ListMenu();
        }
        else if (input == "3")
        {
            menus["Dinner"].ListMenu();
        }
        else if (input == "4")
        {
            // Display menu items categorized as appetizers for each menu
            foreach (var menu in menus.Keys)
            {
                Console.WriteLine($"--- {menu} Menu ---");
                menus[menu].ListMenuItems(typeof(Appetizer));
            }
        }
        else if (input == "5")
        {
            // Display menu items categorized as entrees for each menu
            foreach (var menu in menus.Keys)
            {
                Console.WriteLine($"--- {menu} Menu ---");
                menus[menu].ListMenuItems(typeof(Entre));
            }
        }
        else if (input == "6")
        {
            // Display menu items categorized as desserts for each menu
            foreach (var menu in menus.Keys)
            {
                Console.WriteLine($"--- {menu} Menu ---");
                menus[menu].ListMenuItems(typeof(Desert));
            }
        }
        else if (input == "7")
        {
            // Display menu items categorized as drinks for each menu
            foreach (var menu in menus.Keys)
            {
                Console.WriteLine($"--- {menu} Menu ---");
                menus[menu].ListMenuItems(typeof(Drink));
            }
        }
        else if (input == "8")
        {
            // Display menu items categorized as alcoholic drinks for each menu
            foreach (var menu in menus.Keys)
            {
                Console.WriteLine($"--- {menu} Menu ---");
                menus[menu].ListMenuItems(typeof(AlcDrink));
            }
        }
        else if (input == "9")
        {
            // Create a custom menu based on a specific time range and display its available dishes
            Console.Write("Start time --> ");
            string start = Console.ReadLine();
            Console.Write("End time --> ");
            string end = Console.ReadLine();
            CustomMenu newMenu = new CustomMenu("Custom", start, end);
            var menuItems = insertdata.getMenuItems();
            foreach (MenuItem menuItem in menuItems)
            {
                TimeSpan menuItemStartTime = TimeSpan.Parse(menuItem.StartTime);
                TimeSpan menuItemEndTime = TimeSpan.Parse(menuItem.EndTime);
                TimeSpan menuStartTime = TimeSpan.Parse(newMenu.StartTime);
                TimeSpan menuEndTime = TimeSpan.Parse(newMenu.EndTime);

                if (menuItemEndTime > menuStartTime && menuItemStartTime < menuEndTime)
                {
                    newMenu.AddMenuItem(menuItem);
                }
            }
            newMenu.ListMenu();
        }
        else if (input == "10")
        {
            // Display menu items based on a specific category
            var menuItems = insertdata.getMenuItems();
            Console.Write("--> ");
            string category = Console.ReadLine();
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.category().Contains(category))
                {
                    menuItem.MenuItemDetails();
                }
            }
        }
        else if (input == "11")
        {
            // List all menus and their respective menu items
            foreach (var menu in menus.Keys)
            {
                menus[menu].ListMenu();
            }
        }
    }
}





