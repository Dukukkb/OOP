using System;
using System.Collections.Generic;

public class VendingMachine
{
    private List<Product> products = new List<Product>();
    private List<Coin> coins = new List<Coin>();
    private int money = 0;
    
    public VendingMachine()
    {
        coins.Add(new Coin(1, 50));
        coins.Add(new Coin(2, 40));
        coins.Add(new Coin(5, 30));
        coins.Add(new Coin(10, 25));
        coins.Add(new Coin(50, 15));
        coins.Add(new Coin(100, 10));
        
        products.Add(new Product("Твикс", 70, 12));
        products.Add(new Product("Святой источник", 50, 15));
        products.Add(new Product("Чипсы Pringles", 90, 7));
    }
    
    public void ShowProducts()
    {
        Console.WriteLine("\nТОВАРЫ");
        for (int i = 0; i < products.Count; i++)
        {
            string status = products[i].Quantity > 0 ? "есть" : "НЕТ";
            Console.WriteLine($"{i+1}. {products[i].Name} - {products[i].Price}р ({products[i].Quantity}шт) [{status}]");
        }
        Console.WriteLine($"\nВнесено: {money} руб.");
    }
    
    public void InsertMoney(int value)
    {
        int[] valid = {1, 2, 5, 10, 50, 100};
        bool ok = false;
        for (int i = 0; i < valid.Length; i++)
            if (value == valid[i]) ok = true;
        
        if (!ok)
        {
            Console.WriteLine("Неправильный номинал!");
            return;
        }
        
        money += value;
        
        bool found = false;
        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i].Value == value)
            {
                coins[i].Count++;
                found = true;
                break;
            }
        }
        if (!found) coins.Add(new Coin(value, 1));
        
        Console.WriteLine($"Внесено: {value}р. Всего: {money}р");
    }
    
    public void BuyProduct(int num)
    {
        if (num < 1 || num > products.Count)
        {
            Console.WriteLine("Нет такого товара!");
            return;
        }
        
        Product p = products[num - 1];
        
        if (p.Quantity <= 0)
        {
            Console.WriteLine($"{p.Name} закончился!");
            return;
        }
        
        if (money < p.Price)
        {
            Console.WriteLine($"Не хватает {p.Price - money}р");
            return;
        }
        
        int change = money - p.Price;
        
        if (!CanGiveChange(change))
        {
            Console.WriteLine("Нет монет для сдачи!");
            return;
        }
        
        p.Quantity--;
        Console.WriteLine($"\nКуплено: {p.Name}!");
        
        if (change > 0) GiveChange(change);
        
        money = 0;
    }
    
    private bool CanGiveChange(int amount)
    {
        if (amount == 0) return true;
        
        List<Coin> temp = new List<Coin>();
        for (int i = 0; i < coins.Count; i++)
            temp.Add(new Coin(coins[i].Value, coins[i].Count));
        
        // bubble sort
        for (int i = 0; i < temp.Count; i++)
            for (int j = 0; j < temp.Count - 1; j++)
                if (temp[j].Value < temp[j + 1].Value)
                {
                    Coin t = temp[j];
                    temp[j] = temp[j + 1];
                    temp[j + 1] = t;
                }
        
        int rest = amount;
        for (int i = 0; i < temp.Count; i++)
        {
            while (rest >= temp[i].Value && temp[i].Count > 0)
            {
                rest -= temp[i].Value;
                temp[i].Count--;
            }
        }
        
        return rest == 0;
    }
    
    private void GiveChange(int amount)
    {
        Console.Write($"Сдача {amount}р: ");
        
        for (int i = 0; i < coins.Count; i++)
            for (int j = 0; j < coins.Count - 1; j++)
                if (coins[j].Value < coins[j + 1].Value)
                {
                    Coin t = coins[j];
                    coins[j] = coins[j + 1];
                    coins[j + 1] = t;
                }
        
        int rest = amount;
        for (int i = 0; i < coins.Count; i++)
        {
            while (rest >= coins[i].Value && coins[i].Count > 0)
            {
                Console.Write($"{coins[i].Value}р ");
                rest -= coins[i].Value;
                coins[i].Count--;
            }
        }
        Console.WriteLine();
    }
    
    public void ReturnMoney()
    {
        if (money == 0)
        {
            Console.WriteLine("Нет денег для возврата");
            return;
        }
        
        Console.WriteLine($"Возврат {money}р");
        GiveChange(money);
        money = 0;
    }
    
    public List<Product> GetProducts() { return products; }
    public List<Coin> GetCoins() { return coins; }
}
