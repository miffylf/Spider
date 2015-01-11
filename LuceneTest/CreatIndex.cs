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
using Lucene.Net.Messages;

namespace LuceneTest
{
    public class CreatIndex
    {
        public static void AddDocument(IndexWriter writer, string title, string content)
        {
            Document doc = new Document();
            doc.Add(new Field("title", title, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("content", content, Field.Store.YES, Field.Index.TOKENIZED));
            writer.AddDocument(doc);
        }
    }
}
