using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Mappings;
using prjMSITUCookApi.Models;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Models.ViewModel;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using prjMSITUCookApi.Service.Implement;
using prjMSITUCookApi.Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace prjMSITUCookApi.Controllers
{

    [ApiController]
    [Route("[controller]")] //表示路徑參考controller名稱
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IMapper _mapper;

        public FollowController(){
            _followService = new FollowService();
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ControllerMappings>());

            this._mapper = config.CreateMapper();
            
        }
        /// <summary>
        /// 取得某個會員的追蹤/粉絲清單
        /// </summary>
        /// <param name="parameter">搜尋參數</param>
        /// <response code="200">回傳符合條件追蹤關係列表</response>
        /// <response code="400">parameter有錯</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        public IEnumerable<FollowViewModel> GetList([FromQuery]FollowSearchParameter parameter) {

            if (!ModelState.IsValid) {
                Response.StatusCode = 400;
                return null;
            }
            //轉成parameter
            var condition = _mapper.Map<FollowSearchParameter, FollowSearchInfo>(parameter);
            //使用服務取得資料
            var data = _followService.GetList(condition);

            if (data == null) {
                Response.StatusCode = 404;
                return null;
            }
            //轉成回傳要的形式
            var result = _mapper.Map<IEnumerable<FollowResultModel>, IEnumerable<FollowViewModel>>(data);
            return result;
        }

        /// <summary>
        /// 查詢某條追蹤關係的詳情(用不到但開開看)
        /// </summary>
        /// <param name="id">在資料表中的PK</param>
        /// <response code="200">回傳對應的追蹤關係</response>
        /// <response code="404">找不到該追蹤關係</response>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")] //指定回傳格式是json
        [ProducesResponseType(typeof(FollowViewModel),200)]
        [Route("{id}")] //方法裡的參數要使用路徑裡面的id
        public FollowViewModel Get([FromRoute] int id) {
            var data = _followService.Get(id);
            var result = _mapper.Map<FollowResultModel, FollowViewModel>(data);

            if (result == null) {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }
        /// <summary>
        /// 新增追蹤關係
        /// </summary>
        /// <param name="parameter">新增所需參數</param>
        /// <response code="200">回傳該筆新增資料的編號</response>
        /// <response code="500">server有問題，無法新增</response>
        /// <response code="409">已存在追蹤關係，無法新增</response>
        /// <returns>該筆新增資料的編號</returns>
        [HttpPost]
        public IActionResult Create([FromBody] FollowParameter parameter) {
            
            var info = _mapper.Map<FollowParameter,FollowInfo>(parameter);
            var result= _followService.Insert(info);
            
            if (result == "追蹤成功") {
                return Ok();
            }
            if (result == "已經追蹤") {
                return StatusCode(409);
            }

            return StatusCode(500);
        }
        /// <summary>
        /// 取消追蹤
        /// </summary>
        /// <param name="parameter">取消追蹤所需參數</param>
        /// <response code="200">刪除成功</response>
        /// <response code="500">刪除失敗，可能原因有不存在該追蹤關係或伺服器有問題等等原因</response>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromQuery] FollowDeleteParameter parameter ) {
            var info = _mapper.Map<FollowDeleteParameter, FollowDeleteInfo>(parameter);
            var result = _followService.Delete(info);
            if (result) {
                return Ok();
            }
            return StatusCode(500);
        }
    }

   
    
}
