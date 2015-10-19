﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2015 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.EmberPlus.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>The exception that is thrown when the invocation of a function fails.</summary>
    /// <threadsafety static="true" instance="false"/>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "These ctors do not make much sense here.")]
    public sealed class InvocationFailedException : Exception
    {
        private readonly IResult result;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets the function result.</summary>
        public IResult Result
        {
            get { return this.result; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        internal InvocationFailedException(string message, IResult result) : base(message)
        {
            this.result = result;
        }
    }
}