using BA_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BA_App.DataAPI
{
    public class RDepartment
    {
        public static List<Departments> GetListDepartment()
        {
            //List<Nhanvien> result = new List<Nhanvien>();
            //ResponseModel<List<Nhanvien>> resultAPI = new ResponseModel<List<Nhanvien>>();
            //var uri = "'http://192.168.108.2:8080/api/Home";            
            try
            {
                List<Departments> result = new List<Departments>();
                //ResponseModel<List<Nhanvien>> resultAPI = new ResponseModel<List<Nhanvien>>();
                var uri = "http://192.168.0.106:8080/api/Department";
                HttpClient client = new HttpClient();
                string urlParameters = "";
                client.BaseAddress = new Uri(uri);                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStreamAsync();
                    Stream dataStreamResponse = dataObjects.Result;
                    StreamReader tReader = new StreamReader(dataStreamResponse);
                    string sResponseFromServer = tReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<List<Departments>>(sResponseFromServer);

                }
                return result;

            }
            catch (Exception ex)
            {
                return new List<Departments>();
            }
        }
    } 
}
