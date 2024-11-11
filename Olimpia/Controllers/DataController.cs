using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olimpia.Models;


namespace Olimpia.Controllers
{
    [Route("Data")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Data> Post(CreateDataDto createDataDto)
        {
            var data = new Data()
            {
                Id = Guid.NewGuid(),
                Country = createDataDto.Country,
                County = createDataDto.County,
                Descpription = createDataDto.Description,
                PlayerId = createDataDto.PlayerId,
            };

            if (data != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Datas.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public ActionResult<Data> Get()
        {
            using (var context = new OlimpiaContext())
            {
                return Ok(context.Datas.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Data> GetById(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var data = context.Datas.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
        }
        
    }
}
