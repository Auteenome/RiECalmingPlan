using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiECalmingPlan.SQLite {
    public interface ISQLite {
        Task<SQLiteAsyncConnection> GetConnection();
        Task<SQLiteAsyncConnection> ResetDatabase();
    }
}
