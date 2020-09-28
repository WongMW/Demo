using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.Search.Models
{
    public class RefinerItem
    {

        private string refinementlabel;

        /// <summary>
        /// Gets or sets the name of the refinement
        /// </summary>
        /// <value>
        /// The name of the refinement  and also of the filter field.
        /// </value>
        public string RefinementName { get; set; }

        /// <summary>
        /// Gets or sets the value of the refinement
        /// </summary>
        /// <value>
        /// The RefinementValue.
        /// </value>
        public string RefinementValue { 
            
            get; 
            
            set;
        }


        //get { return FreightTotal + TaxTotal + LineTotal; }

        /// <summary>
        /// Gets or sets the Refinement Count.
        /// </summary>
        /// <value>The RefinementCount.</value>
        public string RefinementCount { get; set; }

        /// <summary>
        /// Gets or sets the Refinement Url.
        /// </summary>
        /// <value>The RefinementUrl.</value>
        public string RefinementUrl { get; set; }

        public string RefinementLabel {
            get
            {
                return refinementlabel;
            }
            set
            {
                string newRefinementLabel = "";
                string oldRefinementLabel = value;
                if (oldRefinementLabel.StartsWith("_"))
                {
                    oldRefinementLabel = oldRefinementLabel.Substring(1);
                }
                if (oldRefinementLabel.EndsWith("_"))
                {
                    oldRefinementLabel = oldRefinementLabel.Substring(0,oldRefinementLabel.Length-1);
                }
                foreach (string s in oldRefinementLabel.Split('_'))
                {
                    newRefinementLabel = newRefinementLabel + " " + convertCategoryLabels(s.UpperFirstLetter());
                }

                refinementlabel = newRefinementLabel;
            }
        }

        private string convertCategoryLabels(string rawCat)
        {
            if (rawCat.Equals("Roi")) { return "RoI"; }
            else if (rawCat.Equals("Uk")) { return "UK"; }
            else if (rawCat.Equals("Rtp")) { return "RTP"; }
            else if (rawCat.Equals("Rtpenews")) { return "RTPeNews"; }
            else if (rawCat.Equals("Enews")) { return "enews"; }
            else if (rawCat.Equals("Cpd")) { return "CPD"; }
            else if (rawCat.Equals("Rtp")) { return "RTP"; }
            else if (rawCat.Equals("Beps")) { return "BEPS"; }
            else if (rawCat.Equals("Lpt")) { return "LPt"; }
            else if (rawCat.Equals("Taxsource")) { return "TaxSource"; }
            else if (rawCat.Equals("Ixbrl")) { return "ixbrl"; }
            else if (rawCat.Equals("Ifrs")) { return "IFRS"; }
            else if (rawCat.Equals("Q&a")) { return "Q&A"; }
            else if (rawCat.Equals("Ni")) { return "NI"; }
            else { return rawCat; }
        }


    }
}