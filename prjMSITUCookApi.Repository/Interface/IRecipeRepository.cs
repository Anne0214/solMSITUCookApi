using prjMSITUCookApi.Repository.Dtos.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Interface
{
    public interface IRecipeRepository
    {
        /// <summary>
        /// 取得一則食譜資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RecipeDataModel Get(int id);

    }
}
