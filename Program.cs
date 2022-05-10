using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkSupermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            string userInput = "";
            Console.WriteLine("Свободная касса.");
            Supermarket supermarket = new Supermarket();

            while (userInput != "Closse")
            {
                Console.WriteLine("Касса открыта и готова обслуживать клиента команда  (Next). Закрыть кассу - ввод команды (Closse)");
                userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "Next":
                        supermarket.ServeClient();
                        break;

                    case "Closse":
                        break;
                }
            }

            Console.WriteLine("Касса закрыта.");                      
            Console.ReadLine();
        }
    }
}

class Supermarket
{
    private List<Client> _clients = new List<Client>();

    public void ServeClient()
    {
        _clients.Add(new Client());
        CountTotal();
    }

    private void CountTotal()
    {
        Client client = _clients.Last();
        int total = client.GetTotalPrice();

        while (client.Money < total)
        {
            Console.WriteLine($"К оплате {total} рублей. У клиента {client.Money}");

            if (client.Money < total)
            {
                Console.WriteLine("Клиенту не хватает денег. Убераем один товар из продуктовой тележки.");
                client.DeleteProduct();
                total = client.GetTotalPrice();
            }     
        }

        if (total == 0)
        {
            Console.WriteLine("Клиенту не хватило денег на покупки.");
        }
        else
        {
            Console.WriteLine("Товар оплачен.");
        }
    }
}

class Client
{
    private List<Product> _products = new List<Product>();
    private int _money;
    private Random _random = new Random();

    public int Money => _money;

    public Client()
    {
        GreatNumberMoney();
        GreatListProduct();
    }

    public void GreatNumberMoney()
    {
        int minMoney = 1000;
        int maxMoney = 50000;

        _money = _random.Next(minMoney, maxMoney);
    }

    public int GetTotalPrice()
    {
        int total = 0;
        for (int i = 0; i < _products.Count; i++)
        {
            Console.WriteLine($"Продукт {_products[i].Title} Цена {_products[i].Price} ");
            total += _products[i].Price;
        }

        return total;
    }

    public void GreatListProduct()
    {
        int minNumber = 1;
        int maxNumber = 6;
        int indexProduct = _random.Next(minNumber, maxNumber);
        
        for (int i = 0; i < indexProduct; i++)
        {
            _products.Add(new Product());
        }
    }

    public void DeleteProduct()
    {
        List<Product> products = _products;
        int indexMin = 0;
        int indexMax = products.Count;
        int index = _random.Next(indexMin, indexMax);

        if (indexMax == 0)
        {
            products.RemoveAt(indexMin);
        }
        else
        {
            products.RemoveAt(index);
        }
    }
}

class Product
{
    private string _title = "";
    private int  _price;
    private Random _random = new Random();
    private int  _minPrice = 100;
    private int _maxPrice = 45000;

    public string Title => _title;
    public int Price { get; private set; }

    public Product()
    {
        GreatTitle();
        GreatPrice();   
    }

    public void GreatTitle()
    {
        Console.WriteLine("Что хотите купить ?");
        _title = Console.ReadLine(); 
    }

    public void GreatPrice()
    {
        _price = _random.Next(_minPrice, _maxPrice);
        Price = _price;
    }
}
