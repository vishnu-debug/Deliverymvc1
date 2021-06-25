using Deliverymvc1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Deliverymvc1.Controllers
{
    public class ExecutiveController : Controller
    {
        string Baseurl = "https://localhost:44394/";



        public async Task<IActionResult> GetAllProducts()
        {
            List<Executive> CustomerInfo = new List<Executive>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executives");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Executive>>(CustomerResponse);

                }
                return View(CustomerInfo);

            }

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Executive p)
        {
            Executive Pobj = new Executive();
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Executives", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Pobj = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Executive p = new Executive();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Executives/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Executive p)
        {
            Executive p1 = new Executive();
            using (var httpClient = new HttpClient())
            {
                int id = p.ExecutiveID;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Executives/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["ExecutiveID"] = id;
            Executive e = new Executive();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Executives/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Executive>(apiResponse);
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Executive p)
        {
            int Prid = Convert.ToInt32(TempData["ExecutiveID"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Executives/" + Prid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("GetAllProducts");
        }
        //public async Task<IActionResult> Citymap()
        //{
        //    List<Customer> req = new List<Customer>();
        //    List<Executive> res = new List<Executive>();
        //    using (var client = new HttpClient())
        //    {
        //        string Baseurl = "https://localhost:44394/";

        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("api/Customers");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var UserResponse = Res.Content.ReadAsStringAsync().Result;
        //            req = JsonConvert.DeserializeObject<List<Customer>>(UserResponse);

        //        }

        //    }
        //    using (var client = new HttpClient())
        //    {
        //        string Baseurl = "https://localhost:44394/";

        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("api/Customers");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var UserResponse = Res.Content.ReadAsStringAsync().Result;
        //            res = JsonConvert.DeserializeObject<List<Executive>>(UserResponse);

        //        }

        //    }


        //    Customer obj1 = (from i in req
        //                     where i.UserName == HttpContext.Session.GetString("Name")
        //                     select i).FirstOrDefault();
        //    Executive obj2 = (from i in res
        //                      where i.City == obj1.City
        //                      select i).FirstOrDefault();
        //    if (obj1 != null && obj2 != null)
        //    {
        //        ViewBag.Message = obj1;
        //        ViewData["ExecutiveCity"] = obj2.ExecutiveID;
        //        return View();
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //    [HttpPost]
            
        //    public async Task<IActionResult> Citymap(Userreq R)
        //    {
        //        Userreq Robj = new Userreq();
                
        //        using (var httpClient = new HttpClient())
        //        {
        //            httpClient.BaseAddress = new Uri(Baseurl);
        //            StringContent content = new StringContent(JsonConvert.SerializeObject(R), Encoding.UTF8, "application/json");



        //            using (var response = await httpClient.PostAsync("api/UserRequests/PostUserRequest", content))
        //            {
        //                string apiResponse = await response.Content.ReadAsStringAsync();
        //                Robj = JsonConvert.DeserializeObject<Userreq>(apiResponse);
        //            }
        //        }
        //        return RedirectToAction("home");
        //    }



        
    }
}
