using SitefinityWebApp.BusinessFacade.Interfaces.Form.Helper;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.BusinessFacade.Services.Form.Helper
{
    public class HelperService : HelperInterface
    {
        public List<TextBox> ClearTextBoxes(List<TextBox> listOfTextBoxes)
        {
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Text = string.Empty;
            }

            return listOfTextBoxes;
        }

        public List<RadioButton> UncheckRadioButtons(List<RadioButton> listOfRadios)
        {
            foreach (var radio in listOfRadios)
            {
                radio.Checked = false;
            }

            return listOfRadios;
        }

        public List<CheckBox> UncheckCheckBoxes(List<CheckBox> listOfCheckBoxes)
        {
            foreach (var checkBox in listOfCheckBoxes)
            {
                checkBox.Checked = false;
            }

            return listOfCheckBoxes;
        }
    }
}