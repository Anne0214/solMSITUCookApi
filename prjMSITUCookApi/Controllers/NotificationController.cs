using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Mappings;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Models.ViewModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using prjMSITUCookApi.Service.Implement;
using prjMSITUCookApi.Service.Interface;
using System.Security.Cryptography.Xml;

namespace prjMSITUCookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController:ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController() {
            _notificationService = new NotificationService();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ControllerMappings>());

            this._mapper = config.CreateMapper();
        }
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

            var info = this._notificationService.Get(id);
            if (info == null) {
                Response.StatusCode = 404;
                return null;
            }
            var result = this._mapper.Map<NotificationResultModel,NotificationViewModel>(info);

            return result;
        }

        /// <summary>
        /// 取得某用戶的某類通知(或全部)
        /// </summary>
        /// <param name="parameter">通知資料</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        public IEnumerable<NotificationViewModel> GetList([FromQuery] NotificationSearchParameter parameter) {
            if (parameter.MemberId == 0) {
                Response.StatusCode = 400;
                return null;
            }
            try { 
                var info = this._mapper.Map<NotificationSearchParameter,NotificationSearchInfo>(parameter);
                var list = _notificationService.GetList(info);

                List<NotificationViewModel> vms = new List<NotificationViewModel>();

                if (list == null || list.Count() == 0) {
                    return vms;
                }
                foreach (var i in list) {
                    var vm = this._mapper.Map<NotificationResultModel, NotificationViewModel>(i);
                    vms.Add(vm);
                }
                vms = vms.OrderByDescending(x => x.NotificationTime).ToList();
                Response.StatusCode = 200;
                return vms;
            }
            catch {
                Response.StatusCode = 500;

                return null; }
            
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
            
            var result = _notificationService.Read(id);
            if (result) {
                return Ok();
            }
            return StatusCode(500);
        }
        /// <summary>
        /// 將一群通知改為已讀
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ReadList([FromBody] List<int> parameter ) {

            var result = _notificationService.ReadList(parameter);
            if (result) {
                return Ok();
            }

            return StatusCode(500);
        }

        /// <summary>
        /// 刪除某則通知
        /// </summary>
        /// <param name="id">該筆通知在資料庫的編號</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id) {

            var result = _notificationService.DeleteById(id);
            if (result) {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
