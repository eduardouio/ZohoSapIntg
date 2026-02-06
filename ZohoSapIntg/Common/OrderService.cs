using SAPbobsCOM;
using ZohoSapIntg.Models;

namespace ZohoSapIntg.Common
{
    public class OrderService
    {
        // Crear Orden de Venta (ORDR + RDR1)
        public int CreateOrder(OrderDto order)
        {
            using var sapConn = new SapCompanyConnection();
            var company = sapConn.Connect();

            var oOrder = (Documents)company.GetBusinessObject(BoObjectTypes.oOrders);

            oOrder.CardCode = order.CardCode;
            oOrder.DocDate = order.DocDate;
            oOrder.DocDueDate = order.DocDueDate;
            oOrder.Comments = order.Comments;

            foreach (var line in order.Lines)
            {
                oOrder.Lines.ItemCode = line.ItemCode;
                oOrder.Lines.Quantity = (double)line.Quantity;
                oOrder.Lines.Price = (double)line.Price;
                oOrder.Lines.WhsCode = line.WhsCode;
                oOrder.Lines.Add();
            }

            if (oOrder.Add() != 0)
            {
                company.GetLastError(out int errCode, out string errMsg);
                throw new Exception($"Error al crear orden: {errCode} - {errMsg}");
            }

            return int.Parse(company.GetNewObjectKey());
        }

        // Obtener Orden por DocEntry
        public OrderDto GetOrder(int docEntry)
        {
            using var sapConn = new SapCompanyConnection();
            var company = sapConn.Connect();

            var oOrder = (Documents)company.GetBusinessObject(BoObjectTypes.oOrders);

            if (!oOrder.GetByKey(docEntry))
            {
                throw new Exception($"Orden {docEntry} no encontrada");
            }

            var order = new OrderDto
            {
                DocEntry = oOrder.DocEntry,
                DocNum = oOrder.DocNum,
                CardCode = oOrder.CardCode,
                CardName = oOrder.CardName,
                DocDate = oOrder.DocDate,
                DocDueDate = oOrder.DocDueDate,
                DocTotal = (decimal)oOrder.DocTotal,
                Comments = oOrder.Comments
            };

            for (int i = 0; i < oOrder.Lines.Count; i++)
            {
                oOrder.Lines.SetCurrentLine(i);
                order.Lines.Add(new OrderLineDto
                {
                    LineNum = oOrder.Lines.LineNum,
                    ItemCode = oOrder.Lines.ItemCode,
                    ItemDescription = oOrder.Lines.ItemDescription,
                    Quantity = (decimal)oOrder.Lines.Quantity,
                    Price = (decimal)oOrder.Lines.Price,
                    WhsCode = oOrder.Lines.WarehouseCode
                });
            }

            return order;
        }

        // Actualizar Orden (solo campos permitidos por SAP)
        public void UpdateOrder(int docEntry, OrderDto order)
        {
            using var sapConn = new SapCompanyConnection();
            var company = sapConn.Connect();

            var oOrder = (Documents)company.GetBusinessObject(BoObjectTypes.oOrders);

            if (!oOrder.GetByKey(docEntry))
            {
                throw new Exception($"Orden {docEntry} no encontrada");
            }

            oOrder.Comments = order.Comments;

            if (oOrder.Update() != 0)
            {
                company.GetLastError(out int errCode, out string errMsg);
                throw new Exception($"Error al actualizar: {errCode} - {errMsg}");
            }
        }

        // Listar Órdenes con query SQL
        public List<OrderDto> ListOrders(string cardCode = null)
        {
            using var sapConn = new SapCompanyConnection();
            var company = sapConn.Connect();

            var rs = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);

            var sql = "SELECT DocEntry, DocNum, CardCode, CardName, DocDate, DocDueDate, DocTotal FROM ORDR WHERE 1=1";
            if (!string.IsNullOrEmpty(cardCode))
            {
                sql += $" AND CardCode = '{cardCode}'";
            }

            rs.DoQuery(sql);

            var orders = new List<OrderDto>();
            while (!rs.EoF)
            {
                orders.Add(new OrderDto
                {
                    DocEntry = (int)rs.Fields.Item("DocEntry").Value,
                    DocNum = (int)rs.Fields.Item("DocNum").Value,
                    CardCode = rs.Fields.Item("CardCode").Value?.ToString(),
                    CardName = rs.Fields.Item("CardName").Value?.ToString(),
                    DocDate = (DateTime)rs.Fields.Item("DocDate").Value,
                    DocDueDate = (DateTime)rs.Fields.Item("DocDueDate").Value,
                    DocTotal = Convert.ToDecimal(rs.Fields.Item("DocTotal").Value)
                });
                rs.MoveNext();
            }

            return orders;
        }
    }
}
