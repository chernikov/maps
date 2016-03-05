using maps.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Tool;

namespace maps.Web.Global.Search
{
    public class SearchEngine
    {
        #region Get
        public static IEnumerable<UtilityDepartment> Get(string SearchString, IQueryable<UtilityDepartment> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Name != null)
                {
                    rank += Regex.Matches(entry.Name.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        public static IEnumerable<VisualizationItem> Get(string SearchString, IQueryable<VisualizationItem> source)
        {
            var term = StringExtension.CleanContent(SearchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var regex = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            int rank;
            foreach (var entry in source)
            {
                rank = 0;
                if (entry.Data != null)
                {
                    rank += Regex.Matches(entry.Data.ToLowerInvariant(), regex).Count;
                }
                if (rank > 0)
                {
                    yield return entry;
                }
            }
        }

        #endregion
    }
}