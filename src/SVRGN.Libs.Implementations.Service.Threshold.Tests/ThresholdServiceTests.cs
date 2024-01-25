using Microsoft.Extensions.DependencyInjection;
using SVRGN.Libs.Contracts.Service.Logging;
using SVRGN.Libs.Contracts.Service.Object;
using SVRGN.Libs.Contracts.Service.Threshold;
using SVRGN.Libs.Implementations.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVRGN.Libs.Implementations.Service.Threshold.Tests
{
    [TestClass]
    [TestCategory("IThresholdService")]
    public class ThresholdServiceTests
    {
        #region Properties

        private IObjectService objectService;

        #endregion Properties

        #region Methods

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            DiContainer.SetServiceCollection(services);
            //TestBootStrapper.Register(new ServiceCollection());
            TestBootStrapper.Register(services);

            this.objectService = DiContainer.Resolve<IObjectService>();
        }
        #endregion Initialize

        #region Instantiate
        [TestMethod]
        public void Instantiate()
        { 
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>(); 

            IThreshold threshold = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold);
        }
        #endregion Instantiate

        #region Add
        [TestMethod]
        public void Add()
        {
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>();

            IThreshold threshold = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");

            bool wasSuccessful = thresholdService.Add(threshold);

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold);
            Assert.IsTrue(wasSuccessful);
            Assert.AreEqual(1, thresholdService.Thresholds.Count);
        }
        #endregion Add

        #region AddNegative
        [TestMethod]
        public void AddNegative()
        {
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>();

            IThreshold threshold1 = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");
            IThreshold threshold2 = this.objectService.Create<IThreshold>(0f, 0.1f, "DoublePing");

            bool wasSuccessful1 = thresholdService.Add(threshold1);
            bool wasSuccessful2 = thresholdService.Add(threshold2);  // second threshold may not be added due to overlap

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold1);
            Assert.IsNotNull(threshold2);
            Assert.IsTrue(wasSuccessful1);
            Assert.IsFalse(wasSuccessful2);
            Assert.AreEqual(1, thresholdService.Thresholds.Count);
        }
        #endregion AddNegative

        #region Clear
        [TestMethod]
        public void Clear()
        {
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>();

            IThreshold threshold = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");

            thresholdService.Add(threshold);

            int thresholdsBefore = thresholdService.Thresholds.Count;
            thresholdService.Clear();
            int thresholdsAfter = thresholdService.Thresholds.Count;

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold);
            Assert.IsTrue(thresholdsBefore > thresholdsAfter);
            Assert.AreNotEqual(0, thresholdsBefore);
            Assert.AreEqual(0, thresholdsAfter);
        }
        #endregion Clear

        #region SetDefaultText
        [TestMethod]
        public void SetDefaultText()
        {
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>();

            IThreshold threshold = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");

            thresholdService.Add(threshold);

            string newDefaultText = "default";
            thresholdService.SetDefaultText(newDefaultText);

            string defaultText = thresholdService.DefaultText;
            string valueText = thresholdService.GetText(0.2f);

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold);
            Assert.AreEqual(newDefaultText, defaultText);
            Assert.AreEqual(defaultText, valueText);
        }
        #endregion SetDefaultText

        #region GetText
        [TestMethod]
        public void GetText()
        {
            IThresholdService thresholdService = this.objectService.Create<IThresholdService>();

            IThreshold threshold1 = this.objectService.Create<IThreshold>(0f, 0.1f, "Ping");
            IThreshold threshold2 = this.objectService.Create<IThreshold>(0.11f, 0.2f, "Ping2");

            thresholdService.Add(threshold1);
            thresholdService.Add(threshold2);

            string newDefaultText = "default";
            thresholdService.SetDefaultText(newDefaultText);

            string defaultText = thresholdService.DefaultText;
            string valueText = thresholdService.GetText(0.05f);

            Assert.IsNotNull(thresholdService);
            Assert.IsNotNull(threshold1);
            Assert.IsNotNull(threshold2);
            Assert.AreNotEqual(defaultText, valueText);
            Assert.AreEqual("Ping", valueText);
        }
        #endregion GetText

        #endregion Methods
    }
}
