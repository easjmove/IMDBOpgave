using System.Data.SqlClient;

namespace IMDBOpgave
{
    public interface IInserter
    {
        void InsertTitles(SqlConnection sqlConn, List<Title> titles);
    }
}