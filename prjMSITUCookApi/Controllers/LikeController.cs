using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Implement;
using prjMSITUCookApi.Service.Interface;
using prjMSITUCookApi.Mappings;

namespace prjMSITUCookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikeController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILikeService _likeService;

        public LikeController() {
            var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<ControllerMappings>());

            this._mapper = config.CreateMapper();

            _likeService = new LikeService();
        }
        /// <summary>
        /// 對文章按讚
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]LikeParameter parameter) {

            var info = this._mapper.Map<LikeParameter, LikeInfo>(parameter);
            var result = this._likeService.Post(info);

            if (result) {
                return Ok();
            }
            return StatusCode(500);       
        }

        /// <summary>
        /// 取消按讚
        /// </summary>
        /// <param name="parameter">取消按讚所需資訊</param>
        /// <response code="200">取消按讚成功</response>
        /// <response code="404">不曾按讚過所以無法刪除</response>
        /// <returns></returns>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromQuery] LikeDeleteParameter parameter) {
            var info = this._mapper.Map<LikeDeleteParameter, LikeDeleteInfo>(parameter);
            var result = this._likeService.Delete(info);

            if (result) {
                return Ok();
            }
            return StatusCode(50);
        }
    }
}
