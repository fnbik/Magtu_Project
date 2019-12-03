using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using System.Linq;

namespace Model
{
    public class ParserHtml
    {
        public static async Task<News[]> ParseNews(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);

            List<News> news = new List<News>();

            var titlePostList = document.QuerySelectorAll("h3[data-region-content=forum-post-core-subject]");
            var authorPostList = document.QuerySelectorAll("h3[data-region-content=forum-post-core-subject]+address a");
            var contentPostList = document.QuerySelectorAll(".post-content-container");

            for (int i = 0; i < titlePostList.Length; i++)
            {
                news.Add(new News(titlePostList[i].TextContent, authorPostList[i].TextContent, "",
                            new StringBuilder(contentPostList[i].TextContent)));
            }

            return news.ToArray();
        }
    }

    public class News
    {
        public string Title { get; }
        public string Author { get; }
        public string PublishDate { get; }
        public StringBuilder Content { get; }

        public News(string title, string author, string publishDate, StringBuilder content)
        {
            Title = title;
            Author = author;
            PublishDate = publishDate;
            Content = content;
        }
    }
}
