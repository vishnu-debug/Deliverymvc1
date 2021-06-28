using Deliverymvc1.Data;
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
    public class ExecutiveresController : Controller
    {
        private readonly DeliverymvcContext _context;
        string Baseurl = "https://localhost:44394/";
        public ExecutiveresController(DeliverymvcContext _Context)
        {
            _context = _Context;
        }



        public async Task<IActionResult> GetAllProducts()
        {
            List<Executiveres> CustomerInfo = new List<Executiveres>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executiveres");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Executiveres>>(CustomerResponse);

                }
                return View(CustomerInfo);

            }

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Executiveres p)
        {
            Executiveres Pobj = new Executiveres();
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Executiveres", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Pobj = JsonConvert.DeserializeObject<Executiveres>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Executiveres p = new Executiveres();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Executiveres/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Executiveres>(apiResponse);
                }
            }
            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Executiveres p)
        {
            Executiveres p1 = new Executiveres();
            using (var httpClient = new HttpClient())
            {
                int id = p.ExrequestID;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Executiveres/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Executiveres>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["ExrequestID"] = id;
            Executiveres e = new Executiveres();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Executiveres/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Executiveres>(apiResponse);
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Executive p)
        {
            int Prid = Convert.ToInt32(TempData["ExrequestID"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Executiveres/" + Prid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("GetAllProducts");
        }



        public async Task<IActionResult> ListAllRequest()
        {
            List<Userreq> UserRequestInfo = new List<Userreq>();
            List<Executive> ExecutiveDetailInfo = new List<Executive>();


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/userreqs");

                if (Res.IsSuccessStatusCode)
                {
                    var UserDetailResponse = Res.Content.ReadAsStringAsync().Result;
                    UserRequestInfo = JsonConvert.DeserializeObject<List<Userreq>>(UserDetailResponse);

                }
                //return View(UserRequestInfo);
            }

            //User Request list
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executives");

                if (Res.IsSuccessStatusCode)
                {
                    var UserRequestResponse = Res.Content.ReadAsStringAsync().Result;
                    ExecutiveDetailInfo = JsonConvert.DeserializeObject<List<Executive>>(UserRequestResponse);

                }

            }

            Executive obj1 = (from i in ExecutiveDetailInfo
                              where i.UserName == HttpContext.Session.GetString("username")
                              select i).FirstOrDefault();

            var obj2 = (from i in UserRequestInfo
                        where i.ExecutiveID == obj1.ExecutiveID
                        select i);
            //Userreq obj3= (from i in UserRequestInfo
            //               where i.WeightOfPackage==obj1.ExecutiveID
            //               select i);

            ViewData["ExecutiveName"] = obj1.Name;
            if (obj2 != null)
            {
                return View(obj2.ToList());
            }
            else
            {
                return View();
            }

        }


        public async Task<IActionResult> AcceptRequest(int reqid)
        {
            //API Requestuser delete


            //Update exe status
            List<Executiveres> Executiveres = new List<Executiveres>();
            List<Userreq> RequestInfo = new List<Userreq>();
            List<Customer> CustomerInfo = new List<Customer>();






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
                    RequestInfo = JsonConvert.DeserializeObject<List<Userreq>>(CustomerResponse);

                }
            }
            using (var client = new HttpClient())
            {
                string Baseurl = "https://localhost:44394/";

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customers");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);

                }
            }
            using (var client = new HttpClient())
            {
                string Baseurl = "https://localhost:44394/";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executiveres");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    Executiveres = JsonConvert.DeserializeObject<List<Executiveres>>(CustomerResponse);

                }
            }

            Userreq obj1 = (from i in RequestInfo
                            where i.RequestID == reqid
                            select i).FirstOrDefault();
            Customer obj2 = (from i in CustomerInfo
                             where i.CustomerID == obj1.CustomerID
                             select i).FirstOrDefault();
            ViewData["RequestID"] = obj1.RequestID;
            ViewData["CustomerName"] = obj2.Name;
            ViewData["Address"] = obj2.Address;
            ViewData["Dateandtimeofpickup"] = obj1.DTofPickup;
            ViewData["CustomerID"] = obj1.CustomerID;

            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/userreqs/" + reqid))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //    }
            //}

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AcceptRequest(Executiveres p)
        {
            New obj= new New();
           
            obj.Status = p.Status;
            obj.Price = p.Price;
            obj.RequestID = p.RequestID;
            obj.CustomerID = p.ExecutiveID;
            New obj2 = new New();
           
            string Baseurl = "https://localhost:44394/";
            Executiveres Pobj = new Executiveres();
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Executiveres", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj2= JsonConvert.DeserializeObject<New>(apiResponse);
                }



            }
            return RedirectToAction("show");
        }
        public async Task<IActionResult> Show()
        {
            List<New> obj1 = new List<New>();
            //List<Customer> Customerinfo = new List<Customer>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Executiveres");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    obj1 = JsonConvert.DeserializeObject<List<New>>(CustomerResponse);
                }
            }
            return View(obj1);
        }
    }
}







            //    public IActionResult RejectRequest()
            //    {

            //    }
            //} 

