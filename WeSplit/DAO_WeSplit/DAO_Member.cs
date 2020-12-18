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
                "insert into dbo.MemberPerTrip(TripID, MemberID, Paid) values " +
                $"('{tripId}', '{memberId}', 0)";

            _conn.Open();
            SqlCommand cmd = new SqlCommand(addMemberPerTrip, _conn);
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void DeleteMemberPerTrip(int tripId, int memberId)
        {
            string deleteMemberPerTrip = $"delete from MemberPerTrip where MemberID = {memberId} and TripID = {tripId}";

            _conn.Open();
            SqlCommand cmd = new SqlCommand(deleteMemberPerTrip, _conn);
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public DataTable GetAmountOfMember()
        {
            DataTable data = new DataTable();
            string query = "select count(MemberID) as Amount from Member";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetAmountOfMember(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select count(MemberID) as Amount from MemberPerTrip where TripID = {tripID}";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetMembersOfTrip(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select MPT.MemberID, MPT.TripID, MPT.Paid, MPT.Paid - T.TripAverage as Change, MPT.Paid, MPT.TripID, MPT.MemberID, M.MemberName, M.MemberDOB, M.MemberSex, M.MemberAvatar " +
                $"from MemberPerTrip MPT, Trip T, Member M " +
                $"where MPT.TripID = T.TripID and T.TripID = {tripID} and M.MemberID = MPT.MemberID";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public DataTable GetAvailableMembers(int tripID)
        {
            DataTable data = new DataTable();
            string query = $"select * from Member where MemberID not in (select Member.MemberID from Member, MemberPerTrip where Member.MemberID = MemberPerTrip.MemberID and MemberPerTrip.TripID = {tripID})";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            return data;
        }

        public void AddMember(DTO_Member member)
        {
            int mS = 0;
            if(member.MemberSex == true)
            {
                mS = 1;
            }

            string addMember = "insert into dbo.Member(MemberID, MemberName, MemberDOB, MemberSex, MemberAvatar) values " +
                $"({member.MemberID}, N'{member.MemberName}', '{member.MemberDOB}', {mS}, '{member.MemberAvatar}')";

            _conn.Open();
            SqlCommand cmd = new SqlCommand(addMember, _conn);
            cmd.ExecuteNonQuery();
            _conn.Close();
        }
    }
}
