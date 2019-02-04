
using Application.Abstracts;
using Application.Base;
using Domain.Abstracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements
{
    public class ProductService : EntityService<Product>, IProductService
    {


        readonly IUnitOfWork _unitOfWork;
        readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
            : base(unitOfWork, productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }


        public CrearProductResponse NuevoProduct(CrearProductRequest requets)
        {

            Product product = _productRepository.FindBy(t => t.Title.Equals(requets.Title)).FirstOrDefault();
            if (product == null)
            {

                Product newProduct = new Product();

                newProduct.Title = requets.Title;
                newProduct.Description = requets.Description;
                newProduct.Quantity = requets.Quantity;
                newProduct.PriceToBuy = requets.PriceToBuy;

                _productRepository.Add(newProduct);
                _unitOfWork.Commit();
                return new CrearProductResponse()
                {
                    Mensaje = $"El Product fue registrado correctamente"
                };
            }
            else
            {

                return new CrearProductResponse()
                {
                    Mensaje = $"El Product con este titulo  ya existe"
                };
            }

        }

    }

    public class CrearProductRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double PriceToBuy { get; set; }
    }
    public class CrearProductResponse
    {
        public string Mensaje { get; set; }
    }
}

