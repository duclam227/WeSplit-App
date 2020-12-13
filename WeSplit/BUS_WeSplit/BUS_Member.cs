using DAO_WeSplit;
using DTO_WeSplit;
using System;
using System.Collections.Generic;
using System.Data;

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
            List<DTO_Member> result = new List<DTO_Member>();

            result = DAO_Member.Instance.GetAllMembers();

            return result;
        } 

        public DTO_Member GetMember(int id)
        {
            DataRow row = DAO_Member.Instance.GetMember(id);
            if (row!=null)
            {
                string name = row["MemberName"].ToString();
                DateTime tmpDOB = (DateTime)row["MemberDOB"];
                string dob = String.Format("{0:dd/MM/yyyy}", tmpDOB);
                bool sex = (bool)row["MemberSex"];
                string avatar = row["MemberAvatar"].ToString();
                DTO_Member result = new DTO_Member(id, name, dob, sex, avatar);
                return result;
            }
            else 
                return null;
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
