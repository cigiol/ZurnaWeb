using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zurna
{
    
        public class Post
        {
            public Guid id { get; set; }
            public Content content { get; set; }
            public List<Comment> comments { get; set; }
            public Guid userid { get; set; }
        }
        public class Content
        {
            public string text { get; set; }
            public string image { get; set; }
            public string hashtag { get; set; }
            public Location location { get; set; }
            public DateTime time { get; set; }
            public long like { get; set; }
            public long dislike { get; set; }
            public long view { get; set; }
        }
        public class Comment
        {
            public string text { get; set; }
            public DateTime time { get; set; }
            public Guid userid { get; set; }
        }
        public class Location
        {
            public string lat { get; set; }
            public string lon { get; set; }
            public string country { get; set; }
            public string city { get; set; }
        }



}
