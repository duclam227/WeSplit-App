using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO_WeSplit;

namespace DAO_WeSplit
{
    public class DAO_Member : DBConnect
    {
        private static DAO_Member _instance = new DAO_Member();

        public static DAO_Member Instance
        {
            get
            {
                return _instance;
            }
        }
        public DataTable GetAllMembers()
        {
            DataTable data = new DataTable();
            string query = "select * from Member";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetMemberNameFromID(int id)
        { 
            DataTable data = new DataTable();
            string query = $"select MemberName from Member where MemberID = {id}";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }
    }
}
