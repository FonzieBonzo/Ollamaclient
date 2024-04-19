using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ollamaclient.SQLiteDatabase
{
    public class Tables
    {

        public class SettingRec
        {
            [PrimaryKey]
            public string Name { get; set; }

            public string ValueString { get; set; }
            public int ValueInt { get; set; }
            public Double ValueDouble { get; set; }
            public DateTime ValueDateTime { get; set; }
            public bool ValueBool { get; set; }
        }

        public class PresetRec
        {
            [PrimaryKey]
            public long Id { get; set; }
            public string Modal { get; set; }
            public string Prompt { get; set; }
           
            public string Keys { get; set; }

        }
    }
}
