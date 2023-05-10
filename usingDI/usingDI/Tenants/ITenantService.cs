namespace usingDI.Tenants
{
    public interface ITenantService
    {
        public string GetTenantId();
        //SaaS : Software as a Service
        //       Solution as a Service

        /*
         * IaaS : Boş kiralık ev
         * PaaS : Eşyalı kiralık ev
         * SaaS : Günlük kiralık - AirBnB
         */

    }

    public class SQLTenant : ITenantService
    {
        public string GetTenantId()
        {
            return "SqlConnection";
        }
    }

    public class OracleTenant : ITenantService
    {
        public string GetTenantId()
        {
            return "OracleConnection";
        }
    }

}
