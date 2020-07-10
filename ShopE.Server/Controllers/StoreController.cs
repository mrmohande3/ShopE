using System;
using System.Collections.Generic;
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
    public class StoreController : ControllerBase
    {
        private ShopEContext _context;

        public StoreController(ShopEContext context)
        {
            _context = context;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            try
            {
                var stores = await _context.Stores.AsNoTracking().ToListAsync();
                return Ok(stores);
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore([FromRoute]int id)
        {
            try
            {
                var store = _context.Stores.Where(s => s.StoreId == id)
                    .Include(s=>s.Commodities)
                    .ThenInclude(c=>c.Payments)
                    .FirstOrDefault();
                if (store != null)
                {
                    List<Payment> payments = new List<Payment>();
                    foreach (var commodity in store.Commodities)
                    {
                        var host = HttpContext.Request.Host.Value;
                        commodity.Photo = $"/api/Commodities/GetPhoto/{commodity.Photo}";
                        payments.AddRange(commodity.Payments);
                    }

                    var profile = new
                    {
                        Name = store.Name,
                        PhoneNumber = store.PhoneNumber,
                        Commodities = store.Commodities,
                        PaymentTotal = payments.Sum(p => p.TotalPrice),
                        Payments = payments,
                        PaymentCount = payments.Count
                    };
                    return Ok(profile);
                }
                else
                {
                    return BadRequest("فروشگاه پیدا نشد");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("Commodities/{id}")]
        public async Task<IActionResult> GetAllCommodity([FromRoute]int id)
        {
            try
            {

                var commodity = await _context.Commodities.Where(c => c.StoreId == id).ToListAsync();

                foreach (var item in commodity)
                {
                    item.Photo = $"/api/Commodities/GetPhoto/{item.Photo}";
                }

                return new ObjectResult(commodity);
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostStore([FromBody] Store store)
        {
            try
            {
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return Ok("با موفقیت ثبت شد");
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var store = await _context.Stores.Where(s => s.UserName == username && s.Password == password).SingleOrDefaultAsync();
                if (store != null)
                {
                    return Ok(store);
                }
                else
                {
                    return NotFound("فروشگاهی با این مشخصات یافت نشد");
                }
            }
            catch (Exception e)
            {
                return BadRequest("در دریافت اطلاعات از سرور خطایی رخ داده است");
            }
        }

        
    }
}
