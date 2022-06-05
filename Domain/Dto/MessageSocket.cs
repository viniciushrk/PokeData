using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class MessageSocket
	{
		public string Event { get; set; }
		public string Room { get; set; }
		public object Data { get; set; }
	}
}
