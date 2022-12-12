﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineShoppingCart.Models
{
    public partial class Product
    {
        public Product()
        {
            CartDetails = new HashSet<CartDetails>();
            ProductReview = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public int ProductCategoriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductPic { get; set; }
        public string ActivationCodes { get; set; }
        public string ShowtoSale { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<CartDetails> CartDetails { get; set; }
        public virtual ICollection<ProductReview> ProductReview { get; set; }
    }
}