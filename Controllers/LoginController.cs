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
using System.Threading.Tasks;

namespace Deliverymvc1.Controllers
{
    public class LoginController : Controller
    {
        private readonly DeliverymvcContext _db;
        
        public LoginController(DeliverymvcContext db)
        {
            _db = db;
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(Login l)
        {
            Admin obj = (from i in _db.Admin
                         where i.Name == l.username && i.Password == l.password
                         select i).FirstOrDefault();
            if(obj!=null)
            {
                string name = obj.Name;
                HttpContext.Session.SetString("Name", name);
                return RedirectToAction("mycustomer", "Admins");

            }
            else
            {
                return View();
            }
        }


        public IActionResult customerlogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> customerlogin(Login l)
        {
            List<Customer> UserInfo = new List<Customer>();

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
                    UserInfo = JsonConvert.DeserializeObject<List<Customer>>(UserResponse);

                }

            }






            Customer obj = (from i in UserInfo
                       where i.UserName == l.username && i.Password == l.password
                         select i).FirstOrDefault();
            if (obj != null)
            {
                string name = obj.UserName;
                HttpContext.Session.SetString("UserName", name);
                return RedirectToAction("Create", "Userreq");

            }
            else
            {
                return View();
            }
        }

        public IActionResult executivelogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> executivelogin(Login m)
        {
            List<Executive> ExecutiveInfo = new List<Executive>();

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
                    ExecutiveInfo = JsonConvert.DeserializeObject<List<Executive>>(UserResponse);

                }

            }


                       Executive obj = (from i in ExecutiveInfo
                            where i.UserName == m.username && i.Password == m.password
                            select i).FirstOrDefault();
            if (obj != null)
            {
                string name = obj.UserName;
                HttpContext.Session.SetString("username", name);
                return RedirectToAction("Create", "Executiveres");

            }
            else
            {
                return View();
            }
        }

    }

}
