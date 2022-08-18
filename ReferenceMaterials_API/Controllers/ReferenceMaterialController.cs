using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReferenceMaterials_API.Models;
using ReferenceMaterials_API.Data;
using ReferenceMaterials_API.Persistence;

namespace ReferenceMaterials_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public partial class ReferenceMaterialController : ControllerBase
    {
        //creating a dbcontext for injection (using in memory database):
        /*private readonly APIContext _context;

        public ReferenceMaterialController (APIContext context)
        {
            //assigning to our private variable:
            _context = context; 
        }
        */

        //for using ravendb use following injection:
        private readonly IRepository<Material> _repository;//providing our model to our generic repository
        public ReferenceMaterialController(IRepository <Material> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public IActionResult Get (string materialId)
        {
            var material = _repository.Get (materialId);//it will automatically find the element by id primary key


            return Ok (material);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post (Material material)
        {
            try
            { 
            //for normal create object
            _repository.InsertOrUpdate(material);
            }catch(Exception ex)
            {
                return StatusCode(500);
            }

            return Ok ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _repository.GetAll();
            return Ok (result);
        }

        //for in memory database access:
        /*
        //Creating or editing (a post request)
        [HttpPost] 
        public JsonResult CreateEdit(Material material)
        {
            //for normal create object
            if(material.Id == 0)
            {
                _context.Materials.Add(material);
            }else
            {
                //in case you provided an id to search
                //checking if the material already exists
                var materialInDb = _context.Materials.Find(material.Id);
                
                //if there is no material we could find
                if (materialInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                //if the id was found we edit the data object in the database
                materialInDb = material;
            }

            _context.SaveChanges();//save your database context
            return new JsonResult(material);


        }

        //Get by id
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Materials.Find(id);

            //if there is no material we could find
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(result);
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Materials.Find(id);

            //if there is no material we could find
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            _context.Materials.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }

        //Get all
        //[HttpGet("/materials")]
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Materials.ToList();
            return new JsonResult(result);
        }
        */
    }

}
