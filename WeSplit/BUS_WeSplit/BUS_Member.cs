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

        public static List<DTO_Member> GetAllMembers()
        {
            List<DTO_Member> result = new List<DTO_Member>();

            result = DAO_Member.Instance.GetAllMembers();

            return result;
        }
    }
}
