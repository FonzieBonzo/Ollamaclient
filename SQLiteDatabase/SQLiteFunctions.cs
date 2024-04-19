using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ollamaclient.SQLiteDatabase.Tables;

namespace Ollamaclient.SQLiteDatabase
{
    public class SQLiteFunctions
    {

        private static SQLiteFunctions instance;
        public SQLiteAsyncConnection db;


        public static SQLiteFunctions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SQLiteFunctions();
                }
                return instance;
            }
        }

        public async Task DBInit(bool Open = true)
        {
            if (!Open)
            {
                if (db != null)
                {
                    await db.CloseAsync();
                    db = null;
                    return;
                }
                return;
            }

            if (db != null)
                return;

            var databasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OllamaClient.db");
            db = new SQLiteAsyncConnection(databasePath);

           await db.CreateTableAsync<SettingRec>();
           await db.CreateTableAsync<PresetRec>();

        }

        public async Task<SettingRec?> SettingGet(string Name, SettingRec? DefaultRecord = null)
        {
            await DBInit();
            SettingRec? TheReturn = null;
            var existingRecord = await db.Table<SettingRec>().FirstOrDefaultAsync(x => x.Name == Name);

            if (existingRecord == null && DefaultRecord != null)
            {
                DefaultRecord.Name = Name;
                // app hangs when making it await
                await db.InsertAsync(DefaultRecord);
                TheReturn = DefaultRecord;
            }
            else
            {
                TheReturn = existingRecord;
            }

            return TheReturn;
        }

        public async Task<bool> SettingSet(SettingRec TheRecord)
        {
            await DBInit();
            var existingRecord = db.Table<SettingRec>().FirstOrDefaultAsync(x => x.Name == TheRecord.Name);

            if (existingRecord.Result == null)
            {
                db.InsertAsync(TheRecord);
            }
            else
            {
                db.UpdateAsync(TheRecord);
            }
            return true;
        }


        public async Task<PresetRec?> PresetRecGet(long Id, PresetRec? DefaultRecord = null)
        {
            await DBInit();
            PresetRec? TheReturn = null;
            var existingRecord = await db.Table<PresetRec>().FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRecord == null && DefaultRecord != null)
            {
                DefaultRecord.Id = Id;
                // app hangs when making it await
                await db.InsertAsync(DefaultRecord);
                TheReturn = DefaultRecord;
            }
            else
            {
                TheReturn = existingRecord;
            }

            return TheReturn;
        }


        internal async Task<PresetRec> PresetRecAddUpdate(PresetRec presetRec)
        {
            await DBInit();

            PresetRec TheReturn = new PresetRec();

            var existingRecord = await db.Table<PresetRec>().FirstOrDefaultAsync(x => x.Id == presetRec.Id);

            if (existingRecord == null)
            {
                await db.InsertAsync(presetRec);
            }
            else
            {
                await db.UpdateAsync(presetRec);
            }

            return TheReturn;
        }
    }
}
