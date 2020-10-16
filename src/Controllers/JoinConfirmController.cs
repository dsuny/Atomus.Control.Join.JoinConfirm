using Atomus.Database;
using Atomus.Models;
using Atomus.Service;

namespace Atomus.Controllers
{
    public static class JoinConfirmController
    {
        internal static IResponse Save(this ICore core, JoinConfirmModel search)
        {
            IServiceDataSet serviceDataSet;
            IService service;

            serviceDataSet = new ServiceDataSet { ServiceName = core.GetAttribute("ServiceName") };
            serviceDataSet["Account"].ConnectionName = core.GetAttribute("ConnectionName"); //"RD";
            serviceDataSet["Account"].CommandText = core.GetAttribute("ProcedureSave"); //"[dbo].[USP_CONFIRM_EMAIL]";
            serviceDataSet["Account"].AddParameter("@EMAIL", DbType.NVarChar, 100);
            serviceDataSet["Account"].AddParameter("@KEY", DbType.NVarChar, 50);

            serviceDataSet["Account"].NewRow();
            serviceDataSet["Account"].SetValue("@EMAIL", search.EMAIL);
            serviceDataSet["Account"].SetValue("@KEY", search.KEY);

            service = (IService)Factory.CreateInstance("Atomus.Service.ServerAdapter");

            return service.Request((ServiceDataSet)serviceDataSet);
        }
    }
}