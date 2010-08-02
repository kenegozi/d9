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