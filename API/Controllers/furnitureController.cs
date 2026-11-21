using API.Database;
using API.Models;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Furniture listings: browse, list an item for sale, mark it sold, or remove it.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class furnitureController : ControllerBase
    {
        // GET: api/furniture
        [HttpGet]
        public List<Furniture> Get()
        {
            IGetAllFurniture reader = new ReadFurnitureData();
            return reader.GetAllFurniture();
        }

        // GET: api/furniture/5
        [HttpGet("{id}", Name = "GetFurniture")]
        public Furniture Get(int id)
        {
            IGetFurniture reader = new ReadFurnitureData();
            return reader.GetFurniture(id);
        }

        // POST: api/furniture
        [HttpPost]
        public void Post([FromBody] Furniture value)
        {
            IPostFurniture writer = new PostFurniture();
            writer.PostFurniture(value);
        }

        // PUT: api/furniture/5 - updates the item's sold status
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Furniture value)
        {
            ISellFurniture writer = new SellFurniture();
            writer.ChangeSoldStatus(value);
        }

        // DELETE: api/furniture/5 - removes the listing and any orders for it
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteData writer = new DeleteData();
            writer.DeleteData(id);
        }
    }
}
