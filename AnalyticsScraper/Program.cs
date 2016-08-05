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
            var path = "";

            while (1 == 1)
            {
                Console.SetWindowSize(100, 40);

                Console.Clear();
                Console.WriteLine("Welcome to the Analytics parsing application\n By Alex Ruffo, 2016");

                if (path == "")
                {
                    Console.WriteLine("\n What is the file name?  Include the extension (.csv)");
                    Console.WriteLine("Note it must be on your desktop. Hit enter to use the default file: 'file.csv'");
                    path = Console.ReadLine();
                    if (path == "")
                        path = "file.csv";
                    Console.WriteLine("The data file is " + path);
                }

                Console.WriteLine("\n What would you like to do? \n \n");
                Console.WriteLine("Press 1 to build the CSV report");
                Console.WriteLine("Press 2 to break down by location");
                Console.WriteLine("Press 3 to count how many life vs non-life");
                Console.WriteLine("Press 4 to analyze keywords");
                Console.WriteLine("Press 5 to analyze company types");
                Console.WriteLine("Press 6 to analyze titles");
                Console.WriteLine("Press 7 to analyze level based searches");
                Console.WriteLine("Press 8 to analyze language searches");
                Console.WriteLine("Press 9 to analyze advanced searches");

                Console.WriteLine("Press ESC to exit");
                Console.WriteLine("\n \nNote that the parsed data from options 2-9 will be outputed to an excel document in the 'ParsedData' folder on your desktop.");
                Console.WriteLine("If this folder does not exist, it will be created");

                var a = Console.ReadKey();


                List<ParsedRecord> parsed = new List<ParsedRecord>();

                if (a.Key == ConsoleKey.Escape)
                {
                    stuff.portal();

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
                        filePath = filePath + @"\" + path;

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
                                if (nrec.url.Contains("&cType=") && nrec.url.Substring(nrec.url.IndexOf("cType=", 50) + 6) != "&")
                                {
                                    var str = nrec.url.Substring(60);
                                    int s1 = str.IndexOf("cType=") + 6;
                                    //s1 += 50;
                                    int len = str.IndexOf("&", s1);
                                    if (len == -1)
                                        len = str.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.cType = str.Substring(s1, len);
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
                                if (nrec.url.Contains("lang=") && nrec.url.Substring(nrec.url.IndexOf("lang=") + 5) != "&")
                                {
                                    int s1 = nrec.url.ToLower().IndexOf("lang=") + 5;
                                    int len = nrec.url.IndexOf("&", s1);
                                    if (len == -1)
                                        len = nrec.url.Substring(s1).Length;
                                    else
                                        len = len - s1;
                                    nrec.lang = nrec.url.Substring(s1, len);
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
                                Console.WriteLine("Keys: " + nrec.keys + " Type: " + nrec.type + " Country: " + nrec.country + " Province: " + nrec.province + " Corp Type: " + nrec.cType + " Title: " + nrec.title + " lvl min: " + nrec.lvlmin + " max: " + nrec.lvlmax + " Search Type: " + nrec.sType);
                            }
                            //Console.WriteLine("Path: " + filePath);
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
                    if (parsed.Count == 0)
                        parsed = parser(path);

                    var keyInf = new List<string>();
                    int totalSearch = 0; int provSearch = 0; int countrySearch = 0;

                    foreach (var p in parsed)
                    {
                        if (p.country != null && p.country != "")
                        {
                            keyInf.Add(p.country);
                            countrySearch += 1;
                        }
                        totalSearch += 1;
                    }
                    var keyMast = new List<keyInfo>();

                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });

                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> countryrec = new List<GenericDataRecord>();

                    Console.WriteLine("\n There are " + distinctList.Count + " countries");
                    Console.WriteLine("\n \n List of countries from most searched to least");
                    Console.WriteLine(String.Format("           | {0,10} | {1,20} | {2,6} |", "Country ID", "Country", "Count"));

                    nrec.f1 = "Country ID";
                    nrec.f2 = "Country";
                    nrec.f3 = "Count";
                    countryrec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        var country = getCountry(j.key);
                        Console.WriteLine(String.Format("           | {0,10} | {1,20} | {2,6} |", j.key, country, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = country;
                        nrec.f3 = j.count.ToString();
                        countryrec.Add(nrec);
                    }

                    Console.WriteLine("\n Note that a blank country means that it may be disabled");
                    nrec = new GenericDataRecord();
                    nrec.f1 = "______";
                    nrec.f2 = "______";
                    nrec.f3 = "______";
                    countryrec.Add(nrec);

                    //keyInf.Clear();
                    //foreach (var p in parsed)
                    //{
                    //    if (p.province != null && p.province != "")
                    //    {
                    //        keyInf.Add(p.province);
                    //    }

                    //}
                    keyMast.Clear();
                    foreach (var k in parsed)
                    {

                        if (k.province != null && k.country != null && k.country != "0" && k.province != "0")
                        {
                            int count = parsed.Count(x => k.country.Equals(x.country) && k.province.Equals(x.province));
                            //if (count > 1)
                            //{
                            keyMast.Add(new keyInfo
                            {
                                count = count,
                                key = k.province,
                                country = k.country
                            });
                            provSearch += 1;
                        }

                    }
                    distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();

                    Console.WriteLine("\n \n Press any key to continue.. ");
                    Console.ReadKey();

                    Console.WriteLine("\n There are " + distinctList.Count + " provinces");
                    Console.WriteLine("\n \n List of provinces from most searched to least");
                    Console.WriteLine(String.Format("           | {0,10} | {1,12} | {2,8} | {3,20} | {4,6} |", "Country ID", "Province ID", "Country", "Province", "Count"));
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Country ID";
                    nrec.f2 = "Province ID";
                    nrec.f3 = "Country";
                    nrec.f4 = "Province";
                    nrec.f5 = "Count";
                    countryrec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        var country = getProvince(j.country, j.key);
                        var cCode = getCountryCode(j.country);
                        Console.WriteLine(String.Format("           | {0,10} | {1,12} | {2,8} | {3,20} | {4,6} |", j.country, j.key, cCode, country, j.count));
                        nrec.f1 = j.country;
                        nrec.f2 = j.key;
                        nrec.f3 = cCode;
                        nrec.f4 = country;
                        nrec.f5 = j.count.ToString(); ;
                        countryrec.Add(nrec);
                    }
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Country Search";
                    nrec.f2 = "Province Search";
                    nrec.f3 = "Total Search";
                    countryrec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = countrySearch.ToString();
                    nrec.f2 = provSearch.ToString();
                    nrec.f3 = totalSearch.ToString();
                    countryrec.Add(nrec);

                    outputGenericRecord(countryrec, "locationResult");
                }

                else if (a.KeyChar == '3')
                {

                    if (parsed.Count == 0)
                        parsed = parser(path);

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

                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> lrec = new List<GenericDataRecord>();
                    nrec.f1 = "Life";
                    nrec.f2 = "Non-Life";
                    nrec.f3 = "Life: Actuarial";
                    nrec.f4 = "Other";
                    nrec.f5 = "Non-Life: Actuarial";
                    nrec.f6 = "CAT Modeling";
                    nrec.f7 = "Other";
                    lrec.Add(nrec);

                    Console.WriteLine("Life   Non-Life   ->   Life: Actuarial  Other  |   Non-Life: Actuarial  CAT Modeling   Other");
                    Console.WriteLine(clife + "      " + cnlife + "       ->              " + cLA + "       " + cLO + "  |                    " + cNLA + "            " + cNLCM + "      " + cNLO);
                    Console.WriteLine("\nTotal Searches: " + ctotal + " out of " + cAll);

                    nrec = new GenericDataRecord();
                    nrec.f1 = clife.ToString();
                    nrec.f2 = cnlife.ToString();
                    nrec.f3 = cLA.ToString();
                    nrec.f4 = cLO.ToString();
                    nrec.f5 = cNLA.ToString();
                    nrec.f6 = cNLCM.ToString();
                    nrec.f7 = cNLO.ToString();
                    lrec.Add(nrec);

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

                    nrec = new GenericDataRecord();
                    nrec.f1 = "";
                    lrec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Life Count";
                    nrec.f2 = "Non Life Count";
                    nrec.f3 = "";
                    nrec.f4 = "Total Life / nLife";
                    nrec.f5 = "Total Searches";
                    lrec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = clife.ToString();
                    nrec.f2 = cnlife.ToString();
                    nrec.f3 = "";
                    nrec.f4 = ctotal.ToString();
                    nrec.f5 = cAll.ToString();
                    lrec.Add(nrec);

                    Console.WriteLine("\n \nNote: this will only work on a subset of the data, as the url for life and non-life has changed over the releases");
                    Console.WriteLine("This percentage of data will grow as the users of the site end up using the newest builds which have the correct url format");

                    outputGenericRecord(lrec, "Life-nLife_Stats");
                }

                else if (a.KeyChar == '4')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;

                    var keyInf = new List<string>();

                    foreach (var p in parsed)
                    {
                        if (p.keys != null && p.keys != "")
                        {
                            var keyArr = p.keys.Split(',');
                            foreach (var k in keyArr)
                                keyInf.Add(k);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    var keyMast = new List<keyInfo>();

                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                        //}

                        //keyInf.Add(new keyInfo
                        //{
                        //    count = 0,
                        //    key = k
                        //});
                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> krec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n There are " + distinctList.Count + " keywords");
                    Console.WriteLine("\n \n List of Keywords from most used to least");
                    Console.WriteLine(String.Format(" | {0,50} | {1,6} | {2,8} |", "Keyword", "Count", "In List"));

                    nrec.f1 = "Keyword";
                    nrec.f2 = "Count";
                    nrec.f3 = "In List";
                    krec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        //Console.WriteLine(j.key + " | "  + j.count);
                        var isKey = getKeyExist(j.key);
                        Console.WriteLine(String.Format(" | {0,50} | {1,6} | {2,8} |", j.key, j.count, isKey));
                        nrec.f1 = j.key;
                        nrec.f2 = j.count.ToString();
                        nrec.f3 = isKey;
                        krec.Add(nrec);
                    }
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Keyword Searches";
                    nrec.f2 = "";
                    nrec.f3 = "Total Searches";
                    krec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = "";
                    nrec.f3 = totalSearch.ToString();
                    krec.Add(nrec);
                    outputGenericRecord(krec, "keywordResults");

                }
                else if (a.KeyChar == '5')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;
                    var keyMast = new List<keyInfo>();
                    var keyInf = new List<string>();

                    foreach (var p in parsed)
                    {
                        if (p.cType != null && p.cType != "")
                        {
                            var keyArr = p.cType.Split(',');
                            foreach (var k in keyArr)
                                if (k != "")
                                    keyInf.Add(k);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> crec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n There are " + distinctList.Count + " company types");
                    Console.WriteLine("\n \n List of company types from most used to least");
                    Console.WriteLine(String.Format(" | {0,10} | {1,60} | {2,6} |", "Type ID", "Company Type", "Count"));

                    nrec.f1 = "Type ID";
                    nrec.f2 = "Company Type";
                    nrec.f3 = "Count";
                    crec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        //Console.WriteLine(j.key + " | "  + j.count);
                        var corp = getCorpType(j.key);
                        if (j.key == "0")
                            corp = "All";
                        Console.WriteLine(String.Format(" | {0,10} | {1,60} | {2,6} |", j.key, corp, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = corp;
                        nrec.f3 = j.count.ToString();
                        crec.Add(nrec);
                    }
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Corp Type Searches";
                    nrec.f2 = "Total Searches";
                    crec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = totalSearch.ToString();
                    crec.Add(nrec);

                    outputGenericRecord(crec, "corpType_Stats");
                }
                else if (a.KeyChar == '6')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;
                    var keyMast = new List<keyInfo>();
                    var keyInf = new List<string>();

                    foreach (var p in parsed)
                    {
                        if (p.title != null && p.title != "")
                        {
                            var keyArr = p.title.Split(',');
                            foreach (var k in keyArr)
                                if (k != "")
                                    keyInf.Add(k);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> crec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n There are " + distinctList.Count + " titles");
                    Console.WriteLine("\n \n List of titles from most searched to least");
                    Console.WriteLine(String.Format(" | {0,50} | {1,6} |", "Title", "Count"));

                    nrec.f1 = "Title";
                    nrec.f2 = "Count";
                    crec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        //Console.WriteLine(j.key + " | "  + j.count);                       
                        Console.WriteLine(String.Format(" | {0,50} | {1,6} |", j.key, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = j.count.ToString();
                        crec.Add(nrec);
                    }
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Title Searches";
                    nrec.f2 = "Total Searches";
                    crec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = totalSearch.ToString();
                    crec.Add(nrec);

                    outputGenericRecord(crec, "title_Stats");
                }
                else if (a.KeyChar == '7')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;
                    var keyMast = new List<keyInfo>();
                    var keyInf = new List<string>();

                    foreach (var p in parsed)
                    {
                        if (p.lvlmax != null && p.lvlmax != "" && p.lvlmax != "0")
                        {
                            keyInf.Add(p.lvlmax);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    int distCtr = distinctList.Count;

                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> crec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n \n List of levels from most searched to least");
                    Console.WriteLine(String.Format(" | {0,12} | {1,6} |", "Min Level", "Count"));

                    nrec.f1 = "Min Level";
                    nrec.f2 = "Count";
                    //nrec.f3 = "Max Level";
                    //nrec.f4 = "Count";
                    crec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        //Console.WriteLine(j.key + " | "  + j.count);                       
                        Console.WriteLine(String.Format(" | {0,12} | {1,6} |", j.key, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = j.count.ToString();
                        //nrec.f3 = j.country;
                        //nrec.f4 = j.count2.ToString();
                        crec.Add(nrec);
                    }

                    keyInf = new List<string>();
                    keyMast.Clear();
                    foreach (var p in parsed)
                    {
                        if (p.lvlmin != null && p.lvlmin != "" && p.lvlmin != "0")
                        {
                            keyInf.Add(p.lvlmin);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                    }
                    distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    distCtr += distinctList.Count;

                    Console.WriteLine(String.Format("\n | {0,12} | {1,6} |", "Max Level", "Count"));
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Max Level";
                    nrec.f2 = "Count";
                    crec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        Console.WriteLine(String.Format(" | {0,12} | {1,6} |", j.key, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = j.count.ToString();
                        crec.Add(nrec);
                    }
                    Console.WriteLine("\n Total Searches with a level: {0} out of {1}", keySearch, totalSearch);
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Level Searches (min and Max)";
                    nrec.f2 = "Total Searches";
                    crec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = totalSearch.ToString();
                    crec.Add(nrec);

                    outputGenericRecord(crec, "level_Stats");

                }
                else if (a.KeyChar == '8')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;
                    var keyMast = new List<keyInfo>();
                    var keyInf = new List<string>();

                    foreach (var p in parsed)
                    {
                        if (p.lang != null && p.lang != "")
                        {
                            var keyArr = p.lang.Split(',');
                            foreach (var k in keyArr)
                                if (k != "")
                                    keyInf.Add(k);
                            keySearch += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                            totalSearch += 1;
                    }
                    foreach (var k in keyInf)
                    {
                        int count = keyInf.Count(x => k.Equals(x));
                        //if (count > 1)
                        //{
                        keyMast.Add(new keyInfo
                        {
                            count = count,
                            key = k
                        });
                    }
                    var distinctList = keyMast.OrderByDescending(x => x.count).Distinct(new SomeClassComparer()).ToList();
                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> crec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n There are " + distinctList.Count + " unique languages");
                    Console.WriteLine("\n \n List of languages from most searched to least");
                    Console.WriteLine(String.Format(" | {0,50} | {1,6} |", "Language", "Count"));

                    nrec.f1 = "Language";
                    nrec.f2 = "Count";
                    crec.Add(nrec);

                    foreach (var j in distinctList)
                    {
                        nrec = new GenericDataRecord();
                        //Console.WriteLine(j.key + " | "  + j.count);                       
                        Console.WriteLine(String.Format(" | {0,50} | {1,6} |", j.key, j.count));
                        nrec.f1 = j.key;
                        nrec.f2 = j.count.ToString();
                        crec.Add(nrec);
                    }
                    nrec = new GenericDataRecord();
                    nrec.f1 = "Language Searches";
                    nrec.f2 = "Total Searches";
                    crec.Add(nrec);
                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = totalSearch.ToString();
                    crec.Add(nrec);

                    outputGenericRecord(crec, "lang_Stats");
                }
                else if (a.KeyChar == '9')
                {
                    if (parsed.Count == 0)
                        parsed = parser(path);
                    int totalSearch = 0;
                    int keySearch = 0;
                    int tadvctr = 0;
                    int tctr = 0;

                    foreach (var p in parsed)
                    {
                        if (p.lang != null && p.lang != "" || p.lvlmax != null && p.lvlmax != "" && p.lvlmax != "0" || p.lvlmin != null && p.lvlmin != "" && p.lvlmin != "0" || p.cType != null && p.cType != "" && p.cType != "0" || p.title != null && p.title != "")
                        {
                            keySearch += 1;
                            int n = 0;
                            Int32.TryParse(p.pageViews, out n);
                            for (int i = 0; i < n; i++)
                                tadvctr += 1;
                        }
                        if (p.url.Contains("search-listing") || p.url.Contains("Search-Listing"))
                        {
                            totalSearch += 1;
                            int x = 0;
                            Int32.TryParse(p.pageViews, out x);
                            for (int i = 0; i < x; i++)
                                tctr += 1;
                        }
                    }

                    GenericDataRecord nrec = new GenericDataRecord();
                    List<GenericDataRecord> crec = new List<GenericDataRecord>();

                    //keyMast.Distinct().ToList();
                    Console.WriteLine("\n \n There are " + keySearch + " advanced searches out of " + totalSearch + " unique searches");
                    Console.WriteLine("\n There are " + tadvctr + " advanced searches out of " + tctr + " total searches");

                    nrec.f1 = "Advanced Searches";
                    nrec.f2 = "Total Searches";
                    crec.Add(nrec);

                    nrec = new GenericDataRecord();
                    nrec.f1 = keySearch.ToString();
                    nrec.f2 = totalSearch.ToString();
                    crec.Add(nrec);

                    outputGenericRecord(crec, "advanced_Stats");
                }

                // end of program // 
                Console.WriteLine("\n Press any key to return to the menu");
                Console.ReadKey();
            }
        }
        static List<ParsedRecord> parser(string path)
        {
            var engine = new FileHelperEngine<Record>();
            var parseEngine = new FileHelperEngine<ParsedRecord>();

            // To Read Us:e
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //filePath = filePath + @"\file.csv";
            if (Directory.Exists(filePath))
            {
                var outpath = filePath + @"\result.csv";
                filePath = filePath + @"\" + path;
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
                        if (nrec.url.Contains("&cType=") && nrec.url.Substring(nrec.url.IndexOf("cType=", 50) + 6) != "&")
                        {
                            var str = nrec.url.Substring(60);
                            int s1 = str.IndexOf("cType=") + 6;
                            //s1 += 50;
                            int len = str.IndexOf("&", s1);
                            if (len == -1)
                                len = str.Substring(s1).Length;
                            else
                                len = len - s1;
                            nrec.cType = str.Substring(s1, len);
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
                        if (nrec.url.Contains("lang=") && nrec.url.Substring(nrec.url.IndexOf("lang=") + 5) != "&")
                        {
                            int s1 = nrec.url.ToLower().IndexOf("lang=") + 5;
                            int len = nrec.url.IndexOf("&", s1);
                            if (len == -1)
                                len = nrec.url.Substring(s1).Length;
                            else
                                len = len - s1;
                            nrec.lang = nrec.url.Substring(s1, len);
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
        public class keyInfo
        {
            public int count { get; set; }
            public string key { get; set; }
            public string country { get; set; }
        }
        public class SomeClassComparer : IEqualityComparer<keyInfo>
        {
            public bool Equals(keyInfo x, keyInfo y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                //Check whether any of the compared objects is null.
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                {
                    return false;
                }

                //Check whether the SomeClass's properties are equal.
                return x.count == y.count &&
                       x.key == y.key;
            }

            public int GetHashCode(keyInfo someClass)
            {
                //Check whether the object is null
                if (ReferenceEquals(someClass, null))
                {
                    return 0;
                }

                //Get hash code for the fields
                var hashSomeId = someClass.count.GetHashCode();
                var hashAnotherId = someClass.key.GetHashCode();

                //Calculate the hash code for the SomeClass.
                return (hashSomeId ^ hashAnotherId);
            }

        }
        public static string getCountry(string cID)
        {
            using (var db = new StrategicStrikeEntities())
            {
                int n = 0;
                if (Int32.TryParse(cID, out n) == true)
                {
                    if (n != 0)
                    {
                        DateTime tdy = DateTime.Today;
                        var result = (from j in db.Jobs.Where(w => w.jbActive == true && w.jbOnWeb == true && w.jbOnWebFrom <= tdy && w.jbOnWebTo >= tdy)
                                      join c in db.plCountries on j.jbCountryID equals c.countryID
                                      select c).Distinct().OrderBy(o => o.country).ToList();
                        if (result.Count >= 1)
                        {
                            int ctr = 1;
                            foreach (var j in result)
                            {
                                if (ctr == n)
                                    return j.country;
                                ctr += 1;
                            }
                        }
                        return "";
                    }
                    return "";
                }
                else return "";
            }
            //return ;
        }
        public static string getKeyExist(string key)
        {
            using (var db = new StrategicStrikeEntities())
            {
                var lstKeys = db.plJobKeywords.OrderBy(o => o.jkWords).ToList();
                foreach (var j in lstKeys)
                {
                    if (j.jkWords == key)
                        return "Y";
                }
                return "N";
            }
        }
        public static void outputGenericRecord(List<GenericDataRecord> genericrec, string outLoc)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath + @"\ParsedData");
                filePath += @"\ParsedData";
                var outpath = filePath + @"\" + outLoc + ".csv";
                var parseEngine = new FileHelperEngine<GenericDataRecord>();
                parseEngine.WriteFile(outpath, genericrec);
            }
        }
        public static string getCountryCode(string cID)
        {
            using (var db = new StrategicStrikeEntities())
            {
                int n = 0;
                if (Int32.TryParse(cID, out n) == true)
                {
                    if (n != 0)
                    {
                        DateTime tdy = DateTime.Today;
                        var result = (from j in db.Jobs.Where(w => w.jbActive == true && w.jbOnWeb == true && w.jbOnWebFrom <= tdy && w.jbOnWebTo >= tdy)
                                      join c in db.plCountries on j.jbCountryID equals c.countryID
                                      select c).Distinct().OrderBy(o => o.country).ToList();
                        if (result.Count >= 1)
                        {
                            int ctr = 1;
                            foreach (var j in result)
                            {
                                if (ctr == n)
                                    return j.Country_Code;
                                ctr += 1;
                            }
                        }
                        return "";
                    }
                    return "";
                }
                else return "";
            }
            //return ;
        }
        public static string getProvince(string cID, string sID)
        {
            using (var db = new StrategicStrikeEntities())
            {
                int n = 0; int s = 0;
                if (Int32.TryParse(cID, out n) == true)
                {
                    if (n != 0)
                    {
                        Int32.TryParse(sID, out s);
                        if (s != 0)
                        {
                            DateTime tdy = DateTime.Today;
                            var result = (from j in db.Jobs.Where(w => w.jbActive == true && w.jbOnWeb == true && w.jbOnWebFrom <= tdy && w.jbOnWebTo >= tdy)
                                          join c in db.plCountries on j.jbCountryID equals c.countryID
                                          select c).Distinct().OrderBy(x => x.country).ToList();
                            int cid = 0;
                            if (result.Count >= 1)
                            {
                                int ctr = 1;
                                foreach (var j in result)
                                {
                                    if (ctr == n)
                                    {
                                        cid = j.countryID;
                                        break;
                                    }

                                    ctr += 1;
                                }


                                var lst = db.plStates.Where(w => w.stCountryID == cid && w.disabled == false).OrderBy(o => o.stateName).ToList();

                                if (lst.Count >= 1)
                                {
                                    int ctr1 = 1;
                                    foreach (var j in lst)
                                    {
                                        if (ctr1 == s)
                                        {
                                            return j.stateName;
                                        }
                                        ctr1 += 1;
                                    }
                                    return "Not in Database";
                                }
                                return "No States Found";
                            }
                            return "ID not found / disabled";


                        }
                        else return "";
                    }
                    else return "";
                    //return ;
                }
                else return "";
            }
        }

        public static string getCorpType(string cID)
        {
            using (var db = new StrategicStrikeEntities())
            {
                int n = 0;
                if (Int32.TryParse(cID, out n) == true)
                {
                    if (n != 0)
                    {
                        var result = (from l in db.plCompanyCategories
                                      where l.disabled == false
                                      select new { cat = l.ccategory }).OrderBy(l => l.cat).ToList();
                        if (result.Count >= 1)
                        {
                            int ctr = 1;
                            foreach (var j in result)
                            {
                                if (ctr == n)
                                    return j.cat;
                                ctr += 1;
                            }
                            return "Not in Database";
                        }
                        return "Not Found / Invalid ID";
                    }
                    return "";
                }
                else return "";
            }
        }

    }
}
