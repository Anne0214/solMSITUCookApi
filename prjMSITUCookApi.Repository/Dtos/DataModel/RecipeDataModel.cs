using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class RecipeDataModel
    {
      public int RECIPE食譜_PK { get; set; }
      public int AUTHOR_作者 { get; set; }
      public string NICK_NAME暱稱 { get; set; }
      public string PROFILE_PHOTO頭貼 { get; set; }
      public string RECIPE_COVER { get; set; }
      public DateTime PUBLISHED_TIME出版時間 { get; set; }
      public string RECIPE_NAME食譜名稱 { get; set; }
      public int LIKES_讚數 { get; set; }

    }
}
