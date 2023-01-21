using System;
using System.Collections.Generic;
using System.IO;

namespace CakeOrdering
{
    class Order
    {
        // Частные поля для хранения информации о заказе

        private decimal totalPrice;

        // Публичное свойства для доступа к информации о заказе
        public string Shape { get; set; }
        public string Size { get; set; }
        public string Taste { get; set; }
        public int Quantity { get; set;  }
        public string Glaze { get; set; }
        public string Decor { get; set; }
        public decimal TotalPrice { get; set; }

        // Частный метод для отображения главного меню и получения выбора пользователя
        private int DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Cake Ordering Menu:\n");
            Console.WriteLine("1. Форма");
            Console.WriteLine("2. Размер");
            Console.WriteLine("3. Вкус");
            Console.WriteLine("4. Количество");
            Console.WriteLine("5. Глазурь");
            Console.WriteLine("6. Украшение");
            Console.WriteLine("\nНажмите Escape что бы выйти.\n");

            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                return -1;
            }
            else
            {
                return int.Parse(keyPressed.KeyChar.ToString());
            }
        }

        // Частный метод для отображения подменю для определенного пункта главного меню и получения выбора пользователя
        private string DisplaySubMenu(int mainMenuItem)
        {
            Console.Clear();
            Console.WriteLine("Пожалуйста, выберите вариант:\n");
            List<MenuItem> menuItems = new List<MenuItem>();
            switch (mainMenuItem)
            {
                case 1:
                    menuItems.Add(new MenuItem("Круг", 10));
                    menuItems.Add(new MenuItem("Квадрат", 12));
                    menuItems.Add(new MenuItem("Сердце", 15));
                    break;
                case 2:
                    menuItems.Add(new MenuItem("Мальнекий", 20));
                    menuItems.Add(new MenuItem("Средний", 25));
                    menuItems.Add(new MenuItem("Большой", 30));
                    break;
                case 3:
                    menuItems.Add(new MenuItem("Шоколад", 5));
                    menuItems.Add(new MenuItem("Ваниль", 6));
                    menuItems.Add(new MenuItem("Клубника", 7));
                    break;
                case 4:
                    menuItems.Add(new MenuItem("1", 1));
                    menuItems.Add(new MenuItem("2", 2));
                    menuItems.Add(new MenuItem("3", 3));
                    menuItems.Add(new MenuItem("4", 4));
                    menuItems.Add(new MenuItem("5", 5));
                    break;
                case 5:
                    menuItems.Add(new MenuItem("Шоколад", 2));
                    menuItems.Add(new MenuItem("Ваниль", 2));
                    menuItems.Add(new MenuItem("Клубника", 2));
                    break;
                case 6:
                    menuItems.Add(new MenuItem("Побрызгать", 1));
                    menuItems.Add(new MenuItem("Помада", 2));
                    menuItems.Add(new MenuItem("Цветы Гампасте", 3));
                    break;
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i].Description} ({menuItems[i].Price}$)");
            }
            Console.WriteLine("\nНажмите Escape что бы вернуться назад.\n");

            ConsoleKeyInfo keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                return null;
            }
            else
            {
                return menuItems[int.Parse(keyPressed.KeyChar.ToString()) - 1].Description;
            }
        }

        // Публичный метод размещения заказа
        public void PlaceOrder()
        {
            int mainMenuItem = 0;
            while (mainMenuItem != -1)
            {
                mainMenuItem = DisplayMainMenu();
                if (mainMenuItem != -1)
                {
                    string subMenuItem = DisplaySubMenu(mainMenuItem);
                    if (subMenuItem != null)
                    {
                        switch (mainMenuItem)
                        {
                            case 1:
                                Shape = subMenuItem;
                                break;
                            case 2:
                                Size = subMenuItem;
                                break;
                            case 3:
                                Taste = subMenuItem;
                                break;
                            case 4:
                                Quantity = int.Parse(subMenuItem);
                                break;
                            case 5:
                                Glaze = subMenuItem;
                                break;
                            case 6:
                                Decor = subMenuItem;
                                break;
                        }
                    }
                }
            }

            // Рассчитываение общей цены
            switch (Size)
            {
                case "Мальнеький":
                    totalPrice += 20;
                    break;
                case "Средний":
                    totalPrice += 25;
                    break;
                case "Большой":
                    totalPrice += 30;
                    break;
            }
            totalPrice += Quantity * 5; // Добавить стоимость по вкусу
            totalPrice += Quantity * 2; //Добавить стоимость глазури
            totalPrice += Quantity * 1; // Добавить стоимость декора

            // Отображение окончательного заказа и общей цены
            Console.Clear();
            Console.WriteLine("Ваша окончательная цена:");
            Console.WriteLine($"Форма: {Shape}");
            Console.WriteLine($"Размер: {Size}");
            Console.WriteLine($"Вкус: {Taste}");
            Console.WriteLine($"Количество: {Quantity}");
            Console.WriteLine($"Глазурь: {Glaze}");
            Console.WriteLine($"Декор: {Decor}");
            Console.WriteLine($"Окончательная цена: {totalPrice}$");
            // Сохранение заказа в файл 
            string filePath = @"C:\tort\OrderHistory.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Форма: {Shape}");
                writer.WriteLine($"Размер: {Size}");
                writer.WriteLine($"Вкус: {Taste}");
                writer.WriteLine($"Количество: {Quantity}");
                writer.WriteLine($"Глазурь: {Glaze}");
                writer.WriteLine($"Декор: {Decor}");
                writer.WriteLine($"Окончательная цена: {totalPrice}$");
                writer.WriteLine("------------------------------");
            }
        }
    }

    // Тип данных для пунктов меню
    class MenuItem
    {
        // Приватные поля для хранения информации о пункте меню
        private string description;
        private decimal price;

        // Общедоступные свойства для доступа к информации о пункте меню
        public string Description { get { return description; } set { description = value; } }
        public decimal Price { get { return price; } set { price = value; } }

        // Конструктор для создания пункта меню
        public MenuItem(string description, decimal price)
        {
            this.description = description;
            this.price = price;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();
            order.PlaceOrder();

            Console.WriteLine("\nНажмите любую клавишу, чтобы разместить другой заказ, или Escape, чтобы выйти.");
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            while (keyPressed.Key != ConsoleKey.Escape)
            {
                order = new Order();
                order.PlaceOrder();
                Console.WriteLine("\nНажмите любую клавишу, чтобы разместить другой заказ, или Escape, чтобы выйти.");
                keyPressed = Console.ReadKey();
            }
        }
    }
}