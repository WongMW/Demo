using SitefinityWebApp.BusinessFacade.Interfaces.Form.Validate;
using SitefinityWebApp.BusinessFacade.Resources;
using SitefinityWebApp.BusinessFacade.Resources.Enum;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.BusinessFacade.Services.Form.Validate
{
    public class GeneralService : GeneralInterface
    {
        public async Task<HtmlGenericControl> MarkAsError(HtmlGenericControl el, bool hasError)
        {
            if (hasError && !el.Attributes["class"].Contains("has-error"))
            {
                el.Attributes["class"] = el.Attributes["class"] + " has-error";
            }
            else if (!hasError)
            {
                el.Attributes["class"] = el.Attributes["class"].Replace("has-error", "");
            }
            return el;
        }

        private async Task<bool> IsFieldValidWithoutRegex(string txt, bool isNullable = false, int min = 0, int max = 0)
        {            
            if ((isNullable))
            {
                return true;
            }
            else
            {
                if (min >= 0 && max > 0)
                {
                    if (txt.Trim().Length > max || txt.Trim().Length < min)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<bool> IsFieldValid(string txt, GeneralEnum.ValidateRegex typeOfRegex, bool isNullable = false, int min = 0, int max = 0)
        {

            var reg = SelectRegex(typeOfRegex);
            var matches = reg.Result.Match(txt.Trim());

            if ((isNullable && !matches.Success))
            {
                return true;
            }
            else
            {
                if (min >= 0 && max > 0)
                {
                    if (txt.Trim().Length > max || txt.Trim().Length < min)
                    {
                        return false;
                    }
                }
            }

            return matches.Success;
        }

        private async Task<bool> IsNumberFieldValid(string txt, GeneralEnum.ValidateRegex typeOfRegex, bool isNullable = false, int sMin = 0, int sMax = 0, int rMin = 0, int rMax = 0)
        {
            var reg = SelectRegex(typeOfRegex);

            var matches = reg.Result.Match(txt.Trim());
            long number;

            bool success = Int64.TryParse(txt.Trim(), out number);
                  
            if (success && matches.Success)
            {
                if (sMin > 0 && sMax > 0)
                {
                    if (txt.Trim().Length > sMax || txt.Trim().Length < sMin)
                    {
                        return false;
                    }
                }

                if (rMin > 0 && rMax > 0)
                {
                    if (Int64.Parse(txt) > rMax || Int64.Parse(txt) < rMin)
                    {
                        return false;
                    }
                }
            }

            return matches.Success;
        }

        //#ADD = 1 REMOVE = 0
        public async Task<string> BuildResultMessage(bool addOrRemove, string errorMessage, string fieldName, GeneralEnum.ValidateMessages typeOfMessage, string htmlText = "")
        {
            switch (typeOfMessage)
            {
                case GeneralEnum.ValidateMessages.ErrorInvalidMessage:
                    if (addOrRemove)
                        errorMessage += string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText);
                    else
                        errorMessage.Replace(string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText), "");
                    break;
                case GeneralEnum.ValidateMessages.ErrorInvalidOrRequiredMessage:
                    if (addOrRemove)
                        errorMessage += string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText);
                    else
                        errorMessage.Replace(string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText), "");
                    break;
                case GeneralEnum.ValidateMessages.ErrorOnlyRequiredMessage:
                    if (addOrRemove)
                        errorMessage += string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText);
                    else
                        errorMessage.Replace(string.Format(ValidateResource.ErrorInvalidMessage, fieldName, htmlText), "");
                    break;
            }
            return errorMessage;
        }

        public async Task<string> FieldIsValid(string fieldName, string text, GeneralEnum.ValidateRegex typeOfRegex, GeneralEnum.ValidateMessages typeOfMessage, string errorMessage, HtmlGenericControl el, bool isNullable = false, string htmlText = "", int sMin = 0, int sMax = 0, int rMin = 0, int rMax = 0)
        {
            bool result = true;

            if (typeOfRegex == GeneralEnum.ValidateRegex.RegexWithoutSpace || typeOfRegex == GeneralEnum.ValidateRegex.RegexWithSpaceAndSpecialCharacters || typeOfRegex == GeneralEnum.ValidateRegex.RegexEmail)
            {
                result = IsFieldValid(text, typeOfRegex, isNullable, sMin, sMax).Result;
            }
            else if (typeOfRegex == GeneralEnum.ValidateRegex.RegexNumbers)
            {
                result = IsNumberFieldValid(text, typeOfRegex, isNullable, sMin, sMax).Result;
            }
            else if (typeOfRegex == GeneralEnum.ValidateRegex.RegexDontNeed)
            {
                result = IsFieldValidWithoutRegex(text, isNullable, sMin, sMax).Result;
            }


            errorMessage = BuildResultMessage((!result), errorMessage, fieldName, typeOfMessage, htmlText).Result;
            el = MarkAsError(el, (!result)).Result;

            return errorMessage;
        }

        private async Task<Regex> SelectRegex(GeneralEnum.ValidateRegex typeOfRegex)
        {
            switch (typeOfRegex)
            {
                case GeneralEnum.ValidateRegex.RegexWithoutSpace:
                    return ValidateResource.RegexWithoutSpace;
                case GeneralEnum.ValidateRegex.RegexWithSpaceAndSpecialCharacters:
                    return ValidateResource.RegexWithSpaceAndSpecialCharacters;
                case GeneralEnum.ValidateRegex.RegexNumbers:
                    return ValidateResource.RegexNumbers;
                case GeneralEnum.ValidateRegex.RegexEmail:
                    return ValidateResource.RegexEmail;
                default:
                    return ValidateResource.RegexWithSpaceAndSpecialCharacters;
            }
        }
    }
}