using Sandbox;
using Sandbox.Mapping;

public sealed class CustomButtonComponent : Component, Component.ITriggerListener, Component.ICollisionListener, Component.IPressable
{
	[Property] GameObject ButtonProp {get; set;}
	[Property] TextRenderer StateLabel {get; set;}
	[Property] TextRenderer PressCountLabel {get; set;}

	Vector3 _startingPosition = new Vector3(0, 0, 0);
	Vector3 _pressedPosition = new Vector3(0, 0, 0);
	Vector3 _pressedPosition_offset = new Vector3(0, 0, -4f);

	bool statePressing = false;
	bool stateIsPressed = false;
	bool stateNotPressed = false;
	int _pressCount = 0;

	protected override void OnAwake()
	{
		_startingPosition = ButtonProp.LocalPosition;
		_pressedPosition = _startingPosition + _pressedPosition_offset;
		StateLabel.Text = "Not pressed";
	}

	protected override void OnUpdate()
	{
		if (stateIsPressed)
		{
			StateLabel.Text = "Is pressed";
			_pressCount += 1;
			PressCountLabel.Text = _pressCount.ToString();
			stateIsPressed = false;
		}
		else if (statePressing)
		{
			StateLabel.Text = "Is pressing";
			ButtonProp.LocalPosition = ButtonProp.LocalPosition.LerpTo(_pressedPosition, Time.Delta * 5f);
			if (ButtonProp.LocalPosition.DistanceSquared(_pressedPosition) < 0.01f)
			{
				stateIsPressed = true;
				statePressing = false;
			}
		}
		else if (stateNotPressed)
		{
			StateLabel.Text = "Not pressed";
			ButtonProp.LocalPosition = ButtonProp.LocalPosition.LerpTo(_startingPosition, Time.Delta * 5f);
			if (ButtonProp.LocalPosition.DistanceSquared(_startingPosition) < 0.01f)
			{
				stateNotPressed = true;
			}
		}
	}


	public void OnTriggerEnter(Collider other)
	{
		if (!other.GameObject.Tags.Has("player")) return;
		statePressing = true;
		stateNotPressed = false;
	}

	public void OnTriggerExit(Collider other)
	{
		if (!other.GameObject.Tags.Has("player")) return;
		stateNotPressed = true;
		statePressing = false;
	}
	

	public bool Press(IPressable.Event @event)
	{
		Log.Info("Press");
		statePressing = true;
		stateNotPressed = false;

		return true;
	}

	public void Release(IPressable.Event @event)
	{
		Log.Info("Release");
		stateIsPressed = false;
		stateNotPressed = true;
	}

	public void Hover(IPressable.Event @event)
	{
		Log.Info("Hovering");
	}

	public void Pressing(IPressable.Event @event)
	{
		Log.Info("Pressing");
	}

	public void Blur(IPressable.Event @event)
	{
		Log.Info("Blur");
	}
}
