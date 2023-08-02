using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjMSITUCookApi.Mappings;
using prjMSITUCookApi.Models.Parameter;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Implement;
using System.Reflection.Metadata;

namespace prjMSITUCookApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ShoppingCartService shoppingCartService;
        private IMapper _mapper;

        public ShoppingCartController()
        {
            shoppingCartService = new ShoppingCartService();
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<ControllerMappings>()).CreateMapper();
        }
        [HttpPost]
        public bool AddToCart(ShoppingCartPostParameter model)
        {
            var data = _mapper.Map<ShoppingCartPostParameter, ShoppingCartPostInfo>(model);
            data.SetupTime = DateTime.Now;

            return shoppingCartService.Insert(data);
        }
    }
}
