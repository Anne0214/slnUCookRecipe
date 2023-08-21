using prjMSITUCookServices.EFModels;
using prjMSITUCookServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Implement
{
    public class FollowService:IFollowService
    {
        private readonly UcookRecipeDatabaseContext _context;
        public FollowService(UcookRecipeDatabaseContext context)
        {
            _context = context;
        }
        public int GetFanCount(int id)
        {
            var fanList = _context.Follow追蹤s.Where(x => x.FollowedMemberId被追蹤者Fk == id); //被追蹤者都是id
            return fanList.Count();
        }
        public int GetFollowerCount(int id)
        {
            var followList = _context.Follow追蹤s.Where(x => x.FollowerMemberId追蹤者Fk == id);
            return followList.Count();
        }

        public List<int> GetWhoFollowMe(int id) { 
            List<int> followMe = _context.Follow追蹤s
                                        .Where(x => x.FollowedMemberId被追蹤者Fk == id)
                                        .Select(y=>y.FollowerMemberId追蹤者Fk)
                                        .ToList();
            return followMe;            
        }
    }
}
