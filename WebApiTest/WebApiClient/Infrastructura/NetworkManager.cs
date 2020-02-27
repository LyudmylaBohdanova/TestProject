using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Models;

namespace WebApiClient.Infrastructura
{
    public class NetworkManager
    {
        const string baseUrl = "http://localhost:50644/api/values";
        static JToken jObject;
        static List<Interval> intervals = new List<Interval>();
        static List<LogInterval> logIntervals = new List<LogInterval>();
        /*
        public NetworkManager()
        {
            intervals = new List<Interval>();
            logIntervals = new List<LogInterval>();
        }*/


        public async Task<string> GetJson(string url)
        {
            HttpClient client = new HttpClient();

            try
            {
                return await client.GetStringAsync(url);

            }
            catch (HttpRequestException ex)
            {
                return "Oops";
            }
        }

        public void ShowListIntervals(IEnumerable<Interval> intervals)
        {
            foreach (var i in intervals)
                Console.WriteLine($"ID: {i.ID}\tBeginDate: {i.BeginDate}\tEndDate: {i.EndDate}");
        }

        public void ShowListLogIntervals(IEnumerable<LogInterval> log)
        {
            foreach (var i in log)
                Console.WriteLine($"ID: {i.ID}\tDate change: {i.DateChange}\tType: {i.TypeChange}\tID: {i.Interval_ID}\t" +
                    $"BeginDate: {i.BeginDate}\tEndDate: {i.EndDate}");
        }

        public async Task GetIntervals()
        {
            intervals.Clear();
            string url = baseUrl;
            string json = await GetJson(url);

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

            ShowListIntervals(intervals);
        }

        public async Task GetLogIntervals()
        {
            logIntervals.Clear();
            string url = $"{baseUrl}/log";
            string json = await GetJson(url);

            jObject = JToken.Parse(json);
            IList<JToken> results = jObject.Children().ToList();

            foreach (var i in results)
            {
                LogInterval log = new LogInterval();
                log.ID = Convert.ToInt32(i["ID"]);
                log.DateChange = Convert.ToDateTime(i["DateChange"]);
                log.TypeChange = i["TypeChange"].ToString();
                if (Convert.ToInt32(i["Interval_ID"]) != 0)
                {
                    log.Interval_ID = Convert.ToInt32(i["Interval_ID"]);
                    log.BeginDate = Convert.ToDateTime(i["BeginDate"]);
                    log.EndDate = Convert.ToDateTime(i["EndDate"]);
                }
                else
                    log.Interval_ID = 0;

                logIntervals.Add(log);
            }
            ShowListLogIntervals(logIntervals);
        }

        public async Task SelectIntervals()
        {
            intervals.Clear();
            string url = $"{baseUrl}/select/";
            Interval interval = new Interval();

            Console.WriteLine("Enter interval:");
            Console.Write("Enter Begin date:  ");
            url = url + Console.ReadLine() + "/";
            Console.Write("Enter End date:  ");
            url = url + Console.ReadLine();

            string json = await GetJson(url);

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

            ShowListIntervals(intervals);
        }

        public async Task CreateInterval()
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
            Console.WriteLine();
            Console.WriteLine(result);
        }
    }
}
