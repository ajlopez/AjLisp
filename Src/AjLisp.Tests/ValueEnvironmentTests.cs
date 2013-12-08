namespace AjLisp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValueEnvironmentTests
    {
        private ValueEnvironment environment;

        [TestInitialize]
        public void Setup()
        {
            this.environment = new ValueEnvironment();
        }

        [TestMethod]
        public void GetUndefinedValueAsNull()
        {
            Assert.IsNull(this.environment.GetValue("undefined"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            this.environment.SetValue("foo", "bar");
            Assert.AreEqual("bar", this.environment.GetValue("foo"));
        }

        [TestMethod]
        public void SetResetAndGetValue()
        {
            this.environment.SetValue("foo", "bar");
            this.environment.SetValue("foo", "bar2");
            Assert.AreEqual("bar2", this.environment.GetValue("foo"));
        }

        [TestMethod]
        public void SetGlobalValueInTopEnvironment()
        {
            this.environment.SetGlobalValue("foo", "bar");
            Assert.AreEqual("bar", this.environment.GetValue("foo"));
        }

        [TestMethod]
        public void SetGlobalValueInTopEnvironmentUsingChild()
        {
            ValueEnvironment child = new ValueEnvironment(this.environment);
            child.SetGlobalValue("foo", "bar");
            Assert.AreEqual("bar", this.environment.GetValue("foo"));
            Assert.AreEqual("bar", child.GetValue("foo"));
        }

        [TestMethod]
        public void SetGlobalAndLocalValue()
        {
            ValueEnvironment child = new ValueEnvironment(this.environment);
            child.SetGlobalValue("foo", "bar");
            child.SetValue("foo", "bar2");
            Assert.AreEqual("bar", this.environment.GetValue("foo"));
            Assert.AreEqual("bar2", child.GetValue("foo"));
        }
    }
}
