using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBOpgave
{
    public class PreparedInserter : IInserter
    {
        public void InsertTitles(SqlConnection sqlConn, List<Title> titles)
        {
            SqlTransaction transaction = sqlConn.BeginTransaction();

            SqlCommand cmd = new SqlCommand("INSERT INTO Titles " +
                    "([Tconst],[TitleType],[PrimaryTitle]," +
                    "[OriginalTitle],[IsAdult],[StartYear]," +
                    "[EndYear],[RuntimeMinutes]) " +
                    "VALUES " +
                    $"(@tconst" +
                    $",@TitleType" +
                    $",@PrimaryTitle" +
                    $",@OriginalTitle" +
                    $",@IsAdult" +
                    $",@StartYear" +
                    $",@EndYear" +
                    $",@RuntimeMinutes" +
                    ")", sqlConn, transaction);
            SqlParameter tConstParameter = new SqlParameter("@tconst",
                System.Data.SqlDbType.VarChar, 50);
            cmd.Parameters.Add(tConstParameter);

            SqlParameter TitleTypeParameter = new SqlParameter("@TitleType",
                System.Data.SqlDbType.VarChar, 50);
            cmd.Parameters.Add(TitleTypeParameter);

            SqlParameter PrimaryTitleParameter = new SqlParameter("@PrimaryTitle",
                System.Data.SqlDbType.VarChar, 500);
            cmd.Parameters.Add(PrimaryTitleParameter);

            SqlParameter OriginalTitleParameter = new SqlParameter("@OriginalTitle",
                System.Data.SqlDbType.VarChar, 500);
            cmd.Parameters.Add(OriginalTitleParameter);

            SqlParameter IsAdultParameter = new SqlParameter("@IsAdult",
                System.Data.SqlDbType.Bit);
            cmd.Parameters.Add(IsAdultParameter);

            SqlParameter StartYearParameter = new SqlParameter("@StartYear",
                System.Data.SqlDbType.SmallInt);
            cmd.Parameters.Add(StartYearParameter);

            SqlParameter EndYearParameter = new SqlParameter("@EndYear",
                System.Data.SqlDbType.SmallInt);
            cmd.Parameters.Add(EndYearParameter);

            SqlParameter RuntimeMinutesParameter = new SqlParameter("@RuntimeMinutes",
                System.Data.SqlDbType.SmallInt);
            cmd.Parameters.Add(RuntimeMinutesParameter);

            cmd.Prepare();

            foreach (Title title in titles)
            {
                FillParameter(tConstParameter, title.Tconst);
                FillParameter(TitleTypeParameter, title.TitleType);
                FillParameter(OriginalTitleParameter, title.OriginalTitle);
                FillParameter(PrimaryTitleParameter, title.PrimaryTitle);
                FillParameter(IsAdultParameter, title.IsAdult);
                FillParameter(StartYearParameter, title.StartYear);
                FillParameter(EndYearParameter, title.EndYear);
                FillParameter(RuntimeMinutesParameter, title.RuntimeMinutes);

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

        public void FillParameter(SqlParameter parameter, object? value)
        {
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            } else
            {
                parameter.Value = value;
            }
        }
    }
}
