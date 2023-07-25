
using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Models.Parameter;

namespace prjMSITUCookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewController:ControllerBase
    {

        /// <summary>
        /// 紀錄瀏覽(都還沒實作)
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ViewParameter Parameter) {
            return Ok();
        }
        
    }
}
