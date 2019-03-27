
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Zurna.Controllers
{
    public class PostController : Controller
    {
        #region Firebase Repo
        private string _authentication = "Q20dKxiR9WNE0PCtSQMByaabvHecwizEyzjD7mUb";
        private string _baseurl = "https://zurna-dbc48.firebaseio.com/";
        private FireRepo<Post> _repo;
        #endregion
        
        // GET: Post
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://77.223.142.42/plesk-site-preview/azorlua.com/api/");
            //HttpResponseMessage responsePost = await client.PostAsync("posts",new Post,CancellationToken);
            HttpResponseMessage responseTask = await client.GetAsync("posts");
            string result = await responseTask.Content.ReadAsStringAsync();
            JArray jsonArray = JArray.Parse(result);
            //HttpResponseMessage responsePost = await client.PostAsync("posts",new Post,CancellationToken);
            var client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://77.223.142.42/plesk-site-preview/azorlua.com/api/hashtags/");
            HttpResponseMessage responseTask2 = await client2.GetAsync("getpopulartags");
            string result2 = await responseTask2.Content.ReadAsStringAsync();
            JArray jsonArray2 = JArray.Parse(result2);

            //To store result of web api response.   
            //_repo = new FireRepo<Post>(_authentication, _baseurl, $"{typeof(Post).Name.ToString()}/");
            //List<Post> result = await _repo.GetList();

            ViewData["MyData"] = jsonArray; // Send this list to the view
            ViewData["MyData2"] = jsonArray2; // Send this list to the view
            
            return View("Index");
        }

        // GET: Post/Details/5
        public async System.Threading.Tasks.Task<ActionResult> Details(string id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://77.223.142.42/plesk-site-preview/azorlua.com/api/");
            HttpResponseMessage responseTask = await client.GetAsync("posts/" + id);
            string result = await responseTask.Content.ReadAsStringAsync();
            Post p = JsonConvert.DeserializeObject<Post>(result.ToString());
            ViewData["MyData"] = p; // Send this list to the view
            ViewBag.aptal = result.ToString();
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(Post post)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://77.223.142.42/plesk-site-preview/azorlua.com/api/");
                //HttpResponseMessage x = await client.PostAsync("posts", post);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
