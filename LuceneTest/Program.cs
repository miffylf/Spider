using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using Lucene.Net.Messages;
using Lucene.Net.QueryParsers;
using HtmlAgilityPack;
using System.Collections;

namespace LuceneTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable hashtable = new Hashtable();
            HtmlWeb htmlweb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = htmlweb.Load("http://www.2345.com/");
            var linksOnPage = from lnks in document.DocumentNode.Descendants()
                              where lnks.Name == "a" &&
                              lnks.Attributes["href"] != null &&
                              lnks.InnerText.Trim().Length > 0
                              select new
                              {
                                  Url = lnks.Attributes["href"].Value,
                                  Text = lnks.InnerText
                              };
            foreach (var item in linksOnPage)
            {
                if (hashtable.ContainsKey(item.Text) == false)
                {
                    hashtable.Add(item.Text, item.Url);
                }
                Console.WriteLine(item);
            }
            Console.ReadKey();
            #region 创建索引
            Analyzer anaylyzer = new StandardAnalyzer();
            IndexWriter writer = new IndexWriter("IndexDirectory", anaylyzer, true);
            CreatIndex.AddDocument(writer, "SQL Server 2008 的发布", "SQL Server 2008 的新特性");
            CreatIndex.AddDocument(writer, "ASP.Net MVC框架配置与分析", "而今，微软推出了新的MVC开发框架。");
            writer.Optimize();
            writer.Close();
            #endregion

            #region 搜索
            Analyzer anay = new StandardAnalyzer();
            IndexSearcher search = new IndexSearcher("IndexDirectory");
            MultiFieldQueryParser parser = new MultiFieldQueryParser(new string[] { "title", "content" }, anay);
            Query query = parser.Parse("sql");
            Hits hits = search.Search(query);
            for (int i = 0; i < hits.Length(); i++)
            {
                Document doc = hits.Doc(i);
                Console.WriteLine(string.Format("title:{0} content:{1}", doc.Get("title"), doc.Get("content")));
            }
            search.Close();
            Console.ReadKey();
            #endregion
        }


    }
}
