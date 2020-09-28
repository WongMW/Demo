using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.BusinessFacade.Interfaces.Form.Helper
{
    public interface HelperInterface
    {
        List<TextBox> ClearTextBoxes(List<TextBox> listOfTextBoxes);
        List<RadioButton> UncheckRadioButtons(List<RadioButton> listOfRadios);
        List<CheckBox> UncheckCheckBoxes(List<CheckBox> listOfCheckBoxes);
    }
}
