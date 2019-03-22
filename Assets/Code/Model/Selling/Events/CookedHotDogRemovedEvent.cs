namespace Assets.Code.Model.Selling.Events
{
	public class CookedHotDogRemovedEvent : GrillEvent
	{
		public int Index { get; }

		public CookedHotDogRemovedEvent(int index)
		{
			Index = index;
		}
	}
}