namespace ZohoSapIntg.Models
{
    // DTO para ORDR (Cabecera)
    public class OrderDto
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public decimal DocTotal { get; set; }
        public string Comments { get; set; }
        public List<OrderLineDto> Lines { get; set; } = new();
    }

    // DTO para RDR1 (Líneas)
    public class OrderLineDto
    {
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string WhsCode { get; set; }
    }
}
