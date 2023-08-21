using prjMSITUCookServices.Dto.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMSITUCookServices.Interface
{
    public interface IIndexService
    {
        HomeIndexResultModel GetIndexContent();
    }
}
