using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WeSplit
{
    class DTO_Member
    {
        private int _memberID;
        private string _memberName;
        private string _memberDOB;
        private char _memberSex;
        private string _memberAvatar;

        public int MemberID { get => _memberID; set => _memberID = value; }
        public string MemberName { get => _memberName; set => _memberName = value; }
        public string MemberDOB { get => _memberDOB; set => _memberDOB = value; }
        public char MemberSex { get => _memberSex; set => _memberSex = value; }
        public string MemberAvatar { get => _memberAvatar; set => _memberAvatar = value; }

        public DTO_Member()
        {

        }
        public DTO_Member(int id, string name, string dob, char sex, string avatar)
        {
            MemberID = id;
            MemberName = name;
            MemberDOB = dob;
            MemberSex = sex;
            MemberAvatar = avatar;
        }
    }
}
