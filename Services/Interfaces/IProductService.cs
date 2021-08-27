using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllOrderedByDateAsync();
        void AddProduct(ProductModel product, string username);
        Task<IEnumerable<ProductModel>> GetAllOrderedByTitleAsync();
        Task<IEnumerable<ProductModel>> GetAllUnperchasedAsync();
        Task<List<int>> CheckLastModifiedGuestAsync(List<int> productId);
        ProductModel GetProductById(int id);
        IEnumerable<ProductModel> GetCart(string UserName);
        void AddToCart(int productId, string username = null);
        void BuyCartUser(string username);
        void BuyCartGuest(List<int> products);
        void RemoveFromCart(int id);
         Task CheckLastModifiedAsync();


    }
}
