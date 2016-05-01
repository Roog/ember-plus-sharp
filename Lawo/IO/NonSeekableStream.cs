﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright 2008-2012 Andreas Huber Doenni, original from http://phuse.codeplex.com.
// <copyright>Copyright 2012-2016 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>Overrides <see cref="Stream"/> members associated with seeking so that they throw
    /// <see cref="NotSupportedException"/>.</summary>
    /// <threadsafety static="true" instance="false"/>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Type derives from Stream, CA bug?")]
    public abstract class NonSeekableStream : Stream
    {
        private static readonly Task Completed = Task.FromResult(false);

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Asynchronously disposes resources.</summary>
        [CLSCompliant(false)]
        public virtual Task DisposeAsync(CancellationToken cancellationToken)
        {
            return Completed;
        }

        /// <inheritdoc/>
        public override bool CanRead
        {
            get { return false; }
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw CreateNotSupportedException();
        }

        /// <inheritdoc/>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw CreateNotSupportedException();
        }

        /// <inheritdoc/>
        /// <value>Always <c>false</c>, even if the underlying stream supports seeking.</value>
        public sealed override bool CanSeek
        {
            get { return false; }
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">Thrown with each call.</exception>
        public sealed override long Length
        {
            get { throw CreateNotSupportedException(); }
        }

        /// <summary>See <see cref="Stream.SetLength"/>.</summary>
        /// <exception cref="NotSupportedException">Thrown with each call.</exception>
        public sealed override void SetLength(long value)
        {
            throw CreateNotSupportedException();
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">Thrown with each get or set operation.</exception>
        public sealed override long Position
        {
            get { throw CreateNotSupportedException(); }
            set { throw CreateNotSupportedException(); }
        }

        /// <summary>See <see cref="Stream.Seek"/>..</summary>
        /// <exception cref="NotSupportedException">Thrown with each call.</exception>
        public sealed override long Seek(long offset, SeekOrigin origin)
        {
            throw CreateNotSupportedException();
        }

        /// <summary>See <see cref="Stream.Flush"/>.</summary>
        /// <remarks>This method does nothing, derived classes should override as necessary.</remarks>
        public override void Flush()
        {
        }

        /// <summary>See <see cref="Stream.FlushAsync(CancellationToken)"/>.</summary>
        /// <remarks>This method does nothing, derived classes should override as necessary.</remarks>
        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Completed;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Initializes a new instance of the <see cref="NonSeekableStream"/> class.</summary>
        protected NonSeekableStream()
        {
        }

        /// <summary>Checks whether the object has been disposed and if so throws <see cref="ObjectDisposedException"/>.
        /// </summary>
        protected void AssertNotDisposed()
        {
            if (!this.CanRead && !this.CanWrite)
            {
                throw new ObjectDisposedException(this.ToString());
            }
        }

        /// <summary>Creates the exception thrown by all unsupported members.</summary>
        protected static Exception CreateNotSupportedException()
        {
            return new NotSupportedException("This operation is not supported.");
        }
    }
}
