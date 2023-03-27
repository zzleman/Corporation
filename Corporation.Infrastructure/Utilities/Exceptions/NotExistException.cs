using System;
namespace Corporation.Infrastructure.Utilities.Exceptions
{
	public class NotExistException:Exception
	{
		public NotExistException(string msg):base(msg)
		{
		}
	}
}

