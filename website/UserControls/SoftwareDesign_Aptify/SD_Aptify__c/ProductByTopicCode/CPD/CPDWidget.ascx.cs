using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Aptify.Framework.ExceptionManagement;
using Aptify.Framework.Web.eBusiness;
using SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD.Designer;
using System.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD
{
    /// <summary>
    /// Class used to create custom page widget
    /// </summary>
    /// <remarks>
    /// If this widget is a part of a Sitefinity module,
    /// you can register it in the site's toolbox by adding this to the module's Install/Upgrade method(s):
    /// initializer.Installer
    ///     .Toolbox(CommonToolbox.PageWidgets)
    ///         .LoadOrAddSection(SectionName)
    ///             .SetTitle(SectionTitle) // When creating a new section
    ///             .SetDescription(SectionDescription) // When creating a new section
    ///             .LoadOrAddWidget<CPDWidget>("CPDWidget")
    ///                 .SetTitle("LinkBoxWidget")
    ///                 .SetDescription("LinkBoxWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(CPDWidgetDesigner))]
    public partial class CPDWidget : BaseUserControlAdvanced, ICallbackEventHandler
    {

        #region Properties

        public string TopicCodes
        {
            get { return _topicCodes; }
            set { _topicCodes = value; }
        }

        public int ProductCount
        {
            get { return _productCount; }
            set { _productCount = value; }
        }

        private string _topicCodes = string.Empty;
        private int _productCount = 10;
        private string _storedProcedureName = "spGetOfflineCPDInfoForTopicCode__sd";

        #endregion

        private string m_callbackResult = "";

        public void RaiseCallbackEvent(string eventArgument)
        {
            String output = "";
            object s = GetDataSource();
            if (s != null)
            {
                IList<CpdWidgetItem> l = (IList<CpdWidgetItem>)s;
                if(l != null && l.Count > 0)
                {
                    foreach (CpdWidgetItem item in l)
                    {
                        AddPriceToItem(item);
                        if(!String.IsNullOrEmpty(output))
                        {
                            output += ";";
                        }
                        output += item.Price;
                    }
                }
            }

            m_callbackResult = output;
        }

        public string GetCallbackResult()
        {
            return m_callbackResult;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BindGrid();
                }
                catch (Exception exception)
                {
                    ExceptionManager.Publish(exception);
                    lblMsg.Text = "Error loading products:" + exception.Message;
                }
                
            }

            OnPageLoad();
        }

        protected virtual void OnPageLoad()
        {
            
        }

        private void BindGrid()
        {
            var source = GetDataSource();

            RadGrid1.DataSource = source;
            RadGrid1.DataBind();
        }

        private IList<CpdWidgetItem> GetDataSource()
        {
            var ret = new Collection<CpdWidgetItem>();

            var dt = GetItemsDt();

            if (dt == null || dt.Rows.Count == 0)
                return ret;

            foreach (DataRow dataRow in dt.Rows)
            {
                var item = new CpdWidgetItem()
                {
                    ProductId = Convert.ToInt32(dataRow["ProductId"]),
                    Title = Convert.ToString(dataRow["Name"]),
                    StartDate = Convert.ToDateTime(dataRow["StartDate"]),
                    CpdHours = Convert.ToInt32(dataRow["EducationUnit"]),
                    Location = Convert.ToString(dataRow["Location"])
                };

                AddProductUrl(item);
                //AddPriceToItem(item);

                ret.Add(item);
            }

            return ret;
        }

        private void AddPriceToItem(CpdWidgetItem item)
        {
            var productId = item.ProductId;
            var memberPriceInfo = ShoppingCart1.GetUserProductPrice(productId);

            var currencyFormat = User1.PreferredCurrencyFormat;
            if (User1.PersonID <= 0)
                currencyFormat = ShoppingCart1.GetCurrencyFormat(2);

            item.Price = memberPriceInfo.Price.ToString(currencyFormat);

            var savings =  ShoppingCart1.GetProductMemberSavings(Context.User, productId);
            item.MembershipDiscount = savings.ToString(currencyFormat);
        }

        private void AddProductUrl(CpdWidgetItem item)
        {
            var url = "~/Meetings/Meeting.aspx?ID=" + item.ProductId;
            url = ResolveUrl(url);
            item.ProductUrl = url;
        }

        private DataTable GetItemsDt()
        {
            var parameters = new List<IDataParameter>();
            try
            {
                var topicCodes = GetTopicCodes();
                var sql = string.Format("{0}..{1}", AptifyApplication.GetEntityBaseDatabase("Products"),
                    StoredProcedureName);

                parameters.Add(GetTopicCodesParam(topicCodes));
                parameters.Add(GetPageSizeParam());

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                return dt;
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return null;
        }

        private IDataParameter GetPageSizeParam()
        {
            var param = DataAction.GetDataParameter("@pageSize", SqlDbType.Int, ProductCount);
            return param;
        }

        private IDataParameter GetTopicCodesParam(DataTable topicCodes)
        {
            var param = (SqlParameter)DataAction.GetDataParameter("@topicCodes", SqlDbType.Structured, topicCodes);
            param.TypeName = "dbo.tblTopicCodes__sd";
            return param;
        }

        private DataTable GetTopicCodes()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("TopicCode", typeof(string)));
            foreach (var topicCode in TopicCodes.Split(','))
            {
                dt.Rows.Add(topicCode);
            }

            return dt;
        }

        public void HideLocationColumn()
        {
            var column = RadGrid1.Columns.FindByUniqueName("Location");
            column.Visible = false;
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void UpdateStoredProcedureName(string spName)
        {
            _storedProcedureName = spName;
        }

        #region Private members & constants

        protected virtual string StoredProcedureName
        {
            get { return _storedProcedureName; }
        }

        #endregion
    }

    public class CpdWidgetItem
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int CpdHours { get; set; }
        public string Location { get; set; }
        public string ProductUrl { get; set; }
        public string Price { get; set; }
        public string MembershipDiscount { get; set; }
    }
}
