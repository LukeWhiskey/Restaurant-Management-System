using System;
using System.Collections.Generic;

namespace MenuRMS
{
    public class ConvertTimee
    {
        // Properties to store start time and end time
        private string StartTime { get; set; }
        private string EndTime { get; set; }

        // Constructor to initialize start time and end time
        public ConvertTimee(string startTime, string endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        // Method to convert a given time to a specific format
        public string ConvertTime(string inputTime)
        {
            TimeSpan time = TimeSpan.Parse(inputTime);

            if (time >= TimeSpan.Parse("12:00"))
            {
                time -= TimeSpan.Parse("12:00");
                return time.ToString(@"hh\:mm") + "pm";
            }
            else
            {
                return time.ToString(@"hh\:mm") + "am";
            }
        }

        // Property to get the converted start time
        public string StartTimeClockConversion
        {
            get { return ConvertTime(StartTime); }
            set { StartTime = value.ToString(); }
        }

        // Property to get the converted end time
        public string EndTimeClockConversion
        {
            get { return ConvertTime(EndTime); }
            set { EndTime = value.ToString(); }
        }

        // Method to print the serving time
        public void ServingTime()
        {
            Console.WriteLine($"Serving Time: {StartTimeClockConversion} - {EndTimeClockConversion}");
        }
    }

    public abstract class MenuItem
    {
        // Properties for menu item details
        protected string Name { get; set; }
        protected decimal Price { get; set; }
        protected bool Available { get; set; }
        protected List<string> Ingredients { get; set; }
        protected List<string> Category { get; set; }
        protected string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // Constructor to initialize menu item details
        public MenuItem(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, string startTime, string endTime)
        {
            Name = name;
            Price = price;
            Available = available;
            Ingredients = ingredients;
            Category = category;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
        }

        // Method to retrieve the category of the menu item
        public virtual string category()
        {
            return string.Join(", ", Category);
        }

        // Method to print the menu item details
        public virtual void MenuItemDetails()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Price: £{Price}");
            Console.WriteLine($"Available: {Available}");
            Console.WriteLine($"Ingredients: {string.Join(", ", Ingredients)}");
            Console.WriteLine($"Category: {string.Join(", ", Category)}");
            Console.WriteLine($"Description: {Description}");
        }

        // Method to print the menu item details and serving time
        public virtual void PrintItems()
        {
            MenuItemDetails();
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
        }
    }

    public class Appetizer : MenuItem
    {
        // Constructor to initialize appetizer details
        public Appetizer(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, string startTime, string endTime) : base(name, price, available, ingredients, category, description, startTime, endTime)
        {

        }
    }

    public class Entre : MenuItem
    {
        // Constructor to initialize entre details
        public Entre(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, string startTime, string endTime) : base(name, price, available, ingredients, category, description, startTime, endTime)
        {

        }
    }

    public class Desert : MenuItem
    {
        // Constructor to initialize desert details
        public Desert(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, string startTime, string endTime) : base(name, price, available, ingredients, category, description, startTime, endTime)
        {

        }
    }

    public abstract class Drinks : MenuItem
    {
        protected int Ml { get; set; }

        // Constructor to initialize drink details
        public Drinks(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, int ml, string startTime, string endTime) : base(name, price, available, ingredients, category, description, startTime, endTime)
        {
            Name = name;
            Price = price;
            Available = available;
            Ingredients = ingredients;
            Category = category;
            Description = description;
            Ml = ml;
            StartTime = startTime;
            EndTime = endTime;
        }

        // Method to print the drink details
        public virtual void MenuItemDrinkDetails()
        {
            MenuItemDetails();
            Console.WriteLine($"Volume: {Ml}ml");
        }

        // Method to print the drink details and serving time
        public override void PrintItems()
        {
            MenuItemDrinkDetails();
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
        }
    }

    public class AlcDrink : Drinks
    {
        protected decimal AlcUnits { get { return (AlcPerc / 100) * Ml / 10; } set { AlcPerc = value; } }
        protected decimal AlcPerc { get; set; }

        // Constructor to initialize alcoholic drink details
        public AlcDrink(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, int ml, decimal alcPerc, string startTime, string endTime) : base(name, price, available, ingredients, category, description, ml, startTime, endTime)
        {
            AlcPerc = alcPerc;
        }

        // Method to print the alcoholic drink details
        public override void PrintItems()
        {
            MenuItemDrinkDetails();
            Console.WriteLine($"Alcoholic Units: {AlcUnits} Units");
            Console.WriteLine($"Alcoholic Percentage: {AlcPerc}%");
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
        }
    }

    public class Drink : Drinks
    {
        // Constructor to initialize drink details
        public Drink(string name, decimal price, bool available, List<string> ingredients, List<string> category, string description, int ml, string startTime, string endTime) : base(name, price, available, ingredients, category, description, ml, startTime, endTime)
        {

        }
    }

    public abstract class Menu
    {
        protected string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // Constructor to initialize menu details
        public Menu(string name, string startTime, string endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }

        // Abstract method to list the menu items
        public abstract void ListMenu();

        private List<MenuItem> _menuItem = new List<MenuItem>();

        // Method to add a menu item
        public void AddMenuItem(MenuItem menuItem)
        {
            _menuItem.Add(menuItem);
        }

        // Method to list menu items of a specific type
        public void ListMenuItems(Type type)
        {
            Console.WriteLine($"--- {type.Name} --- ");
            foreach (MenuItem item in _menuItem)
            {
                if (type.IsAssignableFrom(item.GetType()))
                {
                    item.PrintItems();
                }
            }
        }

        // Method to list all menu items
        public void ListMenuItemsAll()
        {
            ListMenuItems(typeof(Appetizer));
            ListMenuItems(typeof(Entre));
            ListMenuItems(typeof(Desert));
            ListMenuItems(typeof(Drink));
            ListMenuItems(typeof(AlcDrink));
        }
    }

    public class BreakfastMenu : Menu
    {
        // Constructor to initialize breakfast menu details
        public BreakfastMenu(string name, string startTime, string endTime) : base(name, startTime, endTime)
        {
        }

        // Method to list the breakfast menu
        public override void ListMenu()
        {
            Console.WriteLine("------ Breakfast Menu ------");
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
            ListMenuItemsAll();
        }
    }

    public class LunchMenu : Menu
    {
        // Constructor to initialize lunch menu details
        public LunchMenu(string name, string startTime, string endTime) : base(name, startTime, endTime)
        {
        }

        // Method to list the lunch menu
        public override void ListMenu()
        {
            Console.WriteLine("------ Lunch Menu ------");
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
            ListMenuItemsAll();
        }
    }

    public class DinnerMenu : Menu
    {
        // Constructor to initialize dinner menu details
        public DinnerMenu(string name, string startTime, string endTime) : base(name, startTime, endTime)
        {
        }

        // Method to list the dinner menu
        public override void ListMenu()
        {
            Console.WriteLine("------ Dinner Menu ------");
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
            ListMenuItemsAll();
        }
    }

    public class CustomMenu : Menu
    {
        // Constructor to initialize custom menu details
        public CustomMenu(string name, string startTime, string endTime) : base(name, startTime, endTime)
        {
        }

        // Method to list the custom menu
        public override void ListMenu()
        {
            Console.WriteLine($"------ {StartTime} - {EndTime} Custom Menu ------");
            ConvertTimee times = new ConvertTimee(StartTime, EndTime);
            times.ServingTime();
            ListMenuItemsAll();
        }
    }
}
