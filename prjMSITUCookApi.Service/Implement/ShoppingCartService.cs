using AutoMapper;
using prjMSITUCookApi.Repository.Dtos.Condition;
using prjMSITUCookApi.Repository.Implement;
using prjMSITUCookApi.Repository.Interface;
using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Interface;
using prjMSITUCookApi.Service.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Implement
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartService()
        {
            this._shoppingCartRepository = new ShoppingCartRepository();
            this._mapper = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>()).CreateMapper();
        }
        public bool Insert(ShoppingCartPostInfo model)
        {
            var data = _mapper.Map<ShoppingCartPostInfo, ShoppingCartPostCondition>(model);
            return _shoppingCartRepository.Insert(data);
        }
    }
}
