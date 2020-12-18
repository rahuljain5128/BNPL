using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BNPL.Entity;
using System;

namespace BNPL
{
    public class Logic
    {
        private static List<Product> inventory = new List<Product>();
        private static List<Customer> customers = new List<Customer>(){
            new Customer{
                Id = 1,
                Name ="customer",
                CredittLimit = 50000,
                Email = "customer@gmail.com"
            }
        };

        private static List<OrderHistory> orderHistory = new List<OrderHistory>();

        public bool Seed_Inventory(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException();
            }
            inventory.Add(product);
            return true;
        }

        public bool Buy(int productId,int custometId)
        {
            if(productId <= 0 || custometId <=0)
            {
                throw new ArgumentNullException("productId or customerId is less than zero");
            }
            if(inventory == null || inventory.Count == 0)
            {
                Console.WriteLine("there is no product in in inventory");
                return false;
            }
            var product = inventory.Where(x => x.Id == productId)?.ToList();
            if(product == null || product.Count() <= 0 || product[0].Count <=0)
            {
                Console.WriteLine("product is out of stock");
                return false;
            }
            return DoPayment(productId,custometId);
        }

        public bool DoPayment(int productId,int customerId,PaymentStatus payType = PaymentStatus.Prepaid)
        {
            var product = inventory.Where(x => x.Id == productId)?.ToList();
            var customer = customers.Where(x=> x.Id == customerId)?.ToList();
            if(product[0].Price <= customer[0].CredittLimit)
            {
                Console.WriteLine("you can opt BNPL payment also");
                payType = PaymentStatus.BNPL;
            }
            CreateOrderHistory(productId,customerId,payType);
            DecreaseProductCount(productId);
            if(payType == PaymentStatus.BNPL)
            {
                customer[0].CredittLimit = customer[0].CredittLimit - product[0].Price;
            }
            return true;
        }

        public void GetStatus()
        {
            Console.WriteLine("-----inventory status-----");
            if(inventory == null || inventory.Count == 0)
            {
                Console.WriteLine("inventory is null or empty");
            }
            foreach(var product in inventory)
            {
                Console.WriteLine("Id = {0},Name={1},Count={2},Price={3}",product.Id,product.Name,product.Count,product.Price);
            }
            
            Console.WriteLine("-----order history-----");
            if(orderHistory == null || orderHistory.Count == 0)
            {
                Console.WriteLine("orderHistory is null or empty");
            }
            foreach(var order in orderHistory)
            {
                Console.WriteLine("ProducId = {0},CustomerId={1},Status={2},PaymentType={3}",order.ProductId,order.CustomerId,order.Status,order.PaymentStatus);
            }
            return;
        }
        private void DecreaseProductCount(int productId)
        {
            var product = inventory.Where(x => x.Id == productId)?.ToList();
            product[0].Count = product[0].Count - 1;
        }
        private void CreateOrderHistory(int productId,int custometId,PaymentStatus payType)
        {
            orderHistory.Add(new OrderHistory
            {
               CustomerId = custometId,
               ProductId = productId,
               PaymentStatus = payType,
               Status = Entity.Status.Placed,
               DateTime = DateTime.Now
            });
        }
    }
}
