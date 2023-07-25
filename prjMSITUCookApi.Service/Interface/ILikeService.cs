using prjMSITUCookApi.Service.Dtos.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Service.Interface
{
    public interface ILikeService
    {
        bool Post(LikeInfo info);

        bool Delete(LikeDeleteInfo info);
    }
}
