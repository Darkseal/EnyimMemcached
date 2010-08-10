using System;
using System.Collections.Generic;
using System.Text;

namespace Enyim.Caching.Memcached.Operations.Binary
{
	/// <summary>
	/// Implements append/prepend.
	/// </summary>
	internal class ConcatOperation : ItemOperation, IConcatOperation
	{
		private ArraySegment<byte> data;
		private ConcatenationMode mode;

		public ConcatOperation(ConcatenationMode mode, string key, ArraySegment<byte> data)
			: base(key)
		{
			this.data = data;
			this.mode = mode;
		}

		protected internal override IList<ArraySegment<byte>> GetBuffer()
		{
			var request = new BinaryRequest((OpCode)this.mode)
			{
				Key = this.Key,
				Data = this.data
			};

			return request.CreateBuffer();
		}

		protected internal override bool ReadResponse(PooledSocket socket)
		{
			var response = new BinaryResponse();

			return response.Read(socket);
		}

		ConcatenationMode IConcatOperation.Mode
		{
			get { return this.mode; }
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kisk�, enyim.com
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion