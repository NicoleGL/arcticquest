using Godot;
using System;

public partial class Penguin : Sprite2D
{
	#region input
		private Vector2 input;
		private Vector2 lastInput;
	
		private void GatherInput() {
			input = Input.GetVector("Left", "Right", "Up", "Down", 0f); //Gets the input
			if (input.X != 0f || input.Y != 0f){ //Doesn't work. Changes lastInput only if input is not (0,0)
				lastInput.X = input.X;
				lastInput.Y = input.Y;
			}
		}
	#endregion
	
	#region velocity
		private float speed = 5f; //Here is the speed, the one that shoud be changed if someone change the penguin's speed
		private float decelerationCoeficient = 1f; //doesn't work. The coeficient for deceleration
		
		private Vector2 CalculateVelocity(Vector2 inp, float del){
			Vector2 vel = new Vector2();
			if (inp.X > 0 || inp.X < 0 || inp.Y > 0 || inp.Y < 0){ //here velocity is calculated
				vel.X = inp.X * speed;
				vel.Y = inp.Y * speed;
			}
			else{ //doesn't work. Here deceleration is calculated
				if (decelerationCoeficient * del < Mathf.Abs(vel.X) && lastInput.X != 0f){
					if (vel.X < 0f){
						vel.X += decelerationCoeficient * del * 0.001f;
					}
					else {
						vel.X -= decelerationCoeficient * del * 0.001f;
					}
				}
				else if (lastInput.X != 0f){
					vel.X = 0f;
				}
				if (decelerationCoeficient * del < Mathf.Abs(vel.Y) && lastInput.Y != 0f){
					if (vel.Y < 0f){
						vel.Y += decelerationCoeficient * del * 0.001f;
					}
					else {
						vel.Y -= decelerationCoeficient * del * 0.001f;
					}
				}
				else if (lastInput.Y != 0f){
					vel.Y = 0f;
				}
			}
			return vel;
		}
	#endregion
	
	#region animation
		private void ChangeAnimation(Vector2 inp){ //For changing the frame depending on input value
			if (inp.Y < 0){
				this.Frame = 2;
				return;
			}
			else if (inp.Y > 0){
				this.Frame = 0;
				return;
			}
			if (inp.X > 0){
				this.Frame = 3;
			}
			else if (inp.X < 0){
				this.Frame = 1;
			}
		}
	#endregion
	
	#region process
		private Vector2 velocity;
	
		public override void _Ready (){}
	
		public override void _PhysicsProcess(double delta){ //here godot gets input, calculate velocity and change position, also change the frame...
			//Due to changing position must be fluid, PhysicsProcess is used instead of Process
			this.GatherInput();
			this.ChangeAnimation(input);
			this.velocity = CalculateVelocity(input, (float)delta);
			this.Position += velocity;
		}
	#endregion
}
