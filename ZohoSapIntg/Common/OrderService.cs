//using SAPbobsCOM;
using ZohoSapIntg.Models;

namespace ZohoSapIntg.Common
{
    public class OrderService
    {
        // Crear Orden de Venta (ORDR + RDR1)
        public int CreateOrder(OrderDto order)
        {
            throw new NotImplementedException("SAP deshabilitado temporalmente");
        }

        // Obtener Orden por DocEntry
        public OrderDto GetOrder(int docEntry)
        {
            throw new NotImplementedException("SAP deshabilitado temporalmente");
        }

        // Actualizar Orden (solo campos permitidos por SAP)
        public void UpdateOrder(int docEntry, OrderDto order)
        {
            throw new NotImplementedException("SAP deshabilitado temporalmente");
        }

        // Listar Órdenes con query SQL
        public List<OrderDto> ListOrders(string cardCode = null)
        {
            return new List<OrderDto>();
        }
    }
}
