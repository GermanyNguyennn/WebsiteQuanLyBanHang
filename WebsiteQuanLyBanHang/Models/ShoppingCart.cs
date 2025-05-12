using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> items { get; set; }
        public ShoppingCart()
        {
            this.items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var checkCart = items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkCart != null)
            {
                checkCart.Quantity += quantity;
                checkCart.TotalPrice = checkCart.Price * checkCart.Quantity;
            }
            else
            {
                items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkCart = items.SingleOrDefault(x => x.ProductId == id);
            if (checkCart != null)
            {
                items.Remove(checkCart);
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkCart = items.SingleOrDefault(x => x.ProductId == id);
            if (checkCart != null)
            {
                checkCart.Quantity = quantity;
                checkCart.TotalPrice = checkCart.Price * checkCart.Quantity;
            }
        }

        public decimal GetTotalPrice()
        {
            return items.Sum(x => x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }

    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
