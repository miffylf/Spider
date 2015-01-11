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
namespace LuceneTest
{
    class Program
    {
        static void Main(string[] args)
        {
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
