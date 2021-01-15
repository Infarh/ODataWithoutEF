using System;
using System.Linq;
using System.Threading;
using System.Xml;
using Domain.Models;
using Microsoft.OData.Client;
using Simple.OData.Client;

namespace ConsoleClient
{
    class Program
    {
        //[DataServiceEntity]
        //class StudentValue
        //{

        //}

        public const string ServiceAddress = "http://localhost:5000/odata";

        static void Main(string[] args)
        {
            Console.WriteLine("Ожидание сервера.");

            Thread.Sleep(300);
            
            var context = new DataServiceContext(new Uri(ServiceAddress))
            {
                
            };

            
            context.BuildingRequest += (s, e) => Console.WriteLine(e.RequestUri);
            context.ReceivingResponse += (s, e) => Console.WriteLine($"Response status: {e.ResponseMessage.StatusCode}");


            

            //context.AttachTo("");

            var data = context.CreateQuery<Student>("Students").Expand(s => s.School);

            var query = data.Expand("School");

            var uri = ((DataServiceQuery)query).RequestUri.ToString();

            var items = data.ToArray();

            foreach (var item in items)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
