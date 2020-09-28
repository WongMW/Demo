using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.Search.Models
{
    public class Refiner
    {
        /// <summary>
        /// Gets or sets the Name of the Refiner.
        /// </summary>
        /// <value>The RefinerName.</value>
        public string RefinerName { get; set; }

        /// <summary>
        /// Gets or sets the IsSelected.
        /// </summary>
        /// <value>The IsSelected.</value>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Refiner values
        /// </summary>
        public List<RefinerItem> RefinementItems;
    }
}