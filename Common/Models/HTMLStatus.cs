﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class HTMLStatus
	{
		public HTMLStatus() { }
		public HTMLStatus(Status status)
		{
			this.run = status.Run;
			this.runString = status.RunString;
			this.update = status.UpdateDateTime;
			this.updateString = status.UpdateString;
			this.waitTime = status.WaitTime;
			this.attractionId = status.AttractionId;
		}
		public bool run { get; set; } //運営or中止
		public string runString { get; set; }
		public DateTime update { get; set; } //待ち時間が更新された時間(内部)
		public string updateString { get; set; }//待ち時間が実際に操作された時間の文字列
		public int? waitTime { get; set; } //待ち時間（分）
		public int attractionId { get; set; }
	}
}
