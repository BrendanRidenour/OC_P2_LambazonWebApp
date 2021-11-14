using System;
using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns>Returns the working cartline list</returns>
        private List<CartLine> GetCartLineList() => this._cartLineList;

        /// <summary>
        /// Saves the state of the working cartline list
        /// </summary>
        private readonly List<CartLine> _cartLineList = new List<CartLine>();

        /// <summary>
        /// Adds a product to the cart or increments the product's quantity if it already exists
        /// </summary>//
        /// <param name="product">The product to add to cart</param>
        /// <param name="quantity">The quantity to add to cart</param>
        /// <exception cref="ArgumentNullException">Thrown when the product argument is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the quantity is less than 0.</exception>
        public void AddItem(Product product, int quantity)
        {
            // Check product for null and throw if null
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // Check quantity for positive integer and throw if less than 0
            if (quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), message: $"The {nameof(quantity)} argument cannot be less than 0.");

            var cart = this.GetCartLineList();
            var existingItem = cart.SingleOrDefault(l => l.Product.Id == product.Id);

            // If product doesn't already exist in the list, add product to the list
            if (existingItem == null)
            {
                cart.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
            // If product already exists in the list, adjust the quantity
            else
            {
                existingItem.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        /// <param name="product">The product to remove</param>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        /// <returns>Returns the total value of all items in the cart</returns>
        public double GetTotalValue()
        {
            double total = 0;

            foreach (var line in this.GetCartLineList())
            {
                var lineTotal = line.Quantity * line.Product.Price;

                total += lineTotal;
            }

            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        /// <returns>Returns the average value of all items in the cart</returns>
        public double GetAverageValue()
        {
            var totalValue = this.GetTotalValue();

            var totalQuantity = 0;

            foreach (var line in this.GetCartLineList())
            {
                totalQuantity += line.Quantity;
            }

            return totalValue / totalQuantity;
        }

        /// <summary>
        /// Looks for a given product in the cart and returns it if found
        /// </summary>
        /// <param name="productId">The product id to return if found</param>
        /// <returns>The product if it exists or null</returns>
        public Product FindProductInCartLines(int productId)
        {
            var cart = this.GetCartLineList();

            return cart.SingleOrDefault(l => l.Product.Id == productId)?.Product;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        /// <param name="index">The index of the cartline to return</param>
        /// <returns>The cartline found at the given index</returns>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}