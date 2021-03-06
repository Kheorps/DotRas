﻿//--------------------------------------------------------------------------
// <copyright file="RasEnumConnectionsMock.cs" company="Jeff Winn">
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

namespace DotRas.UnitTests.Mocking.Interop
{
    using DotRas.Internal;

    /// <summary>
    /// Provides a mock instance of the <see cref="SafeNativeMethods.EnumConnections"/> method. This class cannot be inherited.
    /// </summary>
    internal sealed class RasEnumConnectionsMock : BufferedPInvokeMock<StructBufferedPInvokeParams, NativeMethods.RASCONN>
    {
        #region Constructors

        #endregion
    }
}