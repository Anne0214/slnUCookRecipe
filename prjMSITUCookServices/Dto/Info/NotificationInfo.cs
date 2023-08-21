using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Dto.Info
{
    public class NotificationInfo
    {
        public int MemberId { get; set; }
        public DateTime NotifyTime { get; set; }

        public DateTime? ReadTime = null;

        public int Type { get; set; }

        public int? RelatedRecipeId { get; set; }
        public int? RelatedOrderId { get; set; }
        public int? RelatedMemberId { get; set; }
    }
}
