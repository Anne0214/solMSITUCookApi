using prjMSITUCookApi.Repository.Dtos.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IShoppingCartRepository
    {
        bool Insert(ShoppingCartPostCondition model);
    }
}
