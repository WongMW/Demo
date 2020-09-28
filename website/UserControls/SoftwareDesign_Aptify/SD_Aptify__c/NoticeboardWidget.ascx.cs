using System;
using System.Text;
using System.Web.UI.WebControls;
using Aptify.Framework.Web.eBusiness;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI;


namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class NoticeboardWidget : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        AptifyWebUserLogin WebUserLogin1
        {
            get { return (AptifyWebUserLogin)this.FindControl("WebUserLogin1"); }
        }

        GridView MessagesGrid
        {
            get { return (GridView)this.FindControl("MessagesGrid"); }
        }

        DropDownList SubjectDropDown
        {
            get { return (DropDownList)this.FindControl("SubjectDropDown"); }
        }

        RadDatePicker DateFromPicker
        {
            get { return (RadDatePicker)this.FindControl("DateFromPicker"); }
        }

        RadDatePicker DateToPicker
        {
            get { return (RadDatePicker)this.FindControl("DateToPicker"); }
        }

        RadWindow ViewMessageModal
        {
            get { return (RadWindow)FindControl("ViewMessageModal"); }
        }

        Label TitleLabel
        {
            get { return (Label)ViewMessageModal.ContentContainer.FindControl("TitleLabel"); }
        }

        Label DateLabel
        {
            get { return (Label)ViewMessageModal.ContentContainer.FindControl("DateLabel"); }
        }

        Label SubjectLabel
        {
            get { return (Label)ViewMessageModal.ContentContainer.FindControl("SubjectLabel"); }
        }

        Label MessageLabel
        {
            get { return (Label)ViewMessageModal.ContentContainer.FindControl("MessageLabel"); }
        }
        GridView AttachmentsGrid
        {
            get { return (GridView)ViewMessageModal.ContentContainer.FindControl("AttachmentsGrid"); }
        }


        private Label CountLabel
        {
            get { return (Label)this.FindControl("CountLabel"); }
        }

        public DateTime GetFirstSeptForYear(int year)
        {
            return new DateTime(year, 9, 1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var personID = WebUserLogin1.User.PersonID;
            DateTime now = DateTime.Today;
            DateTime firstSeptember = GetFirstSeptForYear(now.Year);

            if (now < firstSeptember)
            {
                int year = now.Year - 1;
                firstSeptember = GetFirstSeptForYear(year);
            }

            DateFromPicker.MinDate = firstSeptember;

            if (personID != -1)
            {
                if (!Page.IsPostBack)
                {
                    GetNoticesForUser(personID, -1, null, null);
                    GetSubjects();
                    //DateFromPicker.MinDate =;
                }
                else
                {
                    string dateFrom = "";
                    string dateTo = "";

                    if (DateFromPicker.SelectedDate != null)
                    {
                        dateFrom = ((DateTime)DateFromPicker.SelectedDate).ToString("yyyy-dd-MM HH:mm:ss");
                    }
                    if (DateToPicker.SelectedDate != null)
                    {
                        dateTo = ((DateTime)DateToPicker.SelectedDate).ToString("yyyy-dd-MM HH:mm:ss");
                    }

                    int selectedSubject = Int32.Parse(SubjectDropDown.SelectedItem.Value);
                    GetNoticesForUser(personID, selectedSubject, dateFrom, dateTo);
                }
            }
        }

        private void GetSubjects()
        {
            StringBuilder sql = new StringBuilder();
          //  sql.AppendFormat("select * from Aptify.dbo.StudentMessagingSubject__cai");
            sql.AppendFormat("EXEC [dbo].[spGetStudentMessagingSubjectsall__cai]");
            DataTable result = DataAction.GetDataTable(sql.ToString());

            DataRow newRow = result.NewRow();
            newRow[0] = "0";
            newRow[1] = "Please Select";
            result.Rows.InsertAt(newRow, 0);

            SubjectDropDown.DataSource = result;
            SubjectDropDown.DataTextField = "Name";
            SubjectDropDown.DataValueField = "ID";
            SubjectDropDown.DataBind();
        }

        private void GetNoticesForUser(long personId, int selectedSubject, string dateFrom, string dateTo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("EXEC [dbo].[spGetMessagingForByPersonID__cai] @PersonID = {0}", personId);

            if (selectedSubject > 0)
            {
                sql.AppendFormat(", @SubjectID = {0}", selectedSubject);
            }

            if (!string.IsNullOrEmpty(dateFrom))
            {
                sql.AppendFormat(", @DateFrom = '{0}'", dateFrom);
            }

            if (!string.IsNullOrEmpty(dateTo))
            {
                sql.AppendFormat(", @DateTo = '{0}'", dateTo);
            }
            DataTable result = DataAction.GetDataTable(sql.ToString());
            MessagesGrid.DataSource = result;
            MessagesGrid.DataBind();
            CountLabel.Text = result.Rows.Count.ToString();
            ViewMessageModal.VisibleOnPageLoad = false;
        }

        protected void MessagesGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            DataTable table = (DataTable)MessagesGrid.DataSource;
            var data = table.Rows[row.RowIndex];

            TitleLabel.Text = (string)data["Title"];
            SubjectLabel.Text = (string)data["Subject"];
            DateLabel.Text = ((DateTime)data["PublicationDate"]).ToString("dd/MM/yyyy");
            MessageLabel.Text = (string)data["Message"];

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("[dbo].[spGetAttachmentsForStudentMessage__cai] @MessageID = {0}", data["ID"]);

            DataTable attachmentResult = DataAction.GetDataTable(sql.ToString());

            AttachmentsGrid.DataSource = attachmentResult;
            AttachmentsGrid.DataBind();

            ViewMessageModal.VisibleOnPageLoad = true;
        }

        protected void OnClick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            var id = btn.CommandArgument;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("EXEC [dbo].[spGetAttachmentWithBlob] @ID = {0}", id);
            DataTable result = DataAction.GetDataTable(sql.ToString());

            Byte[] data = (Byte[])result.Rows[0]["BlobData"];
            string fileName = (string)result.Rows[0]["Name"];

            Response.Clear();
            Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Content-Description", "File Download");
            Response.AddHeader("Content-Type", "application/force-download");
            Response.AddHeader("Content-Transfer-Encoding", "binary\n");
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.BinaryWrite(data);
            Response.End();
        }
    }
}
