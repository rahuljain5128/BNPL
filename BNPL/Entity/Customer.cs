﻿using System;

namespace BNPL.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public double CredittLimit { get; set; }
    }
}
