using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5
{
    public class JsonConv
    {
        public List<ApplicationLog> ApplicationLogs { get; set; } 
    }
    public class ApplicationLog
    {
        public string DeviceId { get; set; }
        public string ComputerName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DisplayAlert("as", "asd", "OK");
            IServiceClass obj = new ServiceClass();
            obj.CallDatabase();

            //var a = GetData();
            //var data = a.stringList;
            //int i;
            int.TryParse("2", out int i);
            var b = i;

            
            var strin = "{ApplicationLogs:[{DeviceId:\"1\",LogTimeStamp:\"01-01-2015\",ComputerName:\"\",TimeStamp:\"01-01-0001\",Time:\"01-01-0001\"},{DeviceId:\"1\",LogTimeStamp:\"01-01-2015\",ComputerName:\"not null\",TimeStamp:\"01-01-0001\",Time:\"01-01-0001\"}]}";
            var convertedData = JsonConvert.DeserializeObject<JsonConv>(strin);
            foreach(var item in convertedData.ApplicationLogs)
            {
                if (string.IsNullOrEmpty(item.ComputerName)){
                    item.ComputerName = "Server Name";
                }

                if(item.TimeStamp != null && item.TimeStamp.Year < 1732)
                {
                    item.TimeStamp = DateTime.Now;
                }
            }
            //SetLiencence(b.number)
        }

        public void SetLiencence(string number )
        {
           // texbox.text = number;
        }

        //public List<int> intList GetData()
        //{
        //    return new List<int> { 10 };
        //}
    }
}
