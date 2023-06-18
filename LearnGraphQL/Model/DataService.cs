using System;
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
        public string CustomerNameKeyWord { get; set; }
        public int? Pincode { get; set; }
    }
    public  class CustomerDb
    {
        public  List<Customer> SearchCustomer(SearchCustomer searchCustomer)
        {
            List<Customer> _customers = new List<Customer>() {
            new Customer { CustomerId=1, Address = "ABC", City="Delhi", CustomerName = "Atul Kumar", Pincode= 110091 },
            new Customer { CustomerId=2, Address="XYZ", City="Lucknow", CustomerName = "Shobha Rastogi", Pincode=226016 } };

            var result = new List<Customer>();
            if (searchCustomer.CustomerId.HasValue)
            {
               result.Add(_customers.FirstOrDefault(t => t.CustomerId == searchCustomer.CustomerId));
            }
            if(!string.IsNullOrEmpty(searchCustomer.CustomerNameKeyWord))
            {
                var search = _customers.Where(t => t.CustomerName.Contains(searchCustomer.CustomerNameKeyWord));
                result.AddRange(search);
            }
            if (searchCustomer.Pincode.HasValue &&  searchCustomer.Pincode > 0)
            {
                var search = _customers.Where(t => t.Pincode == searchCustomer.Pincode);
                result.AddRange(search);
            }
            return result;
        }

    }
}

