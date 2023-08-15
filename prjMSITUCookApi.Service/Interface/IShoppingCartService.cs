using prjMSITUCookApi.Service.Dtos.Info;
using prjMSITUCookApi.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Interface
{
    internal interface IShoppingCartService
    {
        public bool Insert(ShoppingCartPostInfo model);
        public List<ShoppingCartResultModel> GetCartByMemberId(int memberId);
    }
}
