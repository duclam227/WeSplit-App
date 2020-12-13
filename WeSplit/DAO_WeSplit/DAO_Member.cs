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

        public DataRow GetMember(int id)
        {
            DataTable data = new DataTable();
            DataRow result;
            string query = $"select * from Member where MemberID='{id}'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);
            if (data.Rows.Count > 0)
            {
                result = data.Rows[0];
                return result;

            }
            else 
                return null;
        }

        public void AddMemberPerTrip(int memberId, int tripId)
        {
            string addMemberPerTrip =
                "insert into dbo.MemberPerTrip(TripID, MemberID) values " +
                $"('{tripId}', '{memberId}')";

            SqlCommand cmd = new SqlCommand(addMemberPerTrip, _conn);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetAmountOfMember()
        {
            DataTable data = new DataTable();
            string query = "select count(MemberID) as Amount from Member";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetMembersOfTrip(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select * from Member, MemberPerTrip where Member.MemberID = MemberPerTrip.MemberID and TripID = {tripID}";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }
    }
}
