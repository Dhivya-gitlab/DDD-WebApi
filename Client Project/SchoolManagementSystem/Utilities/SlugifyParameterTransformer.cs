using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Utilities
{
    
    public class SlugifyParameterTransformer :IOutboundParameterTransformer
    {
        //Part of URL to identify a particular page from a website
        public string TransformOutbound(object value)
        {
            // Slugify value
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower(CultureInfo.InvariantCulture);
        }
    }
}
