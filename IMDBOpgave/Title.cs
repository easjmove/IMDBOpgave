using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBOpgave
{
    public class Title
    {
        public string Tconst { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string Genres { get; set; }

        public Title(string tconst, string titleType,
            string primaryTitle, string originalTitle,
            bool isAdult, int? startYear, int? endYear,
            int? runtimeMinutes, string genres)
        {
            Tconst = tconst;
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
            IsAdult = isAdult;
            StartYear = startYear;
            EndYear = endYear;
            RuntimeMinutes = runtimeMinutes;
            Genres = genres;
        }

        public override string ToString()
        {
            return $"{Tconst},{PrimaryTitle},{StartYear},{EndYear}";
        }
    }
}
    