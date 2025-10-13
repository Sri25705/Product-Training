using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Ecom
{
    public class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;

        public Product(int id, string name, double price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void Display()
        {
            Console.WriteLine($"{Id,-3} {Name,-20} {Price,-8} {Quantity}");
        }
    }

    class Cartitem
    {
        public Product Product;
        public int Quantity;

        public Cartitem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public double Subtotal()
        {
            return Product.Price * Quantity;
        }
    }
    class Shoppingcart
    {
        public List<Cartitem> Cartitems = new List<Cartitem>();
        public void Addproduct(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("no stock");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine("Not enough stock");
                return;
            }
            bool found = false;
            foreach (Cartitem item in Cartitems)
            {
                if (item.Product.Id == product.Id)
                {
                    item.Quantity += quantity;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Cartitems.Add(new Cartitem(product, quantity));
            }
            product.Quantity -= quantity;
            Console.WriteLine($"{quantity}X{product.Name} added to cart");
        }
        public void RemoveProduct(int productId)
        {
            for (int i = 0; i < Cartitems.Count; i++)
            {
                if (Cartitems[i].Product.Id == productId)
                {
                    Cartitems[i].Product.Quantity += Cartitems[i].Quantity;
                    Cartitems.RemoveAt(i);
                    Console.WriteLine("Product removed from cart");
                    return;
                }
            }
            Console.WriteLine("Product not found");
        }
        public void Displaycart()
        {
            if (Cartitems.Count == 0)
            {
                Console.WriteLine("Your cart is empty");
                return;
            }
            double total = 0;
            Console.WriteLine("\n your cart");

            foreach (Cartitem item in Cartitems)
            {
                double subtotal = item.Subtotal();
                Console.WriteLine($"{item.Product.Name,-20}x {item.Quantity,-3} {item.Product.Price,-8} subtotal: {subtotal}");
                total += subtotal;
            }
            Console.WriteLine($"Total:{total}");
        }
        public double Calculatetotal()
        {
            double total = 0;
            foreach (Cartitem item in Cartitems)
            {
                total += item.Subtotal();
            }
            return total;
        }
        public void Checkout()
        {
            if (Cartitems.Count == 0)
            {
                Console.WriteLine("Cartitems is empty,Addproduct product");
                return;
            }
            double total = Calculatetotal();
            Console.WriteLine($"Total amount:{total}");
            Console.WriteLine("Proceed to checkout(yes/no)");
            string confirm = Console.ReadLine().ToLower();

            if (confirm == "yes")
            {
                Console.WriteLine("Payment Successfull");
                Cartitems.Clear();
            }
            else
            {
                Console.WriteLine("Cancelled");
            }
        }
    }
    class Ecommerce
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>
            {
                new Product(1,"Pen",20,5),
                new Product(2,"Pencil",10,5),
                new Product(3,"Eraser",5,10),
            };
            Shoppingcart cart = new Shoppingcart();
            bool exit = false;

            while (!exit)
            {
                try
                {

                    Console.WriteLine("\n E commerce Menu");
                    Console.WriteLine("1.View Product");
                    Console.WriteLine("2.Add to cart");
                    Console.WriteLine("3.View Cart");
                    Console.WriteLine("4.Remove from Cart");
                    Console.WriteLine("5.Checkout");
                    Console.WriteLine("6.Exit");
                    Console.WriteLine("choose an option");

                    string input = Console.ReadLine();
                    int choice;

                    if (!int.TryParse(input, out choice))
                    {
                        Console.WriteLine("Invalid,Please Enter a number ");
                        continue;

                    }

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Available Products:");
                            foreach (Product p in products)
                            {
                                p.Display();
                            }
                            break;

                        case 2:
                            Console.WriteLine("Enter Product Id:");
                            if (!int.TryParse(Console.ReadLine(), out int addId))
                            {
                                Console.WriteLine("Invalid Id");
                                break;
                            }

                            Product selectedProduct = null;
                            foreach (Product p in products)
                            {
                                if (p.Id == addId)
                                {
                                    selectedProduct = p;
                                    break;
                                }
                            }
                            if (selectedProduct == null)
                            {
                                Console.WriteLine("product not found");
                                break;
                            }

                            Console.WriteLine("Enter quantity:");
                            if (!int.TryParse(Console.ReadLine(), out int qty))
                            {
                                Console.WriteLine("Invalid quantity");
                                break;
                            }
                            cart.Addproduct(selectedProduct, qty);
                            break;
                        case 3:
                            cart.Displaycart();
                            break;

                        case 4:
                            Console.Write("Enter product id to remove:");
                            if (!int.TryParse(Console.ReadLine(), out int remId))
                            {
                                Console.WriteLine("Invalid Id");
                                break;
                            }
                            cart.RemoveProduct(remId);
                            break;
                        case 5:
                            cart.Checkout();
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error:{ex.Message}");
                }
                if (!exit)
                {
                    Console.WriteLine("\n press enter to return menu");
                    Console.ReadLine();
                }
            }
        }
    }
}