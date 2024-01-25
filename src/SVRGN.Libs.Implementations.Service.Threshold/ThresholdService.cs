using SVRGN.Libs.Contracts.Service.Threshold;
using System;
using System.Collections.Generic;
using System.Text;

namespace SVRGN.Libs.Implementations.Service.Threshold
{
    public class ThresholdService : IThresholdService
    {
        #region Properties

        public List<IThreshold> Thresholds { get; private set; }

        public string DefaultText { get; private set; }

        #endregion Properties

        #region Construction

        public ThresholdService()
        { 
            this.Thresholds = new List<IThreshold>();
        }

        #endregion Construction

        #region Methods

        #region Add
        public bool Add(IThreshold Threshold)
        {
            bool result = false;
            bool mayAdd = true;

            foreach (IThreshold existingThreshold in this.Thresholds)
            {
                if ((existingThreshold.Minimum >=  Threshold.Minimum) && (existingThreshold.Minimum <= Threshold.Maximum)) 
                { 
                    mayAdd = false;
                }

                if ((existingThreshold.Maximum >= Threshold.Minimum) && (existingThreshold.Maximum <= Threshold.Maximum))
                {
                    mayAdd = false;
                }
            }

            if (mayAdd)
            {
                this.Thresholds.Add(Threshold);
                result = true;
            }

            return result;
        }
        #endregion Add

        #region Clear
        public void Clear()
        {
            this.Thresholds.Clear();
        }
        #endregion Clear

        #region GetText
        public string GetText(float Value)
        {
            string result = this.DefaultText;

            foreach (IThreshold threshold in this.Thresholds)
            {
                if (threshold.Minimum <= Value && threshold.Maximum >= Value)
                { 
                    result = threshold.Text;
                    break;
                }
            }

            return result;
        }
        #endregion GetText

        #region SetDefaultText
        public void SetDefaultText(string NewText)
        {
            this.DefaultText = NewText;
        }
        #endregion SetDefaultText

        #endregion Methods
    }
}
