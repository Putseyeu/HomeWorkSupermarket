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
            string userInput = Console.ReadLine();
            while (userInput != "Closse")
            {
                Console.WriteLine("Касса открыта и готова обслуживать клиента. Закрыть кассу - ввод команды Closse");
                Supermarket supermarket = new Supermarket();
                supermarket.CountTotal();
            }

            Console.WriteLine("Касса закрыта.");                      
            Console.ReadLine();
        }
    }
}

class Supermarket
{
    private Client _client = new Client();

    public void CountTotal()
    {
        int total = _client.GetTotalPrice();

        while (_client.Money < total)
        {
            Console.WriteLine($"К оплате {total} рублей. У клиента {_client.Money}");

            if (_client.Money < total)
            {
                Console.WriteLine("Клиенту не хватает денег. Убераем один товар из продуктовой тележки.");
                _client.DeleteProduct(_client);
                total = _client.GetTotalPrice();
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
    public List<Product> Products => _products;

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

    public void DeleteProduct(Client client)
    {
        List<Product> products = client.Products;
        int indexMin = 0;
        int indexMax = products.Count;

        int index = _random.Next(indexMin, indexMax);

        Console.WriteLine(index);
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
    int minPrice = 100;
    int maxPrice = 45000;

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
        _price = _random.Next(minPrice, maxPrice);
        Price = _price;
    }
}
