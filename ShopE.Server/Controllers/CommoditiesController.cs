using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopE.Server.Models;

namespace ShopE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommoditiesController : ControllerBase
    {
        private readonly ShopEContext _context;

        public CommoditiesController(ShopEContext context)
        {
            _context = context;
        }

        // GET: api/Commodities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodities()
        {
            try
            {
                var commodities = await _context.Commodities.Include(c=>c.Store).AsNoTracking().ToListAsync();
                if (commodities.Count != 0)
                {
                    foreach (var item in commodities)
                    {
                        var host = HttpContext.Request.Host.Value;
                        item.Photo = $"/api/Commodities/GetPhoto/{item.Photo}";
                    }
                    return Ok(commodities);

                }
                else
                {
                    return BadRequest("کالایی وجود ندارد");
                }
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        // GET: api/Commodities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commodity>> GetCommodity(int id)
        {
            try
            {
                var commodity = await _context.Commodities.SingleOrDefaultAsync(c => c.Id == id);
                if (commodity != null)
                {

                    var host = HttpContext.Request.Host.Value;
                    commodity.Photo = $"{host}/api/Commodity/GetPhoto/{commodity.Photo}";
                    return Ok(commodity);
                }
                else
                {
                    return BadRequest("کالایی یافت نشد");
                }
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        // GET: api/Commodities/Search/5
        [HttpGet("Search/{id}")]
        public async Task<IActionResult> SearchCommodity(string id)
        {
            try
            {
                var commodities = await _context.Commodities.Where(c => c.Name.Contains(id.Trim())).ToListAsync();
                if (commodities != null)
                {

                    foreach (var item in commodities)
                    {
                        var host = HttpContext.Request.Host.Value;
                        item.Photo = $"{host}/api/Commodity/GetPhoto/{item.Photo}";
                    }

                    return Ok(commodities);
                }
                else
                {
                    return BadRequest("کالایی یافت نشد");
                }
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");

            }
        }

        // POST: api/Commodities
        [HttpPost]
        public async Task<ActionResult<Commodity>> PostCommodity(Commodity commodity)
        {
            try
            {
                var store = await _context.Stores.SingleOrDefaultAsync(s => s.StoreId == commodity.StoreId);
                if (store != null)
                {
                    var host = HttpContext.Request.Host.Value;
                    byte[] Byte = Convert.FromBase64String(commodity.Photo);
                    var filename = $"{Guid.NewGuid().ToString().Replace("-", "")}.jpg";
                    var FilePath = Path.Combine($"{Directory.GetCurrentDirectory()}/wwwroot/Images/{filename}");
                    await System.IO.File.WriteAllBytesAsync(FilePath, Byte);

                    Commodity c = new Commodity()
                    {
                        Name = commodity.Name,
                        Count = commodity.Count,
                        Price = commodity.Price,
                        Photo = $"{filename}",
                        StoreId = store.StoreId,
                        Off = commodity.Off,
                        TotalPrice = (commodity.Price) - (int) ((float) (commodity.Price) * (commodity.Off / 100))
                    };

                    await _context.Commodities.AddAsync(c);
                    await _context.SaveChangesAsync();
                    return Ok("با موفقیت ثبت شد");
                }
                else
                {
                    return NotFound("فروشگاهی یافت نشد");
                }
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        // DELETE: api/Commodities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commodity>> DeleteCommodity(int id)
        {
            var commodity = await _context.Commodities.FindAsync(id);
            if (commodity == null)
            {
                return NotFound();
            }

            _context.Commodities.Remove(commodity);
            await _context.SaveChangesAsync();

            return commodity;
        }

        // DELETE: api/Payment
        [Route("Payment")]
        [HttpPost]
        public async Task<ActionResult> PostPyment([FromBody]Payment payment)
        {
            try
            {

                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return Ok(payment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetPhoto/{id}")]
        public async Task<IActionResult> GetPhoto(string id)
        {
            var path = $"{Directory.GetCurrentDirectory()}/wwwroot/Images/{id}";
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return new FileStreamResult(memory, "image/jpeg");
        }
    }
}
