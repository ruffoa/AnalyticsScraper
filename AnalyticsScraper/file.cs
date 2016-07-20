using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsScraper
{
    [DelimitedRecord(",")]
    [IgnoreFirst(6)]
    [IgnoreEmptyLines]
    public class Record
    {
        [FieldOptional]
        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string url;

        [FieldOptional]
        public string pageViews;

        [FieldOptional]
        public string uniqueViews;

        [FieldOptional]
        public string avgTime;

        [FieldOptional]
        public string Entrances;

        [FieldOptional]
        public string bounceRate;

        [FieldOptional]
        public string Exitpercent;

        [FieldOptional]
        public string pgValue;

        [FieldOptional]
        public String OptionalValue;
        [FieldOptional]
        public String Optional1;
        [FieldOptional]
        public String Optional2;
        [FieldOptional]
        public String Optional3;
        [FieldOptional]
        public String Optional4;
    }

    [DelimitedRecord(",")]
    public class ParsedRecord
    {
        [FieldOptional]
        [FieldQuoted('"', QuoteMode.OptionalForBoth, MultilineMode.AllowForRead)]
        public string url;

        [FieldOptional]
        public string keys;
        [FieldOptional]
        public string country;
        [FieldOptional]
        public string province;
        [FieldOptional]
        public string type;
        [FieldOptional]
        public string cType;
        [FieldOptional]
        public string sType;
        [FieldOptional]
        public string title;
        [FieldOptional]
        public string lvlmin;
        [FieldOptional]
        public string lvlmax;
        [FieldOptional]
        public string max;
        [FieldOptional]
        public string lang;

        [FieldOptional]
        public string pageViews;

        [FieldOptional]
        public string uniqueViews;

        [FieldOptional]
        public string avgTime;

        [FieldOptional]
        public string Entrances;

        [FieldOptional]
        public string bounceRate;

        [FieldOptional]
        public string Exitpercent;

        [FieldOptional]
        public string pgValue;

        [FieldOptional]
        public String OptionalValue;
        [FieldOptional]
        public String Optional1;
        [FieldOptional]
        public String Optional2;
        [FieldOptional]
        public String Optional3;
        [FieldOptional]
        public String Optional4;

        public static implicit operator ParsedRecord(Record v)
        {
            ParsedRecord x = new ParsedRecord();
            x.url = v.url;
            x.pageViews = v.pageViews;
            x.uniqueViews = v.uniqueViews;
            x.Exitpercent = v.Exitpercent;
            x.Entrances = v.Entrances;
            x.bounceRate = v.bounceRate;
            x.avgTime = v.avgTime;
            x.pgValue = v.pgValue;

            return x;
        }

    }
}
