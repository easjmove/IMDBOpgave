using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBOpgave
{
    public class NormalInserter : IInserter
    {
        public void InsertTitles(SqlConnection sqlConn, List<Title> titles)
        {
            SqlTransaction transaction = sqlConn.BeginTransaction();
            foreach (Title title in titles)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Titles " +
                    "([Tconst],[TitleType],[PrimaryTitle]," +
                    "[OriginalTitle],[IsAdult],[StartYear]," +
                    "[EndYear],[RuntimeMinutes]) " +
                    "VALUES " +
                    $"('{title.Tconst}'" +
                    $",'{title.TitleType}'" +
                    $",'{title.PrimaryTitle.Replace("'", "''")}'" +
                    $",'{title.OriginalTitle.Replace("'", "''")}'" +
                    $",'{title.IsAdult}'" +
                    $",{CheckIntForNull(title.StartYear)}" +
                    $",{CheckIntForNull(title.EndYear)}" +
                    $",{CheckIntForNull(title.RuntimeMinutes)}" +
                    ")", sqlConn, transaction);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(cmd.CommandText);
                }
            }
            transaction.Commit();
        }

        public string CheckIntForNull(int? value)
        {
            if (value == null)
            {
                return "NULL";
            }
            return "" + value;
        }
    }
}
