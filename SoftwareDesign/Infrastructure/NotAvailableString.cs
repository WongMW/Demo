using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.Infrastructure
{
    public class NotAvailableString
    {
        private readonly string _data;
        private const string DefaultMessage = "N/A";

        public NotAvailableString(string data = null, string defaultMessage = DefaultMessage)
        {
            _data = string.IsNullOrWhiteSpace(data) ?
                defaultMessage :
                data;
        }

        public static implicit operator NotAvailableString(string data)
        {
            return new NotAvailableString(data);
        }

        public static implicit operator string(NotAvailableString data)
        {
            return data == null ? 
                DefaultMessage : 
                data._data;
        }
    }
}