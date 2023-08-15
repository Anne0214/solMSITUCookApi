using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.DataModel
{
    public class ShoppingCartDataModel
    {
        public int CartId { get; set; }

        public int MemberId { get; set; }

        public int SpuId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? SetupTime { get; set; }

        public int SkuId { get; set; }
    }
}
