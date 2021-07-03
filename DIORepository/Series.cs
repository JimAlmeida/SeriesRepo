using System.Collections.Generic;

namespace DIORepository.DataTransferObjects
{
    public class Series
    {
        
        public string genre { get; set; }
        public string title { get; set; }
        public string startYear { get; set; }
        public string description { get; set; }
        public int? id { get; set; }
        public bool isDeleted { get; set; }

        public override string ToString()
        {
            string endl = System.Environment.NewLine;
            string output = "";

            if (id != null)
            {
                output += $"Id: {id}" + endl;
            }
            if (genre != null)
            {
                output += $"Genre: {genre}" + endl;
            }
            if (title != null)
            {
                output += $"Title: {title}" + endl;
            }
            if (startYear != null)
            {
                output += $"Year: {startYear}" + endl;
            }
            if (description != null)
            {
                output += $"Description: {description}" + endl;
            }
            return output;
        }
    }
}

namespace DIORepository.DTOExtensions
{
    public static class SeriesExtensions
    {
        public static string[] AsList(this DIORepository.DataTransferObjects.Series series)
        {
            List<string> parameters = new();
            if (series.genre != null)
            {
                parameters.Add(series.genre);
            }
            if (series.title != null)
            {
                parameters.Add(series.title);
            }
            if (series.startYear != null)
            {
                parameters.Add(series.startYear);
            }
            if (series.description != null)
            {
                parameters.Add(series.description);
            }
            return parameters.ToArray();
        }
        public static string[] NotNullParameterList(this DIORepository.DataTransferObjects.Series series)
        {
            List<string> parameters = new();
            if (series.genre != null)
            {
                parameters.Add("genre");
            }
            if (series.title != null)
            {
                parameters.Add("title");
            }
            if (series.startYear != null)
            {
                parameters.Add("startYear");
            }
            if (series.description != null)
            {
                parameters.Add("description");
            }
            return parameters.ToArray();
        }
    }
}
