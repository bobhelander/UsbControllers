using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbController
{
    public partial class Controller
    { 
        /// <summary>
        /// The buffer used when getting the feature report.
        /// </summary>
        private readonly byte[] featureBuffer;

        /// <summary>
        /// Gets the prefered length for the feature buffer.
        /// </summary>
        public int FeatureLength => this.stream.Capabilities.FeatureReportByteLength;
        
        /// <summary>
        /// Gets or sets the feature report of the controller.
        /// </summary>
        public byte[] Feature
        {
            get
            {
                this.stream.ReadFeature(this.featureBuffer, 0, this.FeatureLength);
                return this.featureBuffer;
            }

            set
            {
                this.stream.WriteFeature(value, 0, this.FeatureLength);
            }
        }

        /// <summary>
        /// Gets a feature value
        /// </summary>
        public byte[] FeatureValue
        {
            get
            {
                this.stream.ReadFeature(this.featureBuffer, 0, this.FeatureLength, 0);
                return this.featureBuffer;
            }

            set
            {
                this.stream.WriteFeature(value, 0, this.FeatureLength, 0);
            }
        }
    }
}
