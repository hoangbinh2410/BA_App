using BA_App.Model;
using BA_App.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BA_App.DataAPI
{
   public static class CRUD_Nhanvien
    {
        // danh sach nhan vien
        public static List<Nhanvien> GetListWorker()
        {
            //List<Nhanvien> result = new List<Nhanvien>();
            //ResponseModel<List<Nhanvien>> resultAPI = new ResponseModel<List<Nhanvien>>();
            //var uri = "'http://192.168.108.2:8080/api/Home";
            //HttpClient client = new HttpClient();
            //string urlParameters = "";
            //client.BaseAddress = new Uri(uri);
            ////string URL = VariableSharing.ServerApiUrl + VariableSharing.GetListWorker;
            ////string urlParameters = "";
            ////HttpClient client = new HttpClient();
            ////client.BaseAddress = new Uri(URL);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                List<Nhanvien> result = new List<Nhanvien>();
                //ResponseModel<List<Nhanvien>> resultAPI = new ResponseModel<List<Nhanvien>>();
                var uri = "http://192.168.108.2:8080/api/Home";
                HttpClient client = new HttpClient();
                string urlParameters = "";
                client.BaseAddress = new Uri(uri);
                //string URL = VariableSharing.ServerApiUrl + VariableSharing.GetListWorker;
                //string urlParameters = "";
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStreamAsync();
                    Stream dataStreamResponse = dataObjects.Result;
                    StreamReader tReader = new StreamReader(dataStreamResponse);
                    string sResponseFromServer = tReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<List<Nhanvien>>(sResponseFromServer);
                    
                }
                return result;

            }
            catch (Exception ex)
            {
                return new List<Nhanvien>();
            }
           

        }
        // Them nv
        public static bool AddNewWorker(Nhanvien nhanvien)
        {
            bool result = false;
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            try
            {
               
                string url = "http://192.168.108.2:8080/api/Home";
                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(nhanvien);
                using (var wc = new HttpClient())
                {
                    var modelString = JsonConvert.SerializeObject(nhanvien);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(jsonResult);
                }
                if (resultAPI.success)
                {
                    result = resultAPI.data;
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }
        //update nv
        public static bool UpdateWorker(Nhanvien nhanvien)
        {
            //bool result = false;
            ResponseModel<Nhanvien> resultAPI = new ResponseModel<Nhanvien>();
            try
            {
                string url = "http://192.168.108.2:8080/api/Home/UpdateNv";
                using (var wc = new HttpClient())
                {
                    //wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {VariableSharing._token}");
                    var modelString = JsonConvert.SerializeObject(nhanvien);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<Nhanvien>>(jsonResult);
                }
                if (resultAPI != null)
                {
                    return resultAPI.success;
                }
            }
            catch (Exception ex)
            {
               // FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                return resultAPI.success;
            }
            return resultAPI.success;
        }
        //Xoa nv
        public static bool DeleteWorker(int IDnv)
        {
            bool result = false;
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            string URL = "http://192.168.108.2:8080/api/Home/DeleteNv/";
            string urlParameters = $"{IDnv}";
            //string urlParameters = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStreamAsync();
                    Stream dataStreamResponse = dataObjects.Result;
                    StreamReader tReader = new StreamReader(dataStreamResponse);
                    string sResponseFromServer = tReader.ReadToEnd();
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(sResponseFromServer);
                }
                if (resultAPI !=null)
                {
                    result = resultAPI.success;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                return result;
            }
            return result;
        }
    }
}
