using System;
namespace WebClip.IOS.PCL
{
	public class EventFiles
	{
		public long ID { get; set; }
		public long UserID { get; set; }
		public long EventID { get; set; }
		public string EventName { get; set; }
		public double Duration { get; set; }
		public string ThumbUrl { get; set; }
		public string ThumbName { get; set; }
		public bool IsVideo { get; set; }
		public string FileUrl { get; set; }
		public string FileName { get; set; }

		private bool selected;

		public bool isSelected()
		{
			return selected;
		}
		public void setSelected(bool selected)
		{
			this.selected = selected;
		}
		public long uploaderID { get; set; }

	}
}
