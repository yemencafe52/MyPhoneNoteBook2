using System.Data;
using System.Data.OleDb;
namespace DataLayer
{
    public interface ICAccessDB
    {
        int ExecuteNoneQuery(string sql);
        int ExecuteQuery(string sql, ref DataTable dataTable);
    }

    public class CAccessDB : ICAccessDB
    {
        private readonly string connectionString;

        public CAccessDB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int ExecuteNoneQuery(string sql)
        {
            int res = 0;
            try
            {
                using(OleDbConnection con = new OleDbConnection(this.connectionString))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    res = cmd.ExecuteNonQuery();
                }
            }
            catch
            {

            }
            return res;
        }

        public int ExecuteQuery(string sql, ref DataTable dataTable)
        {
            int res = 0;

            try
            {
                using (OleDbDataAdapter adp = new OleDbDataAdapter(sql, new OleDbConnection(this.connectionString)))
                {
                    adp.Fill(dataTable);
                    res = dataTable.Rows.Count;
                }
            }
            catch { }
            return res;
        }
    }
}
