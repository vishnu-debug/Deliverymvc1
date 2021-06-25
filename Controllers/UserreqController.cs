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
    public class UserreqController : Controller
    {
        string Baseurl = "https://localhost:44394/";



        public async Task<IActionResult> GetAllProducts()
        {
            List<Userreq> CustomerInfo = new List<Userreq>();

            using (var client = new HttpClient())
            {
                string Baseurl = "https://localhost:44394/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Userreqs");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Userreq>>(CustomerResponse);

                }
                return View(CustomerInfo);

            }

        }
        public async Task<IActionResult> Create()
        {
            List<Customer> req = new List<Customer>();
            List<Executive> res = new List<Executive>();
            using (var client = new HttpClient())
            {
                string Baseurl = "https://localhost:44394/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customers");

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;
                    req = JsonConvert.DeserializeObject<List<Customer>>(UserResponse);

                }

            }
            using (var client = new HttpClient())
            {
                string Baseurl = "https://localhost:44394/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executives");

                if (Res.IsSuccessStatusCode)
                {
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;
                    res = JsonConvert.DeserializeObject<List<Executive>>(UserResponse);

                }

            }
            


            Customer obj1 = (from i in req
                             where i.UserName == HttpContext.Session.GetString("UserName")
                             select i).FirstOrDefault();
            Executive obj2 = (from i in res
                              where i.City == obj1.City
                              select i).FirstOrDefault();
            Customer obj3 = (from i in req
                             where i.Address == obj1.Address
                             select i).FirstOrDefault();

            if (obj1 != null && obj2 != null)
            {
                ViewBag.Message = obj1;
                ViewBag.Message = obj3;
                ViewData["ExecutiveCity"] = obj2.ExecutiveID;
                ViewData["ExecutiveName"] = obj2.Name;

                return View();
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Userreq p)
        {
            Userreq Pobj = new Userreq();
      
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Userreqs", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Pobj = JsonConvert.DeserializeObject<Userreq>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Userreq p = new Userreq();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Userreqs/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Userreq>(apiResponse);
                }
            }
            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Userreq p)
        {
            Userreq p1 = new Userreq();
            using (var httpClient = new HttpClient())
            {
                int id = p.RequestID;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Userreqs/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Userreq>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["RequestID"] = id;
            Userreq e = new Userreq();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Userreqs/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Userreq>(apiResponse);
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Userreq p)
        {
            int Prid = Convert.ToInt32(TempData["RequestID"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Userreqs/" + Prid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("GetAllProducts");
        }
    }
}
