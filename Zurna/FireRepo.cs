using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zurna
{
    public class FireRepo<T> : IFireRepo<T>
    {
        #region Local definations
        private string auth { get; set; }
        private string baseurl { get; set; }
        private string path { get; set; }
        #endregion
        #region FireBase Configuration
        private IFirebaseClient client { get; set; }
        private IFirebaseConfig cfg { get; set; }
        #endregion
        #region Local methods/functions
        private async Task<Dictionary<string, JToken>> GetToken(JObject json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, JToken>>(json.ToString());
        }
        private async Task<JObject> GetObject(string path)
        {
            FirebaseResponse resp = await client.GetAsync(path);
            if (resp.Body == null) return null;
            var data = resp.Body;
            JObject json = JObject.Parse(data);
            return json;
        }
        private async Task<JArray> GetArray(string path)
        {
            FirebaseResponse resp = await client.GetAsync(path);
            if (resp.Body == null) return null;
            var data = resp.Body;
            JArray json = JArray.Parse(data);
            return json;
        }
        #endregion
        public FireRepo(string auth, string baseurl, string path)
        {
            this.auth = auth;
            this.baseurl = baseurl;
            this.path = path;
            cfg = new FirebaseConfig() { AuthSecret = auth, BasePath = baseurl };
            client = new FirebaseClient(cfg);
        }

        public async Task Add(T Model, Guid id)
        {
            string savePath = $"{this.path}/{id}";
            SetResponse setInsert = await client.SetAsync(savePath, Model);
        }

        public async Task Delete(Guid id)
        {
            FirebaseResponse deleteResp
                = await client.DeleteAsync($"{this.path}/{id}");
        }

        public async Task<T> Find(Guid id)
        {
            string findPath = $"{this.path}{id}";
            JObject json = await GetObject(findPath);
            var result = await GetToken(json);
            var data = JsonConvert.SerializeObject(result);
            return JsonConvert.DeserializeObject<T>(data.ToString());

        }

        public async Task<List<T>> GetList()
        {
            JObject json = await GetObject(path);
            var result = await GetToken(json);
            var data = JsonConvert.SerializeObject(result.Select(sa => (JObject)sa.Value));
            List<T> dataResult = JsonConvert.DeserializeObject<List<T>>(data.ToString());
            return dataResult;
        }

        public async Task<List<T>> GetList(int counter = 100)
        {
            JObject json = await GetObject(path);
            var result = await GetToken(json);
            var data = JsonConvert.SerializeObject(result.Select(sa => (JObject)sa.Value));
            List<T> dataResult = JsonConvert.DeserializeObject<List<T>>(data.ToString());
            return dataResult.Take(counter).ToList();
        }

        public async Task<T[]> GetListSeries(int counter = 100)
        {
            if (counter <= 0) return null;
            T[] series = new T[] { };
            JObject json = await GetObject(path);
            var result = await GetToken(json);
            var data = JsonConvert.SerializeObject(result.Select(sa => (JObject)sa.Value));
            series = JsonConvert.DeserializeObject<T[]>(data.ToString());
            if (counter != null && counter >= 0)
                return series.Take(counter).ToArray();
            else return series;
        }

        public async Task<T> Update(Guid id, T Model)
        {
            string findPath = $"{this.path}/{id}";
            FirebaseResponse response
                = await client.UpdateAsync(findPath, Model);
            T result = response.ResultAs<T>();
            return result;
        }
        public bool ConnectionTest()
        {
            if (client == null) return false;
            else return true;
        }

        Task IFireRepo<T>.Add(T Model, Guid id)
        {
            throw new NotImplementedException();
        }

        Task IFireRepo<T>.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<List<T>> IFireRepo<T>.GetList()
        {
            throw new NotImplementedException();
        }

        Task<List<T>> IFireRepo<T>.GetList(int counter)
        {
            throw new NotImplementedException();
        }

        Task<T[]> IFireRepo<T>.GetListSeries(int counter)
        {
            throw new NotImplementedException();
        }

        Task<T> IFireRepo<T>.Find(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<T> IFireRepo<T>.Update(Guid id, T Model)
        {
            throw new NotImplementedException();
        }
    }

}