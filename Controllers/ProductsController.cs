using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week_1.Models;

namespace Week_1.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product(){Id=1,Name="abc",Price=100,UnitsInStock=5,Description="aaaaaaaaaaaaaaaaaaaaa"},
            new Product(){Id=2,Name="def",Price=90,UnitsInStock=6,Description="bb"},
            new Product(){Id=3,Name="ghi",Price=80,UnitsInStock=7,Description="ccccccc"},
            new Product(){Id=4,Name="jkl",Price=70,UnitsInStock=8,Description="dddddddddddd"},
            new Product(){Id=5,Name="mno",Price=60,UnitsInStock=9,Description="eeeeeeeeeeeee"},
            new Product(){Id=6,Name="pqr",Price=50,UnitsInStock=1,Description="fffffffffffff"},
            new Product(){Id=7,Name="stu",Price=40,UnitsInStock=2,Description="qqqqqqqqqqqqqq"},
            new Product(){Id=8,Name="vwx",Price=30,UnitsInStock=3,Description="eeeeeeeefffffffffffff"},
            new Product(){Id=9,Name="yz",Price=20,UnitsInStock=4,Description="mmmmmmmmmmmmmmmmm"}
        };

        [HttpGet]
        public IActionResult Get([FromQuery] string sortBy)
        {
            if (sortBy.Equals("+name"))
            {
                var newProducts = products.OrderBy(p => p.Name).ToList();
                return Ok(newProducts);
            }else if (sortBy.Equals("-name"))
            {
                var newProducts = products.OrderByDescending(p => p.Name).ToList();
                return Ok(newProducts);
            }
            else if (sortBy.Equals("+price"))
            {
                var newProducts = products.OrderBy(p => p.Price).ToList();
                return Ok(newProducts);
            }
            else if (sortBy.Equals("-price"))
            {
                var newProducts = products.OrderByDescending(p => p.Price).ToList();
                return Ok(newProducts);
            }else if (sortBy.Equals(""))
            {
                return Ok(products);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var getById = products.Where(p => id == p.Id);
            return Ok(getById);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductDto productDto)
        {
           
            var maxId = products.Max(p => p.Id);
            int isValid=0;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.Equals(productDto.Name))
                {
                    isValid++;
                    products[i].UnitsInStock++;
                    return Ok(products);
                } 
            }
            if (isValid==0)
               products.Add(new Product() { Id = maxId + 1, Name=productDto.Name, Price = productDto.Price, UnitsInStock=productDto.UnitsInStock,Description=productDto.Description});

            return Created("", products.Last());
        }

        [HttpPut("{id}")]
        public IActionResult Update( int id, [FromBody] ProductDto productDto)
        {
            var newProduct = products.FirstOrDefault(p => p.Id == id);
            if (newProduct==null)
            {
                return NotFound("Product not found please try another id");
            }
            newProduct.Name = productDto.Name;
            newProduct.Price = productDto.Price;
            newProduct.UnitsInStock = productDto.UnitsInStock;
            newProduct.Description = productDto.Description;

            return Ok(products);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            var newProduct = products.FirstOrDefault(p => p.Id == id);
            if (newProduct == null)
            {
                return NotFound("Product not found please try another id");
            }

            products.Remove(newProduct);

            return Ok(products);
        }
    }
}
