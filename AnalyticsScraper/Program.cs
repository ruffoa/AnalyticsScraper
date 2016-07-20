using FileHelpers;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            while (1 == 1)
            {
                Console.SetWindowSize(100, 40);

                Console.Clear();
                Console.WriteLine("Welcome to the Analytics parsing application\n By Alex Ruffo, 2016");
                Console.WriteLine("\n What would you like to do? \n \n");
                Console.WriteLine("Press 1 to build the CSV report");
                Console.WriteLine("Press 2 to break down by location");
                Console.WriteLine("Press 3 to count how many life vs non-life");
                Console.WriteLine("Press 4 to analyze keywords");
                Console.WriteLine("Press ESC to exit");


                var a = Console.ReadKey();

                List<ParsedRecord> parsed = new List<ParsedRecord>();

                if (a.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                if (a.Key == ConsoleKey.D1 || a.Key == ConsoleKey.NumPad1)
                {
                    var engine = new FileHelperEngine<Record>();
                    var parseEngine = new FileHelperEngine<ParsedRecord>();

                    //engine.BeforeReadRecord += (sender, args) => args.RecordLine = args.RecordLine.Replace(@"""", "'");

                    // To Read Use:
                    string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    //filePath = filePath + @"\file.csv";
                    if (Directory.Exists(filePath))
                    {
                        var outpath = filePath + @"\result.csv";
                        filePath = filePath + @"\file.csv";

                        var result = engine.ReadFile(filePath);
                        ParsedRecord nrec = new ParsedRecord();
                        nrec.pgValue = "Page Value";
                        nrec.url = "URL";
                        nrec.uniqueViews = "Unique Page Views";
                        nrec.pageViews = "Page Views";
                        nrec.Exitpercent = "Exit Percent";
                        nrec.Entrances = "Entrances";
                        nrec.bounceRate = "Bounce Rate";
                        nrec.avgTime = "Average Time";
                        nrec.keys = "Keywords";
                        nrec.type = "Type";
                        nrec.country = "Country";
                        nrec.province = "Province";
                        nrec.sType = "Search Type";
                        nrec.cType = "Corp Type";
                        nrec.lvlmax = "lvlMax";
                        nrec.lvlmin = "lvlMin";
                        parsed.Add(nrec);

                        foreach (Record rec in result)
                        {
                            Console.WriteLine("Page Info:");
                            Console.WriteLine(rec.url + " - " +
                                              rec.pageViews.ToString());
                            nrec = rec;
                            //return rec.Cast<ParsedRecord>();

                            if (nrec.url.Contains("search-listing") || nrec.url.Contains("Search-Listing"))
                            {
                                if (nrec.url.Contains("keys=") && nrec.url.Substring(nrec.url.IndexOf("keys=") + 5) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("keys=") + 5;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.keys = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("type=") && nrec.url.Substring(nrec.url.IndexOf("type=") + 5) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("type=") + 5;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.type = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("country=") && nrec.url.Substring(nrec.url.IndexOf("country=") + 8) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("country=") + 8;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.country = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("province=") && nrec.url.Substring(nrec.url.IndexOf("province=") + 9) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("province=") + 9;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.province = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("cType=") && nrec.url.Substring(nrec.url.IndexOf("cType=") + 6) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("cType=") + 6;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.cType = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("title=") && nrec.url.Substring(nrec.url.IndexOf("title=") + 6) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("title=") + 6;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.title = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("min=") && nrec.url.Substring(nrec.url.IndexOf("min=") + 4) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("min=") + 4;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.lvlmin = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("max=") && nrec.url.Substring(nrec.url.IndexOf("max=") + 4) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("max=") + 4;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.lvlmax = nrec.url.Substring(s1, len);
                                }
                                if (nrec.url.Contains("display=") && nrec.url.Substring(nrec.url.IndexOf("display=") + 8) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("display=") + 8;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.sType = nrec.url.Substring(s1, len);
                                }
                                Console.WriteLine("Keys: " + nrec.keys + " Type: " + nrec.type + " Country: " + nrec.country + " Province: " + nrec.province + " Corp Type: " + nrec.cType + " Title: " + nrec.title +" lvl min: " + nrec.lvlmin + " max: " + nrec.lvlmax +  " Search Type: " + nrec.sType);

                            }
                            int s = 0;
                            Int32.TryParse(nrec.pageViews, out s);
                            //for (int i=0; i< s;i++)
                            //{
                            //    //if (i > 0)
                            //    //{
                            //    //    nrec.pageViews = "-";
                            //    //    nrec.uniqueViews = "-";
                            //    //}
                            //    parsed.Add(nrec);
                            //}
                            parsed.Add(nrec);
                        }
                        // result is now an array of Customer

                        // To Write Use:
                        parseEngine.WriteFile(outpath, parsed);
                    }
                    else
                    {
                        Console.WriteLine("Please have the data in a file called 'file.csv' on your desktop. \n Path is: " + filePath);
                    }

                }
                else if (a.KeyChar == '2')
                {
                   

                }

                else if (a.KeyChar == '3')
                {

                    if (parsed.Count == 0)
                        parsed = parser();

                    Console.WriteLine("\n \nStats on Job Type - Unique URLs: ");
                    int clife = 0, cnlife = 0, ctotal = 0, cAll = 0, cLA = 0, cLO = 0, cNLA = 0, cNLCM = 0, cNLO = 0;
                    foreach (ParsedRecord rec in parsed)
                    {
                        if (rec.url.Contains("search-listing") || rec.url.Contains("Search-Listing"))
                        {
                            if (rec.type != "" && rec.type != null)
                            {
                                string[] rectype = rec.type.Split(',');

                                if (rectype.Contains("1") || rectype.Contains("Life - Actuarial") || rectype.Contains("Life - Other") || rectype.Contains("2"))
                                {
                                    if (rectype.Contains("1"))
                                        cLA += 1;
                                    if (rectype.Contains("Life - Actuarial"))
                                        cLA += 1;
                                    if (rectype.Contains("Life - Other"))
                                        cLO += 1;
                                    if (rectype.Contains("2"))
                                        cLA += 1;
                                    clife += 1;
                                }
                                if (rectype.Contains("3") || rectype.Contains("Non-Life - Actuarial") || rectype.Contains("Non-Life - CAT Modeling") || rectype.Contains("4") || rectype.Contains("5") || rectype.Contains("Non-Life - Other"))
                                {
                                    if (rectype.Contains("3"))
                                        cNLA += 1;
                                    if (rectype.Contains("Non-Life - Actuarial"))
                                        cNLA += 1;
                                    if (rectype.Contains("Non-Life - CAT Modeling"))
                                        cNLCM += 1;
                                    if (rectype.Contains("4"))
                                        cNLCM += 1;
                                    if (rectype.Contains("5"))
                                        cNLO += 1;
                                    if (rectype.Contains("Non-Life - Other"))
                                        cNLO += 1;

                                    cnlife += 1;
                                }
                                ctotal += 1;
                            }
                        }
                        cAll += 1;
                    }
                    Console.WriteLine("Life   Non-Life   ->   Life: Actuarial  Other  |   Non-Life: Actuarial  CAT Modeling   Other");
                    Console.WriteLine(clife + "      " + cnlife + "       ->              "+ cLA+ "       " + cLO+ "  |                    " + cNLA + "            " + cNLCM + "      " + cNLO);
                    Console.WriteLine("\nTotal Searches: " + ctotal + " out of " + cAll);

                    Console.WriteLine("\n \nStats on Job Type - All URLs: ");
                    clife = 0; cnlife = 0; cAll = 0; ctotal = 0;
                    foreach (ParsedRecord rec in parsed)
                    {
                        if (rec.type != "" && rec.type != null)
                        {
                            string[] rectype = rec.type.Split(',');
                            if (rectype.Contains("1") || rectype.Contains("Life - Actuarial") || rectype.Contains("Life - Other") || rectype.Contains("2"))
                            {
                                int s = 0;
                                Int32.TryParse(rec.pageViews, out s);
                                if (s > 1)
                                {
                                    for (int i = 0; i < s; i++)
                                    {
                                        ctotal += 1;
                                        clife += 1;
                                        cAll += 1;
                                    }
                                }
                                else
                                {
                                    ctotal += 1;
                                    clife += 1;
                                    cAll += 1;
                                }

                            }
                            if (rectype.Contains("3") || rectype.Contains("Non-Life - Actuarial") || rectype.Contains("Non-Life - CAT Modeling") || rectype.Contains("4") || rectype.Contains("5") || rectype.Contains("Non-Life - Other"))
                            {
                                int s = 0;
                                Int32.TryParse(rec.pageViews, out s);
                                if (s > 1)
                                {
                                    for (int i = 0; i < s; i++)
                                    {
                                        ctotal += 1;
                                        cnlife += 1;
                                        cAll += 1;
                                    }
                                }
                                else
                                {
                                    ctotal += 1;
                                    cnlife += 1;
                                    cAll += 1;
                                }
                            }
                        }
                        else
                            cAll += 1;
                    }
                    Console.WriteLine("Life     Non-Life");
                    Console.WriteLine(clife + "        " + cnlife);
                    Console.WriteLine("\nTotal Searches: " + ctotal + " out of " + cAll);

                    Console.WriteLine("\n \nNote: this will only work on a subset of the data, as the url for life and non-life has changed over the releases");
                    Console.WriteLine("This percentage of data will grow as the users of the site end up using the newest builds which have the correct url format");
                }



                // end of program // 
                Console.WriteLine("\n Press any key to return to the menu");
                Console.ReadKey();
            }
        }
        static List<ParsedRecord> parser()
        {
            var engine = new FileHelperEngine<Record>();
            var parseEngine = new FileHelperEngine<ParsedRecord>();

            // To Read Use:
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //filePath = filePath + @"\file.csv";
            if (Directory.Exists(filePath))
            {
                var outpath = filePath + @"\result.csv";
                filePath = filePath + @"\file.csv";
                List<ParsedRecord> parsed = new List<ParsedRecord>();

                var result = engine.ReadFile(filePath);
                ParsedRecord nrec = new ParsedRecord();

                foreach (Record rec in result)
                {

                    nrec = rec;

                    if (nrec.url.Contains("search-listing") || nrec.url.Contains("Search-Listing"))
                    {
                        if (nrec.url.Contains("keys=") && nrec.url.Substring(nrec.url.IndexOf("keys=") + 5) != "&")
                        {
                            int s1 = nrec.url.ToLower().IndexOf("keys=") + 5;
                            int len = nrec.url.IndexOf("&", s1);
                            if (len == -1)
                                len = nrec.url.Substring(s1).Length;
                            else
                                len = len - s1;
                            nrec.keys = nrec.url.Substring(s1, len);
                        }
                        if (nrec.url.Contains("type=") && nrec.url.Substring(nrec.url.IndexOf("type=") + 5) != "&")
                        {
                            int s1 = nrec.url.ToLower().IndexOf("type=") + 5;
                            int len = nrec.url.IndexOf("&", s1);
                            if (len == -1)
                                len = nrec.url.Substring(s1).Length;
                            else
                                len = len - s1;
                            nrec.type = nrec.url.Substring(s1, len);
                        }
                    }
                    //int s = 0;
                    //Int32.TryParse(nrec.pageViews, out s);
                    //for (int i=0; i< s;i++)
                    //{
                    //    //if (i > 0)
                    //    //{
                    //    //    nrec.pageViews = "-";
                    //    //    nrec.uniqueViews = "-";
                    //    //}
                    //    parsed.Add(nrec);
                    //}
                    parsed.Add(nrec);
                }
                return parsed;
            }
            else
            {
                Console.WriteLine("Please have the data in a file called 'file.csv' on your desktop. \n Path is: " + filePath);
                return null;
            }

        }
    }
}
