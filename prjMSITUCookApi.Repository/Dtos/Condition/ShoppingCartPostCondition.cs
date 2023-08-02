using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookApi.Repository.Dtos.Condition
{
    public class ShoppingCartPostCondition
    {

        public int MemberId會員Fk { get; set; }

        public int Spu { get; set; }

        public int? Quantity數量 { get; set; }

        public DateTime? SetupTime建立時間 { get; set; }

        public int Sku { get; set; }
    }
}
