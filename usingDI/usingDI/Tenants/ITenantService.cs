namespace usingDI.Tenants
{
    public interface ITenantService
    {
        public string GetTenantId();
        //SaaS
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
