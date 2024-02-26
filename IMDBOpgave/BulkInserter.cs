using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBOpgave
{
    public class BulkInserter : IInserter
    {
        public void InsertTitles(SqlConnection sqlConn, List<Title> titles)
        {
            DataTable titleTable = new DataTable("Titles");
            titleTable.Columns.Add("tconst", typeof(string));
            titleTable.Columns.Add("TitleType", typeof(string));
            titleTable.Columns.Add("PrimaryTitle", typeof(string));
            titleTable.Columns.Add("OriginalTitle", typeof(string));
            titleTable.Columns.Add("IsAdult", typeof(bool));
            titleTable.Columns.Add("StartYear", typeof(int));
            titleTable.Columns.Add("EndYear", typeof(int));
            titleTable.Columns.Add("RuntimeMinutes", typeof(int));

            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();

                FillRowValue(titleRow, "tconst", title.Tconst);
                FillRowValue(titleRow, "TitleType", title.TitleType);
                FillRowValue(titleRow, "PrimaryTitle", title.PrimaryTitle);
                FillRowValue(titleRow, "OriginalTitle", title.OriginalTitle);
                FillRowValue(titleRow, "IsAdult", title.IsAdult);
                FillRowValue(titleRow, "StartYear", title.StartYear);
                FillRowValue(titleRow, "EndYear", title.EndYear);
                FillRowValue(titleRow, "RuntimeMinutes", title.RuntimeMinutes);

                titleTable.Rows.Add(titleRow);
            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn,
                SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(titleTable);
        }

        public void FillRowValue(DataRow row,
            string columnName, object? value)
        {
            if (value == null)
            {
                row[columnName] = DBNull.Value;
            }
            else
            {
                row[columnName] = value;
            }
        }
    }
}
