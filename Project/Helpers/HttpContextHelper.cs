using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Helpers
{
    public static class HttpContextHelper
    {
        static string productsKey = "ProductsICart";
        static string UsernameKey = "username";

        public static string GetUsername(this HttpContext context)
        {
            return context.Request.Cookies[UsernameKey];
        }

        public static void SetUserName(this HttpContext context, string username)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10);
            context.Response.Cookies.Append(UsernameKey, username, option);
        }

        public static void DeleteUsername(this HttpContext context)
        {
            context.Response.Cookies.Delete(UsernameKey);

        }

        public static List<int> GetProductsInCart(this HttpContext context)
        {
            return context.Request.Cookies.GetProductListId(productsKey);
        }

        public static List<int> AddProductToCart(this HttpContext context, ProductModel product)
        {
            var cartList = context.Request.Cookies.GetProductListId(productsKey) ?? new List<int>();
            cartList.Add(product.Id);
            context.Response.Cookies.SetProductListId(productsKey, cartList);
            return cartList;
        }

        public static void DeleteProductsInCart(this HttpContext context)
        {
          context.Response.Cookies.Delete(productsKey);
          
        }

        public static void RemoveProductFromCart(this HttpContext context,int productId)
        {
            var list = context.GetProductsInCart();
            list.Remove(productId);
            context.Response.Cookies.SetProductListId(productsKey, list);          
        }

        public static void RemoveProductsFromCart(this HttpContext context,List<int> productList)
        {
            if (productList == null) return;
            var list = context.GetProductsInCart();
            foreach (var productId in productList)
                list.Remove(productId);
            context.Response.Cookies.SetProductListId(productsKey,list);          
        }
   
        public static bool CheckIfUserLogin(this HttpContext context)
        {
            return context.GetUsername() != null;
        }     

    }
}
