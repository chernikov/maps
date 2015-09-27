﻿using maps.Dozor.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace maps.Dozor.Console
{
    class Program
    {
        private static volatile bool _shouldStop;

        private static Thread mainThread { get; set; }
        static void Main(string[] args)
        {
            _shouldStop = false;
            mainThread = new Thread(ThreadFunc);
            mainThread.Start();
            char key = ' ';
            while (key != 'q')
            {
                var info = System.Console.ReadKey();
                key = info.KeyChar;
            }
            _shouldStop = true;
        }

        private static void ThreadFunc()
        {
            var dozor = new DataCollector();
            var cookies = dozor.GetCookies();
            var hash = 0;
            var responceBuses = dozor.GetBuses(cookies);
            var responceObjBuses = JsonConvert.DeserializeObject<RecordResponce<BusRecord>>(responceBuses);
            foreach (var responceItem in responceObjBuses.Values)
            {
                System.Console.WriteLine(string.Format("Id: {1}\tName:{0}", responceItem.Name, responceItem.Id));
            }

            while (true)
            {
                try
                {
                    var responce = dozor.GetData(cookies);
                    if (string.IsNullOrWhiteSpace(responce))
                    {
                        cookies = dozor.GetCookies();
                    }
                    else
                    {
                        var responceObj = JsonConvert.DeserializeObject<RecordResponce<PlaceRecord>>(responce);

                        if (hash != responceObj.Hash)
                        {
                            hash = responceObj.Hash;
                            foreach (var responceItem in responceObj.Values)
                            {
                                System.Console.WriteLine(string.Format("DeviceID:{0}, lat: {1}, lng: {2}", responceItem.DeviceId, responceItem.Latitude, responceItem.Longitude));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(string.Format("Thread period error: {0}", ex.Message));
                }
                if (_shouldStop)
                {
                    return;
                }
                Thread.Sleep(5000);
            }
        }

        private static void GrabData()
        {


        }
    }
}
