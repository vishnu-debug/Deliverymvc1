using Deliverymvc1.Models;
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
    public class CustomerController1 : Controller
    {

        string Baseurl = "https://localhost:44394/";



        public async Task<IActionResult> GetAllProducts()
        {
            List<Customer> CustomerInfo = new List<Customer>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Customers");

                if (Res.IsSuccessStatusCode)
                {
                    var CustomerResponse = Res.Content.ReadAsStringAsync().Result;
                    CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(CustomerResponse);

                }
                return View(CustomerInfo);

            }

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer p)
        {
            Customer Pobj = new Customer();
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Customers", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Pobj = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("customerlogin","Login");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Customer p = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Customers/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(p);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer p)
        {
            Customer p1 = new Customer();
            using (var httpClient = new HttpClient())
            {
                int id = p.CustomerID;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44394/api/Customers/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["CustomerID"] = id;
            Customer e = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44394/api/Customers/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }
            return View(e);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(Customer p)
        {
            int Prid = Convert.ToInt32(TempData["CustomerID"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44394/api/Customers/" + Prid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("GetAllProducts");
        }
    }
}
