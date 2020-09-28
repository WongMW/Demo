using System;
using Aptify.Framework.Application;
using Aptify.Framework.ExceptionManagement;

namespace SoftwareDesign.GDPR
{
    public class RegisterOption
    {
        private readonly AptifyApplication _aptifyApplication;
        private readonly long _personId;

        public RegisterOption(AptifyApplication aptifyApplication, long personId)
        {
            if (aptifyApplication == null) throw new ArgumentNullException(nameof(aptifyApplication));
            if (personId <= 0) throw new ArgumentOutOfRangeException(nameof(personId));
            _aptifyApplication = aptifyApplication;
            _personId = personId;
        }

        public void SaveOption(TopicCodeOption option)
        {
            var person = _aptifyApplication
                .GetEntityObject("Persons", _personId);

            if (person == null)
                return;
            person.SetValue("TopicCodeLastSelection__sd", GetOptionName(option));
            person.SetValue("TopicCodeLastChange__sd", DateTime.Now);
            person.Save(false);
        }

        public TopicCodeOption? GetLastSelectionGDPRState
        {
            get
            {
                try
                {
                    var person = _aptifyApplication
                    .GetEntityObject("Persons", _personId);

                    if (person == null)
                        return null;

                    var lastSelection = Convert.ToString(person.GetValue("TopicCodeLastSelection__sd"));
                    if (string.IsNullOrEmpty(lastSelection))
                        return null;

                    TopicCodeOption option;
                    if (!Enum.TryParse(lastSelection, out option))
                    {
                        return null;
                    }

                    return option;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }


        public DateTime? LastConfirmedDate()
        {
            try
            {
                var person = _aptifyApplication
                    .GetEntityObject("Persons", _personId);

                if (person == null)
                    return null;

                var lastSelection = Convert.ToString(person.GetValue("TopicCodeLastSelection__sd"));
                if (string.IsNullOrEmpty(lastSelection))
                    return null;

                TopicCodeOption option;
                if (!Enum.TryParse(lastSelection, out option))
                    return null;

                if (option != TopicCodeOption.Confirmed)
                    return null;

                var lastChange = Convert.ToDateTime(person.GetValue("TopicCodeLastChange__sd"));
                return lastChange;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        private static string GetOptionName(TopicCodeOption option)
        {
            return Enum.GetName(typeof(TopicCodeOption), option);
        }
    }

    public enum TopicCodeOption
    {
        Confirmed,
        Later,
        Now
    }
}
