
using Catalog.infrastructure.Interface;
using CatalogAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
        {
            private readonly ICatalog _Context;
            public ProductController(ICatalog Context)
            {
                _Context = Context;
            }

            [HttpGet]
            [Route("{id}")]
            public async Task<IActionResult> GetAsync(int id)
            {
                var product = await _Context.GetItemById(id);
                return Ok(product);
            }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Products newProduct)
        {
            try
            {

                var res = await _Context.AddItem(newProduct);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  

            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Products updatedItem)
        {
            var res = await _Context.UpdateItem(updatedItem);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Products> products = await _Context.GetAllItems();
            return Ok(products);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //if (id < 1)
            //    return BadRequest();
            //var product =  await _Context.GetItemById(id);
            //if (product == null)
            //    return NotFound();
            //_Context.DeleteItem(product);
            //return Ok();

            var res = await _Context.DeleteItem(id);
            return Ok(res);

        }

       

    }
    }

