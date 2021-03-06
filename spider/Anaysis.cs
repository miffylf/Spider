﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace spider
{
    public class Anaysis
    {
        public void GetHerf(Hashtable hashTable, string url)
        {
            HtmlWeb htmlweb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = htmlweb.Load(url);
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
                if (hashTable.ContainsKey(item.Text) == false)
                {
                    hashTable.Add(item.Text, item.Url);
                }
                Console.WriteLine(item);
            }
        }
    }
}
