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
    public class CRU_User
    {
        public static string CreateUser(User user)
        {
            string result = "Server đang lỗi !!!";
            ResponseModel<bool> resultAPI = new ResponseModel<bool>();
            try
            {
                string urlapi = "http://192.168.108.2:8080/api/Login";
                using (var wc = new HttpClient())
                {
                    //wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {VariableSharing._token}");
                    var modelString = JsonConvert.SerializeObject(user);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync(urlapi, content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<bool>>(jsonResult);
                }
                if (resultAPI != null)
                {
                    result = resultAPI.Message;
                }
            }
            catch (Exception ex)
            {
                //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                return result;
            }
            return result;
        }
        //Thong tin nguoi dung
        public static ResponseModel<User> GetInfoUser(Login login)
        {
            
            ResponseModel<User> resultAPI = new ResponseModel<User>();
            try
            {
                string url = "http://192.168.108.2:8080/api/Login/Test";
                using (var wc = new HttpClient())
                {
                    //wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {VariableSharing._token}");
                    var modelString = JsonConvert.SerializeObject(login);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                    resultAPI = JsonConvert.DeserializeObject<ResponseModel<User>>(jsonResult);
                }
                if (resultAPI != null)
                {
                    return resultAPI;
                }
            }
            catch (Exception ex)
            {
                //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                return resultAPI;
            }
            return resultAPI;
        }
        //Thay doi mk
        public static ResponseModel<User> ChangeUser(Login login)
        {

            ResponseModel<User> newpassAPI = new ResponseModel<User>();
            try
            {
                string url = "http://192.168.108.2:8080/api/Login/ChangePass";
                using (var wc = new HttpClient())
                {
                    //wc.DefaultRequestHeaders.Add("Authorization", $"Bearer {VariableSharing._token}");
                    var modelString = JsonConvert.SerializeObject(login);
                    var content = new StringContent(modelString, Encoding.UTF8, "application/json");
                    var jsonResult = wc.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                    newpassAPI = JsonConvert.DeserializeObject<ResponseModel<User>>(jsonResult);
                }
                if (newpassAPI != null)
                {
                    return newpassAPI;
                }
            }
            catch (Exception ex)
            {
                //FileHelper.GeneratorFileByDay(ex.ToString(), MethodBase.GetCurrentMethod().Name);
                return newpassAPI;
            }
            return newpassAPI;
        }
    }
}
