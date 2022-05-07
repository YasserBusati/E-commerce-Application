using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using Api.Dtos;
using AutoMapper;

namespace Api.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;


        }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productRepo.ListAsync(spec);
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productRepo.GetEntityWithSpec(spec);
        return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductbrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }
}
}