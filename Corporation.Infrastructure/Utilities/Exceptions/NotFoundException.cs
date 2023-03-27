using System;
namespace Corporation.Infrastructure.Utilities.Exceptions
{
	public class NotFoundException:Exception
	{
		public NotFoundException(string msg):base(msg)
		{
		}
	}
}

