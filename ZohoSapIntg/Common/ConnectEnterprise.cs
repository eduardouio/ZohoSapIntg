using System;
using SAPbobsCOM;

namespace ZohoSapIntg.Common
{
    public class SapCompanyConnection : IDisposable
    {
        private Company _company;

        public Company Connect()
        {
            _company = new Company
            {
                Server = "SERVIDOR_SQL",
                DbServerType = BoDataServerTypes.dst_MSSQL2019,
                CompanyDB = "TEST_VINESA",
                UserName = "auditori",
                Password = "1234",
                DbUserName = "intg",
                DbPassword = "Horiz0nt3s",
                UseTrusted = false,
                language = BoSuppLangs.ln_Spanish_La
            };

            int result = _company.Connect();

            if (result != 0)
            {
                _company.GetLastError(out int errorCode, out string errorMessage);
                throw new Exception($"SAP DI API Error {errorCode}: {errorMessage}");
            }

            return _company;
        }

        public void Dispose()
        {
            if (_company != null && _company.Connected)
            {
                _company.Disconnect();
            }

            _company = null;
        }
    }
}
