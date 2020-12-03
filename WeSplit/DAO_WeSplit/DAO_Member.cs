﻿using System;
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
        public List<DTO_Member> GetAllMembers()
        {
            DataTable data = new DataTable();
            List<DTO_Member> result = new List<DTO_Member>();
            string query = "select * from Member";

            SqlDataAdapter adapter = new SqlDataAdapter(query, _conn);
            adapter.Fill(data);

            foreach(DataRow row in data.Rows)
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
    }
}