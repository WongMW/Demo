namespace SitefinityWebApp.BusinessFacade.Resources
{
    public static class SqlResource
    {
        public static string GetWebUserIDByLinkID
        {
            get
            {
                return "EXEC [Aptify].[dbo].[spGetWebUserIDByLinkID__c] @PersonID='{0}'";
            }
        }

        public static string GetPersonDetailsById
        {
            get
            {
                return "{0}..spGetPersonDetails__c @PersonID={1}";
            }
        }


        public static string GetWebUserDetailsByWebUserID
        {
            get
            {
                return "{0}..spGetWebUserDetails__c @WebUserID={1}";
            }
        }
    }
}