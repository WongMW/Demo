using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Aptify.Framework.ExceptionManagement;
using Aptify.Framework.Web.eBusiness;
using SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.BookList.Designer;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.BookList
{
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(BookListWidgetDesigner))]
    public partial class BookListWidget : BaseUserControlAdvanced
    {
        private string _topicCodes = string.Empty;
        private int _productCount = 10;

        private const string AttributeImageNotAvailableUrl = "ImageNotAvailable";
        private const string AttributeControlDefaultName = "BookListWidget";

        private static string StoredProcedureName
        {
            get { return "spGetBookInfoForTopicCode__sd"; }
        }


        private string ImageNotAvailable
        {
            get
            {
                if (ViewState[AttributeImageNotAvailableUrl] != null)
                {
                    return (string)ViewState[AttributeImageNotAvailableUrl];
                }

                return string.Empty;
            }
            set { ViewState[AttributeImageNotAvailableUrl] = value; }
        }

        
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

        protected void Page_Load(object sender, EventArgs e)
        {
            SetProperties();
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
        }

        protected override void SetProperties()
        {
            if (string.IsNullOrEmpty(ID))
            {
                ID = AttributeControlDefaultName;
            }

            base.SetProperties();

            if (string.IsNullOrEmpty(ImageNotAvailable))
            {
                ImageNotAvailable = GetLinkValueFromXML(AttributeImageNotAvailableUrl);
            }
        }

        private void BindGrid()
        {
            var source = GetDataSource();

            RadGrid1.DataSource = source;
            RadGrid1.DataBind();
        }

        private IList<BookWidgetItem> GetDataSource()
        {
            var ret = new Collection<BookWidgetItem>();

            var dt = GetItemsDt();

            if (dt == null || dt.Rows.Count == 0)
                return ret;


            foreach (DataRow dataRow in dt.Rows)
            {
                var item = new BookWidgetItem()
                {
                    Id = Convert.ToInt32(dataRow["Id"]),
                    Title = Convert.ToString(dataRow["Name"]),
                    ImageUrl = Convert.ToString(dataRow["WebImage"])
                };

                FixImageUrl(item);
                AddProductUrl(item);
                AddPriceToItem(item);

                ret.Add(item);
            }

            return ret;
        }

        private void FixImageUrl(BookWidgetItem item)
        {
            if (string.IsNullOrEmpty(item.ImageUrl))
            {
                item.ImageUrl = ImageNotAvailable;
            }
        }

        private void AddProductUrl(BookWidgetItem item)
        {
            var url = "~/ProductCatalog/Product.aspx?ID=" + item.Id;
            url = ResolveUrl(url);
            item.ProductUrl = url;
        }

        private void AddPriceToItem(BookWidgetItem item)
        {
            var productId = item.Id;
            var memberPriceInfo = ShoppingCart1.GetUserProductPrice(productId);

            var currencyFormat = User1.PreferredCurrencyFormat;
            if (User1.PersonID <= 0)
                currencyFormat = ShoppingCart1.GetCurrencyFormat(2);

            item.Price = memberPriceInfo.Price.ToString(currencyFormat);

            var discount = ShoppingCart1.GetProductMemberSavings(Context.User, productId);
            item.MembershipDiscount = discount.ToString(currencyFormat);
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
                ExceptionManager.Publish(ex);
            }

            return null;
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

        private IDataParameter GetTopicCodesParam(DataTable topicCodes)
        {
            var param = (SqlParameter)DataAction.GetDataParameter("@topicCodes", SqlDbType.Structured, topicCodes);
            param.TypeName = "dbo.tblTopicCodes__sd";
            return param;
        }

        private IDataParameter GetPageSizeParam()
        {
            var param = DataAction.GetDataParameter("@pageSize", SqlDbType.Int, ProductCount);
            return param;
        }
    }

    public class BookWidgetItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string MembershipDiscount { get; set; }
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
