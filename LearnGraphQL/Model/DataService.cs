using System;
using HotChocolate.Language;
using HotChocolate.Resolvers;

namespace LearnGraphQL.Model
{
	public class Customer
	{
		public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Pincode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
    public class SearchCustomer
    {
        public int? CustomerId { get; set; }
        public string? CustomerNameKeyWord { get; set; }
        public int? Pincode { get; set; }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public class SearchOrder
    {
        public int? OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public DateTime? OrderDate { get; set; }
    }

    public class CustomerDb
    {
        //[UsePaging]
        public  IQueryable<Customer> Customers(SearchCustomer? searchCustomer)
        {


            var result = new List<Customer>();
            if (searchCustomer == null)
            {
                return AirData.Customers.AsQueryable();
            }
            else
            {
                if (searchCustomer.CustomerId.HasValue)
                {
                    result.Add(AirData.Customers.FirstOrDefault(t => t.CustomerId == searchCustomer.CustomerId));
                }
                if (!string.IsNullOrEmpty(searchCustomer.CustomerNameKeyWord))
                {
                    var search = AirData.Customers.Where(t => t.CustomerName.Contains(searchCustomer.CustomerNameKeyWord));
                    result.AddRange(search);
                }
                if (searchCustomer.Pincode.HasValue && searchCustomer.Pincode > 0)
                {
                    var search = AirData.Customers.Where(t => t.Pincode == searchCustomer.Pincode);
                    result.AddRange(search);
                }
            }
            return result.AsQueryable();
        }
        public IQueryable<Order> Orders(SearchOrder? searchOrder)
        {
            var result = new List<Order>();
            if (searchOrder == null)
            {
                return AirData.Orders.AsQueryable();
            }
            else
            {
                if (searchOrder.CustomerId.HasValue && searchOrder.CustomerId.Value > 0)
                {
                    result.AddRange(AirData.Orders.Where(t => t.CustomerId == searchOrder.CustomerId).ToList());
                }
                if (!string.IsNullOrEmpty(searchOrder.ProductName))
                {
                    var search = AirData.Orders.Where(t => t.ProductName.Contains(searchOrder.ProductName));
                    result.AddRange(search);
                }
            }
            return result.AsQueryable();
        }

    }
    public static class AirData
    {
        private static List<Customer> _customers = null;
        private static List<Order> _orders = null;

        public static List<Customer> Customers
        {
            get
            {
                if(_customers==null)
                {
                    _customers = DummyDataGenerator.GenerateDummyCustomers(100);
                }
                return _customers;
            }
        }
        public static List<Order> Orders
        {
            get
            {
                if (_orders == null)
                {
                    _orders = DummyOrderDataGenerator.GenerateDummyOrders(1000);
                }
                return _orders;
            }
        }

    }

    public class DummyDataGenerator
    {
        private static readonly Random random = new Random();

        private static string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };
        private static string[] streetNames = { "Main St", "Oak St", "Maple Ave", "Cedar Ln", "Elm St", "Pine Rd", "Walnut St", "Birch Ave", "Hickory Ln", "Willow Rd" };

        public static List<Customer> GenerateDummyCustomers(int count)
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 1; i <= count; i++)
            {
                string customerName = GetRandomCustomerName();
                string address = GetRandomAddress();
                string city = GetRandomCity();

                Customer customer = new Customer
                {
                    CustomerId = i,
                    CustomerName = customerName,
                    Pincode = GetRandomPincode(),
                    Address = address,
                    City = city
                };

                customers.Add(customer);
            }

            return customers;
        }

        private static string GetRandomCustomerName()
        {
            string[] names = { "John", "Jane", "Robert", "Susan", "William", "Karen", "Joseph", "Jennifer", "Thomas", "Jessica", "Michael", "Lisa", "David", "Mary", "James", "Linda", "Andrew", "Emily", "Daniel", "Olivia" };
            string[] surnames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Lee" };

            string randomName = names[random.Next(names.Length)];
            string randomSurname = surnames[random.Next(surnames.Length)];

            return $"{randomName} {randomSurname}";
        }

        private static int GetRandomPincode()
        {
            return random.Next(10000, 99999);
        }

        private static string GetRandomAddress()
        {
            int streetNumber = random.Next(1, 100);
            string streetName = streetNames[random.Next(streetNames.Length)];

            return $"{streetNumber} {streetName}";
        }

        private static string GetRandomCity()
        {
            return cities[random.Next(cities.Length)];
        }
    }
    public class DummyOrderDataGenerator
    {
        private static readonly Random random = new Random();

        private static string[] productNames = { "Widget", "Gadget", "Thingamajig", "Doohickey", "Contraption", "Device", "Apparatus", "Invention", "Tool", "Equipment" };

        public static List<Order> GenerateDummyOrders(int count)
        {
            List<Order> orders = new List<Order>();
            for (int i = 1; i <= count; i++)
            {
                Order order = new Order
                {
                    OrderId = i,
                    CustomerId = GetCustomerId(),
                    ProductName = GetRandomProductName(),
                    Price = GetRandomPrice(),
                    OrderDate = GetRandomOrderDate()
                };

                orders.Add(order);
            }

            return orders;
        }

        private static string GetRandomProductName()
        {
            return productNames[random.Next(productNames.Length)];
        }

        private static decimal GetRandomPrice()
        {
            return (decimal)Math.Round(random.NextDouble() * 100, 2);
        }
        private static int GetCustomerId()
        {
            return (int)random.NextInt64(1, 1000);
        }
        private static DateTime GetRandomOrderDate()
        {
            DateTime startDate = new DateTime(2021, 1, 1);
            DateTime endDate = new DateTime(2021, 12, 31);

            int range = (endDate - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }

        
    }
}

