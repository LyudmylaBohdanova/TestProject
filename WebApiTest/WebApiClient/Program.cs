using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Infrastructura;
using WebApiClient.Models;

namespace WebApiClient
{
    class Program
    {
        const string baseUrl = "http://localhost:50644/api/values";
        static NetworkManager networkManager;
        static JToken jObject;
        static List<Interval> intervals = new List<Interval>();
        static List<LogInterval> logIntervals = new List<LogInterval>();


        static async Task Main(string[] args)
        {
            networkManager = new NetworkManager();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Create interval\t\t- 1;\nSelect all interval\t- 2;\nSelect period\t\t- 3;\n" +
                    "Select all log\t\t- 4;\nExit\t\t- 0;");

                Console.Write("Key: ");
                await SelectMethod(Convert.ToInt32(Console.ReadLine()));
            }
        }

        static async Task SelectMethod(int key)
        {
            switch(key)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    await networkManager.CreateInterval();
                    break;
                case 2:
                    await networkManager.GetIntervals();
                    networkManager.ShowListIntervals(intervals);
                    break;
                case 3:
                    await networkManager.SelectIntervals();
                    networkManager.ShowListIntervals(intervals);
                    break;
                case 4:
                    await networkManager.GetLogIntervals();
                    networkManager.ShowListLogIntervals(logIntervals);
                    break;
            }
        }

        /*
        static void ShowListIntervals(IEnumerable<Interval> intervals)
        {
            foreach (var i in intervals)
                Console.WriteLine($"ID: {i.ID}\tBeginDate: {i.BeginDate}\tEndDate: {i.EndDate}");
        }

        static void ShowListLogIntervals(IEnumerable<LogInterval> log)
        {
            foreach (var i in log)
                Console.WriteLine($"ID: {i.ID}\tDate change: {i.DateChange}\tType: {i.TypeChange}\tID: {i.Interval_ID}\t" +
                    $"BeginDate: {i.BeginDate}\tEndDate: {i.EndDate}");
        }

        static async Task GetIntervals()
        {
            intervals.Clear();
            string url = baseUrl;
            string json = await networkManager.GetJson(url);

            jObject = JToken.Parse(json);
            IList<JToken> results = jObject.Children().ToList();

            foreach (var i in results)
            {
                Interval interval = new Interval();
                interval.ID = Convert.ToInt32(i["ID"]);
                interval.BeginDate = Convert.ToDateTime(i["BeginDate"]);
                interval.EndDate = Convert.ToDateTime(i["EndDate"]);

                intervals.Add(interval);
            }
        }

        static async Task GetLogIntervals()
        {
            logIntervals.Clear();
            string url = $"{baseUrl}/log";
            string json = await networkManager.GetJson(url);

            jObject = JToken.Parse(json);
            IList<JToken> results = jObject.Children().ToList();

            foreach (var i in results)
            {
                LogInterval log = new LogInterval();
                log.ID = Convert.ToInt32(i["ID"]);
                log.DateChange = Convert.ToDateTime(i["DateChange"]);
                log.TypeChange = i["TypeChange"].ToString();
                if(Convert.ToInt32(i["Interval_ID"]) != 0)
                {
                    log.Interval_ID = Convert.ToInt32(i["Interval_ID"]);
                    log.BeginDate = Convert.ToDateTime(i["BeginDate"]);
                    log.EndDate = Convert.ToDateTime(i["EndDate"]);
                }
                else
                    log.Interval_ID = 0;

                logIntervals.Add(log);
            }
        }

        static async Task SelectIntervals()
        {
            intervals.Clear();
            string url = $"{baseUrl}/select/";
            Interval interval = new Interval();

            Console.WriteLine("Enter interval:");
            Console.Write("Enter Begin date:  ");
            url = url + Console.ReadLine() + "/";
            Console.Write("Enter End date:  ");
            url = url + Console.ReadLine();

            string json = await networkManager.GetJson(url);

            jObject = JToken.Parse(json);
            IList<JToken> results = jObject.Children().ToList();

            foreach (var i in results)
            {
                Interval val = new Interval();
                val.ID = Convert.ToInt32(i["ID"]);
                val.BeginDate = Convert.ToDateTime(i["BeginDate"]);
                val.EndDate = Convert.ToDateTime(i["EndDate"]);

                intervals.Add(val);
            }
        }

        static async Task CreateInterval()
        {
            HttpClient client = new HttpClient();
            string url = baseUrl;
            Interval interval = new Interval();

            Console.WriteLine("Create new interval:");
            Console.Write("Enter Begin date:  ");
            interval.BeginDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter End date:  ");
            interval.EndDate = Convert.ToDateTime(Console.ReadLine());

            var json = JsonConvert.SerializeObject(interval);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }*/
    }
}
