namespace usingDI.Tenants
{
    public interface IDatabaseClient
    {
    }

    public class OracleClient : IDatabaseClient
    {
        public override string ToString()
        {
            return "Oracle ile çalışıyor...";
        }
    }
    public class SqlClient : IDatabaseClient
    {
        public override string ToString()
        {
            return "SQL ile çalışıyor!...";
        }
    }
}
