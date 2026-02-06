using Microsoft.AspNetCore.Mvc;
using SAPbobsCOM;
using ZohoSapIntg.Common;

namespace ZohoSapIntg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SapController : ControllerBase
    {
        [HttpGet("connect")]
        public IActionResult Connect()
        {
            try
            {
                using var sapConn = new SapCompanyConnection();
                var company = sapConn.Connect();

                var recordset = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);

                var sql = @"SELECT TOP 20 
                            DocEntry, 
                            DocNum, 
                            CardCode, 
                            CardName, 
                            DocDate, 
                            DocDueDate, 
                            DocTotal,
                            DocStatus
                            FROM ORDR 
                            ORDER BY DocEntry DESC";

                recordset.DoQuery(sql);

                var pedidos = new List<object>();

                while (!recordset.EoF)
                {
                    pedidos.Add(new
                    {
                        DocEntry = recordset.Fields.Item("DocEntry").Value,
                        DocNum = recordset.Fields.Item("DocNum").Value,
                        CardCode = recordset.Fields.Item("CardCode").Value?.ToString(),
                        CardName = recordset.Fields.Item("CardName").Value?.ToString(),
                        DocDate = recordset.Fields.Item("DocDate").Value,
                        DocDueDate = recordset.Fields.Item("DocDueDate").Value,
                        DocTotal = recordset.Fields.Item("DocTotal").Value,
                        DocStatus = recordset.Fields.Item("DocStatus").Value?.ToString()
                    });

                    recordset.MoveNext();
                }

                return Ok(new
                {
                    success = true,
                    message = "Conexión exitosa a SAP Business One",
                    company = company.CompanyName,
                    database = company.CompanyDB,
                    totalPedidos = pedidos.Count,
                    pedidos = pedidos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message,
                    errorType = ex.GetType().FullName,
                    hResult = ex.HResult,
                    innerError = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}
