using Application.Common.Help;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Product;
using MediatR;
using Application.ProductLogic.Queries;
using Application.ProductLogic.Commands.CreateProduct;
using Application.ProductLogic.Commands.UpdateProduct;
using Application.ProductLogic.Commands.DeleteProduct;
using Application.ProductLogic.Commands.BuyProduct;
using Application.Common.Models.MediatorModels;

using Microsoft.AspNetCore.Authorization;
namespace Shop.Controllers.ProductsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet,Authorize(Roles = "Admin")]
        public async Task<List<ProductModel>> Products()
        {
            var token = Request.Cookies["token"];
            return await _mediator.Send(new GetProductQuery(token));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("Create")]
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            string cookie = Request.Cookies["token"];
            
            _ProductModel _product = new _ProductModel();
            _product.Product = product;
            _product.Token   = cookie;
            
            Console.WriteLine(_product.Token);
            
             

            var createProductModel = new CreateProductCommand(_product);

            return await _mediator.Send(createProductModel);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Route("Update")]
        public async Task<ProductModel> UpdateProduct( ProductModel product)
        {
            string cookie = Request.Cookies["token"];

            _ProductModel _product = new _ProductModel();
            _product.Product = product;
            _product.Token = cookie;

            var updateProductModel = new UpdateProductCommand(_product);

            return await _mediator.Send(updateProductModel);
            
        }
        [HttpDelete, Authorize(Roles = "Admin")]
        [Route("delete/{id:int}")]
        public async Task<ProductModel> DeleteProduct(int id)
        {
            var token = Request.Cookies["token"];
            return await _mediator.Send(new DeleteProductCommand(id,token));
        }

        [HttpPost,Authorize(Roles = "User")]
        [Route("buy/{id:int}")]
        public async Task<Response<string>> BUYProduct(int id)
        {
            var token = Request.Cookies["token"];
            return await _mediator.Send(new BuyProductCommand(id, token));
        }


    }
}
