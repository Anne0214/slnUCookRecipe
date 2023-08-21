using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.ResultModel
{
    public class MemberResultModel
    {
        public int MemberId { get; set; }
        public string Email { get; set; }

        public string NickName { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
