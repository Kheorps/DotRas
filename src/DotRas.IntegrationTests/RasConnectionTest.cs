﻿//--------------------------------------------------------------------------
// <copyright file="RasConnectionTest.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas.IntegrationTests
{
    using System;
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasConnection"/> and is intended to contain all associated integration tests.
    /// </summary>
    [TestClass]
    public class RasConnectionTest : IntegrationTest
    {
        #region Fields

        private static RasHandle handle;
        private static string phonebookPath;
        private static string entryName;
        private static Guid entryId;

        private static RasConnection target;

        #endregion

        #region Constructors

        #endregion

        #region Methods

        #region ~ Test Class Init

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        /// <param name="context">The text context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            entryName = Guid.NewGuid().ToString();
            phonebookPath = Path.GetTempFileName();

            var pbk = new RasPhoneBook();
            pbk.Open(phonebookPath);

            entryId = TestUtilities.CreateValidVpnEntry(pbk, entryName);

            using (var dialer = new RasDialer())
            {
                dialer.EntryName = entryName;
                dialer.PhoneBookPath = phonebookPath;
                dialer.Credentials = TestUtilities.GetValidCredentials();

                handle = dialer.Dial();
            }

            target = RasConnection.GetActiveConnections().Where(o => o.EntryId == entryId).FirstOrDefault();
        }

        /// <summary>
        /// Performs cleanup for the test class.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanUp()
        {
            var connection = RasConnection.GetActiveConnections().Where(o => o.Handle == handle).FirstOrDefault();
            connection?.HangUp();

            if (File.Exists(phonebookPath))
            {
                File.Delete(phonebookPath);
            }
        }

        #endregion

        #region ~ Test Methods Init

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region ClearConnectionStatistics

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure the expected value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void ClearConnectionStatisticsTest()
        {
            var expected = true;
            var actual = target.ClearConnectionStatistics();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ClearLinkStatistics

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure a value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void ClearLinkStatisticsTest()
        {
            var expected = true;
            var actual = target.ClearLinkStatistics();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetActiveConnections

        /// <summary>
        /// Tests the GetActiveConnections method to ensure the target connection is contained in the resulting collection.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void GetActiveConnectionsTest()
        {
            var connections = RasConnection.GetActiveConnections();

            var s = (from c in connections
                    where c.Handle == handle
                    select c).SingleOrDefault();

            Assert.IsNotNull(s);
        }

        #endregion

        #region GetConnectionStatistics

        /// <summary>
        /// Tests the GetConnectionStatistics method to ensure a value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void GetConnectionStatisticsTest()
        {
            var actual = target.GetConnectionStatistics();

            Assert.IsNotNull(actual);
        }

        #endregion

        #region GetConnectionStatus

        /// <summary>
        /// Tests the GetConnectionStatus method to ensure a value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void GetConnectionStatusTest()
        {
            var actual = target.GetConnectionStatus();
            
            Assert.IsNotNull(actual);
        }

        #endregion

        #region GetLinkStatistics

        /// <summary>
        /// Tests the GetLinkStatistics method to ensure a value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void GetLinkStatisticsTest()
        {
            var actual = target.GetLinkStatistics();

            Assert.IsNotNull(actual);
        }

        #endregion

        #endregion
    }
}