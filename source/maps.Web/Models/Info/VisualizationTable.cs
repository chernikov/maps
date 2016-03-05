using maps.Model;
using maps.Web.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maps.Web.Models.Info
{
    public class VisualizationTable
    {
        public List<VisualizationColumn> Columns { get; set; }

        public List<VisualizationItem> Items { get; set; }


        public VisualizationTable()
        {
            Columns = new List<VisualizationColumn>();
            Items = new List<VisualizationItem>();
        }

        public static VisualizationTable Parse(string data)
        {
            var table = new VisualizationTable();
            var rows = data.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (rows.Count <= 1)
            {
                throw new ArgumentException("Table is empty!");
            }
            var header = rows[0];
            var headerLower = rows[0].ToLower();
            var columnsLower = headerLower.Split(new string[] { ";" }, StringSplitOptions.None).ToList();
            var columns = header.Split(new string[] { ";" }, StringSplitOptions.None).ToList();

            var indexLat = columnsLower.IndexOf("lat") != -1 ? columnsLower.IndexOf("lat") : columnsLower.IndexOf("latitude");
            var indexLng = columnsLower.IndexOf("lng") != -1 ? columnsLower.IndexOf("lng") : columnsLower.IndexOf("longitude");
            if (indexLat == -1 || indexLng == -1)
            {
                throw new ArgumentException("Longitude and latidute column are not exist!");
            }
            table.Gathering(rows, columns, indexLat, indexLng);

            table.Analyze(rows, columns);
            return table;
        }

        private void Gathering(List<string> rows, List<string> columns, int indexLat, int indexLng)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                Columns.Add(new VisualizationColumn()
                {
                    Name = columns[i],
                    Number = i + 1,
                    Type = (int)VisualizationColumn.VisualizationType.Text
                });
            }
            for (int j = 1; j < rows.Count; j++)
            {
                try
                {
                    var items = rows[j].Split(new string[] { ";" }, StringSplitOptions.None).ToList();

                    var latStr = items[indexLat];
                    var lngStr = items[indexLng];

                    var lat = Double.Parse(latStr);
                    var lng = Double.Parse(lngStr);
                    var obj = new List<KeyValuePair<string, string>>();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (items.Count > i)
                        {
                            obj.Add(new KeyValuePair<string, string>(columns[i], items[i]));
                        }
                    }
                    var dataObj = JsonConvert.SerializeObject(obj, new KeyValueConverter());

                    Items.Add(new VisualizationItem()
                    {
                        Lat = lat,
                        Lng = lng,
                        Data = dataObj
                    });
                }
                catch
                {
                    //nothing just skip
                }
            }
        }


        private void Analyze(List<string> rows, List<string> columns)
        {
            var realRows = rows.Skip(1).ToList();
            for (int i = 0; i < Columns.Count; i++)
            {
                //date
                var possibleDate = true;
                foreach (var item in realRows)
                {
                    var items = item.Split(new string[] { ";" }, StringSplitOptions.None).ToList();

                    var value = items[i];
                    DateTime date;
                    if (!DateTime.TryParse(value, out date))
                    {
                        possibleDate = false;
                        break;
                    }
                }
                if (possibleDate)
                {
                    Columns[i].Type = (int)VisualizationColumn.VisualizationType.Date;
                }
                //time
                var possibleTime = true;
                foreach (var item in realRows)
                {
                    var items = item.Split(new string[] { ";" }, StringSplitOptions.None).ToList();

                    var value = items[i];
                    TimeSpan time;
                    if (!TimeSpan.TryParse(value, out time))
                    {
                        possibleTime = false;
                        break;
                    }
                }
                if (possibleTime)
                {
                    Columns[i].Type = (int)VisualizationColumn.VisualizationType.Time;
                }
                //list 
                var list = new List<string>();
                foreach (var item in realRows)
                {
                    var items = item.Split(new string[] { ";" }, StringSplitOptions.None).ToList();
                    list.Add(items[i]);
                }

                var distinctList = list.Distinct().ToList();
                if (distinctList.Count * 3 < list.Count)
                {
                    Columns[i].Type = (int)VisualizationColumn.VisualizationType.List;
                    var filteredValues = string.Join("; ", distinctList.ToArray());
                    Columns[i].FilterValues = filteredValues;
                }
            }
        }
    }
}