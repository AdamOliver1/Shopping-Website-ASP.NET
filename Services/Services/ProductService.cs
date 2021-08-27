using Dal;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService : IProductService
    {
        MyContext _context;
        int _maxNumOfDaysInCart = 10;

        public ProductService(MyContext contaxt)
        {
            _context = contaxt;
        }

        public void AddProduct(ProductModel product, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            _context.Products.Add(product);
            product.Owner = user;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ProductModel>> GetAllUnperchasedAsync()
        {
            return await Task.Run(GetAllUnperchased);
        }

        private IEnumerable<ProductModel> GetAllUnperchased()
        {
            return _context.Products.Where(p => p.State == State.UnPerchased);
        }

        public async Task<IEnumerable<ProductModel>> GetAllOrderedByDateAsync()
        {
            var list = GetAllUnperchasedAsync().Result;
            var res = await Task.Run(() => list.OrderByDescending(p => p.Date).ToList());
            return res;
        }

        public async Task<IEnumerable<ProductModel>> GetAllOrderedByTitleAsync()
        {
            return await Task.Run(() => GetAllUnperchasedAsync().Result.OrderBy(p => p.Title));
        }

        public IEnumerable<ProductModel> GetCart(string UserName)
        {
            return _context.Products.Where(p => p.User.UserName == UserName && p.State == State.InCart);
        }

        public ProductModel GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveFromCart(int id)
        {
            ProductModel product = GetProductById(id);
            if (product != null)
            {
                product.State = State.UnPerchased;
                product.User = default(UserModel);
                product.UserId = null;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }

        public void AddToCart(int productId, string username = null)
        {
            ProductModel product = GetProductById(productId);
            if (product != null)
            {
                product.State = State.InCart;
                product.LastModified = DateTime.Now;
                if (username != null)
                    product.User = _context.Users.FirstOrDefault(u => u.UserName == username);
                _context.Products.Update(product);
                _context.SaveChanges();
            }

        }

        public void BuyCartUser(string username)
        {
            foreach (var item in _context.Products.Where(p => p.User != null && p.User.UserName == username))
                item.State = State.Perchased;
            _context.SaveChanges();
        }

        public void BuyCartGuest(List<int> products)
        {
            if (products == null && products.Count < 1) return;
            foreach (var item in _context.Products.Where(p => products.Contains(p.Id)))
                item.State = State.Perchased;
            _context.SaveChanges();
        }

        public async Task CheckLastModifiedAsync()
        {
            var productsToRemoveList = new List<ProductModel>();
            await Task.Run(() =>
            {

                foreach (var item in _context.Products.Where(p => p.State == State.InCart))
                {
                    if (item.LastModified != default && item.LastModified + TimeSpan.FromDays(10) < DateTime.Now)
                        productsToRemoveList.Add(item);
                }


                GetOutFromCart(productsToRemoveList);
            });

        }

        public async Task<List<int>> CheckLastModifiedGuestAsync(List<int> productId)
        {
            var productsToRemoveList = new List<ProductModel>();
            var productsToRemoveListIds = new List<int>();
            await Task.Run(() =>
            {
                foreach (var item in _context.Products.Where(p => productId.Contains(p.Id)))
                {
                    if (item.LastModified != default && item.LastModified + TimeSpan.FromDays(_maxNumOfDaysInCart) < DateTime.Now)
                    //if (item.LastModified != default && item.LastModified + TimeSpan.FromSeconds(1) < DateTime.Now)
                    {
                        productsToRemoveList.Add(item);
                        productsToRemoveListIds.Add(item.Id);
                    }
                }
                GetOutFromCart(productsToRemoveList);
            });
            return productsToRemoveListIds;
        }

        private void GetOutFromCart(List<ProductModel> list)
        {
            foreach (var item in list)
            {
                item.State = State.UnPerchased;
                item.User = null;
            }
            _context.SaveChanges();
        }

    }
}
