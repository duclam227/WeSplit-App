using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_WeSplit;
using DAO_WeSplit;

namespace BUS_WeSplit
{
    public class BUS_Member
    {
        private static BUS_Member _instance = new BUS_Member();
        public static BUS_Member Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<DTO_Member> GetAllMembers()
        {
            DataTable data = new DataTable();
            List<DTO_Member> result = new List<DTO_Member>();

            data = DAO_Member.Instance.GetAllMembers();

            foreach (DataRow row in data.Rows)
            {
                int id = int.Parse(row["MemberID"].ToString());
                string name = row["MemberName"].ToString();
                DateTime tmpDOB = (DateTime)row["MemberDOB"];
                string dob = String.Format("{0:dd/MM/yyyy}", tmpDOB);
                bool sex = (bool)row["MemberSex"];
                string avatar = row["MemberAvatar"].ToString();

                DTO_Member tmpMember = new DTO_Member(id, name, dob, sex, avatar);
                result.Add(tmpMember);
            }

            return result;
        }

        public string GetMemberNameFromID(int id)
        {
            DataTable data = new DataTable();
            string result;

            data = DAO_Member.Instance.GetMemberNameFromID(id);

            DataRow row = data.Rows[0];
            result = row["MemberName"].ToString();

            return result;
        }
    }
}
