namespace prjMSITUCookApi.Models.Parameter
{
    public class ShoppingCartPostParameter
    {
        public int MemberId { get; set; }
        public int SpuId { get; set; }
        public int SkuId { get; set; }
        public int Quantity { get; set; }
    }
}
