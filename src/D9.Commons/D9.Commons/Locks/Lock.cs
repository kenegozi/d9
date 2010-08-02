#region License

// Copyright (c) 2008-2010 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

using System;
using System.Threading;

namespace D9.Commons.Locks
{
	public class Lock
	{
		public static T ForRead<T>(ReaderWriterLockSlim locker, Func<T> readAction)
		{
			locker.EnterReadLock();
			try
			{
				return readAction();
			}
			finally
			{
				locker.ExitReadLock();
			}
		}

		public static void ForRead(ReaderWriterLockSlim locker, Action readAction)
		{
			locker.EnterReadLock();
			try
			{
				readAction();
			}
			finally
			{
				locker.ExitReadLock();
			}
		}

		public static void ForUpgradeableRead(ReaderWriterLockSlim locker, Action readAction, Func<bool> shouldUpgradeToWrite, Action writeAction)
		{
			locker.EnterUpgradeableReadLock();
			try
			{
				readAction();
				if (shouldUpgradeToWrite() == false) return;
				ForWrite(locker, writeAction);
			}
			finally
			{
				locker.ExitUpgradeableReadLock();
			}
		}

		public static T ForUpgradeableRead<T>(ReaderWriterLockSlim locker, Func<T> readAction, Func<bool> shouldUpgradeToWrite, Func<T> writeAction)
		{
			locker.EnterUpgradeableReadLock();
			try
			{
				var result = readAction();
				if (shouldUpgradeToWrite() == false) return result;
				return ForWrite(locker, writeAction);
			}
			finally
			{
				locker.ExitUpgradeableReadLock();
			}
		}

		public static void ForWrite(ReaderWriterLockSlim locker, Action writeAction)
		{
			locker.EnterWriteLock();
			try
			{
				writeAction();
			}
			finally
			{
				locker.ExitWriteLock();
			}
		}
		public static T ForWrite<T>(ReaderWriterLockSlim locker, Func<T> writeAction)
		{
			locker.EnterWriteLock();
			try
			{
				return writeAction();
			}
			finally
			{
				locker.ExitWriteLock();
			}
		}
	}
}