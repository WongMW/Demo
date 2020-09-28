using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation;
using Telerik.Sitefinity.Workflow.Activities;

namespace SitefinityWebApp.CustomWorkflows
{
    public class CustomExecuteCodeActivity: ExecuteCodeActivity
    {
        protected override void Execute(System.Activities.CodeActivityContext context)
        {
            var dataContext = context.DataContext;
            var masterFluent = dataContext.GetProperties()["masterFluent"].GetValue(dataContext) as AnyDraftFacade;

            var masterFull = masterFluent.Get();
            if (masterFluent.IsPublished())
            {

                var temp = masterFluent.CheckOut();
                var newsItemTempVersion = temp.Get();
                newsItemTempVersion.ExpirationDate = System.DateTime.Now.AddYears(2);

                temp.CheckIn().SaveChanges();
            }
        }
    }
}