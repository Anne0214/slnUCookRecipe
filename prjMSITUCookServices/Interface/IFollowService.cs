using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Interface
{
    public interface IFollowService
    {
        public int GetFanCount(int id);

        public int GetFollowerCount(int id);

        public List<int> GetWhoFollowMe(int id);
    }
}
