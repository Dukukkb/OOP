using System;
using System.Collections.Generic;

public class Admin
{
    private string password = "admin123";
    private VendingMachine machine;
    
    public Admin(VendingMachine vm)
    {
        machine = vm;
    }
    
    public bool Login()
    {
        Console.Write("\nПароль: ");
        if (Console.ReadLine() == password)
        {
            Console.WriteLine("Вход выполнен!");
            return true;
        }
        Console.WriteLine("Неверный пароль!");
        return false;
    }
    
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\nАДМИН");
            Console.WriteLine("1. Состояние");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Пополнить");
            Console.WriteLine("4. Добавить монеты");
            Console.WriteLine("5. Собрать деньги");
            Console.WriteLine("0. Выход");
            Console.Write(">> ");
            
            string c = Console.ReadLine();
            
            if (c == "1") ShowStatus();
            else if (c == "2") AddProduct();
            else if (c == "3") Restock();
            else if (c == "4") AddCoins();
            else if (c == "5") Collect();
            else if (c == "0") break;
        }
    }
    
    private void ShowStatus()
    {
        Console.WriteLine("\nСостояние");
        Console.WriteLine("Товары:");
        foreach (Product p in machine.GetProducts())
            Console.WriteLine($"  {p.Name} - {p.Quantity}шт ({p.Price}р)");
        
        Console.WriteLine("Монеты:");
        int total = 0;
        foreach (Coin c in machine.GetCoins())
        {
            if (c.Count > 0) Console.WriteLine($"  {c.Value}р - {c.Count}шт");
            total += c.Value * c.Count;
        }
        Console.WriteLine($"Всего: {total}р");
    }
    
    private void AddProduct()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine();
        if (name == "" || name == null) return;
        
        Console.Write("Цена: ");
        string priceStr = Console.ReadLine();
        if (priceStr == "" || priceStr == null) return;
        int price = int.Parse(priceStr);
        
        Console.Write("Количество: ");
        string qtyStr = Console.ReadLine();
        if (qtyStr == "" || qtyStr == null) return;
        int qty = int.Parse(qtyStr);
        
        machine.GetProducts().Add(new Product(name, price, qty));
        Console.WriteLine("Добавлено!");
    }
    
    private void Restock()
    {
        List<Product> products = machine.GetProducts();
        for (int i = 0; i < products.Count; i++)
            Console.WriteLine($"{i+1}. {products[i].Name} ({products[i].Quantity}шт)");
        
        Console.Write("Номер: ");
        string numStr = Console.ReadLine();
        if (numStr == "" || numStr == null) return;
        int num = int.Parse(numStr);
        
        Console.Write("Добавить: ");
        string addStr = Console.ReadLine();
        if (addStr == "" || addStr == null) return;
        int add = int.Parse(addStr);
        
        products[num - 1].Quantity += add;
        Console.WriteLine("Пополнено!");
    }
    
    private void AddCoins()
    {
        Console.Write("Номинал (1,2,5,10,50,100): ");
        string valStr = Console.ReadLine();
        if (valStr == "" || valStr == null) return;
        int val = int.Parse(valStr);
        
        Console.Write("Количество: ");
        string cntStr = Console.ReadLine();
        if (cntStr == "" || cntStr == null) return;
        int cnt = int.Parse(cntStr);
        
        bool found = false;
        foreach (Coin c in machine.GetCoins())
        {
            if (c.Value == val)
            {
                c.Count += cnt;
                found = true;
                break;
            }
        }
        if (!found) machine.GetCoins().Add(new Coin(val, cnt));
        
        Console.WriteLine("Добавлено!");
    }
    
    private void Collect()
    {
        int total = 0;
        foreach (Coin c in machine.GetCoins())
            total += c.Value * c.Count;
        
        Console.WriteLine($"В автомате: {total}р");
        Console.Write("Собрать? (да/нет): ");
        
        if (Console.ReadLine() == "да")
        {
            foreach (Coin c in machine.GetCoins())
                if (c.Count > 3) c.Count = 3;
            Console.WriteLine("Собрано!");
        }
    }
}
