using System.Runtime.InteropServices;
using Sandbox;
using Sandbox.Mapping;

public sealed class NativeButtonComponentTest : Component, Component.ITriggerListener, Component.IPressable
{
	[Property] TextRenderer StateLabel {get; set;}
	[Property] TextRenderer PressCountLabel {get; set;}

	int _pressCount = 0;

	protected override void OnAwake()
	{
		StateLabel.Text = "Not pressed";
	}

	public void OnTriggerEnter(Collider other)
	{
		if (!other.GameObject.Tags.Has("player")) return;
		var pressEvent = new IPressable.Event(other, Ray: null);
		Press(pressEvent);
	}
	
	public void OnTriggerExit(Collider other)
	{
		if (!other.GameObject.Tags.Has("player")) return;
		var releaseEvent = new IPressable.Event(other, Ray: null);
		Release(releaseEvent);
	}
	
	public bool Press(IPressable.Event @event)
	{
		Log.Info("Press");
		StateLabel.Text = "Is pressed";
		_pressCount += 1;
		PressCountLabel.Text = _pressCount.ToString();
		return true;
	}

	public void Release(IPressable.Event @event)
	{
		Log.Info("Release");
		StateLabel.Text = "Not pressed";
	}

	public void Hover(IPressable.Event @event)
	{
		Log.Info("Hovering");
	}

	public void Blur(IPressable.Event @event)
	{
	}
}
