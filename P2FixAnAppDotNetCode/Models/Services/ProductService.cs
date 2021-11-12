using P2FixAnAppDotNetCode.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// This class provides services to manages the products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all products from the inventory
        /// </summary>
        /// <returns>A list of all products</returns>
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts().ToList();
        }

        /// <summary>
        /// Get a product from the inventory by its id
        /// </summary>
        /// <param name="id">The id of the product to return</param>
        /// <returns>The product if it exists or null</returns>
        public Product GetProductById(int id) =>
            this.GetAllProducts().SingleOrDefault(p => p.Id == id);

        /// <summary>
        /// Update the quantities left for each product in the inventory depending on the ordered quantities
        /// </summary>
        /// <param name="cart">The cart to update product quantities for</param>
        /// <exception cref="ArgumentNullException">Thrown if cart is null</exception>
        public void UpdateProductQuantities(Cart cart)
        {
            if (cart is null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            foreach (var line in cart.Lines)
            {
                this._productRepository.UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }
    }
}