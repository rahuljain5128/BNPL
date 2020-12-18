using System;
using BNPL.Entity;

namespace BNPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            logic.Seed_Inventory(new Product {Id = 1, Count = 1, Name ="TV" ,Price = 20000.00});
            logic.GetStatus();
            logic.Buy(1,1);
            logic.GetStatus();
            //logic.Buy(1,1);
        }
    }
}
