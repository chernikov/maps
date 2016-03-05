using maps.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Models.Info
{
    public class VisualizationFilter
    {
        public class FilterField
        {
            public VisualizationColumn.VisualizationType Type { get; set; }

            public string Name { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? EndDate { get; set; }

            public TimeSpan? StartTime { get; set; }

            public TimeSpan? EndTime { get; set; }

            public string Value { get; set; }

            public IList<string> FilterValues { get; set; }

            public IEnumerable<SelectListItem> SelectListValue
            {
                get
                {
                    if (FilterValues != null)
                    {
                        yield return new SelectListItem()
                        {
                            Text = "All",
                            Value = null,
                            Selected = Value == null
                        };
                        foreach (var item in FilterValues)
                        {
                            yield return new SelectListItem()
                            {
                                Text = item,
                                Value = item,
                                Selected = Value == item
                            };
                        }
                    }
                }
            }
        }

        public int ID { get; set; }

        public List<FilterField> Fields { get; set; }

        public string SearchString { get; set; }

        public VisualizationFilter()
        {
            Fields = new List<FilterField>();
        }
        public VisualizationFilter(int id, IList<VisualizationColumn> columns)
        {
            ID = id;
            Fields = new List<FilterField>();
            foreach (var column in columns)
            {
                var field = new FilterField() {
                    Type = (VisualizationColumn.VisualizationType)column.Type,
                    Name = column.Name                    
                };
                if (field.Type == VisualizationColumn.VisualizationType.List) 
                {
                    field.FilterValues = column.FilterValues.Split(new string[] {";"}, StringSplitOptions.None);
                }
                Fields.Add(field);
            }
        }
    }
}