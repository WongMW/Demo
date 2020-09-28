using System;
using System.Collections.Generic;
using System.Data;
using Aptify.Applications.OrderEntry;
using Aptify.Framework.Web.eBusiness;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{

    public partial class SDProdListAll : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        protected const string ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL = "ImageNotAvailable";
        RadGrid ProdGrid
        {
            get { return (RadGrid) this.FindControl("ProdGrid"); }
        }

        AptifyShoppingCart ShoppingCart1
        {
            get { return (AptifyShoppingCart)this.FindControl("ShoppingCart1"); }
        }

        private String ImageNotAvailable
        {
            get {
                if (ViewState[ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL] != null)
                {
                    return ViewState[ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL].ToString();
                }
                return string.Empty;
            }
            set {
                ViewState[ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL] = this.FixLinkForVirtualPath(value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageNotAvailable = this.GetLinkValueFromXML(ATTRIBUTE_IMAGE_NOT_AVAILABLE_URL);

            // Manually register the event-handling method for the  
            // ItemDataBound event of the DataGrid control.
            ProdGrid.ItemDataBound += ProdGridOnItemDataBound;

            String CurrencyType = "Euro";

            String sProdctsList = Database + "..SpGetAllProductsForCurrency__cai";
            List<IDataParameter> parameterList = new List<IDataParameter>();
            parameterList.Add(DataAction.GetDataParameter("@CurrencyType", SqlDbType.VarChar, CurrencyType));
            DataTable producTable = DataAction.GetDataTableParametrized(sProdctsList, CommandType.StoredProcedure,
                parameterList.ToArray());

            ProdGrid.DataSource = producTable;
            ProdGrid.DataBind();
        }

        private void ProdGridOnItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        public IProductPrice.PriceInfo GetProductPrice(long lProductID)
        {
            return ShoppingCart1.GetUserProductPrice(lProductID, 1);
        }
    }
}
