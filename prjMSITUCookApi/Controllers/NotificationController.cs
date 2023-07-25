using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Models.ViewModel;

namespace prjMSITUCookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController:ControllerBase
    {
        /// <summary>
        /// 取得一筆通知詳情
        /// </summary>
        /// <param name="id">該筆通知在資料庫的編號</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        [ProducesResponseType(typeof(NotificationViewModel), 200)]
        [Route("{id}")]
        public NotificationViewModel Get([FromRoute] int id){

            var n = new NotificationViewModel();
            return n;
        }

        /// <summary>
        /// 取得某用戶的某類通知(或全部)
        /// </summary>
        /// <param name="parameter">通知資料</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        public IEnumerable<NotificationViewModel> GetList([FromQuery] NotificationSearchParameter parameter) {
            IEnumerable<NotificationViewModel> n = null;

            return n;
        }

        ///// <summary>
        ///// 新增一條通知
        ///// </summary>
        ///// <param name="parameter"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult Post([FromBody] NotificationParameter parameter) {

        //    return Ok();
        //}

        /// <summary>
        /// 將通知改為已讀
        /// </summary>
        /// <param name="id">該筆通知在資料庫的編號</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Read([FromRoute] int id) {
            return Ok();
        }
        /// <summary>
        /// 針對一群通知改為已讀
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ReadList([FromBody] List<int> idList ) {
            return Ok();
        }

        /// <summary>
        /// 刪除某則通知
        /// </summary>
        /// <param name="id">該筆通知在資料庫的編號</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) {

            return Ok();
        }
    }
}
