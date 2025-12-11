using System;

class Program
{
    static void Main()
    {
        Console.WriteLine(" ВЕНДИНГОВЫЙ АВТОМАТ\n");
        
        VendingMachine machine = new VendingMachine();
        
        while (true)
        {
            Console.WriteLine("\nМЕНЮ");
            Console.WriteLine("1. Товары");
            Console.WriteLine("2. Внести деньги");
            Console.WriteLine("3. Купить");
            Console.WriteLine("4. Возврат");
            Console.WriteLine("5. Админ");
            Console.WriteLine("0. Выход");
            Console.Write(">> ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                machine.ShowProducts();
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nМонеты: 1,2,5,10,50,100р (0-выход)");
                while (true)
                {
                    Console.Write("Монета: ");
                    string input = Console.ReadLine();
                    if (input == "" || input == null) break;
                    int coin = int.Parse(input);
                    if (coin == 0) break;
                    machine.InsertMoney(coin);
                }
            }
            else if (choice == "3")
            {
                machine.ShowProducts();
                Console.Write("\nНомер товара: ");
                string input = Console.ReadLine();
                if (input != "" && input != null)
                {
                    int num = int.Parse(input);
                    machine.BuyProduct(num);
                }
            }
            else if (choice == "4")
            {
                machine.ReturnMoney();
            }
            else if (choice == "5")
            {
                Admin admin = new Admin(machine);
                if (admin.Login()) admin.Menu();
            }
            else if (choice == "0")
            {
                Console.WriteLine("До свидания!");
                break;
            }
            
            Console.WriteLine("\n[Enter...]");
            Console.ReadLine();
        }
    }
}
