using SVRGN.Libs.Contracts.Service.Threshold;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVRGN.Libs.Implementations.Service.Threshold
{
    public class Threshold : IThreshold
    {
        #region Properties

        public float Minimum { get; private set; }

        public float Maximum { get; private set; }

        public string Text { get; private set; }

        #endregion Properties

        #region Construction

        public Threshold(float Minimum, float Maximum, string Text)
        { 
            this.Minimum = Minimum;
            this.Maximum = Maximum;
            this.Text = Text;
        }

        #endregion Construction
    }
}
